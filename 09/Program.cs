Console.WriteLine($"*************Day 9 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t\t: {p2.ms}ms");
Console.WriteLine($"*************Day 9  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var total = 0;
    foreach(var measurement in get_measurements(file))
    {
        var reducedSet = measurement.Value;
        var predicted = measurement.Value.Last();
        while(!reducedSet.IsReduced())
        {
            reducedSet = reducedSet.Reduce();
            predicted += reducedSet.Last();
        }
        total += predicted;
    }

    sw.Stop();
    
    return (total, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var total = 0;
    foreach(var measurement in get_measurements(file, true))
    {
        var reducedSet = measurement.Value;
        var predicted = measurement.Value.Last();
        while(!reducedSet.IsReduced())
        {
            reducedSet = reducedSet.Reduce();
            predicted += reducedSet.Last();
        }
        total += predicted;
    }

    sw.Stop();
    
    return (total, sw.Elapsed.TotalMilliseconds);
}

Dictionary<int, List<int>> get_measurements(string file, bool unoreverse = false)
{
    var results = new Dictionary<int, List<int>>();

    var lines = File.ReadAllLines(file);
    for(int i = 0; i < lines.Length; i++)
    {
        var nums = lines[i].Split(' ').Select(int.Parse).ToList();
        results.Add(i, unoreverse ? nums.UnoReverse() : nums);
    }

    return results;
}

public static class Extensions
{
    public static bool IsReduced(this List<int> set)
    {
        return set.All(c => c == 0);
    }

    public static List<int> Reduce(this List<int> set)
    {
        return set.Skip(1).Zip(set, (next, current) => next - current).ToList();
    }

    public static List<int> UnoReverse(this List<int> set)
    {
        set.Reverse();
        return set;
    }

    public static void PrettyPrint(this Dictionary<int, List<int>> dict)
    {
        Console.WriteLine(string.Join(", ", dict.Select(kv => $"{kv.Key}: [{string.Join(", ", kv.Value)}]")));
    }
}
