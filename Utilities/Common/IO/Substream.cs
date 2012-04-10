using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AllenCopeland.Abstraction.IO
{
    /// <summary>
    /// Provides a means of simplifying access to a multifaceted data
    /// structure which points to other sections of the same structure
    /// through data pointers.
    /// </summary>
    /// <remarks>
    /// Typically for structures which were developed in an iterative
    /// manner.</remarks>
    public sealed class Substream :
        Stream
    {
        private Stream baseStream;
        private long offset;
        private long position;
        private long length;
        private bool isFixedLength;

        /// <summary>
        /// Returns whether the <see cref="Substream"/> can be
        /// read from.
        /// </summary>
        public override bool CanRead
        {
            get { return baseStream.CanRead; }
        }

        /// <summary>
        /// Returns whether the <see cref="Substream"/> can change 
        /// locations within the dataset at will.
        /// </summary>
        public override bool CanSeek
        {
            get { return baseStream.CanSeek; }
        }

        /// <summary>
        /// Returns whether the <see cref="Substream"/> can
        /// be written to.
        /// </summary>
        public override bool CanWrite
        {
            get { return baseStream.CanWrite; }
        }

        /// <summary>
        /// Flushes any possible buffers used by the 
        /// <see cref="BaseStream"/>
        /// </summary>
        public override void Flush()
        {
            baseStream.Flush();
        }

        /// <summary>
        /// Returns the <see cref="Stream"/> which the
        /// <see cref="Substream"/> represents a portion of.
        /// </summary>
        public Stream BaseStream { get { return this.baseStream; } }

        /// <summary>
        /// Returns the <see cref="Int64"/> value which denotes
        /// the length of the <see cref="Substream"/> in bytes.
        /// </summary>
        public override long Length
        {
            get { return this.length; }
        }

        /// <summary>
        /// Returns/sets the <see cref="Int64"/> value wich denotes the
        /// location within the <see cref="Substream"/> at which reads
        /// and writes occur.
        /// </summary>
        public override long Position
        {
            get
            {
                return this.position;
            }
            set
            {
                if (value < 0 ||
                    value >= this.Length)
                    throw new ArgumentOutOfRangeException("value");
                this.position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            this.CheckSeekStatus();
            var result = this.baseStream.Read(buffer, offset, count);
            this.position += result;
            return result;
        }

        private void CheckSeekStatus()
        {
            if (baseStream.Position - this.offset != this.position)
                this.baseStream.Seek(this.offset + this.position, SeekOrigin.Begin);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    return this.position = offset;
                case SeekOrigin.Current:
                    return this.position = this.position + offset;
                case SeekOrigin.End:
                    return this.Seek(this.Length - (offset + 1), SeekOrigin.Begin);
            }
            return this.position;
        }

        public override void SetLength(long value)
        {
            if (this.isFixedLength)
                throw new NotSupportedException();
            this.CheckSeekStatus();
            if (this.offset + length > this.baseStream.Length)
                this.baseStream.SetLength(this.offset + length);
            this.length = value;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.CheckSeekStatus();
            this.baseStream.Write(buffer, offset, count);
        }

        /// <summary>
        /// Creates a new <see cref="Substream"/> instance
        /// with the <paramref name="baseStream"/>, <paramref name="offset"/>,
        /// <paramref name="length"/>, and <paramref name="fixedLength"/>
        /// values provided.
        /// </summary>
        /// <param name="baseStream">The <see cref="Stream"/>
        /// the current <see cref="Substream"/> represents
        /// a portion of.</param>
        /// <param name="offset">The <see cref="Int64"/> value
        /// which denotes where within the <paramref name="baseStream"/>
        /// the <see cref="Substream"/> starts.</param>
        /// <param name="length">The <see cref="Int64"/> value which
        /// denotes how much of the <paramref name="baseStream"/>
        /// is taken up by the <see cref="Substream"/>.
        /// </param>
        /// <param name="fixedLength">Whether or not the <see cref="Substream"/>
        /// is fixed in length.</param>
        public Substream(Stream baseStream, long offset, long length, bool fixedLength = true)
        {
            AdjustSubstream(ref baseStream, ref offset);
            this.baseStream = baseStream;
            this.offset = offset;
            this.length = length;
            this.isFixedLength=fixedLength;
        }

        private void AdjustSubstream(ref Stream baseStream, ref long offset)
        {
            if (baseStream is Substream)
            {
                var baseSubstream = baseStream as Substream;
                var realSubstream = baseSubstream.BaseStream;
                offset += baseSubstream.offset;
                baseStream = realSubstream;
                //Further reduce the stream, probably shouldn't happen.
                AdjustSubstream(ref baseStream, ref offset);
            }
        }

        /// <summary>
        /// Returns/sets the <see cref="Int64"/> value which determines
        /// the offset applied to every read/write/seek.
        /// </summary>
        public long Offset { get { return this.offset; } set { this.offset = value; } }

        public override void Close()
        {

        }
    }
}
