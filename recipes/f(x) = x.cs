using System;
using System.Threading.Tasks;

namespace Recipes {
    public static class Identity {
        static void Inspect(string header, Dots.Dot[] vec, ConsoleColor color) {
            Console.WriteLine($"{header}:\r\n");

            for (var i = 0; i < vec.Length; i++) {
                Console.ForegroundColor = color;
                Console.Write($"{vec[i].f}");
                Console.ResetColor();
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        static Dots.Dot[] Vector(int size) {
            Dots.Dot[] vec = new Dots.Dot[size];

            for (var i = 0; i < vec.Length; i++) {
                vec[i] = Dots.Dot.random(1000) / (1000 * 1.0);
            }

            return vec;
        }

        static void Test(Dots.Dot[] Y, Dots.Dot[][] H, int size) {
            Console.WriteLine("\r\n**************************\r\n");

            Dots.Dot[] X;

            X = Vector(size); 

            Dots.compute(Y, H, X);

            Console.WriteLine("\r\n=========================\r\n");

            if (H != null) {
                for (int l = 0; l < H.Length; l++) {
                    Inspect($"H{l}", H[l], ConsoleColor.DarkGray);
                }
            }

            if (Y != null) {
                Inspect("Y", Y, ConsoleColor.Green);
            }

            Inspect("X", X, ConsoleColor.White);

            Console.WriteLine("\r\nE:\r\n");

            Console.WriteLine($"{Dots.error(Y, X)}");

        }

        static void Train(Func<double> α, Func<double> μ, Dots.Dot[] Y,
            Dots.Dot[][] H, Func<Dots.Dot[]> X, Func<Dots.Dot[], Dots.Dot[]> T, int max, int episodes,
            Func<int, Dots.Dot[], double, int> epoch) {
            Parallel.For(0, episodes, new ParallelOptions() { MaxDegreeOfParallelism = 1 }, (episode, state) => {

                var x = X();                 

                Dots.compute(Y, H, x);

                var t = T(x);

                Dots.sgd(Y, H, t, α(), μ());

                var E = Dots.error(Y, t);

                if (E <= double.Epsilon || double.IsNaN(E) || double.IsInfinity(E)) {
                    state.Stop();
                }

                if (epoch != null) {
                    episode = epoch(episode, x, E);
                }

                if (episode == int.MaxValue) {
                    state.Stop();
                }

            });

        }

        static void Main(string[] args) {
            bool canceled = false;

            Console.CancelKeyPress += (sender, e) => {
                e.Cancel = canceled = true;
            };

            int SIZE = 7;

            Dots.Dot[][] H = new Dots.Dot[][]
            {
                 Dots.create(SIZE, Dots.Sigmoid.Default),
                 Dots.create(SIZE, Dots.Sigmoid.Default),
                 Dots.create(SIZE, Dots.Sigmoid.Default),
            };

            Dots.Dot[] Y = Dots.create(SIZE, Dots.Identity.Default);

            Dots.connect(Y, H, SIZE);

            Test(Y, H, SIZE);

            double E = 0.0; double S = 0.0; double A = 0.0; double D = 0.0; double R = 0.0;

            Train(

                // Learning rate

                () => 1 / Math.PI,

                // Momentum

                () => 1 / Math.E,

                // Output vector

                Y,

                // Hidden layers

                H,

                // Input vector

                () => {
                    return Vector(SIZE);
                },

                // Target vector

                (X) => {
                    Dots.Dot[] Yreverse = new Dots.Dot[X.Length];

                    for (int i = 0; i < Yreverse.Length; i++) {
                        // Yreverse[Yreverse.Length - 1 - i] = X[i];
                        Yreverse[i] = X[i];
                    }

                    return Yreverse;
                },

                SIZE,

                32 * 64 * 128 * 256,

                (episode, X, error) => {

                    E += error * error * (episode + 1);

                    S += E * E * (episode + 1);

                    A += S * S * (episode + 1);

                    D += A * A * (episode + 1);

                    R += D * D * (episode + 1);
                     
                    // Error Wheel, Gears

                    if (episode % 37 == 0) {
                        Console.WriteLine($"{1 / R} | {1 / D} | {1 / A} | {1 / S} | {1 / E}");
                    }

                    if (canceled) {
                        episode = int.MaxValue;
                    }

                    return episode;
                }

            );

            Test(Y, H, SIZE);

            Console.ReadKey();
        }
    }
}