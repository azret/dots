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

        static void Inspect(string header, Dots.Dot[] vec)
        {
            Console.WriteLine($"{header}:\r\n");

            for (var i = 0; i < vec.Length; i++)
            {
                Console.WriteLine($"{vec[i].inspect()}");
            }

            Console.WriteLine();
        }

        static Dots.Dot[] Vector(int size)
        {
            Dots.Dot[] vec = new Dots.Dot[size];

            for (var i = 0; i < vec.Length; i++)
            {
                vec[i] = new Dots.Dot()
                {
                    y = Dots.Dot.random()
                };
            }

            return vec;
        }

        static void Test(int inputs, Dots.Dot[][] H, Dots.Dot[] Y)
        {
            Console.WriteLine("\r\n**************************\r\n");

            Dots.Dot[] X;

            X = Vector(inputs);

            Print("X", X);

            Dots.compute(X, H, Y);

            Print("Y", Y);

            Console.WriteLine("\r\n=========================\r\n");

            for (int l = 0; l < H.Length; l++)
            {
                Inspect($"H{l}", H[l]);
            }

            Inspect("O", Y);

            Console.WriteLine("\r\nE:\r\n");

            Console.WriteLine($"{Dots.error(Y, X)}");

        }

        static void Run(bool verbose)
        {
            const int INPUTS = 3; const int HIDDEN = INPUTS; const int OUTPUTS = INPUTS;

            var H = new Dots.Dot[][]
            {
               // Dots.create(HIDDEN)
            };

            var Y = Dots.create(OUTPUTS);

            Dots.path(INPUTS, H, Y);

            Test(INPUTS, H, Y);

            for (int episode = 0; episode < 21 * 128 * 1024; episode++)
            {
                var X = Vector(INPUTS);

                Dots.compute(X, H, Y);

                double E = Dots.learn(Y, 0.001, X);

                if (E <= double.Epsilon || double.IsNaN(E) || double.IsInfinity(E))
                {
                    break;
                }

                if (verbose)
                {
                    Console.WriteLine($"{E}");
                }
            }

            Test(INPUTS, H, Y);
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