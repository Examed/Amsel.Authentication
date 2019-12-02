using System;
using Newtonsoft.Json;

namespace Amsel.DTO.Authentication.Models
{
    public class AuthTokenDTO
    {
        public DateTime ExpireTime { get; set; }
        public string Token { get; set; }

        public bool Expired() {
            if (string.IsNullOrEmpty(Token))
                return true;

            if (ExpireTime == default)
                return false;

            return ExpireTime < DateTime.UtcNow;
        }

        public bool HasToken() { return Token != null && !Expired(); }

        #region  CONSTRUCTORS

        protected AuthTokenDTO() { }

        [JsonConstructor]
        public AuthTokenDTO(string token, DateTime expireTime) {
            Token = token;
            ExpireTime = expireTime;
        }

        #endregion
    }
}