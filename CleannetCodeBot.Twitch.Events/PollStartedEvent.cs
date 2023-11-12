namespace CleannetCodeBot.Twitch.Events;

public record PollStartedEvent(string UserId, DateTime StartTimeUtc);