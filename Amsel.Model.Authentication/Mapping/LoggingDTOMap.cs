using Amsel.Model.Authentication.AccountModels;
using Amsel.Model.Tenant.TenantModels;
using AutoMapper;

namespace Amsel.Model.Authentication.Mapping
{
    public class AuthenticationMap : Profile
    {
        public AuthenticationMap() => CreateMap<Account, TenantEntity>();
    }
}