using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//TLS => Thread Local Storage
namespace ServerCore
{
    //class Lock
    //{
    //    //AutoResetEvent _available = new AutoResetEvent(true);
    //    ManualResetEvent _available = new ManualResetEvent(true);
    //    //true면 누구나 들어올 수 있는 상태, false면 들어올 수 없는 상태

    //    public void Acquire()
    //    {
    //        _available.WaitOne();//한명이 들어가면 문을 닫는다
    //        //문이 자동으로 닫히기 때문에 한명만 들어가면 자동으로 락된다.
    //        _available.Reset();
    //    }
    //    public void Release()
    //    {
    //        _available.Set();//다시 들어올 수 있게 켜준다
    //    }
    //}
    //class SpinLock
    //{
    //    int _locked = 0; //잠겨있는가

    //    public void Acquire()
    //    {
    //        while (true)
    //        {
    //            //잠금이 풀릴때까지 기다려야 한다
    //            int expected = 0; //내가 예상한 값
    //            int desired = 1; //바꾸기를 원하는 값
    //            //int origin = Interlocked.CompareExchange(ref _locked, desired, expected); //Interlocked.Exchange(ref _locked, 1);
    //            if(Interlocked.CompareExchange(ref _locked, desired, expected)== expected)
    //            {
    //                break;
    //            }
    //            Thread.Yield();
    //        }
    //    }
    //    public void Release()
    //    {
    //        _locked = 0;
    //    }
    //}
    //ThreadPool.SetMinThreads(1, 1);
    //ThreadPool.SetMaxThreads(3, 3);
    //Parallel.Invoke(WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI);

    //Task t1 = new Task(() =>
    //{
    //    for (int i = 0; i < 1000000; i++)
    //    {
    //        _lock.WriteLock();
    //        count++;
    //        _lock.WriteUnLock();
    //    }
    //});
    //Task t2 = new Task(() =>
    //{
    //    for (int i = 0; i < 1000000; i++)
    //    {
    //        _lock.WriteLock();
    //        count;
    //        _lock.WriteUnLock();
    //    }
    //});
    //t1.Start();t2.Start();
    //Task.WaitAll(t1, t2);
    //Console.WriteLine(count);
    //Thread.Sleep(1); //무조건 휴식 1ms 정도를 운영체제가 판단해서 재워줌 / 반드시 1ms를 보장받지는 못함
    //Thread.Sleep(0); //자신보다 우선순위가 높은 녀석들에게는 양보하고, 그렇지 않다면 양보하지 않는다.
    //Thread.Yield(); //실행가능한 쓰레드가 있다면 양보, 그렇지 않다면 그냥 대기
    //int count = 0;
    //Task t1 = new Task(Thread_1);
    //Task t2 = new Task(Thread_2);

    //t1.Start();
    //t2.Start();

    //Task.WaitAll(t1, t2);
    //Console.WriteLine($"Exit : {num}");

    //Console.WriteLine($"Exit !! : {count}");

    //int[,] arr = new int[10000, 10000];

    //{
    //    long start = DateTime.Now.Ticks;
    //    for (int i = 0; i < 10000; i++)
    //    {
    //        for (int j = 0; j < 10000; j++)
    //        {
    //            arr[i, j] = 1;
    //        }
    //    }

    //    long ed = DateTime.Now.Ticks;

    //    Console.WriteLine($"행우선 접근 걸린 시간 {ed - start}");
    //}

    //{
    //    long start = DateTime.Now.Ticks;
    //    for (int i = 0; i < 10000; i++)
    //    {
    //        for (int j = 0; j < 10000; j++)
    //        {
    //            arr[j, i] = 0;
    //        }
    //    }
    //    long ed = DateTime.Now.Ticks;

    //    Console.WriteLine($"열우선 접근 걸린 시간 {ed - start}");
    //}
    //Task t = new Task(ThreadMain);
    //t.Start();

    //Thread.Sleep(1000); //현재 실행중인 쓰레드를 1초동안 잠재운다
    //_stop = true;

    //Console.WriteLine("Calling Stop!");
    //Console.WriteLine("Waiting for stop...");
    //t.Wait();

    //Console.WriteLine("Stop Complete");

    //ThreadPool.SetMinThreads(1, 1);
    //ThreadPool.SetMaxThreads(5, 5);

    //for (int i = 0; i < 5; i++)
    //{
    //    Task t = new Task(() =>
    //    {
    //        while (true)
    //        {

    //        }
    //    }, TaskCreationOptions.LongRunning);
    //    t.Start();
    //    //ThreadPool.QueueUserWorkItem(obj =>
    //    //{
    //    //    while (true)
    //    //    {

    //    //    }
    //    //});
    //}
    //ThreadPool.QueueUserWorkItem(MainThread);

    //while (true)
    //{

    //}

    //for (int i = 0; i < 1000; i++)
    //{
    //    //쓰레드풀에서 꺼내오는 모든 쓰레드들은 전부 백그라운드 쓰레드이다.
    //    //쓰레드풀은 용역업체다. 적재적소에 알맞는 자원들을 공급해준다.
    //    ThreadPool.QueueUserWorkItem(MainThread);
    //}


    //Thread t = new Thread(MainThread);
    //t.IsBackground = true;
    //t.Name = "테스트";
    //t.Start();

    //Console.WriteLine("Hello World");

    //t.Join();

    class Program
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

        static void OnAcceptHandler(Socket clientSocket)
        {
            try
            {
                ////네트워크에서 데이터 통신의 단위 byte        (bitStream) -> 몰라도댐

                //byte[] recvBuffer = new byte[1024]; //지금은 그냥 무식하게 1024바이트로 받는다
                //int recvCnt = clientSocket.Receive(recvBuffer); //받기 블럭킹 함수
                //string recvString = Encoding.UTF8.GetString(recvBuffer, 0, recvCnt);
                //Console.WriteLine($"받은 데이터 : {recvString}");

                byte[] sendBuffer = Encoding.UTF8.GetBytes("Welcome to GGM server");

                Session session = new Session();
                session.Init(clientSocket);

                session.Send(sendBuffer);

                Thread.Sleep(1000);
                session.Disconnect();

                //clientSocket.Send(sendBuffer); //블럭킹 함수
                //                               //난 니가 오는걸 듣지도 않고 보내지도 않겠다
                //clientSocket.Shutdown(SocketShutdown.Both);
                //clientSocket.Close();
            }
            catch(Exception e) {
            
            }
        }
        static void Main(string[] args)
        {
            //입장을 담당할 리스너를 만들기
            string host = Dns.GetHostName();

            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            //엔드포인트는 최종적으로 ip주소와 포트를 바인딩시켜서 만드는 도착점이다.
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 54000);
            //Socket listenSocket = new Socket(endPoint.AddressFamily,SocketType.Stream,ProtocolType.Tcp);
            _listener.Init(endPoint, OnAcceptHandler);

            while (true)
            {

            }
        }
    }
}
