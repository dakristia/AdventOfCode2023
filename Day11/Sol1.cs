var inputFile = "input.txt";
var exampleFile = "example.txt";
var exampleTwoFile = "example2.txt";
var exampleThreeFile = "example3.txt";

var lines = File.ReadAllLines(inputFile);

var universe = lines.ToList();
var expandedY = new List<long>();

for (int i = 0; i < universe.Count; i++)
{
    var line = universe[i];
    // check if line is only "."
    if (line.All(c => c == '.'))
    {
        expandedY.Add(i);
        Console.WriteLine($"Expanded Y: {i}");
    }
}


// Flip galaxy
var flippedUniverse = new List<string>();
for (int j = 0; j < universe[0].Length; j++)
{
    var newString = "";
    for (int i = 0; i < universe.Count; i++)
    {
        newString += universe[i][j];
    }
    flippedUniverse.Add(newString);
}


var expandedX = new List<long>();

for (int i = 0; i < flippedUniverse.Count; i++)
{
    var line = flippedUniverse[i];
    // check if line is only "."
    if (line.All(c => c == '.'))
    {
        expandedX.Add(i);
        Console.WriteLine($"Expanded X: {i}");
    }
}


// Expansion complete

// Find galaxies

var galaxies = new List<(long x, long y)>();

for (int y = 0; y < universe.Count; y++)
{
    var line = universe[y];
    for (int x = 0; x < line.Length; x++)
    {
        if (line[x] == '#')
        {
            var galaxy = (y, x);
            galaxies.Add(galaxy);
        }
    }
}


long totalDistance = 0;
long addedExpansion = 1000000 - 1;


for (int g = 0; g < galaxies.Count; g++)
{
    var galaxy = galaxies[g];
    var (y, x) = galaxy;

    for (int go = g + 1; go < galaxies.Count; go++)
    {
        var otherGalaxy = galaxies[go];
        var (oy, ox) = otherGalaxy;
        for (int i = 0; i < expandedY.Count; i++)
        {
            var expanded = expandedY[i];
            // if any empty space on y axis, expand
            var smallest = (int)Math.Min(y, oy);
            var biggest = (int)Math.Max(y, oy);
            var range = Enumerable.Range(smallest, (biggest - smallest));
            if (range.Contains((int)expanded)) { totalDistance += addedExpansion; }
        }
        for (int i = 0; i < expandedX.Count; i++)
        {
            var expanded = expandedX[i];
            // if any empty space on x axis, expand
            var smallest = (int)Math.Min(x, ox);
            var biggest = (int)Math.Max(x, ox);
            var range = Enumerable.Range(smallest, (biggest - smallest));
            if (range.Contains((int)expanded)) { totalDistance += addedExpansion; }
        }

        var distance = Math.Abs(y - oy) + Math.Abs(x - ox);
        // Console.WriteLine($"Values: {y}, {x} and {oy}, {ox}");
        totalDistance += distance;
        // Console.WriteLine($"Distance: {distance}");
    }
}

foreach (var line in universe)
{
    Console.WriteLine(line);
}
Console.WriteLine($"Total distance: {totalDistance}");