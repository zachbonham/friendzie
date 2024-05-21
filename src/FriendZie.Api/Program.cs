using FriendZie.Api.Game;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddSingleton<CosmosClient>(serviceProvider =>
{
    CosmosClientOptions options = new();
    options.SerializerOptions = new CosmosSerializationOptions()
    {
        IgnoreNullValues = true,
        PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
    };
    return new CosmosClient(connectionString: "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", 
        clientOptions: options);    
});

builder.Services.AddScoped<IGameRepository, CosmosDbGameRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.MapGameEndpoints();

app.Run();
