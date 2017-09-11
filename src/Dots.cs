using System;
using System.Collections.Generic;
using System.Text;

public static class Dots
{
    public interface IFunction
    {
        double y(double value);
        double dy(double value);
    }

    public class Tanh : IFunction
    {
        public static IFunction New()
        {
            return new Tanh();
        }

        public static double y(double value)
        {
            return Math.Tanh(value);
        }

        public static double dy(double value)
        {
            return (1 - value * value);
        }

        double IFunction.y(double value)
        {
            return y(value);
        }

        double IFunction.dy(double value)
        {
            return dy(value);
        }
    }

    public class Sigmoid : IFunction
    {
        public static IFunction New()
        {
            return new Sigmoid();
        }

        public static double y(double value)
        {
            return 1 / (1 + Math.Exp(-value));
        }

        public static double dy(double value)
        {
            return value * (1 - value);
        }

        double IFunction.y(double value)
        {
            return y(value);
        }

        double IFunction.dy(double value)
        {
            return dy(value);
        }
    }

    public class Identity : IFunction
    {
        public static IFunction New()
        {
            return new Identity();
        }

        public static double y(double value)
        {
            return value;
        }

        public static double dy(double value)
        {
            return 1.0;
        }

        double IFunction.y(double value)
        {
            return y(value);
        }

        double IFunction.dy(double value)
        {
            return dy(value);
        }
    }

    public class Dot
    {
        public struct θ
        {
            public double a;
            public double x;
            public double b;
        }

        public static readonly Random randomizer = new Random();

        public static double random()
        {
            return randomizer.NextDouble();
        }

        public static int random(int max)
        {
            return randomizer.Next(max);
        }

        θ[] _β;

        public θ[] β
        {
            get
            {
                return _β;
            }
        }

        double _βo;

        public double βo
        {
            get
            {
                return _βo;
            }
        }

        public void randomize()
        {
            for (int j = 0; _β != null && j < _β.Length; j++)
            {
                _β[j].b = random();
            }

            _βo = random();
        }

        public void draw(double Bo, params double[] B)
        {
            _βo = Bo;

            var θn = new θ[B.Length];

            for (int j = 0; j < θn.Length; j++)
            {
                θn[j].a = 0.0; θn[j].x = 0.0; θn[j].b = 0.0;

                if (j < B.Length)
                {
                    θn[j].b = B[j];
                }
            }

            _β = θn;
        }

        IFunction _F;

        public IFunction F
        {
            get
            {
                return _F;
            }
        }

        public Dot(IFunction F = null)
        {
            _F = F;
        }

        double _δ;

        public double δ
        {
            get
            {
                return this._δ;
            }
            set
            {
                this._δ = value;
            }
        }

        double? _y;

        public double y
        {
            get
            {
                return _y.Value;
            }
            set
            {
                _y = value;
            }
        }

        public static implicit operator Dot(double y)
        {
            return new Dot() { y = y };
        }

        public void off()
        {
            _y = null; _dy = null;
        }

        double? _dy;

        public double dy
        {
            get
            {
                return this._dy.Value;
            }
        }

        public int Capacity
        {
            get
            {
                return _β.Length;
            }
        }

        public void connect(int X)
        {
            int j;

            if (_β == null || _β.Length < X)
            {
                var tmp = new θ[X];

                j = 0;

                for (; _β != null && j < _β.Length; j++)
                {
                    tmp[j] = _β[j];
                }

                for (; j < tmp.Length; j++)
                {
                    tmp[j].b = random();
                }

                _β = tmp;
            }
        }

        int _length = 0;

        public int Length
        {
            get
            {
                return _length;
            }
        }

        /// <summary>
        ///  y = f(x0, x1, ..., xn) = Ω(Σ(x0*β0 + x1*β1 + ... + xn*βn + c))
        /// </summary>
        public void compute(params Dot[] X)
        {
            int len = 0;

            if (_β != null)
            {
                len = _β.Length;
            }

            if (X.Length < len)
            {
                len = X.Length;
            }

            len = _length = len;

            var y = 0.0; int j;

            j = 0;

            while (j < len)
            {
                double x;

                x = X[j].y;

                _β[j].x = x;

                y += _β[j].b * x;

                j++;
            }

            y += _βo * 1.0;

            if (_F == null)
            {
                _y = y; _dy = 1.0;
            }
            else
            {
                _y = _F.y(y); _dy = _F.dy(_y.Value);
            }
        }

        public void learn()
        {
            int len = _length;

            int j;

            j = 0;

            while (j < len)
            {
                _β[j].b = _β[j].b + δ * _β[j].x;

                j++;
            }

            _βo += δ * 1.0;
        }

    }

    public static string inspect(this Dot dot)
    {
        StringBuilder s = new StringBuilder();

        s.Append("[");        

        s.Append($"B0:{dot.βo}");

        var j = 0;

        while (dot.β != null && j < dot.β.Length)
        {
            s.Append($", B{j + 1}:{dot.β[j].b}");
            j++;
        }

        s.Append("]");

        return s.ToString();
    }

    public static void create(ref Dot[] Y, int count, IFunction F = null)
    {
        int j;

        if (Y == null)
        {
            Y = new Dot[] 
            {
            };
        }

        if (Y == null || Y.Length < count)
        {
            var tmp = new Dot[count];

            j = 0;

            for (; Y != null && j < Y.Length; j++)
            {
                tmp[j] = Y[j];
            }

            for (; j < tmp.Length; j++)
            {
                tmp[j] = new Dot(F);
            }

            Y = tmp;
        }
    }

    public static Dot create()
    {
        return new Dot(null);
    }

    public static Dot[] create(int size, IFunction F = null)
    {
        var L = new Dots.Dot[size];

        for (int i = 0; i < L.Length; i++)
        {
            L[i] = new Dot(F);            
        }

        L.randomize();

        return L;
    }

    public static Dot create(double Bo, params double[] B)
    {
        var o = new Dot();

        o.draw(Bo, B);

        return o;
    }

    public static Dot sigmoid()
    {
        return new Dot(new Sigmoid());
    }

    public static Dot tanh()
    {
        return new Dot(new Tanh());
    }

    public static void randomize<T>(this IList<T> A)
    {
        int c = A.Count;

        while (c > 1)
        {
            int k = Dot.randomizer.Next(c--);

            T t = A[c]; A[c] = A[k];

            A[k] = t;
        }
    }

    public static void randomize(this Dot[] dots)
    {
        for (int i = 0; dots != null && i < dots.Length; i++)
        {
            dots[i].randomize();
        }
    }

    public static void randomize(this Dot[][] dots)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            randomize(dots[i]);
        }
    }
    
    public static void connect(Dot[] Y, Dot[][] H, Dot[] X)
    {
        if (H != null)
        {
            for (int l = 0; H != null && l < H.Length; l++)
            {
                Dot[] h = H[l];

                for (int i = 0; i < h.Length; i++)
                {
                    h[i].connect(X.Length);
                }

                X = h;
            }
        }

        for (int i = 0; Y != null && i < Y.Length; i++)
        {
            Y[i].connect(X.Length);
        }
    }

    public static void compute(Dot[] Y, Dot[][] H, Dot[] X)
    {
        if (H != null)
        {
            for (int l = 0; H != null && l < H.Length; l++)
            {
                Dot[] h = H[l];

                for (int i = 0; i < h.Length; i++)
                {
                    h[i].compute(X);
                }

                X = h;
            }
        }

        for (int i = 0; Y != null && i < Y.Length; i++)
        {
            Y[i].compute(X);
        }
    }

    public static double sgd(Dot[] Y, Dot[][] H, Dot[] T, double learningRate)
    {
        double Δ;

        sgd(Y, H, T, learningRate, out Δ);

        return Δ;
    }

    public static double sgd(Dot[] X, Dot[] Y, Dot[][] H, Dot[] T, double learningRate)
    {
        double Δ;

        if (X != null)
        {
            compute(Y, H, X);
        }

        sgd(Y, H, T, learningRate, out Δ);

        return Δ;
    }

    public static void sgd(Dot[] Y, Dot[][] H, Dot[] T, double learningRate, out double Δ)
    {
        Δ = 0.0;

        for (int i = 0; i < Y.Length; i++)
        {
            var o = Y[i];

            var t = o;

            if (i < T.Length)
            {
                t = T[i];
            }

            var diff = o.y - t.y;             

            Δ += Math.Pow(diff, 2);

            o.δ = -diff * o.dy * learningRate;
        }

        Δ *= 0.5;

        Dot[] L;

        L = Y;

        if (H != null)
        {
            for (int l = H.Length - 1; l >= 0; l--)
            {
                Dot[] P = L; L = H[l];

                for (int i = 0; i < L.Length; i++)
                {
                    var δ = 0.0;

                    for (int j = 0; j < P.Length; j++)
                    {
                        δ += P[j].δ * P[j].β[i].b;
                    }

                    Δ += 0.0;

                    var o = L[i];

                    o.δ = δ * o.dy * learningRate;
                }
            }
        }

        for (int i = 0; i < Y.Length; i++)
        {
            Y[i].learn();
        }

        if (H != null)
        {
            for (int l = H.Length - 1; l >= 0; l--)
            {
                L = H[l];

                for (int i = 0; i < L.Length; i++)
                {
                    L[i].learn();
                }
            }
        }
    }

    /// <summary>
    /// Δ = 1⁄2 * Σ(y - t)² (Half of the sum of the squared errors)
    /// </summary>
    public static double error(Dot[] Y, Dot[] T)
    {
        double Δ = 0.0;

        for (int i = 0; Y != null && i < Y.Length; i++)
        {
            var o = Y[i];

            var t = o;

            if (i < T.Length)
            {
                t = T[i];
            }

            var diff = o.y - t.y;

            Δ += Math.Pow(diff, 2);
        }

        Δ *= 0.5;

        return Δ;
    }
}
