using MediatR;

namespace FriendZie.Api.Game;

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

        app.MapGet("games", GetGame)
            .WithName("GetGame")
            .Produces<GetGameResponse>()
             .WithOpenApi(operation => new(operation)
             {
                 Summary = "Finds an existing Friendzie game by its unique id.",
                 Description = "This is a description"
             });

        app.MapPost("players", AddPlayer)
            .WithName("AddPlayer")
            .Produces<AddPlayerResponse>()
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Adds a player to an existing session using invitationCode."
            });
    }


    public static async Task<IResult> CreateNewGame(NewGameRequest request, ISender sender)
    {
        var result = await sender.Send(new CreateNewGameComand(request.OwnerName));

        var response = new CreateNewGameResponse(result.Session);
        return Results.Ok(response);

    }

    public static async Task<IResult> GetGame(Guid id, ISender sender)
    {
        var result = await sender.Send(new GetGameCommand(id));
        var response = new GetGameResponse(result.Session);
        return Results.Ok(response);
    }

    public static async Task<IResult> AddPlayer(AddPlayerRequest request, ISender sender)
    {
        var result = await sender.Send(new AddPlayerCommand(InvitationCode: request.InvitationCode, PlayerName: request.Name));
        var response = new AddPlayerResponse(result.Session);

        return Results.Ok(response);
    }

}
