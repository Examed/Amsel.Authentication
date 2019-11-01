using System;

namespace Amsel.Enums.Authentication.Enums
{
    [Flags]
    public enum ETenantRights
    {
        MODERATOR = 1 << 1,
        EDITOR = MODERATOR | (1 << 2),
        BANNED = 1 << 3
    }
}