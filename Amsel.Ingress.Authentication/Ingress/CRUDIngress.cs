using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Amsel.Framework.Structure.Ingress.Models;
using Amsel.Framework.Structure.Interfaces;
using Amsel.Framework.Structure.Models.Address;
using Amsel.Framework.Structure.Service;
using Amsel.Framework.Utilities.Extentions.Http;
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
        public virtual async Task<TEntity> InsertAsync(TEntity data)
        {
            string json = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await PostAsync(InsertAddress, content).ConfigureAwait(false);
            return await response.DeserializeElseThrowAsync<TEntity>().ConfigureAwait(false);
        }

        public virtual bool Remove(TEntity data) { return RemoveAsync(data).Result; }

        [NotNull]
        public virtual async Task<bool> RemoveAsync(object data)
        {
            string json = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await PostAsync(RemoveAddress, content).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        public virtual TEntity Update(TEntity data) { return UpdateAsync(data).Result; }

        [NotNull]
        public virtual async Task<TEntity> UpdateAsync(TEntity data)
        {
            string json = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await PostAsync(UpdateAddress, content).ConfigureAwait(false);
            return await response.DeserializeOrDefaultAsync<TEntity>().ConfigureAwait(false);
        }

        public virtual (IEnumerable<TEntity> value, int count) Read(IEnumerable<SearchDTO>? filter = null, IEnumerable<OrderByDTO>? sort = null, int? skip = null, int? take = null)
        {
            return ReadAsync(filter, sort, skip, take).Result;
        }

        public virtual async Task<(IEnumerable<TEntity> value, int count)> ReadAsync(IEnumerable<SearchDTO>? filter = null, IEnumerable<OrderByDTO>? orderBy = null, int? skip = null, int? take = null)
        {
            KeyValuePair<string, string>[] parameters = {
                new KeyValuePair<string, string>(nameof(filter), JsonConvert.SerializeObject(filter)),
                new KeyValuePair<string, string>(nameof(orderBy), JsonConvert.SerializeObject(orderBy)),
                new KeyValuePair<string, string>(nameof(skip), skip.ToString()),
                new KeyValuePair<string, string>(nameof(take), take.ToString()) };

            HttpResponseMessage response = await GetAsync(ReadAddress, parameters).ConfigureAwait(false);
            return await response.DeserializeOrDefaultAsync<(IEnumerable<TEntity> value, int count)>().ConfigureAwait(false);
        }
    }
}