using CleannetCodeBot.Twitch.Events;
using MassTransit;
using MongoDB.Driver;

namespace CleannetCodeBot.Statistics.Consumers;

public class PollEndedEventConsumer : IConsumer<PollEndedEvent>
{
    private readonly ILogger<PollEndedEventConsumer> _logger;
    private readonly IMongoCollection<VoteData> _votesCollection;

    public PollEndedEventConsumer(ILogger<PollEndedEventConsumer> logger, IMongoDatabase mongoDatabase)
    {
        _logger = logger;

        _votesCollection = mongoDatabase.GetCollection<VoteData>("votes");
    }
    
    public async Task Consume(ConsumeContext<PollEndedEvent> context)
    {
        _logger.LogInformation("Received PollEndedEvent");

        await _votesCollection.InsertManyAsync(context.Message.Votes);
        
        _logger.LogInformation("Processed PollEndedEvent");
    }
}