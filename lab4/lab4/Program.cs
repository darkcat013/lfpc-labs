using lab4;

var path = "../../../myvariant.txt";
var grammar = new Grammar(path, "S");

Console.WriteLine("Context-Free grammar: ");
Console.WriteLine(grammar);
Console.WriteLine();

Console.WriteLine("Remove empty strings: ");
Console.WriteLine(grammar.RemoveEmpty());
Console.WriteLine();

Console.WriteLine("Remove unit productions: ");
Console.WriteLine(grammar.RemoveUnitProductions());
Console.WriteLine();

Console.WriteLine("Remove unproductive productions: ");
Console.WriteLine(grammar.RemoveUnproductive());
Console.WriteLine();

Console.WriteLine("Remove unaccessible productions: ");
Console.WriteLine(grammar.RemoveUnaccessible());
Console.WriteLine();

Console.WriteLine("Chomsky Normal Form: ");
Console.WriteLine(grammar.ToChomskyNormalForm());