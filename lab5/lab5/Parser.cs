namespace lab5
{
    public class Parser
    {
        public List<string> ParsingValue { get; set; }

        public Parser(Grammar grammar, MapMatrix m)
        {
            string initialParsing = "$<";
            for(int i = 1; i < grammar.Input.Length; i++)
            {
                string left = grammar.Input[i - 1].ToString();
                string right = grammar.Input[i].ToString();
                initialParsing += $"{left}{m.Matrix[left][right]}";
            }
            initialParsing += $"{grammar.Input[^1]}>$";
            ParsingValue = initialParsing.Select(x => x.ToString()).ToList();
            Console.WriteLine(string.Join("", ParsingValue));
            Parse(grammar, m);
        }
        private void Parse(Grammar grammar, MapMatrix m)
        {
            if (ParsingValue.Contains("S")) return;
            int posRight = 1;
            int posLeft = 1;
            string derivation = "";
            while (ParsingValue[posRight] != ">")
            {
                if (ParsingValue[posRight] == "<") { posLeft = posRight; derivation = ""; }
                else if (ParsingValue[posRight] != "=") derivation += ParsingValue[posRight];
                posRight++;
            }
            var keysWithDerivation = grammar.P.Where(x =>x.Value.Contains(derivation)).Select(x => x.Key).ToList();
            var newDerivation = "";
            if(keysWithDerivation.Count == 1) newDerivation = keysWithDerivation[0];
            else
            {
                var possibleDerivation = "";
                foreach (var key in keysWithDerivation)
                {
                    var leftDefinition = m.Matrix[ParsingValue[posLeft - 1]][key];
                    var rightDefinition = m.Matrix[key][ParsingValue[posRight + 1]];
                    if(!string.IsNullOrEmpty(leftDefinition) && !string.IsNullOrEmpty(rightDefinition))
                    {
                        if (string.IsNullOrEmpty(possibleDerivation)) possibleDerivation = key;
                        else if(leftDefinition == "=" || rightDefinition == "=") possibleDerivation = key;
                    }
                }
                newDerivation = possibleDerivation;
            }
            ParsingValue.RemoveRange(posLeft + 1, posRight - posLeft - 1);
            ParsingValue.Insert(posLeft + 1, newDerivation);

            ParsingValue[posLeft] = m.Matrix[ParsingValue[posLeft - 1]][ParsingValue[posLeft + 1]];
            ParsingValue[posLeft+2] = m.Matrix[ParsingValue[posLeft + 1]][ParsingValue[posLeft + 3]];

            Console.WriteLine(string.Join("", ParsingValue));
            Parse(grammar, m);
        }
    }
}
