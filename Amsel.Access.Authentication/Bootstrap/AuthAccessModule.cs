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
            builder.RegisterType<AuthAccess>();
            builder.RegisterType<TestAccess>();
            builder.RegisterType<AccountAccess>();

            base.Load(builder);
        }
    }
}