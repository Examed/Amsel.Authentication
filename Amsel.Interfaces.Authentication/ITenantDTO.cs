using Amsel.DTO.Authentication.Models;

namespace Amsel.Interfaces.Authentication
{
    public interface ITenantDTO
    {
        TenantDTO Tenant { get; set; }
    }
}