namespace FriendZie.Tests;

public class D6Should
{
    [Fact]
    public void Have6Sides()
    {

        DieType d6 = new(6);

        d6.Sides.Should().Be(6);

    }

    [Fact]
    public void RollResultLessThanEqualToNumberOfSides()
    {

        DieType d6 = new(6);


        int result = DiceRoller.Roll(d6);

        result.Should().BeLessThanOrEqualTo(d6.Sides);

    }

    [Fact]
    public void RollForMultipleDice()
    {
        var result = DiceRoller.Roll(new D6(), 10);

        result.Should().HaveCount(10);

    }
}