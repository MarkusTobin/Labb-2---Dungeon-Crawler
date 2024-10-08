
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
            total += random.Next(1, sidesPerDice+1);       /// Fixa så det skrivs ut  (Attack: 2d6+2 -> total skada {totalSum} eller liknande, samma med def)
        }
        total += modifier;
        return total;
    }
}