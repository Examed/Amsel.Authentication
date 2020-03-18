using Amsel.Model.Authentication.AccountModels;
using Amsel.Model.Tenant.TenantModels;
using AutoMapper;

namespace Amsel.Endpoint.Logging.Utilities.Mapping.DTO
{
    public class AuthenticationDTOMap : Profile
    {
        public AuthenticationDTOMap()
        {
            CreateMap<Account, TenantEntity>();
        }

    }
}