using CleannetCodeBot.Twitch.Events;
using MassTransit;
using MongoDB.Driver;

namespace CleannetCodeBot.Statistics.Consumers;

public class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
{
    private readonly ILogger<UserCreatedEventConsumer> _logger;
    private readonly IMongoCollection<UserCreatedEvent> _usersCollection;

    public UserCreatedEventConsumer(ILogger<UserCreatedEventConsumer> logger, IMongoDatabase mongoDatabase)
    {
        _logger = logger;

        _usersCollection = mongoDatabase.GetCollection<UserCreatedEvent>("users");
    }
    
    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        _logger.LogInformation("Received UserCreatedEvent");

        await _usersCollection.InsertOneAsync(context.Message);
        
        _logger.LogInformation("Processed UserCreatedEvent");
    }
}