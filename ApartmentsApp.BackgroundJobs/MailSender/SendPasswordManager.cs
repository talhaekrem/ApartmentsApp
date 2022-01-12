using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalhaMarket.Service.MailSender;

namespace ApartmentsApp.BackgroundJobs.MailSender
{
    public class SendPasswordManager
    {
        private readonly IMailService _mailService;

        public SendPasswordManager(IMailService mailService)
        {
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }
        public async Task Process(string email, string displayName, string notCryptedPassword)
        {
            await _mailService.SendUsersMail(email,displayName, notCryptedPassword);
        }
    }
}
