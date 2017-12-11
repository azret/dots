﻿namespace System.Dots {

    /// <summary>
    /// A Dot(·) is a high level linear unit that produces a single scalar value ƒ
    /// </summary>
    public partial class Dot {
        public double ƒ, δƒ;

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
    /// IΩ (Squashing Function)
    /// </summary>
    public interface IΩ {
        double f(double x);
        double df(double x, double y);
    }

    /// <summary>
    /// y = ƒ(χ) = Ω(βᵀχ + ζ)
    /// </summary>
    public partial class Dot {
        public struct Coefficient {
            public double ξ;
            public double β;
            public double δ;

            public override string ToString() {
                return δ.ToString();
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

        public void size(int ξ) {
            if (β == null || β.Length != ξ) {
                Coefficient[] tmp = new Coefficient[ξ];

                for (int j = 0; j < tmp.Length; j++) {
                    if (β != null && j < β.Length) {
                        tmp[j] = β[j];
                    }
                }

                β = tmp;
            }
        }

        public IΩ Ω;

        public unsafe void compute(Dot[] ξ) {
            const double BIAS = 1;

            System.Diagnostics.Debug.Assert(ξ.Length == β.Length);

            double y = 0, x;

            ζ.ξ = x = BIAS; y += ζ.β * x;

            fixed (Coefficient* c = β) {
                for (int j = 0; j < ξ.Length; j++) {
                    c[j].ξ = x = ξ[j].ƒ;
                    y += c[j].β * x;
                }
            }

            if (Ω == null) {
                ƒ = y; δƒ = 1;
            } else {
                ƒ = Ω.f(y); δƒ = Ω.df(y, ƒ);
            }
        }

        public double δ;

#if !SAFE
        [Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public unsafe void move(double learningRate, double momentum) {
            double ᵷ; double Δ = learningRate * δ * δƒ;
            ᵷ = Δ * ζ.ξ; ζ.β += (1 - 0) * ᵷ + (momentum) * ζ.δ; ζ.δ = ᵷ;
            fixed (Coefficient* p = β) {
                Coefficient* c = p;
                int k = β.Length / 7; int r = β.Length % 7; 
                while (k-- > 0) {
                    ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                    ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                    ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                    ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                    ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                    ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                    ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                }
                switch (r) {
                    case 6:
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        break;
                    case 5:
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        break;
                    case 4:
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        break;
                    case 3:
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        break;
                    case 2:
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        break;
                    case 1:
                        ᵷ = Δ * c->ξ; c->β += (1 - 0) * ᵷ + (momentum) * c->δ; c->δ = ᵷ; c++;
                        break;
                    case 0:
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }
#endif
    }

    public static partial class Dots {
        public static readonly Random randomizer = new Random(137);

        public static double random() {
            return randomizer.NextDouble();
        }

        public static void print(Dot[] L, IO.TextWriter W, string separator = ", ") {
            for (int i = 0; L != null && i < L.Length; i++) {
                if (i > 0) {
                    W.Write(separator);
                }
                W.Write(L[i].ToString());
            }
        }

        public static void print(Dot[] L, string format, IO.TextWriter W, string separator = ", ") {
            for (int i = 0; L != null && i < L.Length; i++) {
                if (i > 0) {
                    W.Write(separator);
                }
                W.Write(L[i].ƒ.ToString(format));
            }
        }

        public static Dot[] create(params double[] args) {
            Dot[] L = new Dot[args.Length];
            for (var i = 0; i < L.Length; i++) {
                L[i] = args[i];
            }
            return L;
        }

        public static Dot[] create<Ω>(int count) where Ω : IΩ, new() {
            return create(count, new Ω());
        }

        public static Dot[] create(int count) {
            return create(count, Identity.Ω);
        }

        public static Dot[] create(int count, IΩ F) {
            Dot[] L = new Dot[count];
            for (int i = 0; i < L.Length; i++) {
                L[i] = new Dot() { Ω = F };
            }
            return L;
        }

        public static Dot[] random(int count, double median = 1.0, double scale = 1.0) {
            Dot[] L = new Dot[count];
            for (var i = 0; i < L.Length; i++) {
                L[i] = scale * Math.Round(random(), 2) * (median - Math.Round(random(), 2));
            }
            return L;
        }
    }

    public static partial class Dots {
        public static void connect(this Dot[][] ℳ, int ξ, bool randomize = true) {
            for (int ℓ = 0; ℳ != null && ℓ < ℳ.Length; ℓ++) {
                Dot[] L = ℳ[ℓ];
                for (int i = 0; i < L.Length; i++) {
                    L[i].size(ξ);
                    if (randomize) {
                        for (int j = 0; j < L[i].β.Length; j++) {
                            L[i].β[j].β = random();
                        }
                        L[i].ζ.β = random();
                    }
                }
                ξ = L.Length;
            }
        }

        public static void randomize(this Dot[][] ℳ) {
            for (int ℓ = 0; ℳ != null && ℓ < ℳ.Length; ℓ++) {
                Dot[] ꝟ = ℳ[ℓ];
                for (int i = 0; i < ꝟ.Length; i++) {
                    for (int j = 0; j < ꝟ[i].β.Length; j++) {
                        ꝟ[i].β[j].β = random();
                    }
                    ꝟ[i].ζ.β = random();
                }
            }
        }

        public static double error(this Dot[][] ℳ, Dot[] Ꝙ) {
            Dot[] γ = null;
            for (int ℓ = ℳ.Length - 1; ℓ >= 0; ℓ--) {
                if (γ == null) {
                    γ = ℳ[ℓ];
                    System.Diagnostics.Debug.Assert(γ.Length == Ꝙ.Length);
                    double Σ = 0.0;
                    for (int i = 0; i < γ.Length; i++) {
                        Σ += Math.Pow(γ[i].ƒ - Ꝙ[i].ƒ, 2);
                    }
                    return Σ / 2;
                }
            }
            return double.NaN;
        }

        public static Dot[] compute(this Dot[][] ℳ, Dot[] ξ) {
            for (int ℓ = 0; ℓ < ℳ.Length; ℓ++) {
                Dot[] ꝟ = ℳ[ℓ];
                for (int i = 0; i < ꝟ.Length; i++) {
                    ꝟ[i].compute(ξ);
                }
                ξ = ꝟ;
            }
            return ξ;
        }

        public static void sgd(this Dot[][] ℳ, Dot[] Ꝙ, double learningRate, double momentum) {
            Dot[] γ = null;
            for (int ℓ = ℳ.Length - 1; ℓ >= 0; ℓ--) {
                if (γ == null) {
                    γ = ℳ[ℓ];
                    System.Diagnostics.Debug.Assert(γ.Length == Ꝙ.Length);
                    for (int i = 0; i < γ.Length; i++) {
                        γ[i].δ = -(γ[i].ƒ - Ꝙ[i].ƒ);
                    }
                } else {
                    Dot[] ꝟ = ℳ[ℓ];
                    for (int i = 0; i < ꝟ.Length; i++) {
                        double Σ = 0.0;
                        for (int j = 0; j < γ.Length; j++) {
                            Σ += γ[j].δ * γ[j].β[i].β;
                        }
                        ꝟ[i].δ = Σ;
                    }
                    γ = ꝟ;
                }
            }
            for (int ℓ = ℳ.Length - 1; ℓ >= 0; ℓ--) {
                Dot[] ꝟ = ℳ[ℓ];
                for (int i = 0; i < ꝟ.Length; i++) {
                    ꝟ[i].move(learningRate, momentum);
                }
            }
        }
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
}