using System.ComponentModel.DataAnnotations;

namespace Identity.WebUI.ViewModels.Authorize
{
    public sealed class LoginViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Lütfen e-posta adresini boş geçmeyiniz.")]
        //Data type özelliği html elementleri oluştururken html elementin tipinin EmailAddress olmasını sağlamaktadır.
        //Client uygulamalar için kullanılmaktadır api uygulamaları için gerek yoktur.
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen uygun formatta e-posta adresi giriniz.")]
        [Display(Name = "E-Posta ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifreyi boş geçmeyiniz.")]
        [DataType(DataType.Password, ErrorMessage = "Lütfen uygun formatta şifre giriniz.")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        /// <summary>
        /// Beni hatırla...
        /// </summary>
        [Display(Name = "Beni Hatırla")]
        public bool Persistent { get; set; }
    }
}
