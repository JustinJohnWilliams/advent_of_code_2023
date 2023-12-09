using System.Data;

var sw = new System.Diagnostics.Stopwatch();
sw.Start();

Console.WriteLine($"*************Day 9 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

sw.Stop();

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"Time (total)\t\t: {sw.Elapsed.TotalMilliseconds}ms");
Console.WriteLine($"*************Day 9  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var total = 0;
    var measurements = get_measurements(file);
    for(int i = 0; i < measurements.Count(); i++)
    {
        var reducedSet = measurements[i];
        int predicted = 0;
        while(!reducedSet.IsReduced())
        {
            reducedSet = reduce(reducedSet);
            predicted += reducedSet.Last();
        }
        //Console.WriteLine($"{i} - {string.Join(",", differentials[i])}");
        predicted += measurements[i].Last();
        //Console.WriteLine($"{i} - {predicted}");
        total += predicted;
    }

    sw.Stop();
    
    return (total, sw.Elapsed.TotalMilliseconds);
}

List<int> reduce(List<int> set)
{
    return set.Skip(1).Zip(set, (next, current) => next - current).ToList();
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines(file);
    var total = 0;

    sw.Stop();
    
    return (total, sw.Elapsed.TotalMilliseconds);
}

Dictionary<int, List<int>> get_measurements(string file)
{
    var results = new Dictionary<int, List<int>>();

    var lines = File.ReadAllLines(file);
    for(int i = 0; i < lines.Length; i++)
    {
        results.Add(i, lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(c => Convert.ToInt32(c)).ToList());
    }

    return results;
}

public static class Extensions
{
    public static bool IsReduced(this List<int> set)
    {
        return set.All(c => c == 0);
    }
}
