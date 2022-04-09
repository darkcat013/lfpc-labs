namespace lab4
{
    public static class Step2
    {
        public static Grammar RemoveUnitProductions(this Grammar grammar)
        {
            var (key, value) = grammar.GetUnitProduction();
            if(string.IsNullOrEmpty(value))
            {
                grammar.CleanTerminalNonTerminal();
                return grammar;
            }
            
            grammar.P[key].RemoveWhere(x => x.Equals(value));
            grammar.P[key].UnionWith(grammar.P[value]);

            return RemoveUnitProductions(grammar);
        }

        private static (string, string) GetUnitProduction(this Grammar grammar)
        {
            foreach (var (key, value) in grammar.P)
            {
                var result = value.Where(x => grammar.IsNonTerminal(x)).FirstOrDefault();
                if (!string.IsNullOrEmpty(result)) return (key, result);
            }
            return default;
        }
    }
}
