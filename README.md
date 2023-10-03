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

JWT (Json Web Token) bir kimlik doğrulama yöntemiyken Identity bir kullanıcı yönetim kütüphanesidir.Kullanıcı yönetim kütüphanesi olması yanında basit bir kimlik doğrulama yöntemide sunmaktadır.
Identity ile basit kimlik doğrulama yapılmasına geleneksel yöntemde denir.JWT ve Identity birlikte kullanılabilmektedir.Projede kullanıp kullanılmaması projenin ihtiyacına göre değişebilir.
Genellikle JWT ve Identity'nin beraber kullanılması kompleks projelerde tercih edilebilir.

.net web uygulamada geleneksel yöntemi tercih edebiliriz.JWT token'da gönderebiliriz fakat bu kadar kompleks bir yapıya gerek yok.Cookie yöntemi hem basit hem de daha kullanışlıdır.Ajax'la ya da herhangi bir js kütüphanesiyle bearer token göndermeye gerek yoktur.Kullanıcı Identity ile giriş yaptıktan sonra oluşturulan cookie sürecinde oturumu açık kalacaktır..NET Core ayrıştırma işlemi yaptıktan sonra bu tokenın içerisindeki userId'ye sahip bir kullanıcı veritabanında varsa demekki authenticate kullanıcı anlamına yormaktadır.

Geleneksel kimlik doğrulamada kullanıcı adı ve parola üzerinden ilerlemektedir.Mobil uygulamalarda çerez mantığı olmadığı için yapılan her istekde username ve password gönderilmesi gerekiyordu.
İşte bu gereksiz bir külfet yarattığı için ve json web token'nın sunmuş olduğu iyi özellikler olduğu için json web token'ı tercih ediyoruz. 

Expire Date gibi konuları yine oluşturulan cookie üzerinden okuyabiliriz.

JWT Token değerini genellikle web api uygulamalarında kullanmaktayız.Bunu zaten JWT konulu bir repomda detaylı bir şekilde bahsetmek istiyorum.

[Authorize] özelliği boş bırakıldığı zaman klasik yani geleneksel yöntemi kullanacağımızı belli etmiş oluyoruz.Geleneksel yöntemi sadece Identity ile değil kendimizde manuel (custom) olarak yani
hiçbir kütüphane kullanmadan da gerçekleştirebiliriz.Authorize attribute kendi içerisinde şema alan bir yapıdadır.Bir web uygulaması briden fazla context ile çalışabilir.Bu contextlerin farklı token üretme mantıkları yani şemaları olabilr.Bu nedenle birden fazla context çalışırsa şema belirtmek gerekebilir.

Authorize özelliğini controller veya action seviyesinde yazabiliriz.Bu özellik sayesinde geçerli bir token gönderilmediği zaman otomatik olarak hata dönecektir.

Authorize özelliği içerisine 3 tane değer alabilmektedir;

Scheme:
AuthenticationSchemes : 
Örn: [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
Policy : 
Roles :

Şifreler veritabanında tutulurken mutlaka hashlenir güvenlik nedeniyle normal tutulmazlar.

Bunların dışında identity kullanmadan özelleştirilmiş login olma işlemi yapabilmek için aşağıdaki kod bloğuyla işlem yapabiliriz.

Claimler string olarka tutulmaktadır.

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

Canlı bir uygulamada hata mesajlarını özelleştirirken çok detaya girmemek gerekir.Kötü niyetli kullanıcılar bundan çıkarım sağlamak isteyebilirler.

## Sorular

### Kimlik doğrulama yöntemi ve oturum açma nedir ?

Kimlik Doğrulama (Authentication): Kimlik doğrulama, bir kullanıcının kimliğini doğrulamak ve tanımak için kullanılan süreçtir. Yani, bir kişinin kim olduğunu ve iddia ettiği kişi olup olmadığını doğrulamaktır. Kimlik doğrulama, kullanıcıların erişim yetkilerini ve haklarını belirlemenize yardımcı olur. Kimlik doğrulama işlemi, kullanıcı adı ve şifre, biyometrik veriler (parmak izi, yüz tanıma vb.), sertifikalar veya diğer kimlik doğrulama yöntemleri aracılığıyla gerçekleştirilebilir.

Oturum Açma (Login): Oturum açma, bir kullanıcının bir uygulamaya erişim sağlamak için kimlik doğrulama sürecini tamamladıktan sonra uygulamada oturum açmasını ifade eder. Oturum açma, kimlik doğrulama sonrasında kullanıcının uygulamada belirli bir süre boyunca kimlik bilgilerini koruyan bir oturum başlatmasını içerir. Bu oturum süresince kullanıcı, uygulama içinde çeşitli işlemleri gerçekleştirebilir.

Özetle, kimlik doğrulama, kullanıcının kimliğini doğrulama sürecidir, oturum açma ise kimlik doğrulama sonucunda kullanıcının uygulamada etkin bir şekilde işlem yapabilmesini sağlayan bir süreçtir.

### Oturum açmak ve giriş yapmak farkı nedir ?

Giriş Yapmak: Bu terim, genellikle daha genel bir ifadedir ve kullanıcının bir sistem veya uygulamaya erişim sağlama eylemini tanımlar. Kimlik doğrulama işlemi sonucu, kullanıcı sisteme giriş yapar ve oturum açar. "Giriş yapmak," kullanıcının sistemdeki hesabına erişim sağlama eylemini betimler.

Oturum Açmak: Bu terim, genellikle oturum yönetimi ve kullanıcının belirli bir oturumu başlatma işlemiyle daha özdeşleşmiştir. Oturum açma, kullanıcının kimlik doğrulama sonrası uygulamada bir oturum başlatmasını ve bu oturum süresince etkin olmasını ifade eder. Yani, kullanıcı oturum açarak uygulamada işlem yapmaya başlar.

Yine de bu terimler, farklı bağlamlarda ve farklı teknik dillerde değişebilir, bu nedenle kullanıcılar ve geliştiriciler genellikle bunları eşanlamlı olarak kullanırlar. Önemli olan, kimlik doğrulama sürecinin tamamlanmasının ardından kullanıcının sisteme erişim sağlaması ve oturum açmasıdır, terimler arasındaki farklar genellikle küçük nüanslara dayanır.

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
