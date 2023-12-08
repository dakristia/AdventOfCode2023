// threeOfAKindDict.Add("K333J", "3");
// fullHouseDict.Add("22432", ("2", "4"));
// Now we have all the hands sorted into their respective categories
// We can now compare the hands

//sort fiveOfAKind
// var sortedFiveOfAKind = fiveOfAKindDict.OrderBy(x => cardValues.IndexOf(x.Value));
// var sortedFourOfAKind = fourOfAKindDict.OrderBy(x => cardValues.IndexOf(x.Value));
// var sortedFullHouse = fullHouseDict.OrderBy(x => cardValues.IndexOf(x.Value.Item2));
// sortedFullHouse = sortedFullHouse.OrderBy(x => cardValues.IndexOf(x.Value.Item1)); // This is perfectly sorted now
// var sortedThreeOfAKind = threeOfAKindDict.OrderBy(x => cardValues.IndexOf(x.Value));
// // var sortedTwoPair = twoPairDict.OrderBy(x => cardValues.IndexOf(x.Value));
// var sortedOnePair = onePairDict.OrderBy(x => cardValues.IndexOf(x.Value));
// var sortedHighCard = highCardDict.OrderBy(x => cardValues.IndexOf(x.Value));


// string prev = "";
// foreach (var hand in sortedFourOfAKind)
// {
//     // sortedFourOfAKind
//     Console.WriteLine(hand.Key + " " + hand.Value);
//     prev = hand.Value;
// }

// foreach (var hand in sortedFullHouse)
// {
//     Console.WriteLine(hand.Key + " " + hand.Value);
// }