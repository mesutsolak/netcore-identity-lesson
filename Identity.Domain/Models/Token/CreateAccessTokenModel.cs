namespace Identity.Domain.Models.Token
{
    public sealed class CreateAccessTokenModel : BaseModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
