namespace Identity.WebUI.Security.Token
{
    /// <summary>
    /// Özelleştirilmiş token ayarlarını içeren sınıftır.
    /// </summary>
    public class CustomTokenOptions
    {
        /// <summary>
        /// Dinleyiciler
        /// </summary>
        public string Audience { get; set; }
        
        /// <summary>
        /// Dağıtıcılar
        /// </summary>
        public string Issuer { get; set; }
        
        /// <summary>
        /// Access Token'nın bitme süresi.
        /// </summary>
        public int AccessTokenExpiration { get; set; }

        /// <summary>
        /// Refresh Token'nın bitme süresi.
        /// </summary>
        public int RefreshTokenExpiration { get; set; }

        /// <summary>
        /// Token üretilirken kullanılacak olan güvenli amaçlı anahtarımız.
        /// </summary>
        public string SecurityKey { get; set; }
    }
}
