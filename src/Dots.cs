using System;

public static class Dots {
    public interface IFunction {
        double f(double value);
        double df(double value);
    }
     
    public class Tanh : IFunction {
        public static readonly IFunction Default = New();

        public static IFunction New() {
            return new Tanh();
        }

        public static double f(double value) {
            return Math.Tanh(value);
        }

        public static double df(double value) {
            return (1 - value * value);
        }

        double IFunction.f(double value) {
            return f(value);
        }

        double IFunction.df(double value) {
            return df(value);
        }
    }
     
    public class Sigmoid : IFunction {
        public static readonly IFunction Default = New();

        public static IFunction New() {
            return new Sigmoid();
        }

        public static double f(double value) {
            return 1 / (1 + Math.Exp(-value));
        }

        public static double df(double value) {
            return value * (1 - value);
        }

        double IFunction.f(double value) {
            return f(value);
        }

        double IFunction.df(double value) {
            return df(value);
        }
    }
     
    public class Identity : IFunction {
        public static readonly IFunction Default = New();

        public static IFunction New() {
            return new Identity();
        }

        public static double f(double value) {
            return value;
        }

        public static double df(double value) {
            return 1.0;
        }

        double IFunction.f(double value) {
            return f(value);
        }

        double IFunction.df(double value) {
            return df(value);
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
                β[j].ω = random();
            }

            intercept = random();
        }

        IFunction _F;

        public IFunction F {
            get {
                return _F;
            }
        }

        public Dot(IFunction F = null) {
            _F = F;
        }

        public double intercept;

        public struct Coefficient {
            public double ψ;
            public double ξ;
            public double ω;
        }

        public Coefficient[] β;

        public double δ;

        public double f;

        public double df;

        public static implicit operator Dot(double y) {
            return new Dot() { f = y };
        }

        public override string ToString() {
            return f.ToString();
        }
         
        public void connect(int X) {
            int j;

            if (β == null || β.Length < X) {
                var tmp = new Coefficient[X];

                j = 0;

                for (; β != null && j < β.Length; j++) {
                    tmp[j] = β[j];
                }

                for (; j < tmp.Length; j++) {
                    tmp[j].ω = random();
                }

                β = tmp;
            }
        }         

        public void compute(params Dot[] X) {
            System.Diagnostics.Debug.Assert(X.Length == β.Length);

            int len = β.Length;             

            var y = 0.0; int j;

            j = 0;

            while (j < len) {
                double x;

                x = X[j].f;

                β[j].ξ = x;

                y += β[j].ω * x;

                j++;
            }

            y += intercept * 1.0;

            if (_F == null) {
                f = y; df = 1.0;
            } else {
                f = _F.f(y); df = _F.df(f);
            }
        }

        public void move(double μ) {
            int len = β.Length;

            int j;

            j = 0;

            while (j < len) {
                var change = δ * β[j].ξ;

                β[j].ω += change + μ * β[j].ψ;

                β[j].ψ = change;

                j++;
            }

            intercept += δ * 1.0;
        }

    }
      
    public static Dot[] create(int size, IFunction F = null) {
        var L = new Dots.Dot[size];

        for (int i = 0; i < L.Length; i++) {
            L[i] = new Dot(F);
        }
        
        return L;
    }
     
    public static void connect(Dot[] Y, Dot[][] H, int X) {
        if (H != null) {
            for (int l = 0; H != null && l < H.Length; l++) {
                Dot[] h = H[l];

                for (int i = 0; i < h.Length; i++) {
                    h[i].connect(X);
                }

                X = h.Length;
            }
        }

        for (int i = 0; Y != null && i < Y.Length; i++) {
            Y[i].connect(X);
        }
    }

    public static void compute(Dot[] Y, Dot[][] H, params Dot[] X) {
        if (H != null) {
            for (int l = 0; H != null && l < H.Length; l++) {
                Dot[] h = H[l];

                for (int i = 0; i < h.Length; i++) {
                    h[i].compute(X);
                }

                X = h;
            }
        }

        for (int i = 0; Y != null && i < Y.Length; i++) {
            Y[i].compute(X);
        }
    }

    public static double error(Dot[] Y, Dot[] T) {
        System.Diagnostics.Debug.Assert(Y.Length == T.Length);

        double Δ = 0.0; int len = Y.Length;

        for (int i = 0; i < len; i++) {
            Δ += Math.Pow(Y[i].f - T[i].f, 2);
        }

        return Δ * 0.5;
    }

    public static void sgd(Dot[] Y, Dot[][] H, Dot[] T, double learningRate, double momentum) {
        System.Diagnostics.Debug.Assert(Y.Length == T.Length);

        int len = Y.Length;

        for (int i = 0; i < len; i++) {
            var y = Y[i]; var t = T[i];

            y.δ = -(y.f - t.f) * y.df * learningRate;
        }

        Dot[] L;

        L = Y;

        if (H != null) {

            for (int l = H.Length - 1; l >= 0; l--) {
                Dot[] P = L; L = H[l];

                for (int i = 0; i < L.Length; i++) {
                    var δ = 0.0; len = P.Length;

                    for (int j = 0; j < len; j++) {
                        δ += P[j].δ * P[j].β[i].ω;
                    }

                    var o = L[i];

                    o.δ = δ * o.df * learningRate;
                }
            }
        }

        for (int i = 0; i < Y.Length; i++) {
            Y[i].move(momentum);
        }

        if (H != null) {
            for (int l = H.Length - 1; l >= 0; l--) {
                L = H[l];

                for (int i = 0; i < L.Length; i++) {
                    L[i].move(momentum);
                }
            }
        }

    }
}