// delegate (type) bien = phuong thuc
class Program
{
    public delegate void ShowLog(string message);

    static void Info(string s)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(s);
        Console.ResetColor();
    }
    static void Warning(string s)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(s);
        Console.ResetColor();
    }
    static void Tong(int x, int y, ShowLog log)
    {
        int kq = x + y;
        log?.Invoke($"tong la {kq}");
    }
    static void Hieu(int x, int y, ShowLog log)
    {
        int kq = x - y;
        log?.Invoke($"hieu la {kq}");
    }
    public static void Main(string[] args)
    {
        // ShowLog log = null; 
        // biến kiểu delegate có thể 1 lúc tham chiếu đến nhiều phương thức, sử dung : +=

        // if (log != null)
        // {
        //     log("Xin chao");
        //     log.Invoke("Xin chao Abc");
        // }

        // log = Info;
        // log?.Invoke("Xin chao");

        // log = Warning;
        // log?.Invoke("Nguy hiem");

        // log += Info;
        // log += Info;
        // log += Info;

        // log += Warning;
        // log += Warning;

        // log?.Invoke("Xin chao");

        // Action, Func: delegate - gereric

        // Action action; // delegate void TenKieu();
        // Action<string, int> action1; // delegate void TenKieu(string s, int i);

        // Action<string> action2; // delegate void TenKieu(string s);

        // action2 = Warning;
        // action2 += Info;
        // action2?.Invoke("Thong bao tu Action");

        // Func<int> f1; // trả về kiểu int và k có tham số
        // Func<string, double, string> f2; // trả về kiểu string (vì tham số cuối cùng là string) và có 2 tham số là string và double

        // Func<int, int, int> tinhtoan;
        // int a = 5;
        // int b = 10;

        // tinhtoan = Tong;
        // Console.WriteLine($"KQ {tinhtoan(a, b)}");

        Tong(4, 5, Info);
        Hieu(9, 5, Warning);
    }
}