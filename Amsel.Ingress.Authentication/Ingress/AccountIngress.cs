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
    public class AccountIngress : GenericIngress
    {
        [NotNull] private static readonly APIAddress AllAccountURL = new APIAddress(AuthEndpointResources.ENDPOINT,
                                                                                    AuthEndpointResources.ACCOUNT,
                                                                                    AccountControllerResources.GET_ALL);

        #region  CONSTRUCTORS

        public AccountIngress(IAuthService authenticationService) : base(authenticationService) { }

        #endregion

        [NotNull]
        public async Task<IEnumerable<AccountDTO>> GetAllAsync() {
            HttpResponseMessage response = await GetAsync(AllAccountURL);
            return await response.DeserializeOrDefaultAsync<IEnumerable<AccountDTO>>();
        }
    }
}