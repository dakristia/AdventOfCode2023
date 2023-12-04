// Read input file

var inputFile = "input.txt";
var exampleFile = "example.txt";

var lines = File.ReadAllLines(inputFile);


int valueGrowth = 2;
List<int> values = new List<int>();
List<int> copies = new List<int>();

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
    int cardCopies = 0;


    foreach (var number in cardNumbers)
    {
        // Console.WriteLine(number);
        if (winningNumbers.Contains(number))
        {
            // Console.WriteLine($"Found {number} in {card}");
            if (cardValue == 0) cardValue = 1;
            else cardValue *= valueGrowth;

            cardCopies++;
        }
    }

    values.Add(cardValue);
    copies.Add(cardCopies);
}

// Make list of total scratch cards
// Should be as long as the number of cards
List<int> totalScratchCards = new List<int>(values.Count);

// Console.WriteLine("length: " + totalScratchCards.Count);

foreach (var cardIndex in Enumerable.Range(0, values.Count))
{
    Console.WriteLine($"Card {cardIndex + 1}: {values[cardIndex]}, Copies: {copies[cardIndex]}");

    int cardValue = CalculateValue(cardIndex);

    totalScratchCards.Add(cardValue);
}


// declare recursive function
int CalculateValue(int cardIndex)
{
    // int baseValue = values[cardIndex];
    int followingCopies = copies[cardIndex];

    // int finalValue = baseValue;
    int totalCards = 1;

    foreach (var copyIndex in Enumerable.Range(0, followingCopies))
    {
        totalCards += CalculateValue(cardIndex + copyIndex + 1);
    }

    return totalCards;
}

foreach (int val in totalScratchCards)
{
    Console.WriteLine(val);
}

int sum = totalScratchCards.Sum();
Console.WriteLine($"Sum: {sum}");
