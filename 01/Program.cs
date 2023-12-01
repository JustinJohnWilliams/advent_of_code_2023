using System.Xml;

var sw = new System.Diagnostics.Stopwatch();
sw.Start();

Console.WriteLine($"*************Day 1 START*************");

part_one();
part_two();

sw.Stop();
Console.WriteLine($"*************Day 1 DONE*************");
Console.WriteLine($"Time (ms): {sw.ElapsedMilliseconds} ms");


void part_one()
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines("input.txt");
    var total = 0;

    foreach(var line in lines)
    {
        var lineResult = "";

        //Console.WriteLine(line);
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

        //Console.WriteLine(lineResult);
        total += Convert.ToInt32(lineResult);
    }


    sw.Stop();

    Console.WriteLine($"*************Part 1 DONE*************");
    Console.WriteLine($"Result: {total}");
    Console.WriteLine($"Time (ms): {sw.ElapsedMilliseconds} ms");
}

void part_two()
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines("input.txt");
    var total = 0;
    //var lines = new List<string>{"6512krnnxdxzprbtlgcfoneeightwohfl"}; //6512182

    foreach(var line in lines)
    {
        var lineResult = "";
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
                if(newThing.IsNumber().isNumber)
                {
                    newLine += newThing.IsNumber().number;
                    Console.WriteLine($"***new thing: {newThing}");
                    Console.WriteLine($"i: {i}\tj: {j}");
                    // manually manipulate i to skip ahead and then subtract the incrementor #sexy
                    //i += j - 1;
                }
            }
        }

        for(int i = 0; i < newLine.Length; i++)
        {
            if(char.IsDigit(newLine[i]))
            {
                lineResult = newLine[i].ToString();
                break;
            }
        }
        for(int i = newLine.Length - 1; i >= 0; i--)
        {
            if(char.IsDigit(newLine[i]))
            {
                lineResult += newLine[i].ToString();
                break;
            }
        }

        if(string.IsNullOrEmpty(lineResult))
        {
            throw new Exception($"something fucked: {line}");
        }

        Console.WriteLine($"{line} : {newLine} : {lineResult}");

        total += Convert.ToInt32(lineResult);
    }

    sw.Stop();
    Console.WriteLine($"*************Part 2 DONE*************"); // first result: 53650 53592
    Console.WriteLine($"Result: {total}");
    Console.WriteLine($"Time (ms): {sw.ElapsedMilliseconds} ms");
}


public static class Extentions
{
    public static (bool isNumber, int number) IsNumber(this string str)
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