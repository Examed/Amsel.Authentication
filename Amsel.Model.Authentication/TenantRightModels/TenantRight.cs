using Amsel.Enums.Authentication.Enums;
using Amsel.Framework.Database.SQL.Interfaces;
using Amsel.Model.Authentication.Account;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amsel.Endpoint.Authentication.Persistence
{
    [Owned, ComplexType]
    public class TenantRight
    {
        public ETenantRights Rights { get; set; }
        public Account Tenant { get; set; }
        
        #region PUBLIC METHODES
        public void Add(ETenantRights rights) => Rights |= rights;

        public bool HasRights(ETenantRights rights) => !IsBanned() && Rights.HasFlag(rights);

        public bool IsBanned() => Rights.HasFlag(ETenantRights.BANNED);

        public void Remove(ETenantRights rights) => Rights &= ~rights;
        #endregion

        #region  CONSTRUCTORS

        public TenantRight(ETenantRights rights, [NotNull] Account tenant)
        {
            if (!Enum.IsDefined(typeof(ETenantRights), rights))
                throw new InvalidEnumArgumentException(nameof(rights), (int)rights, typeof(ETenantRights));

            Rights = rights;
           
            Tenant = tenant ?? throw new ArgumentNullException(nameof(tenant));
        }

        protected TenantRight() { }
        #endregion
    }
}