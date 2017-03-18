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

using Lidgren.Network;
using System;
using System.Text;

namespace Orion.IO.Network.Lidgren
{
    public class LidgrenPacketSerializer : IPacketSerializer
    {
        private NetBuffer mMessage;

        public LidgrenPacketSerializer(NetBuffer message)
        {
            mMessage = message;
        }

        public bool Read(ref bool value, long position = -1)
        {
            return (value = ReadBool(position));
        }

        public byte Read(ref byte value, long position = -1)
        {
            if (!mMessage.ReadByte(out value))
            {
                throw new AccessViolationException();
            }

            return value;
        }

        public byte[] Read(ref byte[] value, long position = -1)
        {
            return Read(ref value, ReadInt(position), (position > -1) ? position + 4 : position);
        }

        public byte[] Read(ref byte[] value, int length, long position = -1)
        {
            if (!mMessage.ReadBytes(length, out value))
            {
                throw new AccessViolationException();
            }

            return value;
        }

        public char Read(ref char value, long position = -1)
        {
            return (value = ReadChar(position));
        }

        public decimal Read(ref decimal value, long position = -1)
        {
            return (value = ReadDecimal(position));
        }

        public double Read(ref double value, long position = -1)
        {
            return (value = ReadDouble(position));
        }

        public float Read(ref float value, long position = -1)
        {
            return (value = ReadFloat(position));
        }

        public int Read(ref int value, long position = -1)
        {
            if (!mMessage.ReadInt32(out value))
            {
                throw new AccessViolationException();
            }

            return value;
        }

        public long Read(ref long value, long position = -1)
        {
            return (value = ReadLong(position));
        }

        public sbyte Read(ref sbyte value, long position = -1)
        {
            return (value = ReadSByte(position));
        }

        public short Read(ref short value, long position = -1)
        {
            return (value = ReadShort(position));
        }

        public uint Read(ref uint value, long position = -1)
        {
            if (!mMessage.ReadUInt32(out value))
            {
                throw new AccessViolationException();
            }

            return value;
        }

        public ulong Read(ref ulong value, long position = -1)
        {
            return (value = ReadULong(position));
        }

        public ushort Read(ref ushort value, long position = -1)
        {
            return (value = ReadUShort(position));
        }

        public string Read(ref string value, long position = -1)
        {
            return (value = ReadString(position));
        }

        public bool ReadBool(long position = -1)
        {
            return mMessage.ReadBoolean();
        }

        public byte ReadByte(long position = -1)
        {
            return mMessage.ReadByte();
        }

        public byte[] ReadBytes(long position = -1)
        {
            return ReadBytes(ReadInt(position), (position > -1) ? position + 4 : position);
        }

        public byte[] ReadBytes(int length, long position = -1)
        {
            return mMessage.ReadBytes(length);
        }

        public char ReadChar(long position = -1)
        {
            return BitConverter.ToChar(ReadBytes(sizeof(char), position), 0);
        }

        public decimal ReadDecimal(long position = -1)
        {
            var bytes = ReadBytes(16, position);
            return new decimal(new int[] {
                BitConverter.ToInt32(bytes, 0),
                BitConverter.ToInt32(bytes, 4),
                BitConverter.ToInt32(bytes, 8),
                BitConverter.ToInt32(bytes, 12)
            });
        }

        public double ReadDouble(long position = -1)
        {
            return mMessage.ReadDouble();
        }

        public float ReadFloat(long position = -1)
        {
            return mMessage.ReadFloat();
        }

        public int ReadInt(long position = -1)
        {
            return mMessage.ReadInt32();
        }

        public long ReadLong(long position = -1)
        {
            return mMessage.ReadInt64();
        }

        public sbyte ReadSByte(long position = -1)
        {
            return mMessage.ReadSByte();
        }

        public short ReadShort(long position = -1)
        {
            return mMessage.ReadInt16();
        }

        public string ReadString(long position = -1)
        {
            return Encoding.UTF8.GetString(ReadBytes(position));
        }

        public uint ReadUInt(long position = -1)
        {
            return mMessage.ReadUInt32();
        }

        public ulong ReadULong(long position = -1)
        {
            return mMessage.ReadUInt64();
        }

        public ushort ReadUShort(long position = -1)
        {
            return mMessage.ReadUInt16();
        }

        public void Write(bool value, long position = -1)
        {
            mMessage.Write(value);
        }

        public void Write(byte value, long position = -1)
        {
            mMessage.Write(value);
        }

        public void Write(byte[] value, long position = -1)
        {
            mMessage.Write(value);
        }

        public void Write(byte[] value, int length, long position = -1)
        {
            throw new NotImplementedException();
        }

        public void Write(char value, long position = -1)
        {
            throw new NotImplementedException();
        }

        public void Write(decimal value, long position = -1)
        {
            throw new NotImplementedException();
        }

        public void Write(double value, long position = -1)
        {
            throw new NotImplementedException();
        }

        public void Write(float value, long position = -1)
        {
            throw new NotImplementedException();
        }

        public void Write(int value, long position = -1)
        {
            throw new NotImplementedException();
        }

        public void Write(long value, long position = -1)
        {
            throw new NotImplementedException();
        }

        public void Write(sbyte value, long position = -1)
        {
            throw new NotImplementedException();
        }

        public void Write(short value, long position = -1)
        {
            throw new NotImplementedException();
        }

        public void Write(uint value, long position = -1)
        {
            throw new NotImplementedException();
        }

        public void Write(ushort value, long position = -1)
        {
            throw new NotImplementedException();
        }

        public void Write(string value, long position = -1)
        {
            throw new NotImplementedException();
        }
    }
}
