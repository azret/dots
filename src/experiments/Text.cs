using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace System.Text
{
    public static class Map
    {
        public static void Main()
        {
            HashSet<string> W = new HashSet<string>();  HashSet<string> S = new HashSet<string>();

            Action<string> Process = (file) =>
            {
                int tag = 0;

                FromHtml(File.ReadAllText(file), (t, text, i, len) =>
                {
                    var s = text.Substring(i, len);

                    switch (t)
                    {
                        case Token.Lt:

                            tag++;

                            break;

                        case Token.Gt:

                            tag = 0;

                            break;

                        case Token.Word:

                            if (tag == 0)
                            {
                                W.Add(s);
                            }

                            break;
                    }
                });
            };

            foreach (var file in Directory.EnumerateFiles($"D:\\Dots\\data\\", "*.*"))
            {
                Process(file);
            }

            foreach (var w in W)
            {
                var Q = Syllabify(w);

                foreach (var q in Q)
                {
                    S.Add(q);
                }
            }

            List<string> T = new List<string>(S);

            T.Sort((a, b) =>
            {
                double L = 0;

                for (int i = 0; i < a.Length; i++)
                {
                    // L += Math.Sin(a[i]) * Math.Sin(a[i]);
                    L += a[i] * a[i];
                }

                double R = 0;

                for (int i = 0; i < b.Length; i++)
                {
                    // R += Math.Sin(b[i]) * Math.Sin(b[i]);
                    R += b[i] * b[i];
                }

                if (L < R)
                {
                    return -1;
                }
                else if (L > R)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            });

            foreach (var t in T)
            {             
                Console.WriteLine(t);
            }

            Console.ReadKey();

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

        public static char Ascii(Char c)
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

        public static bool AreConsonants(string args)
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

        public static List<string> Syllabify(string word)
        {
            List<string> S = new List<string>();

            var reader = 0; var len = word.Length; var c = '\0';

            Func<char> next = () =>
            {
                if (reader < len)
                {
                    var t = char.ToLowerInvariant(Ascii(word[reader]));

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
                    if (reader < len && IsVowel(word[reader]))
                    {
                        take(c);
                    }
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

                // Starts with ...

                if (a == 'u' && IsVowel(c) && reader == 2)
                {
                    take(c);
                }

                else if (a == 'i' && IsVowel(c) && reader == 2)
                {
                    take(c);
                }

                // In-between vowels ...

                else if (c == 'i' || c == 'b' || c == 'u')
                {
                    if (IsVowel(p) || IsDiphthong(a, c))
                    {
                        take(c);
                    }
                }

                // Diphthong ...

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
             
            if (S.Count <= 0)
            {
                S.Add(word);
            }

            // Collapse ...

            if (S.Count > 0)
            {
                var last = S[S.Count - 1];

                if (AreConsonants(last))
                {
                    S.RemoveAt(S.Count - 1);

                    if (S.Count > 0)
                    {
                        S[S.Count - 1] += last;
                    }
                    else
                    {
                        S.Add(last);
                    }
                }
            }

            return S;
        }

        public enum Token : byte
        {
            Other  = 0x00,
            Word  = 0x01,
            Number = 0x02,
            Lt = 0x03,
            Gt = 0x04,
        }

        public static IList<string> FromHtml(string text, Action<Token, string, int, int> emit)
        {
            var W = new List<string>();

            var i = 0; var len = text.Length; var c = '\0';

            while (i < len)
            {
                int start = i; c = text[i++];

                switch (c)
                { 
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'z':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Z':
                    case 'ă':
                    case 'ĕ':
                    case 'ĭ':
                    case 'ŏ':
                    case 'ŭ':
                    case 'ў':
                    case 'a':
                    case 'e':
                    case 'i':
                    case 'o':
                    case 'u':
                    case 'y':
                    case 'ā':
                    case 'ē':
                    case 'ī':
                    case 'ō':
                    case 'ū':
                    case 'ȳ':
                    case 'Ă':
                    case 'Ĕ':
                    case 'Ĭ':
                    case 'Ŏ':
                    case 'Ŭ':
                    case 'Ў':
                    case 'A':
                    case 'E':
                    case 'I':
                    case 'O':
                    case 'U':
                    case 'Y':
                    case 'Ā':
                    case 'Ē':
                    case 'Ī':
                    case 'Ō':
                    case 'Ū':
                    case 'Ȳ':

                        while (i < len && char.IsLetter(text[i]))
                        {
                            i++;
                        }
                         
                        emit(Token.Word, text, start, i - start);

                        break;

                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':

                        while (i < len && char.IsDigit(text[i]))
                        {
                            i++;
                        }

                        if (i < len && text[i] == '.')
                        {
                            i++;
                        }

                        while (i < len && char.IsDigit(text[i]))
                        {
                            i++;
                        }

                        emit(Token.Number, text, start, i - start);

                        break;

                    case '<':

                        while (i < len && (char.IsLetter(text[i]) || text[i] == '!' || text[i] == '?' || text[i] == '/'))
                        {
                            i++;
                        }

                        emit(Token.Lt, text, start, i - start);

                        break;

                    case '>':

                        emit(Token.Gt, text, start, i - start);

                        break;

                    case '&':

                        while (i < len && char.IsLetterOrDigit(text[i]))
                        {
                            i++;
                        }

                        if (i < len && text[i] == ';')
                        {
                            i++;
                        }

                        emit(Token.Other, text, start, i - start);

                        break;

                    default:

                        emit(Token.Other, text, start, i - start);

                        break;

                }                
            }

            return W;
        }
    }
}