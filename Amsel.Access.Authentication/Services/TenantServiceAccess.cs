using Amsel.Framework.Structure.Interfaces;
using System;

namespace Amsel.Access.Authentication.Services
{
    public class TenantServiceAccess : TenantAccess
    {
        public TenantServiceAccess(ISecretAuthenticationService authentication) : base(authentication)
        {
        }
    }
}