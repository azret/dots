using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class Grammar
{
    public static class Strings
    {
        public static bool IsNullOrWhiteSpace(string value)
        {
            if (value == null)
            {
                return true;
            }
            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }

    public enum Speach
    {
        None,
        Noun,
        Adverb,
        Interjection,
        Ajective,
        Verb,
        Pronoun,
        Preposition,
        Conjunction,
        Number
    }

    [Flags]
    public enum Attributes : uint
    {
        None = 0u,
        Nominative = 1u,
        Genitive = 2u,
        Dative = 4u,
        Accusative = 8u,
        Ablative = 16u,
        Locative = 32u,
        Vocative = 64u,
        Musculine = 128u,
        Feminine = 256u,
        Neuter = 512u,
        Common = 1024u,
        Positive = 2048u,
        Superlative = 4096u,
        Comparative = 8192u,
        Indicative = 16384u,
        Subjunctive = 32768u,
        Imerative = 65536u,
        Infinitive = 131072u,
        Active = 262144u,
        Passive = 524288u,
        Present = 1048576u,
        Perfect = 2097152u,
        Imperfect = 4194304u,
        Future = 8388608u,
        Participle = 16777216u,
        Supine = 33554432u,
        First = 67108864u,
        Second = 134217728u,
        Third = 268435456u,
        Singular = 536870912u,
        Plural = 1073741824u,
        Contraction = 2147483648u
    }

    public class Node
    {
        public readonly Node Next;

        public readonly int Index;

        public readonly int HashCode;

        public readonly int Entry;

        public readonly string Stem;

        public readonly string Suffix;

        public readonly Attributes Attributes;

        public Node(int entry, string stem, string suffix, Attributes attributes, Node top)
        {
            this.Entry = entry;
            this.Stem = stem;
            this.Suffix = suffix;
            this.Attributes = attributes;
            this.Next = top;
            if (top != null)
            {
                this.Index = top.Index + 1;
            }
            else
            {
                this.Index = 0;
            }
            this.HashCode = Converter.GetHashCode(stem, suffix);
        }

        public Node(int entry, string stem, string suffix, Attributes attributes, int hashCode, Node top)
        {
            this.Entry = entry;
            this.Stem = stem;
            this.Suffix = suffix;
            this.Attributes = attributes;
            this.Next = top;
            if (top != null)
            {
                this.Index = top.Index + 1;
            }
            else
            {
                this.Index = 0;
            }
            this.HashCode = hashCode;
        }
    }

    public class Word
    {
        private readonly int _Index;

        private readonly Speach _Speach;

        private readonly string[] _Flags;

        private readonly string[] _Stems;

        internal string _Definiton = string.Empty;

        public int Index
        {
            get
            {
                return this._Index;
            }
        }

        public Speach Speach
        {
            get
            {
                return this._Speach;
            }
        }

        public string[] Flags
        {
            get
            {
                return this._Flags;
            }
        }

        public string[] Stems
        {
            get
            {
                return this._Stems;
            }
        }

        public string Definiton
        {
            get
            {
                return this._Definiton;
            }
        }

        public static bool Compare(string[] Left, string[] Right)
        {
            if (object.ReferenceEquals(Left, Right))
            {
                return true;
            }
            if (Left == null)
            {
                return Right == null;
            }
            if (Right == null)
            {
                return Left == null;
            }
            if (Left.Length != Right.Length)
            {
                return false;
            }
            for (int i = 0; i < Left.Length; i++)
            {
                if (string.Compare(Left[i], Right[i], StringComparison.InvariantCultureIgnoreCase) != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Compare(Word Left, Word Right)
        {
            if (object.ReferenceEquals(Left, Right))
            {
                return true;
            }
            if (Left == null)
            {
                return Right == null;
            }
            if (Right == null)
            {
                return Left == null;
            }
            return Left.Speach == Right.Speach && Word.Compare(Left.Stems, Right.Stems) && Word.Compare(Left.Flags, Right.Flags);
        }

        public Word(int index, Speach speach, string[] flags, string[] stems)
        {
            this._Index = index;
            this._Speach = speach;
            this._Flags = flags;
            this._Stems = stems;
            this._Definiton = string.Empty;
        }

        public Node Top;
    }

    public class Partitions
    {
        private int n_blank;

        private int n_other;

        private int n_a;

        private int n_b;

        private int n_c;

        private int n_d;

        private int n_e;

        private int n_f;

        private int n_g;

        private int n_h;

        private int n_i;

        private int n_j;

        private int n_k;

        private int n_l;

        private int n_m;

        private int n_n;

        private int n_o;

        private int n_p;

        private int n_q;

        private int n_r;

        private int n_s;

        private int n_t;

        private int n_u;

        private int n_v;

        private int n_w;

        private int n_x;

        private int n_y;

        private int n_z;

        private KeyValuePair<string, int>[] _blank;

        private KeyValuePair<string, int>[] _other;

        private KeyValuePair<string, int>[] _a;

        private KeyValuePair<string, int>[] _b;

        private KeyValuePair<string, int>[] _c;

        private KeyValuePair<string, int>[] _d;

        private KeyValuePair<string, int>[] _e;

        private KeyValuePair<string, int>[] _f;

        private KeyValuePair<string, int>[] _g;

        private KeyValuePair<string, int>[] _h;

        private KeyValuePair<string, int>[] _i;

        private KeyValuePair<string, int>[] _j;

        private KeyValuePair<string, int>[] _k;

        private KeyValuePair<string, int>[] _l;

        private KeyValuePair<string, int>[] _m;

        private KeyValuePair<string, int>[] _n;

        private KeyValuePair<string, int>[] _o;

        private KeyValuePair<string, int>[] _p;

        private KeyValuePair<string, int>[] _q;

        private KeyValuePair<string, int>[] _r;

        private KeyValuePair<string, int>[] _s;

        private KeyValuePair<string, int>[] _t;

        private KeyValuePair<string, int>[] _u;

        private KeyValuePair<string, int>[] _v;

        private KeyValuePair<string, int>[] _w;

        private KeyValuePair<string, int>[] _x;

        private KeyValuePair<string, int>[] _y;

        private KeyValuePair<string, int>[] _z;

        public void Set(Word[] ENTRIES)
        {
            this.n_blank = 0;
            this.n_other = 0;
            this.n_a = 0;
            this.n_b = 0;
            this.n_c = 0;
            this.n_d = 0;
            this.n_e = 0;
            this.n_f = 0;
            this.n_g = 0;
            this.n_h = 0;
            this.n_i = 0;
            this.n_j = 0;
            this.n_k = 0;
            this.n_l = 0;
            this.n_m = 0;
            this.n_n = 0;
            this.n_o = 0;
            this.n_p = 0;
            this.n_q = 0;
            this.n_r = 0;
            this.n_s = 0;
            this.n_t = 0;
            this.n_u = 0;
            this.n_v = 0;
            this.n_w = 0;
            this.n_x = 0;
            this.n_y = 0;
            this.n_z = 0;
            for (int i = 0; i < ENTRIES.Length; i++)
            {
                if (ENTRIES[i] != null)
                {
                    string[] stems = ENTRIES[i].Stems;
                    for (int k = 0; k < stems.Length; k++)
                    {
                        string s = stems[k];
                        if (s == null || s.Length == 0)
                        {
                            this.n_blank++;
                        }
                        else
                        {
                            switch ((ushort)Converter.GetLowerCaseChar(s[0]))
                            {
                                case 97:
                                    this.n_a++;
                                    break;
                                case 98:
                                    this.n_b++;
                                    break;
                                case 99:
                                    this.n_c++;
                                    break;
                                case 100:
                                    this.n_d++;
                                    break;
                                case 101:
                                    this.n_e++;
                                    break;
                                case 102:
                                    this.n_f++;
                                    break;
                                case 103:
                                    this.n_g++;
                                    break;
                                case 104:
                                    this.n_h++;
                                    break;
                                case 105:
                                    this.n_i++;
                                    break;
                                case 106:
                                    this.n_j++;
                                    break;
                                case 107:
                                    this.n_k++;
                                    break;
                                case 108:
                                    this.n_l++;
                                    break;
                                case 109:
                                    this.n_m++;
                                    break;
                                case 110:
                                    this.n_n++;
                                    break;
                                case 111:
                                    this.n_o++;
                                    break;
                                case 112:
                                    this.n_p++;
                                    break;
                                case 113:
                                    this.n_q++;
                                    break;
                                case 114:
                                    this.n_r++;
                                    break;
                                case 115:
                                    this.n_s++;
                                    break;
                                case 116:
                                    this.n_t++;
                                    break;
                                case 117:
                                    this.n_u++;
                                    break;
                                case 118:
                                    this.n_v++;
                                    break;
                                case 119:
                                    this.n_w++;
                                    break;
                                case 120:
                                    this.n_x++;
                                    break;
                                case 121:
                                    this.n_y++;
                                    break;
                                case 122:
                                    this.n_z++;
                                    break;
                                default:
                                    this.n_other++;
                                    break;
                            }
                        }
                    }
                }
            }
            this._blank = new KeyValuePair<string, int>[this.n_blank];
            this._other = new KeyValuePair<string, int>[this.n_other];
            this._a = new KeyValuePair<string, int>[this.n_a];
            this._b = new KeyValuePair<string, int>[this.n_b];
            this._c = new KeyValuePair<string, int>[this.n_c];
            this._d = new KeyValuePair<string, int>[this.n_d];
            this._e = new KeyValuePair<string, int>[this.n_e];
            this._f = new KeyValuePair<string, int>[this.n_f];
            this._g = new KeyValuePair<string, int>[this.n_g];
            this._h = new KeyValuePair<string, int>[this.n_h];
            this._i = new KeyValuePair<string, int>[this.n_i];
            this._j = new KeyValuePair<string, int>[this.n_j];
            this._k = new KeyValuePair<string, int>[this.n_k];
            this._l = new KeyValuePair<string, int>[this.n_l];
            this._m = new KeyValuePair<string, int>[this.n_m];
            this._n = new KeyValuePair<string, int>[this.n_n];
            this._o = new KeyValuePair<string, int>[this.n_o];
            this._p = new KeyValuePair<string, int>[this.n_p];
            this._q = new KeyValuePair<string, int>[this.n_q];
            this._r = new KeyValuePair<string, int>[this.n_r];
            this._s = new KeyValuePair<string, int>[this.n_s];
            this._t = new KeyValuePair<string, int>[this.n_t];
            this._u = new KeyValuePair<string, int>[this.n_u];
            this._v = new KeyValuePair<string, int>[this.n_v];
            this._w = new KeyValuePair<string, int>[this.n_w];
            this._x = new KeyValuePair<string, int>[this.n_x];
            this._y = new KeyValuePair<string, int>[this.n_y];
            this._z = new KeyValuePair<string, int>[this.n_z];
            this.n_blank = 0;
            this.n_other = 0;
            this.n_a = 0;
            this.n_b = 0;
            this.n_c = 0;
            this.n_d = 0;
            this.n_e = 0;
            this.n_f = 0;
            this.n_g = 0;
            this.n_h = 0;
            this.n_i = 0;
            this.n_j = 0;
            this.n_k = 0;
            this.n_l = 0;
            this.n_m = 0;
            this.n_n = 0;
            this.n_o = 0;
            this.n_p = 0;
            this.n_q = 0;
            this.n_r = 0;
            this.n_s = 0;
            this.n_t = 0;
            this.n_u = 0;
            this.n_v = 0;
            this.n_w = 0;
            this.n_x = 0;
            this.n_y = 0;
            this.n_z = 0;
            for (int j = 0; j < ENTRIES.Length; j++)
            {
                if (ENTRIES[j] != null)
                {
                    string[] stems2 = ENTRIES[j].Stems;
                    for (int l = 0; l < stems2.Length; l++)
                    {
                        string s2 = stems2[l];
                        if (s2 == null || s2.Length == 0)
                        {
                            this._blank[this.n_blank] = new KeyValuePair<string, int>(string.Empty, j);
                            this.n_blank++;
                        }
                        else
                        {
                            switch ((ushort)Converter.GetLowerCaseChar(s2[0]))
                            {
                                case 97:
                                    this._a[this.n_a] = new KeyValuePair<string, int>(s2, j);
                                    this.n_a++;
                                    break;
                                case 98:
                                    this._b[this.n_b] = new KeyValuePair<string, int>(s2, j);
                                    this.n_b++;
                                    break;
                                case 99:
                                    this._c[this.n_c] = new KeyValuePair<string, int>(s2, j);
                                    this.n_c++;
                                    break;
                                case 100:
                                    this._d[this.n_d] = new KeyValuePair<string, int>(s2, j);
                                    this.n_d++;
                                    break;
                                case 101:
                                    this._e[this.n_e] = new KeyValuePair<string, int>(s2, j);
                                    this.n_e++;
                                    break;
                                case 102:
                                    this._f[this.n_f] = new KeyValuePair<string, int>(s2, j);
                                    this.n_f++;
                                    break;
                                case 103:
                                    this._g[this.n_g] = new KeyValuePair<string, int>(s2, j);
                                    this.n_g++;
                                    break;
                                case 104:
                                    this._h[this.n_h] = new KeyValuePair<string, int>(s2, j);
                                    this.n_h++;
                                    break;
                                case 105:
                                    this._i[this.n_i] = new KeyValuePair<string, int>(s2, j);
                                    this.n_i++;
                                    break;
                                case 106:
                                    this._j[this.n_j] = new KeyValuePair<string, int>(s2, j);
                                    this.n_j++;
                                    break;
                                case 107:
                                    this._k[this.n_k] = new KeyValuePair<string, int>(s2, j);
                                    this.n_k++;
                                    break;
                                case 108:
                                    this._l[this.n_l] = new KeyValuePair<string, int>(s2, j);
                                    this.n_l++;
                                    break;
                                case 109:
                                    this._m[this.n_m] = new KeyValuePair<string, int>(s2, j);
                                    this.n_m++;
                                    break;
                                case 110:
                                    this._n[this.n_n] = new KeyValuePair<string, int>(s2, j);
                                    this.n_n++;
                                    break;
                                case 111:
                                    this._o[this.n_o] = new KeyValuePair<string, int>(s2, j);
                                    this.n_o++;
                                    break;
                                case 112:
                                    this._p[this.n_p] = new KeyValuePair<string, int>(s2, j);
                                    this.n_p++;
                                    break;
                                case 113:
                                    this._q[this.n_q] = new KeyValuePair<string, int>(s2, j);
                                    this.n_q++;
                                    break;
                                case 114:
                                    this._r[this.n_r] = new KeyValuePair<string, int>(s2, j);
                                    this.n_r++;
                                    break;
                                case 115:
                                    this._s[this.n_s] = new KeyValuePair<string, int>(s2, j);
                                    this.n_s++;
                                    break;
                                case 116:
                                    this._t[this.n_t] = new KeyValuePair<string, int>(s2, j);
                                    this.n_t++;
                                    break;
                                case 117:
                                    this._u[this.n_u] = new KeyValuePair<string, int>(s2, j);
                                    this.n_u++;
                                    break;
                                case 118:
                                    this._v[this.n_v] = new KeyValuePair<string, int>(s2, j);
                                    this.n_v++;
                                    break;
                                case 119:
                                    this._w[this.n_w] = new KeyValuePair<string, int>(s2, j);
                                    this.n_w++;
                                    break;
                                case 120:
                                    this._x[this.n_x] = new KeyValuePair<string, int>(s2, j);
                                    this.n_x++;
                                    break;
                                case 121:
                                    this._y[this.n_y] = new KeyValuePair<string, int>(s2, j);
                                    this.n_y++;
                                    break;
                                case 122:
                                    this._z[this.n_z] = new KeyValuePair<string, int>(s2, j);
                                    this.n_z++;
                                    break;
                                default:
                                    this._other[this.n_other] = new KeyValuePair<string, int>(s2, j);
                                    this.n_other++;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public Dictionary<int, int> Get(string find)
        {
            if (find == null || find.Length <= 0)
            {
                return null;
            }
            KeyValuePair<string, int>[] table;
            switch ((ushort)Converter.GetLowerCaseChar(find[0]))
            {
                case 97:
                    table = this._a;
                    break;
                case 98:
                    table = this._b;
                    break;
                case 99:
                    table = this._c;
                    break;
                case 100:
                    table = this._d;
                    break;
                case 101:
                    table = this._e;
                    break;
                case 102:
                    table = this._f;
                    break;
                case 103:
                    table = this._g;
                    break;
                case 104:
                    table = this._h;
                    break;
                case 105:
                    table = this._i;
                    break;
                case 106:
                    table = this._j;
                    break;
                case 107:
                    table = this._k;
                    break;
                case 108:
                    table = this._l;
                    break;
                case 109:
                    table = this._m;
                    break;
                case 110:
                    table = this._n;
                    break;
                case 111:
                    table = this._o;
                    break;
                case 112:
                    table = this._p;
                    break;
                case 113:
                    table = this._q;
                    break;
                case 114:
                    table = this._r;
                    break;
                case 115:
                    table = this._s;
                    break;
                case 116:
                    table = this._t;
                    break;
                case 117:
                    table = this._u;
                    break;
                case 118:
                    table = this._v;
                    break;
                case 119:
                    table = this._w;
                    break;
                case 120:
                    table = this._z;
                    break;
                case 121:
                    table = this._y;
                    break;
                case 122:
                    table = this._z;
                    break;
                default:
                    table = this._other;
                    break;
            }
            Dictionary<int, int> list = new Dictionary<int, int>();
            for (int i = 0; i < table.Length; i++)
            {
                if (Converter.Possible2(find, table[i].Key))
                {
                    list[table[i].Value] = -1;
                }
            }
            for (int j = 0; j < this._blank.Length; j++)
            {
                list[this._blank[j].Value] = -1;
            }
            return list;
        }
    }

    private string _DictionaryPath = string.Empty;

    public Word[] ENTRIES;

    protected internal Node[] HASH;

    protected Grammar.Partitions PARTITIONS;

    private bool _LOADED;

    public virtual string DictionaryPath
    {
        get
        {
            return this._DictionaryPath;
        }
        set
        {
            this._DictionaryPath = value;
        }
    }

    public static void __Verb(int ENTRY, string[] STEMS, string[] FLAGS, ref Node TOP)
    {
        string key;
        switch (key = FLAGS[1])
        {
            case "1":
                {
                    string a;
                    if ((a = FLAGS[2]) == null || !(a == "1"))
                    {
                        throw new NotSupportedException();
                    }
                    if (FLAGS[3] == "DEP")
                    {
                        Grammar.DoVerbPassive11(ENTRY, STEMS, ref TOP);
                        Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                        Grammar.DoVParActive10(ENTRY, STEMS, ref TOP);
                        Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                        Grammar.DoVParPassive10(ENTRY, STEMS, ref TOP);
                        Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                        return;
                    }
                    Grammar.DoVerbActive00(ENTRY, STEMS, ref TOP);
                    Grammar.DoVerbActive11(ENTRY, STEMS, ref TOP);
                    Grammar.DoVerbPassive11(ENTRY, STEMS, ref TOP);
                    Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                    Grammar.DoVParActive10(ENTRY, STEMS, ref TOP);
                    Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                    Grammar.DoVParPassive10(ENTRY, STEMS, ref TOP);
                    Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                    return;
                }
            case "2":
                {
                    string a2;
                    if ((a2 = FLAGS[2]) == null || !(a2 == "1"))
                    {
                        throw new NotSupportedException();
                    }
                    if (FLAGS[3] == "DEP")
                    {
                        Grammar.DoVerbPassive21(ENTRY, STEMS, ref TOP);
                        Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                        Grammar.DoVParActive20(ENTRY, STEMS, ref TOP);
                        Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                        Grammar.DoVParPassive20(ENTRY, STEMS, ref TOP);
                        Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                        return;
                    }
                    Grammar.DoVerbActive00(ENTRY, STEMS, ref TOP);
                    Grammar.DoVerbActive21(ENTRY, STEMS, ref TOP);
                    Grammar.DoVerbPassive21(ENTRY, STEMS, ref TOP);
                    Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                    Grammar.DoVParActive20(ENTRY, STEMS, ref TOP);
                    Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                    Grammar.DoVParPassive20(ENTRY, STEMS, ref TOP);
                    Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                    return;
                }
            case "3":
                {
                    string a3;
                    if ((a3 = FLAGS[2]) != null)
                    {
                        if (!(a3 == "1"))
                        {
                            if (!(a3 == "2"))
                            {
                                if (!(a3 == "3"))
                                {
                                    if (a3 == "4")
                                    {
                                        if (FLAGS[3] == "DEP")
                                        {
                                            Grammar.DoVerbPassive30(ENTRY, STEMS, ref TOP);
                                            Grammar.DoVerbPassive34(ENTRY, STEMS, ref TOP);
                                            Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                            Grammar.DoVParActive30(ENTRY, STEMS, ref TOP);
                                            Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                            Grammar.DoVParPassive30(ENTRY, STEMS, ref TOP);
                                            Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                            return;
                                        }
                                        Grammar.DoVerbActive00(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVerbActive30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVerbActive34(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVerbPassive30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVerbPassive34(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVParActive30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVParPassive30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                }
                                else
                                {
                                    if (FLAGS[3] == "DEP")
                                    {
                                        Grammar.DoVerbPassive30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVerbPassive33(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVParActive30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVParPassive30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    Grammar.DoVerbActive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVerbActive30(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVerbActive33(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVerbPassive30(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVerbPassive33(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParActive30(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParPassive30(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                            }
                            else
                            {
                                if (FLAGS[3] == "DEP")
                                {
                                    Grammar.DoVerbPassive30(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVerbPassive32(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParActive30(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParPassive30(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                Grammar.DoVerbActive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVerbActive30(ENTRY, STEMS, ref TOP);
                                Grammar.DoVerbActive32(ENTRY, STEMS, ref TOP);
                                Grammar.DoVerbPassive30(ENTRY, STEMS, ref TOP);
                                Grammar.DoVerbPassive32(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParActive30(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParPassive30(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                return;
                            }
                        }
                        else
                        {
                            if (FLAGS[3] == "DEP")
                            {
                                Grammar.DoVerbPassive30(ENTRY, STEMS, ref TOP);
                                Grammar.DoVerbPassive31(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParActive30(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParPassive30(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                return;
                            }
                            Grammar.DoVerbActive00(ENTRY, STEMS, ref TOP);
                            Grammar.DoVerbActive30(ENTRY, STEMS, ref TOP);
                            Grammar.DoVerbActive31(ENTRY, STEMS, ref TOP);
                            Grammar.DoVerbPassive30(ENTRY, STEMS, ref TOP);
                            Grammar.DoVerbPassive31(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParActive30(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParPassive30(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                            return;
                        }
                    }
                    throw new NotSupportedException();
                }
            case "5":
                {
                    string a4;
                    if ((a4 = FLAGS[2]) != null)
                    {
                        if (!(a4 == "1"))
                        {
                            if (a4 == "2")
                            {
                                if (FLAGS[3] == "DEP")
                                {
                                    Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                Grammar.DoVerbActive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVerbActive50(ENTRY, STEMS, ref TOP);
                                Grammar.DoVerbActive52(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                return;
                            }
                        }
                        else
                        {
                            if (FLAGS[3] == "DEP")
                            {
                                Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParActive51(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                return;
                            }
                            Grammar.DoVerbActive00(ENTRY, STEMS, ref TOP);
                            Grammar.DoVerbActive50(ENTRY, STEMS, ref TOP);
                            Grammar.DoVerbActive51(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParActive51(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                            return;
                        }
                    }
                    throw new NotSupportedException();
                }
            case "6":
                {
                    string a5;
                    if ((a5 = FLAGS[2]) != null)
                    {
                        if (!(a5 == "1"))
                        {
                            if (a5 == "2")
                            {
                                if (FLAGS[3] == "DEP")
                                {
                                    Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParActive62(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                Grammar.DoVerbActive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVerbActive62(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParActive62(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                return;
                            }
                        }
                        else
                        {
                            if (FLAGS[3] == "DEP")
                            {
                                Grammar.DoVerbPassive61(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParActive61(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParPassive61(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                return;
                            }
                            Grammar.DoVerbActive00(ENTRY, STEMS, ref TOP);
                            Grammar.DoVerbActive61(ENTRY, STEMS, ref TOP);
                            Grammar.DoVerbPassive61(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParActive61(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParPassive61(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                            return;
                        }
                    }
                    throw new NotSupportedException();
                }
            case "7":
                {
                    string a6;
                    if ((a6 = FLAGS[2]) != null)
                    {
                        if (!(a6 == "1"))
                        {
                            if (!(a6 == "2"))
                            {
                                if (a6 == "3")
                                {
                                    if (FLAGS[3] == "DEP")
                                    {
                                        Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    Grammar.DoVerbActive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVerbActive73(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                            }
                            else
                            {
                                if (FLAGS[3] == "DEP")
                                {
                                    Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParActive72(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                Grammar.DoVerbActive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVerbActive72(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParActive72(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                return;
                            }
                        }
                        else
                        {
                            if (FLAGS[3] == "DEP")
                            {
                                Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                return;
                            }
                            Grammar.DoVerbActive00(ENTRY, STEMS, ref TOP);
                            Grammar.DoVerbActive71(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                            return;
                        }
                    }
                    throw new NotSupportedException();
                }
            case "8":
                {
                    string a7;
                    if ((a7 = FLAGS[2]) != null)
                    {
                        if (!(a7 == "1"))
                        {
                            if (!(a7 == "2"))
                            {
                                if (a7 == "3")
                                {
                                    if (FLAGS[3] == "DEP")
                                    {
                                        Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                        Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    Grammar.DoVerbActive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVerbActive80(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                            }
                            else
                            {
                                if (FLAGS[3] == "DEP")
                                {
                                    Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                    Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                Grammar.DoVerbActive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVerbActive80(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                return;
                            }
                        }
                        else
                        {
                            if (FLAGS[3] == "DEP")
                            {
                                Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                                Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                                return;
                            }
                            Grammar.DoVerbActive00(ENTRY, STEMS, ref TOP);
                            Grammar.DoVerbActive80(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParActive00(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParPassive00(ENTRY, STEMS, ref TOP);
                            Grammar.DoVParSupine00(ENTRY, STEMS, ref TOP);
                            return;
                        }
                    }
                    throw new NotSupportedException();
                }
            case "9":
                {
                    string a8;
                    if ((a8 = FLAGS[2]) != null)
                    {
                        if (a8 == "8")
                        {
                            Grammar.DoVerbOther98(ENTRY, STEMS, ref TOP);
                            return;
                        }
                        if (a8 == "9")
                        {
                            Grammar.DoVerbOther99(ENTRY, STEMS, ref TOP);
                            return;
                        }
                    }
                    throw new NotSupportedException();
                }
        }
        throw new NotSupportedException();
    }

    public static void __Noun(int ENTRY, string[] STEMS, string[] FLAGS, ref Node TOP)
    {
        string key;
        switch (key = FLAGS[1])
        {
            case "1":
                {
                    string a;
                    if ((a = FLAGS[2]) != null)
                    {
                        if (a == "1")
                        {
                            string a2;
                            if ((a2 = FLAGS[3]) != null)
                            {
                                if (a2 == "M")
                                {
                                    Grammar.DoNounM11(ENTRY, STEMS, ref TOP);
                                    Grammar.DoNounC10(ENTRY, STEMS, ref TOP);
                                    Grammar.DoNounC11(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (!(a2 == "F"))
                                {
                                    if (!(a2 == "C"))
                                    {
                                        if (!(a2 == "N"))
                                        {
                                            goto IL_19C;
                                        }
                                    }
                                    else
                                    {
                                        Grammar.DoNounM11(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC10(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC11(ENTRY, STEMS, ref TOP);
                                    }
                                    return;
                                }
                                Grammar.DoNounC10(ENTRY, STEMS, ref TOP);
                                Grammar.DoNounC11(ENTRY, STEMS, ref TOP);
                                return;
                            }
                            IL_19C:
                            throw new NotSupportedException();
                        }
                        if (a == "6")
                        {
                            string a3;
                            if ((a3 = FLAGS[3]) != null)
                            {
                                if (a3 == "M")
                                {
                                    Grammar.DoNounC10(ENTRY, STEMS, ref TOP);
                                    Grammar.DoNounC16(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (!(a3 == "F"))
                                {
                                    if (!(a3 == "C"))
                                    {
                                        if (!(a3 == "N"))
                                        {
                                            goto IL_217;
                                        }
                                    }
                                    else
                                    {
                                        Grammar.DoNounC10(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC16(ENTRY, STEMS, ref TOP);
                                    }
                                    return;
                                }
                                Grammar.DoNounC10(ENTRY, STEMS, ref TOP);
                                Grammar.DoNounC16(ENTRY, STEMS, ref TOP);
                                return;
                            }
                            IL_217:
                            throw new NotSupportedException();
                        }
                        if (a == "7")
                        {
                            string a4;
                            if ((a4 = FLAGS[3]) != null)
                            {
                                if (a4 == "M")
                                {
                                    Grammar.DoNounC10(ENTRY, STEMS, ref TOP);
                                    Grammar.DoNounC17(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (!(a4 == "F"))
                                {
                                    if (!(a4 == "C"))
                                    {
                                        if (!(a4 == "N"))
                                        {
                                            goto IL_292;
                                        }
                                    }
                                    else
                                    {
                                        Grammar.DoNounC10(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC17(ENTRY, STEMS, ref TOP);
                                    }
                                    return;
                                }
                                Grammar.DoNounC10(ENTRY, STEMS, ref TOP);
                                Grammar.DoNounC17(ENTRY, STEMS, ref TOP);
                                return;
                            }
                            IL_292:
                            throw new NotSupportedException();
                        }
                        if (a == "8")
                        {
                            string a5;
                            if ((a5 = FLAGS[3]) != null)
                            {
                                if (a5 == "M")
                                {
                                    Grammar.DoNounM18(ENTRY, STEMS, ref TOP);
                                    Grammar.DoNounC10(ENTRY, STEMS, ref TOP);
                                    Grammar.DoNounC18(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (!(a5 == "F"))
                                {
                                    if (!(a5 == "C"))
                                    {
                                        if (!(a5 == "N"))
                                        {
                                            goto IL_330;
                                        }
                                    }
                                    else
                                    {
                                        Grammar.DoNounM18(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounF18(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC10(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC18(ENTRY, STEMS, ref TOP);
                                    }
                                    return;
                                }
                                Grammar.DoNounF18(ENTRY, STEMS, ref TOP);
                                Grammar.DoNounC10(ENTRY, STEMS, ref TOP);
                                Grammar.DoNounC18(ENTRY, STEMS, ref TOP);
                                return;
                            }
                            IL_330:
                            throw new NotSupportedException();
                        }
                    }
                    throw new NotSupportedException();
                }
            case "2":
                {
                    string key2;
                    switch (key2 = FLAGS[2])
                    {
                        case "1":
                            {
                                string a6;
                                if ((a6 = FLAGS[3]) != null)
                                {
                                    if (a6 == "M")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC21(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX21(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a6 == "F")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC21(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX21(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a6 == "C")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC21(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX21(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a6 == "N")
                                    {
                                        Grammar.DoNounN20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounN21(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX21(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                }
                                throw new NotSupportedException();
                            }
                        case "2":
                            {
                                string a7;
                                if ((a7 = FLAGS[3]) != null)
                                {
                                    if (a7 == "M")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a7 == "F")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a7 == "C")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a7 == "N")
                                    {
                                        Grammar.DoNounN20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounN22(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                }
                                throw new NotSupportedException();
                            }
                        case "3":
                            {
                                string a8;
                                if ((a8 = FLAGS[3]) != null)
                                {
                                    if (a8 == "M")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX23(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a8 == "F")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX23(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a8 == "C")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX23(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a8 == "N")
                                    {
                                        Grammar.DoNounN20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX23(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                }
                                throw new NotSupportedException();
                            }
                        case "4":
                            {
                                string a9;
                                if ((a9 = FLAGS[3]) != null)
                                {
                                    if (a9 == "M")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC24(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX24(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a9 == "F")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC24(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX24(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a9 == "C")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC24(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX24(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a9 == "N")
                                    {
                                        Grammar.DoNounN20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounN24(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX24(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                }
                                throw new NotSupportedException();
                            }
                        case "5":
                            {
                                string a10;
                                if ((a10 = FLAGS[3]) != null)
                                {
                                    if (a10 == "M")
                                    {
                                        Grammar.DoNounM25(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a10 == "F")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a10 == "C")
                                    {
                                        Grammar.DoNounM25(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a10 == "N")
                                    {
                                        Grammar.DoNounN20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                }
                                throw new NotSupportedException();
                            }
                        case "6":
                            {
                                string a11;
                                if ((a11 = FLAGS[3]) != null)
                                {
                                    if (a11 == "M")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC26(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX26(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a11 == "F")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC26(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX26(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a11 == "C")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC26(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX26(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a11 == "N")
                                    {
                                        Grammar.DoNounN20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounN26(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX26(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                }
                                throw new NotSupportedException();
                            }
                        case "7":
                            {
                                string a12;
                                if ((a12 = FLAGS[3]) != null)
                                {
                                    if (a12 == "M")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX27(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a12 == "F")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX27(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a12 == "C")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX27(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a12 == "N")
                                    {
                                        Grammar.DoNounN20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX27(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                }
                                throw new NotSupportedException();
                            }
                        case "8":
                            {
                                string a13;
                                if ((a13 = FLAGS[3]) != null)
                                {
                                    if (a13 == "M")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a13 == "F")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a13 == "C")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a13 == "N")
                                    {
                                        Grammar.DoNounN20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounN28(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                }
                                throw new NotSupportedException();
                            }
                        case "9":
                            {
                                string a14;
                                if ((a14 = FLAGS[3]) != null)
                                {
                                    if (a14 == "M")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC29(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a14 == "F")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC29(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a14 == "C")
                                    {
                                        Grammar.DoNounC20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC29(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a14 == "N")
                                    {
                                        Grammar.DoNounN20(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX20(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                }
                                throw new NotSupportedException();
                            }
                    }
                    throw new NotSupportedException();
                }
            case "3":
                {
                    string key3;
                    switch (key3 = FLAGS[2])
                    {
                        case "1":
                            {
                                string a15;
                                if ((a15 = FLAGS[3]) != null)
                                {
                                    if (a15 == "M")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC31(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a15 == "F")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC31(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a15 == "C")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC31(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a15 == "N")
                                    {
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                }
                                throw new NotSupportedException();
                            }
                        case "2":
                            {
                                string a16;
                                if ((a16 = FLAGS[3]) != null)
                                {
                                    if (a16 == "M")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a16 == "F")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a16 == "C")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a16 == "N")
                                    {
                                        Grammar.DoNounN32(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                }
                                throw new NotSupportedException();
                            }
                        case "3":
                            {
                                string a17;
                                if ((a17 = FLAGS[3]) != null)
                                {
                                    if (a17 == "M")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC33(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a17 == "F")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC33(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a17 == "C")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC33(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a17 == "N")
                                    {
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                }
                                throw new NotSupportedException();
                            }
                        case "4":
                            {
                                string a18;
                                if ((a18 = FLAGS[3]) != null)
                                {
                                    if (a18 == "M")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a18 == "F")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a18 == "C")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a18 == "N")
                                    {
                                        Grammar.DoNounN34(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                }
                                throw new NotSupportedException();
                            }
                        case "6":
                            {
                                string a19;
                                if ((a19 = FLAGS[3]) != null)
                                {
                                    if (a19 == "M")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC36(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a19 == "F")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC36(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a19 == "C")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC36(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a19 == "N")
                                    {
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                }
                                throw new NotSupportedException();
                            }
                        case "7":
                            {
                                string a20;
                                if ((a20 = FLAGS[3]) != null)
                                {
                                    if (a20 == "M")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC37(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX37(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a20 == "F")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC37(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX37(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a20 == "C")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC37(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX37(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a20 == "N")
                                    {
                                        Grammar.DoNounN37(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX37(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                }
                                throw new NotSupportedException();
                            }
                        case "8":
                            {
                                string a21;
                                if ((a21 = FLAGS[3]) != null)
                                {
                                    if (a21 == "M")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC38(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX38(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a21 == "F")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC38(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX38(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a21 == "C")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC38(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX38(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a21 == "N")
                                    {
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX38(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                }
                                throw new NotSupportedException();
                            }
                        case "9":
                            {
                                string a22;
                                if ((a22 = FLAGS[3]) != null)
                                {
                                    if (a22 == "M")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC39(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX39(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a22 == "F")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC39(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX39(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a22 == "C")
                                    {
                                        Grammar.DoNounC30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounC39(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX39(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                    if (a22 == "N")
                                    {
                                        Grammar.DoNounN39(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX30(ENTRY, STEMS, ref TOP);
                                        Grammar.DoNounX39(ENTRY, STEMS, ref TOP);
                                        return;
                                    }
                                }
                                throw new NotSupportedException();
                            }
                    }
                    throw new NotSupportedException();
                }
            case "4":
                {
                    string a23;
                    if ((a23 = FLAGS[2]) != null)
                    {
                        if (a23 == "1")
                        {
                            string a24;
                            if ((a24 = FLAGS[3]) != null)
                            {
                                if (a24 == "M")
                                {
                                    Grammar.DoNounC41(ENTRY, STEMS, ref TOP);
                                    Grammar.DoNounX40(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (a24 == "F")
                                {
                                    Grammar.DoNounC41(ENTRY, STEMS, ref TOP);
                                    Grammar.DoNounX40(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (a24 == "C")
                                {
                                    Grammar.DoNounC41(ENTRY, STEMS, ref TOP);
                                    Grammar.DoNounX40(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (a24 == "N")
                                {
                                    Grammar.DoNounX40(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                            }
                            throw new NotSupportedException();
                        }
                        if (a23 == "2")
                        {
                            string a25;
                            if ((a25 = FLAGS[3]) != null)
                            {
                                if (a25 == "M")
                                {
                                    Grammar.DoNounX40(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (a25 == "F")
                                {
                                    Grammar.DoNounX40(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (a25 == "C")
                                {
                                    Grammar.DoNounX40(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (a25 == "N")
                                {
                                    Grammar.DoNounN42(ENTRY, STEMS, ref TOP);
                                    Grammar.DoNounX40(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                            }
                            throw new NotSupportedException();
                        }
                        if (a23 == "3")
                        {
                            string a26;
                            if ((a26 = FLAGS[3]) != null)
                            {
                                if (a26 == "M")
                                {
                                    Grammar.DoNounC43(ENTRY, STEMS, ref TOP);
                                    Grammar.DoNounX40(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (a26 == "F")
                                {
                                    Grammar.DoNounC43(ENTRY, STEMS, ref TOP);
                                    Grammar.DoNounX40(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (a26 == "C")
                                {
                                    Grammar.DoNounC43(ENTRY, STEMS, ref TOP);
                                    Grammar.DoNounX40(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (a26 == "N")
                                {
                                    Grammar.DoNounX40(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                            }
                            throw new NotSupportedException();
                        }
                    }
                    throw new NotSupportedException();
                }
            case "5":
                {
                    string a27;
                    if ((a27 = FLAGS[2]) != null && a27 == "1")
                    {
                        string a28;
                        if ((a28 = FLAGS[3]) != null)
                        {
                            if (a28 == "M")
                            {
                                Grammar.DoNounC51(ENTRY, STEMS, ref TOP);
                                return;
                            }
                            if (!(a28 == "F"))
                            {
                                if (!(a28 == "C"))
                                {
                                    if (!(a28 == "N"))
                                    {
                                        goto IL_129C;
                                    }
                                }
                                else
                                {
                                    Grammar.DoNounC51(ENTRY, STEMS, ref TOP);
                                }
                                return;
                            }
                            Grammar.DoNounC51(ENTRY, STEMS, ref TOP);
                            return;
                        }
                        IL_129C:
                        throw new NotSupportedException();
                    }
                    throw new NotSupportedException();
                }
            case "6":
                {
                    string arg_12AB_0 = FLAGS[2];
                    throw new NotSupportedException();
                }
            case "7":
                {
                    string arg_12B5_0 = FLAGS[2];
                    throw new NotSupportedException();
                }
            case "8":
                {
                    string arg_12BF_0 = FLAGS[2];
                    throw new NotSupportedException();
                }
            case "9":
                {
                    string a29;
                    if ((a29 = FLAGS[2]) != null)
                    {
                        if (a29 == "8")
                        {
                            string a30;
                            if ((a30 = FLAGS[3]) != null)
                            {
                                if (a30 == "M")
                                {
                                    Grammar.DoNounX98(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (a30 == "F")
                                {
                                    Grammar.DoNounX98(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (a30 == "C")
                                {
                                    Grammar.DoNounX98(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (a30 == "N")
                                {
                                    Grammar.DoNounX98(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                            }
                            throw new NotSupportedException();
                        }
                        if (a29 == "9")
                        {
                            string a31;
                            if ((a31 = FLAGS[3]) != null)
                            {
                                if (a31 == "M")
                                {
                                    Grammar.DoNounX99(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (a31 == "F")
                                {
                                    Grammar.DoNounX99(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (a31 == "C")
                                {
                                    Grammar.DoNounX99(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                                if (a31 == "N")
                                {
                                    Grammar.DoNounX99(ENTRY, STEMS, ref TOP);
                                    return;
                                }
                            }
                            throw new NotSupportedException();
                        }
                    }
                    throw new NotSupportedException();
                }
        }
        throw new NotSupportedException();
    }

    public static void __Adjective(int ENTRY, string[] STEMS, string[] FLAGS, ref Node TOP)
    {
        string key;
        switch (key = FLAGS[1])
        {
            case "0":
                {
                    string a;
                    if ((a = FLAGS[2]) != null && a == "0")
                    {
                        if (STEMS.Length == 4)
                        {
                            Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                            Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                        }
                        return;
                    }
                    throw new NotSupportedException();
                }
            case "1":
                {
                    string key2;
                    switch (key2 = FLAGS[2])
                    {
                        case "0":
                            Grammar.DoAdjective10(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "1":
                            Grammar.DoAdjective10(ENTRY, STEMS, ref TOP);
                            Grammar.DoAdjective11(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "2":
                            Grammar.DoAdjective10(ENTRY, STEMS, ref TOP);
                            Grammar.DoAdjective12(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "3":
                            Grammar.DoAdjective10(ENTRY, STEMS, ref TOP);
                            Grammar.DoAdjective13(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "4":
                            Grammar.DoAdjective10(ENTRY, STEMS, ref TOP);
                            Grammar.DoAdjective14(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "5":
                            Grammar.DoAdjective10(ENTRY, STEMS, ref TOP);
                            Grammar.DoAdjective15(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "6":
                            Grammar.DoAdjective10(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "7":
                            Grammar.DoAdjective10(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "8":
                            Grammar.DoAdjective10(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "9":
                            Grammar.DoAdjective10(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                    }
                    throw new NotSupportedException();
                }
            case "2":
                {
                    string key3;
                    switch (key3 = FLAGS[2])
                    {
                        case "0":
                            Grammar.DoAdjective20(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "1":
                            Grammar.DoAdjective20(ENTRY, STEMS, ref TOP);
                            Grammar.DoAdjective21(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "2":
                            Grammar.DoAdjective20(ENTRY, STEMS, ref TOP);
                            Grammar.DoAdjective22(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "3":
                            Grammar.DoAdjective20(ENTRY, STEMS, ref TOP);
                            Grammar.DoAdjective23(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "4":
                            Grammar.DoAdjective20(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "5":
                            Grammar.DoAdjective20(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "6":
                            Grammar.DoAdjective20(ENTRY, STEMS, ref TOP);
                            Grammar.DoAdjective26(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "7":
                            Grammar.DoAdjective20(ENTRY, STEMS, ref TOP);
                            Grammar.DoAdjective27(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "8":
                            Grammar.DoAdjective20(ENTRY, STEMS, ref TOP);
                            Grammar.DoAdjective28(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "9":
                            Grammar.DoAdjective20(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                    }
                    throw new NotSupportedException();
                }
            case "3":
                {
                    string key4;
                    switch (key4 = FLAGS[2])
                    {
                        case "0":
                            Grammar.DoAdjective30(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "1":
                            Grammar.DoAdjective30(ENTRY, STEMS, ref TOP);
                            Grammar.DoAdjective31(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "2":
                            Grammar.DoAdjective30(ENTRY, STEMS, ref TOP);
                            Grammar.DoAdjective32(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "3":
                            Grammar.DoAdjective30(ENTRY, STEMS, ref TOP);
                            Grammar.DoAdjective33(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "4":
                            Grammar.DoAdjective30(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "5":
                            Grammar.DoAdjective30(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "6":
                            Grammar.DoAdjective30(ENTRY, STEMS, ref TOP);
                            Grammar.DoAdjective36(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "7":
                            Grammar.DoAdjective30(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "8":
                            Grammar.DoAdjective30(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "9":
                            Grammar.DoAdjective30(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                    }
                    throw new NotSupportedException();
                }
            case "4":
                {
                    string key5;
                    switch (key5 = FLAGS[2])
                    {
                        case "0":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "1":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "2":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "3":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "4":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "5":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "6":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "7":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "8":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "9":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                    }
                    throw new NotSupportedException();
                }
            case "5":
                {
                    string key6;
                    switch (key6 = FLAGS[2])
                    {
                        case "0":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "1":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "2":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "3":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "4":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "5":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "6":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "7":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "8":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "9":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                    }
                    throw new NotSupportedException();
                }
            case "6":
                {
                    string key7;
                    switch (key7 = FLAGS[2])
                    {
                        case "0":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "1":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "2":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "3":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "4":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "5":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "6":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "7":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "8":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "9":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                    }
                    throw new NotSupportedException();
                }
            case "7":
                {
                    string key8;
                    switch (key8 = FLAGS[2])
                    {
                        case "0":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "1":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "2":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "3":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "4":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "5":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "6":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "7":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "8":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "9":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                    }
                    throw new NotSupportedException();
                }
            case "8":
                {
                    string key9;
                    switch (key9 = FLAGS[2])
                    {
                        case "0":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "1":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "2":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "3":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "4":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "5":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "6":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "7":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "8":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "9":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                    }
                    throw new NotSupportedException();
                }
            case "9":
                {
                    string key10;
                    switch (key10 = FLAGS[2])
                    {
                        case "0":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "1":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "2":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "3":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "4":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "5":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "6":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "7":
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "8":
                            Grammar.DoAdjective98(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                        case "9":
                            Grammar.DoAdjective99(ENTRY, STEMS, ref TOP);
                            if (STEMS.Length == 4)
                            {
                                Grammar.DoAdjectiveSUPER00(ENTRY, STEMS, ref TOP);
                                Grammar.DoAdjectiveCOMP00(ENTRY, STEMS, ref TOP);
                            }
                            return;
                    }
                    throw new NotSupportedException();
                }
        }
        throw new NotSupportedException();
    }

    public static void __Pronoun(int ENTRY, string[] STEMS, string[] FLAGS, ref Node TOP)
    {
        string key;
        switch (key = FLAGS[1])
        {
            case "0":
                {
                    string arg_E7_0 = FLAGS[2];
                    throw new NotSupportedException();
                }
            case "1":
                {
                    string arg_F1_0 = FLAGS[2];
                    throw new NotSupportedException();
                }
            case "2":
                {
                    string arg_FB_0 = FLAGS[2];
                    throw new NotSupportedException();
                }
            case "3":
                {
                    string a;
                    if ((a = FLAGS[2]) != null && a == "1")
                    {
                        Grammar.DoPronoun30(ENTRY, STEMS, ref TOP);
                        Grammar.DoPronoun31(ENTRY, STEMS, ref TOP);
                        return;
                    }
                    throw new NotSupportedException();
                }
            case "4":
                {
                    string a2;
                    if ((a2 = FLAGS[2]) != null)
                    {
                        if (a2 == "1")
                        {
                            Grammar.DoPronoun40(ENTRY, STEMS, ref TOP);
                            Grammar.DoPronoun41(ENTRY, STEMS, ref TOP);
                            return;
                        }
                        if (a2 == "2")
                        {
                            Grammar.DoPronoun40(ENTRY, STEMS, ref TOP);
                            Grammar.DoPronoun42(ENTRY, STEMS, ref TOP);
                            return;
                        }
                    }
                    throw new NotSupportedException();
                }
            case "5":
                {
                    string a3;
                    if ((a3 = FLAGS[2]) != null)
                    {
                        if (a3 == "1")
                        {
                            Grammar.DoPronoun51(ENTRY, STEMS, ref TOP);
                            return;
                        }
                        if (a3 == "2")
                        {
                            Grammar.DoPronoun52(ENTRY, STEMS, ref TOP);
                            return;
                        }
                        if (a3 == "3")
                        {
                            Grammar.DoPronoun53(ENTRY, STEMS, ref TOP);
                            return;
                        }
                        if (a3 == "4")
                        {
                            Grammar.DoPronoun54(ENTRY, STEMS, ref TOP);
                            return;
                        }
                    }
                    throw new NotSupportedException();
                }
            case "6":
                {
                    string a4;
                    if ((a4 = FLAGS[2]) != null)
                    {
                        if (a4 == "1")
                        {
                            Grammar.DoPronoun60(ENTRY, STEMS, ref TOP);
                            Grammar.DoPronoun61(ENTRY, STEMS, ref TOP);
                            return;
                        }
                        if (a4 == "2")
                        {
                            Grammar.DoPronoun60(ENTRY, STEMS, ref TOP);
                            Grammar.DoPronoun62(ENTRY, STEMS, ref TOP);
                            return;
                        }
                    }
                    throw new NotSupportedException();
                }
            case "7":
                {
                    string a5;
                    if ((a5 = FLAGS[2]) != null)
                    {
                        if (a5 == "1")
                        {
                            Grammar.DoPronoun70(ENTRY, STEMS, ref TOP);
                            Grammar.DoPronoun71(ENTRY, STEMS, ref TOP);
                            return;
                        }
                        if (a5 == "2")
                        {
                            Grammar.DoPronoun70(ENTRY, STEMS, ref TOP);
                            Grammar.DoPronoun72(ENTRY, STEMS, ref TOP);
                            return;
                        }
                    }
                    throw new NotSupportedException();
                }
            case "8":
                {
                    string a6;
                    if ((a6 = FLAGS[2]) != null)
                    {
                        if (a6 == "1")
                        {
                            Grammar.DoPronoun80(ENTRY, STEMS, ref TOP);
                            Grammar.DoPronoun81(ENTRY, STEMS, ref TOP);
                            return;
                        }
                        if (a6 == "2")
                        {
                            Grammar.DoPronoun80(ENTRY, STEMS, ref TOP);
                            Grammar.DoPronoun82(ENTRY, STEMS, ref TOP);
                            return;
                        }
                    }
                    throw new NotSupportedException();
                }
            case "9":
                {
                    string arg_2D1_0 = FLAGS[2];
                    throw new NotSupportedException();
                }
        }
        throw new NotSupportedException();
    }

    static Dictionary<string, int> dict;

    public static void __Number(int ENTRY, string[] STEMS, string[] FLAGS, ref Node TOP)
    {
        if (FLAGS[3] != "CARD" && FLAGS[3] != "X")
        {
            return;
        }
        string key;
        switch (key = FLAGS[1])
        {
            case "1":
                {
                    string key2;
                    if ((key2 = FLAGS[2]) != null)
                    {
                        if (dict == null)
                        {

                            dict = new Dictionary<string, int>(6)
                        {
                        {
                            "0",
                            0
                        },
                        {
                            "1",
                            1
                        },
                        {
                            "2",
                            2
                        },
                        {
                            "3",
                            3
                        },
                        {
                            "4",
                            4
                        },
                        {
                            "5",
                            5
                        }
                    };
                        }
                        int num2;
                        if (dict.TryGetValue(key2, out num2))
			        	{
                            switch (num2)
                            {
                                case 0:
                                    return;
                                case 1:
                                    Grammar.DoNumberCARD11(ENTRY, STEMS, ref TOP);
                                    return;
                                case 2:
                                    Grammar.DoNumberCARD12(ENTRY, STEMS, ref TOP);
                                    return;
                                case 3:
                                    Grammar.DoNumberCARD13(ENTRY, STEMS, ref TOP);
                                    return;
                                case 4:
                                    Grammar.DoNumberCARD14(ENTRY, STEMS, ref TOP);
                                    break;
                                case 5:
                                    break;
                                default:
                                    goto IL_1AD;
                            }
                            return;
                        }
                    }
                    IL_1AD:
                    throw new NotSupportedException();
                }
            case "2":
                {
                    string a;
                    if ((a = FLAGS[2]) != null)
                    {
                        if (a == "0")
                        {
                            Grammar.DoNumberCARD20(ENTRY, STEMS, ref TOP);
                            return;
                        }
                        if (a == "1")
                        {
                            Grammar.DoNumberCARD20(ENTRY, STEMS, ref TOP);
                            return;
                        }
                        if (a == "2")
                        {
                            Grammar.DoNumberCARD20(ENTRY, STEMS, ref TOP);
                            return;
                        }
                        if (a == "3")
                        {
                            Grammar.DoNumberCARD20(ENTRY, STEMS, ref TOP);
                            return;
                        }
                        if (a == "4")
                        {
                            Grammar.DoNumberCARD20(ENTRY, STEMS, ref TOP);
                            return;
                        }
                    }
                    throw new NotSupportedException();
                }
            case "3":
                {
                    string arg_239_0 = FLAGS[2];
                    throw new NotSupportedException();
                }
            case "4":
                {
                    string arg_243_0 = FLAGS[2];
                    throw new NotSupportedException();
                }
            case "5":
                {
                    string arg_24D_0 = FLAGS[2];
                    throw new NotSupportedException();
                }
            case "6":
                {
                    string arg_257_0 = FLAGS[2];
                    throw new NotSupportedException();
                }
            case "7":
                {
                    string arg_261_0 = FLAGS[2];
                    throw new NotSupportedException();
                }
            case "8":
                {
                    string arg_26B_0 = FLAGS[2];
                    throw new NotSupportedException();
                }
            case "9":
                {
                    string arg_275_0 = FLAGS[2];
                    throw new NotSupportedException();
                }
        }
        throw new NotSupportedException();
    }

    public static void DoAdjective10(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "a", Attributes.Vocative | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Accusative | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "orum", Attributes.Genitive | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Nominative | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Ablative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Vocative | Attributes.Feminine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "as", Attributes.Accusative | Attributes.Feminine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "arum", Attributes.Genitive | Attributes.Feminine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Nominative | Attributes.Feminine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Vocative | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Ablative | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "am", Attributes.Accusative | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Nominative | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Vocative | Attributes.Musculine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "os", Attributes.Accusative | Attributes.Musculine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "orum", Attributes.Genitive | Attributes.Musculine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Nominative | Attributes.Musculine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Ablative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Accusative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
        }
    }

    public static void DoAdjective11(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "e", Attributes.Vocative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "us", Attributes.Nominative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "um", Attributes.Vocative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Accusative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Dative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Nominative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Dative | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Genitive | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Dative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
        }
    }

    public static void DoAdjective12(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.Vocative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "", Attributes.Nominative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "um", Attributes.Vocative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Accusative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Dative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Nominative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Dative | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Genitive | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Dative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
        }
    }

    public static void DoAdjective13(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "us", Attributes.Vocative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "us", Attributes.Nominative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "um", Attributes.Vocative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Accusative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Nominative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ius", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
        }
    }

    public static void DoAdjective14(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.Vocative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "", Attributes.Nominative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "um", Attributes.Vocative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Accusative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Nominative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ius", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
        }
    }

    public static void DoAdjective15(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "us", Attributes.Vocative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "us", Attributes.Nominative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "eis", Attributes.Ablative | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ud", Attributes.Vocative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ut", Attributes.Accusative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "d", Attributes.Accusative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ud", Attributes.Accusative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "s", Attributes.Genitive | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ut", Attributes.Nominative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "d", Attributes.Nominative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ud", Attributes.Nominative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "", Attributes.Dative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Dative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "enus", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "us", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
        }
    }

    public static void DoAdjective20(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "a", Attributes.Vocative | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Accusative | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "orum", Attributes.Genitive | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Nominative | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Vocative | Attributes.Feminine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "as", Attributes.Accusative | Attributes.Feminine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "arum", Attributes.Genitive | Attributes.Feminine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Nominative | Attributes.Feminine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Vocative | Attributes.Musculine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "os", Attributes.Accusative | Attributes.Musculine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "orum", Attributes.Genitive | Attributes.Musculine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Nominative | Attributes.Musculine | Attributes.Positive | Attributes.Plural, TOP);
        }
    }

    public static void DoAdjective21(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "e", Attributes.Vocative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "e", Attributes.Nominative | Attributes.Feminine | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "en", Attributes.Accusative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Ablative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Dative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "es", Attributes.Locative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "es", Attributes.Genitive | Attributes.Feminine | Attributes.Singular, TOP);
        }
    }

    public static void DoAdjective22(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "a", Attributes.Vocative | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Ablative | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "am", Attributes.Accusative | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Dative | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Genitive | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Nominative | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
        }
    }

    public static void DoAdjective23(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "a", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "e", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "es", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "am", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "en", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Locative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
        }
    }

    public static void DoAdjective26(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "e", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "os", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "on", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
        }
    }

    public static void DoAdjective27(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "e", Attributes.Vocative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "os", Attributes.Nominative | Attributes.Musculine | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "on", Attributes.Accusative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Ablative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Dative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Musculine | Attributes.Singular, TOP);
        }
    }

    public static void DoAdjective28(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "on", Attributes.Nominative | Attributes.Neuter | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "on", Attributes.Accusative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Ablative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Dative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "on", Attributes.Vocative | Attributes.Neuter | Attributes.Singular, TOP);
        }
    }

    public static void DoAdjective30(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "ia", Attributes.Vocative | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ia", Attributes.Accusative | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ia", Attributes.Nominative | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "es", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ibus", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "es", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ibus", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ium", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "es", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "em", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
        }
    }

    public static void DoAdjective31(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.Accusative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
        }
    }

    public static void DoAdjective32(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "is", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "is", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "eis", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eis", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Vocative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Accusative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Nominative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
        }
    }

    public static void DoAdjective33(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.Vocative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "", Attributes.Nominative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "e", Attributes.Vocative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Accusative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Nominative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Vocative | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Nominative | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
        }
    }

    public static void DoAdjective36(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "as", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Positive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "", Attributes.Accusative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Positive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "os", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, TOP);
        }
    }

    public static void DoAdjective98(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.Musculine | Attributes.Feminine | Attributes.Neuter, TOP);
        }
    }

    public static void DoAdjective99(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.Musculine | Attributes.Feminine | Attributes.Neuter, TOP);
        }
    }

    public static void DoAdjectiveCOMP00(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem2 = STEMS[2];
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "ora", Attributes.Vocative | Attributes.Neuter | Attributes.Comparative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "ora", Attributes.Accusative | Attributes.Neuter | Attributes.Comparative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "ora", Attributes.Nominative | Attributes.Neuter | Attributes.Comparative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "us", Attributes.Vocative | Attributes.Neuter | Attributes.Comparative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "us", Attributes.Accusative | Attributes.Neuter | Attributes.Comparative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "oris", Attributes.Genitive | Attributes.Neuter | Attributes.Comparative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "us", Attributes.Nominative | Attributes.Neuter | Attributes.Comparative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "ores", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Comparative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "oribus", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Comparative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "ores", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Comparative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "oribus", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Comparative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "orum", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Comparative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "ores", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Comparative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "or", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Comparative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "ori", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Comparative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "ore", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Comparative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "orem", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Comparative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "ori", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Comparative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "oris", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Comparative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "or", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Comparative | Attributes.Singular, TOP);
        }
    }

    public static void DoAdjectiveSUPER00(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem3 = STEMS[3];
        if (stem3 != "-")
        {
            TOP = new Node(ENTRY, stem3, "ma", Attributes.Vocative | Attributes.Neuter | Attributes.Superlative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "ma", Attributes.Accusative | Attributes.Neuter | Attributes.Superlative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "morum", Attributes.Genitive | Attributes.Neuter | Attributes.Superlative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "ma", Attributes.Nominative | Attributes.Neuter | Attributes.Superlative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "mum", Attributes.Vocative | Attributes.Neuter | Attributes.Superlative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "mo", Attributes.Ablative | Attributes.Neuter | Attributes.Superlative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "mum", Attributes.Accusative | Attributes.Neuter | Attributes.Superlative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "mo", Attributes.Dative | Attributes.Neuter | Attributes.Superlative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "mi", Attributes.Genitive | Attributes.Neuter | Attributes.Superlative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "mum", Attributes.Nominative | Attributes.Neuter | Attributes.Superlative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "mae", Attributes.Vocative | Attributes.Feminine | Attributes.Superlative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "mas", Attributes.Accusative | Attributes.Feminine | Attributes.Superlative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "marum", Attributes.Genitive | Attributes.Feminine | Attributes.Superlative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "mae", Attributes.Nominative | Attributes.Feminine | Attributes.Superlative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "ma", Attributes.Vocative | Attributes.Feminine | Attributes.Superlative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "ma", Attributes.Ablative | Attributes.Feminine | Attributes.Superlative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "mam", Attributes.Accusative | Attributes.Feminine | Attributes.Superlative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "mae", Attributes.Dative | Attributes.Feminine | Attributes.Superlative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "mae", Attributes.Genitive | Attributes.Feminine | Attributes.Superlative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "ma", Attributes.Nominative | Attributes.Feminine | Attributes.Superlative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "mi", Attributes.Vocative | Attributes.Musculine | Attributes.Superlative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "mis", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Superlative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "mos", Attributes.Accusative | Attributes.Musculine | Attributes.Superlative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "mis", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Superlative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "morum", Attributes.Genitive | Attributes.Musculine | Attributes.Superlative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "mi", Attributes.Nominative | Attributes.Musculine | Attributes.Superlative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "me", Attributes.Vocative | Attributes.Musculine | Attributes.Superlative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "mo", Attributes.Ablative | Attributes.Musculine | Attributes.Superlative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "mum", Attributes.Accusative | Attributes.Musculine | Attributes.Superlative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "mo", Attributes.Dative | Attributes.Musculine | Attributes.Superlative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "mi", Attributes.Genitive | Attributes.Musculine | Attributes.Superlative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "mus", Attributes.Nominative | Attributes.Musculine | Attributes.Superlative | Attributes.Singular, TOP);
        }
    }

    public static void DoADVCOMP1(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.None, TOP);
        }
    }

    public static void DoADVPOS1(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.None, TOP);
        }
    }

    public static void DoADVSUPER1(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.None, TOP);
        }
    }

    public static void DoADVX1(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.None, TOP);
        }
    }

    public static void DoADVX2(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.None, TOP);
        }
    }

    public static void DoADVX3(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.None, TOP);
        }
    }

    public static void DoCONJ10(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.None, TOP);
        }
    }

    public static void DoINTERJ10(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.None, TOP);
        }
    }

    public static void DoNounC10(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "ās", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "abus", Attributes.Ablative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Ablative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "abus", Attributes.Dative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Dative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Locative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ārum", Attributes.Genitive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Vocative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Nominative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ai", Attributes.Dative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Dative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounC11(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "a", Attributes.Vocative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "a", Attributes.Nominative | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "am", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ad", Attributes.Ablative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ā", Attributes.Ablative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Locative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ai", Attributes.Genitive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Genitive | Attributes.Singular, TOP);
        }
    }

    public static void DoNounC16(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "e", Attributes.Vocative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "e", Attributes.Nominative | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "en", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Ablative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "es", Attributes.Locative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "es", Attributes.Genitive | Attributes.Singular, TOP);
        }
    }

    public static void DoNounC17(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "a", Attributes.Vocative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "e", Attributes.Vocative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "es", Attributes.Nominative | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "am", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "en", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Ablative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Ablative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Locative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ai", Attributes.Genitive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Genitive | Attributes.Singular, TOP);
        }
    }

    public static void DoNounC18(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "a", Attributes.Vocative | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "a", Attributes.Ablative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "am", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "an", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Locative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ai", Attributes.Genitive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Genitive | Attributes.Singular, TOP);
        }
    }

    public static void DoNounC20(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "os", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Vocative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Nominative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "om", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Accusative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounC21(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "os", Attributes.Nominative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounC24(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "e", Attributes.Vocative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "us", Attributes.Nominative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounC26(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "on", Attributes.Accusative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounC29(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "us", Attributes.Nominative | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "un", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "u", Attributes.Vocative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounC30(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "es", Attributes.Vocative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Nominative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "es", Attributes.Nominative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Ablative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounC31(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "es", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ium", Attributes.Genitive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Genitive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "em", Attributes.Accusative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounC33(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "eis", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "es", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Genitive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ium", Attributes.Genitive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Nominative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "im", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "em", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Ablative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounC36(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "as", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Genitive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "em", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Genitive | Attributes.Singular, TOP);
        }
    }

    public static void DoNounC37(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "as", Attributes.Accusative | Attributes.Plural, TOP);
        }
    }

    public static void DoNounC38(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "is", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ium", Attributes.Genitive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Genitive | Attributes.Plural, TOP);
        }
    }

    public static void DoNounC39(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "as", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Accusative | Attributes.Plural, TOP);
        }
    }

    public static void DoNounC41(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "us", Attributes.Vocative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "us", Attributes.Nominative | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "uus", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "us", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "uus", Attributes.Vocative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "us", Attributes.Vocative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "uus", Attributes.Nominative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "us", Attributes.Nominative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "u", Attributes.Dative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ui", Attributes.Dative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "os", Attributes.Genitive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "us", Attributes.Genitive | Attributes.Singular, TOP);
        }
    }

    public static void DoNounC43(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "u", Attributes.Vocative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "us", Attributes.Nominative | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "um", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "em", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "u", Attributes.Dative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "u", Attributes.Genitive | Attributes.Singular, TOP);
        }
    }

    public static void DoNounC51(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "es", Attributes.Vocative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "es", Attributes.Nominative | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "es", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ebus", Attributes.Ablative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ebus", Attributes.Dative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "erum", Attributes.Genitive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "es", Attributes.Vocative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "es", Attributes.Nominative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "em", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Ablative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ei", Attributes.Dative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ei", Attributes.Genitive | Attributes.Singular, TOP);
        }
    }

    public static void DoNounF18(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "a", Attributes.Nominative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounM11(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "as", Attributes.Nominative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounM18(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "as", Attributes.Nominative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounM25(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.Vocative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "us", Attributes.Nominative | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "um", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "", Attributes.Genitive | Attributes.Singular, TOP);
        }
    }

    public static void DoNounN20(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "a", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Vocative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Nominative | Attributes.Plural, TOP);
        }
    }

    public static void DoNounN21(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "e", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Nominative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "us", Attributes.Accusative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounN22(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "um", Attributes.Vocative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "om", Attributes.Nominative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "um", Attributes.Nominative | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "um", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Singular, TOP);
        }
    }

    public static void DoNounN24(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "um", Attributes.Vocative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "um", Attributes.Nominative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounN26(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "e", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Nominative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "os", Attributes.Accusative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounN28(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "on", Attributes.Nominative | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "on", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "on", Attributes.Vocative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounN32(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        string stem2 = STEMS[0];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "a", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ium", Attributes.Genitive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Genitive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Vocative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Nominative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Ablative | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "", Attributes.Accusative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounN34(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        string stem2 = STEMS[0];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "ia", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Genitive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ium", Attributes.Genitive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ia", Attributes.Vocative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ia", Attributes.Nominative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Ablative | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "", Attributes.Accusative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounN37(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "a", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Vocative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Nominative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Ablative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounN39(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "a", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Vocative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Nominative | Attributes.Plural, TOP);
        }
    }

    public static void DoNounN42(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "u", Attributes.Vocative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "u", Attributes.Nominative | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "ua", Attributes.Accusative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ua", Attributes.Vocative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ua", Attributes.Nominative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "u", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "u", Attributes.Dative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "us", Attributes.Genitive | Attributes.Singular, TOP);
        }
    }

    public static void DoNounX20(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "is", Attributes.Ablative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Dative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Locative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "orum", Attributes.Genitive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Ablative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Dative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Locative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounX21(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "e", Attributes.Vocative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "us", Attributes.Nominative | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Singular, TOP);
        }
    }

    public static void DoNounX23(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.Vocative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "", Attributes.Nominative | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Singular, TOP);
        }
    }

    public static void DoNounX24(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "um", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "", Attributes.Genitive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Singular, TOP);
        }
    }

    public static void DoNounX26(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "e", Attributes.Vocative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "os", Attributes.Nominative | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "oe", Attributes.Nominative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Singular, TOP);
        }
    }

    public static void DoNounX27(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.Vocative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "", Attributes.Nominative | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "o", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "yn", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "on", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "yos", Attributes.Genitive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Genitive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "", Attributes.Vocative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounX30(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.Vocative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "", Attributes.Nominative | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "ibus", Attributes.Ablative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ibus", Attributes.Dative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ibus", Attributes.Locative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Dative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Dative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Locative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Locative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Genitive | Attributes.Singular, TOP);
        }
    }

    public static void DoNounX37(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "um", Attributes.Genitive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "em", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "os", Attributes.Genitive | Attributes.Singular, TOP);
        }
    }

    public static void DoNounX38(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "en", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "es", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "em", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "os", Attributes.Genitive | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "eu", Attributes.Vocative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Vocative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounX39(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "a", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "on", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "in", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "is", Attributes.Nominative | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "on", Attributes.Genitive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ium", Attributes.Genitive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "on", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "in", Attributes.Accusative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Ablative | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "os", Attributes.Genitive | Attributes.Singular, TOP);
        }
    }

    public static void DoNounX40(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "ubus", Attributes.Ablative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ibus", Attributes.Ablative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ubus", Attributes.Dative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ibus", Attributes.Dative | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Genitive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "uum", Attributes.Genitive | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "u", Attributes.Ablative | Attributes.Singular, TOP);
        }
    }

    public static void DoNounX98(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.None, TOP);
        }
    }

    public static void DoNounX99(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.None, TOP);
        }
    }

    public static void DoNumberADVERB11(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem3 = STEMS[3];
        if (stem3 != "-")
        {
            TOP = new Node(ENTRY, stem3, "", Attributes.Musculine | Attributes.Feminine | Attributes.Neuter, TOP);
        }
    }

    public static void DoNumberADVERB12(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem3 = STEMS[3];
        if (stem3 != "-")
        {
            TOP = new Node(ENTRY, stem3, "", Attributes.Musculine | Attributes.Feminine | Attributes.Neuter, TOP);
        }
    }

    public static void DoNumberADVERB13(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem3 = STEMS[3];
        if (stem3 != "-")
        {
            TOP = new Node(ENTRY, stem3, "", Attributes.Musculine | Attributes.Feminine | Attributes.Neuter, TOP);
        }
    }

    public static void DoNumberADVERB14(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem3 = STEMS[3];
        if (stem3 != "-")
        {
            TOP = new Node(ENTRY, stem3, "iens", Attributes.Musculine | Attributes.Feminine | Attributes.Neuter, TOP);
            TOP = new Node(ENTRY, stem3, "ies", Attributes.Musculine | Attributes.Feminine | Attributes.Neuter, TOP);
        }
    }

    public static void DoNumberADVERB20(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem3 = STEMS[3];
        if (stem3 != "-")
        {
            TOP = new Node(ENTRY, stem3, "iens", Attributes.Musculine | Attributes.Feminine | Attributes.Neuter, TOP);
            TOP = new Node(ENTRY, stem3, "ies", Attributes.Musculine | Attributes.Feminine | Attributes.Neuter, TOP);
        }
    }

    public static void DoNumberCARD11(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "um", Attributes.Vocative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "o", Attributes.Ablative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "um", Attributes.Accusative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "um", Attributes.Nominative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "a", Attributes.Vocative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "a", Attributes.Ablative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "am", Attributes.Accusative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "a", Attributes.Nominative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "e", Attributes.Vocative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "o", Attributes.Ablative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "um", Attributes.Accusative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "i", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ius", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "us", Attributes.Nominative | Attributes.Musculine | Attributes.Singular, TOP);
        }
    }

    public static void DoNumberCARD12(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "o", Attributes.Vocative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "obus", Attributes.Ablative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "o", Attributes.Accusative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "obus", Attributes.Dative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "om", Attributes.Genitive | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "um", Attributes.Genitive | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "orum", Attributes.Genitive | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "o", Attributes.Nominative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ae", Attributes.Vocative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "abus", Attributes.Ablative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "as", Attributes.Accusative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "abus", Attributes.Dative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "arum", Attributes.Genitive | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "a", Attributes.Nominative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "o", Attributes.Nominative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ae", Attributes.Nominative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "o", Attributes.Vocative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "obus", Attributes.Ablative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "o", Attributes.Accusative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "os", Attributes.Accusative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "obus", Attributes.Dative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "om", Attributes.Genitive | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "um", Attributes.Genitive | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "orum", Attributes.Genitive | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "o", Attributes.Nominative | Attributes.Musculine | Attributes.Plural, TOP);
        }
    }

    public static void DoNumberCARD13(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "ia", Attributes.Vocative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ia", Attributes.Accusative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ia", Attributes.Nominative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "es", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ibus", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "is", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "es", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ibus", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ium", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "es", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Plural, TOP);
        }
    }

    public static void DoNumberCARD14(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "a", Attributes.Vocative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "is", Attributes.Ablative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "a", Attributes.Accusative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "is", Attributes.Dative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "um", Attributes.Genitive | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "a", Attributes.Nominative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ae", Attributes.Vocative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "is", Attributes.Ablative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "as", Attributes.Accusative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "is", Attributes.Dative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "arum", Attributes.Genitive | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ae", Attributes.Nominative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "i", Attributes.Vocative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "is", Attributes.Ablative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "os", Attributes.Accusative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "is", Attributes.Dative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "orum", Attributes.Genitive | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "um", Attributes.Genitive | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "i", Attributes.Nominative | Attributes.Musculine | Attributes.Plural, TOP);
        }
    }

    public static void DoNumberCARD20(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.Musculine | Attributes.Feminine | Attributes.Neuter, TOP);
        }
    }

    public static void DoNumberDIST00(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem2 = STEMS[2];
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "a", Attributes.Vocative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "a", Attributes.Accusative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "orum", Attributes.Genitive | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "a", Attributes.Nominative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "ae", Attributes.Vocative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "as", Attributes.Accusative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "arum", Attributes.Genitive | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "ae", Attributes.Nominative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "i", Attributes.Vocative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "is", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "os", Attributes.Accusative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "is", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "orum", Attributes.Genitive | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "um", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "i", Attributes.Nominative | Attributes.Musculine | Attributes.Plural, TOP);
        }
    }

    public static void DoNumberORD00(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "a", Attributes.Vocative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Ablative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Accusative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Dative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "orum", Attributes.Genitive | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Nominative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Vocative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Ablative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Accusative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Dative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Nominative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Vocative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Ablative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "as", Attributes.Accusative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Dative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "arum", Attributes.Genitive | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Nominative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Vocative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Ablative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "am", Attributes.Accusative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Dative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Genitive | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Nominative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Vocative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Ablative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "os", Attributes.Accusative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Dative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "orum", Attributes.Genitive | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Nominative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Vocative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Ablative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Accusative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Dative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "us", Attributes.Nominative | Attributes.Musculine | Attributes.Singular, TOP);
        }
    }

    public static void DoPREPABL1(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.None, TOP);
        }
    }

    public static void DoPREPACC1(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.None, TOP);
        }
    }

    public static void DoPREPGEN1(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.None, TOP);
        }
    }

    public static void DoPronoun10(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        string stem2 = STEMS[0];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "i", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "jus", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "orum", Attributes.Genitive | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "as", Attributes.Accusative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "arum", Attributes.Genitive | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "ae", Attributes.Nominative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "is", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "ibus", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "os", Attributes.Accusative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "is", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "ibus", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "orum", Attributes.Genitive | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "i", Attributes.Nominative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "o", Attributes.Ablative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "am", Attributes.Accusative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "o", Attributes.Ablative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "em", Attributes.Accusative | Attributes.Musculine | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun11(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "i", Attributes.Nominative | Attributes.Musculine | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun12(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "is", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun13(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "a", Attributes.Ablative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "a", Attributes.Nominative | Attributes.Feminine | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun14(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "a", Attributes.Ablative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ae", Attributes.Nominative | Attributes.Feminine | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun15(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "o", Attributes.Ablative | Attributes.Feminine | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun16(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "id", Attributes.Accusative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "id", Attributes.Nominative | Attributes.Neuter | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun17(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "od", Attributes.Accusative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "od", Attributes.Nominative | Attributes.Neuter | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun18(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "ae", Attributes.Accusative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ae", Attributes.Nominative | Attributes.Neuter | Attributes.Plural, TOP);
        }
    }

    public static void DoPronoun19(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "a", Attributes.Accusative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "a", Attributes.Nominative | Attributes.Neuter | Attributes.Plural, TOP);
        }
    }

    public static void DoPronoun30(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "aec", Attributes.Accusative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "orum", Attributes.Genitive | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "aec", Attributes.Nominative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "oc", Attributes.Ablative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "as", Attributes.Accusative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "arum", Attributes.Genitive | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ae", Attributes.Nominative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ac", Attributes.Ablative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "anc", Attributes.Accusative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "aec", Attributes.Nominative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "iis", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ibus", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "is", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "os", Attributes.Accusative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "iis", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ibus", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "is", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "orum", Attributes.Genitive | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ii", Attributes.Nominative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "i", Attributes.Nominative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "oc", Attributes.Ablative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "unc", Attributes.Accusative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ic", Attributes.Nominative | Attributes.Musculine | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "ic", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ius", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun31(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "oc", Attributes.Accusative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "oc", Attributes.Nominative | Attributes.Neuter | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun32(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "uc", Attributes.Accusative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "uc", Attributes.Nominative | Attributes.Neuter | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun40(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        string stem2 = STEMS[0];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "a", Attributes.Accusative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Nominative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Ablative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "as", Attributes.Accusative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ae", Attributes.Nominative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Ablative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Nominative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "os", Attributes.Accusative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Nominative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "o", Attributes.Ablative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ius", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "s", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "is", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "s", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "is", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "i", Attributes.Nominative | Attributes.Musculine | Attributes.Plural, TOP);
        }
    }

    public static void DoPronoun41(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "d", Attributes.Accusative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "d", Attributes.Nominative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "s", Attributes.Nominative | Attributes.Musculine | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "orum", Attributes.Genitive | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "arum", Attributes.Genitive | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "am", Attributes.Accusative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "orum", Attributes.Genitive | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Accusative | Attributes.Musculine | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun42(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.Accusative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "", Attributes.Nominative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "", Attributes.Nominative | Attributes.Musculine | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "a", Attributes.Accusative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "orun", Attributes.Genitive | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Nominative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "arun", Attributes.Genitive | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "an", Attributes.Accusative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "orun", Attributes.Genitive | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "un", Attributes.Accusative | Attributes.Musculine | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun51(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "i", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eme", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "eme", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ed", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ed", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ihi", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ei", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun52(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "ete", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ed", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ete", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ed", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ibe", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ibei", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ibi", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ui", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun53(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "obis", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "os", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "obis", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "os", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "os", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Plural, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "arum", Attributes.Genitive | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "orum", Attributes.Genitive | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "om", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "um", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Plural, TOP);
        }
    }

    public static void DoPronoun54(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "ese", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine, TOP);
            TOP = new Node(ENTRY, stem, "ese", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine, TOP);
            TOP = new Node(ENTRY, stem, "ibi", Attributes.Dative | Attributes.Musculine | Attributes.Feminine, TOP);
            TOP = new Node(ENTRY, stem, "ui", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine, TOP);
        }
    }

    public static void DoPronoun60(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "a", Attributes.Accusative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "orum", Attributes.Genitive | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "a", Attributes.Nominative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "o", Attributes.Ablative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "is", Attributes.Ablative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "as", Attributes.Accusative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "arum", Attributes.Genitive | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ae", Attributes.Nominative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "am", Attributes.Accusative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "a", Attributes.Nominative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "is", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "os", Attributes.Accusative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "is", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "orum", Attributes.Genitive | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "i", Attributes.Nominative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "o", Attributes.Ablative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "um", Attributes.Accusative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "i", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ius", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "e", Attributes.Nominative | Attributes.Musculine | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun61(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "ud", Attributes.Accusative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ud", Attributes.Nominative | Attributes.Neuter | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun62(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "ud", Attributes.Accusative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ud", Attributes.Nominative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "um", Attributes.Accusative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "um", Attributes.Nominative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "os", Attributes.Nominative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "us", Attributes.Nominative | Attributes.Musculine | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun70(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        string stem2 = STEMS[0];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "i", Attributes.Dative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "jus", Attributes.Genitive | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Dative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "jus", Attributes.Genitive | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Dative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "jus", Attributes.Genitive | Attributes.Musculine | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "ibus", Attributes.Ablative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "ae", Attributes.Accusative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "ibus", Attributes.Dative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "orum", Attributes.Genitive | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "ae", Attributes.Nominative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "ibus", Attributes.Ablative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "as", Attributes.Accusative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "ibus", Attributes.Dative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "arum", Attributes.Genitive | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "ae", Attributes.Nominative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "ibus", Attributes.Ablative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "os", Attributes.Accusative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "ibus", Attributes.Dative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "orum", Attributes.Genitive | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "i", Attributes.Nominative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "o", Attributes.Ablative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "o", Attributes.Ablative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "em", Attributes.Accusative | Attributes.Musculine | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun71(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "id", Attributes.Accusative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "id", Attributes.Nominative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "o", Attributes.Ablative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "em", Attributes.Accusative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "is", Attributes.Nominative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "is", Attributes.Nominative | Attributes.Musculine | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun72(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "od", Attributes.Accusative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "od", Attributes.Nominative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "a", Attributes.Ablative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "am", Attributes.Accusative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ae", Attributes.Nominative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "i", Attributes.Nominative | Attributes.Musculine | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun80(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "ibusdam", Attributes.Ablative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "aedam", Attributes.Accusative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ibusdam", Attributes.Dative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "orundam", Attributes.Genitive | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "aedam", Attributes.Nominative | Attributes.Neuter | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ibusdam", Attributes.Ablative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "asdam", Attributes.Accusative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ibusdam", Attributes.Dative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "arundam", Attributes.Genitive | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "aedam", Attributes.Nominative | Attributes.Feminine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ibusdam", Attributes.Ablative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "osdam", Attributes.Accusative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ibusdam", Attributes.Dative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "orundam", Attributes.Genitive | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "idam", Attributes.Nominative | Attributes.Musculine | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "odam", Attributes.Ablative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "adam", Attributes.Ablative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "andam", Attributes.Accusative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "aedam", Attributes.Nominative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "odam", Attributes.Ablative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endam", Attributes.Accusative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "idam", Attributes.Nominative | Attributes.Musculine | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "idam", Attributes.Dative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "jusdam", Attributes.Genitive | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "idam", Attributes.Dative | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "jusdam", Attributes.Genitive | Attributes.Feminine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "idam", Attributes.Dative | Attributes.Musculine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "jusdam", Attributes.Genitive | Attributes.Musculine | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun81(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "iddam", Attributes.Accusative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "iddam", Attributes.Nominative | Attributes.Neuter | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun82(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "oddam", Attributes.Accusative | Attributes.Neuter | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "oddam", Attributes.Nominative | Attributes.Neuter | Attributes.Singular, TOP);
        }
    }

    public static void DoPronoun99(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.Musculine | Attributes.Feminine | Attributes.Neuter, TOP);
        }
    }

    public static void DoVerbActive00(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem2 = STEMS[2];
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "issent", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "issetis", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "issemus", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "isset", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "isses", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "issem", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "erint", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "eritis", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "erimus", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "erit", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "eris", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "erim", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "erint", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "eritis", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "erimus", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Future | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "erit", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "eris", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "ero", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Future | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "erant", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "eratis", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "eramus", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "erat", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "eras", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "eram", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "ere", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "erunt", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "istis", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "imus", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "it", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "isti", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "i", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.First | Attributes.Singular, TOP);
        }
    }

    public static void DoVerbActive11(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        string stem2 = STEMS[2];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "ent", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "etis", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "emus", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "et", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "es", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "em", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "abunt", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "abitis", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "abimus", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "abit", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "abis", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "abo", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "abant", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "abatis", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "abamus", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "abat", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "abas", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "abam", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ant", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "o", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "āre", Attributes.Infinitive | Attributes.Active | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "anto", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "atote", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ato", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ato", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ate", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "a", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "arent", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "aretis", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "aremus", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "aret", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ares", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "arem", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "atis", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "amus", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "at", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "as", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "isse", Attributes.Infinitive | Attributes.Active | Attributes.Perfect, TOP);
        }
    }

    public static void DoVerbActive21(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        string stem2 = STEMS[2];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "eant", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "eatis", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "eamus", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "eat", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "eas", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "eam", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebunt", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebitis", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebimus", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebit", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebis", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebo", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebant", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebatis", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebamus", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebat", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebas", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebam", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ent", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "eo", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "ēre", Attributes.Infinitive | Attributes.Active | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "ento", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "etote", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eto", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "eto", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ete", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "erent", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eretis", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eremus", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eret", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "eres", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "erem", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "etis", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "emus", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "et", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "es", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "isse", Attributes.Infinitive | Attributes.Active | Attributes.Perfect, TOP);
        }
    }

    public static void DoVerbActive30(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "ant", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "atis", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "amus", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "at", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "as", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "am", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ent", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "etis", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "emus", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "et", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "es", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "am", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebant", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebatis", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebamus", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebat", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebas", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebam", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "unt", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "o", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
        }
    }

    public static void DoVerbActive31(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        string stem2 = STEMS[2];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "ere", Attributes.Infinitive | Attributes.Active | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "unto", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "itote", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ito", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ito", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ite", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "erent", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eretis", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eremus", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eret", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "eres", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "erem", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "itis", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "imus", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "it", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "isse", Attributes.Infinitive | Attributes.Active | Attributes.Perfect, TOP);
        }
    }

    public static void DoVerbActive32(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        string stem2 = STEMS[2];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "unto", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "tote", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "to", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "to", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "te", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "tis", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "imus", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "t", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "s", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "re", Attributes.Infinitive | Attributes.Active | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "rent", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "retis", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "remus", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ret", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "res", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "rem", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "isse", Attributes.Infinitive | Attributes.Active | Attributes.Perfect, TOP);
        }
    }

    public static void DoVerbActive33(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        string stem2 = STEMS[0];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "ieri", Attributes.Infinitive | Attributes.Active | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "itote", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ito", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ito", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ite", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "itis", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "imus", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "it", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "erent", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "eretis", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "eremus", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "eret", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "eres", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "erem", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
        }
    }

    public static void DoVerbActive34(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        string stem2 = STEMS[0];
        string stem3 = STEMS[2];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "īre", Attributes.Infinitive | Attributes.Active | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "itote", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ito", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ito", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ite", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "irent", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "iretis", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "iremus", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "iret", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ires", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "irem", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "bant", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "batis", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "bamus", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "bat", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "bas", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "bam", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "itis", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "imus", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "it", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "unto", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "bunt", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "bitis", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "bimus", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "bit", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "bis", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "bo", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.First | Attributes.Singular, TOP);
        }
        if (stem3 != "-")
        {
            TOP = new Node(ENTRY, stem3, "isse", Attributes.Infinitive | Attributes.Active | Attributes.Perfect, TOP);
        }
    }

    public static void DoVerbActive50(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        string stem2 = STEMS[2];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "sint", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "sitis", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "simus", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "sit", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "sis", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "sim", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "sunt", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "sumus", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "sum", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "fore", Attributes.Infinitive | Attributes.Active | Attributes.Future, TOP);
            TOP = new Node(ENTRY, stem, "erint", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "erunt", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eritis", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "erimus", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "erit", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "eris", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ero", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "erant", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eratis", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eramus", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "erat", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "eras", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "eram", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "estis", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "est", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "es", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "isse", Attributes.Infinitive | Attributes.Active | Attributes.Perfect, TOP);
        }
    }

    public static void DoVerbActive51(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        string stem2 = STEMS[0];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "esse", Attributes.Infinitive | Attributes.Active | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "estote", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "esto", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "esto", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "este", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "es", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "forent", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "foretis", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "foremus", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "foret", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "fores", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "forem", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "essent", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "essetis", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "essemus", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "esset", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "esses", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "essem", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "sunto", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
        }
    }

    public static void DoVerbActive52(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[2];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "se", Attributes.Infinitive | Attributes.Active | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem0, "sent", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "setis", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "semus", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "set", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ses", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "sem", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "isse", Attributes.Infinitive | Attributes.Active | Attributes.Perfect, TOP);
        }
    }

    public static void DoVerbActive61(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        string stem2 = STEMS[2];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "unto", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ant", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "atis", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "amus", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "at", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "as", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "am", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "unt", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "o", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "re", Attributes.Infinitive | Attributes.Active | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "tote", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "to", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "to", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "te", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "rent", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "retis", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "remus", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ret", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "res", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "rem", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ent", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "etis", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "emus", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "et", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "es", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "am", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "bunt", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "bitis", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "bimus", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "bit", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "bis", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "bo", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ebant", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ebatis", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ebamus", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ebat", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ebas", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ebam", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "bant", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "batis", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "bamus", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "bat", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "bas", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "bam", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "tis", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "mus", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "t", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "s", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "isse", Attributes.Infinitive | Attributes.Active | Attributes.Perfect, TOP);
        }
    }

    public static void DoVerbActive62(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        string stem2 = STEMS[2];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "unto", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "itote", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ito", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ito", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ite", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "i", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ent", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "etis", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "emus", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "et", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "es", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "am", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebant", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebatis", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebamus", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebat", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebas", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebam", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "unt", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "umus", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "o", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "le", Attributes.Infinitive | Attributes.Active | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "lent", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "letis", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "lemus", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "let", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "les", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "lem", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "int", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "itis", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "imus", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "it", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "im", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "isse", Attributes.Infinitive | Attributes.Active | Attributes.Perfect, TOP);
        }
    }

    public static void DoVerbActive71(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "ebant", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebatis", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebamus", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebat", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebas", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebam", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "unt", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "o", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "i", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "iant", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "iat", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ias", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "it", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ibant", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ibatis", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ibamus", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ibat", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ibas", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ibam", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "it", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
        }
    }

    public static void DoVerbActive72(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        string stem2 = STEMS[0];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "ito", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ito", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "e", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "it", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "isti", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "itis", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "imus", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "it", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "is", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "am", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "i", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "et", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "es", Attributes.Indicative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "ebat", Attributes.Indicative | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "unt", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
        }
    }

    public static void DoVerbActive73(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        string stem2 = STEMS[0];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "se", Attributes.Infinitive | Attributes.Active | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "tote", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "to", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "to", Attributes.Imerative | Attributes.Active | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "te", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "", Attributes.Imerative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "setur", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "sent", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "setis", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "semus", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "set", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ses", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "sem", Attributes.Subjunctive | Attributes.Active | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "tur", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "tis", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "t", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "", Attributes.Indicative | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "int", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "itis", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "imus", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "it", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "is", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "im", Attributes.Subjunctive | Attributes.Active | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
        }
    }

    public static void DoVerbActive80(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem2 = STEMS[2];
        string stem3 = STEMS[1];
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "ent", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "etis", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "emus", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "et", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "es", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "em", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "int", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "itis", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "imus", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "it", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "is", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "im", Attributes.Subjunctive | Attributes.Active | Attributes.Perfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "int", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "itis", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "imus", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Future | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "it", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "is", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "o", Attributes.Indicative | Attributes.Active | Attributes.Perfect | Attributes.Future | Attributes.First | Attributes.Singular, TOP);
        }
        if (stem3 != "-")
        {
            TOP = new Node(ENTRY, stem3, "e", Attributes.Infinitive | Attributes.Active | Attributes.Present, TOP);
        }
    }

    public static void DoVerbOther98(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.None, TOP);
        }
    }

    public static void DoVerbOther99(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "", Attributes.None, TOP);
        }
    }

    public static void DoVerbPassive11(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "entur", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "emini", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "emur", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "etur", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ere", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "eris", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "er", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "abuntur", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "abimini", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "abimur", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "abitur", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "abere", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "aberis", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "abor", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "abantur", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "abamini", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "abamur", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "abatur", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "abare", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "abaris", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "abar", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "antur", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "or", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "arier", Attributes.Infinitive | Attributes.Passive | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "āri", Attributes.Infinitive | Attributes.Passive | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "antor", Attributes.Imerative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ator", Attributes.Imerative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ator", Attributes.Imerative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "amini", Attributes.Imerative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "are", Attributes.Imerative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "arentur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "aremini", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "aremur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "aretur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "arere", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "areris", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "arer", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "amini", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "amur", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "atur", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "are", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "aris", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
        }
    }

    public static void DoVerbPassive21(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "eantur", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "eamini", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "eamur", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "eatur", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "eare", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "earis", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ear", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebuntur", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebimini", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebimur", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebitur", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebere", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "eberis", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebor", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebantur", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebamini", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebamur", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebatur", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebare", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebaris", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebar", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "entur", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "eor", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "erier", Attributes.Infinitive | Attributes.Passive | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "ēri", Attributes.Infinitive | Attributes.Passive | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "entor", Attributes.Imerative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "etor", Attributes.Imerative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "etor", Attributes.Imerative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "emini", Attributes.Imerative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ere", Attributes.Imerative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "erentur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eremini", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eremur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eretur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "erere", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ereris", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "erer", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "emini", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "emur", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "etur", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ere", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "eris", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
        }
    }

    public static void DoVerbPassive30(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "antur", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "amini", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "amur", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "atur", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "are", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "aris", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ar", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "entur", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "emini", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "emur", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "etur", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ere", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "eris", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ar", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebantur", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebamini", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebamur", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ebatur", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebare", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebaris", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ebar", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "untur", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "or", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "imini", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "imur", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
        }
    }

    public static void DoVerbPassive31(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "ier", Attributes.Infinitive | Attributes.Passive | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "i", Attributes.Infinitive | Attributes.Passive | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "untor", Attributes.Imerative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "itor", Attributes.Imerative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "itor", Attributes.Imerative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "imini", Attributes.Imerative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ere", Attributes.Imerative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "erentur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eremini", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eremur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eretur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "erere", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ereris", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "erer", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "itur", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ere", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "eris", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
        }
    }

    public static void DoVerbPassive32(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        string stem2 = STEMS[0];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "ri", Attributes.Infinitive | Attributes.Passive | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "rentur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "remini", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "remur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "retur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "rere", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "reris", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "rer", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "re", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ris", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "untor", Attributes.Imerative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "tor", Attributes.Imerative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "tor", Attributes.Imerative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "imini", Attributes.Imerative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "re", Attributes.Imerative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "tur", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
        }
    }

    public static void DoVerbPassive33(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "erentur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eremini", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eremur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "eretur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ereris", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "erer", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "itur", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "eris", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
        }
    }

    public static void DoVerbPassive34(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        string stem2 = STEMS[0];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "irier", Attributes.Infinitive | Attributes.Passive | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "īri", Attributes.Infinitive | Attributes.Passive | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "itor", Attributes.Imerative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "itor", Attributes.Imerative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "imini", Attributes.Imerative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ire", Attributes.Imerative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "irentur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "iremini", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "iremur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "iretur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "irere", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ireris", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "irer", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "itur", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ire", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "iris", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "untor", Attributes.Imerative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "buntur", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "bimini", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "bimur", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "berit", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "beris", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "bor", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.First | Attributes.Singular, TOP);
        }
    }

    public static void DoVerbPassive61(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        string stem = STEMS[1];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "untor", Attributes.Imerative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "antur", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ar", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "untur", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "or", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.First | Attributes.Singular, TOP);
        }
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "rier", Attributes.Infinitive | Attributes.Passive | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "ri", Attributes.Infinitive | Attributes.Passive | Attributes.Present, TOP);
            TOP = new Node(ENTRY, stem, "tor", Attributes.Imerative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "tor", Attributes.Imerative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "mini", Attributes.Imerative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ere", Attributes.Imerative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "rentur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "remini", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "remur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "retur", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "rere", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "reris", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "rer", Attributes.Subjunctive | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "amini", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "amur", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "atur", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "are", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "aris", Attributes.Subjunctive | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "entur", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "emini", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "emur", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "etur", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ere", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "eris", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ar", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "buntur", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "bimini", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "bimur", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "bitur", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "bere", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "beris", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "bor", Attributes.Indicative | Attributes.Passive | Attributes.Future | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "bantur", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "bamini", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "bamur", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "batur", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "bare", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "baris", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "bar", Attributes.Indicative | Attributes.Passive | Attributes.Imperfect | Attributes.First | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "mini", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "mur", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.First | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "tur", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Third | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "re", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ris", Attributes.Indicative | Attributes.Passive | Attributes.Present | Attributes.Second | Attributes.Singular, TOP);
        }
    }

    public static void DoVParActive00(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem3 = STEMS[3];
        if (stem3 != "-")
        {
            TOP = new Node(ENTRY, stem3, "ura", Attributes.Vocative | Attributes.Neuter | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "ura", Attributes.Accusative | Attributes.Neuter | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "urorum", Attributes.Genitive | Attributes.Neuter | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "ura", Attributes.Nominative | Attributes.Neuter | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "urum", Attributes.Vocative | Attributes.Neuter | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "uro", Attributes.Ablative | Attributes.Neuter | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "urum", Attributes.Accusative | Attributes.Neuter | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "uro", Attributes.Dative | Attributes.Neuter | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "uri", Attributes.Genitive | Attributes.Neuter | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "urum", Attributes.Nominative | Attributes.Neuter | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "urae", Attributes.Vocative | Attributes.Feminine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "uras", Attributes.Accusative | Attributes.Feminine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "urarum", Attributes.Genitive | Attributes.Feminine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "urae", Attributes.Nominative | Attributes.Feminine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "ura", Attributes.Vocative | Attributes.Feminine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "ura", Attributes.Ablative | Attributes.Feminine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "uram", Attributes.Accusative | Attributes.Feminine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "urae", Attributes.Dative | Attributes.Feminine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "urae", Attributes.Genitive | Attributes.Feminine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "ura", Attributes.Nominative | Attributes.Feminine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "uri", Attributes.Vocative | Attributes.Musculine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "uris", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "uros", Attributes.Accusative | Attributes.Musculine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "uris", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "urorum", Attributes.Genitive | Attributes.Musculine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "uri", Attributes.Nominative | Attributes.Musculine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "ure", Attributes.Vocative | Attributes.Musculine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "uro", Attributes.Ablative | Attributes.Musculine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "urum", Attributes.Accusative | Attributes.Musculine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "uro", Attributes.Dative | Attributes.Musculine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "uri", Attributes.Genitive | Attributes.Musculine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "urus", Attributes.Nominative | Attributes.Musculine | Attributes.Active | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
        }
    }

    public static void DoVParActive10(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "antia", Attributes.Vocative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "antia", Attributes.Accusative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "antia", Attributes.Nominative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ans", Attributes.Accusative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "antes", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "antibus", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "antes", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "antibus", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "antum", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "antium", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "antes", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ans", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ante", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "anti", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "antem", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "anti", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "antis", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ans", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
        }
    }

    public static void DoVParActive20(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "entia", Attributes.Vocative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entia", Attributes.Accusative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entia", Attributes.Nominative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ens", Attributes.Accusative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "entes", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entibus", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entes", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entibus", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entum", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entium", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entes", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ens", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ente", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "enti", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "entem", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "enti", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "entis", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ens", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
        }
    }

    public static void DoVParActive30(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "entia", Attributes.Vocative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entia", Attributes.Accusative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entia", Attributes.Nominative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ens", Attributes.Accusative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "entes", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entibus", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entes", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entibus", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entum", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entium", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entes", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ens", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ente", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "enti", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "entem", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "enti", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "entis", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ens", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
        }
    }

    public static void DoVParActive51(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "entia", Attributes.Vocative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "entia", Attributes.Accusative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "entia", Attributes.Nominative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ens", Attributes.Accusative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "entes", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "entibus", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "entes", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "entibus", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "entium", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "entes", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ens", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ente", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "enti", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "entem", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "enti", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "entis", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ens", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ens", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
        }
    }

    public static void DoVParActive61(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem = STEMS[1];
        string stem2 = STEMS[0];
        if (stem != "-")
        {
            TOP = new Node(ENTRY, stem, "entes", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "entibus", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "entes", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "entibus", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "entium", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "entes", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem, "ente", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "enti", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "entem", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "enti", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "entis", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ens", Attributes.Accusative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ens", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem, "ens", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
        }
        if (stem2 != "-")
        {
            TOP = new Node(ENTRY, stem2, "untia", Attributes.Vocative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "untia", Attributes.Accusative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "untia", Attributes.Nominative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "untes", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "untibus", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "untes", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "untibus", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "untium", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "untes", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem2, "unte", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "unti", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "untem", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "unti", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem2, "untis", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
        }
    }

    public static void DoVParActive62(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "entia", Attributes.Vocative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entia", Attributes.Accusative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entia", Attributes.Nominative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ens", Attributes.Accusative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "entes", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entibus", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entes", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entibus", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entium", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entes", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ens", Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ente", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "enti", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "entem", Attributes.Accusative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "enti", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "entis", Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ens", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "entia", Attributes.Vocative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entia", Attributes.Accusative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "entia", Attributes.Nominative | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
        }
    }

    public static void DoVParActive72(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "entes", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ens", Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Active | Attributes.Present | Attributes.Participle | Attributes.Singular, TOP);
        }
    }

    public static void DoVParPassive00(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem3 = STEMS[3];
        if (stem3 != "-")
        {
            TOP = new Node(ENTRY, stem3, "a", Attributes.Vocative | Attributes.Neuter | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "a", Attributes.Accusative | Attributes.Neuter | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "orum", Attributes.Genitive | Attributes.Neuter | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "a", Attributes.Nominative | Attributes.Neuter | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "um", Attributes.Vocative | Attributes.Neuter | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "o", Attributes.Ablative | Attributes.Neuter | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "um", Attributes.Accusative | Attributes.Neuter | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "o", Attributes.Dative | Attributes.Neuter | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "i", Attributes.Genitive | Attributes.Neuter | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "um", Attributes.Nominative | Attributes.Neuter | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "ae", Attributes.Vocative | Attributes.Feminine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "as", Attributes.Accusative | Attributes.Feminine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "arum", Attributes.Genitive | Attributes.Feminine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "ae", Attributes.Nominative | Attributes.Feminine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "a", Attributes.Vocative | Attributes.Feminine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "a", Attributes.Ablative | Attributes.Feminine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "am", Attributes.Accusative | Attributes.Feminine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "ae", Attributes.Dative | Attributes.Feminine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "ae", Attributes.Genitive | Attributes.Feminine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "a", Attributes.Nominative | Attributes.Feminine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "i", Attributes.Vocative | Attributes.Musculine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "is", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "os", Attributes.Accusative | Attributes.Musculine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "is", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "orum", Attributes.Genitive | Attributes.Musculine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "i", Attributes.Nominative | Attributes.Musculine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem3, "e", Attributes.Vocative | Attributes.Musculine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "o", Attributes.Ablative | Attributes.Musculine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "um", Attributes.Accusative | Attributes.Musculine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "o", Attributes.Dative | Attributes.Musculine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "i", Attributes.Genitive | Attributes.Musculine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "us", Attributes.Nominative | Attributes.Musculine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, TOP);
        }
    }

    public static void DoVParPassive10(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "anda", Attributes.Vocative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "anda", Attributes.Accusative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "andorum", Attributes.Genitive | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "anda", Attributes.Nominative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "andum", Attributes.Vocative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ando", Attributes.Ablative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "andum", Attributes.Accusative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ando", Attributes.Dative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "andi", Attributes.Genitive | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "andum", Attributes.Nominative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "andae", Attributes.Vocative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "andas", Attributes.Accusative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "andarum", Attributes.Genitive | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "andae", Attributes.Nominative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "anda", Attributes.Vocative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "anda", Attributes.Ablative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "andam", Attributes.Accusative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "andae", Attributes.Dative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "andae", Attributes.Genitive | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "anda", Attributes.Nominative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "andi", Attributes.Vocative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "andis", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "andos", Attributes.Accusative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "andis", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "andorum", Attributes.Genitive | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "andi", Attributes.Nominative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ande", Attributes.Vocative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ando", Attributes.Ablative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "andum", Attributes.Accusative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "ando", Attributes.Dative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "andi", Attributes.Genitive | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "andus", Attributes.Nominative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
        }
    }

    public static void DoVParPassive20(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "enda", Attributes.Vocative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "enda", Attributes.Accusative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endorum", Attributes.Genitive | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "enda", Attributes.Nominative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endum", Attributes.Vocative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endo", Attributes.Ablative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endum", Attributes.Accusative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endo", Attributes.Dative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endi", Attributes.Genitive | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endum", Attributes.Nominative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endae", Attributes.Vocative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endas", Attributes.Accusative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endarum", Attributes.Genitive | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endae", Attributes.Nominative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "enda", Attributes.Vocative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "enda", Attributes.Ablative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endam", Attributes.Accusative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endae", Attributes.Dative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endae", Attributes.Genitive | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "enda", Attributes.Nominative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endi", Attributes.Vocative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endis", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endos", Attributes.Accusative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endis", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endorum", Attributes.Genitive | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endi", Attributes.Nominative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ende", Attributes.Vocative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endo", Attributes.Ablative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endum", Attributes.Accusative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endo", Attributes.Dative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endi", Attributes.Genitive | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endus", Attributes.Nominative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
        }
    }

    public static void DoVParPassive30(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "unda", Attributes.Vocative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "unda", Attributes.Accusative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undorum", Attributes.Genitive | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "unda", Attributes.Nominative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undum", Attributes.Vocative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undo", Attributes.Ablative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undum", Attributes.Accusative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undo", Attributes.Dative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undi", Attributes.Genitive | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undum", Attributes.Nominative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undae", Attributes.Vocative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undas", Attributes.Accusative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undarum", Attributes.Genitive | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undae", Attributes.Nominative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "unda", Attributes.Vocative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "unda", Attributes.Ablative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undam", Attributes.Accusative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undae", Attributes.Dative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undae", Attributes.Genitive | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "unda", Attributes.Nominative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undi", Attributes.Vocative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undis", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undos", Attributes.Accusative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undis", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undorum", Attributes.Genitive | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undi", Attributes.Nominative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "unde", Attributes.Vocative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undo", Attributes.Ablative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undum", Attributes.Accusative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undo", Attributes.Dative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undi", Attributes.Genitive | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undus", Attributes.Nominative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "enda", Attributes.Vocative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "enda", Attributes.Accusative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endorum", Attributes.Genitive | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "enda", Attributes.Nominative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endum", Attributes.Vocative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endo", Attributes.Ablative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endum", Attributes.Accusative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endo", Attributes.Dative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endi", Attributes.Genitive | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endum", Attributes.Nominative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endae", Attributes.Vocative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endas", Attributes.Accusative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endarum", Attributes.Genitive | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endae", Attributes.Nominative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "enda", Attributes.Vocative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "enda", Attributes.Ablative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endam", Attributes.Accusative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endae", Attributes.Dative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endae", Attributes.Genitive | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "enda", Attributes.Nominative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endi", Attributes.Vocative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endis", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endos", Attributes.Accusative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endis", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endorum", Attributes.Genitive | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "endi", Attributes.Nominative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "ende", Attributes.Vocative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endo", Attributes.Ablative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endum", Attributes.Accusative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endo", Attributes.Dative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endi", Attributes.Genitive | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "endus", Attributes.Nominative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
        }
    }

    public static void DoVParPassive61(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem0 = STEMS[0];
        if (stem0 != "-")
        {
            TOP = new Node(ENTRY, stem0, "unda", Attributes.Vocative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "unda", Attributes.Accusative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undorum", Attributes.Genitive | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "unda", Attributes.Nominative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undum", Attributes.Vocative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undo", Attributes.Ablative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undum", Attributes.Accusative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undo", Attributes.Dative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undi", Attributes.Genitive | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undum", Attributes.Nominative | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undae", Attributes.Vocative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undas", Attributes.Accusative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undarum", Attributes.Genitive | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undae", Attributes.Nominative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "unda", Attributes.Vocative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "unda", Attributes.Ablative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undam", Attributes.Accusative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undae", Attributes.Dative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undae", Attributes.Genitive | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "unda", Attributes.Nominative | Attributes.Feminine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undi", Attributes.Vocative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undis", Attributes.Ablative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undos", Attributes.Accusative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undis", Attributes.Dative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undorum", Attributes.Genitive | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "undi", Attributes.Nominative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Plural, TOP);
            TOP = new Node(ENTRY, stem0, "unde", Attributes.Vocative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undo", Attributes.Ablative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undum", Attributes.Accusative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undo", Attributes.Dative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undi", Attributes.Genitive | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem0, "undus", Attributes.Nominative | Attributes.Musculine | Attributes.Passive | Attributes.Future | Attributes.Participle | Attributes.Singular, TOP);
        }
    }

    public static void DoVParSupine00(int ENTRY, string[] STEMS, ref Node TOP)
    {
        string stem3 = STEMS[3];
        if (stem3 != "-")
        {
            TOP = new Node(ENTRY, stem3, "u", Attributes.Ablative | Attributes.Neuter | Attributes.Supine | Attributes.Singular, TOP);
            TOP = new Node(ENTRY, stem3, "um", Attributes.Accusative | Attributes.Neuter | Attributes.Supine | Attributes.Singular, TOP);
        }
    }

    static Dictionary<string, int> entries;

    private static Word CreateEntry(int INDEX, string[] STEMPS, string[] FLAGS, string DESC)
    {
        string key;
        if ((key = FLAGS[0]) != null)
        {
            if (entries == null)
			{
                entries = new Dictionary<string, int>(9)
                {
                    {
                        "NUM",
                        0
                    },
                    {
                        "ADV",
                        1
                    },
                    {
                        "CONJ",
                        2
                    },
                    {
                        "INTERJ",
                        3
                    },
                    {
                        "PRON",
                        4
                    },
                    {
                        "PREP",
                        5
                    },
                    {
                        "ADJ",
                        6
                    },
                    {
                        "N",
                        7
                    },
                    {
                        "V",
                        8
                    }
                };
            }
            int num;
            if (entries.TryGetValue(key, out num))
			{
                Word ENTRY;
                switch (num)
                {
                    case 0:
                        ENTRY = new Word(INDEX, Speach.Number, FLAGS, STEMPS);
                        break;
                    case 1:
                        ENTRY = new Word(INDEX, Speach.Adverb, FLAGS, STEMPS);
                        break;
                    case 2:
                        ENTRY = new Word(INDEX, Speach.Conjunction, FLAGS, STEMPS);
                        break;
                    case 3:
                        ENTRY = new Word(INDEX, Speach.Interjection, FLAGS, STEMPS);
                        break;
                    case 4:
                        ENTRY = new Word(INDEX, Speach.Pronoun, FLAGS, STEMPS);
                        break;
                    case 5:
                        ENTRY = new Word(INDEX, Speach.Preposition, FLAGS, STEMPS);
                        break;
                    case 6:
                        ENTRY = new Word(INDEX, Speach.Ajective, FLAGS, STEMPS);
                        break;
                    case 7:
                        ENTRY = new Word(INDEX, Speach.Noun, FLAGS, STEMPS);
                        break;
                    case 8:
                        ENTRY = new Word(INDEX, Speach.Verb, FLAGS, STEMPS);
                        break;
                    default:
                        goto IL_13C;
                }
                if (ENTRY != null)
                {
                    if (DESC != null)
                    {
                        DESC = DESC.TrimStart(new char[]
                        {
                            '|',
                            ' ',
                            '\n',
                            '\t',
                            '\r'
                        });
                    }
                    if (!Strings.IsNullOrWhiteSpace(DESC))
                    {
                        Word expr_16A = ENTRY;
                        expr_16A._Definiton += DESC;
                    }
                }
                return ENTRY;
            }
        }
        IL_13C:
        throw new NotSupportedException();
    }

    public static List<Node> LIST(Node top, Attributes attrs)
    {
        List<Node> list = new List<Node>();

        while (top != null)
        {
            if ((top.Attributes & attrs) != Attributes.None)
            {
                list.Add(top);
            }

            top = top.Next;
        }

        return list;
    }

    protected internal Node INFLECT(Word ENTRY)
    {
        Node top = null;
        this.INFLECT(ENTRY, ref top);
        return top;
    }

    protected internal void INFLECT(Word ENTRY, ref Node TOP, bool cache = false)
    {
        if (ENTRY == null)
        {
            return;
        }

        if (ENTRY.Top != null && cache)
        {
            TOP = ENTRY.Top;
            return;
        }

        try
        {
            if (cache)
            {
                TOP = null;
            }

            switch (ENTRY.Speach)
            {
                case Speach.Noun:
                    Grammar.__Noun(ENTRY.Index, ENTRY.Stems, ENTRY.Flags, ref TOP);
                    return;
                case Speach.Adverb:
                case Speach.Interjection:
                case Speach.Preposition:
                case Speach.Conjunction:
                    TOP = new Node(ENTRY.Index, ENTRY.Stems[0], "", Attributes.None, TOP);
                    break;
                case Speach.Ajective:
                    Grammar.__Adjective(ENTRY.Index, ENTRY.Stems, ENTRY.Flags, ref TOP);
                    return;
                case Speach.Verb:
                    Grammar.__Verb(ENTRY.Index, ENTRY.Stems, ENTRY.Flags, ref TOP);
                    if (ENTRY.Stems.Length == 4)
                    {
                        string pefect = ENTRY.Stems[2];
                        if (pefect.Length > 2 && Converter.GetLowerCaseChar(pefect[pefect.Length - 2]) == 97 && Converter.GetLowerCaseChar(pefect[pefect.Length - 1]) == 117)
                        {
                            TOP = new Node(ENTRY.Index, pefect.Remove(pefect.Length - 2) + "ā", "runt", ~(Attributes.Nominative | Attributes.Genitive | Attributes.Dative | Attributes.Accusative | Attributes.Ablative | Attributes.Locative | Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Common | Attributes.Positive | Attributes.Superlative | Attributes.Comparative | Attributes.Subjunctive | Attributes.Imerative | Attributes.Infinitive | Attributes.Passive | Attributes.Present | Attributes.Imperfect | Attributes.Future | Attributes.Participle | Attributes.Supine | Attributes.First | Attributes.Second | Attributes.Singular), TOP);
                            TOP = new Node(ENTRY.Index, pefect.Remove(pefect.Length - 2) + "ā", "rant", ~(Attributes.Nominative | Attributes.Genitive | Attributes.Dative | Attributes.Accusative | Attributes.Ablative | Attributes.Locative | Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Common | Attributes.Positive | Attributes.Superlative | Attributes.Comparative | Attributes.Subjunctive | Attributes.Imerative | Attributes.Infinitive | Attributes.Passive | Attributes.Present | Attributes.Future | Attributes.Participle | Attributes.Supine | Attributes.First | Attributes.Second | Attributes.Singular), TOP);
                            return;
                        }
                    }
                    break;
                case Speach.Pronoun:
                    Grammar.__Pronoun(ENTRY.Index, ENTRY.Stems, ENTRY.Flags, ref TOP);
                    return;
                case Speach.Number:
                    Grammar.__Number(ENTRY.Index, ENTRY.Stems, ENTRY.Flags, ref TOP);
                    return;
                default:
                    return;
            }
        }
        finally
        {
            if (cache)
            {
                ENTRY.Top = TOP;
            }
        }
    }

    protected internal void LOAD()
    {
        if (this._LOADED)
        {
            return;
        }
        this._LOADED = true;
        string[] LINES = File.ReadAllLines(Path.Combine(this.DictionaryPath, "DICTLINE2.GEN"));
        if (LINES == null || LINES.Length <= 0)
        {
            return;
        }
        this.ENTRIES = new Word[LINES.Length + 1];
        Node TOP = null;
        this.ENTRIES[0] = Grammar.CreateEntry(0, new string[]
        {
            "",
            "",
            "fu",
            "fut"
        }, new string[]
        {
            "V",
            "5",
            "1",
            "TO_BEING",
            "X",
            "X",
            "X",
            "X",
            "X"
        }, "I am; I exist;");
        this.INFLECT(this.ENTRIES[0], ref TOP);
        for (int i = 0; i < LINES.Length; i++)
        {
            string line = LINES[i];
            if (line.Length > 0 && line[0] != ' ' && line[0] != '\r' && line[0] != '\n' && line[0] != '*')
            {
                string[] STEMS = line.Substring(0, 76).Split(new char[]
                {
                    ' ',
                    '\t'
                }, StringSplitOptions.RemoveEmptyEntries);
                string[] FLAGS = line.Substring(76, 33).Split(new char[]
                {
                    ' ',
                    '\t'
                }, StringSplitOptions.RemoveEmptyEntries);
                string DESC = line.Substring(110).Trim();
                this.ENTRIES[i + 1] = Grammar.CreateEntry(i + 1, STEMS, FLAGS, DESC);
                this.INFLECT(this.ENTRIES[i + 1], ref TOP);
            }
        }
        this.HASH = new Node[TOP.Index + 1];
        while (TOP != null)
        {
            int I = TOP.HashCode % this.HASH.Length;
            Node NEW = new Node(TOP.Entry, TOP.Stem, TOP.Suffix, TOP.Attributes, TOP.HashCode, this.HASH[I]);
            this.HASH[I] = NEW;
            TOP = TOP.Next;
        }
        this.PARTITIONS = new Grammar.Partitions();
        this.PARTITIONS.Set(this.ENTRIES);
    }

    private bool DoFindWord(string match)
    {
        int hashCode = Converter.GetHashCode(match);
        int pos = hashCode % this.HASH.Length;
        if (pos < 0 || pos >= this.HASH.Length)
        {
            return false;
        }
        for (Node top = this.HASH[pos]; top != null; top = top.Next)
        {
            if (hashCode == top.HashCode)
            {
                bool bOK = Converter.Equals(match, top.Stem, top.Suffix);
                if (bOK)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool FindWord(string match)
    {
        return !Strings.IsNullOrWhiteSpace(match) && this.HASH != null && this.ENTRIES != null && this.HASH.Length > 0 && this.ENTRIES.Length > 0 && (this.DoFindWord(match) || (match.Length > "que".Length + 1 && match[match.Length - 1] == 'e' && match[match.Length - 2] == 'u' && match[match.Length - 3] == 'q' && this.DoFindWord(match.Remove(match.Length - "que".Length))) || (match.Length > "ve".Length + 1 && match[match.Length - 1] == 'e' && match[match.Length - 2] == 'v' && this.DoFindWord(match.Remove(match.Length - "ve".Length))) || (match.Length > "ne".Length + 1 && match[match.Length - 1] == 'e' && match[match.Length - 2] == 'n' && this.DoFindWord(match.Remove(match.Length - "ne".Length))));
    }

    void DoFindWords(string match, ref Dictionary<Word, Node> keyValueList)
    {
        int hashCode = Converter.GetHashCode(match);
        int pos = hashCode % this.HASH.Length;
        if (pos >= 0 && pos < this.HASH.Length)
        {
            Node top = this.HASH[pos];
            while (top != null)
            {
                Word ENTRY = this.ENTRIES[top.Entry];
                if (keyValueList != null && keyValueList.ContainsKey(ENTRY))
                {
                    top = top.Next;
                }
                else
                {
                    if (hashCode == top.HashCode && Converter.Equals(match, top.Stem, top.Suffix))
                    {
                        if (keyValueList == null)
                        {
                            keyValueList = new Dictionary<Word, Node>();
                        }
                        keyValueList[ENTRY] = this.INFLECT(ENTRY);
                    }
                    top = top.Next;
                }
            }
        }
    }

    public Dictionary<Word, Node> FindWords(string match)
    {
        if (Strings.IsNullOrWhiteSpace(match))
        {
            return null;
        }

        if (this.HASH == null || this.ENTRIES == null || this.HASH.Length <= 0 || this.ENTRIES.Length <= 0)
        {
            return null;
        }

        Dictionary<Word, Node> keyValueList = null;

        this.DoFindWords(match, ref keyValueList);

        if (match.Length > "que".Length + 1 && match[match.Length - 1] == 'e' && match[match.Length - 2] == 'u' && match[match.Length - 3] == 'q')
        {
            this.DoFindWords(match.Remove(match.Length - "que".Length), ref keyValueList);
        }
        if (match.Length > "ve".Length + 1 && match[match.Length - 1] == 'e' && match[match.Length - 2] == 'v')
        {
            this.DoFindWords(match.Remove(match.Length - "ve".Length), ref keyValueList);
        }
        if (match.Length > "ne".Length + 1 && match[match.Length - 1] == 'e' && match[match.Length - 2] == 'n')
        {
            this.DoFindWords(match.Remove(match.Length - "ne".Length), ref keyValueList);
        }

        return keyValueList;
    }

    public Dictionary<Word, Node> SearchWords(string search)
    {
        if (Strings.IsNullOrWhiteSpace(search))
        {
            return null;
        }
        if (this.HASH == null || this.ENTRIES == null || this.HASH.Length <= 0 || this.ENTRIES.Length <= 0)
        {
            return null;
        }

        Dictionary<Word, Node> keyValueList = null;

        int hashCode = Converter.GetHashCode(search);

        keyValueList = StartsWith(search, hashCode);

        return keyValueList;
    }

    Dictionary<Word, Node> StartsWith(string start, int hashCode)
    {
        if (Strings.IsNullOrWhiteSpace(start))
        {
            return null;
        }

        if (PARTITIONS == null)
        {
            return null;
        }

        if (this.HASH == null || this.ENTRIES == null || this.HASH.Length <= 0 || this.ENTRIES.Length <= 0)
        {
            return null;
        }

        Dictionary<Word, Node> keyValueList = null;

        // Exact

        int pos = hashCode % this.HASH.Length;

        if (pos >= 0 && pos < this.HASH.Length)
        {
            Node top = this.HASH[pos];

            while (top != null)
            {
                Word ENTRY = this.ENTRIES[top.Entry];

                if (keyValueList != null && keyValueList.ContainsKey(ENTRY))
                {
                    top = top.Next;
                }
                else
                {
                    if (hashCode == top.HashCode && Converter.StartWith2(start, top.Stem, top.Suffix))
                    {
                        if (keyValueList == null)
                        {
                            keyValueList = new Dictionary<Word, Node>();
                        }

                        keyValueList[ENTRY] = this.INFLECT(ENTRY);
                    }

                    top = top.Next;
                }


            }
        } 

        foreach (KeyValuePair<int, int> key in this.PARTITIONS.Get(start))
        {
            if (keyValueList != null && keyValueList.Count > 20)
            {
                break;
            }

            Word ENTRY2 = this.ENTRIES[key.Key];

            if (ENTRY2 != null && (keyValueList == null || !keyValueList.ContainsKey(ENTRY2)))
            {
                // Inflections

                Node top2 = this.INFLECT(ENTRY2);

                for (Node node = top2; node != null; node = node.Next)
                {
                    if (Converter.StartWith2(start, node.Stem, node.Suffix))
                    {
                        if (keyValueList == null)
                        {
                            keyValueList = new Dictionary<Word, Node>();
                        }

                        keyValueList[ENTRY2] = top2;

                        break;
                    }
                }
            }

        }

        if (start.Length > 2)
        {
            foreach (var ERY in ENTRIES)
            {
                // Translations

                if (ERY != null && (keyValueList == null || !keyValueList.ContainsKey(ERY)))
                {
                    if (ERY.Definiton.IndexOf(start, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        Node top2 = this.INFLECT(ERY);

                        if (keyValueList == null)
                        {
                            keyValueList = new Dictionary<Word, Node>();
                        }

                        keyValueList[ERY] = top2;
                    }
                }
            }
        }

        return keyValueList;
    }

    public Node Equals(string match)
    {
        if (Strings.IsNullOrWhiteSpace(match))
        {
            return null;
        }
        if (this.HASH == null || this.ENTRIES == null || this.HASH.Length <= 0 || this.ENTRIES.Length <= 0)
        {
            return null;
        }
        Node keyValueList = null;
        int hashCode = Converter.GetHashCode(match);
        int pos = hashCode % this.HASH.Length;
        if (pos >= 0 && pos < this.HASH.Length)
        {
            for (Node bucket = this.HASH[pos]; bucket != null; bucket = bucket.Next)
            {
                if (hashCode == bucket.HashCode && Converter.Equals(match, bucket.Stem, bucket.Suffix))
                {
                    keyValueList = new Node(bucket.Entry, bucket.Stem, bucket.Suffix, bucket.Attributes, bucket.HashCode, keyValueList);
                }
            }
        }
        return keyValueList;
    }

    public static class Formatter
    {
        public static string GetDefinition(Word entry)
        {
            StringBuilder HTML = new StringBuilder();
            if (entry != null)
            {
                HTML.Append(string.Format("<i>{0}</i>", entry.Definiton.ToString()));
            }
            return HTML.ToString();
        }

        public static string Get(Node top, Attributes match, string search)
        {
            StringBuilder HTML = new StringBuilder();
            Formatter.Get(HTML, top, match, search);
            return HTML.ToString();
        }

        public static void Get(StringBuilder HTML, Node top, Attributes match, string search)
        {
            bool hasComma = false;
            bool hasParan = false;
            while (top != null)
            {
                if (top.Attributes == match)
                {
                    bool bStartsWith = false;
                    if (search != null && Converter.StartWith2(search, top.Stem, top.Suffix))
                    {
                        bStartsWith = true;
                    }
                    if (HTML.Length > 0)
                    {
                        if (!hasComma && !hasParan)
                        {
                            HTML.Append(" (<i>");
                            hasParan = true;
                        }
                        else
                        {
                            HTML.Append(", ");
                            hasComma = true;
                        }
                    }
                    if (!bStartsWith)
                    {
                        if (search == null)
                        {
                            HTML.Append("<b>");
                        }
                        HTML.Append(top.Stem);
                        HTML.Append(top.Suffix);
                        if (search == null)
                        {
                            HTML.Append("</b>");
                        }
                    }
                    else
                    {
                        string str = top.Stem + top.Suffix;
                        str = str.Insert(search.Length, "</b></backcolor>");
                        HTML.Append("<backcolor=#FFEE80><b>");
                        HTML.Append(str);
                    }
                }
                top = top.Next;
            }
            if (hasParan)
            {
                HTML.Append("</i>)");
            }
        }

        public static string GetLong(string stem, string suffix, string[] flags, Attributes attributes, bool isSelected)
        {
            string A = string.Empty;
            if (attributes == Attributes.None)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "indecl.";
            }
            if ((attributes & Attributes.First) == Attributes.First)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "1st";
            }
            if ((attributes & Attributes.Second) == Attributes.Second)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "2nd";
            }
            if ((attributes & Attributes.Third) == Attributes.Third)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "3rd";
            }
            if ((attributes & Attributes.Nominative) == Attributes.Nominative)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "nom.";
            }
            if ((attributes & Attributes.Genitive) == Attributes.Genitive)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "gen.";
            }
            if ((attributes & Attributes.Dative) == Attributes.Dative)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "dat.";
            }
            if ((attributes & Attributes.Accusative) == Attributes.Accusative)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "acc.";
            }
            if ((attributes & Attributes.Ablative) == Attributes.Ablative)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "abl.";
            }
            if ((attributes & Attributes.Locative) == Attributes.Locative)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "loc.";
            }
            if ((attributes & Attributes.Vocative) == Attributes.Vocative)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "voc.";
            }
            if ((attributes & Attributes.Feminine) == Attributes.Feminine && (attributes & Attributes.Neuter) == Attributes.Neuter && (attributes & Attributes.Musculine) == Attributes.Musculine)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "m. f. n.";
            }
            else if ((attributes & Attributes.Feminine) == Attributes.Feminine && (attributes & Attributes.Musculine) == Attributes.Musculine)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "m. f.";
            }
            else if ((attributes & Attributes.Feminine) == Attributes.Feminine)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "f.";
            }
            else if ((attributes & Attributes.Musculine) == Attributes.Musculine)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "m.";
            }
            else if ((attributes & Attributes.Neuter) == Attributes.Neuter)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "n.";
            }
            if ((attributes & Attributes.Singular) == Attributes.Singular)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "sing.";
            }
            if ((attributes & Attributes.Plural) == Attributes.Plural)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "plur.";
            }
            if ((attributes & Attributes.Active) == Attributes.Active)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "act.";
            }
            if ((attributes & Attributes.Passive) == Attributes.Passive)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                if (flags[3] == "DEP")
                {
                    A += "dep.";
                }
                else
                {
                    A += "pass.";
                }
            }
            if ((attributes & Attributes.Indicative) == Attributes.Indicative)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "ind.";
            }
            if ((attributes & Attributes.Subjunctive) == Attributes.Subjunctive)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "subj.";
            }
            if ((attributes & Attributes.Infinitive) == Attributes.Infinitive)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "inf.";
            }
            if ((attributes & Attributes.Imerative) == Attributes.Imerative)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "imper.";
            }
            if ((attributes & Attributes.Present) == Attributes.Present)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "pres.";
            }
            else if ((attributes & Attributes.Perfect) == Attributes.Perfect)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                if ((attributes & Attributes.Future) == Attributes.Future)
                {
                    A += "future ";
                }
                if ((attributes & Attributes.Imperfect) == Attributes.Imperfect)
                {
                    A += "plus ";
                }

                A += "perf.";
            }
            else if ((attributes & Attributes.Future) == Attributes.Future)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "future";
            }
            else if ((attributes & Attributes.Imperfect) == Attributes.Imperfect)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "imperf.";
            }
            if ((attributes & Attributes.Participle) == Attributes.Participle)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "partic.";
            }
            if ((attributes & Attributes.Positive) == Attributes.Positive)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "pos.";
            }
            if ((attributes & Attributes.Comparative) == Attributes.Comparative)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "compar.";
            }
            if ((attributes & Attributes.Superlative) == Attributes.Superlative)
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "super.";
            }
            if ((attributes & ~(Attributes.Nominative | Attributes.Genitive | Attributes.Dative | Attributes.Accusative | Attributes.Ablative | Attributes.Locative | Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Common | Attributes.Positive | Attributes.Superlative | Attributes.Comparative | Attributes.Indicative | Attributes.Subjunctive | Attributes.Imerative | Attributes.Infinitive | Attributes.Active | Attributes.Passive | Attributes.Present | Attributes.Perfect | Attributes.Imperfect | Attributes.Future | Attributes.Participle | Attributes.Supine | Attributes.First | Attributes.Second | Attributes.Third | Attributes.Singular | Attributes.Plural)) == ~(Attributes.Nominative | Attributes.Genitive | Attributes.Dative | Attributes.Accusative | Attributes.Ablative | Attributes.Locative | Attributes.Vocative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Common | Attributes.Positive | Attributes.Superlative | Attributes.Comparative | Attributes.Indicative | Attributes.Subjunctive | Attributes.Imerative | Attributes.Infinitive | Attributes.Active | Attributes.Passive | Attributes.Present | Attributes.Perfect | Attributes.Imperfect | Attributes.Future | Attributes.Participle | Attributes.Supine | Attributes.First | Attributes.Second | Attributes.Third | Attributes.Singular | Attributes.Plural))
            {
                if (A.Length > 0)
                {
                    A += " ";
                }
                A += "contr.";
            }
            if (A.Length > 0)
            {
                A = string.Format("<i>({0})</i>", A);
            }
            if (isSelected)
            {
                return string.Format("<color=#003366><b>{0}{1}</b></color> {2}", stem, suffix, A);
            }
            return string.Format("<b>{0}{1}</b> <size=-2>{2}</size>", stem, suffix, A);
        }

        public static void Dump(Node top)
        {
            for (Node i = top; i != null; i = i.Next)
            {
                string s = ", " + i.Attributes.ToString();
                s = s.Replace(", ", " | Attributes.");
                Console.WriteLine("{0}{1} {2}", i.Stem, i.Suffix, s);
            }
        }

        public static string GetDeclaration(Word entry, Node top, string search)
        {
            StringBuilder HTML = new StringBuilder();
            switch (entry.Speach)
            {
                case Speach.Noun:
                    {
                        string nom = (top.Index == 0) ? Formatter.Get(top, Attributes.None, search) : Formatter.Get(top, Attributes.Nominative | Attributes.Singular, search);
                        if (!Strings.IsNullOrWhiteSpace(nom))
                        {
                            if (HTML.Length > 0)
                            {
                                HTML.Append(", ");
                            }
                            HTML.Append(nom);
                        }
                        if (top.Index != 0)
                        {
                            string gen = Formatter.Get(top, Attributes.Genitive | Attributes.Singular, search);
                            if (!Strings.IsNullOrWhiteSpace(gen))
                            {
                                if (HTML.Length > 0)
                                {
                                    HTML.Append(", ");
                                    HTML.Append("<size=-2><i>gen.</i></size> ");
                                }
                                HTML.Append(gen);
                            }
                            string plur = Formatter.Get(top, Attributes.Nominative | Attributes.Plural, search);
                            if (!Strings.IsNullOrWhiteSpace(plur))
                            {
                                if (HTML.Length > 0)
                                {
                                    HTML.Append(", ");
                                    HTML.Append("<size=-2><i>plur.</i></size> ");
                                }
                                HTML.Append(plur);
                            }
                        }
                        string a;
                        if (HTML.Length <= 0 || (a = entry.Flags[3]) == null)
                        {
                            goto IL_68A;
                        }
                        if (a == "F")
                        {
                            HTML.Append(string.Format(" <i>(f.)</i>", new object[0]));
                            goto IL_68A;
                        }
                        if (a == "M")
                        {
                            HTML.Append(string.Format(" <i>(m.)</i>", new object[0]));
                            goto IL_68A;
                        }
                        if (a == "N")
                        {
                            HTML.Append(string.Format(" <i>(n.)</i>", new object[0]));
                            goto IL_68A;
                        }
                        if (a == "C")
                        {
                            HTML.Append(string.Format(" <i>(f. and m.)</i>", new object[0]));
                            goto IL_68A;
                        }
                        if (!(a == "X"))
                        {
                            goto IL_68A;
                        }
                        HTML.Append(string.Format(" <i>(f. m. and n.)</i>", new object[0]));
                        goto IL_68A;
                    }
                case Speach.Ajective:
                    {
                        string musc = Formatter.Get(top, Attributes.Nominative | Attributes.Musculine | Attributes.Positive | Attributes.Singular, search);
                        if (Strings.IsNullOrWhiteSpace(musc))
                        {
                            musc = Formatter.Get(top, Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Positive | Attributes.Singular, search);
                        }
                        if (!Strings.IsNullOrWhiteSpace(musc))
                        {
                            if (HTML.Length > 0)
                            {
                                HTML.Append(", ");
                            }
                            HTML.Append(musc);
                        }
                        string fem = Formatter.Get(top, Attributes.Nominative | Attributes.Feminine | Attributes.Positive | Attributes.Singular, search);
                        if (Strings.IsNullOrWhiteSpace(fem))
                        {
                            fem = Formatter.Get(top, Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Positive | Attributes.Singular, search);
                        }
                        if (!Strings.IsNullOrWhiteSpace(fem))
                        {
                            if (HTML.Length > 0)
                            {
                                HTML.Append(", ");
                            }
                            HTML.Append(fem);
                        }
                        bool bNeedGenitive = false;
                        string neut = Formatter.Get(top, Attributes.Nominative | Attributes.Neuter | Attributes.Positive | Attributes.Singular, search);
                        if (Strings.IsNullOrWhiteSpace(neut))
                        {
                            neut = Formatter.Get(top, Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, search);
                            bNeedGenitive = true;
                        }
                        if (!Strings.IsNullOrWhiteSpace(neut))
                        {
                            if (HTML.Length > 0)
                            {
                                HTML.Append(", ");
                            }
                            HTML.Append(neut);
                        }
                        else
                        {
                            bNeedGenitive = false;
                        }
                        if (bNeedGenitive)
                        {
                            string gen2 = Formatter.Get(top, Attributes.Genitive | Attributes.Musculine | Attributes.Feminine | Attributes.Neuter | Attributes.Positive | Attributes.Singular, search);
                            if (!Strings.IsNullOrWhiteSpace(gen2))
                            {
                                if (HTML.Length > 0)
                                {
                                    HTML.Append(", ");
                                }
                                HTML.Append(gen2);
                            }
                        }
                        if (HTML.Length > 0)
                        {
                            HTML.Append(string.Format(" <i>(adj.)</i>", new object[0]));
                            goto IL_68A;
                        }
                        goto IL_68A;
                    }
                case Speach.Verb:
                    {
                        string pres = Formatter.Get(top, Attributes.Indicative | Attributes.Present | Attributes.First | Attributes.Singular | ((entry.Flags[3] == "DEP") ? Attributes.Passive : Attributes.Active), search);
                        string inf = Formatter.Get(top, Attributes.Infinitive | Attributes.Present | ((entry.Flags[3] == "DEP") ? Attributes.Passive : Attributes.Active), search);
                        string perf = Formatter.Get(top, Attributes.Indicative | Attributes.Perfect | Attributes.First | Attributes.Singular | ((entry.Flags[3] == "DEP") ? Attributes.Passive : Attributes.Active), search);
                        if (perf == null || perf.Length <= 0)
                        {
                            perf = Formatter.Get(top, Attributes.Nominative | Attributes.Musculine | Attributes.Passive | Attributes.Perfect | Attributes.Participle | Attributes.Singular, search);
                            if (perf != null && perf.Length > 0)
                            {
                                perf += " sum";
                            }
                        }
                        string sup = Formatter.Get(top, Attributes.Accusative | Attributes.Neuter | Attributes.Supine | Attributes.Singular, search);
                        if (!Strings.IsNullOrWhiteSpace(pres))
                        {
                            if (HTML.Length > 0)
                            {
                                HTML.Append(", ");
                            }
                            HTML.Append(pres);
                        }
                        if (!Strings.IsNullOrWhiteSpace(inf))
                        {
                            if (HTML.Length > 0)
                            {
                                HTML.Append(", ");
                            }
                            HTML.Append(inf);
                        }
                        if (!Strings.IsNullOrWhiteSpace(perf))
                        {
                            if (HTML.Length > 0)
                            {
                                HTML.Append(", ");
                            }
                            HTML.Append(perf);
                        }
                        if (!Strings.IsNullOrWhiteSpace(sup))
                        {
                            if (HTML.Length > 0)
                            {
                                HTML.Append(", ");
                            }
                            HTML.Append(sup);
                        }
                        if (HTML.Length <= 0)
                        {
                            goto IL_68A;
                        }
                        if (entry.Flags[3] != "X" && entry.Flags[3] != "TO_BEING")
                        {
                            HTML.Append(string.Format(" <i>(v. {0}.)</i>", entry.Flags[3].ToLowerInvariant()));
                            goto IL_68A;
                        }
                        HTML.Append(string.Format(" <i>(v.)</i>", new object[0]));
                        goto IL_68A;
                    }
                case Speach.Pronoun:
                    {
                        string musc2 = Formatter.Get(top, Attributes.Nominative | Attributes.Musculine | Attributes.Singular, search);
                        if (Strings.IsNullOrWhiteSpace(musc2))
                        {
                            musc2 = Formatter.Get(top, Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, search);
                        }
                        Strings.IsNullOrWhiteSpace(musc2);
                        if (!Strings.IsNullOrWhiteSpace(musc2))
                        {
                            if (HTML.Length > 0)
                            {
                                HTML.Append(", ");
                            }
                            HTML.Append(musc2);
                        }
                        string fem2 = Formatter.Get(top, Attributes.Nominative | Attributes.Feminine | Attributes.Singular, search);
                        if (Strings.IsNullOrWhiteSpace(fem2))
                        {
                            fem2 = Formatter.Get(top, Attributes.Nominative | Attributes.Musculine | Attributes.Feminine | Attributes.Singular, search);
                        }
                        if (!Strings.IsNullOrWhiteSpace(fem2))
                        {
                            if (HTML.Length > 0)
                            {
                                HTML.Append(", ");
                            }
                            HTML.Append(fem2);
                        }
                        string neut2 = Formatter.Get(top, Attributes.Nominative | Attributes.Neuter | Attributes.Singular, search);
                        if (!Strings.IsNullOrWhiteSpace(neut2))
                        {
                            if (HTML.Length > 0)
                            {
                                HTML.Append(", ");
                            }
                            HTML.Append(neut2);
                        }
                        if (HTML.Length > 0)
                        {
                            HTML.Append(string.Format(" <i>(pron. {0}.)</i>", entry.Flags[3].ToLowerInvariant()));
                            goto IL_68A;
                        }
                        goto IL_68A;
                    }
            }
            if (top != null)
            {
                string indecl = (top.Index == 0) ? Formatter.Get(top, Attributes.None, search) : string.Empty;
                if (!Strings.IsNullOrWhiteSpace(indecl))
                {
                    if (HTML.Length > 0)
                    {
                        HTML.Append(", ");
                    }
                    HTML.Append(indecl);
                }
                if (HTML.Length > 0)
                {
                    HTML.Append(string.Format(" <i>({0}.)</i>", entry.Flags[0].ToLowerInvariant()));
                }
            }
            IL_68A:
            return HTML.ToString();
        }
    }
}
