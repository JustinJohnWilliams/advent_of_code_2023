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

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines(file);

    var directions = lines[0];
    var nodes = parse_nodes(lines);

    var starting_nodes = nodes.Keys.Where(c => c.EndsWith('A'));
    var results = starting_nodes.ToDictionary(c => c, v => 0);

    foreach(var node in starting_nodes)
    {
        var result = "";
        var total = 0;
        int i = 0;
        do
        {
            var direction = directions[i];
            var n = total == 0 ? nodes[node] : nodes[result];
            result = traverse_node(nodes, direction, n);
            i = i == directions.Length - 1 ? 0 : i + 1;
            total++;
            results[node] = total;
        }while(!result.EndsWith('Z'));
    }

    var lcm = (long)results.Values.First();

    foreach(var result in results)
    {
        var x = lcm;
        lcm = LCM(lcm, result.Value);
    }

    sw.Stop();

    return (lcm, sw.Elapsed.TotalMilliseconds);
}

// hot damn this is cool. thanks luke!
long LCM(long a, long b) => a * b / GCF(a, b);
long GCF(long a, long b) => b == 0 ? a : GCF(b, a % b);


string traverse_node(Dictionary<string, Tuple<string, string>> nodes, char direction, Tuple<string, string> node)
{
    var result = direction == 'L' ? node.Item1 : node.Item2;

    return result;
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
