// Read input file

class Sol1
{
    public static void Main(string[] args)
    {

        var inputFile = "input.txt";
        var exampleFile = "example.txt";

        var lines = File.ReadAllLines(inputFile);

        var maxRed = 12;
        var maxGreen = 13;
        var maxBlue = 14;

        // lines = lines.split(":")[0];
        List<List<string>> games = new List<List<string>>();


        foreach (var line in lines)
        {

            var setLine = line.Split(":")[1];

            // Split sets and remove ','
            var setSplit = setLine.Split(";").Select(x => x.Trim()).ToList();

            List<string> setList = new List<string>(setSplit);

            games.Add(setList);
        }

        List<int> validGameIds = new List<int>();

        foreach (var gameIndex in Enumerable.Range(0, games.Count))
        {
            bool validGame = true;
            var game = games[gameIndex];
            foreach (var set in game)
            {
                foreach (var cube in set.Split(","))
                {
                    var trimCube = cube.Trim();
                    var num = trimCube.Split(" ")[0];
                    var color = trimCube.Split(" ")[1];

                    if (color == "red" && int.Parse(num) > maxRed)
                    {
                        validGame = false;
                        break;
                    }
                    else if (color == "green" && int.Parse(num) > maxGreen)
                    {
                        validGame = false;
                        break;
                    }
                    else if (color == "blue" && int.Parse(num) > maxBlue)
                    {
                        validGame = false;
                        break;
                    }
                }
            }

            if (validGame) validGameIds.Add(gameIndex + 1);
        }


        int sum = validGameIds.Sum();

        Console.WriteLine($"Sum: {sum}");

    }
}