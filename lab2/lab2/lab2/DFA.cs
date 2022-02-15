using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class DFA : NFA
    {
        public DFA(NFA nfa)
        {
            TransitionVariables = nfa.TransitionVariables;
            Map = NFAtoDFA(nfa);
        }
        public Lookup<string, Tuple<string, string>> NFAtoDFA(NFA nfa)
        {
            Queue<string> statesToProcess = new Queue<string>();
            statesToProcess.Enqueue("q0");
            
            List<string> processedStates = new List<string>();
            List<Tuple<string, string, string>> transitions = new List<Tuple<string, string,string>>();
            while(statesToProcess.Count > 0)
            {
                string nextState = statesToProcess.Dequeue();

                Console.WriteLine($"- Starting to process state '{nextState}'");

                List<string> newStates;
                var processedTransitions = ProcessState(nextState, processedStates, nfa.Map, out newStates);

                if (processedTransitions?.Count > 0)
                {
                    transitions.AddRange(processedTransitions);
                }
                else
                {
                    Console.WriteLine("ehe");
                }
                if(newStates?.Count > 0)
                {
                    newStates.ForEach(statesToProcess.Enqueue);
                }
                processedStates.Add(nextState);
            }

            var result = transitions
                .ToLookup(x => x.Item1, x => new Tuple<string, string>(x.Item2, x.Item3))
                as Lookup<string, Tuple<string, string>>;

            return result;
        }
        private List<Tuple<string, string, string>> ProcessState(string state, List<string> processedStates, Lookup<string, Tuple<string, string>> nfaMap, out List<string> newStates)
        {
            var transitions = nfaMap
                .Where(x => x.Key == state);
            List<Tuple<string,string,string>> result = new List<Tuple<string,string, string>>();
            newStates = new List<string>();
            newStates.Add("q4");
            foreach (var transition in transitions)
            {
                foreach (var pair in transition)
                {
                    Console.WriteLine($"d({transition.Key}, {pair.Item1}) = {pair.Item2}");
                    result.Add(Tuple.Create(transition.Key,pair.Item1,pair.Item2));

                }
            }
            return result;
        }

        private List<Tuple<string, string, string>> ProcessSingleState(string state, List<string> processedStates, Lookup<string, Tuple<string, string>> nfaMap)
        {
            return default;
        }

        private List<Tuple<string, string, string>> ProcessCombinedState(List<string> states, List<string> processedStates, Lookup<string, Tuple<string, string>> nfaMap)
        {
            return default;
        }
    }
}
