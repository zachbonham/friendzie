
namespace FriendZie.Tests
{
    public class PlayerShould
    {

        [Fact]
        public void HaveNameLengthLessThanEqualTo24()
        {
            var player = Player.Create(Utility.GenerateRandomCode(24));
            player.Value.Name.Length.Should().BeLessThanOrEqualTo(24);
        }

        [Fact]
        public void FailForValidationErrorWithNameGreaterThan24()
        {
            var player = Player.Create(Utility.GenerateRandomCode(25));

            player.IsFailed.Should().BeTrue();
            
            player.WithError("Invalid name length");
        }
    }
}
