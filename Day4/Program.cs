await Run();

static async Task Run()
{
    using var streamReader = new StreamReader("./PuzzleInput.txt");

    int overlapped = 0;
    while (!streamReader.EndOfStream)
    {
        var line = await streamReader.ReadLineAsync();

        var sections = line.Split(',');
        
        var elfOne = sections[0].Split('-').Select(x => int.Parse(x)).ToArray();
        var elfTwo = sections[1].Split('-').Select(x => int.Parse(x)).ToArray();

        if((elfOne[0] <= elfTwo[0]
            && elfOne[1] >= elfTwo[1])
            || (elfTwo[0] <= elfOne[0]
                && elfTwo[1] >= elfOne[1])
            ||(elfOne[0] <= elfTwo[0]
                && elfTwo[0] <= elfOne[1])
            || (elfTwo[0] <= elfOne[0]
                && elfTwo[1] >= elfOne[0]))
            {
                overlapped += 1;
            }
    }

    Console.WriteLine(overlapped);
}