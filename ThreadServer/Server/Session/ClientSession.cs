using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using ServerCore;

namespace Server
{
    class ClientSession : PacketSession
    {
        public int sessionId { get; set; }
        public GameRoom Room { get; set; }

        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected From : {endPoint}");
            ServerProgram.Room.Push(() => ServerProgram.Room.Enter(this));

            //Send(sendBuffer);
            //Disconnect();
        }
        public override void OnDisconnected(EndPoint endPoint)
        {
            SessionManager.Instancce.Remove(this);
            Console.WriteLine($"OnDisConnected From : {endPoint}");

            if(Room != null)
            {
                GameRoom room = Room;
                Room.Push(() =>room.Leave(this));
                Room = null;
            }
        }

        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            PacketManager.Instance.OnRecvPacket(this, buffer);
        }

        public override void OnSend(int numOfBytes)
        {
            Console.WriteLine($"전송된 바이트 수 : {numOfBytes}");
        }
    }
    }
