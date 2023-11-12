namespace CleannetCodeBot.Twitch.Events;

public record VoteData(string QuestionId, string UserId, string Answer, bool IsAnswerCorrect);
public record PollEndedEvent(List<VoteData> Votes);