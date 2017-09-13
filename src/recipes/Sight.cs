using System;

namespace Recipes
{
    public static class Sight
    {
        static void Print(string header, Dots.Dot[] vec)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{header}:\r\n");
            for (var i = 0; i < vec.Length; i++)
            {
                Console.WriteLine($"{vec[i].y}");
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        static void Inspect(string header, Dots.Dot[] vec, ConsoleColor color)
        {
            Console.WriteLine($"{header}:\r\n");
            for (var i = 0; i < vec.Length; i++)
            {
                Console.ForegroundColor = color;
                Console.Write($"{vec[i].y}");
                Console.ResetColor();
                // Console.WriteLine($" = {vec[i].inspect()}");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static string[] Unseen = new string[]
        {

@"| No | Name | [] |
| ---|------|----|
| ---|------|----|
| ---|------|----|",

@"| a  | b | c |
| --|-- |---|---|
| --|-- |---|---|",

@"| No | Name |
==========
|  1 | Joe |
|  2 | Bob |
|  3 | Dave |",

@"| ---|------|----| ---|------||------|----|----|
| ---|------|----| ---|------|-------|----| ---|
| ---|------|----| ---|------|-------|----| ---|
| ---|------|----| ---|------|-------|----| ---|"

        };

        public static string[] Seen = new string[]
        {

/* With Header */

@"| No | Name | SSN |
  ==================
  | 1 | Joe |  x |
  | 2 | Bob |  x |",

@"| No | Description | Price |
  ==================
  | 1 | E3F5 |  $ |
  | 2 | A3C7 |  $ |",

@"| a | b | c |
  | 1 | E3F5 |  $ |
  | 2 | A3C7 |  $ |
  | 3 | 33F9 | $  |",

@"| 1 | E3F5 |  $ |
  | 2 | A3C7 |  $ |
  | 2 | A3C7 |  $ |
  | 3 | 33F9 | $  |",

/* 0 */

@"| No | Name | [] |       
| ---|------|----|
| ---|------|----|       
| ---|------|----|       ",

/* 1 */

@"| 1  | 2 | [] |       
| --dfs-|---sdfsdf---|-dsf--|       
| --sdf-|-df-[]-sdf---|---d-|       
| --asdf-|---adsfsd--|--s--|       ",

/* 2 */

@"| ID | Last |       
|  1 | Smith |       
|  2 | Smith |       
|  3 | Smith |       ",

/* 3 */

@"|| ID || First || Last || x ||       
|| -  || --dsf - ||   | ||  x  ||
||  --  || ---|| -sfd-- x  ||       
|| - sdf  || --dsf - ||   | ||  x  ||       ",


/* 4 */

@"| ---|------|----| ---|------|----|
| ---|------|----| ---|------|----|
| ---|------|----| ---|------|----|
| ---|------|----| ---|------|----|",

@"| ---|| ---|------|----|       
| ---|----|------|----|       
| ---|-----|------|----|       
| ---|---|------|----|       ",

@"| ---|-----| ---|------|---|------|----|       
| ---|------|---|------|----|-----|----|       
| ---|------|---|------|----|-----|----|       
| ---|------|---|------|----|-----|----|",

@"| ---|-----| ---|------|---|----|---|------|----|       
| ---|------|---|------|----|---|----|-----|----|       
| ---|------|---|------|----|---|----|-----|----|
| ---|------|---|------|----|---|----|-----|----|",


/* 5 */

@"| ID | First | Last | | Last |       
|  1  | John   |  |
|  3  | John   |  |       ",

/* 6 */

@"| 1 | 2 | [] |              
| ---|------|----|       
| ---|------|----|              
| ---|------|----|       ",

/* 7 */

@"| ---|----|-----|       
| ---|--df----|-----|       
| -d--|------|----|        
| ---|------|----|        ",

/* 8 */

@"| a  | b | c | e | f |       
| --|-- |---|--|---|---|       
| --|-- |---|--|---|---|       ",

/* 8a */

@"| a  | b | 
| -0-|-$- |
| -1-|-$- |
| -2-|-$- |",

@"| a | b | c |       
| -0-|-$- | -- |       
| -1-|-$- | -- |       
| -2-|-$- | -- |       ",


/* 9 */

@"|---|----|
| ---|----|
| ---|----|
| ---|----|
| ---|----|
| ---|----|",

@"|---|----|----|----|
  |---|----|----|----|",

/* 10 */

@"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text 
ever since the 1500s, when an unknown printer took a 
galley of type and scrambled",

/* 11 */

@"1 2 3 4 5 6 7 9",

/* 12 */

@"{""id"" : ""0""}"

        };


        public static double[][] Answer = new double[][]
        {
            /* With Header */

            new double[] { 3, 3, 7 },

            new double[] { 3, 3, 7 },

            new double[] { 3, 3, 7 },

            new double[] { 3, 3, 0 },

            /* 0 */

            new double[] { 3, 3, 7 },

            /* 1 */

           new double[] { 3, 3, 7 },

            /* 2 */

            new double[] { 2, 3, 7 },

            /* 3 */

            new double[] { 4, 3, 7 },

            /* 4 */

           new double[] { 6, 3, 0 },
           new double[] { 4, 3, 0 },
           new double[] { 7, 3, 0 },
           new double[] { 9, 3, 0 },

            /* 5 */

            new double[] { 4, 3, 7 },

            /* 6 */

            new double[] { 3, 3, 7 },
                        
            /* 7 */

            new double[] { 3, 3, 0 },

            /* 8 */

            new double[] { 5, 3, 7 },

            /* 8a */

            new double[] { 2, 3, 7 },
            new double[] { 3, 3, 7 },

            /* 9 */

            new double[] { 2, 3, 0 },
            new double[] { 4, 3, 0 },

           /* 10 */

            new double[] { 0, 0, 0 },

            /* 11 */

            new double[] { 0, 0, 0 },

            /* 12 */

            new double[] { 0, 0, 0 }

        };
        
        static Dots.Dot[] Vector(params double[] source)
        {
            Dots.Dot[] vec = new Dots.Dot[source.Length];

            for (var i = 0; i < vec.Length; i++)
            {
                var scalar = source[i] / (10 * 1.0);

                vec[i] = new Dots.Dot()
                {
                    y = scalar
                };
            }

            return vec;
        }

        static Dots.Dot[] Vector(string source)
        {
            Dots.Dot[] vec = new Dots.Dot[source.Length];

            for (var i = 0; i < vec.Length; i++)
            {
                var scalar = source[i] / (1000 * 1.0);

                vec[i] = new Dots.Dot()
                {
                    y = scalar
                };
            }

            return vec;
        }

        static string ToString(Dots.Dot[] source)
        {
            char[] vec = new char[source.Length];

            for (var i = 0; i < vec.Length; i++)
            {
                vec[i] = (char)(source[i].y * 1000);
            }

            return new string(vec);
        }

        static void Test(Dots.Dot[] Y, Dots.Dot[][] H)
        {
            Console.WriteLine("\r\n**************************\r\n");

            Dots.Dot[] X; int M;

            for (var c = 0; c < 5; c++)
            {
                M = Dots.Dot.random(Seen.Length);

                X = Vector(Seen[M]);

                Console.WriteLine(ToString(X));
                Console.WriteLine();

                Dots.compute(Y, H, X);

                for (int l = 0; l < H.Length; l++)
                {
                    // Inspect($"H{l}", H[l], ConsoleColor.DarkGray);
                }

                if (Y != null)
                {
                    Inspect("Y", Y, ConsoleColor.Yellow);
                }

                var A = Vector(Answer[M]);

                if (Y != null)
                {
                    Inspect("A", A, ConsoleColor.Green);
                }

                Console.WriteLine();
                Console.WriteLine();
            }

            for (var c = 0; c < Unseen.Length; c++)
            {
                M = Dots.Dot.random(Unseen.Length);

                X = Vector(Unseen[M]);

                Console.WriteLine(ToString(X));
                Console.WriteLine();

                Dots.compute(Y, H, X);

                for (int l = 0; l < H.Length; l++)
                {
                    // Inspect($"H{l}", H[l], ConsoleColor.DarkGray);
                }

                if (Y != null)
                {
                    Inspect("Y", Y, ConsoleColor.Green);
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }

        static void Train(Func<double> α, ref Dots.Dot[] Y, 
            Dots.Dot[][] H, Func<int, Dots.Dot[]> X, Func<int, Dots.Dot[]> T, int MAX, int episodes,
            Func<int, Dots.Dot[], double, int> epoch)
        {            
            for (int episode = 0; episode < episodes; episode++)
            {
                int M = Dots.Dot.random(MAX);

                var x = X(M); var t = T(M);                 

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
    
        static void Main(string[] args)
        {
            bool canceled = false;

            Dots.Dot[][] H = new Dots.Dot[][]
            {
                // Dots.create(7)
            };

            Dots.Dot[] Y = null;

            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = canceled = true;
            };

            Test(Y, H);

            double E = 0.0; double S = 0.0; double J = 0.0; double D = 0.0; double R = 0.0;

            Train(
                
                // Learning rate

                () => 0.001,

                // Output vector

                ref Y,

                // Hidden layers

                H,
                
                (M)=> 
                {
                    return Vector(Seen[M]);
                },

                (M) =>
                {
                    var A = Answer[M];

                    var V = new double[] { A[0] };

                    return Vector(A);
                },
                 
                Seen.Length,

                32 * 32 * 1024,

                (episode, X, error)=>
                {
                    E += error * error * (episode + 1);

                    S += E * E * (episode + 1);

                    J += S * S * (episode + 1);

                    D += J * J * (episode + 1);

                    R += D * D * (episode + 1);

                    if (double.IsNaN(D) || double.IsInfinity(D))
                    {
                        return int.MaxValue - 1;
                    }

                    // Error Wheel, Gears

                    Console.WriteLine($"{1 / R} | {1 / D} | {1 / J} | {1 / S} | {1 / E}");

                    if (canceled)
                    {
                        episode = int.MaxValue - 1;
                    }

                    return episode;
                }

            );

            Test(Y, H);

            Console.ReadKey();
        }
    }
}