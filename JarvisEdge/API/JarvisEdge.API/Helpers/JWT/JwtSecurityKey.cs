﻿namespace JarvisEdge.API.Helpers.JWT
{
    using Microsoft.IdentityModel.Tokens;
    using System.Text;

    public static class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
