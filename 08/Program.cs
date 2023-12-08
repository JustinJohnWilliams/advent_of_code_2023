Console.WriteLine($"*************Day 8 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t\t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 8  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines(file);

    var directions = lines[0];
    var nodes = parse_nodes(lines);

    var goal = "ZZZ";
    var result = "";
    var total = 0;
    int i = 0;

    do
    {
        var direction = directions[i];
        var node = total == 0 ? nodes.First(c => c.Key == "AAA").Value : nodes[result];
        result = direction == 'L' ? node.Item1 : node.Item2;
        i = i == directions.Length - 1 ? 0 : i + 1;
        total++;
    }while(result != goal);

    sw.Stop();

    return (total, sw.Elapsed.TotalMilliseconds);
}

(int result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var total = 0;

    sw.Stop();

    return (total, sw.Elapsed.TotalMilliseconds);
}

Dictionary<string, Tuple<string, string>> parse_nodes(string[] instructions)
{
    var results = new Dictionary<string, Tuple<string, string>>();

    foreach(var instruction in instructions)
    {
        if(string.IsNullOrEmpty(instruction) || !instruction.Contains('=')) continue;

        var node = instruction.Split("=", StringSplitOptions.RemoveEmptyEntries)[0].Trim();
        var edges = instruction.Split("=", StringSplitOptions.RemoveEmptyEntries)[1].Trim([' ', '(', ')']).Split(",", StringSplitOptions.RemoveEmptyEntries);

        results.Add(node, new Tuple<string, string>(edges[0].Trim(), edges[1].Trim()));
    }

    return results;
}
