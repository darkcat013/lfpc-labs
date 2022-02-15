using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class NFA
    {
        public Lookup<string, Tuple<string, string>>? Map { get; set; }
        public List<string>? TransitionVariables { get; set; }
        protected NFA() { }
        public NFA(string path)
        {
            ReadFromFile(path);
        }

        public void ReadFromFile(string path)
        {
            string[]? transitions = File.ReadAllLines(path);
            Map = transitions
                .Select(x => x.Split(' '))
                .ToLookup(x => x[0], x => new Tuple<string, string>(x[1], x[2]))
                as Lookup<string, Tuple<string, string>>;

            TransitionVariables = transitions
                .Select(x => x.Split(' ')[1])
                .Distinct()
                .ToList();
        }
        
        public void PrintConsole()
        {
            foreach (var transition in Map)
            {
                foreach (var pair in transition)
                {
                    Console.WriteLine($"d({transition.Key}, {pair.Item1}) = {pair.Item2}");
                }
            }
        }
    }
}
