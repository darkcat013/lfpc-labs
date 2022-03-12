using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexer
{
    public static class Constants
    {
        public const string Digits = "1234567890";
        public const string Letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static readonly Dictionary<string, string> Keywords = new() { 
            { "fun", "FUNCTION" }, 
            { "if", "IF" }, 
            { "elif", "ELIF" }, 
            { "else", "ELSE" },
            { "var", "VAR"},
        };

        public static readonly Dictionary<string, string> PredefinedFunctions = new()
        {
            { "writeln", "WRITELINE" },
            { "main", "MAIN" },
        };
    }
}
