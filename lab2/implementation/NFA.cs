using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class NFA
    {
        //NFA will be of form [state][transitionVariable] = List of states to which the 'state' can go with the 'transitionVariable'
        public Dictionary<string, Dictionary<string, List<string>>> Map { get; set; } = new Dictionary<string, Dictionary<string, List<string>>>();
        
        //empty constructor for inheritance
        protected NFA() { }

        public NFA(string path)
        {
            ReadFromFile(path);
        }

        //read input from file
        private void ReadFromFile(string path)
        {
            //each line from file is a transition where [0] is 'state' [1] is 'transitionVariable' [2] is 'toState'
            //all of them are separated by space
            //read all lines and split each line into strings that contains the information above
            IEnumerable<string[]> transitions = File.ReadAllLines(path).Select(x => x.Split(' '));

            //iterate through each transition
            foreach (string[] transition in transitions)
            {
                //add state if it does not exist
                if (!Map.ContainsKey(transition[0]))
                {
                    Map[transition[0]] = new Dictionary<string, List<string>>();
                }
                //add transition variable for the stat if does not exist
                if (!Map[transition[0]].ContainsKey(transition[1]))
                {
                    Map[transition[0]][transition[1]] = new List<string>();
                }
                //add the state to which the 'state' goes through the 'transitionVariable'
                Map[transition[0]][transition[1]].Add(transition[2]);
            }
        }

        //just iterating through the map and printint it to the console
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
