
using Amsel.Framework.Database.SQL.Models;
using Amsel.Model.Authentication.Account;
using AutoMapper;

namespace Amsel.Endpoint.Logging.Utilities.Mapping.DTO
{
    public class AuthenticationDTOMap : Profile
    {
        public AuthenticationDTOMap()
        {
            CreateMap<Account, Tenant>();
        }

    }
}