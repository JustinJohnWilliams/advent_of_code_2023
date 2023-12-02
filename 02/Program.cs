var sw = new System.Diagnostics.Stopwatch();
sw.Start();

Console.WriteLine($"*************Day 1 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

sw.Stop();

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"Time (total)\t\t: {sw.ElapsedMilliseconds}ms");
Console.WriteLine($"*************Day 1 DONE*************");

(int result, long ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines(file);
    var total = 0;

    foreach(var line in lines)
    {
        var (id, isValid) = process_line(line);
        if(isValid) total += id;
    }

    sw.Stop();

    return (total, sw.ElapsedMilliseconds);
}

(int id, bool isValid) process_line(string line)
{
    var isValid = true;

    var id_and_cubes = line.Split(':');
    if(id_and_cubes.Length != 2) throw new Exception("not enough or too many parts");
    var id_parts = id_and_cubes[0].Split(' ');
    if(id_parts.Length != 2) throw new Exception("not enough parts or too many in id spot");
    if(!int.TryParse(id_parts[1], out int id)) throw new Exception("id not in correct form");

    //Console.WriteLine($"id: {id}");

    foreach(var round in id_and_cubes[1].Split(';'))
    {
        int r = 0, g = 0, b = 0;
        //Console.WriteLine($"round:{round}");
        foreach(var hand in round.Split(','))
        {
            //Console.WriteLine($"hand:{hand.Trim()}");
            var hand_parts = hand.Trim().Split(' ');
            if(hand_parts[1].ToLower() == "red") r += Convert.ToInt32(hand_parts[0]);
            if(hand_parts[1].ToLower() == "green") g += Convert.ToInt32(hand_parts[0]);
            if(hand_parts[1].ToLower() == "blue") b += Convert.ToInt32(hand_parts[0]);
        }

        if(r > 12 || g > 13 || b > 14)
        {
            Console.WriteLine($"{id} is invalid");
            isValid = false;
            break;
        }
    }

    return (id, isValid);

}

(int result, long ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines(file);
    var total = 0;

    foreach(var line in lines)
    {
        var (id, power_rgb) = process_line_two(line);
        total += power_rgb;
    }

    sw.Stop();

    return (total, sw.ElapsedMilliseconds);
}

(int id, int power_rgb) process_line_two(string line)
{
    int max_r = 0, max_g = 0, max_b = 0;

    var id_and_cubes = line.Split(':');
    if(id_and_cubes.Length != 2) throw new Exception("not enough or too many parts");
    var id_parts = id_and_cubes[0].Split(' ');
    if(id_parts.Length != 2) throw new Exception("not enough parts or too many in id spot");
    if(!int.TryParse(id_parts[1], out int id)) throw new Exception("id not in correct form");

    //Console.WriteLine($"id: {id}");

    foreach(var round in id_and_cubes[1].Split(';'))
    {
        int r = 0, g = 0, b = 0;
        //Console.WriteLine($"round:{round}");
        foreach(var hand in round.Split(','))
        {
            //Console.WriteLine($"hand:{hand.Trim()}");
            var hand_parts = hand.Trim().Split(' ');
            if(hand_parts[1].ToLower() == "red") r += Convert.ToInt32(hand_parts[0]);
            if(hand_parts[1].ToLower() == "green") g += Convert.ToInt32(hand_parts[0]);
            if(hand_parts[1].ToLower() == "blue") b += Convert.ToInt32(hand_parts[0]);

            max_r = r > max_r ? r : max_r;
            max_g = g > max_g ? g : max_g;
            max_b = b > max_b ? b : max_b;
        }
    }

    return (id, max_r * max_g * max_b);

}