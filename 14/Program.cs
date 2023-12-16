Console.WriteLine($"*************Day 14 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 14  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines(file);

    tilt_north(lines);
    var total = calculate_load(lines);

    sw.Stop();
    
    return (total, sw.Elapsed.TotalMilliseconds);
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

void tilt_north(string[] grid)
{
    var rows = grid.Length;
    var cols = grid[0].Length;

    for (int col = 0; col < cols; col++)
    {
        for (int row = 1; row < rows; row++)
        {
            if (grid[row][col] == 'O')
            {
                int currentRow = row;
                while (currentRow > 0 && grid[currentRow - 1][col] == '.')
                {
                    currentRow--;
                }

                if (currentRow != row)
                {
                    char[] rowChars = grid[row].ToCharArray();
                    char[] currentRowChars = grid[currentRow].ToCharArray();

                    rowChars[col] = '.';
                    currentRowChars[col] = 'O';

                    grid[row] = new string(rowChars);
                    grid[currentRow] = new string(currentRowChars);
                }
            }
        }
    }
}

int calculate_load(string[] grid)
{
    var totalLoad = 0;
    var rows = grid.Length;

    for (var row = 0; row < rows; row++)
    {
        var loadForRow = rows - row;
        totalLoad += grid[row].Count(c => c == 'O') * loadForRow;
    }

    return totalLoad;
}
