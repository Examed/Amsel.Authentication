using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Amsel.Model.Authentication.Models {
    public class AuthToken {
        protected AuthToken() { }
        public AuthToken(string token) => Token = token;
        [JsonConstructor]
        public AuthToken(string token, DateTime expireTime) {
            Token = token;
            ExpireTime = expireTime;
        }

        public DateTime ExpireTime { get; set; }
        [Required]
        public string Token { get; set; }

        public bool Expired() {
            if (string.IsNullOrEmpty(Token)) {
                return true;
            }

            if (ExpireTime == default) {
                return false;
            }

            return ExpireTime < DateTime.UtcNow;
        }
    }
}