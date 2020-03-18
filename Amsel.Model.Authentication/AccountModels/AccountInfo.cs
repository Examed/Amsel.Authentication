using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Amsel.Model.Authentication.Account
{
    public class AccountInfo : AccountBase
    {
        [DefaultValue(false)]
        public virtual bool Admin { get; protected set; }

        [DefaultValue(false)]
        public virtual bool Banned { get; protected set; }

        [EmailAddress]
        public virtual string Email { get;  set; }

        [DefaultValue(false)]
        public virtual bool Premium { get; protected set; }

        #region PUBLIC METHODES
        /// <summary>
        /// Ban the Account and restricted it from authenticate
        /// </summary>
        public virtual void BanAccount() => Banned = true;

        /// <summary>
        /// UnBan the Account and allow it to authenticate
        /// </summary>
        public virtual void UnBanAccount() => Banned = false;
        #endregion
    }
}