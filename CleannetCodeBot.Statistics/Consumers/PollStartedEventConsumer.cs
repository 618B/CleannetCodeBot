using CleannetCodeBot.Twitch.Events;
using MassTransit;
using MongoDB.Driver;

namespace CleannetCodeBot.Statistics.Consumers;

public class PollStartedEventConsumer : IConsumer<PollStartedEvent>
{
    private readonly ILogger<PollEndedEventConsumer> _logger;
    private readonly IMongoCollection<PollStartedEvent> _pollStartedCollection;

    public PollStartedEventConsumer(ILogger<PollEndedEventConsumer> logger, IMongoDatabase mongoDatabase)
    {
        _logger = logger;

        _pollStartedCollection = mongoDatabase.GetCollection<PollStartedEvent>("pollStarted");
    }
    
    public async Task Consume(ConsumeContext<PollStartedEvent> context)
    {
        _logger.LogInformation("Received PollStartedEvent");
        await _pollStartedCollection.InsertOneAsync(context.Message);
        _logger.LogInformation("Processed PollStartedEvent");
    }
}