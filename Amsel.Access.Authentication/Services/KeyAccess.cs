using Amsel.Framework.Structure.Factory;
using Amsel.Framework.Structure.Services;
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
    public class KeyAccess : GenericAccess
    {
        private const bool RequestLocal = false;

        [NotNull]
        public static readonly UriBuilder PublicKeyURL = UriBuilderFactory.GetAPIBuilder(AuthEndpointResources.ENDPOINT, AuthEndpointResources.KEY, KeyControllerResources.PUBLIC_KEY, RequestLocal);

        public KeyAccess() : base() { }

        [NotNull]
        private async Task<string> GetPublicKeyStringAsync()
        {
            HttpResponseMessage response = await GetAsync(PublicKeyURL).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await (response.Content?.ReadAsStringAsync()).ConfigureAwait(false);
        }

        #region PUBLIC METHODES
        [NotNull]
        public async Task<RSACryptoServiceProvider> GetPublicKeyAsync()
        {
            string content = await GetPublicKeyStringAsync().ConfigureAwait(false);
            return RSACryptoKeyHelper.PublicKeyFromString(content);
        }
        #endregion
    }
}