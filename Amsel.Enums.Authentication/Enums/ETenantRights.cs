using System;

namespace Amsel.Enums.Authentication.Enums
{
    [Flags]
    public enum ETenantRights
    {
        NONE = 0,
        BANNED = (1 << 0),
        MODERATOR = (1 << 1),
        EDITOR = MODERATOR | (1 << 2)
    }
}