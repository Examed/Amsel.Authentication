﻿using Amsel.Enums.Authentication.Enums;

namespace Amsel.DTO.Authentication.Models
{
    public class TwitchTokenDTO
    {
        #region CONSTRUCTORS

        public TwitchTokenDTO(string accessToken, ETwitchScope scope)
        {
            AccessToken = accessToken;
            Scope = scope;
        }

        #endregion CONSTRUCTORS

        #region Public Properties

        public virtual string AccessToken { get; set; }

        public virtual ETwitchScope Scope { get; set; }

        #endregion Public Properties
    }
}