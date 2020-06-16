using Amsel.Framework.Structure.Factory;
using Amsel.Framework.Structure.Interfaces;
using Amsel.Framework.Structure.Services;
using Amsel.Framework.Utilities.Extensions.Http;
using Amsel.Model.Authentication.AccountModels;
using Amsel.Resources.Authentication.Controller;
using Amsel.Resources.Authentication.Endpoints;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Amsel.Access.Authentication.Services {
    public class AccountAccess : GenericAccess {
        private const bool RequestLocal = false;
        [NotNull]
        private static readonly UriBuilder AllAccountURL = UriBuilderFactory.GetAPIBuilder(AuthEndpointResources.ENDPOINT, AuthEndpointResources.ACCOUNT, AccountControllerResources.GET_ALL, RequestLocal);

        public AccountAccess(IAuthenticationService authenticationService) : base(authenticationService) { }

        [NotNull]
        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            HttpResponseMessage response = await GetAsync(AllAccountURL).ConfigureAwait(false);
            return await response.DeserializeOrDefaultAsync<IEnumerable<Account>>().ConfigureAwait(false);
        }
    }

    public class AccountServiceAccess : AccountAccess {
        public AccountServiceAccess(ISecretAuthenticationService authentication) : base(authentication) { }
    }
}