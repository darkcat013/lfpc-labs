using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexer
{
    public class Lexer
    {
        public string Code { get; set; }
        public PositionTracker PositionTracker { get; set; }
        public List<Token> Tokens { get; set; }
        public List<Error> Errors { get; set; }

        public Lexer(string code)
        {
            Code = code;
            PositionTracker = new PositionTracker();
            Errors = new List<Error>();
            Tokens = new List<Token>();
        }

        public void Scan()
        {
            for(PositionTracker.Position = 0; PositionTracker.Position < Code.Length; PositionTracker.Position++, PositionTracker.Column++)
            {
                int i = PositionTracker.Position;

                if (Code[i] == ' ' || Code[i] == '\t' || Code[i] == '\r')
                {
                    continue;
                }
                else if (Code[i] == '\n')
                {
                    PositionTracker.AddLine();
                }
                else if (Code[i].IsDigit())
                {
                    var newToken = TokenGenerator.GenerateNumber(Code, PositionTracker, Errors);
                    if (newToken is not null)
                    {
                        Tokens.Add(newToken);
                    }
                }
                else if (Code[i].IsIdentifierStart())
                {
                    var newToken = TokenGenerator.GenerateIdentifier(Code, PositionTracker, Errors);
                    if (newToken is not null)
                    {
                        Tokens.Add(newToken);
                    }
                }
                else if (Code[i] == '"')
                { 
                    var newTokens = TokenGenerator.GenerateString(Code, PositionTracker, Errors);
                    if (newTokens is not null)
                    {
                        Tokens.AddRange(newTokens);
                    }
                }
                else if (Code[i] == '(')
                {
                    Tokens.Add(new Token(TokenType.LeftParantheses, "("));
                }
                else if (Code[i] == ')')
                {
                    Tokens.Add(new Token(TokenType.RightParantheses, ")"));
                }
                else if (Code[i] == '{')
                {
                    Tokens.Add(new Token(TokenType.LeftBrace, "{"));
                }
                else if (Code[i] == '}')
                {
                    Tokens.Add(new Token(TokenType.RightBrace, "}"));
                }
                else if (Code[i] == ',')
                {
                    Tokens.Add(new Token(TokenType.Comma, ","));
                }
                else if (Code[i] == '-')
                {
                    Tokens.Add(new Token(TokenType.Minus, "-"));
                }
                else if (Code[i] == '+')
                {
                    Tokens.Add(new Token(TokenType.Plus, "+"));
                }
                else if (Code[i] == '*')
                {
                    Tokens.Add(new Token(TokenType.Multiplication, "*"));
                }
                else if (Code[i] == '/')
                {
                    Tokens.Add(new Token(TokenType.Division, "/"));
                }
                else if (Code[i] == '~')
                {
                    Tokens.Add(new Token(TokenType.Return, "~"));
                }
                else if (Code[i].IsComparisonOperator() || Code[i] == '=')
                {
                    if(i < Code.Length - 1 && Code[i + 1] == '=')
                    {
                        Tokens.Add(TokenGenerator.GenerateComparisonOperator(Code, PositionTracker, true));
                    }
                    else 
                    {
                        Tokens.Add(TokenGenerator.GenerateComparisonOperator(Code, PositionTracker, false));
                    }
                }
                else if (Code[i].IsBoolOperatorStart())
                {
                    if(i < Code.Length - 1 && Code[i + 1] == Code[i])
                    {
                        Tokens.Add(TokenGenerator.GenerateBoolOperator(Code, PositionTracker));
                    }
                    else
                    {
                        Errors.Add(Error.UnexpectedSymbol(PositionTracker, Code[i ]));
                    }
                }
                else
                {
                    Errors.Add(Error.UnexpectedSymbol(PositionTracker, Code[i]));
                }
            }
        }

        public void PrintResult()
        {
            if(Errors.Count == 0)
            {
                foreach (var token in Tokens)
                {
                    Console.WriteLine(token);
                }
            }
            else
            {
                foreach (var error in Errors)
                {
                    Console.WriteLine(error);
                }
            }
        }
    }
}
