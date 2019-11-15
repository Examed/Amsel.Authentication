using Amsel.Enums.Authentication.Enums;

namespace Amsel.Enums.Authentication.EnumStrings
{
    public static class ClaimTypesHelper
    {
        public static string ToEnumString(this EClaimTypes claim) {
            switch (claim) {
                case EClaimTypes.TWITCH_ID:
                    return "TwitchId";

                case EClaimTypes.TWITCH_TOKEN:
                    return "TwitchToken";

                case EClaimTypes.TENANT_BANNED:
                    return "TenantBanned";

                case EClaimTypes.TENANT_MODERATOR:
                    return "TenantModerator";

                case EClaimTypes.TENANT_EDITOR:
                    return "TenantEditor";

                default:
                    return string.Empty;
            }
        }
    }
}