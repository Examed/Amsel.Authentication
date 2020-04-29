namespace Amsel.Resources.Authentication.Exceptions {
    public static class AuthControllerMessages
    {
        public const string BANNED = "The Account is Banned from the System";
        public const string CREATE_TOKEN = "Error while generating the Token";
        public const string GET_ACCOUNT = "Error while generating the Token";
        public const string GET_REFRESHTOKEN = "Error while fetching the RefreshToken";
        public const string START_SESSION_FROM_SESSION = "Its not possible to start a Session from a SessionToken";
        public const string TWITCH_AUTHURL = "Error while generating AuthURL";
    }
}