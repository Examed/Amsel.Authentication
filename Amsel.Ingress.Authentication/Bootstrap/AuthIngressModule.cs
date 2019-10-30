using Amsel.Ingress.Authentication.Ingress;
using Autofac;

namespace Amsel.Ingress.Authentication.Bootstrap
{
    /// <inheritdoc />
    public class AuthIngressModule : Module
    {
        #region PROTECTED METHODES

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TenantIngress>();
            builder.RegisterType<AuthIngress>();
            builder.RegisterType<TestIngress>();
            builder.RegisterType<AccountIngress>();

            base.Load(builder);
        }

        #endregion
    }
}