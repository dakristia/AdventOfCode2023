// See https://aka.ms/new-console-template for more information

using System.Linq;

class Sol1
{

    public static void main()
    {


        var inputFile = "input.txt";
        var exampleFile = "example.txt";
        var exampleTwoFile = "example2.txt";

        var lines = File.ReadAllLines(inputFile);

        var handBidDict = new Dictionary<string, int>();

        foreach (var line in lines)
        {
            var split = line.Split(" ");
            var hand = split[0];
            var bid = int.Parse(split[1]);
            handBidDict.Add(hand, bid);
        }


        List<string> cardValues = new List<string>("A K Q J T 9 8 7 6 5 4 3 2".Split(" "));

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

            Console.WriteLine("onePair: " + onePair);

            if (fiveOfAKind != null) { fiveOfAKindList.Add(hand); continue; }
            if (fourOfAKind != null) { fourOfAKindList.Add(hand); continue; }
            if (fullHouse.item1 != null && fullHouse.item2 != null) { fullHouseList.Add(hand); continue; }
            if (threeOfAKind != null) { threeOfAKindList.Add(hand); continue; }
            if (!string.IsNullOrEmpty(twoPair.item1) && !string.IsNullOrEmpty(twoPair.item2)) { Console.WriteLine("twoPair added: " + twoPair); twoPairList.Add(hand); continue; }
            if (onePair != null) { onePairList.Add(hand); continue; }
            else { highCardList.Add(hand); continue; }
        }

        fiveOfAKindList = sortRemainder(fiveOfAKindList);
        fourOfAKindList = sortRemainder(fourOfAKindList);
        fullHouseList = sortRemainder(fullHouseList);
        threeOfAKindList = sortRemainder(threeOfAKindList);
        twoPairList = sortRemainder(twoPairList);
        onePairList = sortRemainder(onePairList);
        highCardList = sortRemainder(highCardList);


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
            var rank = fullList.IndexOf(hand) + 1;
            sum += handBidDict[hand] * rank;
        }

        List<string> sortRemainder(List<string> lists)
        {

            if (lists.Count == 0) return lists;


            List<string> sortedHand = null;


            Console.WriteLine("Before sort: ");
            if (lists[0] == "12345") foreach (var hand in lists) Console.WriteLine(hand);

            sortedHand = lists
                        .OrderBy(x => cardValues.IndexOf(Char.ToString(x[0])))
                        .ThenBy(x => cardValues.IndexOf(Char.ToString(x[1])))
                        .ThenBy(x => cardValues.IndexOf(Char.ToString(x[2])))
                        .ThenBy(x => cardValues.IndexOf(Char.ToString(x[3])))
                        .ThenBy(x => cardValues.IndexOf(Char.ToString(x[4])))
                        .ToList();

            Console.WriteLine(" ");
            Console.WriteLine("After sort: ");
            if (lists[0] == "12345") foreach (var hand in sortedHand) Console.WriteLine(hand);
            Console.WriteLine(" ");
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
                if (first == card && second == card && third == card && fourth == card && fifth == card)
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
                if (first == card) count++;
                if (second == card) count++;
                if (third == card) count++;
                if (fourth == card) count++;
                if (fifth == card) count++;

                if (count == 4)
                {
                    return card;
                }
            }

            return null;
        }

        (string? item1, string? item2) isFullHouse(string hand)
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

            // Check if there is a 3 and a 2
            string? three = null;
            string? two = null;
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
                if (first == card) count++;
                if (second == card) count++;
                if (third == card) count++;
                if (fourth == card) count++;
                if (fifth == card) count++;

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

            // Check if there is a 3 and a 2
            string? three = "";
            string? two = "";
            foreach (var entry in valueDict)
            {
                var key = entry.Key;
                var value = entry.Value;

                if (value == 2 && three != "" && key != three) two = key;
                if (value == 2 && three == "") three = key;
            }

            Console.WriteLine("Returning " + three + " " + two);

            return (three, two);
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
                if (first == card) count++;
                if (second == card) count++;
                if (third == card) count++;
                if (fourth == card) count++;
                if (fifth == card) count++;

                if (count == 2)
                {
                    return card;
                }
            }
            return null;
        }


    }
}