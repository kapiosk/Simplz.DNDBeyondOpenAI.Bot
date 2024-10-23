namespace Simplz.DNDBeyondOpenAI.Bot.Data;

public sealed record Game
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<GameCharacter> GameCharacters { get; init; } = [];
}

