# .NET Core 3.1 Identity Server

Identity Server öncesi MemberShip üyelik yönetimi kullanılmaktadır.

Identity Server projesinde kullanıcı ve token işlemleri 2 farklı işlem olarak ele alınabilir.
İşlemlerimizi gerçekleştirebilmek için Identity'nin hazır metotlarını ve sınıflarını kullanabilirken 
özelleştirerek de bunları projedemizde kullanabiliriz.

Örnek olarak vermemiz gerekirse IdentityUser sınıfıı miras alarak yeni bir sınıf oluşturabiliriz.
Token üretebilmek için "AddIdentity" metodunu kullandıktan sonra "AddDefaultTokenProviders" metodunu kullanarak Identity Server'ın bize sunmuş olduğu otomatik token yapısını kullanabiliriz.

## Kullanılan Nugetler

### Mapster

Mapster sınıflar arasında dönüşüm yapmamızı sağlayan automapper'dan daha hafif bir kütüphanedir.İsimleri aynı olan propertyleri otomatik olarak çevirmektedir.Automapper gibi profile tanımlaması zorunlu olmadan direk olarak extension üzerinden kullanabiliriz.

## Kurallar

* Identity Server üzerinde **Username** alanı otomatik olarak unique tanımlanmaktadır.Örneğin Email adresini AddIdentity dediğimiz alanda unique olması gerektiğini tanımlarken telefon numarası için bir bilgi bulunmamaktadır.Username alanı aynı adla birden fazla kez tanımlanmaya çalışılırsa hata fırlatmaktadır.

## Proje Bilgileri

Katmanlarımızı Core , Data , Domain ve WebUI olmak üzere 4 parçadan oluşmaktadır.

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

## Kurslar

* Fatih Çakıroğlu : Asp.Net Core Üyelik Sistemi+Token Bazlı Kimlik Doğrulama