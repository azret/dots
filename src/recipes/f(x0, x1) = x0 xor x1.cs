using System;

namespace Recipes
{
    public static class Or
    {
        static void Print(string header, Dots.Dot[] X, Dots.Dot[] Y)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("[");

            for (var i = 0; X != null && i < X.Length; i++)
            {
                if (i > 0)
                {
                    Console.Write($", ");
                }

                Console.Write($"{X[i].y}");
            }

            Console.Write("]");

            Console.Write(" = ");

            Console.Write("[");

            for (var i = 0; Y != null && i < Y.Length; i++)
            {
                if (i > 0)
                {
                    Console.Write($", ");
                }

                Console.Write($"{Y[i].y}");
            }

            Console.Write("]");

            Console.WriteLine();

            Console.ResetColor();
        }
        
        static Dots.Dot[][] Data = new Dots.Dot[][] 
        {
            new Dots.Dot[] { -1, -1 },
            new Dots.Dot[] { -1, 1 },
            new Dots.Dot[] { 1, -1 },
            new Dots.Dot[] { 1, 1 },
        };

        static Dots.Dot[][] Answer = new Dots.Dot[][]
        {
            new Dots.Dot[] { -1 },
            new Dots.Dot[] { 1 },
            new Dots.Dot[] { 1 },
            new Dots.Dot[] { -1 },
        };         

        static void Test(Dots.Dot[] Y, Dots.Dot[][] H)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                Dots.compute(Y, H, Data[i]);

                Print("X", Data[i], Y);
            }
        }

        static void Run(Func<double> α, Dots.IFunction F, ref Dots.Dot[] Y, 
            Dots.Dot[][] H, int K, Func<int, Dots.Dot[]> X, Func<int, Dots.Dot[]> T, int episodes,
            Func<int, Dots.Dot[], double, int> epoch)
        {
            
            for (int episode = 0; episode < episodes; episode++)
            {
                int k = Dots.Dot.random(K);

                var x = X(k); var t = T(k);
                
                double E = Dots.sgd(x, ref Y, H, t, α());

                if (E <= double.Epsilon || double.IsNaN(E) || double.IsInfinity(E))
                {
                    break;
                }

                if (epoch != null)
                {
                    episode = epoch(episode, x, E);
                }
            }
        }
    
        static void Main(string[] args)
        {
            bool canceled = false;

            var H = new Dots.Dot[][]
            {
                Dots.create(7, Dots.tanh().F) 
            };

            Dots.Dot[] Y = null;

            Test(Y, H);

            double E = 0.0;

            Run(() => 0.1,

                Dots.tanh().F,

                ref Y,

                H,

                Data.Length,
                
                (k) => 
                {
                    return Data[k];
                },

                (k) =>
                {
                    return Answer[k];
                },

                32 * 32 * 1024,

                (episode, X, error)=>
                {
                    E += error * error * (episode + 1);  

                    Console.WriteLine($"{1 / E}");

                    if (canceled)
                    {
                        episode = int.MaxValue - 1;
                    }

                    return episode;
                }

            );

            Test(Y, H);

            Console.ReadKey();
        }
    }
}