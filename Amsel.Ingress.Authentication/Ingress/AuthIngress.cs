using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Amsel.Framework.Infrastruktur.Application.Helper;
using Amsel.Framework.Infrastruktur.Application.Interfaces;
using Amsel.Framework.Infrastruktur.Application.Models.Address;
using Amsel.Framework.Infrastruktur.Application.Service;
using Amsel.Resources.Authentication.Controller;
using Amsel.Resources.Authentication.Endpoints;
using JetBrains.Annotations;

namespace Amsel.Ingress.Authentication.Ingress
{
    public class AuthIngress : GenericIngress
    {
        #region STATICS, CONST and FIELDS

        [NotNull] public static readonly APIAddress PublicKeyURL = new APIAddress(AuthEndpointResources.ENDPOINT, AuthEndpointResources.KEY, KeyControllerResources.PUBLIC_KEY);

        #endregion

        [NotNull]
        public async Task<RSACryptoServiceProvider> GetPublicKeyAsync() {
            string content = await GetPublicKeyStringAsync().ConfigureAwait(false);
            return RSACryptoKeyHelper.PublicKeyFromString(content);
        }

        [NotNull]
        private async Task<string> GetPublicKeyStringAsync() {
            HttpResponseMessage response = await GetAsync(PublicKeyURL).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await (response.Content?.ReadAsStringAsync()).ConfigureAwait(false);
        }

        #region  CONSTRUCTORS

        public AuthIngress() { }

        public AuthIngress(IAuthService authenticationService) : base(authenticationService) { }

        #endregion
    }
}