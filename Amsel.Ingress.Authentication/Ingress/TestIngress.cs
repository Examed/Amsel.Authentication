﻿using System.Net.Http;
using System.Threading.Tasks;
using Amsel.Framework.Infrastruktur.Application.Interfaces;
using Amsel.Framework.Infrastruktur.Application.Models.Address;
using Amsel.Framework.Infrastruktur.Application.Service;
using JetBrains.Annotations;

namespace Amsel.Ingress.Authentication.Ingress
{
    public class TestIngress : GenericIngress
    {
        public async Task<string> GetAnonymousTestAsync() {
            HttpResponseMessage response = await GetAsync(AnonymousURL).ConfigureAwait(false);
            return await (response?.Content?.ReadAsStringAsync()).ConfigureAwait(false);
        }

        public async Task<string> GetAuthorizedTestAsync() {
            HttpResponseMessage response = await GetAsync(AuthorizedURL).ConfigureAwait(false);
            return await (response?.Content?.ReadAsStringAsync()).ConfigureAwait(false);
        }

        #region STATICS, CONST and FIELDS

        [NotNull] public static readonly APIAddress AnonymousURL = new APIAddress("auth", "/test", "/Anonymous");
        [NotNull] public static readonly APIAddress AuthorizedURL = new APIAddress("auth", "/test", "/Authorized");

        #endregion

        #region  CONSTRUCTORS

        public TestIngress() { }

        public TestIngress(IAuthService authenticationService) : base(authenticationService) { }

        #endregion
    }
}