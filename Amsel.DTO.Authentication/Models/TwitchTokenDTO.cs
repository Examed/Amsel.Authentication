using Amsel.Enums.Authentication.Enums;

namespace Amsel.DTO.Authentication.Models
{
    public class TwitchTokenDTO
    {
        public virtual string AccessToken { get; set; }

        public virtual ETwitchScope Scope { get; set; }

        #region  CONSTRUCTORS

        public TwitchTokenDTO(string accessToken, ETwitchScope scope)
        {
            AccessToken = accessToken;
            Scope = scope;
        }
        #endregion
    }
}