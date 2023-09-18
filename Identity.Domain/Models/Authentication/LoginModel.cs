namespace Identity.Domain.Models.Authentication
{
    public sealed class LoginModel : BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
