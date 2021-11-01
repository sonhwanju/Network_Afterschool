using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerCore
{
    class Session
    {
        Socket _socket;
        int _disconnected = 0; //멤버변수
        SocketAsyncEventArgs _sendArgs = new SocketAsyncEventArgs();
        Queue<byte[]> _sendQueue = new Queue<byte[]>();
        bool _pending = false;

        object _lock = new object(); //락킹용 오브젝트

        public void Init(Socket socket)
        {
            _socket = socket;

            SocketAsyncEventArgs recvArgs = new SocketAsyncEventArgs();
            recvArgs.Completed += OnRecvCompleted;
            recvArgs.SetBuffer(new byte[1024], 0, 1024);
            RegisterRecv(recvArgs);

            _sendArgs.Completed += OnSendCompleted;
            //recvArgs.UserToken = "GGM";
        }



        public void Send(byte[] sendBuffer)
        {
           lock(_lock)
            {
                _sendQueue.Enqueue(sendBuffer);

                if(!_pending)
                {
                    RegisterSend();
                }
                else
                {

                }
            }
        }

        public void Disconnect()
        {
            if (Interlocked.Exchange(ref _disconnected, 1) == 1)
            {
                return;
            }
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }

        #region 네트워크 통신부

        private void RegisterSend()
        {
            _pending = true;

            byte[] buffer = _sendQueue.Dequeue();
            _sendArgs.SetBuffer(buffer, 0, buffer.Length);

            bool pending = _socket.SendAsync(_sendArgs);

            if(!pending)
            {
                OnSendCompleted(null, _sendArgs);
            }

        }

        private void OnSendCompleted(object sender, SocketAsyncEventArgs args)
        {
            lock(_lock)
            {
                if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
                {
                    try
                    {
                        if(_sendQueue.Count > 0)
                        {
                            RegisterSend();
                        }
                        else
                        {
                            _pending = false;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                else
                {
                    Disconnect();
                }
            }
            
        }

        private void RegisterRecv(SocketAsyncEventArgs args)
        {
            bool pending = _socket.ReceiveAsync(args);

            if (!pending)
            {
                OnRecvCompleted(null, args);
            }
        }
        private void OnRecvCompleted(object sender, SocketAsyncEventArgs args)
        {
            if (args.BytesTransferred > 0)
            {
                try
                {
                    string recvString = Encoding.UTF8.GetString(args.Buffer, 0, args.BytesTransferred);
                    Console.WriteLine($"받은 데이터 {recvString}");
                    RegisterRecv(args);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
            else
            {
                //소켓 종료
                Disconnect();
            }
        }
        #endregion
    }
}
