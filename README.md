# bitirme-projesi-talhaekrem
## Gelecek Varlık Fullstack Bootcamp Bitirme Projesi
### Proje içeriği aşağıdaki gibidir:
#### Apartmanların olduğu bir sitede yöneticiyseniz:
* Kiracı veya ev sahiplerine ortak kullanım faturalarını kestiğiniz
* Yeni daireleri eklediğiniz
* Kullanıcılara daire atadığınız
* Kullanıcılardan gelen fatura ödeme bilgileri ve gelen özel mesajları görebildiğiniz
* Kullanıcılara ait CRUD işlemlerini yapabildiğiniz
* Dairelere ait CRUD işlemlerini yapabildiğiniz bir projedir.
#### Apartmanların olduğu bir sitede ev sahibiyseniz:
* Aylık ödemeniz gereken aidat & ortak kullanım faturaları son ödeme tarihlerini görebildiğiniz
* Yöneticiye özel mesaj atabildiğiniz
* Kredi kartıyla ödeme yapabildiğiniz
* Geçmiş aylara ait ödemelerinizi görebildiğiniz bir projedir.


### İlk Başta 1 tane base admin oluyor. Base Admin, projeye yetkilendirme eklenmeden önce sistemde kendisini admin rolüyle kaydetmiş kişidir. Admin paneli 
```cs
[Authorize(Roles = 'Admin')]
```
  özelliğiyle "Admin" rolü istediğinden ilk eklenecek admin bu kontrolün dışında olmalıydı. 
- Admin rolü, kullanıcı eklerken "ekleyeceğiniz kişi yönetici mi?" seçeneğini açarsanız verilmektedir.
- Register sayfası yoktur. Kullanıcıları, admin ekliyor ve eklediği an kullanıcılara rastgele bir parola oluşturuluyor ve bu parola kullanıcının mail adresine yollanıp daha sonrasında parolayı şifreleyip veri tabanına kaydediyoruz. Adminler asla kullanıcıların parolalarını bilemez.
---
### Evler
![Evler](/README-FILES/homes.png)
- Kullanıcılara ait ev bilgileri listelenmektedir. Ayrıca 3 noktadan detay, güncelle ve sil işlemlerini de yapabilmekteyiz.
## Eve ait aidat bedeli ev eklerken formda belirtilmelidir.
---
### Faturalar
![Faturalar](/README-FILES/bills.png)
- Kullanıcıların kaldığı ev idsine göre o aya ait aidat, ortak kullanım elektrik, su, doğalgaz faturalarının ödendi, ödenmedi, fatura kesilmedi bilgilerini görebilmekteyiz.
- Ortak kullanım faturalarının hepsi aynı günde kesilmeyebileceği için her bir fatura için tarih giriyoruz. Henüz girilmemiş fatura varsa "fatura kesilmedi" şeklinde yazıyoruz.
- Fatura veya aidat ile ilgili güncelleme, detay, silme ekranı için "ödendi", "ödenmedi","fatura girilmedi" yazılarına tıklamak yeterlidir.
---
### Kullanıcılar
![Kullanıcılar](/README-FILES/users.png)
- Kullanıcılara ait bilgilerin listelendiği tablodur. Admin veya User farketmeksizin herkes listelenmektedir. 
- 3 noktadan detay, güncelle ve sil işlemlerini de yapabilmekteyiz.
- Kullanıcıya ait parolanın arayüzde gözükmesini engelledim. Veritabanında şifrelenmiş haldedir. Yanlızca mailden görülebilir.
---
### Authentication & Authorization
###### Yetkilendirme ve rol kontrolünü JWT ile yaptım. Login olduğumuzda 1 gün geçerli token oluşturulup cookielerde saklanır. 
###### JWT servislerini arayüz tarafında oluşturdum.
###### Rol ve kimlik kontrolünü ise token oluştururken token içine işlediğim ClaimTypes ile yaptım. Ayrıca Tokenları doğruladığım method da mevcuttur 
```cs
Subject = new ClaimsIdentity(new Claim[]
  {
      new Claim(ClaimTypes.Name, id.ToString()),
      new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")
  })
```
###### startup.cs içerisinde authentication için default olarak JWT schemasını kullanacağımı ve AuthorizeAttribute için cookilerdeki "jwt" keyine sahip tokenı değerlendirmesi söyledim.
#### Login olmak için hesap bilgileri
##### Admin girişi için: 
1. TcNo: **11111111111** Şifre: **_4_1bhMR**

##### Kullanıcı Girişi için:
1. TcNo: **12345612345** Şifre: **V5x2$T_L**
2. TcNo: **12345678900** Şifre: **i6_s_98G**
---
### Kullanıcı Kayıt İşlemi
![Şifre Mail](/README-FILES/mail.jpeg)
- Admin, Kullanıcıyı sisteme kaydettiği an veriler veritabanına yazılmadan hemen önce, hangfire ile arka planda kullanının mailine oluşturulan parola yollanmaktadır.
- Hangfire, tablolarını oluşturması için MS SQL' deki veritabanını kullanmasını söyledim.
- Hangfire iş tanımlamaları "ApartmentsApp.BackgroundJobs" katmanı altındadır.
---
### Mesajlar
![Mesajlar](/README-FILES/messages.png)
- Eğer kullanıcıysanız dropdowndan istediğiniz yöneticiye, eğer yöneticiyseniz dropdowndan istediğiniz kullanıcıya mesaj atabilirsiniz.
- Mesajı yollayan ve mesajı alan kişilerin görüldü bilgileri farklıdır. Karşı taraf mesajı henüz açmamışsa iletildi olarak yazıyor. Mesajı açtığı an görüldü olarak yazıyor.
---
### Veritabanları
### MS SQL 
![DB schema](/README-FILES/mssql-db-schema.png)
- Kredi Kartı verileri hariç her şey MS SQL de tutuluyor. Yukarıdaki görselde oluşturduğum veritabanının şeması yer alıyor. Ayrıca  README-FILES klasörü içerisinde veritabanımın verilerle birlikte scripti mevcuttur. 
---
## MongoDB
![Mongo DB](/README-FILES/credit-card-Mongo.png)
- Kredi Kartı verileri Mongo DBde saklanmaktadır. 
- Kullanıcı tarafında Kredi Kartı Ekleme, Kredi Kartı doğrulama, fatura & aidat ödeme servisleri API katmanında yazdım.
---
### Kullandığım teknolojiler şunlardır:
- Backend: ASP.NET Core 5.0
- Frontend: React js
- Genel Veriler: MS SQL
- Kredi Kartı verileri: Mongo DB
- Kredi Kartı İle Ödeme: ASP.NET Core Web API
