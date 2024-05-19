using MediatR;

namespace FriendZie.Api;


public record NewGameRequest(string OwnerName) { }

public record CreateNewGameComand(string OwnerName) : IRequest<CreateNewGameResult> { }
public record CreateNewGameResult(string InvitationCode, string InviteUrl)  { }


public record CreateNewGameResponse(string InviteCode, string InviteUrl) { };



public static class GameEndpoints
{

    public static void MapGameEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("games", CreateNewGame)

            .WithName("CreateNewGame")             
            .Produces<CreateNewGameResponse>()
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Creates a new friendzie game",
                Description = "This is a description"
            });


    }

    
    public static async Task<IResult> CreateNewGame(NewGameRequest request, ISender sender)
    {
        var result = await sender.Send(new CreateNewGameComand(request.OwnerName));
        var response = new CreateNewGameResponse(result.InvitationCode, result.InviteUrl);
        return Results.Ok(response);

    }

}
