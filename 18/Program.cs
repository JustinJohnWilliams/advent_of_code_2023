Console.WriteLine($"*************Day 18 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t\t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 18  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines(file);
    var points = get_points(lines);
    var sArea = get_shoelace_area(points);
    var sPerimeter = get_perimeter(points);
    var area = Convert.ToInt64(sArea + (sPerimeter / 2) + 1);

    sw.Stop();
    
    return (area, sw.Elapsed.TotalMilliseconds);
}


(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines(file);
    var points = get_points(lines, true);
    var sArea = get_shoelace_area(points);
    var sPerimeter = get_perimeter(points);
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

double get_shoelace_area(List<(long x, long y)> points)
{
    var v = points.Count;
    var a = 0.0;
    for(int i = 0; i < v - 1; i++)
    {
        a += points[i].x * points[i + 1].y - points[i + 1].x * points[i].y;
    }

    return Math.Abs(a / 2.0);
}

double get_perimeter(List<(long x, long y)> points)
{
    var v = points.Count;
    var p = 0.0;
    for(int i = 0; i < v - 1; i++)
    {
        p += Math.Sqrt(Math.Pow(points[i + 1].x - points[i].x, 2) + Math.Pow(points[i + 1].y - points[i].y, 2));
    }

    return p;
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
