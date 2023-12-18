Console.WriteLine($"*************Day 18 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t\t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 18  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();

    var lines = File.ReadAllLines(file);
    sw.Start();

    var points = get_points(lines);
    var sArea = calc_shoelace_area(points);
    var sPerimeter = calc_shoelace_perimeter(points);
    // Pick's Theorem
    var area = Convert.ToInt64(sArea + (sPerimeter / 2) + 1);

    sw.Stop();
    
    return (area, sw.Elapsed.TotalMilliseconds);
}


(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();

    var lines = File.ReadAllLines(file);
    sw.Start();

    var points = get_points(lines, true);
    var sArea = calc_shoelace_area(points);
    var sPerimeter = calc_shoelace_perimeter(points);
    var area = Convert.ToInt64(sArea + (sPerimeter / 2) + 1);

    sw.Stop();
    
    return (area, sw.Elapsed.TotalMilliseconds);
}

List<(long x, long y)> get_points(string[] lines, bool elfScrewUp = false)
{
    (long x, long y) digger = (0, 0);
    var points = new List<(long x, long y)>{ digger };

    foreach(var x in lines)
    {
        var color = x.Split(' ')[2].Trim(['(', ')', '#']);

        var direction = elfScrewUp ? color.extract_direction() : x.Split(' ')[0];
        var distance = elfScrewUp ? color.extract_distance() : Convert.ToInt64(x.Split(' ')[1]);
        digger = digger.move(direction, distance);

        points.Add(digger);
    }

    return points;
}

double calc_shoelace_area(List<(long x, long y)> points)
{
    return Math.Abs(
        Enumerable.Range(0, points.Count - 1)
            .Aggregate(0L, (acc, i) =>
                acc + points[i].x * points[i + 1].y  - points[i + 1].x * points[i].y)
        / 2.0);
}

double calc_shoelace_perimeter(List<(long x, long y)> points)
{
    return Enumerable.Range(0, points.Count - 1)
        .Aggregate(0.0, (acc, i) =>
            acc + Math.Sqrt(Math.Pow(points[i + 1].x - points[i].x, 2) + Math.Pow(points[i + 1].y - points[i].y, 2)));
}

public static class Extensions
{
    public static (long x, long y) move(this (long x, long y) point, string direction, long distance)
    {
        return direction switch
        {
            "L" => (point.x - distance, point.y),
            "R" => (point.x + distance, point.y),
            "U" => (point.x, point.y + distance),
            "D" => (point.x, point.y - distance),
            _ => throw new Exception($"{direction} bad")
        };
    }

    public static string extract_direction(this string str)
    {
        return str.Last() switch
        {
            '0' => "R",
            '1' => "D",
            '2' => "L",
            '3' => "U",
            _ => throw new Exception($"{str.Last()} bad")
        };
    }

    public static long extract_distance(this string str)
    {
        return Convert.ToInt64(str[..^1], 16);
    }
}
