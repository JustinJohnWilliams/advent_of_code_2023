Console.WriteLine($"*************Day 3 START*************");

var p1 = part_one_two("example.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 3  DONE**************");

(int result, double ms) part_one_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var arr = read_into_array(file);
    var wholes = new List<string>();

    var row_length = arr.GetLength(0);
    var col_length = arr.GetLength(1);

    for(int i = 0; i < col_length; i++)
    {
        for(int j = 0; j < row_length; j++)
        {
            var wholeNumber = "";
            var startIdx = j;
            while(arr[i,j].isNum)
            {
                wholeNumber += arr[i,j].c;
                if(j < row_length - 1) j++;
            }

            var row = i;
            var column_begin = 0;
            var column_end = 0;

            //check row above
            if(i > 0)
            {
                row = i - 1;
                column_begin = startIdx > 0 ? startIdx - 1 : startIdx;
                column_end = j < col_length ? j + 1 : j;
                for(int s = column_begin; s <= column_end; s++)
                {
                    if(arr[row, s].isChar)
                    {
                        wholes.Add(wholeNumber);
                    }
                }

            }
            //check current row
            row = i;
            column_begin = startIdx > 0 ? startIdx - 1 : startIdx;
            column_end = j < col_length ? j + 1 : j;
            for(int s = column_begin; s <= column_end; s++)
            {
                if(arr[row, s].isChar)
                {
                    wholes.Add(wholeNumber);
                }
            }

            //check row below
            if(i < row_length - 1)
            {
                row = i + 1;
                column_begin = startIdx > 0 ? startIdx - 1 : startIdx;
                column_end = j < col_length ? j + 1 : j;
                for(int s = column_begin; s <= column_end; s++)
                {
                    if(arr[row, s].isChar)
                    {
                        wholes.Add(wholeNumber);
                    }
                }
            }

        }
    }

    int result = 0;

    foreach(var x in wholes)
    {
        result += Convert.ToInt32(x);
    }

    Console.WriteLine(string.Join(",", wholes));

    sw.Stop();
    return (result, sw.Elapsed.TotalMilliseconds);
}

(int result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var arr = read_into_array(file);
    var wholes = new List<string>();
    for (int i = 0; i < arr.GetLength(0); i++)
    {
        for (int j = 0; j < arr.GetLength(1); j++)
        {
            var startIdx = j;
            var endIdx = startIdx == arr.GetLength(1) - 1 ? startIdx : startIdx +1;
            var currValue = arr[i,j]; // 4
            if(currValue.isNum)
            {
                while(char.IsDigit(arr[i,endIdx].c) && endIdx < arr.GetLength(1) - 1)
                {
                    endIdx++;
                }

                var wholeNumber = "";

                for(int a = startIdx; a <= endIdx - 1; a++)
                {
                    wholeNumber += arr[i,a].c.ToString();
                }

                var finished = false;
                while(!finished)
                {
                    //check row above
                    if(i > 0)
                    {
                        for(int s = (startIdx > 0 ? startIdx - 1 : 0); s <= (endIdx < arr.GetLength(1) - 1 ? endIdx : arr.GetLength(1) - 1); s++)
                        {
                            if(arr[i-1,s].isChar)
                            {
                                wholes.Add(wholeNumber);
                                finished = true;
                                continue;
                            }
                        }

                        if(finished) continue;
                    }
                    // check current row
                    // check left
                    if(startIdx > 0)
                    {
                        if(arr[i, startIdx -1].isChar)
                        {
                            wholes.Add(wholeNumber);
                            finished = true;
                            continue;
                        }

                    }
                    // check right
                    if(endIdx < arr.GetLength(1) - 1)
                    {
                        if(arr[i, endIdx].isChar)
                        {
                            wholes.Add(wholeNumber);
                            finished = true;
                            continue;
                        }
                    }
                    // check next row
                    if(i < arr.GetLength(0) - 1)
                    {
                        for(int s = (startIdx > 0 ? startIdx - 1 : 0); s <= (endIdx < arr.GetLength(1) - 1 ? endIdx : arr.GetLength(1) - 1); s++)
                        {
                            if(arr[i+1,s].isChar)
                            {
                                wholes.Add(wholeNumber);
                                finished = true;
                                continue;
                            }
                        }
                        if(finished) continue;
                    }

                    //Console.WriteLine($"Skipping: {wholeNumber}");
                    finished = true;
                }

                j = endIdx;
            }
        }
    }

    int result = 0;

    foreach(var x in wholes)
    {
        result += Convert.ToInt32(x);
    }

    Console.WriteLine(string.Join(",", wholes));

    sw.Stop();
    return (result, sw.Elapsed.TotalMilliseconds);
}

(int result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    sw.Stop();
    return (0, sw.Elapsed.TotalMilliseconds);
}

(char c,bool isChar,bool isNum)[,] read_into_array(string file)
{
    string[] lines = File.ReadAllLines(file);
    int rows = lines.Length;
    int cols = (rows > 0) ? lines[0].Length : 0;
    (char,bool,bool)[,] twoDArray = new (char,bool,bool)[rows, cols];

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            if (j < lines[i].Length)
            {
                var c = lines[i][j];
                var isNum = char.IsNumber(c);
                var isChar = !isNum && c != '.';
                twoDArray[i, j] = (c, isChar, isNum);
            }
            else
            {
                throw new Exception("sumthin' bad");
            }
        }
    }

    return twoDArray;
}