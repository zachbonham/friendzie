using System.Reflection.Metadata.Ecma335;
using FriendZie.Domain;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace FriendZie.Api.Game;


public interface IGameRepository
{
    Task SaveSession(SessionType session);
    Task<SessionType> GetSession(Guid id);

    Task<SessionType> AddPlayer(string invitationCode, PlayerType player);

}

public class CosmosDbGameRepository(CosmosClient client) : IGameRepository
{
    private CosmosClient Client { get; } = client;
    private const string DatabaseName = "Friendzie";
    private const string ContainerName = "Session";

    private Container GetContainer()
    {
        Database database = Client.GetDatabase(DatabaseName);
        return database.GetContainer(ContainerName);
    }
    public async Task<SessionType> AddPlayer(string invitationCode, PlayerType player)
    {
        
        var session = await FindByInvitationCode(invitationCode);

        // TODO: handle not found invitation code

        var sessionId = session.Id.ToString();

        List<PatchOperation> operations = new()
        {
            PatchOperation.Add("/players/-", player)
        };

        var result = await GetContainer()
            .PatchItemAsync<SessionType>(sessionId, new PartitionKey(sessionId), operations);

        return result;

    }

    public async Task<SessionType> GetSession(Guid id)
    {
        Database database = Client.GetDatabase(DatabaseName);
        Container container = database.GetContainer(ContainerName);

        var session = await container.ReadItemAsync<SessionType>(id: id.ToString(), partitionKey: new PartitionKey(id.ToString()));

        return session;
                
    }

    public async Task SaveSession(SessionType session)
    {
        Database database = Client.GetDatabase(DatabaseName);
        Container container = database.GetContainer(ContainerName);

        await container.UpsertItemAsync(
                item: session,
                partitionKey: new PartitionKey(session.Id.ToString())
            );

        var newPlayer = new PlayerType("josh");

        List<PatchOperation> operations = new()
    {
      
        PatchOperation.Add("/players/-", newPlayer)
    };

        
        await container.PatchItemAsync<SessionType>(session.Id.ToString(), new PartitionKey(session.Id.ToString()), operations );

    }

    private async Task<SessionType?> FindByInvitationCode(string invitationCode)
    {
        Database database = Client.GetDatabase(DatabaseName);
        Container container = database.GetContainer(ContainerName);

        using FeedIterator<SessionType> results = container.GetItemLinqQueryable<SessionType>()
            .Where( p => p.InvitationCode == invitationCode)
            .ToFeedIterator();        

        if (false == results.HasMoreResults) return null;
                
        FeedResponse<SessionType> response = await results.ReadNextAsync();

        return response.First();

    }
}
