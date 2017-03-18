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
using Orion.IO.Network.Packets;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Orion.IO.Network.Lidgren
{
    public class LidgrenClient : AbstractLidgrenNetwork<NetClient>
    {
        public static NetClient CreateClient(NetworkOptions options)
        {
            var config = new NetPeerConfiguration(options.Identifier == null ? "Sirius" : options.Identifier);
            config.AcceptIncomingConnections = false;
            config.UseMessageRecycling = true;
            config.SetMessageTypeEnabled(NetIncomingMessageType.DiscoveryRequest, false);
            config.SetMessageTypeEnabled(NetIncomingMessageType.ConnectionApproval, false);

            config.ConnectionTimeout = 5;

            if (options.PingInterval > 0)
            {
                config.PingInterval = options.PingInterval / 1000f;
            }

            if (options.RetryInterval > 0)
            {
                config.ResendHandshakeInterval = config.ResendHandshakeInterval / 1000f;
            }

            return new NetClient(config);
        }
        
        protected Thread ProcessorThread { get; private set; }

        public LidgrenConnection Connection { get; private set; }

        public event HandleIncomingMessage OnMessageReceived;

        public IPAddress RemoteIP
        {
            get
            {
                var host = Dns.GetHostEntry(Options.Host);
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

        public LidgrenClient(NetworkOptions options) : base(CreateClient(options), options)
        {
            
        }

        protected override void OnStart()
        {
            var connection = Peer.Connect(new IPEndPoint(RemoteIP, Options.Port));
            Connection = new LidgrenClientConnection(this, connection);
        }

        protected override void OnStop()
        {
            Peer.Disconnect("Disconnecting...");
        }

        protected override void Tick()
        {
            NetIncomingMessage incomingMessage;

            while ((incomingMessage = Peer.ReadMessage()) != null)
            {
                Connection.QueueMessage(incomingMessage);
            }
        }

        protected class LidgrenClientConnection : LidgrenConnection
        {
            private LidgrenClient mClient;
            private Thread mThread;

            public LidgrenClientConnection(LidgrenClient client, NetConnection connection) : base(connection)
            {
                mClient = client;
                mThread = new Thread(new ThreadStart(ProcessMessages));
                mThread.Start();
            }

            protected override void ProcessMessages()
            {
                NetIncomingMessage message;

                while (IsConnected)
                {
                    if (MessageQueue.TryTake(out message, 1000))
                    {
                        switch (message.MessageType)
                        {
                            case NetIncomingMessageType.VerboseDebugMessage:
                            case NetIncomingMessageType.DebugMessage:
                            case NetIncomingMessageType.WarningMessage:
                            case NetIncomingMessageType.ErrorMessage:
                                break;

                            case NetIncomingMessageType.StatusChanged:
                                NetConnectionStatus type = (NetConnectionStatus)message.ReadByte();

                                Console.WriteLine("Status Changed: '{0}'", message.ReadString());
                                if (type == NetConnectionStatus.Disconnected)
                                {
                                    Kill();
                                }
                                break;

                            case NetIncomingMessageType.Data:
                                if (mClient.OnMessageReceived != null)
                                {
                                    mClient.OnMessageReceived.Invoke(this, message);
                                }
                                
                                mClient.Peer.Recycle(message);
                                break;

                            default:
                                Console.WriteLine("Unhandled message type '{0}'.", message.MessageType);
                                break;
                        }
                    }

                    Thread.Yield();
                }
            }
        }
    }
}
