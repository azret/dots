namespace System.Dots {

    /// <summary>
    /// A Dot(·) is a high level linear unit that produces a single scalar value ƒ
    /// </summary>
    public partial class Dot {
        public Dot() {
        }

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
        double f(double a);
        double df(double a);
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
                return β.ToString();
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
                ƒ = Ω.f(y); δƒ = Ω.df(ƒ);
            }
        }

        public double δ;

#if !SAFE
        [Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public unsafe void move(double learningRate, double momentum) {
            double δ; double Δ = learningRate * this.δ;
            δ = Δ * this.ζ.ξ; this.ζ.β += (1 - 0) * δ + (momentum) * ζ.δ; ζ.δ = δ;
            fixed (Coefficient* p = this.β) {
                Coefficient* c = p;
                int k = this.β.Length / 7; int r = this.β.Length % 7; 
                while (k-- > 0) {
                    δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                    δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                    δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                    δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                    δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                    δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                    δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                }
                switch (r) {
                    case 6:
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        break;
                    case 5:
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        break;
                    case 4:
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        break;
                    case 3:
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        break;
                    case 2:
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
                        break;
                    case 1:
                        δ = Δ * c->ξ; c->β += (1 - 0) * δ + (momentum) * c->δ; c->δ = δ; c++;
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
        public static readonly Random randomizer = new Random();

        public static double random() {
            return randomizer.NextDouble();
        }

        public static int random(int min, int max) {
            return randomizer.Next(min, max);
        }

        public static Dot[] create(params double[] args) {
            Dot[] ℓ = new Dot[args.Length];
            for (var i = 0; i < ℓ.Length; i++) {
                ℓ[i] = args[i];
            }
            return ℓ;
        }

        public static Dot[] create<Ω>(int len) where Ω : IΩ, new() {
            return create(len, new Ω());
        }

        public static Dot[] create(int len) {
            return create(len, Identity.Ω);
        }

        public static Dot[] create(int len, IΩ Ω) {
            Dot[] ℓ = new Dot[len];
            for (int i = 0; i < ℓ.Length; i++) {
                ℓ[i] = new Dot() { Ω = Ω };
            }
            return ℓ;
        }

        public static Dot[] create(int len, double median = 1.0, double scale = 1.0) {
            Dot[] ℓ = new Dot[len];
            for (var i = 0; i < ℓ.Length; i++) {
                ℓ[i] = scale * Math.Round(random(), 2) * (median - Math.Round(random(), 2));
            }
            return ℓ;
        }

        public static Dot[] encode(int len, string src) {
            Dot[] ℓ = new Dot[len];
            for (var i = 0; i < ℓ.Length; i++) {
                ℓ[i] = 0 / (1307 * 0.5);
                if (i < src.Length) {
                    ℓ[i].ƒ = Math.Round(src[i] / (1307 * 0.5), 4);
                }
            }
            return ℓ;
        }

        public static string decode(this Dot[] src) {
            char[] s = new char[src.Length];
            for (var i = 0; i < src.Length; i++) {
                s[i] = (char)Math.Round(src[i] * (1307 * 0.5));
            }
            return new string(s);
        }

        static Dot.Coefficient[] copy(this Dot.Coefficient[] src) {
            Dot.Coefficient[] ɸ = new Dot.Coefficient[src.Length];
            for (int i = 0; i < ɸ.Length; i++) {
                ɸ[i] = src[i];
            }
            return ɸ;
        }

        static Dot copy(this Dot src) {
            return new Dot() {
                Ω = src.Ω,
                ƒ = src.ƒ, δƒ = src.δƒ,
                β = copy(src.β), ζ = src.ζ,
                δ = src.δ,
            };
        }

        public static Dot[] copy(this Dot[] src) {
            Dot[] ɸ = new Dot[src.Length];
            for (int i = 0; i < ɸ.Length; i++) {
                ɸ[i] = src[i].copy();
            }
            return ɸ;
        }

        public static Dot[][] copy(this Dot[][] src) {
            Dot[][] ɸ = new Dot[src.Length][];
            for (int h = 0; h < ɸ.Length; h++) {
                ɸ[h] = src[h].copy();
            }
            return ɸ;
        }

        static void add(this Dot dst, Dot ɸ) {
            Diagnostics.Debug.Assert(dst.β.Length == ɸ.β.Length);
            for (int j = 0; j < dst.β.Length; j++) {
                dst.β[j].β += ɸ.β[j].β;
            }
            dst.ζ.β += ɸ.ζ.β;
        }

        public static void add(this Dot[] dst, Dot[] ɸ) {
            Diagnostics.Debug.Assert(dst.Length == ɸ.Length);
            for (int i = 0; i < dst.Length; i++) {
                dst[i].add(ɸ[i]);
            }
        }

        public static void add(this Dot[][] dst, Dot[][] ɸ) {
            Diagnostics.Debug.Assert(dst.Length == ɸ.Length);
            for (int h = 0; h < dst.Length; h++) {
                dst[h].add(ɸ[h]);
            }
        }

        public static void multiply(this Dot[][] dst, double scalar) {
            for (int h = 0; h < dst.Length; h++) {
                Dot[] ℓ = dst[h];
                for (int i = 0; i < ℓ.Length; i++) {
                    for (int j = 0; j < ℓ[i].β.Length; j++) {
                        ℓ[i].β[j].β *= scalar;
                    }
                    ℓ[i].ζ.β *= scalar;
                }
            }
        }

        public static void randomize(this Dot[][] dst) {
            for (int h = 0; h < dst.Length; h++) {
                Dot[] ℓ = dst[h];
                for (int i = 0; i < ℓ.Length; i++) {
                    for (int j = 0; j < ℓ[i].β.Length; j++) {
                        ℓ[i].β[j].β = random();
                    }
                    ℓ[i].ζ.β = random();
                }
            }
        }
    }

    public static partial class Dots {
        public static void connect(this Dot[][] ℳ, int ξ, bool randomize = true) {
            for (int h = 0; ℳ != null && h < ℳ.Length; h++) {
                Dot[] ℓ = ℳ[h];
                for (int i = 0; i < ℓ.Length; i++) {
                    ℓ[i].size(ξ);
                    if (randomize) {
                        for (int j = 0; j < ℓ[i].β.Length; j++) {
                            ℓ[i].β[j].β = random();
                        }
                        ℓ[i].ζ.β = random();
                    }
                }
                ξ = ℓ.Length;
            }
        }

        public static double error(this Dot[][] ℳ, Dot[] Ꝙ) {
            Dot[] γ = null;
            for (int h = ℳ.Length - 1; h >= 0; h--) {
                if (γ == null) {
                    γ = ℳ[h];
                    System.Diagnostics.Debug.Assert(γ.Length == Ꝙ.Length);
                    double ε = 0.0;
                    for (int i = 0; i < γ.Length; i++) {
                        ε += Math.Pow(γ[i].ƒ - Ꝙ[i].ƒ, 2);
                    }
                    return ε / 2;
                }
            }
            return double.NaN;
        }

        public static Dot[] compute(this Dot[][] ℳ, Dot[] ξ) {
            for (int h = 0; h < ℳ.Length; h++) {
                Dot[] ℓ = ℳ[h];
                for (int i = 0; i < ℓ.Length; i++) {
                    ℓ[i].compute(ξ);
                }
                ξ = ℓ;
            }
            return ξ;
        }

        public static double sgd(this Dot[][] ℳ, Dot[] Ꝙ, double rate, double momentum) {
            double ε = 0, δ = 0;
            Dot[] γ = null;
            for (int h = ℳ.Length - 1; h >= 0; h--) {
                if (γ == null) {
                    γ = ℳ[h];
                    System.Diagnostics.Debug.Assert(γ.Length == Ꝙ.Length);
                    for (int i = 0; i < γ.Length; i++) {
                        γ[i].δ = δ = -(γ[i].ƒ - Ꝙ[i].ƒ) * γ[i].δƒ;
                        ε += Math.Pow(δ, 2) / 2;
                    }
                } else {
                    Dot[] ℓ = ℳ[h];
                    for (int i = 0; i < ℓ.Length; i++) {
                        δ = 0;
                        for (int j = 0; j < γ.Length; j++) {
                            δ += γ[j].δ * γ[j].β[i].β;
                        }
                        ℓ[i].δ = δ * ℓ[i].δƒ;
                    }
                    γ = ℓ;
                }
            }
            for (int h = ℳ.Length - 1; h >= 0; h--) {
                Dot[] ℓ = ℳ[h];
                for (int i = 0; i < ℓ.Length; i++) {
                    ℓ[i].move(rate, momentum);
                }
            }
            return ε;
        }
    }

    /// <summary>
    /// Tanh (ƒ(a) = (e²ᵃ - 1) / (e²ᵃ + 1))
    /// </summary>
    public class Tanh : IΩ {
        public static readonly IΩ Ω = New();

        public static IΩ New() {
            return new Tanh();
        }

        public override string ToString() {
            return "ƒ(a) = (e²ᵃ - 1) / (e²ᵃ + 1)";
        }

        static double tanh(double exp) {
            return (exp - 1) / (exp + 1);
        }

        public static double f(double a) {
            return tanh(Math.Exp(2 * a));
        }

        public static double df(double a) {
            return (1 - a * a);
        }

        double IΩ.f(double a) {
            return f(a);
        }

        double IΩ.df(double a) {
            return df(a);
        }
    }

    /// <summary>
    /// Sigmoid (ƒ(a) = 1 / (1 + e⁻ᵃ))
    /// </summary>
    public class Sigmoid : IΩ {
        public static readonly IΩ Ω = New();

        public static IΩ New() {
            return new Sigmoid();
        }

        public override string ToString() {
            return "ƒ(a) = 1 / (1 + e⁻ᵃ)";
        }

        static double sigmoid(double exp) {
            return 1 / (1 + exp);
        }

        public static double f(double a) {
            return sigmoid(Math.Exp(-a));
        }

        public static double df(double a) {
            return a * (1 - a);
        }

        double IΩ.f(double a) {
            return f(a);
        }

        double IΩ.df(double a) {
            return df(a);
        }
    }

    /// <summary>
    /// Softplus (ƒ(a) = log(1 + eᵃ))
    /// </summary>
    public class Softplus : IΩ {
        public static readonly IΩ Ω = New();

        public static IΩ New() {
            return new Softplus();
        }

        public override string ToString() {
            return "ƒ(a) = log(1 + eᵃ)";
        }

        public static double f(double a) {
            return Math.Log(1 + Math.Exp(a));
        }

        public static double df(double a) {
            return 1 / (1 + Math.Exp(-a));
        }

        double IΩ.f(double a) {
            return f(a);
        }

        double IΩ.df(double a) {
            return df(a);
        }
    }

    /// <summary>
    /// ReLU (ƒ(a) = max(0, a))
    /// </summary>
    public class ReLU : IΩ {
        public static readonly IΩ Ω = New();

        public static IΩ New() {
            return new ReLU();
        }

        public override string ToString() {
            return "ƒ(a) = max(0, a)";
        }

        public static double f(double a) {
            return Math.Max(0, a);
        }

        public static double df(double a) {
            return Math.Max(0, Math.Sign(a));
        }

        double IΩ.f(double a) {
            return f(a);
        }

        double IΩ.df(double a) {
            return df(a);
        }
    }

    /// <summary>
    /// Identity (ƒ(a) = a)
    /// </summary>
    public class Identity : IΩ {
        public static readonly IΩ Ω = New();

        public static IΩ New() {
            return new Identity();
        }

        public override string ToString() {
            return "ƒ(a) = a";
        }

        public static double f(double a) {
            return a;
        }

        public static double df(double a) {
            return 1;
        }

        double IΩ.f(double a) {
            return f(a);
        }

        double IΩ.df(double a) {
            return df(a);
        }
    }

    public static partial class Dots {
        public static void print(this Dot[] src, IO.TextWriter console, string separator = ", ") {
            for (int i = 0; src != null && i < src.Length; i++) {
                if (i > 0) {
                    console.Write(separator);
                }
                console.Write(src[i].ToString());
            }
        }

        public static void print(this Dot[] src, string format, IO.TextWriter console, string separator = ", ") {
            for (int i = 0; src != null && i < src.Length; i++) {
                if (i > 0) {
                    console.Write(separator);
                }
                console.Write(src[i].ƒ.ToString(format));
            }
        }

        public static void print(this Dot[][] src, IO.TextWriter console) {
            for (int h = 0; h < src.Length; h++) {
                Dot[] ℓ = src[h];
                for (int i = 0; i < ℓ.Length; i++) {
                    console.Write($"ƒ({i}) = [");
                    if (console == System.Console.Out) {
                        if (ℓ[i].ζ.β > 0) {
                            if (ℓ[i].ζ.β >= 0.0001) {
                                System.Console.ForegroundColor = ConsoleColor.Green;
                            } else {
                                System.Console.ForegroundColor = ConsoleColor.Gray;
                            }
                        } else {
                            if (ℓ[i].ζ.β <= -0.0001) {
                                System.Console.ForegroundColor = ConsoleColor.Red;
                            } else {
                                System.Console.ForegroundColor = ConsoleColor.Gray;
                            }
                        }
                    }
                    console.Write($"{ℓ[i].ζ.β:f4}");
                    if (console == System.Console.Out) {
                        System.Console.ResetColor();
                    }
                    for (int j = 0; j < ℓ[i].β.Length; j++) {
                        console.Write($",");
                        if (console == System.Console.Out) {
                            if (ℓ[i].β[j].β > 0) {
                                if (ℓ[i].β[j].β >= 0.0001) {
                                    System.Console.ForegroundColor = ConsoleColor.Green;
                                } else {
                                    System.Console.ForegroundColor = ConsoleColor.Gray;
                                }
                            } else {
                                if (ℓ[i].β[j].β <= -0.0001) {
                                    System.Console.ForegroundColor = ConsoleColor.Red;
                                } else {
                                    System.Console.ForegroundColor = ConsoleColor.Gray;
                                }
                            }
                        }
                        console.Write($"{ℓ[i].β[j].β:f4}");
                        if (console == System.Console.Out) {
                            System.Console.ResetColor();
                        }
                    }
                    console.Write("]\r\n");
                }
            }
        }
    }
}