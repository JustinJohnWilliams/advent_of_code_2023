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
        var (left, right) = total == 0 ? nodes.First(c => c.Key == "AAA").Value : nodes[result];
        result = direction == 'L' ? left : right;
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
            var (left, right) = total == 0 ? nodes[node] : nodes[result];
            result = direction == 'L' ? left : right;
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

Dictionary<string, (string left, string right)> parse_nodes(string[] instructions)
{
    var results = new Dictionary<string, (string, string)>();

    foreach(var instruction in instructions)
    {
        if(string.IsNullOrEmpty(instruction) || !instruction.Contains('=')) continue;

        var node = instruction.SplitAndRemove("=")[0].Trim();
        var edges = instruction.SplitAndRemove("=")[1].Trim([' ', '(', ')']).SplitAndRemove(",");

        results.Add(node, (edges[0].Trim(), edges[1].Trim()));
    }

    return results;
}

public static class Extensions
{
    public static string[] SplitAndRemove(this string obj, string? separator, StringSplitOptions opts = StringSplitOptions.RemoveEmptyEntries)
    {
        return obj.Split(separator, opts);
    }
}

/*
30 (L) - SLA: 11653
30 (L) - AAA: 19783
30 (L) - LVA: 19241
30 (L) - NPA: 16531
30 (L) - GDA: 12737
30 (L) - RCA: 14363

11653,19783,19241,16531,12737,14363
GCF => 271

LCM(271,11653,19783,19241,16531,12737,14363) => 9177460370549
*/