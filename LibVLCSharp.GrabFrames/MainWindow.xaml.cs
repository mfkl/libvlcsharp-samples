using LibVLCSharp.Shared;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using MediaPlayer = LibVLCSharp.Shared.MediaPlayer;

namespace LibVLCSharp.GrabFrames
{
    public partial class MainWindow : Window
    {
        LibVLC _libvlc;
        MediaPlayer _mp;

        /// <summary>
        /// The memory mapped file handle that contains the picture data
        /// </summary>
        private IntPtr memoryMappedFile;

        /// <summary>
        /// The pointer to the buffer that contains the picture data
        /// </summary>
        private IntPtr memoryMappedView;
        private ImageSource VideoSource;

        public MainWindow()
        {
            InitializeComponent();

            Core.Initialize();

            Loaded += MainWindow_Loaded;
        }

        #region interop
        /// <summary>
        /// Creates or opens a named or unnamed file mapping object for a specified file.
        /// </summary>
        /// <param name="hFile">A handle to the file from which to create a file mapping object.</param>
        /// <param name="lpAttributes">A pointer to a SECURITY_ATTRIBUTES structure that determines whether a returned handle can be inherited by child processes. The lpSecurityDescriptor member of the SECURITY_ATTRIBUTES structure specifies a security descriptor for a new file mapping object.</param>
        /// <param name="flProtect">Specifies the page protection of the file mapping object. All mapped views of the object must be compatible with this protection.</param>
        /// <param name="dwMaximumSizeLow">The high-order DWORD of the maximum size of the file mapping object.</param>
        /// <param name="dwMaximumSizeHigh">The low-order DWORD of the maximum size of the file mapping object.</param>
        /// <param name="lpName">The name of the file mapping object.</param>
        /// <returns>The value is a handle to the newly created file mapping object.</returns>
        [DllImport("kernel32", SetLastError = true)]
        public static extern IntPtr CreateFileMapping(IntPtr hFile, IntPtr lpAttributes, PageAccess flProtect, int dwMaximumSizeLow, int dwMaximumSizeHigh, string lpName);

        /// <summary>
        /// Maps a view of a file mapping into the address space of a calling process.
        /// </summary>
        /// <param name="hFileMappingObject">A handle to a file mapping object. The CreateFileMapping and OpenFileMapping functions return this handle.</param>
        /// <param name="dwDesiredAccess">The type of access to a file mapping object, which determines the protection of the pages. This parameter can be one of the following values.</param>
        /// <param name="dwFileOffsetHigh">A high-order DWORD of the file offset where the view begins.</param>
        /// <param name="dwFileOffsetLow">A low-order DWORD of the file offset where the view is to begin. The combination of the high and low offsets must specify an offset within the file mapping.</param>
        /// <param name="dwNumberOfBytesToMap">The number of bytes of a file mapping to map to the view. All bytes must be within the maximum size specified by CreateFileMapping. If this parameter is 0 (zero), the mapping extends from the specified offset to the end of the file mapping.</param>
        /// <returns>The value is the starting address of the mapped view.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, FileMapAccess dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

        /// <summary>
        /// Unmaps a mapped view of a file from the calling process's address space.
        /// </summary>
        /// <param name="lpBaseAddress">A pointer to the base address of the mapped view of a file that is to be unmapped. This value must be identical to the value returned by a previous call to the MapViewOfFile or MapViewOfFileEx function.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport("kernel32", SetLastError = true)]
        public static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="handle">A valid handle to an open object.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport("kernel32", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr handle);

        public enum PageAccess
        {
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            Guard = 0x100,
            NoCache = 0x200,
            WriteCombine = 0x400
        }

        public enum FileMapAccess : uint
        {
            Write = 0x00000002,
            Read = 0x00000004,
            AllAccess = 0x000f001f,
            Copy = 0x00000001,
            Execute = 0x00000020
        }
        #endregion


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _libvlc = new LibVLC();
            _mp = new MediaPlayer(_libvlc);
            _mp.SetVideoCallbacks(LockVideo, null, DisplayVideo);
            _mp.SetVideoFormatCallbacks(VideoFormat, CleanupVideo);
            _mp.Play(new Media(_libvlc, "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4", FromType.FromLocation));
        }

        /// <summary>
        /// Called by vlc when the video format is needed. This method allocats the picture buffers for vlc and tells it to set the chroma to RV32
        /// </summary>
        /// <param name="userdata">The user data that will be given to the <see cref="LockVideo"/> callback. It contains the pointer to the buffer</param>
        /// <param name="chroma">The chroma</param>
        /// <param name="width">The visible width</param>
        /// <param name="height">The visible height</param>
        /// <param name="pitches">The buffer width</param>
        /// <param name="lines">The buffer height</param>
        /// <returns>The number of buffers allocated</returns>
        private uint VideoFormat(out IntPtr userdata, IntPtr chroma, ref uint width, ref uint height, ref uint pitches, ref uint lines)
        {
            var pixelFormat = PixelFormats.Bgr32;

            var bytes = Encoding.ASCII.GetBytes("RV32");
            for (var i = 0; i < 4; i++)
            {
                Marshal.WriteByte(chroma, i, bytes[i]);
            }

            //Correct video width and height according to TrackInfo
            var md = _mp.Media;
            foreach (MediaTrack track in md.Tracks)
            {
                if (track.TrackType == TrackType.Video)
                {
                    var trackInfo = track.Data.Video;
                    if (trackInfo.Width > 0 && trackInfo.Height > 0)
                    {
                        width = trackInfo.Width;
                        height = trackInfo.Height;
                        if (trackInfo.SarDen != 0)
                        {
                            width = width * trackInfo.SarNum / trackInfo.SarDen;
                        }
                    }

                    break;
                }
            }

            pitches = this.GetAlignedDimension((uint)(width * pixelFormat.BitsPerPixel) / 8, 32);
            lines = this.GetAlignedDimension(height, 32);

            var size = pitches * lines;

            this.memoryMappedFile = CreateFileMapping(new IntPtr(-1), IntPtr.Zero, PageAccess.ReadWrite, 0, (int)size, null);
            var handle = memoryMappedFile;

            var args = new
            {
                width,
                height,
                pixelFormat,
                pitches
            };

            Dispatcher.Invoke((Action)(() =>
            {
                VideoSource = (InteropBitmap)Imaging.CreateBitmapSourceFromMemorySection(handle,
                    (int)args.width, (int)args.height, args.pixelFormat, (int)args.pitches, 0);
            }));


            memoryMappedView = MapViewOfFile(memoryMappedFile, FileMapAccess.AllAccess, 0, 0, size);
            var viewHandle = memoryMappedView;

            userdata = viewHandle;
            return 1;
        }

        /// <summary>
        /// Called by Vlc when it requires a cleanup
        /// </summary>
        /// <param name="userdata">The parameter is not used</param>
        private void CleanupVideo(ref IntPtr userdata)
        {
            // This callback may be called by Dispose in the Dispatcher thread, in which case it deadlocks if we call RemoveVideo again in the same thread.
            if (!disposedValue)
            {
                Dispatcher.Invoke((Action)this.RemoveVideo);
            }
        }

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
                _mp?.Dispose();
                _mp = null;
                Dispatcher.BeginInvoke((Action)RemoveVideo);
            }
        }

        private void RemoveVideo()
        {
            VideoSource = null;

            if (memoryMappedFile != IntPtr.Zero)
            {
                UnmapViewOfFile(memoryMappedView);
                memoryMappedView = IntPtr.Zero;
                CloseHandle(memoryMappedFile);
                memoryMappedFile = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Called by libvlc when it wants to acquire a buffer where to write
        /// </summary>
        /// <param name="userdata">The pointer to the buffer (the out parameter of the <see cref="VideoFormat"/> callback)</param>
        /// <param name="planes">The pointer to the planes array. Since only one plane has been allocated, the array has only one value to be allocated.</param>
        /// <returns>The pointer that is passed to the other callbacks as a picture identifier, this is not used</returns>
        private IntPtr LockVideo(IntPtr userdata, IntPtr planes)
        {
            Marshal.WriteIntPtr(planes, userdata);
            return userdata;
        }

        /// <summary>
        /// Called by libvlc when the picture has to be displayed.
        /// </summary>
        /// <param name="userdata">The pointer to the buffer (the out parameter of the <see cref="VideoFormat"/> callback)</param>
        /// <param name="picture">The pointer returned by the <see cref="LockVideo"/> callback. This is not used.</param>
        private void DisplayVideo(IntPtr userdata, IntPtr picture)
        {
            // Invalidates the bitmap
            Dispatcher.BeginInvoke(() =>
            {
                (VideoSource as InteropBitmap)?.Invalidate();
            });
        }

        /// <summary>
        /// Aligns dimension to the next multiple of mod
        /// </summary>
        /// <param name="dimension">The dimension to be aligned</param>
        /// <param name="mod">The modulus</param>
        /// <returns>The aligned dimension</returns>
        private uint GetAlignedDimension(uint dimension, uint mod)
        {
            var modResult = dimension % mod;
            if (modResult == 0)
            {
                return dimension;
            }

            return dimension + mod - (dimension % mod);
        }
    }
}
