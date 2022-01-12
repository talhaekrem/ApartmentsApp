using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalhaMarket.Service.MailSender
{
    public interface IMailService
    {
        Task SendUsersMail(string email, string displayName, string notCryptedPassword);
    }
}
