using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ServerCore;

namespace DummyClient
{
    class GameSession : Session
    {
        public override void OnConnected(EndPoint endPoint)
        {
            //여기까지 코드가 도착하면 연결이 된것
            Console.WriteLine($"Connected to {endPoint}");

            //데이터 보내기
            for (int i = 0; i < 5; i++)
            {
                byte[] sendBuffer = Encoding.UTF8.GetBytes($"Hello GGM {i}");
                Send(sendBuffer);
            }
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnDisconnected from : {endPoint}");
        }

        public override int OnRecv(ArraySegment<byte> buffer)
        {
            //여기서 받기가 이뤄져야한다

            string recvString = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
            Console.WriteLine($"받은 데이터 : {recvString}");

            return buffer.Count; //임시
        }

        public override void OnSend(int numOfBytes)
        {
            Console.WriteLine($"Transfered Bytes : {numOfBytes}");
        }
    }
    class ClientProgram
    {
        static void Main(string[] args)
        {
            //입장을 담당할 리스너를 만들기
            string host = Dns.GetHostName();

            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            //엔드포인트는 최종적으로 ip주소와 포트를 바인딩시켜서 만드는 도착점이다.
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 54000);

            Connector connector = new Connector();
            connector.Connect(endPoint, () => new GameSession());


            while (true)
            {
                
                try
                {

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }


        }
    }
}
