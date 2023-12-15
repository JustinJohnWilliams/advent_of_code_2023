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

    var lines = File.ReadAllLines(file)[0].Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
    var total = 0;

    var boxes = Enumerable.Range(0, 256).ToDictionary(k => k, v => new List<(string Label, int FocalLength)>());

    foreach(var line in lines)
    {
        var (label, focal_length, hash, remove, addOrUpdate) = line.parse_line();

        var box = boxes[hash];
        var foundIndex = box.FindIndex(c => c.Label == label);

        if(remove && foundIndex >= 0) box.RemoveAt(foundIndex);
        else if(addOrUpdate && foundIndex >= 0) box[foundIndex] = (label, focal_length);
        else if(addOrUpdate) box.Add((label, focal_length));
    }

    for(int box = 0; box < boxes.Count; box++)
    {
        for(int slot = 0; slot < boxes[box].Count; slot++)
        {
            var power = (box + 1) * (slot + 1) * boxes[box][slot].FocalLength;
            total += power;
            //Console.WriteLine($"{boxes[box][slot].Label}: {box + 1} * (slot {slot + 1}) * {boxes[box][slot].FocalLength} (focal length) = {power}");
        }
    }

    sw.Stop();
    
    return (total, sw.Elapsed.TotalMilliseconds);
}

public static class Extensions
{
    public static int hash_it_real_good(this string str)
    {
        return str.Aggregate(0, (acc, cur) => (acc + cur) * 17 % 256);
    }

    public static (string label, int focal_length, int hash, bool remove, bool addOrUpdate) parse_line(this string line)
    {
        var label_and_focal = line.Split(new[] {'-', '='}, StringSplitOptions.RemoveEmptyEntries);

        var label = label_and_focal.First();
        var focal_length = label_and_focal.Length > 1 ? Convert.ToInt32(label_and_focal[1]) : 0;
        var hash = label.hash_it_real_good();

        return (label, focal_length, hash, line.Contains('-'), line.Contains('='));
    }
}
