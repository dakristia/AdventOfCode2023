// See https://aka.ms/new-console-template for more information

using System.Linq;

var inputFile = "input.txt";
var exampleFile = "example.txt";
var exampleTwoFile = "example2.txt";
var exampleThreeFile = "example3.txt";

var lines = File.ReadAllLines(inputFile);

var handBidDict = new Dictionary<string, int>();

foreach (var line in lines)
{
    var split = line.Split(" ");
    var hand = split[0];
    var bid = int.Parse(split[1]);
    handBidDict.Add(hand, bid);
}


List<string> cardValues = new List<string>("A K Q T 9 8 7 6 5 4 3 2".Split(" "));

var fiveOfAKindList = new List<string>();
var fourOfAKindList = new List<string>();
var fullHouseList = new List<string>();
var threeOfAKindList = new List<string>();
var twoPairList = new List<string>();
var onePairList = new List<string>();
var highCardList = new List<string>();

foreach (var entry in handBidDict)
{
    var hand = entry.Key;

    var fiveOfAKind = isFiveOfAKind(hand);
    var fourOfAKind = isFourOfAKind(hand);
    var fullHouse = isFullHouse(hand);
    var threeOfAKind = isThreeOfAKind(hand);
    var twoPair = isTwoPair(hand);
    var onePair = isOnePair(hand);


    // if (hand == "77J88")
    // {
    //     Console.WriteLine("77J88");
    //     Console.WriteLine(String.IsNullOrEmpty(fullHouse.item1));
    //     Console.WriteLine(String.IsNullOrEmpty(fullHouse.item2));
    //     Console.WriteLine(fullHouse);
    // }

    if (fiveOfAKind != null) { fiveOfAKindList.Add(hand); continue; }
    if (fourOfAKind != null) { fourOfAKindList.Add(hand); continue; }
    if (!string.IsNullOrEmpty(fullHouse.item1) && !string.IsNullOrEmpty(fullHouse.item2)) { fullHouseList.Add(hand); continue; }
    if (threeOfAKind != null) { threeOfAKindList.Add(hand); continue; }
    if (!string.IsNullOrEmpty(twoPair.item1) && !string.IsNullOrEmpty(twoPair.item2)) { twoPairList.Add(hand); continue; }
    if (onePair != null) { onePairList.Add(hand); continue; }
    else { highCardList.Add(hand); continue; }
}

cardValues.Add("J");

fiveOfAKindList = sortRemainder(fiveOfAKindList);
fourOfAKindList = sortRemainder(fourOfAKindList);
fullHouseList = sortRemainder(fullHouseList);
threeOfAKindList = sortRemainder(threeOfAKindList);
twoPairList = sortRemainder(twoPairList);
onePairList = sortRemainder(onePairList);
highCardList = sortRemainder(highCardList);


List<List<string>> allLists = new List<List<string>>();
allLists.Add(fiveOfAKindList);
allLists.Add(fourOfAKindList);
allLists.Add(fullHouseList);
allLists.Add(threeOfAKindList);
allLists.Add(twoPairList);
allLists.Add(onePairList);
allLists.Add(highCardList);

foreach (var list in allLists)
{
    foreach (var hand in list)
    {
        Console.WriteLine(hand);
    }
    Console.WriteLine();
}

List<string> fullList = new List<string>();
fullList.AddRange(fiveOfAKindList);
fullList.AddRange(fourOfAKindList);
fullList.AddRange(fullHouseList);
fullList.AddRange(threeOfAKindList);
fullList.AddRange(twoPairList);
fullList.AddRange(onePairList);
fullList.AddRange(highCardList);

fullList = fullList.Reverse<string>().ToList();

int sum = 0;


foreach (var hand in fullList)
{
    Console.WriteLine(hand);
    var rank = fullList.IndexOf(hand) + 1;
    sum += handBidDict[hand] * rank;
}

Console.WriteLine(sum);

List<string> sortRemainder(List<string> lists)
{

    if (lists.Count == 0) return lists;


    List<string> sortedHand = null;


    sortedHand = lists
                .OrderBy(x => cardValues.IndexOf(Char.ToString(x[0])))
                .ThenBy(x => cardValues.IndexOf(Char.ToString(x[1])))
                .ThenBy(x => cardValues.IndexOf(Char.ToString(x[2])))
                .ThenBy(x => cardValues.IndexOf(Char.ToString(x[3])))
                .ThenBy(x => cardValues.IndexOf(Char.ToString(x[4])))
                .ToList();

    return sortedHand;
}

// Returns type
string? isFiveOfAKind(string hand)
{

    var first = Char.ToString(hand[0]);
    var second = Char.ToString(hand[1]);
    var third = Char.ToString(hand[2]);
    var fourth = Char.ToString(hand[3]);
    var fifth = Char.ToString(hand[4]);

    foreach (var card in cardValues)
    {
        if ((first == card || first == "J") &&
            (second == card || second == "J") &&
            (third == card || third == "J") &&
            (fourth == card || fourth == "J") &&
            (fifth == card || fifth == "J"))
        {
            return card;
        }
    }
    return null;
}

string? isFourOfAKind(string hand)
{
    var first = Char.ToString(hand[0]);
    var second = Char.ToString(hand[1]);
    var third = Char.ToString(hand[2]);
    var fourth = Char.ToString(hand[3]);
    var fifth = Char.ToString(hand[4]);

    foreach (var card in cardValues)
    {
        //Count number of occurences of card in hand
        var count = 0;
        if (first == card || first == "J") count++;
        if (second == card || second == "J") count++;
        if (third == card || third == "J") count++;
        if (fourth == card || fourth == "J") count++;
        if (fifth == card || fifth == "J") count++;

        if (count == 4)
        {
            return card;
        }
    }

    return null;
}

(string item1, string item2) isFullHouse(string hand)
{
    // Console.WriteLine("NEW CHECK");
    var first = Char.ToString(hand[0]);
    var second = Char.ToString(hand[1]);
    var third = Char.ToString(hand[2]);
    var fourth = Char.ToString(hand[3]);
    var fifth = Char.ToString(hand[4]);

    // Keep counts in here
    var valueDict = new Dictionary<string, int>();
    foreach (var card in cardValues)
    {
        valueDict.Add(card, 0);
    }

    foreach (var card in cardValues)
    {
        //Count number of occurences of card in hand
        var count = 0;
        if (first == card) count++;
        if (second == card) count++;
        if (third == card) count++;
        if (fourth == card) count++;
        if (fifth == card) count++;

        valueDict[card] = count;
    }

    bool hasJoker = false;
    if (hand.ToList().Any(x => x == 'J')) hasJoker = true;

    var twos = valueDict.Where(x => x.Value == 2).ToList();


    // if (hand == "77J88")
    // {
    //     Console.WriteLine("");
    //     Console.WriteLine("77J88");
    //     foreach (var entry in twos)
    //     {
    //         Console.WriteLine(entry);
    //     }
    //     Console.WriteLine((twos.First().Key, twos.Last().Key));
    //     Console.WriteLine("");
    // }

    if (twos.Count == 2 && hasJoker)
    {
        string theFirstUniqueVariableName = twos.First().Key;
        theFirstUniqueVariableName = string.Concat(theFirstUniqueVariableName, "");
        string theSecondUniqueVariableName = twos.Last().Key;
        var result = (theFirstUniqueVariableName, theSecondUniqueVariableName);
        // Console.WriteLine(result);
        return result;
    }


    // Check if there is a 3 and a 2
    string? three = "";
    string? two = "";
    foreach (var entry in valueDict)
    {
        var key = entry.Key;
        var value = entry.Value;

        if (value == 3) three = key;
        if (value == 2) two = key;
    }

    return (three, two);
}

string? isThreeOfAKind(string hand)
{
    var first = Char.ToString(hand[0]);
    var second = Char.ToString(hand[1]);
    var third = Char.ToString(hand[2]);
    var fourth = Char.ToString(hand[3]);
    var fifth = Char.ToString(hand[4]);

    foreach (var card in cardValues)
    {
        //Count number of occurences of card in hand
        var count = 0;
        if (first == card || first == "J") count++;
        if (second == card || second == "J") count++;
        if (third == card || third == "J") count++;
        if (fourth == card || fourth == "J") count++;
        if (fifth == card || fifth == "J") count++;

        if (count == 3)
        {
            return card;
        }
    }

    return null;
}

(string? item1, string? item2) isTwoPair(string hand)
{
    var first = Char.ToString(hand[0]);
    var second = Char.ToString(hand[1]);
    var third = Char.ToString(hand[2]);
    var fourth = Char.ToString(hand[3]);
    var fifth = Char.ToString(hand[4]);

    // Keep counts in here
    var valueDict = new Dictionary<string, int>();
    foreach (var card in cardValues)
    {
        valueDict.Add(card, 0);
    }

    foreach (var card in cardValues)
    {
        //Count number of occurences of card in hand
        var count = 0;
        if (first == card) count++;
        if (second == card) count++;
        if (third == card) count++;
        if (fourth == card) count++;
        if (fifth == card) count++;

        valueDict[card] = count;
    }

    bool hasJoker = false;
    if (hand.ToList().Any(x => x == 'J')) hasJoker = true;

    // Check if there is a 3 and a 2
    string? item1 = "";
    string? item2 = "";
    foreach (var entry in valueDict)
    {
        var key = entry.Key;
        var value = entry.Value;

        if (item1 != "" && key != item1)
        {
            if (value == 2) item2 = key;
            else if (value == 1 && hasJoker) item2 = "J";
        }
        if (value == 2 && item1 == "") item1 = key;
    }

    return (item1, item2);
}

string? isOnePair(string hand)
{
    var first = Char.ToString(hand[0]);
    var second = Char.ToString(hand[1]);
    var third = Char.ToString(hand[2]);
    var fourth = Char.ToString(hand[3]);
    var fifth = Char.ToString(hand[4]);

    foreach (var card in cardValues)
    {
        //Count number of occurences of card in hand
        var count = 0;
        if (first == card || first == "J") count++;
        if (second == card || second == "J") count++;
        if (third == card || third == "J") count++;
        if (fourth == card || fourth == "J") count++;
        if (fifth == card || fifth == "J") count++;
        if (count == 2)
        {
            return card;
        }
    }
    return null;
}

