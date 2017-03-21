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

        public bool Read(ref bool value)
        {
            return (value = ReadBool());
        }

        public byte Read(ref byte value)
        {
            if (!Message.ReadByte(out value))
            {
                throw new AccessViolationException();
            }

            return value;
        }

        public byte[] Read(ref byte[] value)
        {
            return Read(ref value, ReadInt());
        }

        public byte[] Read(ref byte[] value, int length)
        {
            if (!Message.ReadBytes(length, out value))
            {
                throw new AccessViolationException();
            }

            return value;
        }

        public char Read(ref char value)
        {
            return (value = ReadChar());
        }

        public decimal Read(ref decimal value)
        {
            return (value = ReadDecimal());
        }

        public double Read(ref double value)
        {
            return (value = ReadDouble());
        }

        public float Read(ref float value)
        {
            return (value = ReadFloat());
        }

        public int Read(ref int value)
        {
            if (!Message.ReadInt32(out value))
            {
                throw new AccessViolationException();
            }

            return value;
        }

        public long Read(ref long value)
        {
            return (value = ReadLong());
        }

        public sbyte Read(ref sbyte value)
        {
            return (value = ReadSByte());
        }

        public short Read(ref short value)
        {
            return (value = ReadShort());
        }

        public uint Read(ref uint value)
        {
            if (!Message.ReadUInt32(out value))
            {
                throw new AccessViolationException();
            }

            return value;
        }

        public ulong Read(ref ulong value)
        {
            return (value = ReadULong());
        }

        public ushort Read(ref ushort value)
        {
            return (value = ReadUShort());
        }

        public string Read(ref string value)
        {
            return (value = ReadString());
        }

        public bool ReadBool()
        {
            return Message.ReadBoolean();
        }

        public byte ReadByte()
        {
            return Message.ReadByte();
        }

        public byte[] ReadBytes()
        {
            return ReadBytes(ReadInt());
        }

        public byte[] ReadBytes(int length)
        {
            return Message.ReadBytes(length);
        }

        public char ReadChar()
        {
            return BitConverter.ToChar(ReadBytes(sizeof(char)), 0);
        }

        public decimal ReadDecimal()
        {
            var bytes = ReadBytes(16);
            return new decimal(new int[] {
                BitConverter.ToInt32(bytes, 0),
                BitConverter.ToInt32(bytes, 4),
                BitConverter.ToInt32(bytes, 8),
                BitConverter.ToInt32(bytes, 12)
            });
        }

        public double ReadDouble()
        {
            return Message.ReadDouble();
        }

        public float ReadFloat()
        {
            return Message.ReadFloat();
        }

        public int ReadInt()
        {
            return Message.ReadInt32();
        }

        public long ReadLong()
        {
            return Message.ReadInt64();
        }

        public sbyte ReadSByte()
        {
            return Message.ReadSByte();
        }

        public short ReadShort()
        {
            return Message.ReadInt16();
        }

        public string ReadString()
        {
            return Encoding.UTF8.GetString(ReadBytes());
        }

        public uint ReadUInt()
        {
            return Message.ReadUInt32();
        }

        public ulong ReadULong()
        {
            return Message.ReadUInt64();
        }

        public ushort ReadUShort()
        {
            return Message.ReadUInt16();
        }

        public void Write(bool value)
        {
            Message.Write(value);
        }

        public void Write(byte value)
        {
            Message.Write(value);
        }

        public void Write(byte[] value)
        {
            Message.Write(value.Length);
            Write(value, value.Length);
        }

        public void Write(byte[] value, int length)
        {
            Message.Write(value, 0, length);
        }

        public void Write(char value)
        {
            Write(BitConverter.GetBytes(value), 2);
        }

        public void Write(decimal value)
        {
            var intBuffer = decimal.GetBits(value);

            var bytes = new byte[16];
            Buffer.BlockCopy(BitConverter.GetBytes(intBuffer[0]), 0, bytes, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(intBuffer[1]), 0, bytes, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(intBuffer[2]), 0, bytes, 8, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(intBuffer[3]), 0, bytes, 12, 4);

            Write(bytes, sizeof(int) * 4);
        }

        public void Write(double value)
        {
            Message.Write(value);
        }

        public void Write(float value)
        {
            Message.Write(value);
        }

        public void Write(int value)
        {
            Message.Write(value);
        }

        public void Write(long value)
        {
            Message.Write(value);
        }

        public void Write(sbyte value)
        {
            Message.Write(value);
        }

        public void Write(short value)
        {
            Message.Write(value);
        }

        public void Write(uint value)
        {
            Message.Write(value);
        }

        public void Write(ulong value)
        {
            Message.Write(value);
        }

        public void Write(ushort value)
        {
            Message.Write(value);
        }

        public void Write(string value)
        {
            Write(Encoding.UTF8.GetBytes(value));
        }
    }
}
