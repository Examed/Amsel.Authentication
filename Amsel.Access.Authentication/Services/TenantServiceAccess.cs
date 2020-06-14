using Amsel.Access.Tenant.Services;
using Amsel.Framework.Structure.Interfaces;

namespace Amsel.Access.Authentication.Services {
    public class TenantServiceAccess : TenantAccess {
        public TenantServiceAccess(ISecretAuthenticationService authentication) : base(authentication) { }
    }
}