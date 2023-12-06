Console.WriteLine($"*************Day 5 START*************");

var p1 = part_one("example.txt");
var p2 = part_two("example.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 5  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    read_file(file);

    sw.Stop();

    var result = 0;

    return (0, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();
    sw.Stop();

    var result = 0;

    return (0, sw.Elapsed.TotalMilliseconds);
}

int[] read_file(string file)
{
    var lines = File.ReadAllLines(file);

    var seeds = lines[0].Split(':', StringSplitOptions.RemoveEmptyEntries)[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

    Console.WriteLine($"{string.Join(",", seeds)}");

    foreach(var line in lines)
    {
        if(line.Contains("map"))
        {

        }
    }

    return seeds.Select(c => Convert.ToInt32(c)).ToArray();
}