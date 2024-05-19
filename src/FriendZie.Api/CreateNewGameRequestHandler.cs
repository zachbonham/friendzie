using MediatR;
using FriendZie.Domain;

namespace FriendZie.Api
{
    public class CreateNewGameRequestHandler : IRequestHandler<CreateNewGameComand, CreateNewGameResult>
    {
        public async Task<CreateNewGameResult> Handle(CreateNewGameComand request, CancellationToken cancellationToken)
        {
            await Task.Delay(100);

            var sessions = Session.Create(new PlayerType(Name: request.OwnerName));

            return new CreateNewGameResult(InvitationCode: sessions.Value.InvitationCode, InviteUrl: sessions.Value.GameLink.ToString() );
        }
    }
}
