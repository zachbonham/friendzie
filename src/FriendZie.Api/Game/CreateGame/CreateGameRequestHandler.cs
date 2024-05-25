using FriendZie.Domain.Player;
using FriendZie.Domain.Session;
using MediatR;

namespace FriendZie.Api.Game.CreateGame;


public class CreateGameRequestHandler(IGameRepository db) : IRequestHandler<CreateGameRequest, CreateGameResponse>
{
    private IGameRepository Db = db;

    public async Task<CreateGameResponse> Handle(CreateGameRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);

        var session = Session.Create(new PlayerType(Name: request.OwnerName));

        // if(session.IsFailed)

        await Db.SaveSession(session.Value);



        return new CreateGameResponse(session.Value);
    }
}