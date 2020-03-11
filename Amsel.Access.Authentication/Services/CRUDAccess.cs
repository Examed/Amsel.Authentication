using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Amsel.Framework.Structure.Client.Service;
using Amsel.Framework.Structure.Interfaces;
using Amsel.Framework.Structure.Models.Address;
using Amsel.Framework.Utilities.Extensions.Http;
using Amsel.Resources.Authentication.Controller;
using JetBrains.Annotations;
using Syncfusion.EJ2.Blazor;

namespace Amsel.Access.Authentication.Services
{
    // TODO move to Project


    public abstract class CRUDAccess<TEntity> : GenericAccess
    {
        protected CRUDAccess(IAuthenticationService authService) : base(authService) { }


        [NotNull] protected abstract string Endpoint { get; }

        [NotNull] protected abstract string Resource { get; }


        [NotNull] protected virtual APIAddress ReadAddress => new APIAddress(Endpoint, Resource, CRUDControllerResources.READ);
        [NotNull] protected virtual APIAddress ReadEJ2Address => new APIAddress(Endpoint, Resource, CRUDControllerResources.READ_EJ2);
        [NotNull] protected virtual APIAddress InsertAddress => new APIAddress(Endpoint, Resource, CRUDControllerResources.INSERT);

        [NotNull] protected virtual APIAddress UpdateAddress => new APIAddress(Endpoint, Resource, CRUDControllerResources.UPDATE);

        [NotNull] protected virtual APIAddress RemoveAddress => new APIAddress(Endpoint, Resource, CRUDControllerResources.REMOVE);

        [NotNull] protected virtual APIAddress GetByIdAddress => new APIAddress(Endpoint, Resource, CRUDControllerResources.GET_BY_ID);

        [NotNull] protected virtual APIAddress GetAllAddress => new APIAddress(Endpoint, Resource, CRUDControllerResources.GET_ALL);

        [NotNull]
        public virtual async Task<TEntity> InsertAsync(TEntity data)
        {
            HttpResponseMessage response = await PostAsync(InsertAddress, GetJsonContent(data)).ConfigureAwait(false);
            return await response.DeserializeElseThrowAsync<TEntity>().ConfigureAwait(false);
        }


        [NotNull]
        public virtual async Task<object> RemoveAsync(object data)
        {
            HttpResponseMessage response = await PostAsync(RemoveAddress, GetJsonContent(data)).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }


        [NotNull]
        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            HttpResponseMessage response = await GetAsync(GetByIdAddress, ("id", id)).ConfigureAwait(false);
            return await response.DeserializeOrDefaultAsync<TEntity>().ConfigureAwait(false);
        }


        [NotNull]
        public virtual async Task<TEntity> UpdateAsync(TEntity data)
        {

            HttpResponseMessage response = await PostAsync(UpdateAddress, GetJsonContent(data)).ConfigureAwait(false);
            return await response.DeserializeOrDefaultAsync<TEntity>().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(int? skip = null, int? take = null)
        {
            HttpResponseMessage response = await GetAsync(GetAllAddress, (nameof(skip),skip), (nameof(take),take)).ConfigureAwait(false);
            return await response.DeserializeOrDefaultAsync<IEnumerable<TEntity>>().ConfigureAwait(false);
        }

        public virtual async Task<(IEnumerable<TEntity> value, int count)> ReadAsync(int? skip = null, int? take = null)
        {
            HttpResponseMessage response = await PostAsync(ReadAddress, (nameof(skip),skip), (nameof(take),take)).ConfigureAwait(false);
            return await response.DeserializeOrDefaultAsync<(IEnumerable<TEntity> value, int count)>().ConfigureAwait(false);
        }

        public virtual async Task<(IEnumerable<TEntity> value, int count)> ReadEJ2Async(DataManagerRequest dm = null)
        {
            HttpResponseMessage response = await PostAsync(ReadEJ2Address, GetJsonContent(dm)).ConfigureAwait(false);
            return await response.DeserializeOrDefaultAsync<(IEnumerable<TEntity> value, int count)>().ConfigureAwait(false);
        }
    }
}