using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Amsel.Enums.Authentication.Enums
{
    public static class AuthRoles
    {
        [NotNull]
        public static IEnumerable<string> GetPaymentPolicy(this ESubscriptionPolicy role)
        {
            List<string> result = new List<string> { role.ToString(), nameof(ESubscriptionPolicy.NONE) };

            if (!role.HasFlag(ESubscriptionPolicy.TIER_1))
                result.Add(nameof(ESubscriptionPolicy.TIER_1));
            if (!role.HasFlag(ESubscriptionPolicy.TIER_2))
                result.Add(nameof(ESubscriptionPolicy.TIER_2));
            if (!role.HasFlag(ESubscriptionPolicy.TIER_3))
                result.Add(nameof(ESubscriptionPolicy.TIER_3));
            if (!role.HasFlag(ESubscriptionPolicy.PREMIUM))
                result.Add(nameof(ESubscriptionPolicy.PREMIUM));

            return result;
        }

        [NotNull]
        public static IEnumerable<string> GetRoles(this ERoles role)
        {
            List<string> result = new List<string>();

            if (role.HasFlag(ERoles.VIEWER))
                result.Add(nameof(ERoles.VIEWER));
            if (role.HasFlag(ERoles.MODERATOR))
                result.Add(nameof(ERoles.MODERATOR));
            if (role.HasFlag(ERoles.EDITOR))
                result.Add(nameof(ERoles.EDITOR));
            if (role.HasFlag(ERoles.SERVICE))
                result.Add(nameof(ERoles.SERVICE));
            if (role.HasFlag(ERoles.ADMIN))
                result.Add(nameof(ERoles.ADMIN));

            return result;
        }

        public static bool HasPaymentPolicy(this ESubscriptionPolicy policy, IEnumerable<Claim> claims)
        {
            if (policy == ESubscriptionPolicy.NONE)
                return true;

            Claim claim = claims?.FirstOrDefault(x => x.Type.Equals(nameof(EClaimTypes.SUBSCRIPTION_LEVEL)));
            if (claim == null)
                return false;

            Enum.TryParse(claim.Value, out ESubscriptionPolicy value);
            return value.HasFlag(policy);
        }


        [Flags]
        public enum ERoles
        {
            VIEWER = 0,
            MODERATOR = VIEWER | (1 << 0),
            EDITOR = VIEWER | MODERATOR | (1 << 1),
            STREAMER = EDITOR | (1 << 2),
            SERVICE = STREAMER | (1 << 3),
            ADMIN = SERVICE | (1 << 4)
        }


        public enum ESubscriptionPolicy
        {
            NONE = 0,
            TIER_1 = NONE | (1 << 0),
            TIER_2 = TIER_1 | (1 << 1),
            TIER_3 = TIER_2 | (1 << 2),
            PREMIUM = TIER_3 | (1 << 3)
        }
    }
}