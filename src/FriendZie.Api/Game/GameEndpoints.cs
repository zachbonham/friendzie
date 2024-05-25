using FriendZie.Api.Game.AddPlayer;
using FriendZie.Api.Game.CreateGame;
using FriendZie.Api.Game.GetGame;
using MediatR;

namespace FriendZie.Api.Game;

public static class GameEndpoints
{

    public static void MapGameEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("games", CreateNewGame)

            .WithName("CreateNewGame")
            .Produces<CreateGameResponse>()
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


    public static async Task<IResult> CreateNewGame(CreateGameRequest request, ISender sender)
    {
        var response = await sender.Send(request);

        return Results.Ok(new CreateGameResponse(response.Value));

    }

    public static async Task<IResult> GetGame(Guid id, ISender sender)
    {
        var response = await sender.Send(new GetGameRequest(id));
        if (response.IsFailed) return Results.NotFound();

        return Results.Ok(new GetGameResponse(response.Value));
    }

    public static async Task<IResult> AddPlayer(AddPlayerRequest request, ISender sender)
    {
        var response = await sender.Send(request);

        if (response.IsFailed) return Results.NotFound();

        return Results.Ok(new AddPlayerResponse(response.Value));
    }

}
