namespace Identity.Domain.Models.Authentication
{
    public sealed class RefreshTokenModel : BaseModel
    {
        public string RefreshToken { get; set; }
    }
}
