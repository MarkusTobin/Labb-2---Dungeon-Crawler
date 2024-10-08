
abstract class LevelElement
{
    public int X { get; set; }
    public int Y { get; set; }
    protected char TypeOfElement { get; }
    public ConsoleColor Color { get; }
    public bool DiscoveredWalls { get; set; }

    public LevelElement(int x, int y, char typeOfElement, ConsoleColor color)
    {
        X = x;
        Y = y;
        TypeOfElement = typeOfElement;
        Color = color;
    }
    public virtual void Draw(bool isVisible)
    {
        if (isVisible || DiscoveredWalls)
        {

        Console.SetCursorPosition(X, Y + 3);
        Console.CursorVisible = false;
        Console.ForegroundColor = Color;
        Console.Write(TypeOfElement);
        Console.ResetColor();
        }
        else
        {
            ClearPreviousPosition(X,Y);
        }
    }
    public void ClearPreviousPosition(int x, int y)
    {
        Console.SetCursorPosition(X, Y + 3);
        Console.Write(' ');
    }
    public void ClearCurrentPosition(int x, int y)
    {
        Console.SetCursorPosition(X, Y + 3);
        Console.Write(' ');
    }


}