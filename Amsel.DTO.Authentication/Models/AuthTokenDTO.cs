using Newtonsoft.Json;
using System;

namespace Amsel.DTO.Authentication.Models
{
    public class AuthTokenDTO
    {
        public DateTime ExpireTime { get; set; }

        public string Token { get; set; }

        #region PUBLIC METHODES
        public bool Expired()
        {
            if(string.IsNullOrEmpty(Token))
                return true;

            if(ExpireTime == default)
                return false;

            return ExpireTime < DateTime.UtcNow;
        }
        #endregion

        #region  CONSTRUCTORS

        protected AuthTokenDTO() { }
        public AuthTokenDTO(string token) {  Token = token;}

        [JsonConstructor]
        public AuthTokenDTO(string token, DateTime expireTime)
        {
            Token = token;
            ExpireTime = expireTime;
        }
        #endregion
    }
}