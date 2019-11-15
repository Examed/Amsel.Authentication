﻿using System;

namespace Amsel.Enums.Authentication.Enums
{
    [Flags]
    public enum ETwitchScope
    {
        CHANNEL_CHECK_SUBSCRIPTION = 1 << 1,
        CHANNEL_COMMERCIAL = 1         << 2,
        CHANNEL_EDITOR = 1             << 3,
        CHANNEL_FEED_EDIT = 1          << 4,
        CHANNEL_FEED_READ = 1          << 5,
        CHANNEL_READ = 1               << 6,
        CHANNEL_STREAM = 1             << 7,
        CHANNEL_SUBSCRIPTIONS = 1      << 8,

        COLLECTIONS_EDIT = 2 << 1,

        COMMUNITIES_EDIT = 3     << 1,
        COMMUNITIES_MODERATE = 3 << 2,

        USER_BLOCKS_EDIT = 4   << 3,
        USER_BLOCKS_READ = 4   << 4,
        USER_FOLLOWS_EDIT = 4  << 5,
        USER_READ = 4          << 6,
        USER_SUBSCRIPTIONS = 4 << 7,

        VIEWING_ACTIVITY_READ = 5 << 1,

        OPENID = 6 << 1,

        HELIX_USER_EDIT = 7                  << 1,
        HELIX_USER_READ_EMAIL = 7            << 2,
        HELIX_CLIPS_EDIT = 7                 << 3,
        HELIX_BITS_READ = 7                  << 4,
        HELIX_ANALYTICS_READ_GAMES = 7       << 5,
        HELIX_ANALYTICS_READ_EXTENTIONS = 7  << 6,
        HELIX_CHANNEL_READ_SUBSCRIPTIONS = 7 << 7,
        HELIX_USER_READ_BROADCAST = 7        << 8,
        HELIX_USER_EDIT_BROADCAST = 7        << 9,

        PUBSUB_CHANNEL_MODERATE = 8 << 1,
        PUBSUB_CHAT_EDIT = 8        << 2,
        PUBSUB_CHAT_READ = 8        << 3,
        PUBSUB_WHISPERS_READ = 8    << 4,
        PUBSUB_WHISPERS_EDIT = 8    << 5,

        DEFAULT = USER_READ                                                         | USER_SUBSCRIPTIONS,
        CHAT = DEFAULT | PUBSUB_CHAT_EDIT | PUBSUB_CHAT_READ | PUBSUB_WHISPERS_EDIT | PUBSUB_WHISPERS_READ,

        PUBSUB = PUBSUB_CHANNEL_MODERATE | PUBSUB_CHAT_EDIT | PUBSUB_CHAT_READ | PUBSUB_WHISPERS_READ |
                 PUBSUB_WHISPERS_EDIT,

        HELIX = HELIX_USER_EDIT            | HELIX_USER_READ_EMAIL | HELIX_CLIPS_EDIT |
                HELIX_BITS_READ            |
                HELIX_ANALYTICS_READ_GAMES | HELIX_ANALYTICS_READ_EXTENTIONS | HELIX_USER_EDIT_BROADCAST |
                HELIX_USER_READ_BROADCAST,

        USER = USER_READ | USER_BLOCKS_EDIT | USER_BLOCKS_READ | USER_FOLLOWS_EDIT | USER_SUBSCRIPTIONS,
        COMMUNITIES = COMMUNITIES_EDIT                                             | COMMUNITIES_MODERATE,

        CHANNEL = CHANNEL_CHECK_SUBSCRIPTION | CHANNEL_COMMERCIAL | CHANNEL_EDITOR | CHANNEL_FEED_EDIT |
                  CHANNEL_FEED_READ          | CHANNEL_STREAM     | CHANNEL_SUBSCRIPTIONS,

        ALL = DEFAULT | HELIX | USER | CHANNEL | COMMUNITIES | OPENID | CHAT | VIEWING_ACTIVITY_READ | COLLECTIONS_EDIT
    }
}