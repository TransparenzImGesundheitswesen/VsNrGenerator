using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsNrGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            List<string> numbers = new List<string> { "111", "222", "333", "444", "555", "666", "777", "888", "999", "000" };

            Parallel.For(1, 27,  i =>
            {
                char c = letters[i - 1];

                using (System.IO.StreamWriter datei = new System.IO.StreamWriter(string.Format("eGK-Nummern_{0}.txt", c), false))
                {
                    for (long l = 0; l <= 99999999; l++)
                    {
                        string erg = string.Format("{0}{1:00000000}{2}", c, l, PrüfsummeBerechnen(i, l));

                        if (!numbers.Any(s => erg.Contains(s)))
                            datei.WriteLine(erg);

                        if (l % 1000000 == 0)
                            System.Console.WriteLine(erg);
                    }
                }
            });

            System.Console.WriteLine("Fertig!");
            System.Console.ReadLine();
        }

        static long PrüfsummeBerechnen(int i, long l)
        {
            long sum =
                QuersummeBerechnen(i / 10) +
                QuersummeBerechnen((i % 10) * 2) +
                QuersummeBerechnen(NteStelle(l, 8)) +
                QuersummeBerechnen(NteStelle(l, 7) * 2) +
                QuersummeBerechnen(NteStelle(l, 6)) +
                QuersummeBerechnen(NteStelle(l, 5) * 2) +
                QuersummeBerechnen(NteStelle(l, 4)) +
                QuersummeBerechnen(NteStelle(l, 3) * 2) +
                QuersummeBerechnen(NteStelle(l, 2)) +
                QuersummeBerechnen(NteStelle(l, 1) * 2);

            return sum % 10;
        }

        static long NteStelle(long x, long n)
        {
            n--;

            while (n-- > 0)
            {
                x /= 10;
            }
            return x % 10;
        }

        static long QuersummeBerechnen(long n)
        {
            long sum = 0;

            while (n != 0)
            {
                sum += n % 10;
                n /= 10;
            }

            return sum;
        }
    }
}
