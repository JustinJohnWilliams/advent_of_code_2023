Console.WriteLine($"*************Day 15 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 15  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines(file)[0].Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
    var total = lines.Sum(c => c.hash_it_real_good());

    sw.Stop();
    
    return (total, sw.Elapsed.TotalMilliseconds);
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

public static class Extensions
{
    public static int hash_it_real_good(this string str)
    {
        return str.Aggregate(0, (acc, cur) => (acc + cur) * 17 % 256);
    }
}
