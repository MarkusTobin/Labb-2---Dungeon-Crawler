
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
    private int ratAttack(Player player, Enemy enemy)
    {
        int enemyAttack = enemy.AttackDice.Throw();
        int playerDefence = player.DefenceDice.Throw();
        int enemyDamage = enemyAttack - playerDefence;

        if (enemyDamage > 0)
        {
            player.Health -= enemyDamage;
            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{enemy.Name} attacked you, dealing {enemyDamage} dmg");
            Console.ResetColor();
            if (!player.IsAlive())
            {
                Console.SetCursorPosition(0, 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have died, game over");
                // Implement game over logic if needed no need
            }
            return enemyDamage;
        }
        else
        {
            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"{enemy.Name} attacked you, but did no dmg");
            Console.ResetColor();
        }
        return 0;
    }
}