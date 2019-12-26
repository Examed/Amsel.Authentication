using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Amsel.Framework.Structure.Ingress.Models;
using Amsel.Framework.Structure.Interfaces;
using Amsel.Framework.Structure.Models.Address;
using Amsel.Framework.Structure.Service;
using Amsel.Framework.Utilities.Extentions.Http;
using Amsel.Framework.Utilities.Extentions.Types;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Amsel.Ingress.Authentication.Ingress
{
    public abstract class CRUDIngress<TEntity> : GenericIngress
    {
        protected CRUDIngress(IAuthService authService) : base(authService) { }

        [NotNull]
        protected abstract APIAddress ReadAddress { get; }

        [NotNull]
        protected abstract APIAddress InsertAddress { get; }

        [NotNull]
        protected abstract APIAddress UpdateAddress { get; }

        [NotNull]
        protected abstract APIAddress RemoveAddress { get; }


        public virtual TEntity Insert(TEntity data) { return InsertAsync(data).Result; }

        [NotNull]
        public virtual async Task<TEntity> InsertAsync(TEntity data) {
            HttpResponseMessage response = await PostAsync(InsertAddress, GetJsonContent(data)).ConfigureAwait(false);
            return await response.DeserializeElseThrowAsync<TEntity>().ConfigureAwait(false);
        }

        public virtual bool Remove(TEntity data) { return RemoveAsync(data).Result; }

        [NotNull]
        public virtual async Task<bool> RemoveAsync(object data) {
            HttpResponseMessage response = await PostAsync(RemoveAddress, GetJsonContent(data)).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        public virtual TEntity Update(TEntity data) { return UpdateAsync(data).Result; }

        [NotNull]
        public virtual async Task<TEntity> UpdateAsync(TEntity data) {
            HttpResponseMessage response = await PostAsync(UpdateAddress, GetJsonContent(data)).ConfigureAwait(false);
            return await response.DeserializeOrDefaultAsync<TEntity>().ConfigureAwait(false);
        }

        public virtual (IEnumerable<TEntity> value, int? count) Read(string jsonLogicFilter = null, IEnumerable<OrderByDTO>? sort = null, int? skip = null, int? take = null) {
            return ReadAsync(jsonLogicFilter, sort, skip, take).Result;
        }

        public virtual async Task<(IEnumerable<TEntity> value, int? count)> ReadAsync(string jsonLogicFilter = null, IEnumerable<OrderByDTO>? orderBy = null, int? skip = null, int? take = null) {
            Dictionary<string, object> parameters = new Dictionary<string, object> {{nameof(skip), skip.ToString()}, {nameof(take), take.ToString()}};
            if (!jsonLogicFilter.IsNullOrEmpty())
                parameters.Add(nameof(jsonLogicFilter), JsonConvert.SerializeObject(jsonLogicFilter));
            // if (!orderBy.IsNullOrEmpty())
            //parameters.Add(nameof(orderBy), JsonConvert.SerializeObject(orderBy));

            HttpResponseMessage response = await GetAsync(ReadAddress, parameters).ConfigureAwait(false);
            return await response.DeserializeOrDefaultAsync<(IEnumerable<TEntity> value, int? count)>().ConfigureAwait(false);
        }
    }
}