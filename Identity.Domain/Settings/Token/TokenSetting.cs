namespace Identity.Domain.Settings
{
    public sealed class TokenSetting : BaseSetting, ITokenSetting
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }

    /// <summary>
    /// Özelleştirilmiş token ayarlarını içeren sınıftır.
    /// </summary>
    public interface ITokenSetting
    {
        /// <summary>
        /// Dinleyiciler
        /// </summary>
        string Audience { get; set; }

        /// <summary>
        /// Dağıtıcılar
        /// </summary>
        string Issuer { get; set; }

        /// <summary>
        /// Access Token'nın bitme süresi.
        /// </summary>
        int AccessTokenExpiration { get; set; }

        /// <summary>
        /// Refresh Token'nın bitme süresi.
        /// </summary>
        int RefreshTokenExpiration { get; set; }

        /// <summary>
        /// Token üretilirken kullanılacak olan güvenli amaçlı anahtarımız.
        /// </summary>
        string SecurityKey { get; set; }
    }
}
