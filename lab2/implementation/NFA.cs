using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class NFA
    {
        public Dictionary<string, Dictionary<string, List<string>>> Map { get; set; } = new Dictionary<string, Dictionary<string, List<string>>>();
        protected NFA() { }
        public NFA(string path)
        {
            ReadFromFile(path);
        }

        public void ReadFromFile(string path)
        {
            var transitions = File.ReadAllLines(path).Select(x => x.Split(' '));
            foreach (var transition in transitions)
            {
                if (!Map.ContainsKey(transition[0]))
                {
                    Map[transition[0]] = new Dictionary<string, List<string>>();
                }
                if (!Map[transition[0]].ContainsKey(transition[1]))
                {
                    Map[transition[0]][transition[1]] = new List<string>();
                }
                Map[transition[0]][transition[1]].Add(transition[2]);
            }
        }

        public void PrintConsole()
        {
            foreach (var state in Map)
            {
                foreach (var transitionVariable in state.Value)
                {
                    foreach (var toState in transitionVariable.Value)
                    {
                        Console.WriteLine($"d({state.Key}, {transitionVariable.Key}) = {toState}");
                    }
                }
            }
        }
    }
}
