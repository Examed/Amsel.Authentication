using Amsel.Framework.Infrastruktur.Application.Interfaces;
using Amsel.Framework.Infrastruktur.Application.Service;

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