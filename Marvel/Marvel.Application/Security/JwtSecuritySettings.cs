
namespace Marvel.Application.Security
{
    public class JwtSecuritySettings
    {
        public string? Secret { get; set; }

        public string? Issuer { get; set; }

        public string? Audience { get; set; }

        public int ExpireTimeHours { get; set; }
    }
}
