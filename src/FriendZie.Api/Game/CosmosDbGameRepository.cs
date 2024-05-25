using FluentResults;
using FriendZie.Domain.Player;
using FriendZie.Domain.Session;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace FriendZie.Api.Game;


public interface IGameRepository
{
    Task<Result<SessionType>> SaveSession(SessionType session);
    Task<Result<SessionType>> GetSession(Guid id);

    Task<Result<SessionType>> AddPlayer(string invitationCode, PlayerType player);

}

public class CosmosDbGameRepository(CosmosClient client) : IGameRepository
{
    private CosmosClient Client { get; } = client;
    private const string DatabaseName = "FriendZie";
    private const string ContainerName = "Sessions";

    private Container GetContainer()
    {
        Database database = Client.GetDatabase(DatabaseName);
        return database.GetContainer(ContainerName);
    }
    public async Task<Result<SessionType>> AddPlayer(string invitationCode, PlayerType player)
    {

        var session = await FindByInvitationCode(invitationCode);

        if (session.IsFailed) return session;

        if (session.Value.Players.Count() >= session.Value.MaximumPlayers) return Result.Fail("Maximum players already reached");

        var sessionId = session.Value.Id.ToString();

        List<PatchOperation> operations = new()
        {
            PatchOperation.Add("/players/-", player)
        };

        var result = await GetContainer()
            .PatchItemAsync<SessionType>(sessionId, new PartitionKey(sessionId), operations);

        return Result.Ok(result.Resource);
    }

    public async Task<Result<SessionType>> GetSession(Guid id)
    {
        Database database = Client.GetDatabase(DatabaseName);
        Container container = database.GetContainer(ContainerName);

        var session = await container.ReadItemAsync<SessionType>(id: id.ToString(), partitionKey: new PartitionKey(id.ToString()));

        if (session.Resource == null) return Result.Fail($"Session not found: {id}");

        return Result.Ok(session.Resource);
    }

    public async Task<Result<SessionType>> SaveSession(SessionType session)
    {
        Database database = Client.GetDatabase(DatabaseName);
        Container container = database.GetContainer(ContainerName);

        await container.UpsertItemAsync(
                item: session,
                partitionKey: new PartitionKey(session.Id.ToString())
            );

        return Result.Ok(session);
    }

    private async Task<Result<SessionType>> FindByInvitationCode(string invitationCode)
    {
        Database database = Client.GetDatabase(DatabaseName);
        Container container = database.GetContainer(ContainerName);

        using FeedIterator<SessionType> results = container.GetItemLinqQueryable<SessionType>()
            .Where(p => p.InvitationCode == invitationCode)
            .ToFeedIterator();

        if (false == results.HasMoreResults) return Result.Fail($"Invitation code not found: {invitationCode}");

        FeedResponse<SessionType> response = await results.ReadNextAsync();

        return Result.Ok(response.First());
    }
}
