using Core.Domain.Ddd;
using System;
using System.Collections.Generic;

namespace Bellwether.Domain.Users.Entities
{
  public class BellwetherUser : AggregateRoot
  {
    public string Name { get; private set; }
    public string Surname { get; private set; }

    public string Email { get; private set; }

    public string Password { get; private set; }
    public DateTime Created { get; private set; }
    public DateTime LastLoginDate { get; private set; }
    public string Salt { get; private set; }
    public UserType UserType { get; set; }

    protected BellwetherUser() : base(Guid.Empty)
    {
    }

    protected BellwetherUser(Guid id,
                    string name,
                    string surname,
                    string email,
                    string password,
                    string salt,
                    UserType userType) : base(id)
    {
      Name = name;
      Surname = surname;
      Email = email;
      Password = password;
      Salt = salt;
      UserType = userType;
    }
    public void UpdateLastLoginDate() => LastLoginDate = DateTime.Now;

    #region Factory methods
    public static BellwetherUser Create(Guid id,
                    string name,
                    string surname,
                    string email,
                    string password,
                    string salt,
                    UserType userType)
    {
      return new BellwetherUser(id, name, surname, email, password, salt, userType);
    }

    #endregion
  }
}
