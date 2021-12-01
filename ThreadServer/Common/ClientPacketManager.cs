
using ServerCore;
using System;
using System.Collections.Generic;

class PacketManager
{
    #region Singleton
    static PacketManager _instance = new PacketManager();
    public static PacketManager Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion

    private PacketManager()
    {
        Register();
    }

    Dictionary<ushort, Action<PacketSession, IPacket>> _handler
        = new Dictionary<ushort, Action<PacketSession, IPacket>>();
    Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>> _onRecv
        = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>>();

    public void Register()
    {
        
    }

    public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer)
    {
        ushort count = 0;
        ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
        count += sizeof(ushort);
        ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
        count += sizeof(ushort);

        Action<PacketSession, ArraySegment<byte>> action = null;
        if (_onRecv.TryGetValue(id, out action))
        {
            action(session, buffer);
        }
    }

    private void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer)
        where T : IPacket, new()
    {
        T t = new T();
        t.Read(buffer);

        Action<PacketSession, IPacket> action = null;
        if (_handler.TryGetValue(t.Protocol, out action))
        {
            action(session, t);
        }
    }
}
