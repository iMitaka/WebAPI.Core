namespace JarvisEdge.Helpers.Jwt
{
    public static class JwtConstants
    {
        private const string jarvisIssuerValidation = "Jarvis.Security.Bearer.Issuer";
        private const string jarvisAudienceValidation = "Jarvis.Security.Bearer.Audience";
        private const string jarvisSigningKey = "@jarvis-security";

        public static string GetIssuer()
        {
            return jarvisIssuerValidation;
        }

        public static string GetAudience()
        {
            return jarvisAudienceValidation;
        }

        public static string GetSigningKey()
        {
            return jarvisSigningKey;
        }
    }
}
