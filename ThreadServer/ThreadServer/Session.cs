using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerCore
{
    public abstract class Session
    {
        Socket _socket;
        int _disconnected = 0; //멤버변수
        RecvBuffer _recvBuffer = new RecvBuffer(2048);

        SocketAsyncEventArgs _sendArgs = new SocketAsyncEventArgs();
        Queue<byte[]> _sendQueue = new Queue<byte[]>();

        List<ArraySegment<byte>> _pendingList = new List<ArraySegment<byte>>();
        //배열의 일부를 가져오는 형태


        //bool _pending = false;

        object _lock = new object(); //락킹용 오브젝트

        public void Init(Socket socket)
        {
            _socket = socket;

            SocketAsyncEventArgs recvArgs = new SocketAsyncEventArgs();
            recvArgs.Completed += OnRecvCompleted;
            //recvArgs.SetBuffer(new byte[1024], 0, 1024);

            RegisterRecv(recvArgs);

            _sendArgs.Completed += OnSendCompleted;
            //recvArgs.UserToken = "GGM";
        }

        public abstract void OnConnected(EndPoint endPoint);
        public abstract int OnRecv(ArraySegment<byte> buffer);
        public abstract void OnSend(int numOfBytes);
        public abstract void OnDisconnected(EndPoint endPoint);


        public void Send(byte[] sendBuffer)
        {
           lock(_lock)
            {
                _sendQueue.Enqueue(sendBuffer);

                //보내려고 대기중인 얘가 없다면
                if(_pendingList.Count == 0)
                {
                    RegisterSend();
                }
            }
        }

        public void Disconnect()
        {
            if (Interlocked.Exchange(ref _disconnected, 1) == 1)
            {
                return;
            }
            OnDisconnected(_socket.RemoteEndPoint);
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }

        #region 네트워크 통신부

        private void RegisterSend()
        {
            //_pending = true;

            //byte[] buffer = _sendQueue.Dequeue();
            //_sendArgs.SetBuffer(buffer, 0, buffer.Length);
            while (_sendQueue.Count > 0)
            {
                byte[] buffer = _sendQueue.Dequeue();
                //_sendArgs.BufferList.Add(new ArraySegment<byte>(buffer, 0, buffer.Length));
                //이렇게하면 안된다.
                _pendingList.Add(new ArraySegment<byte>(buffer, 0, buffer.Length));
            }
            _sendArgs.BufferList = _pendingList;

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
                        _sendArgs.BufferList = null;
                        _pendingList.Clear();
                        OnSend(_sendArgs.BytesTransferred);
                        if(_sendQueue.Count > 0)
                        {
                            RegisterSend();
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
            _recvBuffer.Clean(); //버퍼를 청소해주고
            ArraySegment<byte> segment = _recvBuffer.WriteSegment;
            args.SetBuffer(segment.Array,segment.Offset,segment.Count);
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
                    if(!_recvBuffer.OnWrite(args.BytesTransferred))
                    {
                        Disconnect();
                        return;
                    }
                    int processLength = OnRecv(_recvBuffer.ReadSegment);
                    if(processLength < 0 || _recvBuffer.DataSize < processLength)
                    {
                        Disconnect();
                        return;
                    } 
                    if(!_recvBuffer.OnRead(processLength))
                    {
                        Disconnect();
                        return;
                    }

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
