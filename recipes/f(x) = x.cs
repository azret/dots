using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

public static class App {
    static void Inspect(string header, Dot[] A, ConsoleColor color, bool verbose) {
        Console.WriteLine($"\r\n{header}:\r\n");
        for (var i = 0; i < A.Length; i++) {
            Console.ForegroundColor = color;
            Console.Write($"{Math.Round(A[i].ƒ, 2)}");
            if (A[i].β != null && verbose) {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                int c = 0;
                for (int j = 0; j < A[i].β.Length; j++) {
                    if (j > 0) {
                        Console.Write($", ");
                    } else {
                        Console.Write($" [");
                    }
                    c++;
                    Console.Write($"{A[i].β[j].β}");
                }
                if (c > 0) {
                    Console.Write($"]");
                }
            }
            Console.ResetColor();
            if (((i + 1) % 7) == 0) {
                Console.WriteLine();
            } else {
                Console.Write($", ");
            }
        }
        Console.WriteLine();
    }

    static Dot[] Vector(int size) {
        Dot[] A = new Dot[size];

        for (var i = 0; i < A.Length; i++) {
            A[i] = Dot.random(1000) / (1000 * 1.0);
        }

        return A;
    }

    static void Test(Dot[] X, Dot[][] H, Dot[] Y, bool verbose) {
        Console.WriteLine("\r\n");

        Dots.compute(X, H, Y);

        if (H != null && verbose) {
            for (int l = 0; l < H.Length; l++) {
                Inspect($"H{l}", H[l], ConsoleColor.White, verbose);
            }
        }

        if (Y != null) {
            Inspect("Y", Y, ConsoleColor.Yellow, verbose);
        }

        Inspect("X", X, ConsoleColor.Green, verbose);
    }

    static void Train(int episodes, Func<double> α, Func<double> μ, Dot[] Y,
        Dot[][] H, Func<Dot[]> X, Func<Dot[], Dot[]> T,
        Func<int, Dot[], double, double> epoch) {

        object spin = new object();

        for (var i = 0; i < episodes; i++) {
            var x = X();

            var t = T(x);

            double e;

            Dots.compute(x, H, Y);

            e = Dots.sgd(H, Y, t, α(), μ());

            if (epoch != null) {
                e = epoch(i, x, e);
            }

            System.Threading.Thread.Sleep(0);

            if (e <= double.Epsilon || double.IsNaN(e) || double.IsInfinity(e)) {
                return;
            }
        }
    }

    static void Main(string[] args) {

        int SIZE = 17;

        Dot[][] H = new Dot[][]
        {
            Dots.create(SIZE, Sigmoid.Ω),
        };

        Dot[] Y = Dots.create(SIZE, Identity.Ω);

        Dots.connect(SIZE, H, Y);

        bool canceled = false;

        Console.CancelKeyPress += (sender, e) => {

            if (canceled) {
                Process.GetCurrentProcess().Kill();
            }

            e.Cancel = canceled = true;
        };

        Test(Vector(SIZE), H, Y, verbose: false);

        double E = 0.0; double S = 0.0; double A = 0.0; double D = 0.0; double R = 0.0;

        Train(

            // 8 * 16 * 32 * 64 * 128,

            32 * 64 * 128,

            // Learning rate

            () => 0.01,

            // Momentum

            () => 0.3,

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
                Dot[] T = new Dot[X.Length];

                for (int i = 0; i < T.Length; i++) {
                    T[i] = X[i].ƒ;
                }

                return T;
            },

            (episode, X, error) => {

                E += error * error * (episode + 1);

                S += E * E * (episode + 1);

                A += S * S * (episode + 1);

                D += A * A * (episode + 1);

                R += D * D * (episode + 1);

                // Error Wheel, Gears

                if (episode % 256 == 0) {
                    Console.WriteLine($"[{episode:n0}] - {1 / R} | {1 / D} | {1 / A} | {1 / S} | {1 / E}");
                }

                if (canceled) {
                    error = double.NaN;
                }

                return error;
            }

        );

        Test(Vector(SIZE), H, Y, verbose: false);
        Test(Vector(SIZE), H, Y, verbose: false);

        Console.ReadKey();
    }
}