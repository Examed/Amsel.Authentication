using System;
using Newtonsoft.Json;

namespace Amsel.DTO.Authentication.Models
{
    public class AuthRefreshTokenDTO
    {
        public AuthRefreshTokenDTO(AuthTokenDTO token, AuthTokenDTO refresh)
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));
            Refresh = refresh ?? throw new ArgumentNullException(nameof(refresh));
        }

        public AuthTokenDTO Token { get; set; }
        public AuthTokenDTO Refresh { get; set; }
    }

    public class AuthTokenDTO
    {
        #region  CONSTRUCTORS

        protected AuthTokenDTO() { }

        [JsonConstructor]
        public AuthTokenDTO(string token, DateTime expireTime)
        {
            Token = token;
            ExpireTime = expireTime;
        }


        #endregion

        public DateTime ExpireTime { get; set; }


        public string Token { get; set; }

        public bool Expired()
        {
            if (string.IsNullOrEmpty(Token))
                return true;

            if (ExpireTime == default)
                return false;

            return ExpireTime < DateTime.UtcNow;
        }

        public bool HasToken()
        {
            return Token != null && !Expired();
        }
    }
}