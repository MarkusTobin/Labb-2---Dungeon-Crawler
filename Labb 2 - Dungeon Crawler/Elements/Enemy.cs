abstract class Enemy : LevelElement
{
    public string Name { get; protected set; }
    public int Health { get; set; }
    public Dice AttackDice { get; protected set; }
    public Dice DefenceDice { get; protected set; }

    protected LevelData levelData;
    public Enemy(int x, int y, string name, int health, Dice attackDice, Dice defenceDice, char typeOfElement, ConsoleColor color, LevelData levelData)
        : base(x, y, typeOfElement, color)
    {
        Name = name;
        Health = health;
        AttackDice = attackDice;
        DefenceDice = defenceDice;
        this.levelData = levelData;
    }

    public abstract void Update(Player player);


    public bool IsAlive()
    {
        return Health > 0;
    }
}
