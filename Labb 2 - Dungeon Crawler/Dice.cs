
public class Dice
{
   public int numberOfDice;
   public int sidesPerDice;
   public int modifier;

    public Dice(int numberOfDice, int sidesPerDice, int modifier)
    {
        this.numberOfDice = numberOfDice;
        this.sidesPerDice = sidesPerDice;
        this.modifier = modifier;  
    }
    public int Throw()
    {
       Random random = new Random();
        int total = 0;

        for (int i = 0; i < numberOfDice; i++)
        {
            total += random.Next(1, sidesPerDice+1);
        }
        total += modifier;
        return total;
    }
}