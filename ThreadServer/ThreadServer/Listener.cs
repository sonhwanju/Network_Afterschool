﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerCore
{
    public class Listener
    {
        Socket _listenSocket;
        Func<Session> _sessionFactory; //세션을 생성하는 메서드를 넘겨주는것


        public void Init(EndPoint endPoint, Func<Session> sessionFactory)
        {
            _listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _listenSocket.Bind(endPoint);
            _listenSocket.Listen(2000);

            _sessionFactory = sessionFactory;

            SocketAsyncEventArgs args = new SocketAsyncEventArgs(); //비동기 이벤트를 관리해주는 녀석
            args.Completed += OnAcceptCompleted;
            RegisterAccept(args); //한번 등록
        }

        private void RegisterAccept(SocketAsyncEventArgs args)
        {
            args.AcceptSocket = null;

            bool pending = _listenSocket.AcceptAsync(args);

            if(!pending)
            {
                //pending은 대기가 걸렸는가 pending이 false라는 
                //어셉트가 완성되었을 때 작업을 해줘야 함
                OnAcceptCompleted(null, args);
            }
            else
            {
                
            }
        }
        private void OnAcceptCompleted(object sender, SocketAsyncEventArgs args)
        {
            if(args.SocketError == SocketError.Success)
            {
                //에러 없이 처리된 경우
                //_onAcceptHandler(args.AcceptSocket);
                Session session = _sessionFactory();
                session.Init(args.AcceptSocket);

                session.OnConnected(args.RemoteEndPoint);
            }
            else
            {
                Console.WriteLine(args.SocketError.ToString());
            }
            RegisterAccept(args);
        }
        //public Socket Accept()
        //{
        //    return _listenSocket.Accept();
        //}
        

    }
}
