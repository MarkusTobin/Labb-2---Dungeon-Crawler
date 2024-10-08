
class Player : LevelElement
{
    public int visionRange = 5;
    private HashSet<(int, int)> visableWalls = new HashSet<(int, int)>();
    public int Health { get; set; }
    private LevelData levelData;
    public Dice AttackDice { get; set; }
    public Dice DefenceDice { get; set; }
    public bool IsAlive()
    {
        return Health > 0;
    }
    public Player(int x, int y, LevelData levelData) : base(x, y, '@', ConsoleColor.Yellow)
    {
        Health = 100;
        this.levelData = levelData;

        AttackDice = new Dice(2, 6, 2);
        DefenceDice = new Dice(2, 6, 0);

    }
    public bool WithinVision(LevelElement element)
    {
        double distance = Math.Sqrt(Math.Pow(X - element.X, 2) + Math.Pow(Y - element.Y, 2));

        return distance <= visionRange;
    }
    public void Move(ConsoleKey key)
    {

        ClearPreviousPosition(X, Y);

        switch (key)
        {
            case ConsoleKey.UpArrow:
                MoveUp(); break;
            case ConsoleKey.DownArrow:
                MoveDown(); break;
            case ConsoleKey.LeftArrow:
                MoveLeft(); break;
            case ConsoleKey.RightArrow:
                MoveRight(); break;
        }
        levelData.UpdateVisibility(this);
    }

    public void MoveUp()
    {
        if (!levelData.IsWallAt(X, Y - 1) && !levelData.IsEnemyAt(X, Y - 1))
        {
            Y--;
        }
        else if (levelData.IsEnemyAt(X, Y - 1))
        {
            Enemy enemy = levelData.GetEnemyAt(X, Y - 1);
            Combat(enemy);
        }
    }
    public void MoveDown()
    {
        if (!levelData.IsWallAt(X, Y + 1) && !levelData.IsEnemyAt(X, Y + 1))
        {
            Y++;
        }
        else if (levelData.IsEnemyAt(X, Y + 1))
        {
            Enemy enemy = levelData.GetEnemyAt(X, Y + 1);
            Combat(enemy);
        }
    }
    public void MoveLeft()
    {
        if (!levelData.IsWallAt(X - 1, Y) && !levelData.IsEnemyAt(X - 1, Y))
        {
            X--;
        }
        else if (levelData.IsEnemyAt(X - 1, Y))
        {
            Enemy enemy = levelData.GetEnemyAt(X - 1, Y);
            Combat(enemy);
        }
    }
    public void MoveRight()
    {
        if (!levelData.IsWallAt(X + 1, Y) && !levelData.IsEnemyAt(X + 1, Y))
        {
            X++;
        }
        else if (levelData.IsEnemyAt(X + 1, Y))
        {
            Enemy enemy = levelData.GetEnemyAt(X + 1, Y);
            Combat(enemy);
        }
    }

    private int PlayerAttack(Enemy enemy)
    {
        int playerAttack = AttackDice.Throw();
        int enemyDefence = enemy.DefenceDice.Throw();
        int damage = playerAttack - enemyDefence;

        if (damage > 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"You attack {enemy.Name} and deal {damage} dmg");
            enemy.Health -= damage;

            if (!enemy.IsAlive())
            {
                Console.Write(", ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"the stinky {enemy.Name} has died!");
                Console.ResetColor();
                enemy.ClearCurrentPosition(enemy.X, enemy.Y);
                levelData.RemoveElement(enemy);
            }
            else
            {
                Console.WriteLine();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"You attack {enemy.Name} and deal no dmg");
            Console.ResetColor();
        }

        return damage;
    }
    private int EnemyAttack(Player player, Enemy enemy)
    {
        int enemyAttack = enemy.AttackDice.Throw();
        int playerDefence = player.DefenceDice.Throw();
        int enemyDamage = enemyAttack - playerDefence;


        if (enemyDamage > 0)
        {
            player.Health -= enemyDamage;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{enemy.Name} hits you for {enemyDamage} dmg");
            Console.ResetColor();
            /*if (player.Health <= 0)
            {
                Console.SetCursorPosition(0,1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have died, game over");
                return 0;
            }*/
            return enemyDamage;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"{enemy.Name} attacked you, but did no dmg");
            Console.ResetColor();
        }
        return 0;
    }

    public void Combat(Enemy enemy, bool enemyAttackedFirst)
    {
        ClearLine(1);
        ClearLine(2);

        if (enemyAttackedFirst)
        {
            Console.SetCursorPosition(0, 1);
            EnemyAttack(this, enemy);

            if (IsAlive() && enemy.IsAlive())
            {
                PlayerAttack(enemy);
            }
        }
        else
        {
            Console.SetCursorPosition(0, 1);
            PlayerAttack(enemy);

            if (enemy.IsAlive())
            {
                EnemyAttack(this, enemy);
            }
        }
    }
    public void Combat(Enemy enemy)
    {
        Combat(enemy, false);
    }

    public void ClearLine(int lineNumber)
    {
        Console.SetCursorPosition(0, lineNumber);
        Console.Write(new string(' ', Console.WindowWidth));
    }
}



