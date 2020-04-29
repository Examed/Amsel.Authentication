using Amsel.Enums.Authentication.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Amsel.Model.Authentication.AccountModels {
    public partial class Account
    {
        [Owned]
        public class TenantRight
        {
            protected TenantRight() { }

            public TenantRight(ETenantRights rights, [NotNull] Account tenant) {
                if(!Enum.IsDefined(typeof(ETenantRights), rights)) {
                    throw new InvalidEnumArgumentException(nameof(rights), (int)rights, typeof(ETenantRights));
                }

                Rights = rights;
                Tenant = tenant ?? throw new ArgumentNullException(nameof(tenant));
            }

            public ETenantRights Rights { get; set; }
            [ForeignKey(nameof(TenantId))]
            public virtual Account Tenant { get; set; }
            public Guid? TenantId { get; set; }

            public void Add(ETenantRights rights) => Rights |= rights;

            public bool HasRights(ETenantRights rights) => !IsBanned() && Rights.HasFlag(rights);

            public bool IsBanned() => Rights.HasFlag(ETenantRights.BANNED);

            public void Remove(ETenantRights rights) => Rights &= ~rights;
        }
    }
}