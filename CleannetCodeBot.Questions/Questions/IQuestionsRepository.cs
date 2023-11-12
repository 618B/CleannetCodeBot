namespace CleannetCodeBot.Questions.Questions;

public interface IQuestionsRepository
{
    public Task AddQuestion(PollQuestion question);

    public Task AddQuestions(IEnumerable<PollQuestion> questions);
}