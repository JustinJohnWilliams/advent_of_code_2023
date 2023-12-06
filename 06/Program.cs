var sw = new System.Diagnostics.Stopwatch();
sw.Start();

Console.WriteLine($"*************Day 6 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

sw.Stop();

Console.WriteLine($"Part 1 Result: {p1.result} \t\t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"Time (total)\t\t\t: {sw.Elapsed.TotalMilliseconds}ms");
Console.WriteLine($"*************Day 6 START*************");

(int result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines(file);
    var times = lines[0].Split(":", StringSplitOptions.RemoveEmptyEntries)[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(c => Convert.ToInt32(c));
    var distances = lines[1].Split(":", StringSplitOptions.RemoveEmptyEntries)[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(c => Convert.ToInt32(c));

    var total = 1; 

    for(var i = 0; i < times.Count(); i++)
    {
        var win_count = 0;
        var time_allowed = times.ElementAt(i);
        var distance_to_beat = distances.ElementAt(i);

        for(int t = 1; t <= time_allowed - 1; t++)
        {
            var x = time_allowed - t;

            if(x * t > distance_to_beat)
            {
                win_count++;
            }
        }

        if(win_count > 0)
        {
            total *= win_count;
        }
    }


    sw.Stop();

    return (total, sw.Elapsed.TotalMilliseconds);
}

(int result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines(file);
    var times = lines[0].Split(":", StringSplitOptions.RemoveEmptyEntries)[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
    var distances = lines[1].Split(":", StringSplitOptions.RemoveEmptyEntries)[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

    var time = Convert.ToInt64(string.Join("", times));
    var distance = Convert.ToInt64(string.Join("", distances));
    var total = 0; 

    for(int t = 1; t<= time -1; t++)
    {
        var x = time - t;
        if(x * t > distance)
        {
            total++;
        }
    }

    sw.Stop();

    return (total, sw.Elapsed.TotalMilliseconds);
}