using Core.Domain.Ddd;
using System;

namespace Bellwether.Domain.Users.Entities
{
    public class User : AggregateRoot
    {
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string UserName { get; private set; }

        protected User() : base(Guid.Empty)
        {
        }

        protected User(Guid id,
                       byte[] passwordHash,
                       byte[] passwordSalt,
                       string firstName,
                       string lastName,
                       string userName) : base(id)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
        }
        #region Factory methods
        public static User Create(Guid id, byte[] passwordHash, byte[] passwordSalt, string firstName, string lastName, string userName)
        {
            return new User(id, passwordHash, passwordSalt, firstName, lastName, userName);
        }

        #endregion
    }
}
