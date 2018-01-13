namespace JarvisEdge.API.Helpers.JWT
{
    public static class JwtConstants
    {
        private const string jarvisTokenValidation = "Jarvis.Security.Bearer";
        private const string jarvisSigningKey = "@jarvis-security";

        public static string GetIssuer()
        {
            return jarvisTokenValidation;
        }

        public static string GetAudience()
        {
            return jarvisTokenValidation;
        }

        public static string GetSigningKey()
        {
            return jarvisSigningKey;
        }
    }
}
