using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using ApartmentsApp.Services.UserServices;

namespace TalhaMarket.Service.MailSender
{
    //mail yollama servisidir. Hangfire delayed olarak tetiklenince burası çalışacaktır.
    //mail yollamak için Mimekit paketini kullanıyorum. Çoğu portu destekler.
    public class MailManager : IMailService
    {
        //öncelikle userIdyi alıyorum. Maili yollayacağım kullanıcıya ait bilgileri veritabanından almak için(adı, soyadı, e posta adresi...)
        public async Task SendUsersMail(string email, string displayName, string notCryptedPassword)
        {
            //altta private olarak tanımladığım smtp bağlantısını kurgulayıp çalıştırıyorum.
            using var client = CreateSmtpClient();
            //userı alıyorum.
            //mail yollama servisi
            MimeMessage message = new MimeMessage();

            //maili yollayan kişinin görünen adı ve maili
            MailboxAddress from = new MailboxAddress("My Apartments", "talhaekrem0@yandex.com");
            message.From.Add(from);

            //maili alacak kişinin görünen adı ve maili
            MailboxAddress to = new MailboxAddress($"{displayName}", email);
            message.To.Add(to);
            //mailin konusu
            message.Subject = "My Apartments Platformuna Hoş Geldiniz";
            //mailin spama düşmemesi için Headerda Unsubscribe seçeneği olması gerekiyor.Yandexin makalelerinde yazıyordu. onu ekliyorum
            message.Headers.Add("List-Unsubscribe", "<mailto:talhekrem0@yandex.com?subject=unsubscribe>");
            //mesaj içeriğini html olarak hazırlıyorum.
            message.Body = new TextPart("html")
            {
                Text = String.Format(
@"<h1>Hoş Geldin {0}</h1>
<h2>Sizi burada görmekten dolayı çok mutluyuz.</h2>
<hr>
<h4>Bu platformda sitendeki ortak kullanım faturalarını ve aidatlarını kolayca ödeyebilirsin</h4>
 <p>Hesabın, yönetici tarafından sisteme başarıyla kaydedilmiştir</p>
<hr>
<h4>Sisteme girişte kullanacpınız parola otomatik olarak oluşturup şifrelenmiştir. Bu şifreyi senden başka kimse bilmiyor. Lütfen şifrenizi kimseyle paylaşmayın</h4>
<p>Platforma girişte kullanacağınız şifreniz: <strong>{1}</strong></p>
<br>--My Apartments Mail Sender With <b>Hangfire</b><hr><p>My Apartments Yönetimi</p>", displayName, notCryptedPassword)
            };
            //asenkron olarak maili yolla ve smtpden çıkış yap, bırak
            await client.SendAsync(message);
            client.Disconnect(true);
            client.Dispose();
        }

        //gmailin smtp servisi, ücretli olan workspace hesabı istediğinden yandexin ücretsiz servisini kullanıyorum.
        // Connect yeri sabittir. İnternette mevcut. bağantıyı kurduktan sonra maili atacak ana hesabı girmeniz gerekiyor.
        //bunun için kendime yandex hesabı oluşturdum. İnternette nasıl Authenticate yapılacağına dair yazılar mevcuttur.
        // Lütfen benim hesabını kullanmayın. Kendinize hesap açıp kendi şifrenizi oluşturun.
        private SmtpClient CreateSmtpClient()
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Connect("smtp.yandex.com", 465, true);
            smtp.Authenticate("talhaekrem0@yandex.com", "njfqmehnzooebuum");
            return smtp;
        }
    }
}