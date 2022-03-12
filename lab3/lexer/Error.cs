using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexer
{
    public class Error
    {
        public string Value { get; set; }

        public Error(string value)
        {
            Value = value;
        }
        public static Error UnexpectedSymbol(PositionTracker positionTracker, char symbol)
        {
            return new Error($"Unexpected symbol: {symbol} at line {positionTracker.Line}, column {positionTracker.Column}");
        }
        
        public static Error ExpectedSymbol(PositionTracker positionTracker, char symbol)
        {
            return new Error($"Expected symbol: {symbol} at line {positionTracker.Line}, column {positionTracker.Column}");
        }
        public override string ToString()
        {
            return $"Error: {Value}";
        }
    }
}
