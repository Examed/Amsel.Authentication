using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Amsel.Framework.Structure.Helper;
using Amsel.Framework.Structure.Models.Address;
using Amsel.Resources.Authentication.Controller;
using Amsel.Resources.Authentication.Endpoints;
using JetBrains.Annotations;

namespace Amsel.Access.Authentication.Services
{
    public static class KeyAccess
    {
        #region STATICS, CONST and FIELDS

        [NotNull] public static readonly APIAddress PublicKeyURL = new APIAddress(AuthEndpointResources.ENDPOINT, AuthEndpointResources.KEY, KeyControllerResources.PUBLIC_KEY);

        #endregion

        [NotNull]
        private static readonly HttpClient Client = new HttpClient();

        [NotNull]
        public static async Task<RSACryptoServiceProvider> GetPublicKeyAsync() {
            string content = await GetPublicKeyStringAsync().ConfigureAwait(false);
            return RSACryptoKeyHelper.PublicKeyFromString(content);
        }

        [NotNull]
        private static async Task<string> GetPublicKeyStringAsync() {
            HttpResponseMessage response = await Client.GetAsync(PublicKeyURL.GetURL()).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await (response.Content?.ReadAsStringAsync()).ConfigureAwait(false);
        }
    }
}