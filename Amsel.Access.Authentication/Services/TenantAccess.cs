using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Amsel.DTO.Authentication.Models;
using Amsel.Framework.Base.DTO;
using Amsel.Framework.Structure.Interfaces;
using Amsel.Framework.Structure.Models.Address;
using Amsel.Framework.Utilities.Extensions.Http;
using Amsel.Resources.Authentication.Controller;
using Amsel.Resources.Authentication.Endpoints;
using JetBrains.Annotations;

namespace Amsel.Access.Authentication.Services
{
    public class TenantAccess : CRUDAccess<MultiTenantDTO>
    {
        #region  CONSTRUCTORS

        public TenantAccess(IAuthenticationService authenticationService) : base(authenticationService) { }

        #endregion


        public async Task<Guid?> GetIdByNameAsync(string name)
        {
            MultiTenantDTO tenant = await GetTenantByNameAsync(name).ConfigureAwait(false);
            return tenant.Id;
        }

        [NotNull]
        public async Task<MultiTenantDTO> GetTenantByNameAsync(string name)
        {
            HttpResponseMessage response = await GetAsync(TenantGet, ("name", name)).ConfigureAwait(false);
            return await response.DeserializeElseThrowAsync<MultiTenantDTO>().ConfigureAwait(false);
        }

        public async Task<MultiTenantDTO> GetTenantAsync(Guid id)
        {
            HttpResponseMessage response = await GetAsync(TenantGet, ("id", id)).ConfigureAwait(false);
            return await response.DeserializeElseThrowAsync<MultiTenantDTO>().ConfigureAwait(false);
        }

        #region STATICS, CONST and FIELDS

        /// <inheritdoc />
        protected override string Endpoint => AuthEndpointResources.ENDPOINT;

        /// <inheritdoc />
        protected override string Resource => AuthEndpointResources.TENANT;

        [NotNull] private APIAddress TenantGet => new APIAddress(Endpoint, Resource, TenantControllerResources.GET);

        #endregion
    }
}