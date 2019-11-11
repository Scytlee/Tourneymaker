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
            if (nickname?.Length > 50)
            {
                output = false;
                errorMessageBuilder.Append("\nNickname can be at most 50 characters long.");
            }
            if (firstName?.Length > 50)
            {
                output = false;
                errorMessageBuilder.Append("\nFirst name can be at most 50 characters long.");
            }
            if (lastName?.Length > 50)
            {
                output = false;
                errorMessageBuilder.Append("\nLast name can be at most 50 characters long.");
            }
            if (discordTag?.Length > 100)
            {
                output = false;
                errorMessageBuilder.Append("\nDiscord tag can be at most 100 characters long.");
            }
            if (emailAddress?.Length > 200)
            {
                output = false;
                errorMessageBuilder.Append("\nEmail address can be at most 200 characters long.");
            }

            // TODO Check validity of Discord tag and email address
            // TODO Refactor validation

            errorMessage = errorMessageBuilder.Length == 0 ? "" : errorMessageBuilder.ToString();

            return output;
        }

        public static bool ValidateEntryForm(out string errorMessage, string entryName, List<PersonModel> entryMembers)
        {
            bool output = true;
            StringBuilder errorMessageBuilder = new StringBuilder();

            if (entryMembers.Count == 0)
            {
                output = false;
                errorMessageBuilder.Append("\nEntry has to have at least 1 member.");
            }
            if (entryMembers.Count == 1 && !string.IsNullOrWhiteSpace(entryName))
            {
                output = false;
                errorMessageBuilder.Append("\nEntry name can be set only for entries with 2 or more members.");
            }
            if (entryMembers.Count >= 2 && string.IsNullOrWhiteSpace(entryName))
            {
                output = false;
                errorMessageBuilder.Append("\nYou have to specify entry name.");
            }

            if (entryName?.Length > 100)
            {
                output = false;
                errorMessageBuilder.Append("\nEntry name can be at most 100 characters long.");
            }

            // TODO Refactor validation

            errorMessage = errorMessageBuilder.Length == 0 ? "" : errorMessageBuilder.ToString();

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
                errorMessageBuilder.Append("\nYou have to specify tournament name.");
            }
            if (tournamentEntries.Count < 2)
            {
                output = false;
                errorMessageBuilder.Append("\nTournament has to have at least 2 entries.");
            }

            if (tournamentName?.Length > 100)
            {
                output = false;
                errorMessageBuilder.Append("\nTournament name can be at most 100 characters long.");
            }

            // TODO Refactor validation

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
                errorMessageBuilder.Append("\nYou have to specify score of the first entry.");
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
                errorMessageBuilder.Append("\nYou have to specify score of the second entry.");
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

            // TODO String length validation

            errorMessage = errorMessageBuilder.Length == 0 ? "" : errorMessageBuilder.ToString();

            return output;
        }
    }
}
