using Amsel.Enums.Authentication.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Amsel.Endpoint.Authentication.Persistence
{
    public class TwitchTokenBase
    {
        [Required]
        public ETwitchScope Scope { get; protected set; }

        [Required]
        public string AccessToken { get; protected set; }
    }
}