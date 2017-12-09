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
        public static readonly Random randomizer = new Random(137);

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

            ζ.β = random();
        }

        public struct Coefficient {
            public double ξ;
            public double β;
            public double θ;
        }

        public Coefficient[] β;

        public Coefficient ζ;

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

        /// <summary>
        /// ƒ = βᵀχ + ζ
        /// </summary>
        public unsafe void compute(Dot[] X) {
            System.Diagnostics.Debug.Assert(X.Length == β.Length);

            double Σ = 0, ξ;

            ζ.ξ = ξ = 1; Σ += ζ.β * ξ;

            fixed (Coefficient* p = β) {
                for (int j = 0; j < X.Length; j++) {
                    p[j].ξ = ξ = X[j].ƒ; Σ += p[j].β * ξ;
                }
            }

            ƒ = Σ; δƒ = 1;

            if (Ω != null) {
                ƒ = Ω.f(Σ); δƒ = Ω.df(Σ, ƒ);
            }
        }

#if SAFE
        [Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void move(double α, double μ) {
            double Δ = α * δ; double ϟ;
            for (int j = 0; j < β.Length; j++) {
                ϟ = Δ * β[j].ξ;
                β[j].β += ϟ + μ * β[j].θ;
                β[j].θ = ϟ;
            }
            ϟ = Δ * ζ.ξ;
            ζ.β += ϟ + μ * ζ.θ;
            ζ.θ = ϟ;
        }
#endif

#if !SAFE
        [Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        unsafe static void move(Coefficient* c, int k, int r, double Δ, double μ) {
            double ϟ;
            while (k-- > 0) {
                ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
            }
            switch (r) {
                case 6:
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    break;
                case 5:
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    break;
                case 4:
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    break;
                case 3:
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    break;
                case 2:
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    break;
                case 1:
                    ϟ = Δ * c->ξ; c->β += ϟ + μ * c->θ; c->θ = ϟ; c++;
                    break;
                case 0:
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
        [Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public unsafe void move(double α, double μ) {
            double Δ = α * δ;

            fixed (Coefficient* p = β) {
                move(p, β.Length / 7, β.Length % 7, Δ, μ);
            }

            fixed (Coefficient* p = &ζ) {
                double ϟ = Δ * p->ξ; p->β += ϟ + μ * p->θ; p->θ = ϟ;
            }
        }
#endif
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
                Dot y = Y[i]; Dot t = T[i];

                double ϟ;

                ϟ = y.ƒ - t.ƒ; y.δ = -ϟ * y.δƒ;

                Ε += ϟ * ϟ;
            }

            Ε *= 0.5;

            Dot[] h = Y;

            if (H != null) {
                for (int l = H.Length - 1; l >= 0; l--) {
                    Dot[] U = h; h = H[l]; Dot u; double Δ;

                    for (int i = 0; i < h.Length; i++) {
                        Δ = 0.0;

                        for (int j = 0; j < U.Length; j++) {
                            u = U[j]; Δ += u.δ * u.β[i].β;
                        }

                        u = h[i]; u.δ = Δ * u.δƒ;
                    }
                }
            }

            for (int i = 0; i < Y.Length; i++) {
                Y[i].move(learningRate, momentum);
            }

            if (H != null) {
                for (int l = H.Length - 1; l >= 0; l--) {
                    h = H[l];

                    for (int i = 0; i < h.Length; i++) {
                        h[i].move(learningRate, momentum);
                    }
                }
            }

            return Ε;
        }
    }
}
