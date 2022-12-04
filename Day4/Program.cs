await Run();

static async Task Run()
{
    using var streamReader = new StreamReader("./PuzzleInput.txt");

    int overlapped = 0;
    while (!streamReader.EndOfStream)
    {
        var line = await streamReader.ReadLineAsync();

        var sections = line
            .Split(',')
            .Select(x => x
                .Split('-')
                .Select(x => int.Parse(x))
                .ToArray())
            .Select(x => Enumerable.Range(x[0], (x[1] - x[0]) + 1).ToList())
            .ToList();
        
        overlapped = (sections[0].Join(sections[1], 
                                            x => x,
                                            y => y,
                                            (x, y) => x).Count() > 0) ? overlapped += 1 : overlapped;
    }

    Console.WriteLine(overlapped);
}