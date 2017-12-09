namespace System {
    public interface IFunction {
        double f(double x);
        double df(double x, double y);
    }

    public class Tanh : IFunction {
        public static readonly IFunction Ω = New();

        public static IFunction New() {
            return new Tanh();
        }

        static double tanh(double exp) {
            return (exp - 1) / (exp + 1);
        }

        public static double f(double x) {
            return tanh(Math.Exp(2 * x));
        }

        public static double df(double x, double y) {
            return (1 - y * y);
        }

        double IFunction.f(double x) {
            return f(x);
        }

        double IFunction.df(double x, double y) {
            return df(x, y);
        }
    }

    public class Sigmoid : IFunction {
        public static readonly IFunction Ω = New();

        public static IFunction New() {
            return new Sigmoid();
        }

        static double sigmoid(double exp) {
            return 1 / (1 + exp);
        }

        public static double f(double x) {
            return sigmoid(Math.Exp(-x));
        }

        public static double df(double x, double y) {
            return y * (1 - y);
        }

        double IFunction.f(double x) {
            return f(x);
        }

        double IFunction.df(double x, double y) {
            return df(x, y);
        }
    }

    public class Softplus : IFunction {
        public static readonly IFunction Ω = New();

        public static IFunction New() {
            return new Softplus();
        }

        public static double f(double x) {
            return Math.Log(1 + Math.Exp(x));
        }

        public static double df(double x, double y) {
            return 1 / (1 + Math.Exp(-x));
        }

        double IFunction.f(double x) {
            return f(x);
        }

        double IFunction.df(double x, double y) {
            return df(x, y);
        }
    }

    public class Identity : IFunction {
        public static readonly IFunction Ω = New();

        public static IFunction New() {
            return new Identity();
        }

        public static double f(double x) {
            return x;
        }

        public static double df(double x, double y) {
            return 1;
        }

        double IFunction.f(double x) {
            return f(x);
        }

        double IFunction.df(double x, double y) {
            return df(x, y);
        }
    }

    public class Dot {
        public static readonly Random randomizer = new Random();

        public static double random() {
            return randomizer.NextDouble();
        }

        public static int random(int max) {
            return randomizer.Next(max);
        }

        public void randomize() {
            for (int j = 0; β != null && j < β.Length; j++) {
                β[j].β = random();
            }

            ζ = random();
        }

        public struct Coefficient {
            public double ξ;
            public double β;
            public double θ;
        }

        public Coefficient[] β;

        public double ζ;

        public IFunction Ω;

        public double ƒ;

        public double δƒ;

        public double δ;

        public static implicit operator Dot(double value) {
            return new Dot() { ƒ = value };
        }

        public static implicit operator double(Dot value) {
            return value.ƒ;
        }

        public override string ToString() {
            return ƒ.ToString();
        }

        public void connect(int X) {
            int j;

            if (β == null || β.Length != X) {
                Coefficient[] tmp = new Coefficient[X];

                j = 0;

                for (; β != null && j < β.Length; j++) {
                    tmp[j] = β[j];
                }

                for (; j < tmp.Length; j++) {
                    tmp[j].β = random();
                }

                β = tmp;
            }
        }

        public void compute(Dot[] X) {
            System.Diagnostics.Debug.Assert(X.Length == β.Length);

            double y = ζ * 1.0;

            for (int j = 0; j < β.Length; j++) {
                double ξ = X[j].ƒ;

                β[j].ξ = ξ;

                y += β[j].β * ξ;
            }

            if (Ω == null) {
                ƒ = y; δƒ = 1.0;
            } else {
                ƒ = Ω.f(y); δƒ = Ω.df(y, ƒ);
            }
        }

        public void move(double μ) {
            ζ += δ * 1.0;

            for (int j = 0; j < β.Length; j++) {
                double ϟ = δ * β[j].ξ;

                β[j].β += ϟ + μ * β[j].θ;

                β[j].θ = ϟ;
            }
        }
    }

    public static class Dots {
        public static Dot[] create(int size, IFunction F = null) {
            Dot[] L = new Dot[size];

            for (int i = 0; i < L.Length; i++) {
                L[i] = new Dot() { Ω = F };
            }

            return L;
        }

        public static void connect(int X, Dot[][] H, Dot[] Y) {
            for (int l = 0; H != null && l < H.Length; l++) {
                Dot[] h = H[l];

                for (int i = 0; i < h.Length; i++) {
                    h[i].connect(X);
                }

                X = h.Length;
            }

            for (int i = 0; Y != null && i < Y.Length; i++) {
                Y[i].connect(X);
            }
        }

        public static void compute(Dot[] X, Dot[][] H, Dot[] Y) {
            for (int l = 0; H != null && l < H.Length; l++) {
                Dot[] h = H[l];

                for (int i = 0; i < h.Length; i++) {
                    h[i].compute(X);
                }

                X = h;
            }

            for (int i = 0; Y != null && i < Y.Length; i++) {
                Y[i].compute(X);
            }
        }

        public static double error(Dot[] Y, Dot[] T) {
            System.Diagnostics.Debug.Assert(Y.Length == T.Length);

            double Ε = 0.0;

            for (int i = 0; i < Y.Length; i++) {
                double ϟ = Y[i].ƒ - T[i].ƒ;

                Ε += ϟ * ϟ;
            }

            return Ε * 0.5;
        }

        public static double sgd(Dot[][] H, Dot[] Y, Dot[] T, double learningRate, double momentum) {
            System.Diagnostics.Debug.Assert(Y.Length == T.Length);

            double Ε = 0.0;

            for (int i = 0; i < Y.Length; i++) {
                double ϟ = Y[i].ƒ - T[i].ƒ;

                Ε += ϟ * ϟ;

                Y[i].δ = - ϟ * Y[i].δƒ * learningRate;
            }

            Ε *= 0.5;

            Dot[] h = Y;

            if (H != null) {
                for (int l = H.Length - 1; l >= 0; l--) {
                    Dot[] P = h; h = H[l];

                    for (int i = 0; i < h.Length; i++) {
                        double Δ = 0.0;

                        for (int j = 0; j < P.Length; j++) {
                            Δ += P[j].δ * P[j].β[i].β;
                        }

                        h[i].δ = Δ * h[i].δƒ * learningRate;
                    }
                }
            }

            for (int i = 0; Y != null && i < Y.Length; i++) {
                Y[i].move(momentum);
            }

            if (H != null) {
                for (int l = H.Length - 1; l >= 0; l--) {
                    h = H[l];

                    for (int i = 0; i < h.Length; i++) {
                        h[i].move(momentum);
                    }
                }
            }

            return Ε;
        }
    }
}