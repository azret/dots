namespace System.Dots {

    /// <summary>
    /// A Dot(·) is a high level linear unit that produces a single scalar value ƒ
    /// </summary>
    public partial class Dot {
        public float ƒ, δƒ, δ;

        public static implicit operator Dot(float value) {
            return new Dot() { ƒ = value };
        }

        public static implicit operator Dot(double value) {
            return new Dot() { ƒ = (float)value };
        }

        public static implicit operator Dot(int value) {
            return new Dot() { ƒ = value };
        }

        public override string ToString() {
            return ƒ.ToString();
        }
    }

    /// <summary>
    /// Math
    /// </summary>
    public static partial class Math {
        public static float max(float a, float b) {
            return System.Math.Max(a, b);
        }

        public static int sign(float a) {
            return System.Math.Sign(a);
        }

        public static float sqr(float a) {
            return a * a;
        }

        public static float exp(float a) {
            return (float)System.Math.Exp(a);
        }

        public static float log(float a) {
            return (float)System.Math.Log(a);
        }
    }

    /// <summary>
    /// IΩ (Squashing Function)
    /// </summary>
    public interface IΩ {
        float ƒ(float a);
        float δƒ(float a);
    }

    /// <summary>
    /// y = ƒ(χ) = Ω(βᵀχ + ζ)
    /// </summary>
    public partial class Dot {
        public struct Coefficient {
            public float χ;
            public float β;
            public float δ;

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

        public void size(int χ) {
            if (β == null || β.Length != χ) {
                Coefficient[] tmp = new Coefficient[χ];

                for (int j = 0; j < tmp.Length; j++) {
                    if (β != null && j < β.Length) {
                        tmp[j] = β[j];
                    }
                }

                β = tmp;
            }
        }

        public IΩ Ω;

        [Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public unsafe void compute(Dot[] χ) {
            const float BIAS = 1;

            System.Diagnostics.Debug.Assert(χ.Length == β.Length);

            float y = 0, x;

            ζ.χ = x = BIAS; y += ζ.β * x;

            fixed (Coefficient* c = β) {
                for (int j = 0; j < χ.Length; j++) {
                    c[j].χ = x = χ[j].ƒ;
                    y += c[j].β * x;
                }
            }

            if (Ω == null) {
                ƒ = y; δƒ = 1;
            } else {
                ƒ = Ω.ƒ(y); δƒ = Ω.δƒ(ƒ);
            }
        }

        [Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public unsafe void move(float rate, float momentum) {
            float δ; float Δ = (1 - momentum) * rate * this.δ;
            δ = Δ * this.ζ.χ; this.ζ.β += (1 - 0) * δ + (momentum) * ζ.δ; ζ.δ = δ;
            fixed (Coefficient* p = this.β) {
                Coefficient* c = p;
                int k = this.β.Length / 7; int r = this.β.Length % 7; 
                while (k-- > 0) {
                    δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                    δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                    δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                    δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                    δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                    δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                    δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                }
                switch (r) {
                    case 6:
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        break;
                    case 5:
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        break;
                    case 4:
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        break;
                    case 3:
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        break;
                    case 2:
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        break;
                    case 1:
                        δ = Δ * c->χ; c->β += δ + (momentum) * c->δ; c->δ = δ; c++;
                        break;
                    case 0:
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }
    }

    public static partial class Dots {
        public static Dot[] create(params float[] args) {
            Dot[] dst = new Dot[args.Length];
            for (var i = 0; i < dst.Length; i++) {
                dst[i] = args[i];
            }
            return dst;
        }

        public static Dot[] create<Ω>(int len) where Ω : IΩ, new() {
            return create(len, new Ω());
        }

        public static Dot[] create(int len) {
            return create(len, Identity.Ω);
        }

        public static Dot[] create(int len, IΩ Ω) {
            Dot[] dst = new Dot[len];
            for (int i = 0; i < dst.Length; i++) {
                dst[i] = new Dot() { Ω = Ω };
            }
            return dst;
        }

        public static Dot[] encode(int len, string src) {
            Dot[] dst = new Dot[len];
            for (var i = 0; i < dst.Length; i++) {
                dst[i] = 0f;
                if (i < src.Length) {
                    dst[i].ƒ = src[i] / 113f;
                }
            }
            return dst;
        }

        public static string decode(this Dot[] src) {
            char[] dst = new char[src.Length];
            for (var i = 0; i < src.Length; i++) {
                dst[i] = (char)(src[i].ƒ * 113f);
            }
            return new string(dst);
        }

        static Dot.Coefficient[] copy(this Dot.Coefficient[] src) {
            Dot.Coefficient[] dst = new Dot.Coefficient[src.Length];
            for (int i = 0; i < dst.Length; i++) {
                dst[i] = src[i];
            }
            return dst;
        }

        static Dot copy(Dot src) {
            return new Dot() {
                Ω = src.Ω,
                ƒ = src.ƒ, δƒ = src.δƒ,
                β = copy(src.β), ζ = src.ζ,
                δ = src.δ,
            };
        }

        static Dot[] copy(Dot[] src) {
            Dot[] dst = new Dot[src.Length];
            for (int i = 0; i < dst.Length; i++) {
                dst[i] = copy(src[i]);
            }
            return dst;
        }

        public static Dot[][] copy(this Dot[][] src) {
            Dot[][] dst = new Dot[src.Length][];
            for (int h = 0; h < dst.Length; h++) {
                dst[h] = copy(src[h]);
            }
            return dst;
        }

        static void add(Dot dst, Dot src) {
            Diagnostics.Debug.Assert(dst.β.Length == src.β.Length);
            for (int j = 0; j < dst.β.Length; j++) {
                dst.β[j].β += src.β[j].β;
            }
            dst.ζ.β += src.ζ.β;
        }

        static void add(Dot[] dst, Dot[] src) {
            Diagnostics.Debug.Assert(dst.Length == src.Length);
            for (int i = 0; i < dst.Length; i++) {
                add(dst[i], src[i]);
            }
        }

        public static void add(this Dot[][] dst, Dot[][] src) {
            Diagnostics.Debug.Assert(dst.Length == src.Length);
            for (int h = 0; h < dst.Length; h++) {
                add(dst[h], src[h]);
            }
        }

        static void multiply(this Dot dst, float scalar) {
            for (int j = 0; j < dst.β.Length; j++) {
                dst.β[j].β *= scalar;
            }
            dst.ζ.β *= scalar;
        }

        static void multiply(this Dot[] dst, float scalar) {
            for (int i = 0; i < dst.Length; i++) {
                multiply(dst[i], scalar);
            }
        }

        public static void multiply(this Dot[][] dst, float scalar) {
            for (int h = 0; h < dst.Length; h++) {
                multiply(dst[h], scalar);
            }
        }
    }

    public static partial class Dots {
        static readonly Random randomizer = new Random();

        public static float random(Random r = null) {
            if (r == null) {
                r = randomizer;
            }
            return (float)r.NextDouble();
        }

        public static int random(int min, int max, Random r = null) {
            if (r == null) {
                r = randomizer;
            }
            return r.Next(min, max);
        }

        static void randomize(this Dot dst, Random r) {
            for (int j = 0; j < dst.β.Length; j++) {
                dst.β[j].β = random(r);
            }
            dst.ζ.β = random(r);
        }

        static void randomize(this Dot[] dst, Random r) {
            for (int i = 0; i < dst.Length; i++) {
                randomize(dst[i], r);
            }
        }

        public static void randomize(this Dot[][] dst, Random r = null) {
            for (int h = 0; h < dst.Length; h++) {
                randomize(dst[h], r);
            }
        }

        public static Dot[] random(int len, Random r = null, float median = 1.0f, float scale = 1.0f) {
            Dot[] dst = new Dot[len];
            for (var i = 0; i < dst.Length; i++) {
                dst[i] = scale * random(r) * (median - random(r));
            }
            return dst;
        }
    }

    public static partial class Dots {
        public static void connect(this Dot[][] ℳ, int X, bool randomize = true) {
            for (int h = 0; h < ℳ.Length; h++) {
                Dot[] Y = ℳ[h];
                for (int i = 0; i < Y.Length; i++) {
                    Y[i].size(X);
                    if (randomize) {
                        for (int j = 0; j < Y[i].β.Length; j++) {
                            Y[i].β[j].β = random();
                        }
                        Y[i].ζ.β = random();
                    }
                }
                X = Y.Length;
            }
        }

        public static Dot[] compute(this Dot[][] ℳ, Dot[] X) {
            for (int h = 0; h < ℳ.Length; h++) {
                Dot[] Y = ℳ[h];
                for (int i = 0; i < Y.Length; i++) {
                    Y[i].compute(X);
                }
                X = Y;
            }
            return X;
        }

        public static float cost(this Dot[][] ℳ, Dot[] Ŷ) {
            Dot[] Y = null;
            for (int h = ℳ.Length - 1; h >= 0; h--) {
                if (Y == null) {
                    Y = ℳ[h];
                    System.Diagnostics.Debug.Assert(Y.Length == Ŷ.Length);
                    var cost = 0.0f;
                    for (int i = 0; i < Y.Length; i++) {
                        var δ = Y[i].ƒ - Ŷ[i].ƒ;
                        cost += δ * δ;
                    }
                    return cost / 2;
                }
            }
            return float.NaN;
        }

        public static float sgd(this Dot[][] ℳ, Dot[] X, Dot[] Ŷ, float rate, float momentum) {
            Dot[] Y;
            for (int h = 0; h < ℳ.Length; h++) {
                Y = ℳ[h];
                for (int i = 0; i < Y.Length; i++) {
                    Y[i].compute(X);
                }
                X = Y;
            }
            var cost = 0.0f; Y = null;
            for (int h = ℳ.Length - 1; h >= 0; h--) {
                float δ; Dot y; Dot ŷ; Dot x;
                if (Y == null) {
                    Y = ℳ[h]; 
                    System.Diagnostics.Debug.Assert(Y.Length == Ŷ.Length);
                    for (int i = 0; i < Y.Length; i++) {
                        y = Y[i]; ŷ = Ŷ[i];
                        δ = - (y.ƒ - ŷ.ƒ);
                        y.δ = δ * y.δƒ;
                        cost += Math.sqr(δ);
                    }
                } else {
                    X = ℳ[h];
                    for (int j = 0; j < X.Length; j++) {
                        δ = 0.0f;
                        for (int i = 0; i < Y.Length; i++) {
                            y = Y[i];
                            δ += y.δ * y.β[j].β;
                        }
                        x = X[j];
                        x.δ = δ * x.δƒ;
                    }
                    for (int i = 0; i < Y.Length; i++) {
                        Y[i].move(rate, momentum);
                    }
                    Y = X;
                }
            }
            for (int i = 0; i < Y.Length; i++) {
                Y[i].move(rate, momentum);
            }
            return cost / 2;
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

        static float tanh(float exp) {
            return (exp - 1f) / (exp + 1f);
        }

        public static float f(float a) {
            return tanh(Math.exp(2 * a));
        }

        public static float df(float a) {
            return (1 - a * a);
        }

        float IΩ.ƒ(float a) {
            return f(a);
        }

        float IΩ.δƒ(float a) {
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

        static float sigmoid(float exp) {
            return 1 / (1 + exp);
        }

        public static float f(float a) {
            return sigmoid(Math.exp(-a));
        }

        public static float df(float a) {
            return a * (1 - a);
        }

        float IΩ.ƒ(float a) {
            return f(a);
        }

        float IΩ.δƒ(float a) {
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

        public static float f(float a) {
            return Math.log(1 + Math.exp(a));
        }

        public static float df(float a) {
            return 1 / (1 + Math.exp(-a));
        }

        float IΩ.ƒ(float a) {
            return f(a);
        }

        float IΩ.δƒ(float a) {
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

        public static float f(float a) {
            return Math.max(0, a);
        }

        public static float df(float a) {
            return Math.max(0, Math.sign(a));
        }

        float IΩ.ƒ(float a) {
            return f(a);
        }

        float IΩ.δƒ(float a) {
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

        public static float f(float a) {
            return a;
        }

        public static float df(float a) {
            return 1;
        }

        float IΩ.ƒ(float a) {
            return f(a);
        }

        float IΩ.δƒ(float a) {
            return df(a);
        }
    }

    public static partial class Dots {
        public static void Write(this IO.TextWriter console, Dot[] src, string format = "n4", string separator = ", ") {
            for (int i = 0; src != null && i < src.Length; i++) {
                if (i > 0) {
                    console.Write(separator);
                }
                console.Write(src[i].ƒ.ToString(format));
            }
        }

        public static void WriteLine(this IO.TextWriter console, Dot[] src, string format, string separator = ", ") {
            Write(console, src, format, separator);
            console.WriteLine();
        }
        
        public static void Write(this IO.TextWriter console, Dot[][] src, string format = "n4") {
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
                    console.Write($"{ℓ[i].ζ.β.ToString(format)}");
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
                        console.Write($"{ℓ[i].β[j].β.ToString(format)}");
                        if (console == System.Console.Out) {
                            System.Console.ResetColor();
                        }
                    }
                    console.Write("]\r\n");
                }
            }
        }

        public static void WriteLine(this IO.TextWriter console, Dot[][] src, string format = "n4") {
            Write(console, src, format);
            console.WriteLine();
        }
    }
}