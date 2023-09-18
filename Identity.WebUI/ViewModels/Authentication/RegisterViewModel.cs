namespace Identity.WebUI.ViewModels.Authentication
{
    public sealed class RegisterViewModel : BaseViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
