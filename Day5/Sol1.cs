class Sol1
{
    static void Main()
    {
        Console.WriteLine("Hello World!");


        // Read input file

        var inputFile = "input.txt";
        var exampleFile = "example.txt";

        var lines = File.ReadAllLines(inputFile);


        // Get seeds
        List<string> seeds = lines[0].Split(" ").Skip(1).ToList();
        lines = lines.Skip(1).ToArray();

        Dictionary<string, List<List<string>>> maps = new Dictionary<string, List<List<string>>>();
        string currentDict = "";
        List<List<string>> currentRanges = new List<List<string>>();

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
                currentRanges = new List<List<string>>(); // Reset range list.
            }
            else
            {
                // If string is not empty, then we know it is a range, since we skip the map name
                var ranges = line.Split(" ").ToList();
                currentRanges.Add(ranges);
            }

            // Always increment by 1 at the end
            i++;
        }
        maps.Add(currentDict, currentRanges); // add last set 

        foreach (var map in maps)
        {
            Console.WriteLine(map.Key);
            foreach (var range in map.Value)
            {
                Console.WriteLine(range[0] + " " + range[1] + " " + range[2]);
            }
            Console.WriteLine();
        }

        Dictionary<string, string> sourceToMapDict = new Dictionary<string, string>();

        foreach (var map in maps)
        {

            var mapSplit = map.Key.Split("-");
            string source = mapSplit[0];
            string destination = mapSplit[2];

            sourceToMapDict.Add(source, map.Key);

        }



        // * Input reading done.

        long? lowestLocation = null;

        //! NB: range[0] IS DESITNATION, range[1] IS SOURCE, range[2] IS RANGE
        //! THATS WEIRD BUT OKAY

        foreach (var seed in seeds)
        {
            bool done = false;
            var currentSource = "seed";
            var currentMap = sourceToMapDict[currentSource];
            var sourceNum = seed;

            while (!done)
            {
                // Console.WriteLine("Seed: " + seed + " | " + " " + currentSource + " " + sourceNum + "");
                // Gets 2d list of ranges
                var currentMapRanges = maps[currentMap];

                // Find the range that contains the seed
                var possibleRanges = currentMapRanges.Where(
                    r =>
                        long.Parse(r[1]) <= long.Parse(sourceNum) && // sourceNum is bigger than or equal to source
                        (long.Parse(r[1]) + long.Parse(r[2])) > long.Parse(sourceNum)).ToList(); // Seed is lower than source + range


                if (possibleRanges.Count > 1) new Exception("RANGE COUNT SHOULD ONLY BE 1");

                // if (seed == "79" && currentSource == "water")
                // {
                //     Console.WriteLine("Possible ranges: " + possibleRanges.Count);
                //     foreach (var range in possibleRanges)
                //     {
                //         Console.WriteLine(range[0] + " " + range[1] + " " + range[2]);
                //     }

                // }

                long destination = -1;

                if (possibleRanges.Count == 1)
                {
                    var range = possibleRanges[0];

                    long diff = long.Parse(sourceNum) - long.Parse(range[1]);
                    destination = long.Parse(range[0]) + diff;
                }
                else // Count is 0
                {
                    destination = long.Parse(sourceNum);
                }

                // new source
                currentSource = currentMap.Split("-")[2];

                // location is the end polong
                if (currentSource == "location")
                {
                    if (lowestLocation == null || destination < lowestLocation) lowestLocation = destination;
                    Console.WriteLine("Location: " + destination);
                    done = true;
                    break;
                }

                // If we get here, we ready the next map.
                currentMap = sourceToMapDict[currentSource];
                sourceNum = destination.ToString();
            }
        }


        Console.WriteLine("Lowest location: " + lowestLocation);


    }
}