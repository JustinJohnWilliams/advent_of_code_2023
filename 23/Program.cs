Console.WriteLine($"*************Day 23 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 23  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();

    var lines = File.ReadAllLines(file);
    var result = 0L;
    sw.Start();

    var map = create_map(lines);
    var visitedNodes = new bool[map.GetLength(0), map.GetLength(1)];
    
    var xi = 0;
    var yi = Array.IndexOf(lines[0].ToCharArray(), '.');

    result = find_longest_hike(xi, yi, map, visitedNodes, 0, []) - 1;
    
    sw.Stop();
    
    return (result, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();

    var lines = File.ReadAllLines(file);
    var result = 0L;
    sw.Start();

    var map = create_map(lines);
    var visitedNodes = new bool[map.GetLength(0), map.GetLength(1)];
    
    var xi = 0;
    var yi = Array.IndexOf(lines[0].ToCharArray(), '.');

    result = find_longest_hike(xi, yi, map, visitedNodes, 0, [], false) - 1;

    sw.Stop();
    
    return (result, sw.Elapsed.TotalMilliseconds);
}

char[,] create_map(string[] mapInput)
{
    int rows = mapInput.Length;
    int cols = mapInput[0].Length;
    char[,] map = new char[rows, cols];

    for (int i = 0; i < rows; i++)
        for (int j = 0; j < cols; j++)
            map[i, j] = mapInput[i][j];

    return map;
}

bool is_valid_step(int x, int y, char[,] map, bool[,] visited)
{
    if(x < 0 || x >= map.GetLength(0)) return false;
    if(y < 0 || y >= map.GetLength(1)) return false;
    if(map[x,y] == '#') return false;
    if(visited[x,y]) return false;

    return true;
}

int find_longest_hike(int x, int y, char[,] map, bool[,] visited, int currentLength, Dictionary<(int, int), int> memo, bool considerSlopes = true)
{
    if(!is_valid_step(x, y, map, visited)) return currentLength;

    if (memo.TryGetValue((x, y), out int memoizedResult))
        return memoizedResult + currentLength;

    visited[x,y] = true;
    var lengthToBeat = currentLength;

    if(considerSlopes)
    {
        lengthToBeat = map[x, y] switch
        {
            '^' => Math.Max(lengthToBeat, find_longest_hike(x - 1, y, map, visited, currentLength + 1, memo)),
            '>' => Math.Max(lengthToBeat, find_longest_hike(x, y + 1, map, visited, currentLength + 1, memo)),
            'v' => Math.Max(lengthToBeat, find_longest_hike(x + 1, y, map, visited, currentLength + 1, memo)),
            '<' => Math.Max(lengthToBeat, find_longest_hike(x, y - 1, map, visited, currentLength + 1, memo)),
            _ => new[] {
                find_longest_hike(x - 1, y, map, visited, currentLength + 1, memo),
                find_longest_hike(x, y + 1, map, visited, currentLength + 1, memo),
                find_longest_hike(x + 1, y, map, visited, currentLength + 1, memo),
                find_longest_hike(x, y - 1, map, visited, currentLength + 1, memo)
            }.Max()
        };
    }
    else
    {
        lengthToBeat =
             new[] {
                find_longest_hike(x - 1, y, map, visited, currentLength + 1, memo, false),
                find_longest_hike(x, y + 1, map, visited, currentLength + 1, memo, false),
                find_longest_hike(x + 1, y, map, visited, currentLength + 1, memo, false),
                find_longest_hike(x, y - 1, map, visited, currentLength + 1, memo, false)
            }.Max();
    }

    visited[x,y] = false;
    memo[(x, y)] = lengthToBeat - currentLength;
    return lengthToBeat;
}
