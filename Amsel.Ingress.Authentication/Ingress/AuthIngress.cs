using Amsel.Framework.Structure.Interfaces;
using Amsel.Framework.Structure.Service;

namespace Amsel.Ingress.Authentication.Ingress
{
    public class AuthIngress : GenericIngress
    {
        #region  CONSTRUCTORS

        public AuthIngress() { }

        public AuthIngress(IAuthService authenticationService) : base(authenticationService) { }

        #endregion
    }
}