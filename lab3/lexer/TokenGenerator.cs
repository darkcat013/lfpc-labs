using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexer
{
    public static class TokenGenerator
    {
        public static Token GenerateNumber(string code, PositionTracker positionTracker, List<Error> errors)
        {
            string tokenValue = "";
            bool isFloat = false;
            for (; positionTracker.Position < code.Length; positionTracker.Position++)
            {
                int i = positionTracker.Position;
                
                if (code[i].IsWhiteSpace() || code[i].IsOperator() || code[i].IsParantheses() || code[i].IsBrace())
                {
                    positionTracker.Position--;
                    break;
                }

                positionTracker.Column++;

                if(code[i] == '.' && isFloat)
                {
                    errors.Add(Error.UnexpectedSymbol(positionTracker, code[i]));
                }
                else if(code[i] == '.')
                {
                    tokenValue += code[i];
                    isFloat = true;
                }
                else if (code[i].IsDigit())
                {
                    tokenValue += code[i];
                }
                else
                {
                    errors.Add(Error.UnexpectedSymbol(positionTracker, code[i]));
                }
            }
            
            if(errors.Count > 0)
            {
                return null;
            }
            return isFloat ?
                new Token(TokenType.Float, tokenValue) :
                new Token(TokenType.Int, tokenValue);
        }

        public static Token GenerateIdentifier(string code, PositionTracker positionTracker, List<Error> errors)
        {
            string tokenValue = "";

            for (; positionTracker.Position < code.Length; positionTracker.Position++)
            {
                int i = positionTracker.Position;

                if (code[i].IsWhiteSpace() || code[i].IsOperator() || code[i].IsParantheses() || code[i].IsBrace() || code[i] == ',')
                {
                    positionTracker.Position--;
                    break;
                }

                positionTracker.Column++;

                if (code[i].IsIdentifierSymbol())
                {
                    tokenValue += code[i];
                }
                else
                {
                    errors.Add(Error.UnexpectedSymbol(positionTracker, code[i]));
                }
            }

            if(errors.Count > 0)
            {
                return null;
            }
            if(tokenValue.IsKeyword())
            {

                return new Token($"{TokenType.Keyword}[{Constants.Keywords[tokenValue]}]", tokenValue);
            }
            if (tokenValue.IsPredefinedFunction())
            {
                return new Token($"{TokenType.PredefinedFunction}[{Constants.PredefinedFunctions[tokenValue]}]", tokenValue);
            }
            return new Token(TokenType.Identifier, tokenValue);
        }

        public static List<Token> GenerateString(string code, PositionTracker positionTracker, List<Error> errors)
        {
            List<Token> tokens = new List<Token>();
            tokens.Add(new Token(TokenType.StringStart, "\""));
            positionTracker.Position++;
            positionTracker.Column++;

            string tokenValue = "";

            for (; positionTracker.Position < code.Length; positionTracker.Position++, positionTracker.Column++)
            {
                int i = positionTracker.Position;

                if (code[i] == '"')
                {
                    positionTracker.Position++;
                    positionTracker.Column++;
                    break;
                }

                if (code[i].IsCharacter())
                {
                    tokenValue += code[i];
                }
                else
                {
                    errors.Add(Error.ExpectedSymbol(positionTracker, '"'));
                }
            }
            
            if (errors.Count > 0)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(tokenValue))
            {
                tokens.Add(new Token(TokenType.String, tokenValue));
            }
            tokens.Add(new Token(TokenType.StringEnd, "\""));

            return tokens;
        }

        public static Token GenerateComparisonOperator(string code, PositionTracker positionTracker, bool withEquals)
        {
            string tokenValue = "";
            tokenValue += code[positionTracker.Position];

            if (withEquals)
            {
                positionTracker.Position++;
                positionTracker.Column++;
                tokenValue += code[positionTracker.Position];
                switch(tokenValue[0])
                {
                    case '>': return new Token(TokenType.GreaterOrEqual, tokenValue);
                    case '<': return new Token(TokenType.LessOrEqual, tokenValue);
                    case '=': return new Token(TokenType.Equals, tokenValue);
                    case '!': return new Token(TokenType.NotEquals, tokenValue);
                }
            }
            else
            {
                switch (tokenValue[0])
                {
                    case '>': return new Token(TokenType.Greater, tokenValue);
                    case '<': return new Token(TokenType.Less, tokenValue);
                    case '=': return new Token(TokenType.Assignment, tokenValue);
                    case '!': return new Token(TokenType.Not, tokenValue);
                }
            }
            return null;
        }

        public static Token GenerateBoolOperator(string code, PositionTracker positionTracker)
        {
            string tokenValue = "";
            tokenValue += code[positionTracker.Position];

            positionTracker.Position++;
            positionTracker.Column++;
            tokenValue += code[positionTracker.Position];

            switch (tokenValue[0])
            {
                case '|': return new Token(TokenType.Or, tokenValue);
                case '&': return new Token(TokenType.And, tokenValue);
            }

            return null;
        }
    }
}
