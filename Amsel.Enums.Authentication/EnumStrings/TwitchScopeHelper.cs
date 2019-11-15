using System;
using System.Collections.Generic;
using System.Text;
using Amsel.Enums.Authentication.Enums;
using JetBrains.Annotations;

namespace Amsel.Enums.Authentication.EnumStrings
{
    public static class TwitchScopeHelper
    {
        public static ETwitchScope GetScopes([NotNull] this IList<string> scopeList) {
            if (scopeList == null) throw new ArgumentNullException(nameof(scopeList));
            ETwitchScope scope = ETwitchScope.DEFAULT;

            if (scopeList.Contains("channel_check_subscription"))
                scope |= ETwitchScope.CHANNEL_CHECK_SUBSCRIPTION;
            if (scopeList.Contains("channel_commercial"))
                scope |= ETwitchScope.CHANNEL_COMMERCIAL;
            if (scopeList.Contains("channel_editor"))
                scope |= ETwitchScope.CHANNEL_EDITOR;
            if (scopeList.Contains("channel_feed_edit"))
                scope |= ETwitchScope.CHANNEL_FEED_EDIT;
            if (scopeList.Contains("channel_feed_read"))
                scope |= ETwitchScope.CHANNEL_FEED_READ;
            if (scopeList.Contains("channel_read"))
                scope |= ETwitchScope.CHANNEL_READ;
            if (scopeList.Contains("channel_stream"))
                scope |= ETwitchScope.CHANNEL_STREAM;
            if (scopeList.Contains("channel_subscriptions"))
                scope |= ETwitchScope.CHANNEL_SUBSCRIPTIONS;
            if (scopeList.Contains("collections_edit"))
                scope |= ETwitchScope.COLLECTIONS_EDIT;
            if (scopeList.Contains("communities_edit"))
                scope |= ETwitchScope.COMMUNITIES_EDIT;
            if (scopeList.Contains("communities_moderate"))
                scope |= ETwitchScope.COMMUNITIES_MODERATE;
            if (scopeList.Contains("user_blocks_edit"))
                scope |= ETwitchScope.USER_BLOCKS_EDIT;
            if (scopeList.Contains("user_blocks_read"))
                scope |= ETwitchScope.USER_BLOCKS_READ;
            if (scopeList.Contains("user_follows_edit"))
                scope |= ETwitchScope.USER_FOLLOWS_EDIT;
            if (scopeList.Contains("user_read"))
                scope |= ETwitchScope.USER_READ;
            if (scopeList.Contains("user_subscriptions"))
                scope |= ETwitchScope.USER_SUBSCRIPTIONS;
            if (scopeList.Contains("viewing_activity_read"))
                scope |= ETwitchScope.VIEWING_ACTIVITY_READ;
            if (scopeList.Contains("openid"))
                scope |= ETwitchScope.OPENID;
            if (scopeList.Contains("user:edit"))
                scope |= ETwitchScope.HELIX_USER_EDIT;
            if (scopeList.Contains("user:read:email"))
                scope |= ETwitchScope.HELIX_USER_READ_EMAIL;
            if (scopeList.Contains("clips:edit"))
                scope |= ETwitchScope.HELIX_CLIPS_EDIT;
            if (scopeList.Contains("analytics:read:games"))
                scope |= ETwitchScope.HELIX_ANALYTICS_READ_GAMES;
            if (scopeList.Contains("analytics:read:extensions"))
                scope |= ETwitchScope.HELIX_ANALYTICS_READ_EXTENTIONS;
            if (scopeList.Contains("bits:read"))
                scope |= ETwitchScope.HELIX_BITS_READ;
            if (scopeList.Contains("user:edit:broadcast"))
                scope |= ETwitchScope.HELIX_USER_EDIT_BROADCAST;
            if (scopeList.Contains("user:read:broadcast"))
                scope |= ETwitchScope.HELIX_USER_READ_BROADCAST;
            if (scopeList.Contains("channel:read:subscriptions"))
                scope |= ETwitchScope.HELIX_CHANNEL_READ_SUBSCRIPTIONS;
            if (scopeList.Contains("channel:moderate"))
                scope |= ETwitchScope.PUBSUB_CHANNEL_MODERATE;
            if (scopeList.Contains("chat:edit"))
                scope |= ETwitchScope.PUBSUB_CHAT_EDIT;
            if (scopeList.Contains("chat:read"))
                scope |= ETwitchScope.PUBSUB_CHAT_READ;
            if (scopeList.Contains("whispers:read"))
                scope |= ETwitchScope.PUBSUB_WHISPERS_READ;
            if (scopeList.Contains("whispers:edit"))
                scope |= ETwitchScope.PUBSUB_WHISPERS_EDIT;

            return scope;
        }

        public static string ToScopeString(this ETwitchScope scope) {
            StringBuilder builder = new StringBuilder();
            if (scope.HasFlag(ETwitchScope.CHANNEL_CHECK_SUBSCRIPTION))
                builder.Append("channel_check_subscription ");
            if (scope.HasFlag(ETwitchScope.CHANNEL_COMMERCIAL))
                builder.Append("channel_commercial ");
            if (scope.HasFlag(ETwitchScope.CHANNEL_EDITOR))
                builder.Append("channel_editor ");
            if (scope.HasFlag(ETwitchScope.CHANNEL_FEED_EDIT))
                builder.Append("channel_feed_edit ");
            if (scope.HasFlag(ETwitchScope.CHANNEL_FEED_READ))
                builder.Append("channel_feed_read ");
            if (scope.HasFlag(ETwitchScope.CHANNEL_READ))
                builder.Append("channel_read ");
            if (scope.HasFlag(ETwitchScope.CHANNEL_STREAM))
                builder.Append("channel_stream ");
            if (scope.HasFlag(ETwitchScope.CHANNEL_SUBSCRIPTIONS))
                builder.Append("channel_subscriptions ");
            if (scope.HasFlag(ETwitchScope.COLLECTIONS_EDIT))
                builder.Append("collections_edit ");
            if (scope.HasFlag(ETwitchScope.COMMUNITIES_EDIT))
                builder.Append("communities_edit ");
            if (scope.HasFlag(ETwitchScope.COMMUNITIES_MODERATE))
                builder.Append("communities_moderate ");
            if (scope.HasFlag(ETwitchScope.USER_BLOCKS_EDIT))
                builder.Append("user_blocks_edit ");
            if (scope.HasFlag(ETwitchScope.USER_BLOCKS_READ))
                builder.Append("user_blocks_read ");
            if (scope.HasFlag(ETwitchScope.USER_FOLLOWS_EDIT))
                builder.Append("user_follows_edit ");
            if (scope.HasFlag(ETwitchScope.USER_READ))
                builder.Append("user_read ");
            if (scope.HasFlag(ETwitchScope.USER_SUBSCRIPTIONS))
                builder.Append("user_subscriptions ");
            if (scope.HasFlag(ETwitchScope.VIEWING_ACTIVITY_READ))
                builder.Append("viewing_activity_read ");
            if (scope.HasFlag(ETwitchScope.OPENID))
                builder.Append("openid ");
            if (scope.HasFlag(ETwitchScope.HELIX_USER_EDIT))
                builder.Append("user:edit ");
            if (scope.HasFlag(ETwitchScope.HELIX_USER_READ_EMAIL))
                builder.Append("user:read:email ");
            if (scope.HasFlag(ETwitchScope.HELIX_CLIPS_EDIT))
                builder.Append("clips:edit ");
            if (scope.HasFlag(ETwitchScope.HELIX_ANALYTICS_READ_GAMES))
                builder.Append("analytics:read:games ");
            if (scope.HasFlag(ETwitchScope.HELIX_ANALYTICS_READ_EXTENTIONS))
                builder.Append("analytics:read:extensions ");
            if (scope.HasFlag(ETwitchScope.HELIX_BITS_READ))
                builder.Append("bits:read ");
            if (scope.HasFlag(ETwitchScope.HELIX_USER_EDIT_BROADCAST))
                builder.Append("user:edit:broadcast ");
            if (scope.HasFlag(ETwitchScope.HELIX_USER_READ_BROADCAST))
                builder.Append("user:read:broadcast ");
            if (scope.HasFlag(ETwitchScope.HELIX_CHANNEL_READ_SUBSCRIPTIONS))
                builder.Append("channel:read:subscriptions ");
            if (scope.HasFlag(ETwitchScope.PUBSUB_CHANNEL_MODERATE))
                builder.Append("channel:moderate ");
            if (scope.HasFlag(ETwitchScope.PUBSUB_CHAT_EDIT))
                builder.Append("chat:edit ");
            if (scope.HasFlag(ETwitchScope.PUBSUB_CHAT_READ))
                builder.Append("chat:read ");
            if (scope.HasFlag(ETwitchScope.PUBSUB_WHISPERS_READ))
                builder.Append("whispers:read ");
            if (scope.HasFlag(ETwitchScope.PUBSUB_WHISPERS_EDIT))
                builder.Append("whispers:edit ");
            return builder.ToString().Trim();
        }
    }
}