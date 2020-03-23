using Amsel.Access.Authentication.Services;
using Amsel.Access.Tenant.Bootstrap;
using Autofac;

namespace Amsel.Access.Authentication.Bootstrap
{
    /// <inheritdoc/>
    public class AuthenticationAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new TenantAccessModule());
            builder.RegisterType<TenantServiceAccess>();
            builder.RegisterType<TestAccess>();
            builder.RegisterType<TestServiceAccess>();
            builder.RegisterType<KeyAccess>();
            builder.RegisterType<AccountAccess>();
            builder.RegisterType<AccountServiceAccess>();

            base.Load(builder);
        }
    }
}