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
        public NetBuffer Message { get; set; }

        public LidgrenPacketSerializer(NetBuffer message)
        {
            Message = message;
        }

        public bool Read(ref bool value, long position = -1)
        {
            return (value = ReadBool(position));
        }

        public byte Read(ref byte value, long position = -1)
        {
            if (!Message.ReadByte(out value))
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
            if (!Message.ReadBytes(length, out value))
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
            if (!Message.ReadInt32(out value))
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
            if (!Message.ReadUInt32(out value))
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
            return Message.ReadBoolean();
        }

        public byte ReadByte(long position = -1)
        {
            return Message.ReadByte();
        }

        public byte[] ReadBytes(long position = -1)
        {
            return ReadBytes(ReadInt(position), (position > -1) ? position + 4 : position);
        }

        public byte[] ReadBytes(int length, long position = -1)
        {
            return Message.ReadBytes(length);
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
            return Message.ReadDouble();
        }

        public float ReadFloat(long position = -1)
        {
            return Message.ReadFloat();
        }

        public int ReadInt(long position = -1)
        {
            return Message.ReadInt32();
        }

        public long ReadLong(long position = -1)
        {
            return Message.ReadInt64();
        }

        public sbyte ReadSByte(long position = -1)
        {
            return Message.ReadSByte();
        }

        public short ReadShort(long position = -1)
        {
            return Message.ReadInt16();
        }

        public string ReadString(long position = -1)
        {
            return Encoding.UTF8.GetString(ReadBytes(position));
        }

        public uint ReadUInt(long position = -1)
        {
            return Message.ReadUInt32();
        }

        public ulong ReadULong(long position = -1)
        {
            return Message.ReadUInt64();
        }

        public ushort ReadUShort(long position = -1)
        {
            return Message.ReadUInt16();
        }

        public void Write(bool value, long position = -1)
        {
            Message.Write(value);
        }

        public void Write(byte value, long position = -1)
        {
            Message.Write(value);
        }

        public void Write(byte[] value, long position = -1)
        {
            Message.Write(value.Length);
            Write(value, value.Length, position);
        }

        public void Write(byte[] value, int length, long position = -1)
        {
            Message.Write(value, 0, length);
        }

        public void Write(char value, long position = -1)
        {
            Write(BitConverter.GetBytes(value), 2, position);
        }

        public void Write(decimal value, long position = -1)
        {
            var intBuffer = decimal.GetBits(value);

            var bytes = new byte[16];
            Buffer.BlockCopy(BitConverter.GetBytes(intBuffer[0]), 0, bytes, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(intBuffer[1]), 0, bytes, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(intBuffer[2]), 0, bytes, 8, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(intBuffer[3]), 0, bytes, 12, 4);

            Write(bytes, sizeof(int) * 4, position);
        }

        public void Write(double value, long position = -1)
        {
            Message.Write(value);
        }

        public void Write(float value, long position = -1)
        {
            Message.Write(value);
        }

        public void Write(int value, long position = -1)
        {
            Message.Write(value);
        }

        public void Write(long value, long position = -1)
        {
            Message.Write(value);
        }

        public void Write(sbyte value, long position = -1)
        {
            Message.Write(value);
        }

        public void Write(short value, long position = -1)
        {
            Message.Write(value);
        }

        public void Write(uint value, long position = -1)
        {
            Message.Write(value);
        }

        public void Write(ulong value, long position = -1)
        {
            Message.Write(value);
        }

        public void Write(ushort value, long position = -1)
        {
            Message.Write(value);
        }

        public void Write(string value, long position = -1)
        {
            Write(Encoding.UTF8.GetBytes(value), position);
        }
    }
}
