await Run();

static async Task Run()
{
    using var streamReader = new StreamReader("./PuzzleInput.txt");

    List<string> data = new();
    while (!streamReader.EndOfStream)
    {
        data.Add(await streamReader.ReadLineAsync());
    }
    int[,] grid = new int[data[0].Length, data.Count];

    for (int y = 0; y < grid.GetLength(0); y++)
    {
        for (int x = 0; x < grid.GetLength(1); x++)
        {
            grid[x, y] = int.Parse(data[y][x].ToString());
        }
        Console.WriteLine();
    }

    int max = 0;
    for (int y = 0; y < grid.GetLength(0); y++)
    {
        for (int x = 0; x < grid.GetLength(1); x++)
        {
            var result = Search(grid, x, y);
            if (result > max)
                max = result;
        }
    }

    Console.WriteLine(max);
}

static int Search(int[,] grid, int x, int y)
{
    int leftScore = 0;
    int rightScore = 0;
    int upScore = 0;
    int downScore = 0;

    if (x > 0)
    {
        for (int sx = x - 1; sx >= 0; sx--)
        {
            if (grid[sx, y] < grid[x, y])
                leftScore++;
            else if (grid[sx, y] >= grid[x, y])
            {
                leftScore++;
                sx = -1;
            }
            else
                throw new InvalidOperationException();
        }
    }

    if (x < grid.GetLength(1) - 1)
    {
        for (int sx = x + 1; sx < grid.GetLength(1); sx++)
        {
            if (grid[sx, y] < grid[x, y])
                rightScore++;
            else if (grid[sx, y] >= grid[x, y])
            {
                rightScore++;
                sx = grid.GetLength(1);
            }
            else
                throw new InvalidOperationException();
        }
    }

    if (y > 0)
    {
        for (int sy = y - 1; sy >= 0; sy--)
        {
            if (grid[x, sy] < grid[x, y])
                upScore++;
            else if (grid[x, sy] >= grid[x, y])
            {
                upScore++;
                sy = -1;
            }
            else
                throw new InvalidOperationException();
        }
    }

    if (y < grid.GetLength(0) - 1)
    {
        for (int sy = y + 1; sy < grid.GetLength(0); sy++)
        {
            if (grid[x, sy] < grid[x, y])
                downScore++;
            else if (grid[x, sy] >= grid[x, y])
            {
                downScore++;
                sy = grid.GetLength(0);
            }
            else
                throw new InvalidOperationException();
        }
    }

    return upScore * leftScore * rightScore * downScore;
}