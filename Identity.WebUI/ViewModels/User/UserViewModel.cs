using System;
using System.ComponentModel.DataAnnotations;

namespace Identity.WebUI.ViewModels.User
{
    public sealed class UserViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Lütfen kullanıcı adını boş geçmeyiniz...")]
        [StringLength(15, ErrorMessage = "Lütfen kullanıcı adını 4 ile 15 karakter arasında giriniz...", MinimumLength = 4)]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [RegularExpression(@"^(0(\d{3})) (\d{3}) (\d{2}) (\d{2})$", ErrorMessage = "Telefon numarası uygun formatta değil")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Lütfen emaili boş geçmeyiniz...")]
        [EmailAddress(ErrorMessage = "Lütfen email formatında bir değer belirtiniz...")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifreyi boş geçmeyiniz...")]
        [DataType(DataType.Password, ErrorMessage = "Lütfen şifreyi tüm kuralları göz önüne alarak giriniz...")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Picture { get; set; }
        public string City { get; set; }
        public Gender Gender { get; set; }
    }
}
