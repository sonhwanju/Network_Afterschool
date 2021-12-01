START ../PacketGenerator/bin/PacketGenerator.exe ../PacketGenerator/PDL.xml
TIMEOUT 2
XCOPY /Y GenPacks.cs "../DummyClient/Packet"
XCOPY /Y GenPacks.cs "../Server/Packet"
XCOPY /Y ClientPacketManager.cs "../DummyClient/Packet"
XCOPY /Y ServerPacketManager.cs "../Server/Packet"
PAUSE