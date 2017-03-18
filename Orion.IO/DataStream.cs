/*
MIT License

Copyright (c) 2017 Robert Lodico

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.IO;
using System.Text;

namespace Orion.IO
{
    public class DataStream : Stream, IBinarySerializer
    {
        #region Properties
        private Stream mInternalBuffer;
        protected Stream InternalBuffer {
            get {
                if (mInternalBuffer == null)
                {
                    mInternalBuffer = new MemoryStream();
                }

                return mInternalBuffer;
            }

            set
            {
                mInternalBuffer = value;
            }
        }
        #endregion Properties

        #region Constructor
        public DataStream(int capacity = 0) : this(new MemoryStream(capacity))
        {
            /* Do nothing else. */
        }

        public DataStream(Stream internalBuffer)
        {
            this.mInternalBuffer = internalBuffer;
        }
        #endregion Constructor

        #region Utility
        public virtual void Clear()
        {
            /* Don't bother re-creating the stream if it's already empty. */
            if (InternalBuffer.Length != 0)
            {
                InternalBuffer.SetLength(0);
                InternalBuffer.Position = 0;
            }
        }
        #endregion Utility

        #region Read
        public virtual bool ReadBool(long position = -1)
        {
            return BitConverter.ToBoolean(ReadBytes(sizeof(bool), position), 0);
        }

        public virtual bool Read(ref bool value, long position = -1)
        {
            return (value = ReadBool(position));
        }

        public virtual byte ReadByte(long position = -1)
        {
            var currentPosition = InternalBuffer.Position;
            if (position > -1) { InternalBuffer.Position = position; }
            byte value = (byte)(InternalBuffer.ReadByte() & 0xFF);
            if (position > -1) { InternalBuffer.Position = currentPosition; }

            return value;
        }

        public virtual byte Read(ref byte value, long position = -1)
        {
            return (value = ReadByte(position));
        }

        public virtual byte[] ReadBytes(long position = -1)
        {
            return ReadBytes(ReadInt(position), (position > -1) ? position + 4 : position);
        }

        public virtual byte[] ReadBytes(int length, long position = -1)
        {
            var currentPosition = InternalBuffer.Position;
            if (position > -1) { InternalBuffer.Position = position; }
            byte[] buffer = new byte[length];
            if (length != InternalBuffer.Read(buffer, 0, length))
            {
                throw new OutOfMemoryException();
            }
            if (position > -1) { InternalBuffer.Position = currentPosition; }

            return buffer;
        }

        public virtual byte[] Read(ref byte[] value, long position = -1)
        {
            return ReadBytes(position);
        }

        public virtual byte[] Read(ref byte[] value, int length, long position = -1)
        {
            return ReadBytes(length, position);
        }

        public virtual char ReadChar(long position = -1)
        {
            return BitConverter.ToChar(ReadBytes(sizeof(char), position), 0);
        }

        public virtual char Read(ref char value, long position = -1)
        {
            return (value = ReadChar(position));
        }

        public virtual decimal ReadDecimal(long position = -1)
        {
            var bytes = ReadBytes(16, position);
            return new decimal(new int[] {
                BitConverter.ToInt32(bytes, 0),
                BitConverter.ToInt32(bytes, 4),
                BitConverter.ToInt32(bytes, 8),
                BitConverter.ToInt32(bytes, 12)
            });
        }

        public virtual decimal Read(ref decimal value, long position = -1)
        {
            return (value = ReadDecimal(position));
        }

        public virtual double ReadDouble(long position = -1)
        {
            return (BitConverter.ToDouble(ReadBytes(sizeof(double), position), 0));
        }

        public virtual double Read(ref double value, long position = -1)
        {
            return (value = BitConverter.ToDouble(ReadBytes(sizeof(double), position), 0));
        }

        public virtual float ReadFloat(long position = -1)
        {
            return (BitConverter.ToSingle(ReadBytes(sizeof(float), position), 0));
        }

        public virtual float Read(ref float value, long position = -1)
        {
            return (value = BitConverter.ToSingle(ReadBytes(sizeof(float), position), 0));
        }

        public virtual int ReadInt(long position = -1)
        {
            return (BitConverter.ToInt32(ReadBytes(sizeof(int), position), 0));
        }

        public virtual int Read(ref int value, long position = -1)
        {
            return (value = BitConverter.ToInt32(ReadBytes(sizeof(int), position), 0));
        }

        public virtual long ReadLong(long position = -1)
        {
            return (BitConverter.ToInt64(ReadBytes(sizeof(long), position), 0));
        }

        public virtual long Read(ref long value, long position = -1)
        {
            return (value = BitConverter.ToInt64(ReadBytes(sizeof(long), position), 0));
        }

        public virtual short ReadShort(long position = -1)
        {
            return (BitConverter.ToInt16(ReadBytes(sizeof(short), position), 0));
        }

        public virtual short Read(ref short value, long position = -1)
        {
            return (value = BitConverter.ToInt16(ReadBytes(sizeof(short), position), 0));
        }

        public virtual sbyte ReadSByte(long position = -1)
        {
            return ((sbyte)ReadByte(position));
        }

        public virtual sbyte Read(ref sbyte value, long position = -1)
        {
            return (value = (sbyte)ReadByte(position));
        }

        public virtual uint ReadUInt(long position = -1)
        {
            return (BitConverter.ToUInt16(ReadBytes(sizeof(uint), position), 0));
        }

        public virtual uint Read(ref uint value, long position = -1)
        {
            return (value = BitConverter.ToUInt16(ReadBytes(sizeof(uint), position), 0));
        }

        public virtual ulong ReadULong(long position = -1)
        {
            return (BitConverter.ToUInt64(ReadBytes(sizeof(ulong), position), 0));
        }

        public virtual ulong Read(ref ulong value, long position = -1)
        {
            return (value = BitConverter.ToUInt64(ReadBytes(sizeof(ulong), position), 0));
        }

        public virtual ushort ReadUShort(long position = -1)
        {
            return BitConverter.ToUInt16(ReadBytes(sizeof(ushort), position), 0);
        }

        public virtual ushort Read(ref ushort value, long position = -1)
        {
            return (value = ReadUShort(position));
        }

        public virtual string ReadString(long position = -1)
        {
            return Encoding.UTF8.GetString(ReadBytes(position));
        }

        public virtual string Read(ref string value, long position = -1)
        {
            return (value = ReadString(position));
        }
        #endregion Read

        #region Write
        public void Write(bool value, long position = -1)
        {
            Write(BitConverter.GetBytes(value), sizeof(bool), position);
        }

        public virtual void Write(byte value, long position = -1)
        {
            Write(new byte[] { value }, 1, position);
        }

        public virtual void Write(byte[] value, long position = -1)
        {
            Write(value.Length, position);
            Write(value, value.Length, (position > -1) ? position + 4 : InternalBuffer.Position);
        }

        public void Write(byte[] value, int length, long position = -1)
        {
            var currentPosition = InternalBuffer.Position;
            if (position > -1) { InternalBuffer.Position = position; }
            InternalBuffer.Write(value, 0, length);
            if (position > -1) { InternalBuffer.Position = currentPosition; }
        }

        public virtual void Write(char value, long position = -1)
        {
            Write(BitConverter.GetBytes(value), sizeof(char), position);
        }

        public virtual void Write(decimal value, long position = -1)
        {
            var intBuffer = decimal.GetBits(value);

            var bytes = new byte[16];
            Buffer.BlockCopy(BitConverter.GetBytes(intBuffer[0]), 0, bytes, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(intBuffer[1]), 0, bytes, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(intBuffer[2]), 0, bytes, 8, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(intBuffer[3]), 0, bytes, 12, 4);

            Write(bytes, sizeof(int) * 4, position);
        }

        public virtual void Write(double value, long position = -1)
        {
            Write(BitConverter.GetBytes(value), sizeof(double), position);
        }

        public virtual void Write(float value, long position = -1)
        {
            Write(BitConverter.GetBytes(value), sizeof(float), position);
        }

        public virtual void Write(int value, long position = -1)
        {
            Write(BitConverter.GetBytes(value), sizeof(int), position);
        }

        public virtual void Write(long value, long position = -1)
        {
            Write(BitConverter.GetBytes(value), sizeof(long), position);
        }

        public virtual void Write(sbyte value, long position = -1)
        {
            Write((byte)value, position);
        }

        public virtual void Write(short value, long position = -1)
        {
            Write(BitConverter.GetBytes(value), sizeof(short), position);
        }

        public virtual void Write(uint value, long position = -1)
        {
            Write(BitConverter.GetBytes(value), sizeof(uint), position);
        }

        public virtual void Write(ulong value, long position = -1)
        {
            Write(BitConverter.GetBytes(value), sizeof(ulong), position);
        }

        public virtual void Write(ushort value, long position = -1)
        {
            Write(BitConverter.GetBytes(value), sizeof(ushort), position);
        }

        public virtual void Write(string value, long position = -1)
        {
            var data = Encoding.UTF8.GetBytes(value);
            Write(data, position);
        }
        #endregion Write

        #region Stream
        public override bool CanRead { get { return InternalBuffer.CanRead; } }

        public override bool CanSeek { get { return InternalBuffer.CanSeek; } }

        public override bool CanWrite { get { return InternalBuffer.CanWrite; } }

        public override long Length { get { return InternalBuffer.Length; } }

        public override long Position
        {
            get { return InternalBuffer.Position; }
            set { InternalBuffer.Position = value; }
        }

        public override void Flush()
        {
            InternalBuffer.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return InternalBuffer.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            InternalBuffer.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return InternalBuffer.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            InternalBuffer.Write(buffer, offset, count);
        }
        #endregion Stream
    }
}
