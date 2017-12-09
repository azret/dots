using System;
using System.IO;

namespace Recipes
{
    public static class Iris
    {
        static void Print(string header, Dots.Dot[] X, Dots.Dot[] Ycomputed, Dots.Dot[] Yexpected)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("[");

            for (var i = 0; X != null && i < X.Length; i++)
            {
                if (i > 0)
                {
                    Console.Write($", ");
                }

                Console.Write($"{X[i].y}");
            }

            Console.Write("]");

            Console.Write(" = ");

            Console.Write("[");

            int max = -1;

            for (var i = 0; Ycomputed != null && i < Ycomputed.Length; i++)
            {
                if (i > 0)
                {
                    Console.Write($", ");
                }

                if (max < 0)
                {
                    max = i;
                }
                else
                {
                    if (Ycomputed[i].y > Ycomputed[max].y)
                    {
                        max = i;
                    }
                }

                Console.Write($"{Ycomputed[i].y}");
            }

            Console.Write("]");

            Console.Write(" ~ ");

            Console.Write("[");

            for (var i = 0; Yexpected != null && i < Yexpected.Length; i++)
            {
                if (i > 0)
                {
                    Console.Write($", ");
                }

                Console.Write($"{Yexpected[i].y}");
            }

            Console.Write("]");

            Console.Write(" - ");

            Console.Write("[");

            for (var i = 0; Ycomputed != null && i < Ycomputed.Length; i++)
            {
                if (i > 0)
                {
                    Console.Write($", ");
                }
                
                if (i == max)
                {
                    Console.Write($"{1}");
                }
                else
                {
                    Console.Write($"{0}");
                }

            }

            Console.Write("]");

            Console.WriteLine();

            Console.ResetColor();
        }
         
        static void Test(Dots.Dot[] X, Dots.Dot[] Y, Dots.Dot[][] H, Dots.Dot[] Expected)
        {
            Dots.compute(Y, H, X);

            Print("X", X, Y, Expected);
        }

        static void Run(Func<double> α, Dots.IFunction F, ref Dots.Dot[] Y,
            Dots.Dot[][] H, int K, Func<int, Dots.Dot[]> X, Func<int, Dots.Dot[]> T, int episodes,
            Func<int, Dots.Dot[], double, int> epoch)
        {

            for (int episode = 0; episode < episodes; episode++)
            {
                int k = Dots.Dot.random(K);

                var x = X(k); var t = T(k);

                double E = Dots.sgd(x, ref Y, H, t, α());

                if (E <= double.Epsilon || double.IsNaN(E) || double.IsInfinity(E))
                {
                    break;
                }

                if (epoch != null)
                {
                    episode = epoch(episode, x, E);
                }
            }
        }

        static string itoa(int n)
        {
            string a = null;

            while (n > 0)
            {
                Console.WriteLine(n % 10);
                n = n / 10;
            }

            return a;
        }

        static string itoh(int n)
        {
            char[] digest = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

            string a = null; int b = digest.Length;

            while (n > 0)
            {
                Console.WriteLine(digest[n % b]);
                n = n / b;
            }

            return a;
        }

        static void Main(string[] args)
        {
            itoh(16);

            Console.ReadKey();
            return;

            var Input = new System.Collections.Generic.List<Dots.Dot[]>();
            var Output = new System.Collections.Generic.List<Dots.Dot[]>();

            Plant[] IRIS = Load();

            // Shuffle the data

            Dots.randomize(IRIS);

            // Normalize

            foreach (var m in IRIS)
            {
                Input.Add(new Dots.Dot[] 
                {
                    m.SepalLength * 0.1,
                    m.SepalWidth * 0.1,
                    m.PetalLength * 0.1,
                    m.PetalWidth * 0.1,
                });

                switch (m.Name)
                {
                    case "Iris-setosa":
                        Output.Add(new Dots.Dot[] { 1, 0, 0 });
                        break;
                    case "Iris-versicolor":
                        Output.Add(new Dots.Dot[] { 0, 1, 0 });
                        break;
                    case "Iris-virginica":
                        Output.Add(new Dots.Dot[] { 0, 0, 1 });
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
             
            bool canceled = false;

            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = canceled = true;
            };

            var H = new Dots.Dot[][]
            {
                // No hiddent layer
            };

            Dots.Dot[] Y = null;

            var i = Dots.Dot.random(Input.Count);

            Test(Input[i], Y, H, Output[i]);

            double E = 0.0;

            Run(() => 0.01,

                null,

                ref Y,

                H,

                IRIS.Length,

                (k) =>
                {
                    return Input[k];
                },

                (k) =>
                {
                    return Output[k];
                },

                32 * 1024,

                (episode, X, error) =>
                {
                    E += error * error * (episode + 1);

                    if (episode % 100 == 0)
                    {
                        Console.WriteLine($"{1 / E}");
                    }

                    if (canceled)
                    {
                        episode = int.MaxValue - 1;
                    }

                    return episode;
                }

            );

            for (i = 0; i < Input.Count; i++)
            {
                Test(Input[i], Y, H, Output[i]);
            }

            Console.ReadKey();

        }

        public class Plant
        {
            /// <summary>
            /// Name of the Plant
            /// </summary>
            public string Name;
            /// <summary>
            ///  Sepal Length (cm)
            /// </summary>
            public double SepalLength;
            /// <summary>
            ///  Sepal Width (cm)
            /// </summary>
            public double SepalWidth;
            /// <summary>
            ///  Petal Width (cm)
            /// </summary>
            public double PetalLength;
            /// <summary>
            ///  Petal Length (cm)
            /// </summary>
            public double PetalWidth;
            public override string ToString()
            {
                return string.Format("{0},{1},{2},{3},{4}", SepalLength, SepalWidth, PetalLength, PetalWidth, Name);
            }
        }

        static Plant[] iris;

        public static Plant[] Load()
        {
            if (iris != null)
            {
                return iris;
            }

            System.Collections.Generic.List<Plant> list = new System.Collections.Generic.List<Plant>();

            string line = null;

            using (TextReader reader = new StringReader(data))
            {
                while (!string.IsNullOrWhiteSpace(line = reader.ReadLine()))
                {
                    string[] fields = line.Split(',');

                    System.Diagnostics.Debug.Assert(fields.Length == 5);

                    Plant rec = new Plant()
                    {
                        SepalLength = double.Parse(fields[0]),
                        SepalWidth = double.Parse(fields[1]),
                        PetalLength = double.Parse(fields[2]),
                        PetalWidth = double.Parse(fields[3]),
                        Name = fields[4]
                    };

                    list.Add(rec);
                }
            }

            return iris = list.ToArray();
        }

        static readonly string data =
            @"5.1,3.5,1.4,0.2,Iris-setosa
            4.9,3.0,1.4,0.2,Iris-setosa
            4.7,3.2,1.3,0.2,Iris-setosa
            4.6,3.1,1.5,0.2,Iris-setosa
            5.0,3.6,1.4,0.2,Iris-setosa
            5.4,3.9,1.7,0.4,Iris-setosa
            4.6,3.4,1.4,0.3,Iris-setosa
            5.0,3.4,1.5,0.2,Iris-setosa
            4.4,2.9,1.4,0.2,Iris-setosa
            4.9,3.1,1.5,0.1,Iris-setosa
            5.4,3.7,1.5,0.2,Iris-setosa
            4.8,3.4,1.6,0.2,Iris-setosa
            4.8,3.0,1.4,0.1,Iris-setosa
            4.3,3.0,1.1,0.1,Iris-setosa
            5.8,4.0,1.2,0.2,Iris-setosa
            5.7,4.4,1.5,0.4,Iris-setosa
            5.4,3.9,1.3,0.4,Iris-setosa
            5.1,3.5,1.4,0.3,Iris-setosa
            5.7,3.8,1.7,0.3,Iris-setosa
            5.1,3.8,1.5,0.3,Iris-setosa
            5.4,3.4,1.7,0.2,Iris-setosa
            5.1,3.7,1.5,0.4,Iris-setosa
            4.6,3.6,1.0,0.2,Iris-setosa
            5.1,3.3,1.7,0.5,Iris-setosa
            4.8,3.4,1.9,0.2,Iris-setosa
            5.0,3.0,1.6,0.2,Iris-setosa
            5.0,3.4,1.6,0.4,Iris-setosa
            5.2,3.5,1.5,0.2,Iris-setosa
            5.2,3.4,1.4,0.2,Iris-setosa
            4.7,3.2,1.6,0.2,Iris-setosa
            4.8,3.1,1.6,0.2,Iris-setosa
            5.4,3.4,1.5,0.4,Iris-setosa
            5.2,4.1,1.5,0.1,Iris-setosa
            5.5,4.2,1.4,0.2,Iris-setosa
            4.9,3.1,1.5,0.2,Iris-setosa
            5.0,3.2,1.2,0.2,Iris-setosa
            5.5,3.5,1.3,0.2,Iris-setosa
            4.9,3.6,1.4,0.1,Iris-setosa
            4.4,3.0,1.3,0.2,Iris-setosa
            5.1,3.4,1.5,0.2,Iris-setosa
            5.0,3.5,1.3,0.3,Iris-setosa
            4.5,2.3,1.3,0.3,Iris-setosa
            4.4,3.2,1.3,0.2,Iris-setosa
            5.0,3.5,1.6,0.6,Iris-setosa
            5.1,3.8,1.9,0.4,Iris-setosa
            4.8,3.0,1.4,0.3,Iris-setosa
            5.1,3.8,1.6,0.2,Iris-setosa
            4.6,3.2,1.4,0.2,Iris-setosa
            5.3,3.7,1.5,0.2,Iris-setosa
            5.0,3.3,1.4,0.2,Iris-setosa
            7.0,3.2,4.7,1.4,Iris-versicolor
            6.4,3.2,4.5,1.5,Iris-versicolor
            6.9,3.1,4.9,1.5,Iris-versicolor
            5.5,2.3,4.0,1.3,Iris-versicolor
            6.5,2.8,4.6,1.5,Iris-versicolor
            5.7,2.8,4.5,1.3,Iris-versicolor
            6.3,3.3,4.7,1.6,Iris-versicolor
            4.9,2.4,3.3,1.0,Iris-versicolor
            6.6,2.9,4.6,1.3,Iris-versicolor
            5.2,2.7,3.9,1.4,Iris-versicolor
            5.0,2.0,3.5,1.0,Iris-versicolor
            5.9,3.0,4.2,1.5,Iris-versicolor
            6.0,2.2,4.0,1.0,Iris-versicolor
            6.1,2.9,4.7,1.4,Iris-versicolor
            5.6,2.9,3.6,1.3,Iris-versicolor
            6.7,3.1,4.4,1.4,Iris-versicolor
            5.6,3.0,4.5,1.5,Iris-versicolor
            5.8,2.7,4.1,1.0,Iris-versicolor
            6.2,2.2,4.5,1.5,Iris-versicolor
            5.6,2.5,3.9,1.1,Iris-versicolor
            5.9,3.2,4.8,1.8,Iris-versicolor
            6.1,2.8,4.0,1.3,Iris-versicolor
            6.3,2.5,4.9,1.5,Iris-versicolor
            6.1,2.8,4.7,1.2,Iris-versicolor
            6.4,2.9,4.3,1.3,Iris-versicolor
            6.6,3.0,4.4,1.4,Iris-versicolor
            6.8,2.8,4.8,1.4,Iris-versicolor
            6.7,3.0,5.0,1.7,Iris-versicolor
            6.0,2.9,4.5,1.5,Iris-versicolor
            5.7,2.6,3.5,1.0,Iris-versicolor
            5.5,2.4,3.8,1.1,Iris-versicolor
            5.5,2.4,3.7,1.0,Iris-versicolor
            5.8,2.7,3.9,1.2,Iris-versicolor
            6.0,2.7,5.1,1.6,Iris-versicolor
            5.4,3.0,4.5,1.5,Iris-versicolor
            6.0,3.4,4.5,1.6,Iris-versicolor
            6.7,3.1,4.7,1.5,Iris-versicolor
            6.3,2.3,4.4,1.3,Iris-versicolor
            5.6,3.0,4.1,1.3,Iris-versicolor
            5.5,2.5,4.0,1.3,Iris-versicolor
            5.5,2.6,4.4,1.2,Iris-versicolor
            6.1,3.0,4.6,1.4,Iris-versicolor
            5.8,2.6,4.0,1.2,Iris-versicolor
            5.0,2.3,3.3,1.0,Iris-versicolor
            5.6,2.7,4.2,1.3,Iris-versicolor
            5.7,3.0,4.2,1.2,Iris-versicolor
            5.7,2.9,4.2,1.3,Iris-versicolor
            6.2,2.9,4.3,1.3,Iris-versicolor
            5.1,2.5,3.0,1.1,Iris-versicolor
            5.7,2.8,4.1,1.3,Iris-versicolor
            6.3,3.3,6.0,2.5,Iris-virginica
            5.8,2.7,5.1,1.9,Iris-virginica
            7.1,3.0,5.9,2.1,Iris-virginica
            6.3,2.9,5.6,1.8,Iris-virginica
            6.5,3.0,5.8,2.2,Iris-virginica
            7.6,3.0,6.6,2.1,Iris-virginica
            4.9,2.5,4.5,1.7,Iris-virginica
            7.3,2.9,6.3,1.8,Iris-virginica
            6.7,2.5,5.8,1.8,Iris-virginica
            7.2,3.6,6.1,2.5,Iris-virginica
            6.5,3.2,5.1,2.0,Iris-virginica
            6.4,2.7,5.3,1.9,Iris-virginica
            6.8,3.0,5.5,2.1,Iris-virginica
            5.7,2.5,5.0,2.0,Iris-virginica
            5.8,2.8,5.1,2.4,Iris-virginica
            6.4,3.2,5.3,2.3,Iris-virginica
            6.5,3.0,5.5,1.8,Iris-virginica
            7.7,3.8,6.7,2.2,Iris-virginica
            7.7,2.6,6.9,2.3,Iris-virginica
            6.0,2.2,5.0,1.5,Iris-virginica
            6.9,3.2,5.7,2.3,Iris-virginica
            5.6,2.8,4.9,2.0,Iris-virginica
            7.7,2.8,6.7,2.0,Iris-virginica
            6.3,2.7,4.9,1.8,Iris-virginica
            6.7,3.3,5.7,2.1,Iris-virginica
            7.2,3.2,6.0,1.8,Iris-virginica
            6.2,2.8,4.8,1.8,Iris-virginica
            6.1,3.0,4.9,1.8,Iris-virginica
            6.4,2.8,5.6,2.1,Iris-virginica
            7.2,3.0,5.8,1.6,Iris-virginica
            7.4,2.8,6.1,1.9,Iris-virginica
            7.9,3.8,6.4,2.0,Iris-virginica
            6.4,2.8,5.6,2.2,Iris-virginica
            6.3,2.8,5.1,1.5,Iris-virginica
            6.1,2.6,5.6,1.4,Iris-virginica
            7.7,3.0,6.1,2.3,Iris-virginica
            6.3,3.4,5.6,2.4,Iris-virginica
            6.4,3.1,5.5,1.8,Iris-virginica
            6.0,3.0,4.8,1.8,Iris-virginica
            6.9,3.1,5.4,2.1,Iris-virginica
            6.7,3.1,5.6,2.4,Iris-virginica
            6.9,3.1,5.1,2.3,Iris-virginica
            5.8,2.7,5.1,1.9,Iris-virginica
            6.8,3.2,5.9,2.3,Iris-virginica
            6.7,3.3,5.7,2.5,Iris-virginica
            6.7,3.0,5.2,2.3,Iris-virginica
            6.3,2.5,5.0,1.9,Iris-virginica
            6.5,3.0,5.2,2.0,Iris-virginica
            6.2,3.4,5.4,2.3,Iris-virginica
            5.9,3.0,5.1,1.8,Iris-virginica";

    }
}