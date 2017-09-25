using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Recipes
{
    public class Dict : Grammar
    {
        public Dict()
        {
        }

        bool _Loaded;

        public void Load()
        {
            if (this._Loaded)
            {
                return;
            }

            DictionaryPath = "..\\";

            LOAD();

            this._Loaded = true;
        }

        public StringBuilder GetHTML(string q, bool search)
        {
            if (q != null)
            {
                q = q.Trim();
            }
            char letter = 'a';
            StringBuilder HTML = new StringBuilder();
            Dictionary<Word, Node> keyValueList = SearchWords(q);
            if (keyValueList != null)
            {
                foreach (KeyValuePair<Word, Node> i in keyValueList)
                {
                    string arg_8A_1 = "({0}.) {1}";
                    char expr_6C = letter;
                    letter++;
                    HTML.AppendFormat(arg_8A_1, expr_6C, Formatter.GetDeclaration(i.Key, i.Value, null));
                    HTML.Append("<p>");
                    HTML.Append(Formatter.GetDefinition(i.Key));
                    HTML.Append("</p>");
                    Node node = i.Value;
                    bool added = false;

                    while (node != null)
                    {
                        if (node.Attributes != Attributes.None && Converter.Equals(q, node.Stem, node.Suffix))
                        {
                            HTML.AppendFormat("<span>{0}</span>", Formatter.GetLong(node.Stem, node.Suffix, i.Key.Flags, node.Attributes, false));
                            HTML.Append("<br/>");
                            added = true;
                        }
                        node = node.Next;
                    }

                    if (added)
                    {
                        HTML.Append("<br/>");
                    }
                }
            }
            return HTML;
        }
    }

    public static class Web
    {
        public static string MD5(string data, System.Text.Encoding encoding = null)
        {
            StringBuilder buidler = new StringBuilder();
            if (data == null)
            {
                data = "";
            }
            if (encoding == null)
            {
                encoding = System.Text.Encoding.UTF8;
            }
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            try
            {
                byte[] hashBytes = md5.ComputeHash(encoding.GetBytes(data));
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    buidler.Append(hashBytes[i].ToString("x2"));
                }
                return buidler.ToString().ToLower();
            }
            finally
            {
                md5.Dispose();
            }
        }

        public static ISet<string> Download(string path, string[] urls)
        {
            var files = new HashSet<string>();

            foreach (var url in urls)
            {
                Uri uri = new Uri(url);

                string host = $"{uri.Scheme}://{uri.Host}:{uri.Port}";

                string html = null;

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var local = Path.Combine(path, $"{MD5(uri.ToString())}.cache");

                if (File.Exists(local))
                {
                    html = File.ReadAllText(local);
                }
                else
                {
                    html = Get(host, $"{uri.PathAndQuery}", "GET", null);

                    File.WriteAllText(local, html);
                }

                // Console.WriteLine($"{host}{uri.PathAndQuery}");

                files.Add(local); 
            }

            return files;
        }

        public static string Get(string host, string path, string method, string data)
        {

            Exception e; String r = Get(host, path, method, null, data, out e);

            if (e != null)
            {
                Console.Error?.WriteLine(e.ToString());
                return null;
            }

            if (r == null)
            {
                return String.Empty;
            }

            return r;
        }

        public static string Get(string host, string path, string method,
            string contentType, string data, out Exception e)
        {
            String url = host;

            e = null;

            while (url != null && url.EndsWith("/"))
            {
                url = url.Remove(url.Length - 1);
            }

            try
            {                
                if (path != null)
                {
                    path = path.Trim();
                }

                if (String.IsNullOrWhiteSpace(path))
                {
                    throw new ArgumentNullException(nameof(path));
                }

                while (path != null && path.StartsWith("/"))
                {
                    path = path.Remove(0, 1);
                }

                var bytes = data != null ? Encoding.UTF8.GetBytes(data) : null;

                var request = (HttpWebRequest)WebRequest.Create(url + "/" + path);

                request.Method = method;

                if (contentType == "json")
                {
                    request.ContentType = "application/json";
                }
                else if (!string.IsNullOrWhiteSpace(contentType))
                {
                    request.ContentType = contentType;
                }

                if (bytes != null && bytes.Length > 0)
                {
                    request.ContentLength = bytes.Length;

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }
                else
                {
                    request.ContentLength = 0;
                }

                var response = (HttpWebResponse)request.GetResponse();

                try
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream))
                        {

                            data = reader.ReadToEnd();

                            if (data == null)
                            {
                                return String.Empty;
                            }

                            e = null;

                            return data;
                        }
                    }
                }
                finally
                {
                    response.Dispose();
                }

            }
            catch (Exception innerError)
            {
                e = innerError;

                try
                {
                    if (e is WebException)
                    {
                        using (Stream stream = ((WebException)e).Response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream))
                            {
                                string msg = reader.ReadToEnd();

                                if (!string.IsNullOrWhiteSpace(msg))
                                {
                                    e = new WebException(
                                        msg,
                                        e,
                                        ((WebException)e).Status,
                                        ((WebException)e).Response
                                    );
                                }
                            }
                        }
                    }
                }

                catch
                {
                }

                return null;
            }
        }
    }

    public static class Words
    {
        static bool IsEven(int value)
        {
            if (value % 2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static char[] Borders = { '|' };

        static char[] Spaces = { '-', ' ', ' ', '\t', '$', '*' };

        static string Grid(ref int columns, ref int rows, ref int spacing, ref char fill, ref char border)
        {
            columns = Dots.Dot.random(10);
            rows = Dots.Dot.random(10);

            spacing = Dots.Dot.random(3);

            fill = Spaces[Dots.Dot.random(Borders.Length)];

            border = Borders[Dots.Dot.random(Borders.Length)];

            return Grid(
                    columns,
                    rows,
                    spacing,
                    fill,
                    border
            );
        }

        static string Grid(int columns, int rows, int spacing, char fill, char border)
        {

            StringBuilder sb = new StringBuilder();

            for (int r = 0; r < rows; r++)
            {
                if (sb.Length > 0)
                {
                    sb.Append("\r\n");
                }

                for (int c = 0; c < columns; c++)
                {
                    sb.Append(border);

                    for (var i = 0; i < spacing; i++)
                    {
                        sb.Append(fill);
                    }

                    if (c == columns - 1)
                    {
                        sb.Append(border);
                    }
                }
            }

            return sb.ToString();
        }

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
                int columns = 0;
                int rows = 0;
                int spacing = 0;
                char fill = '\0';
                char border = '\0';

                var grid = Grid(ref columns, ref rows, ref spacing, ref fill, ref border);

                X = Vector(grid);

                Console.WriteLine(ToString(X));
                Console.WriteLine();

                Dots.compute(Y, H, X);

                for (int l = 0; l < H.Length; l++)
                {
                    // Inspect($"H{l}", H[l], ConsoleColor.DarkGray);
                }

                if (Y != null)
                {
                    Inspect("X", X, ConsoleColor.Green);
                }

                if (Y != null)
                {
                    Inspect("Y", Y, ConsoleColor.Yellow);
                }

                if (Y != null)
                {
                    var grid2 = ToString(Y);
                    Console.WriteLine(grid2);
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
                int columns = 0;
                int rows = 0;
                int spacing = 0;
                char fill = '\0';
                char border = '\0';

                var grid = Grid(ref columns, ref rows, ref spacing, ref fill, ref border);

                var x = Vector(grid);

                var t = Vector(columns * rows * (20 * 1.0), columns / (10 * 1.0), rows / (10 * 1.0));

                double E = Dots.sgd(x, ref Y, H, x, α());

                if (E <= double.Epsilon || double.IsNaN(E) || double.IsInfinity(E))
                {
                    //    break;
                }

                if (epoch != null)
                {
                    episode = epoch(episode, x, E);
                }
            }
        }


        static class Library
        {
            public static string[] DeBello = new string[]
                {
                "http://www.thelatinlibrary.com/caesar/gall1.shtml",
                "http://www.thelatinlibrary.com/caesar/gall2.shtml",
                "http://www.thelatinlibrary.com/caesar/gall3.shtml",
                "http://www.thelatinlibrary.com/caesar/gall4.shtml",
                "http://www.thelatinlibrary.com/caesar/gall5.shtml",
                "http://www.thelatinlibrary.com/caesar/gall6.shtml",
                "http://www.thelatinlibrary.com/caesar/gall7.shtml",
                "http://www.thelatinlibrary.com/caesar/gall8.shtml"
                };

            public static string[] Epistulae = new string[]
                {
                "http://www.thelatinlibrary.com/sen/seneca.ep1.shtml",
                "http://www.thelatinlibrary.com/sen/seneca.ep2.shtml",
                "http://www.thelatinlibrary.com/sen/seneca.ep3.shtml",
                "http://www.thelatinlibrary.com/sen/seneca.ep4.shtml",
                "http://www.thelatinlibrary.com/sen/seneca.ep5.shtml",
                "http://www.thelatinlibrary.com/sen/seneca.ep6.shtml",
                "http://www.thelatinlibrary.com/sen/seneca.ep7.shtml",
                "http://www.thelatinlibrary.com/sen/seneca.ep8.shtml",
                "http://www.thelatinlibrary.com/sen/seneca.ep10.shtml",
                "http://www.thelatinlibrary.com/sen/seneca.ep11-13.shtml",
                "http://www.thelatinlibrary.com/sen/seneca.ep14-15.shtml",
                "http://www.thelatinlibrary.com/sen/seneca.ep16.shtml",
                "http://www.thelatinlibrary.com/sen/seneca.ep17-18.shtml",
                "http://www.thelatinlibrary.com/sen/seneca.ep19.shtml",
                "http://www.thelatinlibrary.com/sen/seneca.ep20.shtml",
                };
            public static string[] Questiones = new string[]
                {
                "http://www.thelatinlibrary.com/sen/sen.qn1.shtml",
                "http://www.thelatinlibrary.com/sen/sen.qn2.shtml",
                "http://www.thelatinlibrary.com/sen/sen.qn3.shtml",
                "http://www.thelatinlibrary.com/sen/sen.qn4.shtml",
                "http://www.thelatinlibrary.com/sen/sen.qn5.shtml",
                "http://www.thelatinlibrary.com/sen/sen.qn6.shtml",
                "http://www.thelatinlibrary.com/sen/sen.qn7.shtml",
                };

            public static string[] DeIra = new string[]
            {
                "http://www.thelatinlibrary.com/sen/sen.ira1.shtml",
                "http://www.thelatinlibrary.com/sen/sen.ira2.shtml",
                "http://www.thelatinlibrary.com/sen/sen.ira3.shtml"
            };

            public static string[] Ovid = new string[]
            {
                "http://www.thelatinlibrary.com/ovid/ovid.met1.shtml",
                "http://www.thelatinlibrary.com/ovid/ovid.met2.shtml",
                "http://www.thelatinlibrary.com/ovid/ovid.met3.shtml",
                "http://www.thelatinlibrary.com/ovid/ovid.met4.shtml",
                "http://www.thelatinlibrary.com/ovid/ovid.met5.shtml",
                "http://www.thelatinlibrary.com/ovid/ovid.met6.shtml",
                "http://www.thelatinlibrary.com/ovid/ovid.met7.shtml",
                "http://www.thelatinlibrary.com/ovid/ovid.met8.shtml",
                "http://www.thelatinlibrary.com/ovid/ovid.met9.shtml",
                "http://www.thelatinlibrary.com/ovid/ovid.met10.shtml",
                "http://www.thelatinlibrary.com/ovid/ovid.met11.shtml",
                "http://www.thelatinlibrary.com/ovid/ovid.met12.shtml",
                "http://www.thelatinlibrary.com/ovid/ovid.met13.shtml",
                "http://www.thelatinlibrary.com/ovid/ovid.met14.shtml",
                "http://www.thelatinlibrary.com/ovid/ovid.met15.shtml",
            };

            public static string[] AdFamiliares = new string[]
            {
                "http://www.thelatinlibrary.com/cicero/fam1.shtml",
                "http://www.thelatinlibrary.com/cicero/fam2.shtml",
                "http://www.thelatinlibrary.com/cicero/fam3.shtml",
                "http://www.thelatinlibrary.com/cicero/fam4.shtml",
                "http://www.thelatinlibrary.com/cicero/fam5.shtml",
                "http://www.thelatinlibrary.com/cicero/fam6.shtml",
                "http://www.thelatinlibrary.com/cicero/fam7.shtml",
                "http://www.thelatinlibrary.com/cicero/fam8.shtml",
                "http://www.thelatinlibrary.com/cicero/fam9.shtml",
                "http://www.thelatinlibrary.com/cicero/fam10.shtml",
                "http://www.thelatinlibrary.com/cicero/fam11.shtml",
                "http://www.thelatinlibrary.com/cicero/fam12.shtml",
                "http://www.thelatinlibrary.com/cicero/fam13.shtml",
                "http://www.thelatinlibrary.com/cicero/fam14.shtml",
                "http://www.thelatinlibrary.com/cicero/fam15.shtml",
                "http://www.thelatinlibrary.com/cicero/fam16.shtml",
            };           
            
            public static string[] Philippica = new string[]
            {
                "http://www.thelatinlibrary.com/cicero/phil1.shtml",
                "http://www.thelatinlibrary.com/cicero/phil2.shtml",
                "http://www.thelatinlibrary.com/cicero/phil3.shtml",
                "http://www.thelatinlibrary.com/cicero/phil4.shtml",
                "http://www.thelatinlibrary.com/cicero/phil5.shtml",
                "http://www.thelatinlibrary.com/cicero/phil6.shtml",
                "http://www.thelatinlibrary.com/cicero/phil7.shtml",
                "http://www.thelatinlibrary.com/cicero/phil8.shtml",
                "http://www.thelatinlibrary.com/cicero/phil9.shtml",
                "http://www.thelatinlibrary.com/cicero/phil10.shtml",
                "http://www.thelatinlibrary.com/cicero/phil11.shtml",
                "http://www.thelatinlibrary.com/cicero/phil12.shtml",
                "http://www.thelatinlibrary.com/cicero/phil13.shtml",
                "http://www.thelatinlibrary.com/cicero/phil14.shtml",
            };

            public static string[] Varro = new string[]
            {
                "http://www.thelatinlibrary.com/varro.rr1.html",
                "http://www.thelatinlibrary.com/varro.rr2.html",
                "http://www.thelatinlibrary.com/varro.rr3.html",
                "http://www.thelatinlibrary.com/varro.ll5.html",
                "http://www.thelatinlibrary.com/varro.ll6.html",
                "http://www.thelatinlibrary.com/varro.ll7.html",
                "http://www.thelatinlibrary.com/varro.ll8.html",
                "http://www.thelatinlibrary.com/varro.ll9.html",
                "http://www.thelatinlibrary.com/varro.ll10.html",
            };

            public static string[] Gellius = new string[]
            {
                "http://www.thelatinlibrary.com/gellius/gellius1.shtml",
                "http://www.thelatinlibrary.com/gellius/gellius2.shtml",
                "http://www.thelatinlibrary.com/gellius/gellius3.shtml",
                "http://www.thelatinlibrary.com/gellius/gellius4.shtml",
                "http://www.thelatinlibrary.com/gellius/gellius5.shtml",
                "http://www.thelatinlibrary.com/gellius/gellius6.shtml",
                "http://www.thelatinlibrary.com/gellius/gellius7.shtml",
                "http://www.thelatinlibrary.com/gellius/gellius8.shtml",
                "http://www.thelatinlibrary.com/gellius/gellius9.shtml",
                "http://www.thelatinlibrary.com/gellius/gellius10.shtml",
                "http://www.thelatinlibrary.com/gellius/gellius11.shtml",
                "http://www.thelatinlibrary.com/gellius/gellius12.shtml",
                "http://www.thelatinlibrary.com/gellius/gellius13.shtml",
            };

            public static string[] Empty = new string[]
            {
            };


            public static string[] Livy = new string[]
            {
                "http://www.thelatinlibrary.com/livy/liv.1.shtml",
                "http://www.thelatinlibrary.com/livy/liv.2.shtml",
                "http://www.thelatinlibrary.com/livy/liv.3.shtml",
                "http://www.thelatinlibrary.com/livy/liv.4.shtml",
                "http://www.thelatinlibrary.com/livy/liv.5.shtml",
                "http://www.thelatinlibrary.com/livy/liv.6.shtml",
                "http://www.thelatinlibrary.com/livy/liv.7.shtml",
                "http://www.thelatinlibrary.com/livy/liv.8.shtml",
                "http://www.thelatinlibrary.com/livy/liv.9.shtml",
                "http://www.thelatinlibrary.com/livy/liv.10.shtml",
                "http://www.thelatinlibrary.com/livy/liv.11.shtml",
                "http://www.thelatinlibrary.com/livy/liv.12.shtml",
                "http://www.thelatinlibrary.com/livy/liv.13.shtml",
                "http://www.thelatinlibrary.com/livy/liv.14.shtml",
                "http://www.thelatinlibrary.com/livy/liv.15.shtml",
                "http://www.thelatinlibrary.com/livy/liv.16.shtml",
                "http://www.thelatinlibrary.com/livy/liv.17.shtml",
                "http://www.thelatinlibrary.com/livy/liv.18.shtml",
                "http://www.thelatinlibrary.com/livy/liv.19.shtml",
                "http://www.thelatinlibrary.com/livy/liv.20.shtml",
                "http://www.thelatinlibrary.com/livy/liv.21.shtml",
                "http://www.thelatinlibrary.com/livy/liv.22.shtml",
                "http://www.thelatinlibrary.com/livy/liv.23.shtml",
                "http://www.thelatinlibrary.com/livy/liv.24.shtml",
                "http://www.thelatinlibrary.com/livy/liv.25.shtml",
                "http://www.thelatinlibrary.com/livy/liv.26.shtml",
                "http://www.thelatinlibrary.com/livy/liv.27.shtml",
                "http://www.thelatinlibrary.com/livy/liv.28.shtml",
                "http://www.thelatinlibrary.com/livy/liv.29.shtml",
                "http://www.thelatinlibrary.com/livy/liv.30.shtml",
                "http://www.thelatinlibrary.com/livy/liv.31.shtml",
                "http://www.thelatinlibrary.com/livy/liv.32.shtml",
                "http://www.thelatinlibrary.com/livy/liv.33.shtml",
                "http://www.thelatinlibrary.com/livy/liv.34.shtml",
                "http://www.thelatinlibrary.com/livy/liv.35.shtml",
                "http://www.thelatinlibrary.com/livy/liv.36.shtml",
                "http://www.thelatinlibrary.com/livy/liv.37.shtml",
                "http://www.thelatinlibrary.com/livy/liv.38.shtml",
                "http://www.thelatinlibrary.com/livy/liv.39.shtml",
                "http://www.thelatinlibrary.com/livy/liv.40.shtml",
                "http://www.thelatinlibrary.com/livy/liv.41.shtml",
                "http://www.thelatinlibrary.com/livy/liv.42.shtml",
                "http://www.thelatinlibrary.com/livy/liv.43.shtml",
                "http://www.thelatinlibrary.com/livy/liv.44.shtml",
                "http://www.thelatinlibrary.com/livy/liv.45.shtml"
            };

            public static string[] Donatus = new string[]
            {
                "http://www.thelatinlibrary.com/don.html"
            };

            public static string[] Quintilian = new string[]
            {
                "http://www.thelatinlibrary.com/quintilian/quintilian.institutio1.shtml",
                "http://www.thelatinlibrary.com/quintilian/quintilian.institutio2.shtml",
                "http://www.thelatinlibrary.com/quintilian/quintilian.institutio3.shtml",
                "http://www.thelatinlibrary.com/quintilian/quintilian.institutio4.shtml",
                "http://www.thelatinlibrary.com/quintilian/quintilian.institutio5.shtml",
                "http://www.thelatinlibrary.com/quintilian/quintilian.institutio6.shtml",
                "http://www.thelatinlibrary.com/quintilian/quintilian.institutio7.shtml",
                "http://www.thelatinlibrary.com/quintilian/quintilian.institutio8.shtml",
                "http://www.thelatinlibrary.com/quintilian/quintilian.institutio9.shtml",
                "http://www.thelatinlibrary.com/quintilian/quintilian.institutio10.shtml",
                "http://www.thelatinlibrary.com/quintilian/quintilian.institutio11.shtml",
                "http://www.thelatinlibrary.com/quintilian/quintilian.institutio12.shtml",
            }; 

            public static string[] Apuleus = new string[]
            {
                "http://www.thelatinlibrary.com/apuleius/apuleius1.shtml",
                "http://www.thelatinlibrary.com/apuleius/apuleius2.shtml",
                "http://www.thelatinlibrary.com/apuleius/apuleius3.shtml",
                "http://www.thelatinlibrary.com/apuleius/apuleius4.shtml",
                "http://www.thelatinlibrary.com/apuleius/apuleius5.shtml",
                "http://www.thelatinlibrary.com/apuleius/apuleius6.shtml",
                "http://www.thelatinlibrary.com/apuleius/apuleius7.shtml",
                "http://www.thelatinlibrary.com/apuleius/apuleius8.shtml",
                "http://www.thelatinlibrary.com/apuleius/apuleius9.shtml",
                "http://www.thelatinlibrary.com/apuleius/apuleius10.shtml",
                "http://www.thelatinlibrary.com/apuleius/apuleius11.shtml",
            };

            public static string[] Iuvenalis = new string[]
            {
                "http://www.thelatinlibrary.com/juvenal/1.shtml",
                "http://www.thelatinlibrary.com/juvenal/2.shtml",
                "http://www.thelatinlibrary.com/juvenal/3.shtml",
                "http://www.thelatinlibrary.com/juvenal/4.shtml",
                "http://www.thelatinlibrary.com/juvenal/5.shtml",
                "http://www.thelatinlibrary.com/juvenal/6.shtml",
                "http://www.thelatinlibrary.com/juvenal/7.shtml",
                "http://www.thelatinlibrary.com/juvenal/8.shtml",
                "http://www.thelatinlibrary.com/juvenal/9.shtml",
                "http://www.thelatinlibrary.com/juvenal/10.shtml",
                "http://www.thelatinlibrary.com/juvenal/11.shtml",
                "http://www.thelatinlibrary.com/juvenal/12.shtml",
                "http://www.thelatinlibrary.com/juvenal/13.shtml",
                "http://www.thelatinlibrary.com/juvenal/14.shtml",
                "http://www.thelatinlibrary.com/juvenal/15.shtml",
                "http://www.thelatinlibrary.com/juvenal/16.shtml",
            };

        }

        static Dict _Dict;

        static void Main(string[] args)
        {
            if (_Dict == null)
            {
                _Dict = new Dict();
                _Dict.Load();
            }

            string path = "..\\data";

            var CORPUS = new HashSet<string>();

            Merge(Web.Download(path, Library.DeBello), CORPUS);

            Merge(Web.Download(path, Library.Epistulae), CORPUS);
            Merge(Web.Download(path, Library.DeIra), CORPUS);
            Merge(Web.Download(path, Library.Questiones), CORPUS);

            Merge(Web.Download(path, Library.AdFamiliares), CORPUS);
            Merge(Web.Download(path, Library.Philippica), CORPUS);
            Merge(Web.Download(path, Library.Livy), CORPUS);

            Merge(Web.Download(path, Library.Iuvenalis), CORPUS);
            Merge(Web.Download(path, Library.Apuleus), CORPUS);

            Merge(Web.Download(path, Library.Varro), CORPUS);
            Merge(Web.Download(path, Library.Gellius), CORPUS);
            Merge(Web.Download(path, Library.Quintilian), CORPUS);
            Merge(Web.Download(path, Library.Donatus), CORPUS);

            var Excludes = new HashSet<string>(
                new string[] {
                    "The",
                    "EN",
                    "C",
                    "poetry",
                    "latin",
                    "library",
                    "www",
                    "doc",
                    "<",
                    ">",
                    "title",
                    "font",
                    "name",
                    "p",
                    "html",
                    "div",
                    "body",
                    "i",
                    "b",
                    "a",
                    "o",
                    "li",
                    "ul",
                    "ol",
                    "span",
                    "style",
                    "script",
                    "head",
                    "http",
                    "font",
                    "size",
                    "class",
                    "media",
                    "BR",
                    "FONT",
                    "nbsp",
                    "href",
                    "center",
                    "border",
                    "smallborder",
                    "align",
                    "tr",
                    "td",
                    "dir",
                },

                StringComparer.OrdinalIgnoreCase
            );

            ISet<string> B = new HashSet<string>();

            IDictionary<string, Language.Word> W = new Dictionary<string, Language.Word>(StringComparer.InvariantCultureIgnoreCase);

            foreach (var file in CORPUS)
            {
                Language.Sum(Language.Words(File.ReadAllText(file)), ref W, 7, _Dict);
            }            
            
            int C = 0;

            foreach (var i in W.OrderBy(k => k.Value.Score).Skip(0).Take(int.MaxValue))
            {
                if (Excludes.Contains(i.Key) || i.Value.Count < 1 || i.Key.Length < 2)
                {
                    continue;
                }

                var Q = Language.Syllabify(i.Value.Key);

                if (Q.Count <= 0)
                {
                    continue;
                }

                foreach (var q in Q)
                {
                    B.Add(q);
                }

                Func<KeyValuePair<string, Language.Word.Summary>, bool> where = (k) =>
                {
                    return k.Key.Length > 1 && k.Value.Count > 0 && !Excludes.Contains(k.Key);
                };

                var P = i.Value.Window.Where(where).OrderByDescending(k => k.Value.Count).Select((k) => { return k.Key + " (" + k.Value.Count + ")"; }).Take(337);
                 
                if (P.Count() > 0)
                {
                    Console.WriteLine($"*{string.Join(".", Q)} ({i.Value.Count}):{i.Value.Score} [{string.Join(",", P)}]:{P.Count()}");
                    C++;
                }
            }

            Console.WriteLine($"\r\n{C} - Total Words");

            var I = B.OrderBy(s => s);

            foreach (var i in I)
            {
                // Console.WriteLine(i);
            } 

            // Console.ReadKey();
        }

        private static void Merge(IEnumerable<string> P, HashSet<string> Merge)
        {
            foreach (var s in P)
            {
                Merge.Add(s);
            }
        }

        public class Language
        {
            public class Word
            {
                Grammar.Word[] _lemas;

                public Grammar.Word[] GetLemas(Dict dict)
                {
                    if (_lemas != null)
                    {
                        return _lemas;
                    }

                    HashSet<Grammar.Word> list = new HashSet<Grammar.Word>();

                    if (dict != null)
                    {
                        Grammar.Node n = dict.Equals(Key);

                        while (n != null)
                        {

                            list.Add(dict.ENTRIES[n.Entry]);

                            n = n.Next;
                        }
                    }

                    return _lemas = list.ToArray();
                }

                List<string> _syllables;

                public List<string> Syllables
                {
                    get
                    {
                        return _syllables;
                    }
                }

                double _score;

                public double Score
                {
                    get
                    {
                        return _score;
                    }
                }

                public static double Sigmoid(double value)
                {
                    return 1 / (1 + Math.Exp(-value));
                }

                public Word(string key)
                {
                    _syllables = Syllabify(key);

                    _score = 0;

                    for (int i = 0; i < key.Length; i++)
                    {
                        _score += Math.Sin(key[i]) * Math.Sin(key[i]);
                    }

                    if (key.Length > 0)
                    {
                        _score /= (key.Length * 1.0);
                    }

                    _key = key;

                    _ante = new Dictionary<string, Summary>(StringComparer.InvariantCultureIgnoreCase);
                    _post = new Dictionary<string, Summary>(StringComparer.InvariantCultureIgnoreCase);
                    _window = new Dictionary<string, Summary>(StringComparer.InvariantCultureIgnoreCase);
                }

                public override string ToString()
                {
                    return _key.ToString();
                }

                public override int GetHashCode()
                {
                    return _key.GetHashCode();
                }

                public override bool Equals(object obj)
                {
                    if (object.ReferenceEquals(this, obj))
                    {
                        return true;
                    }

                    Word w = obj as Word;

                    if (w == null)
                    {
                        return false;
                    }

                    return _key.Equals(w._key);
                }

                string _key;

                public string Key
                {
                    get
                    {
                        return _key;
                    }
                }

                public class Summary
                {
                    int _count;

                    public int Count
                    {
                        get
                        {
                            return _count;
                        }
                        set
                        {
                            _count = value;
                        }
                    }

                    public override string ToString()
                    {
                        return Count.ToString();
                    }
                }

                IDictionary<string, Summary> _ante;

                public IDictionary<string, Summary> Ante
                {
                    get
                    {
                        return _ante;
                    }
                }

                IDictionary<string, Summary> _post;

                public IDictionary<string, Summary> Post
                {
                    get
                    {
                        return _post;
                    }
                }

                IDictionary<string, Summary> _window;

                public IDictionary<string, Summary> Window
                {
                    get
                    {
                        return _window;
                    }
                }

                int _count;

                public int Count
                {
                    get
                    {
                        return _count;
                    }
                    set
                    {
                        _count = value;
                    }
                }
            }

            public static bool IsDiphthong(char a, char e)
            {
                switch (a)
                {
                    case 'a':
                        return e == 'e' || e == 'u';
                    case 'o':
                        return e == 'e' || e == 'u';
                }

                return false;
            }

            public static List<string> Syllabify(string word)
            {
                List<string> S = new List<string>();

                var reader = 0; var len = word.Length; var c = '\0';

                Func<char> next = () =>
                {
                    if (reader < len)
                    {
                        var t = char.ToLowerInvariant(Correct(word[reader]));

                        reader++;

                        return c = t;
                    }

                    return c = '\0';
                };

                next();

L0:
                StringBuilder fragment = null;

                Action<char> take = (what) =>
                {
                    if (fragment == null)
                    {
                        fragment = new StringBuilder();
                    }

                    fragment.Append(what);

                    next();
                };

                fragment = new StringBuilder();

                while (IsConsonant(c))
                {
                    var q = c;

                    take(c);

                    if (c == 'u' && (q == 'q' || q == 'g'))
                    {
                        take(c);
                    }
                }

                if (IsVowel(c))
                {
                    var a = c;

                    take(c);
                    
                    var p = '\0';

                    if (reader < len)
                    {
                        p = word[reader];
                    }

                    if (a == 'u' && IsVowel(c) && reader == 2)
                    {
                        take(c);
                    }

                    // In-between vowels

                    else if (c == 'i' || c == 'b' || c == 'u')
                    {
                        if (IsVowel(p) || IsDiphthong(a, c))
                        {
                            take(c);
                        }
                    }

                    // diphthong

                    else if (IsDiphthong(a, c))
                    {
                        take(c);
                    }
                }

                if (IsConsonant(c))
                {
                    var p = '\0';

                    if (reader < len)
                    {
                        p = word[reader];
                    }

                    if (IsDoubleConsonant(c) || IsConsonant(p))
                    {
                        take(c);
                    }
                }

                if (fragment.Length > 0)
                {
                    S.Add(fragment.ToString());
                    goto L0;
                }

                if (S.Count > 0)
                {
                    var last = S[S.Count - 1];

                    S.RemoveAt(S.Count - 1);

                    if (AreAllConsonants(last))
                    {
                        if (S.Count > 0)
                        {
                            S[S.Count - 1] += last;
                        }
                        else
                        {
                            S.Add(last);
                        }
                    }
                    else
                    {
                        S.Add(last);
                    }
                }

                if (S.Count <= 0)
                {
                    S.Add(word);
                }

                return S;
            }

            public static Word ToWord(string w, ref IDictionary<string, Word> W)
            {
                Word s;

                if (W.ContainsKey(w))
                {
                    s = W[w];
                }
                else
                {
                    s = W[w] = new Word(w);
                }

                return s;
            }

            public static IDictionary<string, Word> Sum(IList<string> text, ref IDictionary<string, Word> W, int size, Dict dict)
            {
                if (W == null) W = new Dictionary<string, Word>(StringComparer.InvariantCultureIgnoreCase);

                for (int i = 0; i < text.Count; i++)
                {
                    var w = text[i];

                    Word s = ToWord(w, ref W);

                    s.Count++;

                    List<string> window = new List<string>();
                    
                    for (var j = i - size / 2; j < i + size / 2; j++)
                    {
                        if (j < 0 || j >= text.Count)
                        {
                            continue;
                        }

                        Word tmp = ToWord(text[j], ref W);
                         
                        foreach (var l in tmp.GetLemas(dict))
                        {
                            if ((l.Speach == Grammar.Speach.Verb || l.Speach == Grammar.Speach.Adverb ||
                                 l.Speach == Grammar.Speach.Noun || l.Speach == Grammar.Speach.Ajective) && l.Stems.Length > 0)
                            {
                                Grammar.Node TOP = null;

                                if (l.Speach == Grammar.Speach.Noun || l.Speach == Grammar.Speach.Verb)
                                {
                                    dict.INFLECT(l, ref TOP, true);
                                }

                                while (TOP != null)
                                {
                                    if (((TOP.Attributes & Grammar.Attributes.Nominative) == Grammar.Attributes.Nominative)
                                                                        || ((TOP.Attributes & Grammar.Attributes.Infinitive) == Grammar.Attributes.Infinitive))
                                    {
                                        window.Add(TOP.Stem + TOP.Suffix);
                                    }

                                    TOP = TOP.Next;
                                }
                            }
                        }
                    }
                    
                    for (var j = 0; j < window.Count; j++)
                    {
                        Word.Summary r;

                        if (s.Window.ContainsKey(window[j]))
                        {
                            r = s.Window[window[j]];
                        }
                        else
                        {
                            r = s.Window[window[j]] = new Word.Summary();
                        }

                        r.Count++;
                    }

                }
                           
                return W;
            }

            private static bool Has(Grammar.Node n, params Grammar.Attributes[] args)
            {
                for (var i = 0; i < args.Length; i++)
                {
                    if ((n.Attributes & args[i]) == args[i])
                    {
                        return true;
                    }
                }

                return false;
            }

            public static IList<string> Words(string text)
            {
                var W = new List<string>();

                var i = 0; var len = text.Length; var c = '\0';

                Func<char> next = () =>
                {
                    if (i < len)
                    {
                        return c = text[i++];
                    }

                    return c = '\0';
                };

                next();

                StringBuilder w = null;

                Action<char> take = (what) =>
                {
                    if (w == null)
                    {
                        w = new StringBuilder();
                    }

                    w.Append(what);

                    next();
                };

                while (c != '\0')
                {
                    if (c == '<' || c == ',' || c == '>' || c == '.' || c == '?' || c == '!')
                    {
                        W.Add(c.ToString());
                        next();
                        continue;
                    }

                    w = new StringBuilder();

                    while (IsVowel(c) || IsConsonant(c))
                    {
                        take(c);
                    }

                    if (w.Length > 0)
                    {
                        W.Add(w.ToString());
                    }

                    Debug.Assert(!(IsVowel(c) || IsConsonant(c)));

                    next();
                }

                return W;
            }

            public static bool AreAllConsonants(string args)
            {
                foreach (var c in args)
                {
                    if (!IsConsonant(c))
                    {
                        return false;
                    }
                }

                return args.Length > 0;
            }

            public static bool IsConsonant(Char c)
            {
                switch (c)
                {
                    case 'b': return true;
                    case 'c': return true;
                    case 'd': return true;
                    case 'f': return true;
                    case 'g': return true;
                    case 'h': return true;
                    case 'j': return true;
                    case 'k': return true;
                    case 'l': return true;
                    case 'm': return true;
                    case 'n': return true;
                    case 'p': return true;
                    case 'q': return true;
                    case 'r': return true;
                    case 's': return true;
                    case 't': return true;
                    case 'v': return true;
                    case 'w': return true;
                    case 'x': return true;
                    case 'z': return true;
                    case 'B': return true;
                    case 'C': return true;
                    case 'D': return true;
                    case 'F': return true;
                    case 'G': return true;
                    case 'H': return true;
                    case 'J': return true;
                    case 'K': return true;
                    case 'L': return true;
                    case 'M': return true;
                    case 'N': return true;
                    case 'P': return true;
                    case 'Q': return true;
                    case 'R': return true;
                    case 'S': return true;
                    case 'T': return true;
                    case 'V': return true;
                    case 'W': return true;
                    case 'X': return true;
                    case 'Z': return true;
                }

                return false;
            }

            public static bool IsDoubleConsonant(Char c)
            {
                switch (c)
                {
                    case 'j': return true;
                    case 'w': return true;
                    case 'x': return true;
                    case 'z': return true;
                    case 'J': return true;
                    case 'W': return true;
                    case 'X': return true;
                    case 'Z': return true;
                }
                return false;
            }

            public static bool IsVowel(Char c)
            {
                switch (c)
                {
                    case 'ă': return true;
                    case 'ĕ': return true;
                    case 'ĭ': return true;
                    case 'ŏ': return true;
                    case 'ŭ': return true;
                    case 'ў': return true;
                    case 'a': return true;
                    case 'e': return true;
                    case 'i': return true;
                    case 'o': return true;
                    case 'u': return true;
                    case 'y': return true;
                    case 'ā': return true;
                    case 'ē': return true;
                    case 'ī': return true;
                    case 'ō': return true;
                    case 'ū': return true;
                    case 'ȳ': return true;
                    case 'Ă': return true;
                    case 'Ĕ': return true;
                    case 'Ĭ': return true;
                    case 'Ŏ': return true;
                    case 'Ŭ': return true;
                    case 'Ў': return true;
                    case 'A': return true;
                    case 'E': return true;
                    case 'I': return true;
                    case 'O': return true;
                    case 'U': return true;
                    case 'Y': return true;
                    case 'Ā': return true;
                    case 'Ē': return true;
                    case 'Ī': return true;
                    case 'Ō': return true;
                    case 'Ū': return true;
                    case 'Ȳ': return true;
                }
                return false;
            }

            public static char Correct(Char c)
            {
                switch (c)
                {
                    case 'ă': return 'a';
                    case 'ĕ': return 'e';
                    case 'ĭ': return 'i';
                    case 'ŏ': return 'o';
                    case 'ŭ': return 'u';
                    case 'ў': return 'y';
                    case 'a': return 'a';
                    case 'e': return 'e';
                    case 'i': return 'i';
                    case 'o': return 'o';
                    case 'u': return 'u';
                    case 'y': return 'y';
                    case 'ā': return 'a';
                    case 'ē': return 'e';
                    case 'ī': return 'i';
                    case 'ō': return 'o';
                    case 'ū': return 'u';
                    case 'ȳ': return 'y';
                    case 'Ă': return 'A';
                    case 'Ĕ': return 'E';
                    case 'Ĭ': return 'I';
                    case 'Ŏ': return 'O';
                    case 'Ŭ': return 'U';
                    case 'Ў': return 'Y';
                    case 'A': return 'A';
                    case 'E': return 'E';
                    case 'I': return 'I';
                    case 'O': return 'O';
                    case 'U': return 'U';
                    case 'Y': return 'Y';
                    case 'Ā': return 'A';
                    case 'Ē': return 'E';
                    case 'Ī': return 'I';
                    case 'Ō': return 'O';
                    case 'Ū': return 'U';
                    case 'Ȳ': return 'Y';
                }
                return c;
            }
        }

    }
}