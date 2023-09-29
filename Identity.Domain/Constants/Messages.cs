namespace Identity.Domain.Constants
{
    public static class MessageTitle
    {
    }

    public static class MessageText
    {
        public const string SignInFailed = "E-posta veya şifre bilgisi yanlıştır !";
        public const string LockoutEndDateFaided = "Art arda 3 başarısız giriş denemesi yaptığınızdan dolayı hesabınız 1 dk kitlenmiştir !";
        public const string UserNotFoundByEmail = "Email bilgisine göre kullanıcı bulunamadı !";
        public const string UserNotFoundByUserName = "Kullanıcı adına göre kullanıcı bulunamadı !";
        public const string PhoneNumberInUse = "Girilen telefon numarası zaten kullanılmaktadır !";
    }
}
