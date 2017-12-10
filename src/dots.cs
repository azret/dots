namespace System.Dots {

    /// <summary>
    /// A Dot(·) is a high level linear unit that produces a single scalar value ƒ
    /// </summary>
    public partial class Dot {
        public double ƒ;

        public static implicit operator Dot(double value) {
            return new Dot() { ƒ = value };
        }

        public static implicit operator double(Dot value) {
            return value.ƒ;
        }

        public override string ToString() {
            return ƒ.ToString();
        }
    }

    /// <summary>
    /// IΩ
    /// </summary>
    public interface IΩ {
        double f(double x);
        double df(double x, double y);
    }

    /// <summary>
    /// Tanh (ƒ(x) = (e²ˣ - 1) / (e²ˣ + 1))
    /// </summary>
    public class Tanh : IΩ {
        public static readonly IΩ Ω = New();

        public static IΩ New() {
            return new Tanh();
        }

        public override string ToString() {
            return "ƒ(x) = (e²ˣ - 1) / (e²ˣ + 1)";
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

        double IΩ.f(double x) {
            return f(x);
        }

        double IΩ.df(double x, double y) {
            return df(x, y);
        }
    }

    /// <summary>
    /// Sigmoid (ƒ(x) = 1 / (1 + e⁻ˣ))
    /// </summary>
    public class Sigmoid : IΩ {
        public static readonly IΩ Ω = New();

        public static IΩ New() {
            return new Sigmoid();
        }

        public override string ToString() {
            return "ƒ(x) = 1 / (1 + e⁻ˣ)";
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

        double IΩ.f(double x) {
            return f(x);
        }

        double IΩ.df(double x, double y) {
            return df(x, y);
        }
    }

    /// <summary>
    /// Softplus (ƒ(x) = log(1 + eˣ))
    /// </summary>
    public class Softplus : IΩ {
        public static readonly IΩ Ω = New();

        public static IΩ New() {
            return new Softplus();
        }

        public override string ToString() {
            return "ƒ(x) = log(1 + eˣ)";
        }

        public static double f(double x) {
            return Math.Log(1 + Math.Exp(x));
        }

        public static double df(double x, double y) {
            return 1 / (1 + Math.Exp(-x));
        }

        double IΩ.f(double x) {
            return f(x);
        }

        double IΩ.df(double x, double y) {
            return df(x, y);
        }
    }

    /// <summary>
    /// ReLU (ƒ(x) = max(0, x))
    /// </summary>
    public class ReLU : IΩ {
        public static readonly IΩ Ω = New();

        public static IΩ New() {
            return new ReLU();
        }

        public override string ToString() {
            return "ƒ(x) = max(0, x)";
        }

        public static double f(double x) {
            return Math.Max(0, x);
        }

        public static double df(double x, double y) {
            return Math.Max(0, Math.Sign(x));
        }

        double IΩ.f(double x) {
            return f(x);
        }

        double IΩ.df(double x, double y) {
            return df(x, y);
        }
    }

    /// <summary>
    /// Identity (ƒ(x) = x)
    /// </summary>
    public class Identity : IΩ {
        public static readonly IΩ Ω = New();

        public static IΩ New() {
            return new Identity();
        }

        public override string ToString() {
            return "ƒ(x) = x";
        }

        public static double f(double x) {
            return x;
        }

        public static double df(double x, double y) {
            return 1;
        }

        double IΩ.f(double x) {
            return f(x);
        }

        double IΩ.df(double x, double y) {
            return df(x, y);
        }
    }

    /// <summary>
    /// y = ƒ(χ) = Ω(βᵀχ + ζ)
    /// </summary>
    public partial class Dot {
        public static readonly Random randomizer = new Random(137);

        public static double random() {
            return randomizer.NextDouble();
        }

        public static int random(int max) {
            return randomizer.Next(max);
        }

        public double δƒ;

        public struct Coefficient {
            public double ξ;
            public double β;
            public double θ;

            public override string ToString() {
                return θ.ToString();
            }
        }

        public Coefficient ζ;

        public Coefficient[] β;

        public int size() {
            if (β != null) {
                return β.Length;
            }
            return 0;
        }

        public void size(int X) {
            if (β == null || β.Length != X) {
                Coefficient[] tmp = new Coefficient[X];

                for (int j = 0; j < tmp.Length; j++) {
                    if (β != null && j < β.Length) {
                        tmp[j] = β[j];
                    }
                }

                β = tmp;
            }
        }

        public void randomize() {
            for (int j = 0; β != null && j < β.Length; j++) {
                β[j].β = random();
            }
            ζ.β = random();
        }

        public IΩ Ω;

        public unsafe void compute(Dot[] X) {
            const double BIAS = 1.0;

            System.Diagnostics.Debug.Assert(X.Length == β.Length);

            double Σ = 0, ξ;

            ζ.ξ = ξ = BIAS;
            Σ += ζ.β * ξ;

            fixed (Coefficient* p = β) {
                for (int j = 0; j < X.Length; j++) {
                    p[j].ξ = ξ = X[j].ƒ;
                    Σ += p[j].β * ξ;
                }
            }

            ƒ = Σ; δƒ = 1;

            if (Ω != null) {
                ƒ = Ω.f(Σ);
                δƒ = Ω.df(Σ, ƒ);
            }
        }

        public double δ;

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
                double ϟ = Δ * p->ξ;
                p->β += ϟ + μ * p->θ;
                p->θ = ϟ;
            }
        }
#endif
    }

    /// <summary>
    /// Dots
    /// </summary>
    public static class Dots {
        public static Dot[] create(int count, IΩ F = null) {
            Dot[] h = new Dot[count];

            for (int i = 0; i < h.Length; i++) {
                h[i] = new Dot() { Ω = F };
            }

            return h;
        }

        public static void randomize(Dot[][] H, params Dot[] Y) {
            for (int l = 0; H != null && l < H.Length; l++) {
                Dot[] h = H[l];

                for (int i = 0; i < h.Length; i++) {
                    h[i].randomize();
                }
            }

            for (int i = 0; Y != null && i < Y.Length; i++) {
                Y[i].randomize();
            }
        }

        public static void connect(int X, Dot[][] H, Dot[] Y) {
            for (int l = 0; H != null && l < H.Length; l++) {
                Dot[] h = H[l];

                for (int i = 0; i < h.Length; i++) {
                    h[i].size(X);
                }

                X = h.Length;
            }

            for (int i = 0; Y != null && i < Y.Length; i++) {
                Y[i].size(X);
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

                double ϟ = y.ƒ - t.ƒ;

                y.δ = -ϟ * y.δƒ;
                Ε += ϟ * ϟ;
            }

            Ε *= 0.5;

            if (H != null) {
                Dot[] h = Y; Dot u; Dot c;

                for (int l = H.Length - 1; l >= 0; l--) {
                    Dot[] U = h; h = H[l];

                    for (int i = 0; i < h.Length; i++) {
                        double Δ = 0.0;

                        for (int j = 0; j < U.Length; j++) {
                            u = U[j];
                            Δ += u.δ * u.β[i].β;
                        }

                        c = h[i];
                        c.δ = Δ * c.δƒ;
                    }
                }
            }

            for (int i = 0; i < Y.Length; i++) {
                Y[i].move(learningRate, momentum);
            }

            if (H != null) {
                for (int l = H.Length - 1; l >= 0; l--) {
                    Dot[] h = H[l];

                    for (int i = 0; i < h.Length; i++) {
                        h[i].move(learningRate, momentum);
                    }
                }
            }

            return Ε;
        }
    }
}