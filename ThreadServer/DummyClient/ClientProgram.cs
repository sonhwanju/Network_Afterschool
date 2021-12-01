using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ServerCore;

namespace DummyClient
{
 
    class ClientProgram
    {
        static void Main(string[] args)
        {
            //PacketManager.Instance.Register();


            //입장을 담당할 리스너를 만들기
            string host = Dns.GetHostName();

            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            //엔드포인트는 최종적으로 ip주소와 포트를 바인딩시켜서 만드는 도착점이다.
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 54000);

            Connector connector = new Connector();
            connector.Connect(endPoint, () => SessionManager.Instancce.Generate(),10);


            while (true)
            {
                
                try
                {
                    SessionManager.Instancce.SendForEach("Hello Server! This is Client");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                Thread.Sleep(250); //일반적인 mmo에서 이동패킷은 1초에 4번정도 쏘게 되어있다.
            }


        }
    }
}
