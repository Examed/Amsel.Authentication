using System;
using Newtonsoft.Json;

namespace Amsel.DTO.Authentication.Models
{
    public class AuthTokenDTO
    {
        #region  CONSTRUCTORS

        protected AuthTokenDTO() { }

        public AuthTokenDTO(string token, DateTime expireTime) {
            Token             = token;
            RefreshToken      = null;
            ExpireTime        = expireTime;
            RefreshExpireTime = expireTime;
        }

        [JsonConstructor]
        public AuthTokenDTO(string token, string refreshToken, DateTime expireTime, DateTime refreshExpireTime) {
            Token             = token;
            RefreshToken      = refreshToken;
            ExpireTime        = expireTime;
            RefreshExpireTime = refreshExpireTime;
        }

        #endregion

        public DateTime ExpireTime { get; set; }

        public DateTime RefreshExpireTime { get; set; }

        public string RefreshToken { get; set; }

        public string Token { get; set; }

        public bool Expired() {
            if (string.IsNullOrEmpty(Token))
                return true;

            return ExpireTime < DateTime.UtcNow || RefreshExpireTime < DateTime.UtcNow;
        }

        public bool RefreshExpired() {
            if (string.IsNullOrEmpty(RefreshToken))
                return true;

            return RefreshExpireTime < DateTime.UtcNow.AddMinutes(1);
        }

        public void RemoveToken() {
            Token = null;
        }

        public bool HasRefreshToken() {
            return RefreshToken != null && !RefreshExpired();
        }

        public bool HasToken() {
            return Token != null && !Expired();
        }
    }
}