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
        bool ReadBool(long position = -1);
        byte ReadByte(long position = -1);
        byte[] ReadBytes(long position = -1);
        byte[] ReadBytes(int length, long position = -1);
        char ReadChar(long position = -1);
        decimal ReadDecimal(long position = -1);
        double ReadDouble(long position = -1);
        float ReadFloat(long position = -1);
        int ReadInt(long position = -1);
        long ReadLong(long position = -1);
        sbyte ReadSByte(long position = -1);
        short ReadShort(long position = -1);
        uint ReadUInt(long position = -1);
        ulong ReadULong(long position = -1);
        ushort ReadUShort(long position = -1);
        string ReadString(long position = -1);

        bool Read(ref bool value, long position = -1);
        byte Read(ref byte value, long position = -1);
        byte[] Read(ref byte[] value, long position = -1);
        byte[] Read(ref byte[] value, int length, long position = -1);
        char Read(ref char value, long position = -1);
        decimal Read(ref decimal value, long position = -1);
        double Read(ref double value, long position = -1);
        float Read(ref float value, long position = -1);
        int Read(ref int value, long position = -1);
        long Read(ref long value, long position = -1);
        sbyte Read(ref sbyte value, long position = -1);
        short Read(ref short value, long position = -1);
        uint Read(ref uint value, long position = -1);
        ulong Read(ref ulong value, long position = -1);
        ushort Read(ref ushort value, long position = -1);
        string Read(ref string value, long position = -1);

        void Write(bool value, long position = -1);
        void Write(byte value, long position = -1);
        void Write(byte[] value, long position = -1);
        void Write(byte[] value, int length, long position = -1);
        void Write(char value, long position = -1);
        void Write(decimal value, long position = -1);
        void Write(double value, long position = -1);
        void Write(float value, long position = -1);
        void Write(int value, long position = -1);
        void Write(long value, long position = -1);
        void Write(sbyte value, long position = -1);
        void Write(short value, long position = -1);
        void Write(uint value, long position = -1);
        void Write(ulong value, long position = -1);
        void Write(ushort value, long position = -1);
        void Write(string value, long position = -1);
    }
}
