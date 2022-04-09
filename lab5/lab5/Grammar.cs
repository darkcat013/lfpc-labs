namespace lab5
{
    public class Grammar
    {
        public HashSet<string> Vn { get; set; }
        public HashSet<string> Vt { get; set; }
        public Dictionary<string, HashSet<string>> P { get; set; }
        public string S { get; set; }
        public string Input { get; set; }
        public Grammar(string path, string startSymbol)
        {
            Vt = new HashSet<string>();
            Vn = new HashSet<string>();
            P = new Dictionary<string, HashSet<string>>();
            S = startSymbol;

            var rawGrammar = File.ReadAllLines(path);

            Vn.UnionWith(rawGrammar[0].Select(x => x.ToString()).Where(x => !x.Equals(" ")));
            Vt.UnionWith(rawGrammar[1].Select(x => x.ToString()).Where(x => !x.Equals(" ")));
            Input = rawGrammar[2];

            for (int i = 3; i < rawGrammar.Length; i++)
            {
                var leftSide = rawGrammar[i][0].ToString();
                var rightSide = rawGrammar[i][3..];

                if (!P.ContainsKey(leftSide)) P[leftSide] = new HashSet<string>();
                P[leftSide].Add(rightSide);
            }
        }

        public bool IsNonTerminal(string symbol) => Vn.Contains(symbol);
        public bool IsNonTerminal(char symbol) => IsNonTerminal(symbol.ToString());
        public bool IsTerminal(string symbol) => Vt.Contains(symbol);
        public bool IsTerminal(char symbol) => IsTerminal(symbol.ToString());

        public void CleanTerminalNonTerminal()
        {
            var vn = new HashSet<string>();
            var vt = new HashSet<string>();

            foreach (var (key, value) in P)
            {
                foreach (var str in value)
                {
                    foreach (var c in str)
                    {
                        string sc = c.ToString();
                        if (IsNonTerminal(sc)) vn.Add(sc);
                        else if (IsTerminal(sc)) vt.Add(sc);
                    }
                }
                if (P[key].Count > 0) vn.Add(key);
            }

            Vn.IntersectWith(vn);
            Vt.IntersectWith(vt);
        }

        public Dictionary<string, HashSet<string>> CloneProductions()
        {
            return P.ToDictionary(x => x.Key, x => x.Value.ToHashSet());
        }

        public void RemoveEverything(IEnumerable<string> symbols)
        {
            foreach (var item in symbols)
            {
                RemoveEverything(item);
            }
        }

        public void RemoveEverything(string symbol)
        {
            RemoveKey(symbol);
            RemoveInsideProductions(symbol);
            CleanTerminalNonTerminal();
        }

        public void RemoveKey(string symbol)
        {
            P.Remove(symbol);
        }

        private void RemoveInsideProductions(string symbol)
        {
            var newProductions = new Dictionary<string, HashSet<string>>();
            foreach (var (key, values) in P)
            {
                newProductions[key] = new HashSet<string>();
                foreach (var str in values)
                {
                    newProductions[key].Add(str.Replace(symbol, ""));
                }
            }
            P = newProductions;
        }

        public override string ToString()
        {
            string vn = "";
            foreach (var s in Vn)
            {
                vn += $"{s}, ";
            }
            vn = vn.Remove(vn.Length - 2);

            string vt = "";
            foreach (var s in Vt)
            {
                vt += $"{s}, ";
            }
            vt = vt.Remove(vt.Length - 2);

            string p = "";
            foreach (var (key, value) in P)
            {
                foreach (var right in value)
                {
                    p += $"{key}->{right}\n";
                }
            }

            return $"Vn={{{vn}}}\nVt={{{vt}}}\nP={{{p}}}";
        }
    }
}
