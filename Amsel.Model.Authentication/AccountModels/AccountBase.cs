using System;
using System.ComponentModel.DataAnnotations;

namespace Amsel.Model.Authentication.AccountModels
{
    public class AccountBase
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual string TwitchId { get; protected set; }
    }
}