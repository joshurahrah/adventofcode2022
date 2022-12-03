await Run();

static async Task Run()
{
    using var streamReader = new StreamReader("./PuzzleInput.txt");

    int totalScore = 0;
    List<string> rucksacks = new List<string>();
    while (!streamReader.EndOfStream)
    {
        rucksacks.Add(await streamReader.ReadLineAsync());
    }

    for(int i = 0; i < rucksacks.Count; i = i + 3)
    {
        var firstElf = rucksacks[i];
        var secondElf = rucksacks[i + 1];
        var thirdElf = rucksacks[i + 2];

        var common = firstElf.Join(secondElf,
                                    f => f,
                                    s => s,
                                    (f, s) => f)
                                .Join(thirdElf,
                                    f => f,
                                    t => t,
                                    (f, t) => f)
                                    .Distinct()
                                    .First();
        

        var numeric = Char.IsUpper(common)
                        ? (int)Convert.ToChar(common) - 38
                        : (int)Convert.ToChar(common) - 96;
    
        totalScore += numeric;
    }

    Console.WriteLine(totalScore);
}