using MediatR;
using FriendZie.Domain;

namespace FriendZie.Api.Game;

public class CreateNewGameRequestHandler(IGameRepository db) : IRequestHandler<CreateNewGameComand, CreateNewGameResult>
{
    private IGameRepository _db { get; } = db;

    public async Task<CreateNewGameResult> Handle(CreateNewGameComand request, CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);

        var session = Session.Create(new PlayerType(Name: request.OwnerName));

        // if(session.IsFailed)

        await _db.SaveSession(session.Value);


        
        return new CreateNewGameResult(session.Value);
    }
}
