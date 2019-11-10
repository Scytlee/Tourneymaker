using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMLibrary.Helpers
{
    public static class ValidationHelper
    {
        public static bool ValidatePersonCreatorForm(out string errorMessage, string nickname, string firstName,
            string lastName, string discordTag, string emailAddress)
        {
            bool output = true;
            StringBuilder errorMessageBuilder = new StringBuilder();

            // Checking personal data of the person
            if (string.IsNullOrWhiteSpace(nickname) && (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName)))
            {
                output = false;
                errorMessageBuilder.Append("\nYou have to specify either nickname or first and last name of the person.");
            }

            // Checking communication way with the person
            if (string.IsNullOrWhiteSpace(discordTag) && string.IsNullOrWhiteSpace(emailAddress))
            {
                output = false;
                errorMessageBuilder.Append("\nYou have to specify either Discord tag or email address of the person.");
            }

            // Checking length of input data
            if (nickname.Length > 50)
            {
                output = false;
                errorMessageBuilder.Append("\nNickname can be at most 50 characters long.");
            }
            if (firstName.Length > 50)
            {
                output = false;
                errorMessageBuilder.Append("\nFirst name can be at most 50 characters long.");
            }
            if (lastName.Length > 50)
            {
                output = false;
                errorMessageBuilder.Append("\nLast name can be at most 50 characters long.");
            }
            if (discordTag.Length > 100)
            {
                output = false;
                errorMessageBuilder.Append("\nDiscord tag can be at most 100 characters long.");
            }
            if (emailAddress.Length > 200)
            {
                output = false;
                errorMessageBuilder.Append("\nEmail address can be at most 200 characters long.");
            }

            // TODO Add error message box
            // TODO Check validity of Discord tag and email address
            // TODO Refactor validation

            if (errorMessageBuilder.Length == 0)
            {
                errorMessage = "";
            }
            else
            {
                errorMessage = errorMessageBuilder.ToString();
            }

            return output;
        }
    }
}
