using FluentResults;
using FriendZie.Domain.Player;
using FriendZie.Domain.Session;
using MediatR;

namespace FriendZie.Api.Game.AddPlayer;

public class AddPlayerRequstHandler(IGameRepository db) : IRequestHandler<AddPlayerRequest, Result<SessionType>>
{
    private readonly IGameRepository Db = db;

    public async Task<Result<SessionType>> Handle(AddPlayerRequest request, CancellationToken cancellationToken)
    {
        var player = new PlayerType(Name: request.PlayerName);

        var session = await Db.AddPlayer(request.InvitationCode, player);        

        return session;
    }
}
