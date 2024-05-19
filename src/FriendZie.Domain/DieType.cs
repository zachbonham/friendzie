
namespace FriendZie.Domain;

public record DieType(int Sides, int Value = 0)
{
    public static implicit operator int(DieType d) => d.Sides;
    public static explicit operator DieType(int i) => new(i);
}

public record D6() : DieType(6);


public class DiceRoller
{
    public static Random random = new Random();

    public static DieType Roll(DieType d)
    {
        var result = random.Next(1, d.Sides + 1);
        return new DieType(d.Sides, result);
    }  
    public static IEnumerable<DieType> Roll(DieType d, int numberOfDice)
    {
        
        var dice = Enumerable.Range(0, numberOfDice).Select(x => Roll(d));
                
        return dice;
    }

}
