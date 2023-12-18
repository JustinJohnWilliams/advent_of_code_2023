Console.WriteLine($"*************Day 18 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
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
    var area = Convert.ToInt32(sArea + (sPerimeter / 2) + 1);

    sw.Stop();
    
    return (area, sw.Elapsed.TotalMilliseconds);
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

List<(int x, int y)> get_points(string[] lines)
{
    (int x, int y) digger = (0, 0);
    var points = new List<(int x, int y)>{ digger };

    foreach(var x in lines)
    {
        var direction = x.Split(' ')[0];
        var distance = Convert.ToInt32(x.Split(' ')[1]);
        var color = x.Split(' ')[2].Trim(['(', ')', '#']);
        digger = digger.move(direction, distance);

        points.Add(digger);
    }

    return points;
}

double get_shoelace_area(List<(int x, int y)> points)
{
    var v = points.Count;
    var a = 0.0;
    for(int i = 0; i < v - 1; i++)
    {
        a += points[i].x * points[i + 1].y - points[i + 1].x * points[i].y;
    }

    return Math.Abs(a / 2.0);
}

double get_perimeter(List<(int x, int y)> points)
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
    public static (int x, int y) move(this (int x, int y) point, string direction, int distance)
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
}