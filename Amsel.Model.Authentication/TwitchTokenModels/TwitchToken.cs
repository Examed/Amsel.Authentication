using Amsel.Enums.Authentication.Enums;
using Amsel.Enums.Authentication.EnumStrings;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amsel.Model.Authentication.TwitchTokenModels
{
    [Owned]
    [ComplexType]
    public class TwitchToken : TwitchTokenBase
    {
        protected TwitchToken() { }

        [JsonConstructor]
        public TwitchToken([JsonProperty("access_Token")]string token,
                           [JsonProperty("scope")] List<string> scope,
                           [JsonProperty("expires_in")] double expireId,
                           [JsonProperty("refresh_Token")] string refreshToken)
        {
            AccessToken = token;

            if(scope == null)
                Scope = ETwitchScope.NONE;
            else
                Scope = scope.GetScopes();

            if(expireId > 0)
                ExpireTime = expireId.Equals(double.MaxValue) ? DateTime.MaxValue : DateTime.UtcNow.AddSeconds(expireId);
            ;

            RefreshToken = refreshToken;
        }


        public bool IsExpired()
        {
            if(ExpireTime == null)
                return false;
            if(ExpireTime == default)
                return false;
            if(ExpireTime >= DateTime.UtcNow)
                return false;
            return true;
        }

        public void UpdateToken(TwitchToken token)
        {
            RefreshToken = token.RefreshToken;
            AccessToken = token.AccessToken;
            Scope = token.Scope;
            ExpireTime = token.ExpireTime;
        }

        [DefaultValue(null)]
        public DateTime ExpireTime { get; protected set; }

        [DefaultValue(null)]
        public string RefreshToken { get; protected set; }
    }
}