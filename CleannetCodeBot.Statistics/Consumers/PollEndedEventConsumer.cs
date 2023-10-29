using CleannetCodeBot.Twitch.Events;
using CleannetCodeBot.Twitch.Polls;
using MassTransit;
using MongoDB.Driver;

namespace CleannetCodeBot.Statistics.Consumers;

public class PollEndedEventConsumer : IConsumer<PollEndedEvent>
{
    private readonly ILogger<PollEndedEventConsumer> _logger;
    private readonly IMongoCollection<Vote> _votesCollection;

    public PollEndedEventConsumer(ILogger<PollEndedEventConsumer> logger, IMongoDatabase mongoDatabase)
    {
        _logger = logger;

        _votesCollection = mongoDatabase.GetCollection<Vote>("votes");
    }
    
    public async Task Consume(ConsumeContext<PollEndedEvent> context)
    {
        _logger.LogInformation("Received PollEndedEvent");

        await _votesCollection.InsertManyAsync(context.Message.Votes);
        
        _logger.LogInformation("Processed PollEndedEvent");
    }
}