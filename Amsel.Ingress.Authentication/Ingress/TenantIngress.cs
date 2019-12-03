using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Amsel.DTO.Authentication.Models;
using Amsel.Framework.Infrastruktur.Application.Interfaces;
using Amsel.Framework.Infrastruktur.Application.Models.Address;
using Amsel.Framework.Utilities.Extentions.Http;
using Amsel.Resources.Authentication.Controller;
using Amsel.Resources.Authentication.Endpoints;
using JetBrains.Annotations;

namespace Amsel.Ingress.Authentication.Ingress
{
    public class TenantIngress : CRUDIngress<TenantDTO>
    {
        #region  CONSTRUCTORS

        public TenantIngress(IAuthService authenticationService) : base(authenticationService) { }

        #endregion


        [CanBeNull]
        public async Task<Guid?> GetIdByNameAsync(string name) {
            TenantDTO tenant = await GetTenantByNameAsync(name).ConfigureAwait(false);
            return tenant.Id;
        }

        [NotNull]
        public async Task<TenantDTO> GetTenantByNameAsync(string name) {
            KeyValuePair<string, string> nameValue = new KeyValuePair<string, string>("name", name);
            HttpResponseMessage response = await GetAsync(TenantGet, nameValue).ConfigureAwait(false);
            return await response.DeserializeElseThrowAsync<TenantDTO>().ConfigureAwait(false);
        }

        public async Task<TenantDTO> GetTenantAsync(Guid id) {
            KeyValuePair<string, string> idValue = new KeyValuePair<string, string>("id", id.ToString());
            HttpResponseMessage response = await GetAsync(TenantGet, idValue).ConfigureAwait(false);
            return await response.DeserializeElseThrowAsync<TenantDTO>().ConfigureAwait(false);
        }

        #region STATICS, CONST and FIELDS

        protected override APIAddress ReadAddress => new APIAddress(AuthEndpointResources.ENDPOINT, AuthEndpointResources.TENANT, CRUDControllerResources.READ);

        protected override APIAddress InsertAddress => new APIAddress(AuthEndpointResources.ENDPOINT, AuthEndpointResources.TENANT, CRUDControllerResources.INSERT);

        protected override APIAddress UpdateAddress => new APIAddress(AuthEndpointResources.ENDPOINT, AuthEndpointResources.TENANT, CRUDControllerResources.UPDATE);

        protected override APIAddress RemoveAddress => new APIAddress(AuthEndpointResources.ENDPOINT, AuthEndpointResources.TENANT, CRUDControllerResources.REMOVE);

        [NotNull]
        private static readonly APIAddress TenantGet = new APIAddress(AuthEndpointResources.ENDPOINT, AuthEndpointResources.TENANT, TenantControllerResources.GET);

        #endregion
    }
}