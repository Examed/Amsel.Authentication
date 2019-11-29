using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Amsel.DTO.Authentication.Models;
using Amsel.Framework.Infrastruktur.Application.Interfaces;
using Amsel.Framework.Infrastruktur.Application.Models.Address;
using Amsel.Framework.Infrastruktur.Application.Service;
using Amsel.Framework.Utilities.Extentions.Http;
using Amsel.Resources.Authentication.Controller;
using Amsel.Resources.Authentication.Endpoints;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Amsel.Ingress.Authentication.Ingress
{
    public abstract class CRUDIngress<TEntity> : GenericIngress
    {
        protected CRUDIngress(IAuthService authService) : base(authService)
        {
        }

        [NotNull]
        protected abstract APIAddress ReadAddress { get; }
        [NotNull]
        protected abstract APIAddress InsertAddress { get; }
        [NotNull]
        protected abstract APIAddress UpdateAddress { get; }
        [NotNull]
        protected abstract APIAddress RemoveAddress { get; }

        [NotNull]
        public virtual TEntity Insert(TEntity data)
        {
            return InsertAsync(data).Result;
        }

        [NotNull]
        public virtual async Task<TEntity> InsertAsync(TEntity data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await PostAsync(InsertAddress, content);
            return await response.DeserializeElseThrowAsync<TEntity>();
        }

        [NotNull]
        public virtual bool Remove(TEntity data)
        {
            return RemoveAsync(data).Result;
        }

        [NotNull]
        public virtual async Task<bool> RemoveAsync(TEntity data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await PostAsync(RemoveAddress, content);
            return response.IsSuccessStatusCode;
        }


        [NotNull]
        public virtual TEntity Update(TEntity data)
        {
            return UpdateAsync(data).Result;
        }

        [NotNull]
        public virtual async Task<TEntity> UpdateAsync(TEntity data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await PutAsync(UpdateAddress, content);
            return await response.DeserializeOrDefaultAsync<TEntity>();
        }
     
        public virtual IEnumerable<TEntity> Read(int? skip = null, int? take = null)
        {
            return ReadAsync(skip, take).Result;
        }

        public virtual async Task<IEnumerable<TEntity>> ReadAsync(int? skip = null, int? take = null)
        {
            HttpResponseMessage response = await GetAsync(ReadAddress, skip, take);
            return await response.DeserializeOrDefaultAsync<IEnumerable<TEntity>>();
        }
    }

    public class TenantIngress : CRUDIngress<TenantDTO>
    {
        #region STATICS, CONST and FIELDS

        protected override APIAddress ReadAddress
            => new APIAddress(AuthEndpointResources.ENDPOINT, AuthEndpointResources.TENANT, CRUDControllerResources.READ);
        protected override APIAddress InsertAddress
            => new APIAddress(AuthEndpointResources.ENDPOINT, AuthEndpointResources.TENANT, CRUDControllerResources.INSERT);
        protected override APIAddress UpdateAddress
            => new APIAddress(AuthEndpointResources.ENDPOINT, AuthEndpointResources.TENANT, CRUDControllerResources.UPDATE);
        protected override APIAddress RemoveAddress
            => new APIAddress(AuthEndpointResources.ENDPOINT, AuthEndpointResources.TENANT, CRUDControllerResources.REMOVE);

        [NotNull]
        private static readonly APIAddress TenantGetByNameURL
            = new APIAddress(AuthEndpointResources.ENDPOINT, AuthEndpointResources.TENANT, TenantControllerResources.GET_BY_NAME);

        [NotNull]
        private static readonly APIAddress TenantGetByIdURL
            = new APIAddress(AuthEndpointResources.ENDPOINT, AuthEndpointResources.TENANT, TenantControllerResources.GET_BY_ID);

        #endregion

        #region  CONSTRUCTORS

        public TenantIngress(IAuthService authenticationService) : base(authenticationService) { }

        #endregion




        [CanBeNull]
        public async Task<Guid?> GetIdByNameAsync(string name)
        {
            TenantDTO tenant = await GetTenantByNameAsync(name);
            return tenant.Id;
        }

        [NotNull]
        public async Task<TenantDTO> GetTenantByNameAsync(string name)
        {
            KeyValuePair<string, string> nameValue = new KeyValuePair<string, string>("name", name);
            HttpResponseMessage response = await GetAsync(TenantGetByNameURL, nameValue);
            return await response.DeserializeElseThrowAsync<TenantDTO>();
        }

        public async Task<TenantDTO> GetTenantAsync(Guid id)
        {
            KeyValuePair<string, string> idValue = new KeyValuePair<string, string>("id", id.ToString());
            HttpResponseMessage response = await GetAsync(TenantGetByIdURL, idValue);
            return await response.DeserializeElseThrowAsync<TenantDTO>();
        }
    }
}