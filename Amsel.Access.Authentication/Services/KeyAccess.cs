using Amsel.Framework.Structure.Blazor.Authorize;
using Amsel.Framework.Structure.Client.Service;
using Amsel.Framework.Structure.Interfaces;
using Amsel.Framework.Structure.Models.Address;
using Amsel.Framework.Utilities.Helper;
using Amsel.Resources.Authentication.Controller;
using Amsel.Resources.Authentication.Endpoints;
using Autofac.Features.AttributeFilters;
using JetBrains.Annotations;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Amsel.Access.Authentication.Services
{
    public class KeyAccess : GenericAccess
    {
        public KeyAccess( ):base()
        {

        }
        #region STATICS, CONST and FIELDS

        private const bool RequestLocal = false;
        [NotNull] public static readonly UriBuilder PublicKeyURL = UriBuilderFactory.GetAPIBuilder(AuthEndpointResources.ENDPOINT, AuthEndpointResources.KEY, KeyControllerResources.PUBLIC_KEY, RequestLocal);
        #endregion

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