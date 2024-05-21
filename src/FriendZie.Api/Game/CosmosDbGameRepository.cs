using FriendZie.Domain;
using Microsoft.Azure.Cosmos;

namespace FriendZie.Api.Game;


public interface IGameRepository
{
    Task SaveSession(SessionType session);
    Task<SessionType> GetSession(Guid id);

    Task<SessionType> AddPlayer(PlayerType player);
}

public class CosmosDbGameRepository(CosmosClient client) : IGameRepository
{
    private CosmosClient Client { get; } = client;
    private const string Database = "Friendzie";
    private const string Container = "Session";

    public Task<SessionType> AddPlayer(PlayerType player)
    {
        throw new NotImplementedException();
    }

    public async Task<SessionType> GetSession(Guid id)
    {
        Database database = Client.GetDatabase(Database);
        Container container = database.GetContainer(Container);

        var session = await container.ReadItemAsync<SessionType>(id: id.ToString(), partitionKey: new PartitionKey(id.ToString()));

        return session;
                
    }

    public async Task SaveSession(SessionType session)
    {
        Database database = Client.GetDatabase(Database);
        Container container = database.GetContainer(Container);

        await container.UpsertItemAsync(
                item: session,
                partitionKey: new PartitionKey(session.Id.ToString())
            );

    }
}
