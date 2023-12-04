// Read input file

class Sol1
{
    public static void Main(string[] args)
    {

        var inputFile = "input.txt";
        var exampleFile = "example.txt";

        var lines = File.ReadAllLines(inputFile);


        List<int> numbersWithAdjacentSymbol = new List<int>();
        bool continiousNumber = false;

        for (int yCord = 0; yCord < lines.Length; yCord++)
        {
            string currentNum = "";
            var line = lines[yCord];
            for (int xCord = 0; xCord < line.Length; xCord++)
            {
                var currentChar = line[xCord];
                if (char.IsDigit(currentChar))
                {
                    if (!continiousNumber) continiousNumber = true;
                    currentNum += currentChar;


                    // We're at the end of the line, so we need to check if the number is adjacent to a symbol
                    if (xCord == line.Length - 1)
                    {
                        if (continiousNumber)
                        {
                            // + 1 because we want to include the last number
                            int xStart = xCord - (currentNum.Length) + 1;
                            int xEnd = xCord + 1;

                            if (HasAdjacentSymbol(xStart, xEnd, yCord)) numbersWithAdjacentSymbol.Add(int.Parse(currentNum));

                        }
                        continiousNumber = false;
                        currentNum = "";
                    }
                }
                else
                {
                    if (continiousNumber)
                    {
                        int xStart = xCord - (currentNum.Length);
                        int xEnd = xCord;

                        if (HasAdjacentSymbol(xStart, xEnd, yCord)) numbersWithAdjacentSymbol.Add(int.Parse(currentNum));

                    }
                    continiousNumber = false;
                    currentNum = "";
                }
            }
        }

        bool HasAdjacentSymbol(int xStart, int xEnd, int y)
        {
            int adjacentXStart = xStart - 1;
            int adjacentXEnd = xEnd + 1;
            int adjacentYStart = y - 1;
            int adjacentYEnd = y + 1;

            for (int yCord = adjacentYStart; yCord < adjacentYEnd + 1; yCord++)
            {

                for (int xCord = adjacentXStart; xCord < adjacentXEnd; xCord++)
                {
                    try
                    {

                        char currentChar = lines[yCord][xCord];
                        if (!char.IsDigit(currentChar) && currentChar != '.') return true;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        continue;
                    }
                }
            }
            return false;
        }

        // foreach (var number in numbersWithAdjacentSymbol)
        // {
        //     Console.WriteLine(number);
        // }

        int sum = numbersWithAdjacentSymbol.Sum();
        Console.WriteLine($"Sum: {sum}");

    }
}