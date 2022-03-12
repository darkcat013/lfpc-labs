// See https://aka.ms/new-console-template for more information
using lexer;

var program = File.ReadAllText("noroc.txt");
Lexer lexer = new Lexer(program);
lexer.Scan();
lexer.PrintResult();