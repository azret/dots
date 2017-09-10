using System;
using System.Collections.Generic;
using System.Text;

public static class Dots
{
    public interface IFunction
    {
        double y(double x);
        double dy(double y);
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

        public void grow(int X)
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

    public static void grow(int X, Dot[][] H, Dot[] Y)
    {
        if (H != null)
        {
            for (int l = 0; H != null && l < H.Length; l++)
            {
                Dot[] h = H[l];

                for (int i = 0; i < h.Length; i++)
                {
                    h[i].grow(X);
                }

                X = h.Length;
            }
        }

        for (int i = 0; Y != null && i < Y.Length; i++)
        {
            Y[i].grow(X);
        }
    }

    public static void grow(Dot[] X, Dot[][] H, Dot[] Y)
    {
        grow(X.Length, H, Y);
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


    public static double train(Dot[] Y, Dot[][] H, Dot[] T, double learningRate)
    {
        double Δ;

        train(Y, H, T, learningRate, out Δ);

        return Δ;
    }

    public static void train(Dot[] Y, Dot[][] H, Dot[] T, double learningRate, out double Δ)
    {
        Δ = 0.0;

        for (int i = 0; i < Y.Length; i++)
        {
            var o = Y[i];

            double t = o.y;

            if (i < T.Length)
            {
                t = T[i].y;
            }

            Δ += Math.Pow(o.y - t, 2);

            o.δ = -(o.y - t) * o.dy * learningRate;
        }

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

    public static double error(Dot[] Y, Dot[] T)
    {
        double Δ = 0.0;

        for (int i = 0; i < Y.Length; i++)
        {
            var o = Y[i];

            double t = o.y;

            if (i < T.Length)
            {
                t = T[i].y;
            }

            Δ += Math.Pow(o.y - t, 2);
        }

        return Δ;
    }
}
