using System;
using System.Runtime.InteropServices;

namespace LibVLCFadeAnimation.Environments
{
    public static partial class LinuxApi
    {
        private static IntPtr display = IntPtr.Zero;

        #region Initializing and Shutdown
        /// <summary>
        /// Initializes the X threading system (only Linux X11)
        /// </summary>
        [DllImport("libX11", CallingConvention = CallingConvention.Cdecl)]
        public static extern int XInitThreads();

        [DllImport("libX11")]
        private static extern IntPtr XOpenDisplay(string? display);

        [DllImport("libX11")]
        private static extern int XCloseDisplay(IntPtr display);

        public static void Initialize()
        {
            // sets up LibVLC: initialize multithreading support
            XInitThreads();

            // opening x-server connection
            display = XOpenDisplay(null);
            if (display == IntPtr.Zero)
            {
                // TODO
                throw new NotImplementedException("Failed to open X display.");
            }
        }

        public static void Shutdown()
        {
            if (display != IntPtr.Zero)
            {
                XCloseDisplay(display);
                display = IntPtr.Zero;
            }
        }
        #endregion



        #region Opacity
        [DllImport("libX11")]
        private static extern int XChangeProperty(
            IntPtr display,
            IntPtr window,
            IntPtr property,
            IntPtr type,
            int format,
            int mode,
            ref ulong data,
            int nelements);

        [DllImport("libX11")]
        private static extern IntPtr XInternAtom(IntPtr display, string atom_name, bool only_if_exists);

        [DllImport("libX11")]
        private static extern int XFlush(IntPtr display);

        public static void SetWindowOpacity(IntPtr windowId, double opacity)
        {
            ulong opacityValue = (ulong)(opacity * 0xFFFFFFFF);

            IntPtr atom = XInternAtom(display, "_NET_WM_WINDOW_OPACITY", false);
            XChangeProperty(display, windowId, atom, (IntPtr)6 /* XA_CARDINAL */, 32, 0 /* PropModeReplace */, ref opacityValue, 1);
            XFlush(display);
        }
        #endregion
    }
}
