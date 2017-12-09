using System;
using System.Threading.Tasks;

public static class App {
    static void Inspect(string header, Dot[] A, ConsoleColor color) {
        Console.WriteLine($"{header}:\r\n");
        for (var i = 0; i < A.Length; i++) {
            Console.ForegroundColor = color;
            Console.Write($"{A[i].ƒ}");
            if (A[i].β != null) {
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
            Console.WriteLine();
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

    static void Test(Dot[] Y, Dot[][] H, int size) {
        Console.WriteLine("\r\n");

        Dot[] X;

        X = Vector(size);

        Dots.compute(X, H, Y);

        if (H != null) {
            for (int l = 0; l < H.Length; l++) {
                Inspect($"H{l}", H[l], ConsoleColor.White);
            }
        }

        if (Y != null) {
            Inspect("Y", Y, ConsoleColor.Yellow);
        }

        Inspect("X", X, ConsoleColor.Green);

        Console.WriteLine("\r\nE:\r\n");

        Console.WriteLine($"{Dots.error(Y, X)}");

    }

    static void Train(int episodes, Func<double> α, Func<double> μ, Dot[] Y,
        Dot[][] H, Func<Dot[]> X, Func<Dot[], Dot[]> T,
        Func<int, Dot[], double, double> epoch) {

        for (int i = 0; i < episodes; i++) {
            var x = X();

            Dots.compute(x, H, Y);

            var t = T(x);

            var E = Dots.sgd(H, Y, t, α(), μ());

            if (epoch != null) {
                E = epoch(i, x, E);
            }

            if (E <= double.Epsilon || double.IsNaN(E) || double.IsInfinity(E)) {
                return;
            }
        }
    }

    static void Main(string[] args) {
        bool canceled = false;

        Console.CancelKeyPress += (sender, e) => {
            e.Cancel = canceled = true;
        };

        int SIZE = 7;

        Dot[][] H = new Dot[][]
        {
           Dots.create(SIZE, Softplus.Ω),
        };

        Dot[] Y = Dots.create(SIZE, Identity.Ω);

        Dots.connect(SIZE, H, Y);
        
        Test(Y, H, SIZE);

        double E = 0.0; double S = 0.0; double A = 0.0; double D = 0.0; double R = 0.0;

        Train(

            8 * 16 * 32 * 64 * 128,

            // Learning rate

            () => 0.01,

            // Momentum

            () => 0.01,

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

                if (episode % 37 == 0) {
                    Console.WriteLine($"{1 / R} | {1 / D} | {1 / A} | {1 / S} | {1 / E}");
                }

                if (canceled) {
                    error = double.NaN;
                }

                return error;
            }

        );

        Test(Y, H, SIZE);

        Console.ReadKey();
    }
}