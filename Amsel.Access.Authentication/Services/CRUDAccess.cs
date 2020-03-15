using Amsel.Framework.Structure.Client.Service;
using Amsel.Framework.Structure.Interfaces;
using Amsel.Framework.Structure.Models;
using Amsel.Framework.Structure.Models.Address;
using Amsel.Framework.Utilities.Extensions.Http;
using Amsel.Resources.Authentication.Controller;
using JetBrains.Annotations;
using Syncfusion.EJ2.Blazor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Amsel.Access.Authentication.Services
{
    public abstract class CRUDAccess<TEntity> : GenericAccess
    {
        [NotNull] protected abstract string Endpoint { get; }
        [NotNull] protected abstract bool RequestLocal { get; }

        [NotNull] protected virtual UriBuilder GetAllAddress => UriBuilderFactory.GetAPIBuilder(Endpoint, Resource, CRUDControllerResources.GET_ALL, RequestLocal);

        [NotNull] protected virtual UriBuilder GetByIdAddress => UriBuilderFactory.GetAPIBuilder(Endpoint, Resource, CRUDControllerResources.GET_BY_ID, RequestLocal);

        [NotNull] protected virtual UriBuilder InsertAddress => UriBuilderFactory.GetAPIBuilder(Endpoint, Resource, CRUDControllerResources.INSERT, RequestLocal);

        [NotNull] protected virtual UriBuilder ReadAddress => UriBuilderFactory.GetAPIBuilder(Endpoint, Resource, CRUDControllerResources.READ, RequestLocal);

        [NotNull] protected virtual UriBuilder ReadEJ2Address => UriBuilderFactory.GetAPIBuilder(Endpoint, Resource, CRUDControllerResources.READ_EJ2, RequestLocal);

        [NotNull] protected virtual UriBuilder RemoveAddress => UriBuilderFactory.GetAPIBuilder(Endpoint, Resource, CRUDControllerResources.REMOVE, RequestLocal);

        [NotNull] protected abstract string Resource { get; }

        [NotNull] protected virtual UriBuilder UpdateAddress => UriBuilderFactory.GetAPIBuilder(Endpoint, Resource, CRUDControllerResources.UPDATE, RequestLocal);

        protected CRUDAccess(TenantName tenantName, IAuthenticationService authService) : base(tenantName, authService) { }
        protected CRUDAccess(IAuthenticationService authService) : base(authService) { }

        #region PUBLIC METHODES
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(int? skip = null, int? take = null)
        {
            HttpResponseMessage response = await GetAsync(GetAllAddress, (nameof(skip), skip), (nameof(take), take))
                                                     .ConfigureAwait(false);
            return await response.DeserializeOrDefaultAsync<IEnumerable<TEntity>>().ConfigureAwait(false);
        }

        [NotNull]
        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            HttpResponseMessage response = await GetAsync(GetByIdAddress, (nameof(id), id)).ConfigureAwait(false);
            return await response.DeserializeOrDefaultAsync<TEntity>().ConfigureAwait(false);
        }

        [NotNull]
        public virtual async Task<TEntity> InsertAsync(TEntity data)
        {
            HttpResponseMessage response = await PostAsync(InsertAddress, GetJsonContent(data)).ConfigureAwait(false);
            return await response.DeserializeElseThrowAsync<TEntity>().ConfigureAwait(false);
        }

        public virtual async Task<(IEnumerable<TEntity> value, int count)> ReadAsync(int? skip = null, int? take = null)
        {
            HttpResponseMessage response = await PostAsync(ReadAddress, null, (nameof(skip), skip), (nameof(take), take))
                                                     .ConfigureAwait(false);
            return await response.DeserializeOrDefaultAsync<(IEnumerable<TEntity> value, int count)>()
                             .ConfigureAwait(false);
        }

        public virtual async Task<(IEnumerable<TEntity> value, int count)> ReadEJ2Async(DataManagerRequest dm = null)
        {
            HttpResponseMessage response = await PostAsync(ReadEJ2Address, GetJsonContent(dm)).ConfigureAwait(false);
            return await response.DeserializeOrDefaultAsync<(IEnumerable<TEntity> value, int count)>()
                             .ConfigureAwait(false);
        }

        [NotNull]
        public virtual async Task<object> RemoveAsync(object data)
        {
            HttpResponseMessage response = await PostAsync(RemoveAddress, GetJsonContent(data)).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        [NotNull]
        public virtual async Task<TEntity> UpdateAsync(TEntity data)
        {
            HttpResponseMessage response = await PostAsync(UpdateAddress, GetJsonContent(data)).ConfigureAwait(false);
            return await response.DeserializeOrDefaultAsync<TEntity>().ConfigureAwait(false);
        }
        #endregion
    }
}