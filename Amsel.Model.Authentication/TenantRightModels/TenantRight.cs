using Amsel.Enums.Authentication.Enums;
using Amsel.Model.Authentication.AccountModels;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amsel.Model.Authentication.TenantRightModels
{
    [Owned]
    [ComplexType]
    public class TenantRight
    {
        protected TenantRight() { }


        public TenantRight(ETenantRights rights, [NotNull] Account tenant)
        {
            if(!Enum.IsDefined(typeof(ETenantRights), rights))
                throw new InvalidEnumArgumentException(nameof(rights), (int)rights, typeof(ETenantRights));

            Rights = rights;

            Tenant = tenant ?? throw new ArgumentNullException(nameof(tenant));
        }

        public void Add(ETenantRights rights) => Rights |= rights;

        public bool HasRights(ETenantRights rights) => !IsBanned() && Rights.HasFlag(rights);

        public bool IsBanned() => Rights.HasFlag(ETenantRights.BANNED);

        public void Remove(ETenantRights rights) => Rights &= ~rights;

        public ETenantRights Rights { get; set; }

        public Account Tenant { get; set; }
    }
}