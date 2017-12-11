using System;
using System.Dots;
using System.Diagnostics;

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

    static void Test(Dot[] X, Dot[][] ℳ,bool verbose) {
        Dots.compute(ℳ, X);

        for (int l = 0; l < ℳ.Length; l++) {
            Inspect($"H{l}", ℳ[l], ConsoleColor.White, verbose);
        }

        Inspect("X", X, ConsoleColor.Green, verbose);
        Console.WriteLine();
    }

    static void Train(int episodes, Func<double> α, Func<double> μ,
        Dot[][] ℳ, Func<Dot[]> X, Func<Dot[], Dot[]> T,
        Func<int, Dot[], double, double> epoch) {

        double E = 0;

        for (var i = 0; i < episodes; i++) {
            var x = X();

            var t = T(x);

            double e;

            Dots.compute(ℳ, x);

            Dots.sgd(ℳ, t, α(), μ());

            e = Dots.error(ℳ, t);

            if (E == e) {
                return;
            }

            E = e;

            if (epoch != null) {
                e = epoch(i, x, e);
            }

            if (e <= double.Epsilon || double.IsNaN(e) || double.IsInfinity(e)) {
                return;
            }
        }
    }

    static void Main(string[] args) {
        int INPUTS = 7;

        Dot[][] ℳ = new Dot[][]
        {
            Dots.create<Sigmoid>(INPUTS),
            Dots.create(INPUTS),
        };

        Dots.connect(ℳ, INPUTS);

        Dots.randomize(ℳ);

        bool canceled = false;

        Console.CancelKeyPress += (sender, e) => {

            if (canceled) {
                Process.GetCurrentProcess().Kill();
            }

            e.Cancel = canceled = true;
        };

        Test(Dots.random(INPUTS), ℳ, verbose: false);

        double E0 = 0.0; double E1 = 0.0; double E2 = 0.0; double E3 = 0.0; double E4 = 0.0;

        Train(

            // Number of episodes

            4 * 8 * 32 * 64 * 128,

            // Learning rate

            () => 1.0 / (INPUTS * ℳ.Length),

            // Momentum

            () => 1 - (1.0 / INPUTS * ℳ.Length),

            // Hidden layers

            ℳ,

            // Input vector

            () => {
                return Dots.random(INPUTS);
            },

            // Target vector

            (X) => {
                Dot[] T = new Dot[X.Length];

                for (int i = 0; i < T.Length; i++) {
                    T[i] = X[i].ƒ;
                }

                return T;
            },

            // Error Wheel, Gears

            (i, X, E) => {

                E0 += E * E * (i + 1);

                E1 += E0 * E0 * (i + 1);

                E2 += E1 * E1 * (i + 1);

                E3 += E2 * E2 * (i + 1);

                E4 += E3 * E3 * (i + 1);

                if (i % 256 == 0) {
                    Console.WriteLine($"[{i:n0}] - {1 / E4} | {1 / E3} | {1 / E2} | {1 / E1} | {1 / E0}");
                }

                if (canceled) {
                    E = double.NaN;
                }

                return E;
            }

        );

        Test(Dots.random(INPUTS), ℳ, verbose: false);

        Console.ReadKey();
    }
}