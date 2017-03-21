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

namespace Orion.IO
{
    public interface IBinarySerializer
    {
        bool ReadBool();
        byte ReadByte();
        byte[] ReadBytes();
        byte[] ReadBytes(int length);
        char ReadChar();
        decimal ReadDecimal();
        double ReadDouble();
        float ReadFloat();
        int ReadInt();
        long ReadLong();
        sbyte ReadSByte();
        short ReadShort();
        uint ReadUInt();
        ulong ReadULong();
        ushort ReadUShort();
        string ReadString();

        bool Read(ref bool value);
        byte Read(ref byte value);
        byte[] Read(ref byte[] value);
        byte[] Read(ref byte[] value, int length);
        char Read(ref char value);
        decimal Read(ref decimal value);
        double Read(ref double value);
        float Read(ref float value);
        int Read(ref int value);
        long Read(ref long value);
        sbyte Read(ref sbyte value);
        short Read(ref short value);
        uint Read(ref uint value);
        ulong Read(ref ulong value);
        ushort Read(ref ushort value);
        string Read(ref string value);

        void Write(bool value);
        void Write(byte value);
        void Write(byte[] value);
        void Write(byte[] value, int length);
        void Write(char value);
        void Write(decimal value);
        void Write(double value);
        void Write(float value);
        void Write(int value);
        void Write(long value);
        void Write(sbyte value);
        void Write(short value);
        void Write(uint value);
        void Write(ulong value);
        void Write(ushort value);
        void Write(string value);
    }
}
