using Amsel.Framework.Structure.Factory;
using Amsel.Framework.Structure.Interfaces;
using Amsel.Framework.Structure.Services;
using JetBrains.Annotations;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Amsel.Access.Authentication.Services {
    public class TestAccess : GenericAccess
    {
        [NotNull] public static readonly UriBuilder AnonymousURL = UriBuilderFactory.GetAPIBuilder("auth", "/test", "/Anonymous");
        [NotNull] public static readonly UriBuilder AuthorizedURL = UriBuilderFactory.GetAPIBuilder("auth", "/test", "/Authorized");

        public TestAccess()
        {
        }
        public TestAccess(IAuthenticationService authenticationService) : base(authenticationService)
        {
        }

        #region public methods
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
    }

    public class TestServiceAccess : TestAccess
    {
        public TestServiceAccess(ISecretAuthenticationService authentication) : base(authentication)
        {
        }
    }
}