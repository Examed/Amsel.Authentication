using System;

namespace Amsel.DTO.Authentication.Models
{
    public class TenantDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public string TenantName { get; set; }
    }
}