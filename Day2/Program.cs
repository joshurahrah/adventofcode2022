await Run();

static async Task Run()
{
    using var streamReader = new StreamReader("./PuzzleInput.txt");

    int totalScore = 0;
    while (!streamReader.EndOfStream)
    {
        var line = (await streamReader.ReadLineAsync()).Split(' ');

        var opp = (int)Convert.ToChar(line[0]) - 64;
        var me = DeterminePlay(opp, line[1]);

        totalScore += me;

        if(me == opp)
            totalScore += 3;
        else if (me == 1
            && opp == 3)
            totalScore += 6;
        else if (me == 2
            && opp == 1)
            totalScore += 6;
        else if (me == 3
            && opp == 2)
            totalScore += 6;
            
    }

    Console.WriteLine(totalScore);
}

static int DeterminePlay(int opp, string instruction)
{
    if(instruction == "Y")
    {
        return opp;
    }
    else if(instruction == "X")
    {
        if(opp == 1)
            return 3;
        else if (opp == 2)
            return 1;
        else
            return 2;
    }
    else
    {
        if(opp == 1)
            return 2;
        else if (opp == 2)
            return 3;
        else
            return 1;
    }
}