using DummyClient;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;

class PacketHandler
{
    public static void ChatBroadHandler(PacketSession session, IPacket packet)
    {
        ChatBroad cbPacket = packet as ChatBroad;

        ServerSession serverSession = session as ServerSession;

        if(cbPacket.playerId == 1)
        {
            Console.WriteLine(cbPacket.chat);
        }
    }
}
