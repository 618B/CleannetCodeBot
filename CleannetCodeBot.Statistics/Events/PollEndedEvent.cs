using CleannetCodeBot.Twitch.Polls;

namespace CleannetCodeBot.Twitch.Events;

public record PollEndedEvent(List<Vote> Votes);