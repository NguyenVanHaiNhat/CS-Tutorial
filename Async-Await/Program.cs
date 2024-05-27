internal class Program
{
    static void DoSomeThing(int second, string mgs, ConsoleColor color)
    {
        lock (Console.Out)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"{mgs,10} ... Start");
            Console.ResetColor();
        }

        // các thread khác muốn truy cập tới biến a => khóa biến a lại để cái threa khác không truy cập được cho đến khi đc mở khóa

        for (int i = 1; i <= second; i++)
        {
            lock (Console.Out)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"{mgs,10} {i,2}");
                Console.ResetColor();
            }


            Thread.Sleep(1000);
        }
        lock (Console.Out)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"{mgs,10} ... End");
            Console.ResetColor();
        }

    }
    static async Task task2()
    {
        Task t2 = new Task(
            () =>
            {
                DoSomeThing(10, "T2", ConsoleColor.Red);
            }
        );
        t2.Start();
        // t2.Wait();
        await t2; // khác với Wait thì await không khóa thread chính của Main
        Console.WriteLine("T2 đã hoàn thành");
        // return t2; không cần return nữa vì await đã làm việc đó
    }
    static async Task task3()
    {
        Task t3 = new Task(
            (object ob) =>
            {
                string tentacvu = (string)ob;
                DoSomeThing(4, tentacvu, ConsoleColor.Blue);
            }, "T3");
        t3.Start();
        // t3.Wait();
        await t3;
        Console.WriteLine("T3 đã hoàn thành");
        // return t3;
    }
    // async/await
    private static async Task Main(string[] args)
    {
        // synchronized

        // DoSomeThing(6, "T1", ConsoleColor.Magenta);


        // Task t2 = task2();
        // Task t3 = task3();

        // khi khai báo Wait ở trong task thì task chính sẽ bị khóa lại cho đến khi cái task phụ hoàn thành xong mới được chạy
        // => thiết kế để chạy song song nhiều tác vụ cùng lúc bị phá vỡ ==> nên sẽ sử dung async/await

        // t2.Wait();
        // t3.Wait(); // đảm bảo là task kết thúc mới hiện ra dòng dưới
        // Task.WaitAll(t2, t3);
        // await t2;
        // await t3;

        // Console.WriteLine("Press any key");
        // khi task T1 kết thúc tức là hàm Main kết thúc thì task T2 cũng kết thúc khi chưa chạy xong => cần Main lại để cho T2 chạy xong
        // Console.ReadKey();

        // asynchronous
        // Task<T>
        Task<string> t4 = new Task<string>(
            () =>
            {
                DoSomeThing(10, "T4", ConsoleColor.Green);
                return "Return from T4";
            }
        );
        Task<string> t5 = new Task<string>(
            (object ob) =>
            {
                string t = (string)ob;
                DoSomeThing(4, t, ConsoleColor.Yellow);
                return $"Return from {t}";
            }
        , "T5");
        t4.Start();
        t5.Start();
        DoSomeThing(6, "T1", ConsoleColor.Magenta);

        Task.WaitAll(t4, t5);

        var kq4 = t4.Result;
        var kq5 = t5.Result;

        Console.WriteLine("Press any key");
        Console.ReadKey();

    }
}