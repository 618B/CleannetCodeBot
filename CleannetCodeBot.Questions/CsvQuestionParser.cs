using CleannetCodeBot.Questions.Questions;

namespace CleannetCodeBot.Questions;

public static class CsvQuestionParser
{
    private const int QuestionPartsCount = 5;
    private const int QuestionContentIndex = 0;
    private const int QuestionAnswerKeyIndex = 5;
    private static readonly Range QuestionAnswersRange = new Range(1, 4);

    public static PollQuestion ParseOne(string question)
    {
        var questionParts = question.Split(',');
        
        if (questionParts.Length != QuestionPartsCount)
            throw new ArgumentException("Wrong question parts count");

        if (!int.TryParse(questionParts.Last(), out var rightKey))
            throw new ArgumentException("Wrong question answer key");
        

        var answers = questionParts[QuestionAnswersRange];

        return new PollQuestion()
        {
            Content = questionParts[QuestionContentIndex],
            Answers = answers.Select((x, i) => new QuestionAnswer(i.ToString(), x)).ToArray(),
            CorrectAnswerKey = questionParts[QuestionAnswerKeyIndex]
        };
    }
}