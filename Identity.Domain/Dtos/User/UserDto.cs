namespace Identity.Data.Dtos.User
{
    public sealed class UserDto : BaseDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
