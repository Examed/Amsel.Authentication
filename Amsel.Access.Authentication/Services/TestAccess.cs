using Amsel.Framework.Structure.Client.Service;
using Amsel.Framework.Structure.Interfaces;
using Amsel.Framework.Structure.Models.Address;
using JetBrains.Annotations;
using System.Net.Http;
using System.Threading.Tasks;

namespace Amsel.Access.Authentication.Services
{
    public class TestAccess : GenericAccess
    {
        #region PUBLIC METHODES
        public async Task<string> GetAnonymousTestAsync()
        {
            HttpResponseMessage response = await GetAsync(AnonymousURL).ConfigureAwait(false);
            return await (response?.Content?.ReadAsStringAsync()).ConfigureAwait(false);
        }

        public async Task<string> GetAuthorizedTestAsync()
        {
            HttpResponseMessage response = await GetAsync(AuthorizedURL).ConfigureAwait(false);
            return await (response?.Content?.ReadAsStringAsync()).ConfigureAwait(false);
        }
        #endregion

        #region STATICS, CONST and FIELDS

        [NotNull] public static readonly APIAddress AnonymousURL = new APIAddress("auth", "/test", "/Anonymous");
        [NotNull] public static readonly APIAddress AuthorizedURL = new APIAddress("auth", "/test", "/Authorized");

        #endregion

        #region  CONSTRUCTORS

        public TestAccess() { }

        public TestAccess(IAuthenticationService authenticationService) : base(authenticationService)
        {
        }
        #endregion
    }
}