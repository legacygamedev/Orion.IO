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

namespace Orion.IO.Network.Packets
{
    public struct BinaryPacket : IBarePacket<BinaryPacket>, IBinarySerializer
    {
        public IConnection Sender { get; private set; }

        private DataBuffer mBuffer;
        private DataBuffer Buffer
        {
            get
            {
                if (mBuffer == null)
                {
                    mBuffer = new DataBuffer();
                }

                return mBuffer;
            }
        }

        public long Length { get { return mBuffer.Length; } }

        public BinaryPacket(IConnection sender)
        {
            Sender = sender;

            mBuffer = null;
        }

        public bool Read(IPacketSerializer serializer)
        {
            Buffer.Write(serializer.ReadBytes());

            return true;
        }

        public bool Write(IPacketSerializer serializer)
        {
            serializer.Write(Buffer.ToBytes());

            return true;
        }

        public bool Read(ref bool value)
        {
            return Buffer.Read(ref value);
        }

        public byte Read(ref byte value)
        {
            return Buffer.Read(ref value);
        }

        public byte[] Read(ref byte[] value)
        {
            return Buffer.Read(ref value);
        }

        public byte[] Read(ref byte[] value, int length)
        {
            return Buffer.Read(ref value, length);
        }

        public char Read(ref char value)
        {
            return Buffer.Read(ref value);
        }

        public decimal Read(ref decimal value)
        {
            return Buffer.Read(ref value);
        }

        public double Read(ref double value)
        {
            return Buffer.Read(ref value);
        }

        public float Read(ref float value)
        {
            return Buffer.Read(ref value);
        }

        public int Read(ref int value)
        {
            return Buffer.Read(ref value);
        }

        public long Read(ref long value)
        {
            return Buffer.Read(ref value);
        }

        public sbyte Read(ref sbyte value)
        {
            return Buffer.Read(ref value);
        }

        public short Read(ref short value)
        {
            return Buffer.Read(ref value);
        }

        public uint Read(ref uint value)
        {
            return Buffer.Read(ref value);
        }

        public ulong Read(ref ulong value)
        {
            return Buffer.Read(ref value);
        }

        public ushort Read(ref ushort value)
        {
            return Buffer.Read(ref value);
        }

        public string Read(ref string value)
        {
            return Buffer.Read(ref value);
        }

        public bool ReadBool()
        {
            return Buffer.ReadBool();
        }

        public byte ReadByte()
        {
            return Buffer.ReadByte();
        }

        public byte[] ReadBytes()
        {
            return Buffer.ReadBytes();
        }

        public byte[] ReadBytes(int length)
        {
            return Buffer.ReadBytes(length);
        }

        public char ReadChar()
        {
            return Buffer.ReadChar();
        }

        public decimal ReadDecimal()
        {
            return Buffer.ReadDecimal();
        }

        public double ReadDouble()
        {
            return Buffer.ReadDouble();
        }

        public float ReadFloat()
        {
            return Buffer.ReadFloat();
        }

        public int ReadInt()
        {
            return Buffer.ReadInt();
        }

        public long ReadLong()
        {
            return Buffer.ReadLong();
        }

        public sbyte ReadSByte()
        {
            return Buffer.ReadSByte();
        }

        public short ReadShort()
        {
            return Buffer.ReadShort();
        }

        public string ReadString()
        {
            return Buffer.ReadString();
        }

        public uint ReadUInt()
        {
            return Buffer.ReadUInt();
        }

        public ulong ReadULong()
        {
            return Buffer.ReadULong();
        }

        public ushort ReadUShort()
        {
            return Buffer.ReadUShort();
        }

        public void Write(bool value)
        {
            Buffer.Write(value);
        }

        public void Write(byte value)
        {
            Buffer.Write(value);
        }

        public void Write(byte[] value)
        {
            Buffer.Write(value);
        }

        public void Write(byte[] value, int length)
        {
            Buffer.Write(value, length);
        }

        public void Write(char value)
        {
            Buffer.Write(value);
        }

        public void Write(decimal value)
        {
            Buffer.Write(value);
        }

        public void Write(double value)
        {
            Buffer.Write(value);
        }

        public void Write(float value)
        {
            Buffer.Write(value);
        }

        public void Write(int value)
        {
            Buffer.Write(value);
        }

        public void Write(long value)
        {
            Buffer.Write(value);
        }

        public void Write(sbyte value)
        {
            Buffer.Write(value);
        }

        public void Write(short value)
        {
            Buffer.Write(value);
        }

        public void Write(uint value)
        {
            Buffer.Write(value);
        }

        public void Write(ulong value)
        {
            Buffer.Write(value);
        }

        public void Write(ushort value)
        {
            Buffer.Write(value);
        }

        public void Write(string value)
        {
            Buffer.Write(value);
        }
    }
}
