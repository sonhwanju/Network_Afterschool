using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class GameRoom : IJobQueue
    {
        List<ClientSession> _sessions = new List<ClientSession>();

        object _lock = new object();

        JobQueue _jobQueue = new JobQueue();

        public void Push(Action job)
        {
            _jobQueue.Push(job);
        }

        public void Enter(ClientSession session)
        {
            lock(_lock)
            {
                if (!_sessions.Contains(session))
                {
                    _sessions.Add(session);
                    session.Room = this;
                }
            }
            
        }
        public void Leave(ClientSession session)
        {
            lock (_lock)
            {
                if (_sessions.Contains(session))
                {
                    _sessions.Remove(session);
                }
            }
        }
        public void BroadCast(ClientSession session, string chat)
        {
            ChatBroad chatBroad = new ChatBroad();
            chatBroad.playerId = session.sessionId;
            chatBroad.chat = chat;

            ArraySegment<byte> segment = chatBroad.Write();

            lock(_lock)
            {
                _sessions.ForEach(x => x.Send(segment));
            }
        }

        
    }
}
