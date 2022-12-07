await Run();

static async Task Run()
{
    using var streamReader = new StreamReader("./PuzzleInput.txt");

    var input = await streamReader.ReadToEndAsync();

    for(int i = 0; i < input.Count(); i++)
    {
        var chars = input.Substring(i, 14);

        if(chars.Distinct().Count() == 14)
        {
            Console.WriteLine(i + 14);

            i = input.Count() + 1;
        }
    }
}