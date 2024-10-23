using Microsoft.EntityFrameworkCore;

namespace Simplz.DNDBeyondOpenAI.Bot.Data;

public class GameContext : DbContext
{
    // dotnet ef migrations add InitialCreate
    // dotnet ef database update
    // dotnet aspnet-codegenerator blazor CRUD -dbProvider sqlite -dc GameContext -m GameCharacter

    public string DbPath { get; }

    public GameContext()
    {
        // var folder = Environment.SpecialFolder.LocalApplicationData;
        // var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join("Temp", "Game.db"); //Path.Join(path, "Game.db");
    }

    public DbSet<Character> Characters { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<GameCharacter> GameCharacters { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameCharacter>(entity =>
        {
            entity.HasKey(gc => new { gc.GameId, gc.CharacterId });
            entity.HasOne(gc => gc.Game)
                  .WithMany(g => g.GameCharacters)
                  .HasForeignKey(gc => gc.GameId);
            entity.HasOne(gc => gc.Character)
                  .WithMany(c => c.GameCharacters)
                  .HasForeignKey(gc => gc.CharacterId);
        });

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public sealed record Character
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<GameCharacter> GameCharacters { get; init; } = [];
}

public sealed record Game
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<GameCharacter> GameCharacters { get; init; } = [];
}

public sealed record GameCharacter
{
    public int GameId { get; init; }
    public Game Game { get; set; } = default!;
    public int CharacterId { get; init; }
    public Character Character { get; set; } = default!;
}

