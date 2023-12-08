

class Sol1
{
    public void main()
    {
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

        var start = "AAA";
        var target = "ZZZ";

        var currentNode = start;
        int steps = 0;


        while (true)
        {
            var currentStep = sequence[steps % sequence.Count];

            if (currentNode == target)
            {
                Console.WriteLine("Found target in " + steps + " steps");
                return;
            }

            if (currentStep == "L") currentNode = network[currentNode].Item1;
            if (currentStep == "R") currentNode = network[currentNode].Item2;
            steps++;
        }

    }
}

