namespace PetFamily.Accounts.Infrastructure.Options
{
    public class JwtOptions
    {
        public const string JWT = nameof(JWT);
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty ;
        public string Key { get; set; } = string.Empty;
        public string ExpiredMinutesTime { get; set; }
    }
}
