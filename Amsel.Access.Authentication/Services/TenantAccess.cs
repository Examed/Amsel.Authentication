using Amsel.Framework.Base.DTO;
using Amsel.Framework.Structure.Blazor.Authorize;
using Amsel.Framework.Structure.Interfaces;
using Amsel.Framework.Structure.Models.Address;
using Amsel.Framework.Utilities.Extensions.Http;
using Amsel.Resources.Authentication.Controller;
using Amsel.Resources.Authentication.Endpoints;
using JetBrains.Annotations;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Amsel.Access.Authentication.Services
{

    public class TenantServiceAccess : TenantAccess
    {
        public TenantServiceAccess(SecretAuthentication authentication) : base(authentication)
        {
        }
    }

    public class TenantAccess : CRUDAccess<TenantDTO>
    {
        #region  CONSTRUCTORS

        public TenantAccess(IAuthenticationService authenticationService) : base(authenticationService) { }

        #region PUBLIC METHODES
        #endregion


        public async Task<Guid?> GetIdByNameAsync(string name)
        {
            TenantDTO tenant = await GetTenantByNameAsync(name).ConfigureAwait(false);
            return tenant.Id;
        }

        public async Task<TenantDTO> GetTenantAsync(Guid id)
        {
            HttpResponseMessage response = await GetAsync(TenantGet, (nameof(id), id)).ConfigureAwait(false);
            return await response.DeserializeElseThrowAsync<TenantDTO>().ConfigureAwait(false);
        }

        [NotNull]
        public async Task<TenantDTO> GetTenantByNameAsync(string name)
        {
            HttpResponseMessage response = await GetAsync(TenantGet, (nameof(name), name)).ConfigureAwait(false);
            return await response.DeserializeElseThrowAsync<TenantDTO>().ConfigureAwait(false);
        }
        #endregion

        #region STATICS, CONST and FIELDS

        /// <inheritdoc/>
        protected override string Endpoint => AuthEndpointResources.ENDPOINT;

        /// <inheritdoc/>
        protected override string Resource => AuthEndpointResources.TENANT;

        [NotNull] private UriBuilder TenantGet => UriBuilderFactory.GetAPIBuilder(Endpoint, Resource, TenantControllerResources.GET, RequestLocal);

        protected override bool RequestLocal => false;
        #endregion
    }
}