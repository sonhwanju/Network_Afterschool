using System;
using System.Net;
using System.Text;
using System.Threading;
using ServerCore;

namespace Server
{
    
    class ServerProgram
    {
        //static void MainThread(object state)
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        Console.WriteLine("Hello Thread");
        //    }
        //}
        //static volatile bool _stop = false;
        ////메모리 배리어

        //static void ThreadMain()
        //{
        //    Console.WriteLine("Start Thread....");

        //    //if(_stop == false)
        //    //{
        //    //    while (true)
        //    //    {

        //    //    }
        //    //}

        //    while (_stop == false)
        //    {
        //        //누군가가 stop변수를 true로 변경해줄 때까지 계속해서 멈춰있다
        //    }
        //    Console.WriteLine("Stop Thread");
        //}

        //static int x = 0;
        //static int y = 0;
        //static int r1 = 0;
        //static int r2 = 0;


        //static void Thread_1()
        //{
        //    y = 1; //Store y
        //    Thread.MemoryBarrier();
        //    r1 = x; // Load x
        //}
        //static void Thread_2()
        //{
        //    x = 1; //store x
        //    Thread.MemoryBarrier();
        //    r2 = y; //load y
        //}

        //static int num;
        //static object _obj = new object(); //아무의미 없는 오브젝트를 선언함
        //static SpinLock _lock = new SpinLock();
        //static Lock _lock = new Lock();
        //static Mutex _lock = new Mutex();
        //static SpinLock _lock = new SpinLock();
        //static void Thread_1()
        //{
        //    for (int i = 0; i < 10000; i++)
        //    {
        //        bool lockTaken = false;
        //        try
        //        {
        //            _lock.Enter(ref lockTaken);
        //            //여기에 하고싶은걸 쓰면 된다.
        //        }finally
        //        {
        //            if(lockTaken)
        //            {
        //                _lock.Exit();
        //            }
        //        }

        //        //_lock.WaitOne();
        //        //num++;
        //        //_lock.ReleaseMutex();
        //        //_lock.Acquire();
        //        //num++;
        //        //_lock.Release();

        //        //Monitor.Enter(_obj);
        //        //lock(_obj)
        //        //{
        //        //    num--;
        //        //}
        //        //Monitor.Exit(_obj);
        //        //Interlocked.Increment(ref num);
        //        //인터락 연산은 아토믹 실행을 반드시 보장해준다
        //    }
        //}
        //static void Thread_2()
        //{
        //    for (int i = 0; i < 10000; i++)
        //    {
        //        //_lock.WaitOne();
        //        //num--;
        //        //_lock.ReleaseMutex();
        //        //_lock.Acquire();
        //        //num--;
        //        //_lock.Release();
        //        //lock (_obj)
        //        //{
        //        //    num++;
        //        //}
        //        //Monitor.Enter(_obj);
        //        //num++;
        //        //Monitor.Exit(_obj);
        //        //Interlocked.Decrement(ref num);
        //    }
        //}

        //public static volatile int count = 0;
        //static Lock _lock = new Lock();

        //static ThreadLocal<string> ThreadName = new ThreadLocal<string>(() => $"My Name is {Thread.CurrentThread.ManagedThreadId}"); 
        ////쓰레드 로컬 저장소로 스트링 저장소를 만듦

        //static void WhoAmI()
        //{
        //    bool repeat = ThreadName.IsValueCreated;
        //    Thread.Sleep(1000);
        //    if (repeat)
        //    {
        //        Console.WriteLine(ThreadName.Value + "(repeated)");
        //    }else
        //    {
        //        Console.WriteLine(ThreadName.Value);
        //    }
        //    //ThreadName.Value = $"My Name is {Thread.CurrentThread.ManagedThreadId}";
        //}

        static Listener _listener = new Listener();

        public static GameRoom Room = new GameRoom();

        //static void OnAcceptHandler(Socket clientSocket)
        //{
        //    try
        //    {
        //        ////네트워크에서 데이터 통신의 단위 byte        (bitStream) -> 몰라도댐

        //        //byte[] recvBuffer = new byte[1024]; //지금은 그냥 무식하게 1024바이트로 받는다
        //        //int recvCnt = clientSocket.Receive(recvBuffer); //받기 블럭킹 함수
        //        //string recvString = Encoding.UTF8.GetString(recvBuffer, 0, recvCnt);
        //        //Console.WriteLine($"받은 데이터 : {recvString}");

        //        byte[] sendBuffer = Encoding.UTF8.GetBytes("Welcome to GGM server");

        //        GameSession session = new GameSession();
        //        session.Init(clientSocket);

        //        session.Send(sendBuffer);

        //        Thread.Sleep(1000);
        //        session.Disconnect();

        //        //clientSocket.Send(sendBuffer); //블럭킹 함수
        //        //                               //난 니가 오는걸 듣지도 않고 보내지도 않겠다
        //        //clientSocket.Shutdown(SocketShutdown.Both);
        //        //clientSocket.Close();
        //    }
        //    catch(Exception e) {

        //    }
        //}
        static void Main(string[] args)
        {
            //PacketManager.Instance.Register();
            //입장을 담당할 리스너를 만들기
            string host = Dns.GetHostName();

            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            //엔드포인트는 최종적으로 ip주소와 포트를 바인딩시켜서 만드는 도착점이다.
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 54000);
            //Socket listenSocket = new Socket(endPoint.AddressFamily,SocketType.Stream,ProtocolType.Tcp);
            _listener.Init(endPoint, () => SessionManager.Instancce.Generate());

            while (true)
            {
                ;
            }
        }
    }
}
