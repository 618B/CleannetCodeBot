using CleannetCodeBot.Questions.Questions;
using CleannetCodeBot.Twitch.Events;
using MassTransit;

namespace CleannetCodeBot.Questions;

public class QuestionsService
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IBus _bus;
    private readonly ILogger<QuestionsService> _logger;

    public QuestionsService(IQuestionsRepository questionsRepository, IBus bus, ILogger<QuestionsService> logger)
    {
        _questionsRepository = questionsRepository;
        _bus = bus;
        _logger = logger;
    }
    
    public void AddQuestion(PollQuestion question)
    {
        _logger.LogInformation("Add new question");
        
        _questionsRepository.AddQuestion(question);

        _bus.Publish<QuestionUpdatedEvent>(question);
        
        _logger.LogInformation("Question add completed");
    }

    public void AddQuestions(IEnumerable<PollQuestion> pollQuestions)
    {
        _logger.LogInformation("Adding new questions");
        
        var questions = pollQuestions as PollQuestion[] ?? pollQuestions.ToArray();
        
        _questionsRepository.AddQuestions(questions);

        _bus.PublishBatch(questions.Select(x => new QuestionUpdatedEvent(
            Answers: x.Answers.Select(answer => new QuestionAnswerData(answer.Key, answer.Content)).ToArray(),
            Content: x.Content,
            Id: x.Id,
            CorrectAnswerKey: x.CorrectAnswerKey
        )));

        _logger.LogInformation("Questions add completed");
    }
}