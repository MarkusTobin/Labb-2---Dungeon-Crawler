class Game
{
    public LevelData levelData = new LevelData();
    private int turn = 0;
    public void StartGame()
    {
        levelData.Load($"Levels\\Level1.txt");
        Console.Clear();
        Console.WriteLine("Press any key to start the game:");
        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                {
                    break;
                }
                PlayerTurn(key);
                if (!levelData.player.IsAlive())
                {
                    Console.Clear() ;
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have died, game over");
                    Console.ResetColor();
                    break;
                }
                EnemyTurn();
                Turn();
            }
        }
    }
    private void Turn()
    {
        Console.SetCursorPosition(0, 0);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, 0);
        Console.WriteLine($"Turns: {turn}. Health: {levelData.player.Health}");
        turn++;

    }
    private void PlayerTurn(ConsoleKey key)
    {
        levelData.player.Move(key);
        foreach (var element in levelData.Elements)
        {
            if (element is Enemy enemy && levelData.player.X == enemy.X && levelData.player.Y == enemy.Y && enemy.IsAlive())
            {
                levelData.player.Combat(enemy, true);
                if (!enemy.IsAlive())
                {

                }
            }
        }
    }
    private void EnemyTurn()
    {
        List<Enemy> enemiesToRemove = new List<Enemy>();
        foreach (var element in levelData.Elements.ToList())
        {
            if (element is Enemy enemy)
            {
                enemy.Update(levelData.player);
                if (!enemy.IsAlive())
                {
                    enemy.ClearPreviousPosition(enemy.X, enemy.Y);
                    enemiesToRemove.Add(enemy);
                }
            }
        }
        foreach (var enemy in enemiesToRemove)
        {
            levelData.RemoveElement(enemy);
        }
        foreach (var element in levelData.Elements.OfType<Enemy>())
        {
            element.Draw(levelData.player.WithinVision(element));
        }
    }
}

