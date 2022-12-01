await Run();

static async Task Run()
{
    var elfCalories = await ReadElfCaloriesAndSum("./PuzzleInput.txt");

    Console.WriteLine(elfCalories.Max());

    Console.WriteLine(elfCalories.OrderByDescending(x => x).Take(3).Sum());
}


static async Task<List<int>> ReadElfCaloriesAndSum(string path)
{
    using var streamReader = new StreamReader(path);

    int currentSum = 0;
    List<int> elfCalories = new();
    while(!streamReader.EndOfStream)
    {
        var line = await streamReader.ReadLineAsync();
        int value = 0;
        if(string.IsNullOrWhiteSpace(line))
        {
            elfCalories.Add(currentSum);
            currentSum = 0;
        }
        else if(int.TryParse(line, out value))
        {
            currentSum += value;
        }
    }

    return elfCalories;
}