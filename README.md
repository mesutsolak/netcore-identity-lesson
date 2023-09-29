# .NET Core 3.1 Identity Server

Identity Server öncesi MemberShip üyelik yönetimi kullanılmaktadır.

Identity Server'ı ekleyebilmek için aşağıdaki kod bloğunu kullanabiliriz:

 `builder.Services.AddIdentity<AppUser, AppRole>(options =>
 {
    options.User.RequireUniqueEmail = false;
})`


Identity kod bloğunun eklenmesiyle birlikte çeşitli ayarlamalarda default olarak gelmektedir.
Identity Server'ı projemizde kullanabilmek için zorunlu olarak SSL sertifikası bulunması gerekmektedir (Https)

`User.Identity.IsAuthenticated` : Bir kullanıcının giriş yapıp yapmadığını bilgisine erişebiliriz.
`User.Identity.Name` : Giriş yapmış kullanıcının kullanıcı adı bilgisi alabilmekteyiz.


## Cookie Ayarlaması

.NET identity ile geliştiren bir uygulamada login olduktan sonra LoginPath , LogoutPath , AccessDeniedPath vb gibi bilgilerin otomatik ayarların atandığı bir cookie oluşturulur.Bu cookie ".AspNetCore.Identity.Application" ismiyle oluşturulur.Örnek bir veri vermem gerekirse LoginPath değeri 'Account/Login' olarak ayarlanmaktadır.

![Identity-2](https://github.com/mesutsolak/netcore-identity-lesson/assets/56551511/8043700f-571c-4ca2-a2c8-08be61443f35)

Oluşturulan cookie her istek de bulunulduğu zaman .net core sistemi üzerinde çözülmektedir.Cookie çözüldüğü zaman kullanıcının authentication ve authorization adımlarını kontrol etmek de ve ona göre yönlendirme yapmaktadır.

Oluşturulan bu cookie özelliğini kod içerisinde özelleştirebilmekteyiz.Örneğin LoginPath url adresini değiştirerek kullanıcının login olmama durumlarda yönlendirilecek adresi belirleyebiliriz.Konuyla ilgili daha detaylı bilgiyi ConfigureApplicationCookie metodu üzerinden bulabiliriz.

Bunların dışında identity kullanmadan özelleştirilmiş login olma işlemi yapabilmek için aşağıdaki kod bloğuyla işlem yapabiliriz.

`builder.Services.AddAuthentication().AddCookie()`

## Authentication & Authorization

Identity Server üzerinde bir kullanıcının giriş yapıp yapmadığını ve istek de bulunduğu sayfaya yetkisinin olup olmadığını anlayabilmek için [Authorize] özelliğini kullanaibliriz.
[Authorize] özelliğini kullanabilmek için aşağıdaki kod bloklarını 'sırasıyla' eklememiz gerekmektedir.

İlk önce IServiceCollection interfaceni kullanarak özelliklerimizi ekliyoruz.

`builder.Services.AddAuthentication();`

`builder.Services.AddAuthorization();`

Daha sonra eklemiş olduğumuz özellikleri IApplicationBuilder interface'i yardımıyla aşağıda sırasıyla ekliyoruz.

`app.UseAuthentication();`

`app.UseAuthorization();`

## Özelleştirmek

Identity Server projesinde kullanıcı yönetimi ve token işlemleri 2 farklı işlem olarak ele alınmaktadır.Bu projede sadece identiy üzerine yoğunlaşıldı.
İşlemlerimizi gerçekleştirebilmek için Identity'nin hazır metotlarını ve sınıflarını kullanabilirken özelleştirerek de bunları projedemizde kullanabiliriz.
Örnek olarak vermemiz gerekirse IdentityUser sınıfını miras alarak yeni bir sınıf oluşturabiliriz.


## Kullanılan Nugetler

### Mapster

Mapster sınıflar arasında dönüşüm yapmamızı sağlayan automapper'dan daha hafif bir kütüphanedir.İsimleri aynı olan propertyleri otomatik olarak çevirmektedir.Automapper gibi profile tanımlaması zorunlu olmadan direk olarak extension üzerinden kullanabiliriz.

## Kurallar

* Identity Server üzerinde **Username** alanı otomatik olarak unique tanımlanmaktadır.Örneğin Email adresini AddIdentity dediğimiz alanda unique olması gerektiğini tanımlarken telefon numarası için bir bilgi bulunmamaktadır.Username alanı aynı adla birden fazla kez tanımlanmaya çalışılırsa hata fırlatmaktadır.

## Proje Bilgileri

Katmanlarımızı Core , Data , Domain ve WebUI olmak üzere 4 parçadan oluşmaktadır.

Identity Server mekanizmasının bize sunmuş olduğu hazır metotlar kullanıldığı için kullanıcı işlemlerinde repository pattern kullanılmamıştır.

Kullanılan **_Service_** ismi external olarak kullanılan servislerde geçerlidir.

Kullanılan **_Helper / Handler_** ismi kullanılan nuget paketlerini daha kolay ve aktif bir şekilde kullanılmak için geçerlidir.

Kullanılan **_Repository_** ismi var olan **entity** sınıflarına daha kolay ulaşabilmek için geçerlidir.

Kullanılan **_Manager_** ismi var olan **repository** sınıflarına daha kolay ulaşabilmek için geçerlidir.

## Sunulan Özellikler

* Ad Yazdırma
* Login Olma
* Logout Olma
* Rol Bazlı Yetkilendirme
* Claim Bazlı Yetkilendirme
* Kullanıcı İşlemleri
* Üçüncü Taraf Kimlik Doğrulaması (Google Authenticator veya LastPass gibi)

## Kaynaklar

* https://www.tektutorialshub.com/asp-net-core/asp-net-core-identity-tutorial/
* https://www.gencayyildiz.com/blog/asp-net-core-identity-yazi-dizisi/
* https://furkan-dvlp.medium.com/net-core-web-api-ile-identity-authentication-8b772bc9f823
* https://medium.com/@ajidejibola/authentication-and-authorization-in-net-6-with-jwt-and-asp-net-identity-2566e75851fe
* https://semihcelikol.com/net-core-web-api-jwt-identity-kullanimi/
* https://medium.com/@f.cakiroglu16/asp-net-core-cookie-authentication-identity-olmadan-1-18d6ff4db857

## Kurslar

* Fatih Çakıroğlu : Asp.Net Core Üyelik Sistemi+Token Bazlı Kimlik Doğrulama