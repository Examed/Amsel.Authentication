using Amsel.Access.Authentication.Services;
using Autofac;

namespace Amsel.Access.Authentication.Bootstrap
{
    /// <inheritdoc/>
    public class AuthAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TenantAccess>();
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