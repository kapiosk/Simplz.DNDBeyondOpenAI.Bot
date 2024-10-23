using Microsoft.EntityFrameworkCore;

namespace Simplz.DNDBeyondOpenAI.Bot.Data;

public class GameContext(DbContextOptions<GameContext> options) : DbContext(options)
{
    // dotnet ef migrations add InitialCreate
    // dotnet ef database update
    // dotnet aspnet-codegenerator blazor CRUD -dbProvider sqlite -dc GameContext -m GameCharacter

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
}
