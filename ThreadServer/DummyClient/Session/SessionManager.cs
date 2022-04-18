﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DummyClient
{
    class SessionManager
    {
        static SessionManager _instance = new SessionManager();
        public static SessionManager Instance { get { return _instance;  } }

        List<ServerSession> _sessions = new List<ServerSession>();

        object _lock = new object();
        Random _rand = new Random();

        public ServerSession Generate()
        {
            lock(_lock)
            {
                ServerSession session = new ServerSession();
                _sessions.Add(session);
                return session;
            }
        }

        public void SendForEach(string msg)
        {
            lock(_lock)
            {
                _sessions.ForEach(x =>
                {
                    Move movePacket = new Move();
                    movePacket.posX = _rand.Next(-50, 50);
                    movePacket.posY = 0;
                    movePacket.posZ = _rand.Next(-50, 50);

                    x.Send(movePacket.Write());
                });
            }
        }
    }
}
