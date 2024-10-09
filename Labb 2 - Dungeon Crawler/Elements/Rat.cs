
class Rat : Enemy
{
    public Rat(int x, int y, LevelData levelData) : base(x, y, "Rat", 10, new Dice(1, 6, 3), new Dice(1, 6, 1), 'r', ConsoleColor.Red, levelData)
    {
    }
    public override void Update(Player player)
    {
        ClearPreviousPosition(X, Y);

        Random random = new Random();
        int direction = random.Next(4);
        Move(direction, player);
        Draw(false);
    }
    private void Move(int direction, Player player)
    {
        int newX = X;
        int newY = Y;
        switch (direction)
        {
            case 0: newY--; break;  // Up
            case 1: newY++; break;  // Down
            case 2: newX--; break;  // Left
            case 3: newX++; break;  // Right
        }

        if (!levelData.IsWallAt(newX, newY) && !levelData.IsEnemyAt(newX, newY))
        {
            if (newX == player.X && newY == player.Y)
            {
                ratCombat(player);
            }
            else
            {
                X = newX;
                Y = newY;
            }
        }
    }
    private void ratCombat(Player player)
    {
        if (player.IsAlive())
        {
            player.Combat(this, true);
        }
    }
}