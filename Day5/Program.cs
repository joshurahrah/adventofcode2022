using System.Text.RegularExpressions;

await Run();

static async Task Run()
{
    using var streamReader = new StreamReader("./PuzzleInput.txt");

    Dictionary<int, List<char>> stacks = new();

    List<string> lines = new();

    for (int i = 0; i < 8; i++)
        lines.Add(await streamReader.ReadLineAsync());

    var line = await streamReader.ReadLineAsync();
    var numberOfStacks = line.Where(x => x != ' ').Select(x => int.Parse(x.ToString())).ToList();

    foreach (var n in numberOfStacks)
        stacks.Add(n, new List<char>());

    lines.Reverse();

    foreach (var l in lines)
    {
        for (int i = 0; i < stacks.Count(); i++)
        {
            var letter = l[(i * 4) + 1];

            if (letter == ' ')
                continue;

            stacks[i + 1].Add(letter);
        }
    }

    await streamReader.ReadLineAsync();

    while (!streamReader.EndOfStream)
    {
        var instruct = await streamReader.ReadLineAsync();

        var reg = new Regex(@"([a-z]*)\w+");

        var matches = reg.Matches(instruct);

        var move = int.Parse(matches[1].Value);
        var from = int.Parse(matches[3].Value);
        var to = int.Parse(matches[5].Value);

        var toMove = stacks[from].TakeLast(move).ToList();

        stacks[to].AddRange(toMove);

        for (int i = 0; i < move; i++)
        {
            stacks[from].RemoveAt(stacks[from].Count() - 1);
        }
    }

    Console.WriteLine("");
    for(int i = 1; i < 10; i++)
    {
        var l = stacks[i].Last();
        Console.Write(l);
    }
}