using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Amsel.Model.Authentication.AccountModels
{
    public class AccountInfo : AccountBase
    {
        /// <summary>
        /// Ban the Account and restricted it from authenticate
        /// </summary>
        public virtual void BanAccount() => Banned = true;

        /// <summary>
        /// UnBan the Account and allow it to authenticate
        /// </summary>
        public virtual void UnBanAccount() => Banned = false;

        [DefaultValue(false)]
        public virtual bool Admin { get; protected set; }

        [DefaultValue(false)]
        public virtual bool Banned { get; protected set; }

        [EmailAddress]
        public virtual string Email { get; set; }

        [DefaultValue(false)]
        public virtual bool Premium { get; protected set; }
    }
}