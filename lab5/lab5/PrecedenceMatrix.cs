using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public class PrecedenceMatrix
    {
        Dictionary<string, List<string>> Rule1 { get; set; }
        Dictionary<string, List<string>> Rule2 { get; set; }
        List<Tuple<List<string>, List<string>>> Rule3 { get; set; }
        public MapMatrix M { get; set; }

        public PrecedenceMatrix(FirstLastTable firstLastTable, Grammar grammar)
        {
            Rule1 = CompleteRule1(grammar);
            Rule2 = CompleteRule2(firstLastTable, grammar);
            Rule3 = CompleteRule3(firstLastTable, grammar);
            M = new MapMatrix(grammar.Vn, grammar.Vt);
            FillMatrix();
        }

        private static Dictionary<string, List<string>> CompleteRule1(Grammar grammar)
        {
            var result = new Dictionary<string, List<string>>();
            foreach (var values in grammar.P.Values)
            {
                foreach (var str in values)
                {
                    if (str.Length > 1)
                    {
                        for (int i = 1; i < str.Length; i++)
                        {
                            string key = str[i - 1].ToString();
                            string value = str[i].ToString();

                            if (!result.ContainsKey(key)) result[key] = new List<string>();

                            result[key].Add(value);
                        }
                    }
                }
            }
            return result;
        }

        private static Dictionary<string, List<string>> CompleteRule2(FirstLastTable firstLastTable, Grammar grammar)
        {
            var result = new Dictionary<string, List<string>>();
            foreach (var values in grammar.P.Values)
            {
                foreach (var str in values)
                {
                    if (str.Length > 1)
                    {
                        for (int i = 1; i < str.Length; i++)
                        {
                            string key = str[i - 1].ToString();
                            string value = str[i].ToString();

                            if (grammar.IsTerminal(key) && grammar.IsNonTerminal(value))
                            {
                                if (!result.ContainsKey(key)) result[key] = new List<string>();

                                result[key].AddRange(firstLastTable.First(value));
                            }
                        }
                    }
                }
            }
            return result;
        }

        private static List<Tuple<List<string>, List<string>>> CompleteRule3(FirstLastTable firstLastTable, Grammar grammar)
        {
            var result = new List<Tuple<List<string>, List<string>>>();
            foreach (var values in grammar.P.Values)
            {
                foreach (var str in values)
                {
                    if (str.Length > 1)
                    {
                        for (int i = 1; i < str.Length; i++)
                        {
                            string key = str[i - 1].ToString();
                            string value = str[i].ToString();

                            if (grammar.IsNonTerminal(key) && grammar.IsTerminal(value))
                            {
                                result.Add(new(new(firstLastTable.Last(key)), new() { value }));
                            }
                            else if (grammar.IsNonTerminal(key) && grammar.IsNonTerminal(value))
                            {
                                result.Add(new(new(firstLastTable.Last(key)), new(firstLastTable.First(value).Where(x => grammar.IsTerminal(x)))));
                            }
                        }
                    }
                }
            }
            return result;
        }

        private void FillMatrix()
        {
            foreach (var (key, values) in Rule1)
            {
                foreach (var value in values)
                {
                    M.Matrix[key][value] = "=";
                }
            }

            foreach (var (key, values) in Rule2)
            {
                foreach (var value in values)
                {
                    M.Matrix[key][value] = "<";
                }
            }

            foreach (var (value1, value2) in Rule3)
            {
                foreach (var key in value1)
                {
                    foreach (var value in value2)
                    {
                        M.Matrix[key][value] = ">";
                    }
                }
            }
        }

        public override string ToString()
        {
            string rule1 = "Rule1: ";
            foreach (var (key, values) in Rule1)
            {
                values.ForEach(x => rule1 += $"{key}={x}, ");
            }
            rule1 = rule1.Remove(rule1.Length - 2);
            rule1 += "\n";

            string rule2 = "Rule2: ";
            foreach (var (key, values) in Rule2)
            {
                rule2 += $"{key}<";
                string help = "{";
                values.ForEach(x => help += $"{x}, ");
                help = help.Remove(help.Length - 2);
                help += "}, ";
                rule2 += help;
            }
            rule2 = rule2.Remove(rule2.Length - 2);
            rule2 += "\n";

            string rule3 = "Rule3: ";
            foreach (var (value1, value2) in Rule3)
            {
                string help1 = "{";
                value1.ForEach(x => help1 += $"{x}, ");
                help1 = help1.Remove(help1.Length - 2);
                help1 += "}";

                string help2 = "{";
                value2.ForEach(x => help2 += $"{x}, ");
                help2 = help2.Remove(help2.Length - 2);
                help2 += "}, ";
                rule3 += $"{help1}>{help2}";
            }
            rule3 = rule3.Remove(rule3.Length - 2);
            rule3 += "\n";
            return $"{rule1}{rule2}{rule3}\n{M}";
        }
    }
}
