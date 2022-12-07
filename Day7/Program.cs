using System.Text.RegularExpressions;

await Run();

static async Task Run()
{
    using var streamReader = new StreamReader("./PuzzleInput.txt");

    var directory = new Folder()
    {
        Name = "/"
    };

    var currentFolder = directory;


    while (!streamReader.EndOfStream)
    {
        var line = await streamReader.ReadLineAsync();

        if (line.StartsWith("$ cd"))
        {
            var cmd = line.Substring(5);
            if (cmd == "..")
                currentFolder = currentFolder.Parent;
            else if(cmd == "/")
                currentFolder = directory;
            else
                currentFolder = currentFolder.Folders.FirstOrDefault(x => x.Name == cmd);
        }
        else if(!line.StartsWith("$ ls"))
        {
            if (line.StartsWith("dir"))
            {
                var cmd = line.Substring(4);
                if(!currentFolder.Folders.Exists(x => x.Name == cmd))
                    currentFolder.Folders.Add(new Folder
                    {
                        Name = cmd,
                        Parent = currentFolder
                    });
            }
            else
            {
                var fileRegex = new Regex("([0-9])+");

                var size = fileRegex.Match(line).Value;
                var name = line.Substring(size.Length).Trim();

                currentFolder.Files.Add(new File
                {
                    Name = name,
                    Size = int.Parse(size)
                });
            }
        }
    }
    
    var spaceRemaining = 70000000 - directory.CalcSize();
    var spaceNeeded = 30000000 - spaceRemaining;

    int sum = Search(directory, spaceNeeded, directory.CalcSize());

    Console.WriteLine(sum);
}

static int Search(Folder folder, int spaceNeeded, int idealDir)
{
    var found = idealDir;
    foreach(var f in folder.Folders)
    {
        found = Search(f, spaceNeeded, found);
    }

    var cSize = folder.CalcSize();
    if(cSize > spaceNeeded && cSize < idealDir)
        return cSize;

    return found;
}

public class Folder
{
    public Folder()
    {
        Folders = new();
        Files = new();
    }

    public string Name { get; set; }

    public Folder Parent { get; set; }

    public List<Folder> Folders { get; }

    public List<File> Files { get; }

    public int CalcSize()
    {
        return Calculate(this);
    }

    private int Calculate(Folder folder)
    {
        var childSize = folder.Folders.Select(x => Calculate(x)).Sum();
        var currentSize = folder.Files.Select(x => x.Size).Sum();
        
        return childSize + currentSize;
    }
}

public class File
{
    public string Name { get; set; }
    public int Size { get; set; }
}