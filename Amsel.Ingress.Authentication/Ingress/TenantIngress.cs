using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Amsel.DTO.Authentication.Models;
using Amsel.Framework.Infrastruktur.Application.Interfaces;
using Amsel.Framework.Infrastruktur.Application.Models.Address;
using Amsel.Framework.Infrastruktur.Application.Service;
using Amsel.Framework.Utilities.Extentions.Http;
using Amsel.Resources.Authentication.Controller;
using Amsel.Resources.Authentication.Endpoints;
using JetBrains.Annotations;

namespace Amsel.Ingress.Authentication.Ingress
{
    public class TenantIngress : GenericIngress
    {
        #region STATICS, CONST and FIELDS

        [NotNull] private static readonly APIAddress AllTenantsURL = new APIAddress(AuthEndpointResources.ENDPOINT,
                                                                                    AuthEndpointResources.TENANT,
                                                                                    TenantControllerResources.GET_ALL);

        [NotNull] private static readonly APIAddress TenantGetByNameURL = new APIAddress(AuthEndpointResources.ENDPOINT,
                                                                                         AuthEndpointResources.TENANT,
                                                                                         TenantControllerResources
                                                                                             .GET_BY_NAME);

        [NotNull] private static readonly APIAddress TenantGetByIdURL = new APIAddress(AuthEndpointResources.ENDPOINT,
                                                                                       AuthEndpointResources.TENANT,
                                                                                       TenantControllerResources
                                                                                           .GET_BY_ID);

        #endregion

        #region  CONSTRUCTORS

        public TenantIngress(IAuthService authenticationService) : base(authenticationService) { }

        #endregion

        [NotNull]
        public async Task<IEnumerable<TenantDTO>> GetAllAsync() {
            HttpResponseMessage response = await GetAsync(AllTenantsURL);
            return await response.DeserializeOrDefaultAsync<IEnumerable<TenantDTO>>();
        }


        [CanBeNull]
        public async Task<Guid?> GetIdByNameAsync(string name) {
            TenantDTO tenant = await GetTenantByNameAsync(name);
            return tenant.Id;
        }

        [NotNull]
        public async Task<TenantDTO> GetTenantByNameAsync(string name) {
            KeyValuePair<string, string> nameValue = new KeyValuePair<string, string>("name", name);
            HttpResponseMessage response = await GetAsync(TenantGetByNameURL, nameValue);
            return await response.DeserializeElseThrowAsync<TenantDTO>();
        }

        public async Task<TenantDTO> GetTenantAsync(Guid id) {
            KeyValuePair<string, string> idValue = new KeyValuePair<string, string>("id", id.ToString());
            HttpResponseMessage response = await GetAsync(TenantGetByIdURL, idValue);
            return await response.DeserializeElseThrowAsync<TenantDTO>();
        }
    }
}