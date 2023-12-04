// Read input file

var inputFile = "input.txt";
var exampleFile = "example.txt";

var lines = File.ReadAllLines(inputFile);



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

List<int> powerList = new List<int>();

foreach (var gameIndex in Enumerable.Range(0, games.Count))
{
    var maxRed = 0;
    var maxGreen = 0;
    var maxBlue = 0;

    var game = games[gameIndex];
    foreach (var set in game)
    {
        foreach (var cube in set.Split(","))
        {
            var trimCube = cube.Trim();
            var num = trimCube.Split(" ")[0];
            var color = trimCube.Split(" ")[1];

            if (color == "red")
            {
                if (int.Parse(num) > maxRed)
                {
                    maxRed = int.Parse(num);
                }
            }
            else if (color == "green")
            {
                if (int.Parse(num) > maxGreen)
                {
                    maxGreen = int.Parse(num);
                }
            }
            else if (color == "blue")
            {
                if (int.Parse(num) > maxBlue)
                {
                    maxBlue = int.Parse(num);
                }
            }
        }
    }

    int power = maxRed * maxGreen * maxBlue;

    powerList.Add(power);

}



var sum = powerList.Sum();

Console.WriteLine($"Sum: {sum}");