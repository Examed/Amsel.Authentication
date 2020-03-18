using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Amsel.Model.Authentication.Account
{
    public class AccountBase
    {
        [Key]
        public Guid Id { get;  set; }

        [Required]
        public string Name { get;  set; }

        [Required]
        public virtual string TwitchId { get; protected set; }
    }
}