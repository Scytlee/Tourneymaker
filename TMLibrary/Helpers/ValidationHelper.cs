using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMLibrary.Models;

namespace TMLibrary.Helpers
{
    public static class ValidationHelper
    {
        private static string NoValueMessage(string valueName)
        {
            valueName = $"{ valueName.Substring(0, 1).ToLower() }{ valueName.Substring(1) }";
            return $"\nYou have to specify { valueName }.";
        }

        private static string TooLongValueMessage(string valueName, int valueLength)
        {
            valueName = $"{ valueName.Substring(0, 1).ToUpper() }{ valueName.Substring(1) }";
            return $"\n{ valueName } can be at most { valueLength } characters long.";
        }

        public static bool ValidatePersonCreatorForm(out string errorMessage, string nickname, string firstName,
            string lastName, string discordTag, string emailAddress)
        {
            bool output = true;
            StringBuilder errorMessageBuilder = new StringBuilder();

            // Checking personal data of the person
            if (string.IsNullOrWhiteSpace(nickname) && (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName)))
            {
                output = false;
                errorMessageBuilder.Append(NoValueMessage("either nickname or full name"));
            }

            // Checking communication way with the person
            if (string.IsNullOrWhiteSpace(discordTag) && string.IsNullOrWhiteSpace(emailAddress))
            {
                output = false;
                errorMessageBuilder.Append(NoValueMessage("either Discord tag or email address"));
            }

            // Checking length of input data
            if (nickname?.Length > 50)
            {
                output = false;
                errorMessageBuilder.Append(TooLongValueMessage("nickname", 50));
            }
            if (firstName?.Length > 50)
            {
                output = false;
                errorMessageBuilder.Append(TooLongValueMessage("first name", 50));
            }
            if (lastName?.Length > 50)
            {
                output = false;
                errorMessageBuilder.Append(TooLongValueMessage("last name", 50));
            }
            if (discordTag?.Length > 100)
            {
                output = false;
                errorMessageBuilder.Append(TooLongValueMessage("Discord tag", 100));
            }
            if (emailAddress?.Length > 200)
            {
                output = false;
                errorMessageBuilder.Append(TooLongValueMessage("email address", 200));
            }

            // TODO Check validity of Discord tag and email address

            errorMessage = errorMessageBuilder.Length == 0 ? "" : errorMessageBuilder.ToString();

            return output;
        }

        public static bool ValidateEntryFormWithErrorMessage(out string errorMessage, string entryName, IEnumerable<PersonModel> entryMembers)
        {
            bool output = true;
            StringBuilder errorMessageBuilder = new StringBuilder();

            if (entryMembers.Count() == 0)
            {
                output = false;
                errorMessageBuilder.Append("\nEntry has to have at least 1 member.");
            }
            if (entryMembers.Count() == 1 && !string.IsNullOrWhiteSpace(entryName))
            {
                output = false;
                errorMessageBuilder.Append("\nEntry name can be set only for entries with 2 or more members.");
            }
            if (entryMembers.Count() >= 2 && string.IsNullOrWhiteSpace(entryName))
            {
                output = false;
                errorMessageBuilder.Append(NoValueMessage("entry name"));
            }

            if (entryName?.Length > 100)
            {
                output = false;
                errorMessageBuilder.Append(TooLongValueMessage("entry name", 100));
            }

            errorMessage = errorMessageBuilder.Length == 0 ? "" : errorMessageBuilder.ToString();

            return output;
        }

        public static bool ValidateEntryForm(string entryName, IEnumerable<PersonModel> entryMembers)
        {
            // Null check
            if (entryMembers == null)
            {
                return false;
            }

            bool output = true;

            if (entryMembers.Count() == 0)
            {
                output = false;
            }
            if (entryMembers.Count() == 1 && !string.IsNullOrWhiteSpace(entryName))
            {
                output = false;
            }
            if (entryMembers.Count() >= 2 && string.IsNullOrWhiteSpace(entryName))
            {
                output = false;
            }

            if (entryName?.Length > 100)
            {
                output = false;
            }

            return output;
        }

        public static bool ValidateTournamentForm(out string errorMessage, string tournamentName,
            List<EntryModel> tournamentEntries)
        {
            bool output = true;
            StringBuilder errorMessageBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(tournamentName))
            {
                output = false;
                errorMessageBuilder.Append(NoValueMessage("tournament name"));
            }
            if (tournamentEntries.Count < 2)
            {
                output = false;
                errorMessageBuilder.Append("\nTournament has to have at least 2 entries.");
            }

            if (tournamentName?.Length > 100)
            {
                output = false;
                errorMessageBuilder.Append(TooLongValueMessage("tournament name", 100));
            }

            errorMessage = errorMessageBuilder.Length == 0 ? "" : errorMessageBuilder.ToString();

            return output;
        }

        public static bool ValidateUpdateScoreForm(out string errorMessage, string entryOneScore, string entryTwoScore)
        {
            bool output = true;
            StringBuilder errorMessageBuilder = new StringBuilder();

            bool isScoreOneValid = false;
            bool isScoreTwoValid = false;
            double scoreOne = 0;
            double scoreTwo = 0;

            if (string.IsNullOrWhiteSpace(entryOneScore))
            {
                output = false;
                errorMessageBuilder.Append(NoValueMessage("score of the first entry"));
            }
            else
            {
                isScoreOneValid = double.TryParse(entryOneScore, out scoreOne);

                if (!isScoreOneValid)
                {
                    output = false;
                    errorMessageBuilder.Append("\nScore of the first entry is not a valid number.");
                }
            }
            if (string.IsNullOrWhiteSpace(entryTwoScore))
            {
                output = false;
                errorMessageBuilder.Append(NoValueMessage("score of the second entry"));
            }
            else
            {
                isScoreTwoValid = double.TryParse(entryTwoScore, out scoreTwo);

                if (!isScoreTwoValid)
                {
                    output = false;
                    errorMessageBuilder.Append("\nScore of the second entry is not a valid number.");
                }
            }

            if (isScoreOneValid && isScoreTwoValid && scoreOne == scoreTwo)
            {
                output = false;
                errorMessageBuilder.Append("\nThe matchup cannot result in a tie.");
            }

            errorMessage = errorMessageBuilder.Length == 0 ? "" : errorMessageBuilder.ToString();

            return output;
        }
    }
}
