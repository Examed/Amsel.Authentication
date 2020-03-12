using Amsel.Framework.Base.DTO;
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
    public class TenantAccess : CRUDAccess<MultiTenantDTO>
    {
        #region  CONSTRUCTORS

        public TenantAccess(IAuthenticationService authenticationService) : base(authenticationService) { }

        #region PUBLIC METHODES
        #endregion


        public async Task<Guid?> GetIdByNameAsync(string name)
        {
            MultiTenantDTO tenant = await GetTenantByNameAsync(name).ConfigureAwait(false);
            return tenant.Id;
        }

        public async Task<MultiTenantDTO> GetTenantAsync(Guid id)
        {
            HttpResponseMessage response = await GetAsync(TenantGet, (nameof(id), id)).ConfigureAwait(false);
            return await response.DeserializeElseThrowAsync<MultiTenantDTO>().ConfigureAwait(false);
        }

        [NotNull]
        public async Task<MultiTenantDTO> GetTenantByNameAsync(string name)
        {
            HttpResponseMessage response = await GetAsync(TenantGet, (nameof(name), name)).ConfigureAwait(false);
            return await response.DeserializeElseThrowAsync<MultiTenantDTO>().ConfigureAwait(false);
        }
        #endregion

        #region STATICS, CONST and FIELDS

        /// <inheritdoc/>
        protected override string Endpoint => AuthEndpointResources.ENDPOINT;

        /// <inheritdoc/>
        protected override string Resource => AuthEndpointResources.TENANT;

        [NotNull] private APIAddress TenantGet => new APIAddress(Endpoint, Resource, TenantControllerResources.GET);
    #endregion
    }
}