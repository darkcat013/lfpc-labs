using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public class Parser
    {
        public string ParsingValue { get; set; }

        public Parser(Grammar grammar, MapMatrix m)
        {
            ParsingValue += "$<";
            for(int i = 1; i < grammar.Input.Length; i++)
            {
                string left = grammar.Input[i - 1].ToString();
                string right = grammar.Input[i].ToString();
                ParsingValue += $"{left}{m.Matrix[left][right]}";
            }
            ParsingValue += $"{grammar.Input[^1]}>$";
            Console.WriteLine(ParsingValue);
        }
    }
}
