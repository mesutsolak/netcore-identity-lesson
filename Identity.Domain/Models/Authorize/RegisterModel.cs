namespace Identity.Domain.Models.Authorize
{
    public sealed class RegisterModel : BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
