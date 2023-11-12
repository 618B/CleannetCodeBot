namespace CleannetCodeBot.Questions.Questions;

public record QuestionAnswer(string Key, string Content);

public class PollQuestion
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    
    public string Content { get; init; }
    
    public QuestionAnswer[] Answers { get; init; }
    
    public string CorrectAnswerKey { get; init; }
}