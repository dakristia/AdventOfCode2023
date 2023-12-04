// Read input file

var inputFile = "input.txt";
var exampleFile = "example.txt";

var lines = File.ReadAllLines(inputFile);


List<int> cogsWithTwoNumbers = new List<int>();


for (int yCord = 0; yCord < lines.Length; yCord++)
{
    var line = lines[yCord];
    for (int xCord = 0; xCord < line.Length; xCord++)
    {
        var currentChar = line[xCord];
        if (currentChar == '*')
        {
            Console.WriteLine("Found a star at " + yCord + ", " + xCord);
            cogsWithTwoNumbers.Add(CalculateAdjacentNumbers(xCord, yCord));
            // Check if surrounded by numbers

        }
    }
}

// foreach (var number in cogsWithTwoNumbers)
// {
//     Console.WriteLine(number);
// }

int sum = cogsWithTwoNumbers.Sum();
Console.WriteLine($"Sum: {sum}");

int CalculateAdjacentNumbers(int x, int y)
{
    int numberOfNumbers = 0;
    int product = 1;

    int adjacentXStart = x - 1;
    int adjacentXEnd = x + 1;
    int adjacentYStart = y - 1;
    int adjacentYEnd = y + 1;

    for (int yCord = adjacentYStart; yCord < adjacentYEnd + 1; yCord++)
    {
        List<int> checkedXIndexes = new List<int>();

        for (int xCord = adjacentXStart; xCord <= adjacentXEnd; xCord++)
        {
            try
            {

                char currentChar = lines[yCord][xCord];
                if (char.IsDigit(currentChar))
                {
                    if (checkedXIndexes.Contains(xCord - 1))
                    {
                        checkedXIndexes.Add(xCord);
                        continue; // If we've already checked the number to the left, we've found the full number
                    }
                    // Console.WriteLine("Found a number at " + yCord + ", " + xCord);
                    numberOfNumbers++;
                    if (numberOfNumbers > 2) return 0;

                    product *= findFullNumber(xCord, yCord);
                    checkedXIndexes.Add(xCord);
                }
            }
            catch (IndexOutOfRangeException)
            {
                continue;
            }
        }
    }

    if (numberOfNumbers != 2) return 0;

    Console.WriteLine("Product: " + product);
    return product;
}


int findFullNumber(int x, int y)
{
    string fullNumber = "";

    string center = lines[y][x].ToString();
    // Console.WriteLine("center: " + center);
    string left = "";
    string right = "";

    int tempX = x - 1;
    while (tempX >= 0 && char.IsDigit(lines[y][tempX]))
    {
        left = lines[y][tempX].ToString() + left;
        tempX--;
    }

    tempX = x + 1;
    while (tempX <= lines[y].Length && char.IsDigit(lines[y][tempX]))
    {
        right = right + lines[y][tempX].ToString();
        tempX++;
    }

    fullNumber = left + center + right;
    Console.WriteLine("fullNumber: " + fullNumber);

    return int.Parse(fullNumber);
}
