using MediatR;

namespace FriendZie.Api.Game.GetGame;

public class GetGameRequestHandler(IGameRepository db) : IRequestHandler<GetGameRequest, GetGameResponse>
{
    private IGameRepository Db = db;

    public async Task<GetGameResponse> Handle(GetGameRequest request, CancellationToken cancellationToken)
    {

        // if(session.IsFailed)

        var session = await Db.GetSession(request.Id);



        return new GetGameResponse(session);
    }
}
