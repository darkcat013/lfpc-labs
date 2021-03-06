using lab5;

var path = "../../../class.txt";
var grammar = new Grammar(path, "S");

Console.WriteLine("Initial grammar: ");
Console.WriteLine(grammar);
Console.WriteLine();

var firstLastTable = new FirstLastTable(grammar);
Console.WriteLine("First Last table: ");
Console.WriteLine(firstLastTable);

var precedenceMatrix = new PrecedenceMatrix(firstLastTable, grammar);
Console.WriteLine("Rules and Precedence matrix: ");
Console.WriteLine(precedenceMatrix);

Console.WriteLine("Parsing: ");
var parser = new Parser(grammar, precedenceMatrix.M);