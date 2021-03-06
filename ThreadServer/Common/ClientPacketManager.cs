
using ServerCore;
using System;
using System.Collections.Generic;

public class PacketManager
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
    Dictionary<ushort, Func<PacketSession, ArraySegment<byte>, IPacket>> _onRecv
        = new Dictionary<ushort, Func<PacketSession, ArraySegment<byte>,IPacket>>();

    public void Register()
    {
        
        _onRecv.Add((ushort)PacketID.BroadcastEnterGame, MakePacket<BroadcastEnterGame>);
        _handler.Add((ushort)PacketID.BroadcastEnterGame, PacketHandler.BroadcastEnterGameHandler);


        _onRecv.Add((ushort)PacketID.BroadcastLeaveGame, MakePacket<BroadcastLeaveGame>);
        _handler.Add((ushort)PacketID.BroadcastLeaveGame, PacketHandler.BroadcastLeaveGameHandler);


        _onRecv.Add((ushort)PacketID.PlayerList, MakePacket<PlayerList>);
        _handler.Add((ushort)PacketID.PlayerList, PacketHandler.PlayerListHandler);


        _onRecv.Add((ushort)PacketID.BroadcastMove, MakePacket<BroadcastMove>);
        _handler.Add((ushort)PacketID.BroadcastMove, PacketHandler.BroadcastMoveHandler);


    }

    public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer, 
            Action<PacketSession, IPacket> onRecvCallBack = null)
    {
        ushort count = 0;
        ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
        count += sizeof(ushort);
        ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
        count += sizeof(ushort);

        Func<PacketSession, ArraySegment<byte>, IPacket> func = null;
        if (_onRecv.TryGetValue(id, out func))
        {
            IPacket packet = func(session, buffer);

            if(onRecvCallBack != null){ //???????????? ?????? ??????
                onRecvCallBack(session, packet);
            }else{
                HandlePacket(session, packet);
            }
        }
    }

    private T MakePacket<T>(PacketSession session, ArraySegment<byte> buffer)
        where T : IPacket, new()
    {
        T t = new T();
        t.Read(buffer);

        return t;
    }

    public void HandlePacket(PacketSession session, IPacket t){
        Action<PacketSession, IPacket> action = null;
        if (_handler.TryGetValue(t.Protocol, out action))
        {
            action(session, t);
        }
    }
}
