using Amsel.Framework.Structure.Models.Address;
using Amsel.Framework.Utilities.Helper;
using Amsel.Resources.Authentication.Controller;
using Amsel.Resources.Authentication.Endpoints;
using JetBrains.Annotations;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Amsel.Access.Authentication.Services
{
    public static class KeyAccess
    {
        [NotNull] private static readonly HttpClient Client = new HttpClient();
        #region STATICS, CONST and FIELDS

        private const bool RequestLocal = false;
        [NotNull] public static readonly UriBuilder PublicKeyURL = UriBuilderFactory.GetAPIBuilder(AuthEndpointResources.ENDPOINT, AuthEndpointResources.KEY, KeyControllerResources.PUBLIC_KEY, RequestLocal);
        #endregion

        [NotNull]
        private static async Task<string> GetPublicKeyStringAsync()
        {
            HttpResponseMessage response = await Client.GetAsync(PublicKeyURL.Uri).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await (response.Content?.ReadAsStringAsync()).ConfigureAwait(false);
        }

        #region PUBLIC METHODES
        [NotNull]
        public static async Task<RSACryptoServiceProvider> GetPublicKeyAsync()
        {
            string content = await GetPublicKeyStringAsync().ConfigureAwait(false);
            return RSACryptoKeyHelper.PublicKeyFromString(content);
        }
        #endregion
    }
}