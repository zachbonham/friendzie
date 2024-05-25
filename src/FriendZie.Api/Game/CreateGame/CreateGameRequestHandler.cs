using FluentResults;
using FriendZie.Domain.Player;
using FriendZie.Domain.Session;
using MediatR;

namespace FriendZie.Api.Game.CreateGame;


public class CreateGameRequestHandler(IGameRepository db) : IRequestHandler<CreateGameRequest, Result<SessionType>>
{
    private IGameRepository Db = db;

    public async Task<Result<SessionType>> Handle(CreateGameRequest request, CancellationToken cancellationToken)
    {

        var session = Session.Create(new PlayerType(Name: request.OwnerName));

        // if(session.IsFailed)

        session = await Db.SaveSession(session.Value);

        return session;
    }
}