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
            NFAtoDFA(nfa);
        }
        public void NFAtoDFA(NFA nfa)
        {
            Map = new Dictionary<string, Dictionary<string, List<string>>>();
            ProcessState(new List<string> {"q0"}, nfa.Map, new List<string>());
            JoinTransitions();
        }

        private void ProcessState(List<string> states, Dictionary<string, Dictionary<string, List<string>>> nfaMap, List<string> processedStates)
        {
            string stateName = string.Join("", states);
            if(processedStates.Contains(stateName))
            {
                return;
            }
            if(!Map.ContainsKey(stateName))
            {
                Map[stateName] = new Dictionary<string, List<string>>();
                Console.WriteLine($"- Added state {stateName} to DFA");
            }

            foreach (string state in states)
            {
                if (!nfaMap.ContainsKey(state))
                {
                    return;
                }
                foreach (var transitionVariable in nfaMap[state])
                {
                    if (!Map[stateName].ContainsKey(transitionVariable.Key))
                    {
                        Map[stateName][transitionVariable.Key] = new List<string>();
                    }
                    foreach(var toState in transitionVariable.Value)
                    {
                        if(!Map[stateName][transitionVariable.Key].Contains(toState))
                        {
                            Map[stateName][transitionVariable.Key].Add(toState);
                        }
                    }
                }
            }
            foreach(var transitionVariable in Map[stateName])
            {
                Console.WriteLine($"- Added transition d({stateName}, {transitionVariable.Key}) = {string.Join("", transitionVariable.Value)}");
            }
            processedStates.Add(stateName);
            
            foreach(var transitionVariable in Map[stateName])
            {
                ProcessState(transitionVariable.Value, nfaMap, processedStates);
            }
        }

        private void JoinTransitions()
        {
            foreach(var state in Map)
            {
                foreach(var transitionVariable in state.Value)
                {
                    var finalState = string.Join("", transitionVariable.Value);
                    transitionVariable.Value.Clear();
                    transitionVariable.Value.Add(finalState);
                }
            }
        }
    }
}
