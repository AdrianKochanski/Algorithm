using System.Diagnostics;

namespace cSharpCode;
class Program
{
    static void Main(string[] args)
    {
        A:
        try {
            Console.WriteLine("Type the first array (only numbers separated by comma)");
            IEnumerable<int> a = Console.ReadLine().Split(',').Select(n => Convert.ToInt32(n));
            
            Console.WriteLine("Type the second array (only numbers separated by comma)");
            IEnumerable<int> b = Console.ReadLine().Split(',').Select(n => Convert.ToInt32(n));

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int[] c = Algorithm(a.ToArray(), b.ToArray());
            stopwatch.Stop();

            Console.WriteLine("Your C array is: [{0}]", String.Join(',', c));
            Console.WriteLine("Elapsed Time: {0} ms", stopwatch.ElapsedMilliseconds);
        }
        catch(Exception e) 
        {
            goto A;
        }
        
    }

    static int[] Algorithm(int[] a, int[] b)
    {
        return a.Where(n_a => !IsPrime(b.Where(n_b => n_b == n_a).Count())).ToArray();
    }

    static bool IsPrime(int n)
    {
        if (n > 1)
        {
            return (
                Enumerable
                .Range(1, Convert.ToInt32(Math.Floor(Math.Sqrt(n))))
                .Append(n)
                .Where(x => n%x == 0)
                .SequenceEqual(new[] {1, n})
            );
        }

        return false;
    }
}
