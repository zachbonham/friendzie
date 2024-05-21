using MediatR;
using FriendZie.Domain;

namespace FriendZie.Api.Game;

public class GetGameRequestHandler(IGameRepository db) : IRequestHandler<GetGameCommand, GetGameResult>
{
    private IGameRepository _db { get; } = db;

    public async Task<GetGameResult> Handle(GetGameCommand request, CancellationToken cancellationToken)
    {

        // if(session.IsFailed)

        var session = await _db.GetSession(request.Id);


        
        return new GetGameResult(session);
    }
}
