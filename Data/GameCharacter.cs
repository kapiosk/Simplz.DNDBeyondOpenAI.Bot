namespace Simplz.DNDBeyondOpenAI.Bot.Data;

public sealed record GameCharacter
{
    public int GameId { get; init; }
    public Game Game { get; set; } = default!;
    public int CharacterId { get; init; }
    public Character Character { get; set; } = default!;
}

