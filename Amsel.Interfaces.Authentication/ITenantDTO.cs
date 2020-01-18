using System;

namespace Amsel.DTO.Authentication.Models
{
    public interface ITenantDTO
    {
        TenantDTO Tenant { get; set; }
    }
}