using System;
using System.Collections.Generic;

namespace System.Text
{
    public static class Converter
    {
        private static char[] s_map;

        static Converter()
        {
            Dictionary<char, char> hash = new Dictionary<char, char>();
            hash.Add('ʼ', '\'');
            hash.Add('‘', '\'');
            hash.Add('’', '\'');
            hash.Add('ʻ', '\'');
            hash.Add('–', '-');
            hash.Add('¯', '_');
            hash.Add('—', '-');
            hash.Add('?', '?');
            hash.Add('!', '!');
            hash.Add('(', '(');
            hash.Add(')', ')');
            hash.Add('"', '"');
            hash.Add('Ą', 'A');
            hash.Add('á', 'a');
            hash.Add('Á', 'A');
            hash.Add('à', 'a');
            hash.Add('À', 'A');
            hash.Add('â', 'a');
            hash.Add('Â', 'A');
            hash.Add('ä', 'a');
            hash.Add('Ä', 'A');
            hash.Add('ă', 'a');
            hash.Add('Ă', 'A');
            hash.Add('ā', 'a');
            hash.Add('Ā', 'A');
            hash.Add('ã', 'a');
            hash.Add('Ã', 'A');
            hash.Add('å', 'a');
            hash.Add('Å', 'A');
            hash.Add('Ǻ', 'A');
            hash.Add('ầ', 'a');
            hash.Add('Ầ', 'A');
            hash.Add('ắ', 'a');
            hash.Add('Ắ', 'A');
            hash.Add('ằ', 'a');
            hash.Add('Ằ', 'A');
            hash.Add('ẵ', 'a');
            hash.Add('Ẵ', 'A');
            hash.Add('ả', 'a');
            hash.Add('Ả', 'A');
            hash.Add('ạ', 'a');
            hash.Add('Ạ', 'A');
            hash.Add('ậ', 'a');
            hash.Add('Ậ', 'A');
            hash.Add('ấ', 'a');
            hash.Add('Ấ', 'A');
            hash.Add('ą', 'a');
            hash.Add('ß', 'b');
            hash.Add('þ', 'b');
            hash.Add('Þ', 'B');
            hash.Add('ć', 'c');
            hash.Add('Ć', 'C');
            hash.Add('č', 'c');
            hash.Add('Č', 'C');
            hash.Add('ç', 'c');
            hash.Add('Ç', 'C');
            hash.Add('ĉ', 'c');
            hash.Add('Ĉ', 'C');
            hash.Add('ċ', 'c');
            hash.Add('Ċ', 'C');
            hash.Add('ḑ', 'd');
            hash.Add('Ḑ', 'D');
            hash.Add('đ', 'd');
            hash.Add('Đ', 'D');
            hash.Add('ð', 'd');
            hash.Add('Ð', 'D');
            hash.Add('ḍ', 'd');
            hash.Add('Ḍ', 'D');
            hash.Add('ď', 'd');
            hash.Add('Ď', 'D');
            hash.Add('é', 'e');
            hash.Add('É', 'E');
            hash.Add('è', 'e');
            hash.Add('È', 'E');
            hash.Add('ė', 'e');
            hash.Add('Ė', 'E');
            hash.Add('ê', 'e');
            hash.Add('Ê', 'E');
            hash.Add('ë', 'e');
            hash.Add('Ë', 'E');
            hash.Add('ě', 'e');
            hash.Add('Ě', 'E');
            hash.Add('ĕ', 'e');
            hash.Add('Ĕ', 'E');
            hash.Add('ē', 'e');
            hash.Add('Ē', 'E');
            hash.Add('ę', 'e');
            hash.Add('Ę', 'E');
            hash.Add('ế', 'e');
            hash.Add('Ế', 'E');
            hash.Add('ề', 'e');
            hash.Add('Ề', 'E');
            hash.Add('ệ', 'e');
            hash.Add('Ệ', 'E');
            hash.Add('ǝ', 'e');
            hash.Add('Ǝ', 'E');
            hash.Add('ə', 'e');
            hash.Add('Ə', 'E');
            hash.Add('ể', 'e');
            hash.Add('Ể', 'E');
            hash.Add('ễ', 'e');
            hash.Add('Ễ', 'E');
            hash.Add('ɛ', 'e');
            hash.Add('Ɛ', 'E');
            hash.Add('ğ', 'g');
            hash.Add('Ğ', 'G');
            hash.Add('ĝ', 'g');
            hash.Add('Ĝ', 'G');
            hash.Add('ġ', 'g');
            hash.Add('Ġ', 'G');
            hash.Add('ģ', 'g');
            hash.Add('Ģ', 'G');
            hash.Add('ḩ', 'h');
            hash.Add('Ḩ', 'H');
            hash.Add('ħ', 'h');
            hash.Add('Ħ', 'H');
            hash.Add('ḥ', 'h');
            hash.Add('Ḥ', 'H');
            hash.Add('ĥ', 'h');
            hash.Add('Ĥ', 'H');
            hash.Add('ẖ', 'h');
            hash.Add('ı', 'i');
            hash.Add('I', 'I');
            hash.Add('í', 'i');
            hash.Add('Í', 'I');
            hash.Add('ì', 'i');
            hash.Add('Ì', 'I');
            hash.Add('i', 'i');
            hash.Add('İ', 'I');
            hash.Add('î', 'i');
            hash.Add('Î', 'I');
            hash.Add('ï', 'i');
            hash.Add('Ï', 'I');
            hash.Add('ĭ', 'i');
            hash.Add('Ĭ', 'I');
            hash.Add('ī', 'i');
            hash.Add('Ī', 'I');
            hash.Add('ĩ', 'i');
            hash.Add('Ĩ', 'I');
            hash.Add('ỉ', 'i');
            hash.Add('Ỉ', 'I');
            hash.Add('ị', 'i');
            hash.Add('Ị', 'I');
            hash.Add('ķ', 'k');
            hash.Add('Ķ', 'K');
            hash.Add('ļ', 'l');
            hash.Add('Ļ', 'L');
            hash.Add('ł', 'l');
            hash.Add('Ł', 'L');
            hash.Add('ľ', 'l');
            hash.Add('Ľ', 'L');
            hash.Add('ň', 'n');
            hash.Add('Ň', 'N');
            hash.Add('ñ', 'n');
            hash.Add('Ñ', 'N');
            hash.Add('ń', 'n');
            hash.Add('Ń', 'N');
            hash.Add('ŋ', 'n');
            hash.Add('Ŋ', 'N');
            hash.Add('ņ', 'n');
            hash.Add('Ņ', 'N');
            hash.Add('ó', 'o');
            hash.Add('Ó', 'O');
            hash.Add('ò', 'o');
            hash.Add('Ò', 'O');
            hash.Add('ô', 'o');
            hash.Add('Ô', 'O');
            hash.Add('ö', 'o');
            hash.Add('Ö', 'O');
            hash.Add('ŏ', 'o');
            hash.Add('Ŏ', 'O');
            hash.Add('ō', 'o');
            hash.Add('Ō', 'O');
            hash.Add('õ', 'o');
            hash.Add('Õ', 'O');
            hash.Add('ő', 'o');
            hash.Add('Ő', 'O');
            hash.Add('ố', 'o');
            hash.Add('Ố', 'O');
            hash.Add('ồ', 'o');
            hash.Add('Ồ', 'O');
            hash.Add('ø', 'o');
            hash.Add('Ø', 'O');
            hash.Add('ơ', 'o');
            hash.Add('Ơ', 'O');
            hash.Add('ọ', 'o');
            hash.Add('Ọ', 'O');
            hash.Add('ớ', 'o');
            hash.Add('Ớ', 'O');
            hash.Add('ộ', 'o');
            hash.Add('Ộ', 'O');
            hash.Add('ɔ', 'o');
            hash.Add('Ɔ', 'O');
            hash.Add('ổ', 'o');
            hash.Add('Ổ', 'O');
            hash.Add('ỏ', 'o');
            hash.Add('Ỏ', 'O');
            hash.Add('ř', 'r');
            hash.Add('Ř', 'R');
            hash.Add('ś', 's');
            hash.Add('Ś', 'S');
            hash.Add('š', 's');
            hash.Add('Š', 'S');
            hash.Add('ş', 's');
            hash.Add('Ş', 'S');
            hash.Add('ṣ', 's');
            hash.Add('Ṣ', 'S');
            hash.Add('ŝ', 's');
            hash.Add('Ŝ', 'S');
            hash.Add('ș', 's');
            hash.Add('ţ', 't');
            hash.Add('Ţ', 'T');
            hash.Add('ṭ', 't');
            hash.Add('Ṭ', 'T');
            hash.Add('ŧ', 't');
            hash.Add('Ŧ', 'T');
            hash.Add('ț', 't');
            hash.Add('ť', 't');
            hash.Add('Ť', 'T');
            hash.Add('ú', 'u');
            hash.Add('Ú', 'U');
            hash.Add('ù', 'u');
            hash.Add('Ù', 'U');
            hash.Add('ü', 'u');
            hash.Add('Ü', 'U');
            hash.Add('ŭ', 'u');
            hash.Add('Ŭ', 'U');
            hash.Add('ū', 'u');
            hash.Add('Ū', 'U');
            hash.Add('ũ', 'u');
            hash.Add('Ũ', 'U');
            hash.Add('ų', 'u');
            hash.Add('Ų', 'U');
            hash.Add('ủ', 'u');
            hash.Add('Ủ', 'U');
            hash.Add('ư', 'u');
            hash.Add('Ư', 'U');
            hash.Add('ừ', 'u');
            hash.Add('Ừ', 'U');
            hash.Add('û', 'u');
            hash.Add('Û', 'U');
            hash.Add('ự', 'u');
            hash.Add('Ự', 'U');
            hash.Add('ů', 'u');
            hash.Add('Ů', 'U');
            hash.Add('ụ', 'u');
            hash.Add('Ụ', 'U');
            hash.Add('ṳ', 'u');
            hash.Add('Ṳ', 'U');
            hash.Add('ứ', 'u');
            hash.Add('Ứ', 'U');
            hash.Add('ŵ', 'w');
            hash.Add('Ŵ', 'W');
            hash.Add('ý', 'y');
            hash.Add('Ý', 'Y');
            hash.Add('ỹ', 'y');
            hash.Add('Ỹ', 'Y');
            hash.Add('ỳ', 'y');
            hash.Add('Ỳ', 'Y');
            hash.Add('ź', 'z');
            hash.Add('Ź', 'Z');
            hash.Add('ž', 'z');
            hash.Add('Ž', 'Z');
            hash.Add('ż', 'z');
            hash.Add('Ż', 'Z');
            hash.Add('ẕ', 'z');
            hash.Add('Ẕ', 'Z');
            hash.Add('J', 'I');
            hash.Add('j', 'i');
            hash.Add('V', 'U');
            hash.Add('v', 'u');
            for (char i = 'a'; i <= 'z'; i += '\u0001')
            {
                if (!hash.ContainsKey(i))
                {
                    hash.Add(i, i);
                }
            }
            for (char j = 'A'; j <= 'Z'; j += '\u0001')
            {
                if (!hash.ContainsKey(j))
                {
                    hash.Add(j, j);
                }
            }
            int m_max = -2147483648;
            foreach (KeyValuePair<char, char> k in hash)
            {
                m_max = Math.Max((int)k.Key, m_max);
            }
            Converter.s_map = new char[m_max + 1];
            foreach (KeyValuePair<char, char> l in hash)
            {
                Converter.s_map[(int)l.Key] = l.Value;
            }
            for (int m = 0; m < Converter.s_map.Length; m++)
            {
                char t = Converter.s_map[m];
                if (t == '\0')
                {
                    t = (char)m;
                }
                if (Converter.IsUpperCase(t))
                {
                    t = (char)((int)t + (int)'a' - (int)'A');
                }
                Converter.s_map[m] = t;
            }
        }

        private static bool IsUpperCase(char c)
        {
            switch (c)
            {
                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                case 'F':
                case 'G':
                case 'H':
                case 'I':
                case 'J':
                case 'K':
                case 'L':
                case 'M':
                case 'N':
                case 'O':
                case 'P':
                case 'Q':
                case 'R':
                case 'S':
                case 'T':
                case 'U':
                case 'V':
                case 'W':
                case 'X':
                case 'Y':
                case 'Z':
                    return true;
                default:
                    return false;
            }
        }

        public static int GetLowerCaseChar(char c)
        {
            if ((int)c < Converter.s_map.Length)
            {
                char t = Converter.s_map[(int)c];
                if (t != '\0')
                {
                    return (int)t;
                }
            }
            return (int)c;
        }

        public static bool StartWith2(string start, string str0, string str1)
        {
            int max = start.Length;
            int len0 = str0.Length;
            int len = str1.Length;
            if (max > len0 + len)
            {
                return false;
            }
            int pos = 0;
            for (int i = 0; i < str0.Length; i++)
            {
                if (pos >= max)
                {
                    return true;
                }
                if (Converter.GetLowerCaseChar(start[pos]) != Converter.GetLowerCaseChar(str0[i]))
                {
                    return false;
                }
                pos++;
            }
            for (int j = 0; j < str1.Length; j++)
            {
                if (pos >= max)
                {
                    return true;
                }
                if (Converter.GetLowerCaseChar(start[pos]) != Converter.GetLowerCaseChar(str1[j]))
                {
                    return false;
                }
                pos++;
            }
            return pos >= max;
        }

        public static bool Equals(string left, string right0)
        {
            int max = left.Length;
            int len0 = right0.Length;
            if (max != len0)
            {
                return false;
            }
            int pos = 0;
            for (int i = 0; i < len0; i++)
            {
                if (pos >= max)
                {
                    return false;
                }
                if (Converter.GetLowerCaseChar(left[pos]) != Converter.GetLowerCaseChar(right0[i]))
                {
                    return false;
                }
                pos++;
            }
            return true;
        }

        public static bool Equals(string left, string right0, string right1)
        {
            int max = left.Length;
            int len0 = right0.Length;
            int len = right1.Length;
            if (max != len0 + len)
            {
                return false;
            }
            int pos = 0;
            for (int i = 0; i < len0; i++)
            {
                if (pos >= max)
                {
                    return false;
                }
                if (Converter.GetLowerCaseChar(left[pos]) != Converter.GetLowerCaseChar(right0[i]))
                {
                    return false;
                }
                pos++;
            }
            for (int j = 0; j < len; j++)
            {
                if (pos >= max)
                {
                    return false;
                }
                if (Converter.GetLowerCaseChar(left[pos]) != Converter.GetLowerCaseChar(right1[j]))
                {
                    return false;
                }
                pos++;
            }
            return true;
        }

        public static bool Possible2(string find, string str)
        {
            if (find == null || find.Length <= 0)
            {
                return false;
            }
            if (str == null || str.Length <= 0)
            {
                return false;
            }
            if (object.ReferenceEquals(find, str))
            {
                return true;
            }
            if (find.Length <= str.Length)
            {
                for (int i = find.Length - 1; i >= 0; i--)
                {
                    if (Converter.GetLowerCaseChar(find[i]) != Converter.GetLowerCaseChar(str[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            for (int j = str.Length - 1; j >= 0; j--)
            {
                if (Converter.GetLowerCaseChar(find[j]) != Converter.GetLowerCaseChar(str[j]))
                {
                    return false;
                }
            }
            return true;
        }

        public static int GetHashCode(string arg0)
        {
            int hash = 5381;
            for (int i = 0; i < arg0.Length; i++)
            {
                char c = arg0[i];
                hash = (hash << 5) + hash + Converter.GetLowerCaseChar(c);
            }
            return hash & 2147483646;
        }

        public static int GetHashCode(string arg0, string arg1)
        {
            int hash = 5381;
            for (int i = 0; i < arg0.Length; i++)
            {
                hash = (hash << 5) + hash + Converter.GetLowerCaseChar(arg0[i]);
            }
            for (int j = 0; j < arg1.Length; j++)
            {
                hash = (hash << 5) + hash + Converter.GetLowerCaseChar(arg1[j]);
            }
            return hash & 2147483646;
        }

        public static int GetHashCode(params string[] args)
        {
            if (args == null || args.Length <= 0)
            {
                return 0;
            }
            int hash = 5381;
            for (int i = 0; i < args.Length; i++)
            {
                string s = args[i];
                for (int j = 0; j < s.Length; j++)
                {
                    char c = s[j];
                    hash = (hash << 5) + hash + Converter.GetLowerCaseChar(c);
                }
            }
            return hash & 2147483646;
        }

        public static string ToHash(string S)
        {
            if (string.IsNullOrEmpty(S))
            {
                return S;
            }
            StringBuilder builder = new StringBuilder();
            return builder.ToString();
        }
    }
}
