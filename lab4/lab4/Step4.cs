namespace lab4
{
    public static class Step4
    {
        public static Grammar RemoveUnaccessible(this Grammar grammar)
        {
            var accessible = new HashSet<string>();
            var queue = new Queue<string>();
            queue.Enqueue(grammar.S);

            while(queue.Count > 0)
            {
                var current = queue.Dequeue();
                accessible.Add(current);
                foreach (var str in grammar.P[current])
                {
                    foreach (var c in str)
                    {
                        var sc = c.ToString();
                        if(grammar.IsNonTerminal(sc) && !queue.Contains(sc) && !accessible.Contains(sc)) queue.Enqueue(sc);
                    }
                }
            }

            var unaccessible = grammar.Vn.Except(accessible);

            if(!unaccessible.Any())
            {
                grammar.CleanTerminalNonTerminal();
                return grammar;
            }

            grammar.RemoveEverything(unaccessible);
            grammar.RemoveEmpty();
            grammar.RemoveUnitProductions();
            return grammar.RemoveUnaccessible();
        }
    }
}
