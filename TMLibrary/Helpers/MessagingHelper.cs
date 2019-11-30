using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMLibrary.Helpers
{
    // TODO Implement messaging
    public static class MessagingHelper
    {
        public static void SendEmail(string to, string message)
        {
            throw new NotImplementedException();
        }

        public static void SendEmailToGroup(List<string> to, string message)
        {
            foreach (string email in to)
            {
                SendEmail(email, message);
            }
        }

        public static void SendDiscordMessage(string to, string message)
        {
            throw new NotImplementedException();
        }

        public static void SendDiscordMessageToGroup(List<string> to, string message)
        {
            foreach (string user in to)
            {
                SendDiscordMessage(user, message);
            }
        }

        public static string GenerateMessage()
        {
            throw new NotImplementedException();
        }
    }
}
