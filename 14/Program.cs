Console.WriteLine($"*************Day 14 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("example.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 14  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines(file);

    tilt(lines, Direction.N);
    var total = calculate_load(lines);

    sw.Stop();
    
    return (total, sw.Elapsed.TotalMilliseconds);
}


(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines(file);

    tilt(lines, Direction.N);
    tilt(lines, Direction.W);
    tilt(lines, Direction.S);
    tilt(lines, Direction.E);
    var total = calculate_load(lines);
    foreach(var line in lines)
    {
        Console.WriteLine(line);
    }

    sw.Stop();
    
    return (total, sw.Elapsed.TotalMilliseconds);
}

void tilt(string[] grid, Direction d)
{
    int rows = grid.Length;
    int cols = grid[0].Length;

    switch (d)
    {
        case Direction.N:
            for (int col = 0; col < cols; col++)
            {
                for (int row = 1; row < rows; row++)
                {
                    if (grid[row][col] == 'O')
                    {
                        int targetRow = row;
                        while (targetRow > 0 && grid[targetRow - 1][col] == '.')
                        {
                            targetRow--;
                        }
                        if (targetRow != row)
                        {
                            move_rock(grid, row, col, targetRow - row, 0);
                            row = targetRow;
                        }
                    }
                }
            }
            break;
        case Direction.S:
            for (int col = 0; col < cols; col++)
            {
                for (int row = rows - 2; row >= 0; row--)
                {
                    if (grid[row][col] == 'O')
                    {
                        int targetRow = row;
                        while (targetRow < rows - 1 && grid[targetRow + 1][col] == '.')
                        {
                            targetRow++;
                        }
                        if (targetRow != row)
                        {
                            move_rock(grid, row, col, targetRow - row, 0);
                            row = targetRow;
                        }
                    }
                }
            }
            break;
        case Direction.E:
            for (int row = 0; row < rows; row++)
            {
                for (int col = cols - 2; col >= 0; col--)
                {
                    if (grid[row][col] == 'O')
                    {
                        int targetCol = col;
                        while (targetCol < cols - 1 && grid[row][targetCol + 1] == '.')
                        {
                            targetCol++;
                        }
                        if (targetCol != col)
                        {
                            move_rock(grid, row, col, 0, targetCol - col);
                            col = targetCol;
                        }
                    }
                }
            }
            break;
        case Direction.W:
            for (int row = 0; row < rows; row++)
            {
                for (int col = 1; col < cols; col++)
                {
                    if (grid[row][col] == 'O')
                    {
                        int targetCol = col;
                        while (targetCol > 0 && grid[row][targetCol - 1] == '.')
                        {
                            targetCol--;
                        }
                        if (targetCol != col)
                        {
                            move_rock(grid, row, col, 0, targetCol - col);
                            col = targetCol;
                        }
                    }
                }
            }
            break;
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

void move_rock(string[] grid, int row, int col, int dY, int dX)
{
    char[] currentRowChars = grid[row].ToCharArray();
    char[] newRowChars = grid[row + dY].ToCharArray();

    currentRowChars[col] = '.';
    newRowChars[col + dX] = 'O';

    grid[row] = new string(currentRowChars);
    grid[row + dY] = new string(newRowChars);
}

enum Direction { N, S, E, W }