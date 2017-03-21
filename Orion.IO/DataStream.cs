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
        public virtual bool ReadBool()
        {
            return BitConverter.ToBoolean(ReadBytes(sizeof(bool)), 0);
        }

        public virtual bool Read(ref bool value)
        {
            return (value = ReadBool());
        }

        public virtual byte ReadByte()
        {
            return (byte)(InternalBuffer.ReadByte() & 0xFF);
        }

        public virtual byte Read(ref byte value)
        {
            return (value = ReadByte());
        }

        public virtual byte[] ReadBytes()
        {
            return ReadBytes(ReadInt());
        }

        public virtual byte[] ReadBytes(int length)
        {
            byte[] buffer = new byte[length];
            if (length != InternalBuffer.Read(buffer, 0, length))
            {
                throw new OutOfMemoryException();
            }

            return buffer;
        }

        public virtual byte[] Read(ref byte[] value)
        {
            return ReadBytes();
        }

        public virtual byte[] Read(ref byte[] value, int length)
        {
            return ReadBytes(length);
        }

        public virtual char ReadChar()
        {
            return BitConverter.ToChar(ReadBytes(sizeof(char)), 0);
        }

        public virtual char Read(ref char value)
        {
            return (value = ReadChar());
        }

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

        public virtual decimal Read(ref decimal value)
        {
            return (value = ReadDecimal());
        }

        public virtual double ReadDouble()
        {
            return (BitConverter.ToDouble(ReadBytes(sizeof(double)), 0));
        }

        public virtual double Read(ref double value)
        {
            return (value = BitConverter.ToDouble(ReadBytes(sizeof(double)), 0));
        }

        public virtual float ReadFloat()
        {
            return (BitConverter.ToSingle(ReadBytes(sizeof(float)), 0));
        }

        public virtual float Read(ref float value)
        {
            return (value = BitConverter.ToSingle(ReadBytes(sizeof(float)), 0));
        }

        public virtual int ReadInt()
        {
            return (BitConverter.ToInt32(ReadBytes(sizeof(int)), 0));
        }

        public virtual int Read(ref int value)
        {
            return (value = BitConverter.ToInt32(ReadBytes(sizeof(int)), 0));
        }

        public virtual long ReadLong()
        {
            return (BitConverter.ToInt64(ReadBytes(sizeof(long)), 0));
        }

        public virtual long Read(ref long value)
        {
            return (value = BitConverter.ToInt64(ReadBytes(sizeof(long)), 0));
        }

        public virtual short ReadShort()
        {
            return (BitConverter.ToInt16(ReadBytes(sizeof(short)), 0));
        }

        public virtual short Read(ref short value)
        {
            return (value = BitConverter.ToInt16(ReadBytes(sizeof(short)), 0));
        }

        public virtual sbyte ReadSByte()
        {
            return ((sbyte)ReadByte());
        }

        public virtual sbyte Read(ref sbyte value)
        {
            return (value = (sbyte)ReadByte());
        }

        public virtual uint ReadUInt()
        {
            return (BitConverter.ToUInt16(ReadBytes(sizeof(uint)), 0));
        }

        public virtual uint Read(ref uint value)
        {
            return (value = BitConverter.ToUInt16(ReadBytes(sizeof(uint)), 0));
        }

        public virtual ulong ReadULong()
        {
            return (BitConverter.ToUInt64(ReadBytes(sizeof(ulong)), 0));
        }

        public virtual ulong Read(ref ulong value)
        {
            return (value = BitConverter.ToUInt64(ReadBytes(sizeof(ulong)), 0));
        }

        public virtual ushort ReadUShort()
        {
            return BitConverter.ToUInt16(ReadBytes(sizeof(ushort)), 0);
        }

        public virtual ushort Read(ref ushort value)
        {
            return (value = ReadUShort());
        }

        public virtual string ReadString()
        {
            return Encoding.UTF8.GetString(ReadBytes());
        }

        public virtual string Read(ref string value)
        {
            return (value = ReadString());
        }
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
