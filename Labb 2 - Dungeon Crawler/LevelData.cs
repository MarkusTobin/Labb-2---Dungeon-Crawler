class LevelData
{
    private readonly List<LevelElement> _elements = [];
    public IReadOnlyList<LevelElement> Elements => _elements.AsReadOnly();
    public Player player { get; private set; }
    public void RemoveElement(LevelElement element)
    {
        _elements.Remove(element);
    }
    public void Load(string filename)
    {
        try
        {
            String[] lines = File.ReadAllLines(filename);
            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    char typeOfElement = line[x];
                    switch (typeOfElement)
                    {
                        case '#':
                            _elements.Add(new Wall(x, y)); break;
                        case 'r':
                            _elements.Add(new Rat(x, y, this)); break;
                        case 's':
                            _elements.Add(new Snake(x, y, this)); break;
                        case '@':
                            player = new Player(x, y, this);
                            _elements.Add(player); break;
                    }
                }
            }
        }
        catch
        {
            Console.WriteLine("Something went wrong");
        }
    }

    public void UpdateVisibility(Player player)
    {
        foreach (var element in Elements)
        {
            if(element is Wall wall)
            {
                if (player.WithinVision(wall))
                {
                    wall.DiscoveredWalls = true;
                }
            }
            bool isVisible = player.WithinVision(element);
            element.Draw(isVisible);                       
        }
    }
    public bool IsWallAt(int x, int y)
    {
        foreach (var element in Elements)
        {
            if (element is Wall && element.X == x && element.Y == y)
            {
                return true;
            }
        }
        return false;
    }
    public bool IsEnemyAt(int x, int y)
    {
        foreach (var element in Elements)
        {
            if (element is Enemy enemy && enemy.X == x && enemy.Y == y)
            {
                return true;
            }
        }
        return false;
    }
    public Enemy GetEnemyAt(int x, int y)
    {
        foreach (var element in Elements)
        {
            if (element is Enemy enemy && enemy.X == x && enemy.Y == y)
            {
                return enemy;
            }
        }
        return null;
    }
}
