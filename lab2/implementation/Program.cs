using System;

namespace lab2
{
    static class Program
    {
        static void Main(string[] args)
        {
            //check if file exists
            string path = "NFA.txt";
            CheckFile(path);

            //initialize NFA
            Console.WriteLine("NFA: ");
            NFA nfa = new NFA("NFA.txt");
            nfa.PrintConsole();

            //initialize DFA from given NFA
            Console.WriteLine();
            DFA dfa = new DFA(nfa);

            Console.WriteLine("\nDFA:");
            dfa.PrintConsole();
        }

        //default input is my variant
        static void CheckFile(string path)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path,
                    //input has the form 'state' 'transitionVariable' 'toState'
                    "q0 a q0\n" +
                    "q1 b q2\n" +
                    "q0 a q1\n" +
                    "q2 a q2\n" +
                    "q2 b q3\n" +
                    "q2 c q0");
            }
        }
    }
}