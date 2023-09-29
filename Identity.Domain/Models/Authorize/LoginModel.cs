namespace Identity.Domain.Models.Authorize
{
    public sealed class LoginModel : BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Persistent { get; set; }
        public bool Lock { get; set; }
    }
}
