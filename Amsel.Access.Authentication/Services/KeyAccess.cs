﻿using Amsel.Framework.Structure.Factory;
using Amsel.Framework.Structure.Services;
using Amsel.Framework.Utilities.Helper;
using Amsel.Resources.Authentication.Controller;
using Amsel.Resources.Authentication.Endpoints;
using JetBrains.Annotations;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Amsel.Access.Authentication.Services {
    public class KeyAccess : GenericAccess {
        [NotNull]
        public static readonly UriBuilder PublicKeyURL = UriBuilderFactory.GetAPIBuilder(AuthEndpointResources.ENDPOINT, AuthEndpointResources.KEY, KeyControllerResources.PUBLIC_KEY, RequestLocal);
        private const bool RequestLocal = false;

        public KeyAccess() : base() { }

        [NotNull]
        public async Task<RSACryptoServiceProvider> GetPublicKeyAsync()
        {
            string content = await GetPublicKeyStringAsync().ConfigureAwait(false);
            return RSACryptoKeyHelper.PublicKeyFromString(content);
        }

        [NotNull]
        private async Task<string> GetPublicKeyStringAsync()
        {
            HttpResponseMessage response = await GetAsync(PublicKeyURL).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await (response.Content?.ReadAsStringAsync()).ConfigureAwait(false);
        }
    }
}