using System;
using System.Threading;
using System.Threading.Tasks;
namespace ServerCore
{
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

        static int num;

        static void Thread_1()
        {
            for (int i = 0; i < 100000; i++)
            {
                //num--;
                Interlocked.Increment(ref num);
                //인터락 연산은 아토믹 실행을 반드시 보장해준다
            }
        }
        static void Thread_2()
        {
            for (int i = 0; i < 100000; i++)
            {
                //num++;
                Interlocked.Decrement(ref num);
            }
        }
        static void Main(string[] args)
        {
            //int count = 0;
            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);

            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);
            Console.WriteLine($"Exit : {num}");

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
        }
    }
}
