using System;

namespace Recipes
{
    public static class Identity
    {
        static void Print(string header, Dots.Dot[] vec)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine($"{header}:\r\n");

            for (var i = 0; i < vec.Length; i++)
            {
                Console.WriteLine($"{vec[i].y}");
            }

            Console.WriteLine();

            Console.ResetColor();
        }

        static void Inspect(string header, Dots.Dot[] vec, ConsoleColor color)
        {
            Console.WriteLine($"{header}:\r\n");

            for (var i = 0; i < vec.Length; i++)
            {
                Console.ForegroundColor = color;
                Console.Write($"{vec[i].y}");
                Console.ResetColor();
                Console.WriteLine($" = {vec[i].inspect()}");
            }

            Console.WriteLine();
        }

        static Dots.Dot[] Vector()
        {
            Dots.Dot[] vec = new Dots.Dot[1 + Dots.Dot.random(11)];

            for (var i = 0; i < vec.Length; i++)
            {
                vec[i] = new Dots.Dot()
                {
                    y = Dots.Dot.random()
                };
            }

            return vec;
        }

        static void Test(Dots.Dot[] Y, Dots.Dot[][] H)
        {
            Console.WriteLine("\r\n**************************\r\n");

            Dots.Dot[] X;

            X = Vector();

            Print("X", X);

            Dots.compute(Y, H, X);
            
            Console.WriteLine("\r\n=========================\r\n");

            for (int l = 0; l < H.Length; l++)
            {
                Inspect($"H{l}", H[l], ConsoleColor.DarkGray);
            }

            Inspect("Y", Y, ConsoleColor.Green);

            Console.WriteLine("\r\nE:\r\n");

            Console.WriteLine($"{Dots.error(Y, X)}");

        }

        static void Run(bool verbose)
        {
            const int OUTPUTS = 7;

            var H = new Dots.Dot[][]
            {
               Dots.create(OUTPUTS)
            };

            var Y = Dots.create(OUTPUTS);
            
            Test(Y, H);

            for (int episode = 0; episode < 32 * 128 * 1024; episode++)
            {
                var X = Vector();

                Dots.grow(X, H, Y);

                Dots.compute(Y, H, X);

                double E = Dots.train(Y, H, X, 0.01);

                if (E <= double.Epsilon || double.IsNaN(E) || double.IsInfinity(E))
                {
                    break;
                }

                if (verbose)
                {
                    Console.WriteLine($"{E}");
                }
            }

            Test(Y, H);
        }
    
        static void Main(string[] args)
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
            };

            Run(false);

            Console.ReadKey();
        }
    }
}