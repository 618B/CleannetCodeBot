using CleannetCodeBot.Questions.Questions;
using MongoDB.Driver;

namespace CleannetCodeBot.Questions.Infrastructure;

public class QuestionsRepository : IQuestionsRepository
{
    private IMongoCollection<PollQuestion> _questionsCollection;

    public QuestionsRepository(IMongoDatabase mongoDatabase)
    {
        _questionsCollection = mongoDatabase.GetCollection<PollQuestion>("questions");
    }

    public async Task AddQuestion(PollQuestion question)
    {
        await _questionsCollection.InsertOneAsync(question);
    }

    public async Task AddQuestions(IEnumerable<PollQuestion> questions)
    {
        await _questionsCollection.InsertManyAsync(questions);
    }
}