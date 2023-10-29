namespace CleannetCodeBot.Twitch;

public class TwitchUser
{
    public static readonly string CollectionName = "users";
    
    public required string TwitchUserId { get; init; }
    
    public required string Username { get; init; }
}