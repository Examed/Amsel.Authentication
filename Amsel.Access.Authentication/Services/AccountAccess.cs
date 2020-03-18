using Amsel.Framework.Structure.Client.Service;
using Amsel.Framework.Structure.Interfaces;
using Amsel.Framework.Structure.Models.Address;
using Amsel.Framework.Utilities.Extensions.Http;
using Amsel.Model.Authentication.Account;
using Amsel.Resources.Authentication.Controller;
using Amsel.Resources.Authentication.Endpoints;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Amsel.Access.Authentication.Services
{
    public class AccountServiceAccess : AccountAccess
    {
        public AccountServiceAccess(ISecretAuthenticationService authentication) : base(authentication)
        {
        }

    }

    public class AccountAccess : GenericAccess
    {
        #region STATICS, CONST and FIELDS

        private const bool RequestLocal = false;
        [NotNull] private static readonly UriBuilder AllAccountURL = UriBuilderFactory.GetAPIBuilder(AuthEndpointResources.ENDPOINT, AuthEndpointResources.ACCOUNT, AccountControllerResources.GET_ALL, RequestLocal);

        #endregion

        #region  CONSTRUCTORS

        public AccountAccess(IAuthenticationService authenticationService) : base(authenticationService) { }
        #endregion

        #region PUBLIC METHODES
        [NotNull]
        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            HttpResponseMessage response = await GetAsync(AllAccountURL).ConfigureAwait(false);
            return await response.DeserializeOrDefaultAsync<IEnumerable<Account>>().ConfigureAwait(false);
        }
        #endregion
    }
}