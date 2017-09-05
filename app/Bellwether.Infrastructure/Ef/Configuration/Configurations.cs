using Bellwether.Domain.Games.Entities;
using Bellwether.Domain.Jokes.Entities;
using Bellwether.Domain.Languages.Entities;
using Bellwether.Domain.Users.Entities;
using Bellwether.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bellwether.Infrastructure.Ef.Configurations
{
  #region Languages
  public class LanguageConfiguration : EntityMappingConfiguration<Language>
  {
    public override void Map(EntityTypeBuilder<Language> builder)
    {
      builder.ToTable(nameof(Language), SchemaName.Bellwether);
    }
  }
  #endregion

  #region Games
  public class GameConfiguration : EntityMappingConfiguration<Game>
  {
    public override void Map(EntityTypeBuilder<Game> builder)
    {
      builder.ToTable(nameof(Game), SchemaName.Bellwether);
    }
  }
  #endregion

  #region Jokes
  public class JokeConfiguration : EntityMappingConfiguration<Joke>
  {
    public override void Map(EntityTypeBuilder<Joke> builder)
    {
      builder.ToTable(nameof(Joke), SchemaName.Bellwether);
    }
  }
  #endregion

  #region Users
  public class UserConfiguratinon : EntityMappingConfiguration<BellwetherUser>
  {
    public override void Map(EntityTypeBuilder<BellwetherUser> builder)
    {
      builder.HasMany(x => x.UserRoles);
      builder.ToTable(nameof(BellwetherUser), SchemaName.Bellwether);
    }
  }

  public class RoleConfiguration : EntityMappingConfiguration<Role>
  {
    public override void Map(EntityTypeBuilder<Role> builder)
    {
      builder.ToTable(nameof(Role), SchemaName.Bellwether);
    }
  }

  public class UserRoleConfiguration : EntityMappingConfiguration<UserRole>
  {
    public override void Map(EntityTypeBuilder<UserRole> builder)
    {
      builder.HasKey(ur => new { ur.RoleId, ur.UserId });
      builder.HasOne(ur => ur.User)
        .WithMany(u => u.UserRoles)
        .HasForeignKey(ur => ur.UserId);
      builder.HasOne(ur => ur.Role)
        .WithMany(r => r.UserRoles)
        .HasForeignKey(ur => ur.RoleId);

      builder.ToTable(nameof(UserRole), SchemaName.Bellwether);
    }
  }
  #endregion


}
