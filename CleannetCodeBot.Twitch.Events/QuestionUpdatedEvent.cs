namespace CleannetCodeBot.Twitch.Events;

public record QuestionAnswerData(string Key, string Content);
public record QuestionUpdatedEvent(string Id, string Content, QuestionAnswerData[] Answers, string CorrectAnswerKey);