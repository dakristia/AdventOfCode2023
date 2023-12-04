// Read input file


class Sol1
{
    static void Main(string[] args)
    {

        var inputFile = "input.txt";
        var exampleFile = "example.txt";

        var lines = File.ReadAllLines(inputFile);


        int valueGrowth = 2;
        List<int> values = new List<int>();

        foreach (var card in lines)
        {
            //Split card on '|'
            var cardSplit = card.Split('|');

            // Split winning numbers on ' '
            // Split on any whitespace
            // Remove "Card #" from winning numbers
            // Remove empty entries
            // Convert to list
            var winningNumbers = cardSplit[0].Split(' ').Skip(2).Where(x => !string.IsNullOrEmpty(x)).ToList();

            var cardNumbers = cardSplit[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToList();

            int cardValue = 0;


            foreach (var number in cardNumbers)
            {
                // Console.WriteLine(number);
                if (winningNumbers.Contains(number))
                {
                    // Console.WriteLine($"Found {number} in {card}");
                    if (cardValue == 0) cardValue = 1;
                    else cardValue *= valueGrowth;

                }
            }

            values.Add(cardValue);
        }

        int sum = values.Sum();
        Console.WriteLine($"Sum: {sum}");

    }
}
