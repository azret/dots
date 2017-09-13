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
                // Console.WriteLine($" = {vec[i].inspect()}");
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        static Dots.Dot[] Vector()
        {
            Dots.Dot[] vec = new Dots.Dot[7 + Dots.Dot.random(11)];

            for (var i = 0; i < vec.Length; i++)
            {
                var scalar = Dots.Dot.random(1000) / (1000 * 1.0);
                
                vec[i] = new Dots.Dot()
                {
                    y = scalar
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
              //   Inspect($"H{l}", H[l], ConsoleColor.DarkGray);
            }

            if (Y != null)
            {
                Inspect("Y", Y, ConsoleColor.Green);
            }

            Console.WriteLine("\r\nE:\r\n");

            Console.WriteLine($"{Dots.error(Y, X)}");

        }

        static void Train(Func<double> α, ref Dots.Dot[] Y, 
            Dots.Dot[][] H, Func<Dots.Dot[]> X, Func<Dots.Dot[], Dots.Dot[]> T, int max, int episodes,
            Func<int, Dots.Dot[], double, int> epoch)
        {            
            for (int episode = 0; episode < episodes; episode++)
            {
                var x = X();

                if (max < 0)
                {
                    Dots.grow(ref Y, x.Length);
                }
                else
                {
                    Dots.grow(ref Y, Math.Min(max, x.Length));
                }

                Dots.connect(Y, H, x);

                Dots.compute(Y, H, x);

                var t = T(x);

                double E = Dots.sgd(Y, H, t, α());

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
            bool canceled = false; int MAX = 32;

            Dots.Dot[][] H = new Dots.Dot[][]
            {
            };

            Dots.Dot[] Y = null;

            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = canceled = true;
            };

            Test(Y, H);

            double E = 0.0; double S = 0.0; double A = 0.0; double D = 0.0; double R = 0.0;

            Train(
                
                // Learning rate

                () => 0.0001,

                // Output vector

                ref Y,

                // Hidden layers

                H,
                
                // Random input vector

                ()=> 
                {
                    return Vector();
                },

                (X) =>
                {
                    return X;
                },

                MAX,

                32 * 32 * 1024,

                (episode, X, error)=>
                {
                    E += error * error * (episode + 1);

                    S += E * E * (episode + 1);

                    A += S * S * (episode + 1);

                    D += A * A * (episode + 1);

                    R += D * D * (episode + 1);

                    if (double.IsNaN(D) || double.IsInfinity(D))
                    {
                        return int.MaxValue - 1;
                    }

                    // Error Wheel, Gears

                    Console.WriteLine($"{1 / R} | {1 / D} | {1 / A} | {1 / S} | {1 / E}");

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