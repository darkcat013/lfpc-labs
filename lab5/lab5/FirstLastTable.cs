namespace lab5
{
    public class FirstLastTable
    {
        public Dictionary<string, Tuple<List<string>, List<string>>> Table { get; set; }

        public FirstLastTable(Grammar grammar)
        {
            Table = new Dictionary<string, Tuple<List<string>, List<string>>>();
            foreach (var (key, values) in grammar.P)
            {
                if (!Table.ContainsKey(key)) Table[key] = new(new(), new());
                Table[key].Item1.AddRange(GetFirst(key));
                Table[key].Item2.AddRange(GetLast(key));
            }

            List<string> GetFirst(string key)
            {
                var result = new HashSet<string>();
                var firsts = grammar.P[key]
                    .Select(x => x.First().ToString())
                    .ToList();

                result.UnionWith(firsts);

                Queue<string> queue = new();
                result.Where(x => grammar.IsNonTerminal(x.First())).ToList().ForEach(x => queue.Enqueue(x));

                while(queue.Count > 0)
                {
                    var currentKey = queue.Dequeue();
                    firsts = grammar.P[currentKey]
                        .Select(x => x.First().ToString())
                        .ToList();

                    firsts.Where(x => grammar.IsNonTerminal(x.First())).ToList().ForEach(x => { if (!result.Contains(x)) queue.Enqueue(x); });

                    result.UnionWith(firsts);
                }    
                return result.ToList();
            }

            List<string> GetLast(string key)
            {
                var result = new HashSet<string>();
                var firsts = grammar.P[key]
                    .Select(x => x.Last().ToString())
                    .ToList();

                result.UnionWith(firsts);

                Queue<string> queue = new();
                result.Where(x => grammar.IsNonTerminal(x.Last())).ToList().ForEach(x => queue.Enqueue(x));

                while (queue.Count > 0)
                {
                    var currentKey = queue.Dequeue();
                    firsts = grammar.P[currentKey]
                        .Select(x => x.Last().ToString())
                        .ToList();

                    firsts.Where(x => grammar.IsNonTerminal(x.Last())).ToList().ForEach(x => { if (!result.Contains(x)) queue.Enqueue(x); });

                    result.UnionWith(firsts);

                }
                return result.ToList();
            }
        }

        public List<string> First(string key)
        {
            return Table[key].Item1;
        }

        public List<string> Last(string key)
        {
            return Table[key].Item2;
        }

        public override string ToString()
        {
            Console.WriteLine("\tFirst\t\tLast");
            string result = "";
            foreach (var (key, values) in Table)
            {
                result += $"{key}\t";
                foreach (var value in values.Item1)
                {
                    result += $"{value}, ";
                }
                result = result.Remove(result.Length - 2);
                result += "\t\t";

                foreach (var value in values.Item2)
                {
                    result += $"{value}, ";
                }
                result = result.Remove(result.Length - 2);
                result += "\n";
            }
            return result;
        }
    }
}
