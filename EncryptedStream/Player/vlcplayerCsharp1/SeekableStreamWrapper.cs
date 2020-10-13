using System;
using System.IO;

namespace vlcplayerCsharp1
{

    /// <summary>
    /// A wrapper that destroys and creates a stream on demand to be able to seek into non-seekable stream...
    /// </summary>
    public class SeekableStreamWrapper : Stream
    {
        private readonly Func<Stream> _createStream;
        private Stream _innerStream;
        private long _position = 0;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="createStream"></param>
        public SeekableStreamWrapper(Func<Stream> createStream)
        {
            this._createStream = createStream;
            this._innerStream = createStream();
        }

        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => false;

        public override long Length => this._innerStream.Length;

        public override long Position { get => this._position; set => this.Seek(value, SeekOrigin.Begin); }

        public override void Flush() => this._innerStream.Flush();

        public override int Read(byte[] buffer, int offset, int count)
        {
            var read = this._innerStream.Read(buffer, offset, count);
            this._position += read;
            return read;
        }


        public override long Seek(long offset, SeekOrigin origin)
        {
            if (origin != SeekOrigin.Begin)
            {
                throw new NotSupportedException("Only supports seeking from the beginning");
            }

            long remaining = 0;
            if (offset > this._position)
            {
                remaining = offset - this._position;
            }
            else
            {
                try
                {
                    this._innerStream.Dispose();
                }
                catch (Exception)
                {
                    // Shit happens... On .NET framework, calling Dispose() on a crypto stream without having read anything can throw...
                }
                this._innerStream = this._createStream();
                remaining = offset;
            }

            var buffer = new byte[Math.Min(0x100_000, remaining)];
            while (remaining > 0)
            {
                var read = this._innerStream.Read(buffer, 0, Math.Min(0x100_000, (remaining > int.MaxValue) ? int.MaxValue : (int)remaining));
                remaining -= read;
            }

            this._position = offset;
            return offset;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._innerStream.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}