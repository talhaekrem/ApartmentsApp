using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsApp.BackgroundJobs.MailSender
{
    public static class SendPasswordJob
    {
        [Obsolete]
        public static void SendUsersPassword(string email,string displayName, string notCryptedPassword)
        {
            Hangfire.BackgroundJob.Enqueue<SendPasswordManager>(job => job.Process(email, displayName, notCryptedPassword));
        }
    }
}
