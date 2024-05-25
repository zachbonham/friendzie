using MediatR;

namespace FriendZie.Api.Game.AddPlayer
{
    public record AddPlayerRequest(string InvitationCode, string PlayerName) : IRequest<AddPlayerResponse> { }
}
