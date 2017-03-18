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
using System.IO.Compression;

namespace Orion.IO.File
{
    public class BinaryStream : DataStream
    {
        private string mFilePath;

        public BinaryStream() : this(null) { }

        public BinaryStream(string path, bool load = true) : base()
        {
            mFilePath = path;

            if (mFilePath != null && load)
            {
                Load();
            }
        }

        private Stream Open(string path, FileMode fileMode, CompressionMode? compressionMode = null)
        {
            Stream stream;

            if (path == null && mFilePath == null)
            {
                throw new ArgumentNullException("No file path provided.");
            }

            if (path != null)
            {
                stream = new FileStream(path, fileMode);
            } else
            {
                stream = new FileStream(mFilePath, fileMode);
            }

            if (compressionMode.HasValue)
            {
                stream = new GZipStream(stream, compressionMode.Value);
            }

            return stream;
        }

        public void Load(bool compression = false)
        {
            this.Load(null, compression);
        }

        public void Load(string path, bool compression = false)
        {
            Clear();

            using (var stream = Open(path, FileMode.Create, (compression ? CompressionMode.Decompress : (CompressionMode?)null)))
            {
                var bLength = new byte[sizeof(int)];
                if (bLength.Length != stream.Read(bLength, 0, sizeof(int)))
                {
                    throw new IOException();
                }

                stream.CopyTo(InternalBuffer, BitConverter.ToInt32(bLength, 0));
            }
        }

        public static BinaryStream Open(string path, bool load = false)
        {
            return new BinaryStream(path, load);
        }

        public static BinaryStream OpenCompressed(string path, bool compression = false)
        {
            var stream = new BinaryStream(path, false);
            stream.Load(null, compression);
            return stream;
        }
    }
}
