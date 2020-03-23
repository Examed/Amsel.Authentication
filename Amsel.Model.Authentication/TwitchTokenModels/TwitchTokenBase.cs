using Amsel.Enums.Authentication.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Amsel.Model.Authentication.TwitchTokenModels
{
    public class TwitchTokenBase
    {
        [Required]
        public string AccessToken { get; protected set; }

        [Required]
        public ETwitchScope Scope { get; protected set; }
    }
}