Console.WriteLine($"*************Day 7 START*************");

var p1 = part_one("example.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t\t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 7  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var total = 0;

    sw.Stop();

    return (total, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var total = 0;

    sw.Stop();

    return (total, sw.Elapsed.TotalMilliseconds);
}

enum CamelType
{
    FiveOfAKind,
    FourOfAKind,
    FullHouse,
    ThreeOfAKind,
    TwoPair,
    OnePair,
    HighCard
}

class Hand
{
    public Hand(int id, string line)
    {
        Id = id;
        Raw = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0].Trim();
        Bid = Convert.ToInt32(line.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1].Trim());
        Type = GetCamelType(Raw);
    }

    public int Id { get; set; }
    public string Raw { get; set; }
    public int Bid { get; set;}
    public CamelType Type {get; set;}
    public int Rank {get; set;}

    CamelType GetCamelType(string raw)
    {
        for(int i = 0; i < raw.Length; i++)
        {
            
        }
    }

}