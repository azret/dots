using System;

namespace Recipes
{
    public static class Identity
    {
        static void Print(string header, Dots.Dot[] vec)
        {
            Console.WriteLine(header);

            for (var i = 0; i < vec.Length; i++)
            {
                Console.WriteLine($"{vec[i].y}");
            }

            Console.WriteLine();
        }

        static void Inspect(string header, Dots.Dot[] vec)
        {
            Console.WriteLine(header);

            for (var i = 0; i < vec.Length; i++)
            {
                Console.WriteLine($"{vec[i].ToString()}");
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

        static void Run(string[] args)
        {
            const int INPUTS = 4; const int OUTPUTS = 4;

            var Y = new Dots.Dot[OUTPUTS]
            {
                Dots.create(),
                Dots.create(),
                Dots.create(),
                Dots.create()
            };

            Dots.path(Vector(INPUTS), null, Y);

            double Error = 0.0;

            for (int episode = 0; episode < 128 * 1024; episode++)
            {
                Error = 0.0;

                Dots.Dot[] T = Vector(INPUTS);

                Dots.compute(T, null, Y);

                double E;

                double LearningRate = 0.001;

                Dots.learn(null, Y, LearningRate, out E, T);

                Error += E;

                if (double.IsNaN(E) || double.IsInfinity(E))
                {
                    break;
                }

                E = 0.5 * (Error * Error);

                if (double.IsNaN(E) || double.IsInfinity(E))
                {
                    break;
                }

                if (E <= double.Epsilon)
                {
                    break;
                }

                Console.WriteLine($"{E}");

            }

            Console.WriteLine();

            var X = Vector(INPUTS);

            Print("X", X);

            Dots.compute(X, null, Y);

            Inspect("B", Y);

            Print("Y", Y);

        }

        static void Main(string[] args)
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
            };

            Run(args);

            Console.ReadKey();
        }
    }
}