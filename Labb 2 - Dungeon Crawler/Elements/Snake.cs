
class Snake : Enemy
{
    public Snake(int x, int y, LevelData levelData) : base(x, y, "Snake", 25, new Dice(1, 6, 4), new Dice(2, 6, 0), 's', ConsoleColor.Green, levelData)
    {
    }
    public override void Update(Player player)
    {


        int distansFromPlayer = (int)Math.Sqrt(Math.Pow(X - player.X, 2) + Math.Pow(Y - player.Y, 2));
        if (distansFromPlayer <= 2)
        {
            ClearPreviousPosition(X, Y);
            MoveAwayFromPlayer(player);
        }
        Draw(false);
    }
    private void MoveAwayFromPlayer(Player player)
    {
        if (player.X < X && !levelData.IsWallAt(X + 1, Y) && !levelData.IsEnemyAt(X + 1, Y)) X++;
        else if (player.X > X && !levelData.IsWallAt(X - 1, Y) && !levelData.IsEnemyAt(X - 1, Y)) X--;
        if (player.Y < Y && !levelData.IsWallAt(X, Y + 1) && !levelData.IsEnemyAt(X, Y + 1)) Y++;
        else if (player.Y > Y && !levelData.IsWallAt(X, Y - 1) && !levelData.IsEnemyAt(X, Y - 1)) Y--;
    }
}