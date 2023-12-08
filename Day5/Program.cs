

// Read input file

var inputFile = "input.txt";
var exampleFile = "example.txt";

var lines = File.ReadAllLines(inputFile);


// Get seeds
List<string> seeds = lines[0].Split(" ").Skip(1).ToList();
List<string[]> seedRanges = new List<string[]>();
// List<string> seeds = new List<string>();


// Mange seed range pairs
int seedIndex = 0;
while (seedIndex < seeds.Count)
{
    string seedStart = seeds[seedIndex];
    string seedRangeLength = seeds[seedIndex + 1];

    string[] oneRange = new string[] { seedStart, seedRangeLength };

    seedRanges.Add(oneRange);

    // Console.WriteLine(oneRange[0] + " " + oneRange[1]);

    seedIndex += 2;

}

lines = lines.Skip(1).ToArray();



// Map names to values
Dictionary<string, List<List<long>>> maps = new Dictionary<string, List<List<long>>>();
string currentDict = "";
List<List<long>> currentRanges = new List<List<long>>();

var i = 0;
while (i < lines.Length)
{
    var line = lines[i];
    if (line == "")
    {
        if (currentDict != "") maps.Add(currentDict, currentRanges);

        // If line is empty, then we know the next line will be a map name
        i++;
        currentDict = lines[i].Split(" ")[0];
        currentRanges = new List<List<long>>(); // Reset range list.
    }
    else
    {
        // If string is not empty, then we know it is a range, since we skip the map name
        var stringRanges = line.Split(" ").ToList();
        List<long> ranges = new List<long>();
        foreach (var stringRange in stringRanges)
        {
            ranges.Add(long.Parse(stringRange));
        }
        currentRanges.Add(ranges);
    }

    // Always increment by 1 at the end
    i++;
}
maps.Add(currentDict, currentRanges); // add last set 


// Map source name to map name
Dictionary<string, string> sourceToMapDict = new Dictionary<string, string>();

foreach (var map in maps)
{

    var mapSplit = map.Key.Split("-");
    string source = mapSplit[0];
    string destination = mapSplit[2];

    sourceToMapDict.Add(source, map.Key);

    Console.WriteLine("Source: " + source + " Map: " + map.Key);

}




var sourcesReversed = sourceToMapDict.Keys.ToList();
sourcesReversed.Reverse();

var lastMapSource = sourcesReversed.First();
// var lastMapSource = sourceToMapDict.Keys.ToList().Last();

Console.WriteLine(lastMapSource);


var lastMapName = sourceToMapDict[lastMapSource];

Console.WriteLine(lastMapName);


var lastMap = maps[lastMapName];

// Get first element of every range
var lastMapDestinations = lastMap.Select(r => r[0]).ToList();

var minDestination = lastMapDestinations.Min();

Console.WriteLine(minDestination);

var worthWhileSource = lastMap.Where(s => s[0] == minDestination).First()[1];
var worthWhileSourceDiff = lastMap.Where(s => s[0] == minDestination).First()[2];


Console.WriteLine(minDestination + " " + worthWhileSource + " " + worthWhileSourceDiff);

var secondSource = sourcesReversed[1];

var secondMapName = sourceToMapDict[secondSource];

var secondMap = maps[secondMapName];


// s => Range(s[0], s[2]) >= worthWhileSource && s[0] <= worthWhileSource + worthWhileSourceDiff - 1

// s => s[0] + s[2] >= worthWhileSource && s[0] <= worthWhileSource + worthWhileSourceDiff - 1

secondMap = secondMap.Where(s => s[0] >= worthWhileSource && s[0] <= worthWhileSource + worthWhileSourceDiff - 1).ToList();

Console.WriteLine(secondMap.Count);

foreach (var range in secondMap)
{
    Console.WriteLine(range[0] + " " + range[1] + " " + range[2]);
}

// var aRange = Enumerable.Range(worthWhileSource, worthWhileSourceDiff);


static bool Overlaps(long start1, long end1, long start2, long end2)
{
    return Math.Max(start1, start2) <= Math.Min(end1, end2);
}













//! OLD AND OUTDATED BELOW
// int seedIndex = 0;
// while (seedIndex < seeds.Count)
// {
//     string seedStart = seeds[seedIndex];
//     string seedRangeLength = seeds[seedIndex + 1];

//     string[] oneRange = new string[] { seedStart, seedRangeLength };

//     seedRanges.Add(oneRange);

//     // Console.WriteLine(oneRange[0] + " " + oneRange[1]);

//     seedIndex += 2;

// }

// // return;
// lines = lines.Skip(1).ToArray();

// Dictionary<string, List<List<string>>> maps = new Dictionary<string, List<List<string>>>();
// string currentDict = "";
// List<List<string>> currentRanges = new List<List<string>>();

// var i = 0;
// while (i < lines.Length)
// {
//     var line = lines[i];
//     if (line == "")
//     {
//         if (currentDict != "") maps.Add(currentDict, currentRanges);

//         // If line is empty, then we know the next line will be a map name
//         i++;
//         currentDict = lines[i].Split(" ")[0];
//         currentRanges = new List<List<string>>(); // Reset range list.
//     }
//     else
//     {
//         // If string is not empty, then we know it is a range, since we skip the map name
//         var ranges = line.Split(" ").ToList();
//         currentRanges.Add(ranges);
//     }

//     // Always increment by 1 at the end
//     i++;
// }
// maps.Add(currentDict, currentRanges); // add last set 

// // foreach (var map in maps)
// // {
// //     Console.WriteLine(map.Key);
// //     foreach (var range in map.Value)
// //     {
// //         Console.WriteLine(range[0] + " " + range[1] + " " + range[2]);
// //     }
// //     Console.WriteLine();
// // }

// Dictionary<string, string> sourceToMapDict = new Dictionary<string, string>();

// foreach (var map in maps)
// {

//     var mapSplit = map.Key.Split("-");
//     string source = mapSplit[0];
//     string destination = mapSplit[2];

//     sourceToMapDict.Add(source, map.Key);

// }



// // * Input reading done.

// long? lowestLocation = null;

// //! NB: range[0] IS DESITNATION, range[1] IS SOURCE, range[2] IS RANGE
// //! THATS WEIRD BUT OKAY

// // Attempting to handle logic. We intitally want to start looking at each of the seed ranges from the input file
// // We will use that seed range to generate new ranges based on the seed-to-soil map. We will then use these ranges
// // on the next map, and so on, until we reach the location map. We will then take the last range and find the end
// foreach (var seedRange in seedRanges)
// {
//     List<string[]> upcomingSourceRanges = new List<string[]>();
//     upcomingSourceRanges.Add(seedRange);
//     List<string[]> currentSourceRanges = new List<string[]>();

//     bool done = false;
//     var currentSource = "seed";

//     // Iterates through maps until we reach the location map
//     while (!done)
//     {
//         // Every loop, we set the current seed ranges to the upcoming seed ranges
//         currentSourceRanges = upcomingSourceRanges;
//         upcomingSourceRanges = new List<string[]>();


//         var currentMap = sourceToMapDict[currentSource];

//         Console.WriteLine("Current map: " + currentMap);

//         // Will only be one in the first iteration
//         foreach (var oneSeedRange in currentSourceRanges)
//         {
//             Console.WriteLine(oneSeedRange[0] + " " + oneSeedRange[1]);


//             long seedMin = long.Parse(oneSeedRange[0]);
//             long seedMax = seedMin + long.Parse(oneSeedRange[1]) - 1;


//             // Gets 2d list of ranges
//             var currentMapRanges = maps[currentMap];

//             // Find the ranges that contain the minimum.
//             var minimumRanges = currentMapRanges.Where(
//                 r =>
//                     long.Parse(r[1]) <= seedMin && // seedMin is bigger than or equal to source
//                     (long.Parse(r[1]) + long.Parse(r[2])) > seedMin).ToList(); // Seed is lower than source + range


//             foreach (var minimumRange in minimumRanges)
//             {
//                 Console.WriteLine(minimumRange[0] + " " + minimumRange[1] + " " + minimumRange[2]);
//             }
//             return;

//             long destination = -1;





//             // location is the end polong
//             if (currentSource == "location")
//             {
//                 if (lowestLocation == null || destination < lowestLocation) lowestLocation = destination;
//                 Console.WriteLine("Location: " + destination);
//                 done = true;
//                 break;
//             }

//         }

//         // new source
//         currentSource = currentMap.Split("-")[2];
//     }
// }


// Console.WriteLine("Lowest location: " + lowestLocation);