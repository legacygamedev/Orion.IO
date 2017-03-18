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
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Orion.IO.Network.Lidgren
{
    public abstract class AbstractLidgrenNetwork<T> : ILidgrenNetwork<T> where T : NetPeer
    {
        protected Thread Thread { get; private set; }

        public T Peer { get; private set; }

        public IPAddress LocalIP
        {
            get
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    switch (ip.AddressFamily)
                    {
                        case AddressFamily.InterNetwork:
                        case AddressFamily.InterNetworkV6:
                            return new IPAddress(ip.GetAddressBytes());
                    }
                }

                return null;
            }
        }

        public NetworkOptions Options { get; private set; }
        public bool IsRunning { get; private set; }

        protected AbstractLidgrenNetwork(T peer, NetworkOptions options)
        {
            Thread = new Thread(new ThreadStart(Loop));
            Peer = peer;
            Options = options;
        }

        public void Start()
        {
            IsRunning = true;
            
        }

        protected abstract void OnStart();

        private void Loop()
        {
            OnStart();

            while (IsRunning)
            {
                try
                {
                    Tick();
                } catch (Exception exception)
                {
                    Console.WriteLine(exception.StackTrace);
                }

                Thread.Yield();
            }

            OnStop();
        }

        protected abstract void Tick();

        public void Stop()
        {
            IsRunning = false;
        }

        protected abstract void OnStop();
    }
}
