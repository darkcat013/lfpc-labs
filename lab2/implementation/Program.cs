using System;

namespace lab2
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("NFA: ");
            NFA nfa = new NFA("NFA.txt");
            nfa.PrintConsole();

            Console.WriteLine();
            DFA dfa = new DFA(nfa);

            Console.WriteLine("\nDFA:");
            dfa.PrintConsole();
        }
    }
}