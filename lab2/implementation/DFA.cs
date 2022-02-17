using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    //inherit the map and PrintConsole() function
    public class DFA : NFA
    {
        public DFA(NFA nfa)
        {
            NFAtoDFA(nfa);
        }

        //get the map of DFA from given NFA
        private void NFAtoDFA(NFA nfa)
        {
            //making sure the map is empty
            Map = new Dictionary<string, Dictionary<string, List<string>>>();
            //recursive function that makes all the magic
            ProcessState(new List<string> {"q0"}, nfa.Map, new List<string>());
            //join the same variable transitions for multiple state keys
            JoinTransitions();
        }

        //states - the states that are the part of the current state
        //nfaMap - the nfa from where the function gets all the transitions
        //processedStates - keep track of which states were processedd
        private void ProcessState(List<string> states, Dictionary<string, Dictionary<string, List<string>>> nfaMap, List<string> processedStates)
        {
            //the state name composed of all the states in its name
            string stateName = string.Join("", states);
            //if state already processed then do nothing
            if(processedStates.Contains(stateName))
            {
                return;
            }
            //if state not in map, add it
            if(!Map.ContainsKey(stateName))
            {
                Map[stateName] = new Dictionary<string, List<string>>();
                Console.WriteLine($"- Added state {stateName} to DFA");
            }

            //iterate through each state of the current state
            foreach (string state in states)
            {
                //if the state does not have any transitions, do nothing
                if (!nfaMap.ContainsKey(state))
                {
                    return;
                }
                //add each transition for each state to the current state
                foreach (var transitionVariable in nfaMap[state])
                {
                    //if transition variable not a part of the state, add it
                    if (!Map[stateName].ContainsKey(transitionVariable.Key))
                    {
                        Map[stateName][transitionVariable.Key] = new List<string>();
                    }
                    //add all the states that the current state goes to through the transitionVariable
                    foreach(var toState in transitionVariable.Value)
                    {
                        //if the state is not added, add it
                        if(!Map[stateName][transitionVariable.Key].Contains(toState))
                        {
                            Map[stateName][transitionVariable.Key].Add(toState);
                        }
                    }
                }
            }
            //print all the steps of adding the transitions
            foreach(var transitionVariable in Map[stateName])
            {
                Console.WriteLine($"- Added transition d({stateName}, {transitionVariable.Key}) = {string.Join("", transitionVariable.Value)}");
            }
            //mark current state as processed
            processedStates.Add(stateName);
            
            //process each state that the current state goes to
            foreach(var transitionVariable in Map[stateName])
            {
                ProcessState(transitionVariable.Value, nfaMap, processedStates);
            }
        }

        //joining the same variable transition for the same state
        private void JoinTransitions()
        {
            foreach(var state in Map)
            {
                foreach(var transitionVariable in state.Value)
                {
                    var finalToState = string.Join("", transitionVariable.Value);
                    transitionVariable.Value.Clear();
                    transitionVariable.Value.Add(finalToState);
                }
            }
        }
    }
}
