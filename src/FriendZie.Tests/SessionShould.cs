
namespace FriendZie.Tests;


public class SessionShould
{
    [Fact]
    public void HaveAnInvitationCodeWithLengthOf4()
    {
        var owner = Player.Create("zach");
        var session = Session.Create(owner.Value);

        session.IsSuccess.Should().BeTrue();
        session.Value.InvitationCode.Should().HaveLength(4);
    }

    [Fact]
    public void HaveMaximumNumberOfPlayersGreaterThanMinimumPlayers()
    {
        var owner = Player.Create("zach");
        var session = Session.Create(owner.Value);
        session.Value.MaximumPlayers.Should().BeGreaterThan(session.Value.MinimumPlayers);
    }

    [Fact]
    public void HaveAnOwnerWithName()
    {
        var expectedName = "zach";
        var owner = Player.Create(expectedName);
        var session = Session.Create(owner.Value);

        session.Value.Owner.Should().NotBeNull();
        session.Value.Owner.Name.Should().Be(expectedName);
    }

    [Fact]
    public void HaveOwnerAsPlayer()
    {
        var expectedName = "zach";
        var owner = Player.Create(expectedName);
        var session = Session.Create(owner.Value);

        session.Value.Owner.Should().NotBeNull();
        session.Value.Players.Should().HaveCount(1);
        session.Value.Players.Should().Contain(owner.Value);
    }

    [Fact]
    public void BeAbleToAddPlayers()
    {
        var expectedName = "zach";
        var owner = Player.Create(expectedName);
        var session = Session.Create(owner.Value);

        session = Session.AddPlayer(session.Value, Player.Create("player 1").Value);
        session = Session.AddPlayer(session.Value, Player.Create("player 2").Value);

        session.Value.Players.Should().HaveCount(3);

    }

    [Fact]
    public void NotBeAbleToAddMoreThanMaximumPlayers()
    {
        var expectedName = "zach";
        var owner = Player.Create(expectedName);
        var session = Session.Create(owner.Value);
        var maximumNumberOfPlayers = session.Value.MaximumPlayers;
                
        for(int i=0; i<maximumNumberOfPlayers; i++)
        {
            session = Session.AddPlayer(session.Value, Player.Create("player 1").Value);
        }

        session.Should().BeFailure()
            .Which.Should().HaveReason(SessionErrors.MaximumNumberOfPLayersExceeded(maximumNumberOfPlayers).Message);
        
    }

    [Fact]
    public void ShouldHaveInvitationLink()
    {
        var owner = Player.Create("zach");
        var session = Session.Create(owner.Value);

        session.Value.GameLink.ToString().Should().Contain(session.Value.InvitationCode);
    }

}

