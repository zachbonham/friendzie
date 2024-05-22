using MediatR;
using FriendZie.Domain;

namespace FriendZie.Api.Game;

public class AddPlayerRequstHandler(IGameRepository db) : IRequestHandler<AddPlayerCommand, AddPlayerResult>
{
    private IGameRepository _db { get; } = db;

    public async Task<AddPlayerResult> Handle(AddPlayerCommand request, CancellationToken cancellationToken)
    {
        var player = new PlayerType(Name: request.PlayerName);

        var session = await _db.AddPlayer(request.InvitationCode, player);

        return new AddPlayerResult(session);
    }
}
