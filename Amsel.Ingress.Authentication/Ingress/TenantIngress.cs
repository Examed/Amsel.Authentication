using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Amsel.DTO.Authentication.Models;
using Amsel.Framework.Structure.Interfaces;
using Amsel.Framework.Structure.Models.Address;
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
            KeyValuePair<string, object> nameValue = new KeyValuePair<string, object>("name", name);
            HttpResponseMessage response = await GetAsync(TenantGet, nameValue).ConfigureAwait(false);
            return await response.DeserializeElseThrowAsync<TenantDTO>().ConfigureAwait(false);
        }

        public async Task<TenantDTO> GetTenantAsync(Guid id) {
            KeyValuePair<string, object> idValue = new KeyValuePair<string, object>("id", id.ToString());
            HttpResponseMessage response = await GetAsync(TenantGet, idValue).ConfigureAwait(false);
            return await response.DeserializeElseThrowAsync<TenantDTO>().ConfigureAwait(false);
        }

        #region STATICS, CONST and FIELDS

        /// <inheritdoc />
        protected override string Endpoint => AuthEndpointResources.ENDPOINT;

        /// <inheritdoc />
        protected override string Resource => AuthEndpointResources.TENANT;

        [NotNull]
        private APIAddress TenantGet => new APIAddress(Endpoint, Resource, TenantControllerResources.GET);

        #endregion
    }
}