using MediatR;

namespace FriendZie.Api.Game;

public record CreateNewGameComand(string OwnerName) : IRequest<CreateNewGameResult> { }
public record GetGameCommand(Guid Id) : IRequest<GetGameResult> { }

public record AddPlayerCommand(string InvitationCode, string PlayerName) : IRequest<AddPlayerResult> { }

