using Amsel.Enums.Authentication.Enums;
using Amsel.Enums.Authentication.EnumStrings;
using Amsel.Framework.Base.Interfaces;
using Amsel.Framework.Utilities.Extensions.Guids;
using Amsel.Framework.Utilities.Extensions.Types;
using Amsel.Model.Authentication.TenantRightModels;
using Amsel.Model.Authentication.TwitchTokenModels;
using Amsel.Model.Tenant.TenantModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Amsel.Model.Authentication.AccountModels
{
    [ComplexType]
    public class Account : AccountInfo, IGuidEntity, ISynchronize<TenantEntity>
    {
        protected Account() { }

        public Account(string tenantName, string twitchId = null)
        {
            TenantName = tenantName;
            TwitchId = twitchId;
        }

        string GetRightsAccountString([NotNull] IEnumerable<TenantRight> rights)
        {
            if(rights == null)
                throw new ArgumentNullException(nameof(rights));
            StringBuilder builder = new StringBuilder();
            foreach(TenantRight item in from TenantRight item in rights where item != null select item)
                builder.Append($"{item.Tenant.Id};");

            return builder.ToString().TrimEnd(';');
        }

        public void AddTenantRights([NotNull] Account tenant, ETenantRights rights)
        {
            if(tenant == null)
                throw new ArgumentNullException(nameof(tenant));
            TenantRight current = TenantRights.FirstOrDefault(x => x.Tenant.Id == tenant.Id);
            if(current == null)
                TenantRights.Add(new TenantRight(rights, tenant));
            else
                current.Add(rights);
        }

        public void GenerateClientSecret() => ClientSecret = CryptoGuid.GetExtendedCryptoString();

        public List<Claim> GetClaims()
        {
            List<Claim> claimsIdentity = new List<Claim>
            {
                (Admin ? (new Claim(ClaimTypes.Role, AuthRoles.ERoles.ADMIN.ToString())) : (new Claim(ClaimTypes.Role, AuthRoles.ERoles.VIEWER.ToString()))),
                new Claim(ClaimTypes.Sid, Id.ToString()),
                new Claim(ClaimTypes.Name, Name)
            };

            if(Admin || Premium)
                claimsIdentity.Add(new Claim(nameof(EClaimTypes.SUBSCRIPTION_LEVEL), nameof(AuthRoles.ESubscriptionPolicy.PREMIUM)));

            string banned = GetRightsAccountString(TenantRights.Where(x => x.IsBanned()));
            if(!string.IsNullOrEmpty(banned))
                claimsIdentity.Add(new Claim(EClaimTypes.TENANT_BANNED.ToEnumString(), banned));
            string editor = GetRightsAccountString(TenantRights.Where(x => x.HasRights(ETenantRights.EDITOR)));
            if(!string.IsNullOrEmpty(editor))
                claimsIdentity.Add(new Claim(EClaimTypes.TENANT_EDITOR.ToEnumString(), editor));
            string moderator = GetRightsAccountString(TenantRights.Where(x => !x.HasRights(ETenantRights.EDITOR) && x.HasRights(ETenantRights.MODERATOR)));
            if(!string.IsNullOrEmpty(moderator))
                claimsIdentity.Add(new Claim(EClaimTypes.TENANT_MODERATOR.ToEnumString(), moderator));

            return claimsIdentity;
        }

        public void RemoveRights([NotNull] Account tenant, ETenantRights rights)
        {
            if(tenant == null)
                throw new ArgumentNullException(nameof(tenant));
            if(!Enum.IsDefined(typeof(ETenantRights), rights))
                throw new InvalidEnumArgumentException(nameof(rights), (int)rights, typeof(ETenantRights));

            TenantRight current = TenantRights.FirstOrDefault(x => (x != null) && (x.Tenant.Id == tenant.Id));
            if(current == null)
                return;

            current.Remove(rights);
            if(current.Rights.HasNoFlags<ETenantRights>())
                TenantRights.Remove(current);
        }

        public void SetTwitchToken(TwitchToken twitchToken)
        {
            if((twitchToken == null) || twitchToken.IsExpired())
                return;

            if(TwitchToken == null)
                TwitchToken = twitchToken;
            else if(TwitchToken.IsExpired() || twitchToken.Scope.HasFlag(TwitchToken.Scope))
                TwitchToken.UpdateToken(twitchToken);
        }

        [DefaultValue(null)]
        public string ClientSecret { get; protected set; }

        [DefaultValue(null)]
        public string TenantName { get; set; }

        [NotNull]
        public ICollection<TenantRight> TenantRights { get; protected set; } = new List<TenantRight>();

        public TwitchToken TwitchToken { get; protected set; }
    }
}