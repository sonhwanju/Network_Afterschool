﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerCore
{
    public class Connector
    {
        Func<Session> _sessionFactory;

        public void Connect(IPEndPoint endPoint, Func<Session> sessionFactory)
        {
            _sessionFactory = sessionFactory;

            Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.Completed += OnConnectedCompleted;
            args.RemoteEndPoint = endPoint;
            args.UserToken = socket;
            RegisterConnect(args);
        }

        private void RegisterConnect(SocketAsyncEventArgs args)
        {
            Socket socket = (Socket)args.UserToken;
            if (socket == null) return;

            bool pending = socket.ConnectAsync(args);
            if(!pending)
            {
                OnConnectedCompleted(null, args);
            }
        }
        private void OnConnectedCompleted(object sender, SocketAsyncEventArgs args)
        {
            if(args.SocketError == SocketError.Success)
            {
                Session session = _sessionFactory();
                session.Init(args.ConnectSocket);
                session.OnConnected(args.RemoteEndPoint);
            }
            else
            {
                Console.WriteLine($"OnConnection COmplete Failed : {args.SocketError}");
            }
        }
    }
}
