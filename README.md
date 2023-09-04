# .NET Core 3.1 Identity Server

Identity Server öncesi MemberShip üyelik yönetimi kullanılmaktadır.


Identity Server projesinde kullanıcı ve token işlemleri 2 farklı işlem olarak ele alınabilir.
İşlemlerimizi gerçekleştirebilmek için Identity'nin hazır metotlarını ve sınıflarını kullanabilirken 
özelleştirerek de bunları projedemizde kullanabiliriz.

Örnek olarak vermemiz gerekirse IdentityUser sınıfıı miras alarak yeni bir sınıf oluşturabiliriz.
Token üretebilmek için "AddIdentity" metodunu kullandıktan sonra "AddDefaultTokenProviders" metodunu kullanarak Identity Server'ın bize
sunmuş olduğu otomatik token yapısını kullanabiliriz.

##Proje Bilgileri

Katmanlarımızı Core , Data , Domain ve WebUI olmak üzere 4 parçadan oluşmaktadır.