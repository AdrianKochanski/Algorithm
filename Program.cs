using System.Diagnostics;
using System.Linq;

namespace cSharpCode;
class Program
{

    static Algorithm algorithm = new Algorithm();


    static void Main(string[] args)
    {
        // algorithm isPrime seed
        foreach(int i in Enumerable.Range(1, 1000000)) 
        {
            algorithm.IsPrime(i);
        }

        A:
        try {
            Console.WriteLine("Type the first array (only numbers separated by comma)");
            IEnumerable<int> a = Console.ReadLine().Split(',').Select(n => Convert.ToInt32(n));
            
            Console.WriteLine("Type the second array (only numbers separated by comma)");
            IEnumerable<int> b = Console.ReadLine().Split(',').Select(n => Convert.ToInt32(n));

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int[] c = algorithm.SetData(a.ToArray(), b.ToArray()).GetOutput();
            stopwatch.Stop();

            Console.WriteLine("Your C array is: [{0}]", String.Join(',', c));
            Console.WriteLine("Elapsed Time: {0} ms", stopwatch.ElapsedMilliseconds);
        }
        catch(Exception e) 
        {
            goto A;
        }
        
    }

    }

class Algorithm {
    
    public int[] a;
    public int[] b;
    private Dictionary<int, bool> isPrimeMapping;
    private Dictionary<int, int> bCount;

    public Algorithm()
    {
        this.a =  new List<int>().ToArray();
        this.b =  new List<int>().ToArray();
        this.isPrimeMapping = new Dictionary<int, bool>();
        this.bCount = new Dictionary<int, int>();
    }

    public Algorithm SetData(int[] a, int[] b) {
        this.a = a;
        this.b = b;
        this.bCount = new Dictionary<int, int>();

        foreach(int n_b in b) {
            if(!bCount.ContainsKey(n_b)) {
                bCount.Add(n_b, b.Where(n => n == n_b).Count());
            }
        }

        return this;
    }

    public int[] GetOutput()
    {
        return a.Where(n_a => !IsPrime(GetBCount(n_a))).ToArray();
    }

    public bool IsPrime(int n)
    {
        if (n > 1)
        {
            if(isPrimeMapping.ContainsKey(n)) 
            {
                return isPrimeMapping[n];
            }
            else {
                bool result = Enumerable
                .Range(1, Convert.ToInt32(Math.Floor(Math.Sqrt(n))))
                .Append(n)
                .Where(x => n%x == 0)
                .SequenceEqual(new[] {1, n});

                isPrimeMapping.Add(n, result);

                return result;
            }
        }

        return false;
    }

    private int GetBCount(int n) {
        if(bCount.ContainsKey(n)) return bCount[n];
        else return 0;
    }
}
