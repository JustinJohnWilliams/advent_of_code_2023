part_one();
part_two();


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

    Console.WriteLine($"*************DONE*************");
    Console.WriteLine($"Result: {total}");
    Console.WriteLine($"Time (ms): {sw.ElapsedMilliseconds} ms");
}

void part_two()
{

}