namespace CleannetCodeBot.Twitch;

public record PollStartRequest(string UserId, string Username, string BroadcasterUserId, string AccessToken);