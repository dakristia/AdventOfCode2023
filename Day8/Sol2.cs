var inputFile = "input.txt";
var exampleFile = "example.txt";
var exampleTwoFile = "example2.txt";
var exampleThreeFile = "example3.txt";

var lines = File.ReadAllLines(inputFile);

var sequence = lines[0].ToCharArray().Select(c => c.ToString()).ToList();

lines = lines.Skip(2).ToArray();

var network = new Dictionary<string, (string, string)>();

foreach (var line in lines)
{
    var parts = line.Split(" = (");
    var node = parts[0];
    var intructions = parts[1].Remove(parts[1].Length - 1).Split(", ");

    network.Add(node, (intructions[0], intructions[1]));
}

var start = "A";
var target = "Z";

var allNodes = network.Keys.ToList();
// Get all nodes that start with A
var currentNodes = allNodes.Where(n => n.EndsWith(start)).ToList();
Dictionary<int, int> stepsToDividedDict = new Dictionary<int, int>();

int steps = 0;

Console.WriteLine("Number of starting nodes: " + currentNodes.Count);
Console.WriteLine("Sequence length: " + sequence.Count);

while (currentNodes.Count > 0)
{
    var currentStep = sequence[steps % sequence.Count];

    var toRemove = new List<string>();

    foreach (var n in currentNodes)
    {
        if (n.EndsWith(target))
        {
            Console.WriteLine("Found target in " + steps + " steps. Divided by sequence length " + (steps / sequence.Count) + " times");
            toRemove.Add(n);
        }
    }
    foreach (var n in toRemove) { currentNodes.Remove(n); }

    if (currentStep == "L")
    {
        currentNodes = currentNodes.Select(n => network[n].Item1).ToList();
    }
    if (currentStep == "R")
    {
        currentNodes = currentNodes.Select(n => network[n].Item2).ToList();
    }
    steps++;
}




Console.WriteLine("Steps performed  " + steps + " steps");
