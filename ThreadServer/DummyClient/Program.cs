using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DummyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //입장을 담당할 리스너를 만들기
            string host = Dns.GetHostName();

            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            //엔드포인트는 최종적으로 ip주소와 포트를 바인딩시켜서 만드는 도착점이다.
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 54000);

            while (true)
            {
                Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    socket.Connect(endPoint); //블럭킹

                    //여기까지 코드가 도착하면 연결이 된것
                    Console.WriteLine($"Connected to {socket.RemoteEndPoint}");

                    //데이터 보내기
                    for (int i = 0; i < 5; i++)
                    {
                        byte[] sendBuffer = Encoding.UTF8.GetBytes($"Hello GGM {i}");
                        int sendCnt = socket.Send(sendBuffer);
                    }
                    //여기서 받기가 이뤄져야한다

                    byte[] recvBuffer = new byte[1024]; //지금은 그냥 무식하게 1024바이트로 받는다
                    int recvCnt = socket.Receive(recvBuffer); //받기 블럭킹 함수
                    string recvString = Encoding.UTF8.GetString(recvBuffer, 0, recvCnt);
                    Console.WriteLine($"받은 데이터 : {recvString}");

                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Thread.Sleep(500);
            }


        }
    }
}
