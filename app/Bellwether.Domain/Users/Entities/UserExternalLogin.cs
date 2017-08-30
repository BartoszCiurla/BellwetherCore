using Core.Domain.Ddd;
using System;

namespace Bellwether.Domain.Users.Entities
{
    public class UserExternalLogin : Entity
    {
        public Guid UserId { get; private set; }
        public string Provider { get; private set; }
        public string ExternalId { get; private set; }

        protected UserExternalLogin() : base(Guid.Empty)
        {
        }

        protected UserExternalLogin(Guid id, Guid userId, string provider, string externalId) : base(id)
        {
            UserId = userId;
            Provider = provider;
            ExternalId = externalId;
        }

        public static UserExternalLogin Create(Guid id, Guid userId, string provider, string externalId)
        {
            return new UserExternalLogin(id, userId, provider, externalId);
        }

        public void UpdateId(string externalId)
        {
            ExternalId = externalId;
        }
    }
}
