using Amsel.Enums.Authentication.Enums;
using Amsel.Framework.Base.Interfaces;
using Amsel.Framework.Utilities.Extensions.Guids;
using Amsel.Framework.Utilities.Extensions.Types;
using Amsel.Model.Authentication.Models;
using Amsel.Model.Tenant.TenantModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Amsel.Model.Authentication.AccountModels {
    [ComplexType]
    public partial class Account : AccountInfo, IGuidEntity, ISynchronize<TenantEntity> {
        protected Account() { }

        public Account(string name, string twitchId = null) {
            Name = name;
            TwitchId = twitchId;
        }

        [DefaultValue(null)]
        public string ClientSecret { get; protected set; }
        [DefaultValue(null)]
        public bool IsTenant { get; set; }
        [NotNull]
        public virtual ICollection<TenantRight> TenantRights { get; protected set; } = new List<TenantRight>();
        public virtual TwitchToken TwitchToken { get; protected set; }

        public void AddTenantRights([NotNull] Account tenant, ETenantRights rights) {
            if (tenant == null) {
                throw new ArgumentNullException(nameof(tenant));
            }

            TenantRight current = TenantRights.FirstOrDefault(x => x.TenantId == tenant.Id);
            if (current == null) {
                TenantRights.Add(new TenantRight(rights, tenant));
            }
            else {
                current.Add(rights);
            }
        }

        public void GenerateClientSecret() => ClientSecret = CryptoGuid.GetExtendedCryptoString();

        public List<Claim> GetClaims() {
            List<Claim> claimsIdentity = new List<Claim> {
                (Admin
                ? (new Claim(ClaimTypes.Role, AuthRoles.ERoles.ADMIN.ToString()))
                : (new Claim(ClaimTypes.Role, AuthRoles.ERoles.VIEWER.ToString()))),
                new Claim(ClaimTypes.Name, Name)
            };

            if (Admin || Premium) {
                claimsIdentity.Add(new Claim(nameof(EClaimTypes.SUBSCRIPTION_LEVEL), nameof(AuthRoles.ESubscriptionPolicy.PREMIUM)));
            }

            string banned = GetRightsAccountString(TenantRights.Where(x => x.IsBanned()));
            if (!string.IsNullOrEmpty(banned)) {
                claimsIdentity.Add(new Claim(EClaimTypes.TENANT_BANNED.ToString(), banned));
            }

            string editor = GetRightsAccountString(TenantRights.Where(x => x.HasRights(ETenantRights.EDITOR)));
            if (!string.IsNullOrEmpty(editor)) {
                claimsIdentity.Add(new Claim(EClaimTypes.TENANT_EDITOR.ToString(), editor));
            }

            string moderator = GetRightsAccountString(TenantRights.Where(x
                => !x.HasRights(ETenantRights.EDITOR) && x.HasRights(ETenantRights.MODERATOR)));
            if (!string.IsNullOrEmpty(moderator)) {
                claimsIdentity.Add(new Claim(EClaimTypes.TENANT_MODERATOR.ToString(), moderator));
            }

            return claimsIdentity;
        }

        public void RemoveRights([NotNull] Account tenant, ETenantRights rights) {
            if (tenant == null) {
                throw new ArgumentNullException(nameof(tenant));
            }

            if (!Enum.IsDefined(typeof(ETenantRights), rights)) {
                throw new InvalidEnumArgumentException(nameof(rights), (int)rights, typeof(ETenantRights));
            }

            TenantRight current = TenantRights.FirstOrDefault(x => (x != null) && (x.TenantId == tenant.Id));
            if (current == null) {
                return;
            }

            current.Remove(rights);
            if (current.Rights.HasNoFlags<ETenantRights>()) {
                TenantRights.Remove(current);
            }
        }

        public void SetTwitchToken(TwitchToken twitchToken) {
            if ((twitchToken == null) || twitchToken.IsExpired()) {
                return;
            }

            if (TwitchToken == null) {
                TwitchToken = twitchToken;
            }
            else if (TwitchToken.IsExpired() || twitchToken.Scope.HasFlag(TwitchToken.Scope)) {
                TwitchToken.UpdateToken(twitchToken);
            }
        }

        private string GetRightsAccountString([NotNull] IEnumerable<TenantRight> rights) {
            if ((rights == null) || !rights.Any()) {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();
            foreach (TenantRight item in from TenantRight item in rights
                where (item != null) && (item.Tenant != null)
                select item) {
                builder.Append($"{item.Tenant.Name};");
            }

            return builder.ToString().TrimEnd(';');
        }
    }

    public class AccountBase {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public virtual string TwitchId { get; protected set; }
    }

    public class AccountInfo : AccountBase {
        [DefaultValue(false)]
        public virtual bool Admin { get; protected set; }
        [DefaultValue(false)]
        public virtual bool Banned { get; protected set; }
        [EmailAddress]
        public virtual string Email { get; set; }
        [DefaultValue(false)]
        public virtual bool Premium { get; protected set; }

        /// <summary>
        /// Ban the Account and restricted it from authenticate
        /// </summary>
        public virtual void BanAccount() => Banned = true;

        /// <summary>
        /// UnBan the Account and allow it to authenticate
        /// </summary>
        public virtual void UnBanAccount() => Banned = false;
    }
}