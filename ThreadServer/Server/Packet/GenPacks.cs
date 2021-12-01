
using ServerCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

public enum PacketID
{
    ChatMSG = 1, 
	ChatBroad = 2, 
	
}

public interface IPacket
{
	ushort Protocol { get; }
	void Read(ArraySegment<byte> segment);
	ArraySegment<byte> Write();
}


class ChatMSG : IPacket
{
    public string chat;
    
    public ushort Protocol { get { return (ushort)PacketID.ChatMSG; } }

    public void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;
        count += sizeof(ushort);
        count += sizeof(ushort);
        
        ushort chatLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.chat = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, chatLen);
		count += chatLen;
    }

    public ArraySegment<byte> Write()
    {
        ArraySegment<byte> segment = SendBufferHelper.Open(1024);

        ushort count = 0;
        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes(this.Protocol), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);
        
        ushort chatLen = (ushort)Encoding.Unicode.GetByteCount(this.chat);
		Array.Copy(BitConverter.GetBytes(chatLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		byte[] chatByte = Encoding.Unicode.GetBytes(this.chat);
		Array.Copy(chatByte, 0, segment.Array, segment.Offset + count, chatLen);
		count += chatLen;
        
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);
    }
}

class ChatBroad : IPacket
{
    public int playerId;
	public string chat;
    
    public ushort Protocol { get { return (ushort)PacketID.ChatBroad; } }

    public void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;
        count += sizeof(ushort);
        count += sizeof(ushort);
        
        this.playerId = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
		ushort chatLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.chat = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, chatLen);
		count += chatLen;
    }

    public ArraySegment<byte> Write()
    {
        ArraySegment<byte> segment = SendBufferHelper.Open(1024);

        ushort count = 0;
        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes(this.Protocol), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);
        
        Array.Copy(BitConverter.GetBytes(this.playerId), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);
		ushort chatLen = (ushort)Encoding.Unicode.GetByteCount(this.chat);
		Array.Copy(BitConverter.GetBytes(chatLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		byte[] chatByte = Encoding.Unicode.GetBytes(this.chat);
		Array.Copy(chatByte, 0, segment.Array, segment.Offset + count, chatLen);
		count += chatLen;
        
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);
    }
}

