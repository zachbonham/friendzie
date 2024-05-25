using FluentResults;
using FriendZie.Domain.Session;
using MediatR;

namespace FriendZie.Api.Game.GetGame;

public class GetGameRequestHandler(IGameRepository db) : IRequestHandler<GetGameRequest, Result<SessionType>>
{
    private readonly IGameRepository Db = db;

    public async Task<Result<SessionType>> Handle(GetGameRequest request, CancellationToken cancellationToken)
    {
        var session = await Db.GetSession(request.Id);

        return session;
    }
}
