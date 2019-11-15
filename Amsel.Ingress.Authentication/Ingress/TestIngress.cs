using System.Net.Http;
using System.Threading.Tasks;
using Amsel.Framework.Infrastruktur.Application.Interfaces;
using Amsel.Framework.Infrastruktur.Application.Models.Address;
using Amsel.Framework.Infrastruktur.Application.Service;
using JetBrains.Annotations;

namespace Amsel.Ingress.Authentication.Ingress
{
    public class TestIngress : GenericIngress
    {
        [NotNull] public static readonly APIAddress AnonymousURL = new APIAddress("auth", "/test", "/Anonymous");
        [NotNull] public static readonly APIAddress AuthorizedURL = new APIAddress("auth", "/test", "/Authorized");

        #region  CONSTRUCTORS

        public TestIngress() { }

        public TestIngress(IAuthService authenticationService) : base(authenticationService) { }

        #endregion

        public async Task<string> GetAnonymousTest() {
            HttpResponseMessage response = await GetAsync(AnonymousURL);
            return await response?.Content?.ReadAsStringAsync();
        }

        public async Task<string> GetAuthorizedTest() {
            HttpResponseMessage response = await GetAsync(AuthorizedURL);
            return await response?.Content?.ReadAsStringAsync();
        }
    }
}