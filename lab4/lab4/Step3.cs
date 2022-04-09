namespace lab4
{
    public static class Step3
    {
        public static Grammar RemoveUnproductive(this Grammar grammar)
        {
            var productive = grammar.GetProductive();

            var nonProductive = grammar.Vn.Except(productive);

            if (!nonProductive.Any())
            {
                grammar.CleanTerminalNonTerminal();
                return grammar;
            }

            grammar.RemoveEverything(nonProductive);
            grammar.RemoveEmpty();
            grammar.RemoveUnitProductions();
            return grammar.RemoveUnproductive();
        }

        private static HashSet<string> GetProductive(this Grammar grammar, HashSet<string>? productive = null)
        {
            productive ??= new HashSet<string>();
            int initialCount = productive.Count;

            foreach (var (key, value) in grammar.P)
            {
                if (productive.Contains(key)) continue;
                if(value.Any(x => x.All(
                    c => grammar.IsTerminal(c) || productive.Contains(c.ToString()))
                )) productive.Add(key);
            }

            if(productive.Count == initialCount) return productive;
            return GetProductive(grammar, productive);
        }
    }
}
