using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace VironIT_Social_network_server.WEB.Identity.JWT
{
    public class JwtOptions
    {
        public const string Issuer = "http://localhost:5001/";
        public const string Audience = "http://localhost:4200/";
        public const int Lifetime = 30;
        public const string Secret = "AL1ghtFr0mTh3Shad0wShallSpr1ng";
    }
}
