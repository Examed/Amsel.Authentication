using Amsel.Framework.Structure.Client.Service;
using Amsel.Framework.Structure.Interfaces;
using Amsel.Framework.Structure.Service;

namespace Amsel.Access.Authentication.Services
{
    public class AuthAccess : GenericAccess
    {
        #region  CONSTRUCTORS

        public AuthAccess() { }

        public AuthAccess(IAuthenticationService authenticationService) : base(authenticationService) { }

        #endregion
    }
}