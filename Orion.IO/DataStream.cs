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

        public override void Close()
        {
            InternalBuffer?.Close();
            base.Close();
        }
        #endregion Utility

        #region Read
        public virtual bool ReadBool() => BitConverter.ToBoolean(ReadBytes(sizeof(bool)), 0);

        public virtual bool Read(out bool value) => (value = ReadBool());

        public virtual byte ReadByte() => (byte)(InternalBuffer.ReadByte() & 0xFF);

        public virtual byte Read(out byte value) => (value = ReadByte());

        public virtual byte[] ReadBytes() => ReadBytes(ReadInt());

        public virtual byte[] ReadBytes(int length)
        {
            var buffer = new byte[length];
            if (length != InternalBuffer.Read(buffer, 0, length))
            {
                throw new OutOfMemoryException();
            }

            return buffer;
        }

        public virtual byte[] Read(out byte[] value) => (value = ReadBytes());

        public virtual byte[] Read(out byte[] value, int length) => (value = ReadBytes(length));

        public virtual char ReadChar() => BitConverter.ToChar(ReadBytes(sizeof(char)), 0);

        public virtual char Read(out char value) => (value = ReadChar());

        public virtual decimal ReadDecimal()
        {
            var bytes = ReadBytes(16);
            return new decimal(new int[] {
                BitConverter.ToInt32(bytes, 0),
                BitConverter.ToInt32(bytes, 4),
                BitConverter.ToInt32(bytes, 8),
                BitConverter.ToInt32(bytes, 12)
            });
        }

        public virtual decimal Read(out decimal value) => (value = ReadDecimal());

        public virtual double ReadDouble() => (BitConverter.ToDouble(ReadBytes(sizeof(double)), 0));

        public virtual double Read(out double value) => (value = BitConverter.ToDouble(ReadBytes(sizeof(double)), 0));

        public virtual float ReadFloat() => (BitConverter.ToSingle(ReadBytes(sizeof(float)), 0));

        public virtual float Read(out float value) => (value = BitConverter.ToSingle(ReadBytes(sizeof(float)), 0));

        public virtual int ReadInt() => (BitConverter.ToInt32(ReadBytes(sizeof(int)), 0));

        public virtual int Read(out int value) => (value = BitConverter.ToInt32(ReadBytes(sizeof(int)), 0));

        public virtual long ReadLong() => (BitConverter.ToInt64(ReadBytes(sizeof(long)), 0));

        public virtual long Read(out long value) => (value = BitConverter.ToInt64(ReadBytes(sizeof(long)), 0));

        public virtual short ReadShort() => (BitConverter.ToInt16(ReadBytes(sizeof(short)), 0));

        public virtual short Read(out short value) => (value = BitConverter.ToInt16(ReadBytes(sizeof(short)), 0));

        public virtual sbyte ReadSByte() => ((sbyte)ReadByte());

        public virtual sbyte Read(out sbyte value) => (value = (sbyte)ReadByte());

        public virtual uint ReadUInt() => (BitConverter.ToUInt16(ReadBytes(sizeof(uint)), 0));

        public virtual uint Read(out uint value) => (value = BitConverter.ToUInt16(ReadBytes(sizeof(uint)), 0));

        public virtual ulong ReadULong() => (BitConverter.ToUInt64(ReadBytes(sizeof(ulong)), 0));

        public virtual ulong Read(out ulong value) => (value = BitConverter.ToUInt64(ReadBytes(sizeof(ulong)), 0));

        public virtual ushort ReadUShort() => BitConverter.ToUInt16(ReadBytes(sizeof(ushort)), 0);

        public virtual ushort Read(out ushort value) => (value = ReadUShort());

        public virtual string ReadString() => Encoding.UTF8.GetString(ReadBytes());

        public virtual string Read(out string value) => (value = ReadString());

        #endregion Read

        #region Write
        public void Write(bool value)
        {
            Write(BitConverter.GetBytes(value), sizeof(bool));
        }

        public virtual void Write(byte value)
        {
            Write(new byte[] { value }, 1);
        }

        public virtual void Write(byte[] value)
        {
            Write(value.Length);
            Write(value, value.Length);
        }

        public void Write(byte[] value, int length)
        {
            InternalBuffer.Write(value, 0, length);
        }

        public virtual void Write(char value)
        {
            Write(BitConverter.GetBytes(value), sizeof(char));
        }

        public virtual void Write(decimal value)
        {
            var intBuffer = decimal.GetBits(value);

            var bytes = new byte[16];
            Buffer.BlockCopy(BitConverter.GetBytes(intBuffer[0]), 0, bytes, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(intBuffer[1]), 0, bytes, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(intBuffer[2]), 0, bytes, 8, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(intBuffer[3]), 0, bytes, 12, 4);

            Write(bytes, sizeof(int) * 4);
        }

        public virtual void Write(double value)
        {
            Write(BitConverter.GetBytes(value), sizeof(double));
        }

        public virtual void Write(float value)
        {
            Write(BitConverter.GetBytes(value), sizeof(float));
        }

        public virtual void Write(int value)
        {
            Write(BitConverter.GetBytes(value), sizeof(int));
        }

        public virtual void Write(long value)
        {
            Write(BitConverter.GetBytes(value), sizeof(long));
        }

        public virtual void Write(sbyte value)
        {
            Write((byte)value);
        }

        public virtual void Write(short value)
        {
            Write(BitConverter.GetBytes(value), sizeof(short));
        }

        public virtual void Write(uint value)
        {
            Write(BitConverter.GetBytes(value), sizeof(uint));
        }

        public virtual void Write(ulong value)
        {
            Write(BitConverter.GetBytes(value), sizeof(ulong));
        }

        public virtual void Write(ushort value)
        {
            Write(BitConverter.GetBytes(value), sizeof(ushort));
        }

        public virtual void Write(string value)
        {
            var data = Encoding.UTF8.GetBytes(value);
            Write(data);
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
