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

        public bool Read(ref bool value, long position = -1)
        {
            return Buffer.Read(ref value, position);
        }

        public byte Read(ref byte value, long position = -1)
        {
            return Buffer.Read(ref value, position);
        }

        public byte[] Read(ref byte[] value, long position = -1)
        {
            return Buffer.Read(ref value, position);
        }

        public byte[] Read(ref byte[] value, int length, long position = -1)
        {
            return Buffer.Read(ref value, length, position);
        }

        public char Read(ref char value, long position = -1)
        {
            return Buffer.Read(ref value, position);
        }

        public decimal Read(ref decimal value, long position = -1)
        {
            return Buffer.Read(ref value, position);
        }

        public double Read(ref double value, long position = -1)
        {
            return Buffer.Read(ref value, position);
        }

        public float Read(ref float value, long position = -1)
        {
            return Buffer.Read(ref value, position);
        }

        public int Read(ref int value, long position = -1)
        {
            return Buffer.Read(ref value, position);
        }

        public long Read(ref long value, long position = -1)
        {
            return Buffer.Read(ref value, position);
        }

        public sbyte Read(ref sbyte value, long position = -1)
        {
            return Buffer.Read(ref value, position);
        }

        public short Read(ref short value, long position = -1)
        {
            return Buffer.Read(ref value, position);
        }

        public uint Read(ref uint value, long position = -1)
        {
            return Buffer.Read(ref value, position);
        }

        public ulong Read(ref ulong value, long position = -1)
        {
            return Buffer.Read(ref value, position);
        }

        public ushort Read(ref ushort value, long position = -1)
        {
            return Buffer.Read(ref value, position);
        }

        public string Read(ref string value, long position = -1)
        {
            return Buffer.Read(ref value, position);
        }

        public bool ReadBool(long position = -1)
        {
            return Buffer.ReadBool(position);
        }

        public byte ReadByte(long position = -1)
        {
            return Buffer.ReadByte(position);
        }

        public byte[] ReadBytes(long position = -1)
        {
            return Buffer.ReadBytes(position);
        }

        public byte[] ReadBytes(int length, long position = -1)
        {
            return Buffer.ReadBytes(length, position);
        }

        public char ReadChar(long position = -1)
        {
            return Buffer.ReadChar(position);
        }

        public decimal ReadDecimal(long position = -1)
        {
            return Buffer.ReadDecimal(position);
        }

        public double ReadDouble(long position = -1)
        {
            return Buffer.ReadDouble(position);
        }

        public float ReadFloat(long position = -1)
        {
            return Buffer.ReadFloat(position);
        }

        public int ReadInt(long position = -1)
        {
            return Buffer.ReadInt(position);
        }

        public long ReadLong(long position = -1)
        {
            return Buffer.ReadLong(position);
        }

        public sbyte ReadSByte(long position = -1)
        {
            return Buffer.ReadSByte(position);
        }

        public short ReadShort(long position = -1)
        {
            return Buffer.ReadShort(position);
        }

        public string ReadString(long position = -1)
        {
            return Buffer.ReadString(position);
        }

        public uint ReadUInt(long position = -1)
        {
            return Buffer.ReadUInt(position);
        }

        public ulong ReadULong(long position = -1)
        {
            return Buffer.ReadULong(position);
        }

        public ushort ReadUShort(long position = -1)
        {
            return Buffer.ReadUShort(position);
        }

        public void Write(bool value, long position = -1)
        {
            Buffer.Write(value, position);
        }

        public void Write(byte value, long position = -1)
        {
            Buffer.Write(value, position);
        }

        public void Write(byte[] value, long position = -1)
        {
            Buffer.Write(value, position);
        }

        public void Write(byte[] value, int length, long position = -1)
        {
            Buffer.Write(value, length, position);
        }

        public void Write(char value, long position = -1)
        {
            Buffer.Write(value, position);
        }

        public void Write(decimal value, long position = -1)
        {
            Buffer.Write(value, position);
        }

        public void Write(double value, long position = -1)
        {
            Buffer.Write(value, position);
        }

        public void Write(float value, long position = -1)
        {
            Buffer.Write(value, position);
        }

        public void Write(int value, long position = -1)
        {
            Buffer.Write(value, position);
        }

        public void Write(long value, long position = -1)
        {
            Buffer.Write(value, position);
        }

        public void Write(sbyte value, long position = -1)
        {
            Buffer.Write(value, position);
        }

        public void Write(short value, long position = -1)
        {
            Buffer.Write(value, position);
        }

        public void Write(uint value, long position = -1)
        {
            Buffer.Write(value, position);
        }

        public void Write(ushort value, long position = -1)
        {
            Buffer.Write(value, position);
        }

        public void Write(string value, long position = -1)
        {
            Buffer.Write(value, position);
        }
    }
}
