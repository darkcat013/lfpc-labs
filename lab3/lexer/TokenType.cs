using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexer
{
    public static class TokenType
    {
        public const string Int = "INT";
        public const string Identifier = "IDENTIFIER";
        public const string Keyword = "KEYWORD";
        public const string PredefinedFunction = "PREDEFINED_FUNCTION";
        public const string StringStart = "STRING_START";
        public const string String = "STRING";
        public const string StringEnd = "STRING_END";
        public const string LeftParantheses = "LEFT_PARANTHESES";
        public const string RightParantheses = "RIGHT_PARANTHESES";
        public const string LeftBrace = "LEFT_BRACE";
        public const string RightBrace = "RIGHT_BRACE";
        public const string Comma = "COMMA";
        public const string Plus = "PLUS";
        public const string Minus = "MINUS";
        public const string Multiplication = "MULTIPLICATION";
        public const string Division = "DIVISION";
        public const string Greater = "GREATER";
        public const string GreaterOrEqual = "GREATER_OR_EQUAL";
        public const string Less = "LESS";
        public const string LessOrEqual = "LESS_OR_EQUAL";
        public const string Not = "NOT";
        public const string NotEquals = "NOT_EQUALS";
        public const string Equals = "EQUALS";
        public const string Assignment = "ASSIGNMENT";
        public const string Return = "RETURN";
        public const string Or = "OR";
        public const string And = "AND";
        public const string Float = "FLOAT";
    }
}
