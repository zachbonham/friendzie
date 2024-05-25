using FriendZie.Domain.Player;
using MediatR;

namespace FriendZie.Api.Game.AddPlayer;

public class AddPlayerRequstHandler(IGameRepository db) : IRequestHandler<AddPlayerRequest, AddPlayerResponse>
{
    private readonly IGameRepository Db = db;

    public async Task<AddPlayerResponse> Handle(AddPlayerRequest request, CancellationToken cancellationToken)
    {
        var player = new PlayerType(Name: request.PlayerName);

        var session = await Db.AddPlayer(request.InvitationCode, player);

        return new AddPlayerResponse(session);
    }
}
