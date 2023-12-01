Console.WriteLine($"*************Day 1 START*************");

var p1 = part_one("input.txt"); // 55621
var p2 = part_two("input.txt"); // 53592

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 1  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines(file);
    var total = 0;

    foreach(var line in lines)
    {
        total += process_line(line);
    }

    sw.Stop();

    return (total, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines(file);
    var total = 0;

    foreach(var line in lines)
    {
        var newLine = convert_words_to_numbers(line);

        total += process_line(newLine);
    }

    sw.Stop();

    return (total, sw.Elapsed.TotalMilliseconds);
}

int process_line(string line)
{
    var lineResult = "";

    for(int i = 0; i < line.Length; i++)
    {
        if(char.IsDigit(line[i]))
        {
            lineResult = line[i].ToString();
            break;
        }
    }
    for(int i = line.Length - 1; i >= 0; i--)
    {
        if(char.IsDigit(line[i]))
        {
            lineResult += line[i].ToString();
            break;
        }
    }

    if(string.IsNullOrEmpty(lineResult))
    {
        throw new Exception($"something fucked: {line}");
    }

    return Convert.ToInt32(lineResult);
}

string convert_words_to_numbers(string line)
{
    var newLine = "";
    for(int i = 0; i < line.Length; i++)
    {
        // is it already a digit
        if(char.IsDigit(line[i]))
        {
            newLine += line[i];
        }

        // skip if not letter
        if(!char.IsLetter(line[i])) continue;

        // if we parse a number from text append that number
        for(int j = 1; j + i <= line.Length; j++)
        {
            var newThing = line.Substring(i, j);
            if(newThing.Digitize().isNumber)
            {
                newLine += newThing.Digitize().value;
                // manually manipulate i to skip ahead and then subtract the incrementor #sexy
                //i += j - 1;
            }
        }
    }

    return newLine;

}


public static class Extentions
{
    public static (bool isNumber, int value) Digitize(this string str)
    {
        return str.ToLower() switch
        {
            "zero"  => (true, 0),
            "one"   => (true, 1),
            "two"   => (true, 2),
            "three" => (true, 3),
            "four"  => (true, 4),
            "five"  => (true, 5),
            "six"   => (true, 6),
            "seven" => (true, 7),
            "eight" => (true, 8),
            "nine"  => (true, 9),
            _ => (false, 0),
        };
    }

}