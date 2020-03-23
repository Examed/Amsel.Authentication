using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Amsel.Model.Authentication.AuthTokenModels
{
    public class AuthToken
    {
        protected AuthToken() { }
        public AuthToken(string token) => Token = token;

        [JsonConstructor]
        public AuthToken(string token, DateTime expireTime)
        {
            Token = token;
            ExpireTime = expireTime;
        }

        public bool Expired()
        {
            if(string.IsNullOrEmpty(Token))
                return true;

            if(ExpireTime == default)
                return false;

            return ExpireTime < DateTime.UtcNow;
        }

        public DateTime ExpireTime { get; set; }

        [Required]
        public string Token { get; set; }
    }
}