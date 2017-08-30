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
    public class UserConfiguratinon : EntityMappingConfiguration<User>
    {
        public override void Map(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User), SchemaName.Bellwether);
        }
    }
    #endregion
}
