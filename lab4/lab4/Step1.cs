namespace lab4
{
    public static class Step1
    {
        public static Grammar RemoveEmpty(this Grammar grammar)
        {
            grammar.Cleanup();
            var keyWithEmpty = grammar.KeyWithEmpty();
            if (keyWithEmpty is null)
            { 
                grammar.CleanTerminalNonTerminal();
                return grammar;
            }
            grammar.P[keyWithEmpty].RemoveWhere(x => x.Equals("ε") || string.IsNullOrEmpty(x));

            var grammarProductionsCopy = grammar.CloneProductions();

            foreach (var (key, value) in grammarProductionsCopy)
            {
                foreach (var str in value)
                {
                    if(str.Contains(keyWithEmpty))
                    {
                        List<int> matchingIndexes = MatchingIndexes(keyWithEmpty, str);
                        grammar.P[key].UnionWith(Combinations(str, matchingIndexes));
                    }
                }
            }
            return grammar.RemoveEmpty();
        }

        private static string? KeyWithEmpty(this Grammar grammar)
        {
            foreach (var (key, value) in grammar.P)
            {
                if (value.Contains("ε") || value.Where(x => string.IsNullOrEmpty(x)).Any()) return key;
            }
            return null;
        }

        private static List<int> MatchingIndexes(string symbol, string str)
        {
            var result = new List<int>();
            for(int i = 0; i < str.Length; i++)
            {
                if(symbol.Equals(str[i].ToString())) result.Add(i);
            }
            return result;
        }

        private static List<string> Combinations(string str, List<int> matchingIndexes)
        {
            List<string> results = new();
            int totalCombinations = (int)Math.Pow(2, matchingIndexes.Count)-1;
            for(; totalCombinations > 0; totalCombinations--)
            {
                var bits = Convert.ToString(totalCombinations, 2);
                var strCopy = str;
                for (int i = 0; i < bits.Length && i < matchingIndexes.Count && i < strCopy.Length; i++)
                {
                    if (bits[i] == '1')
                    {
                        strCopy = strCopy.Remove(matchingIndexes[i]-i, 1);
                    }
                }
                if (string.IsNullOrEmpty(strCopy)) results.Add("ε");
                else results.Add(strCopy);
            }
            return results;
        }

        private static void Cleanup(this Grammar grammar)
        {
            var result = new Dictionary<string,HashSet<string>>();
            foreach (var (key, value) in grammar.P)
            {
                var newValue  = value.Select(x => x.Replace("ε","")).ToHashSet();
                result.Add(key, newValue);
            }
            grammar.P = result;
        }
    }
}
