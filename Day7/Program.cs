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
    
    int sum = Searchs(directory);

    Console.WriteLine(sum);
}

static int Searchs(Folder folder)
{
    var total = 0;
    foreach(var f in folder.Folders)
    {
        total += Searchs(f);
    }

    var cSize = folder.CalcSize();
    if(cSize > 100000)
        return total;

    return total += cSize;
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