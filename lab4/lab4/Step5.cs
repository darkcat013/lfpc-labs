using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public static class Step5
    {
        public static Grammar ToChomskyNormalForm(this Grammar grammar)
        {
            var X = new Dictionary<string, string>();
            var Y = new Dictionary<string, string>();

            var chomsky = new Dictionary<string, HashSet<string>>();
            foreach (var (key, values) in grammar.P)
            {
                chomsky[key] = new HashSet<string>();
                foreach (var str in values)
                {
                    if (SatisfiesChomsky(str)) chomsky[key].Add(str);
                    else chomsky[key].Add(ToChomsky(str.Select(c => c.ToString()).ToList()));
                }
            }

            bool SatisfiesChomsky(string str) => grammar.IsTerminal(str) || (str.Length == 2 && grammar.IsNonTerminal(str[0]) && grammar.IsNonTerminal(str[1]));

            void NewX(string symbol)
            {
                X.Add(symbol, $"X{X.Count}");
                grammar.Vn.Add(X[symbol]);
            }

            void NewY(string symbol)
            {
                Y.Add(symbol, $"Y{Y.Count}");
                grammar.Vn.Add(Y[symbol]);
            }

            string ToChomsky(List<string> symbols)
            {
                for(int i = 0; i < 2; i ++)
                {
                    if (grammar.IsNonTerminal(symbols[i])) continue;
                    if (!X.ContainsKey(symbols[i]))
                    {
                        NewX(symbols[i]);
                    }
                    symbols[i] = X[symbols[i]];
                }
                
                return GetChomsky(symbols);
            }

            string GetChomsky(List<string> symbols)
            {
                if (symbols.Count == 2) return symbols[0] + symbols[1];
                if (grammar.IsTerminal(symbols[2]))
                {
                    if (!X.ContainsKey(symbols[2]))
                    {
                        NewX(symbols[2]);
                    }
                    symbols[2] = X[symbols[2]];
                }
                else
                {
                    if (!Y.ContainsKey(symbols[2]))
                    {
                        NewY(symbols[2]);
                    }
                    symbols[2] = Y[symbols[2]];
                }
                if(!Y.ContainsKey(symbols[1]+symbols[2]))
                {
                    NewY(symbols[1] + symbols[2]);
                    symbols[1] = Y[symbols[1]+symbols[2]];
                }
                symbols.RemoveAt(2);
                return GetChomsky(symbols);
            }

            foreach (var (value, key) in X)
            {
                chomsky[key] = new HashSet<string>() { value };
            }
            foreach (var (value, key) in Y)
            {
                chomsky[key] = new HashSet<string>() { value };
            }
            grammar.P = chomsky;
            return grammar;
        }
    }
}
