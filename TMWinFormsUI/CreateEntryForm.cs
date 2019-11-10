using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMLibrary;
using TMLibrary.Models;
using TMLibrary.Helpers;

namespace TMWinFormsUI
{
    public partial class CreateEntryForm : Form
    {
        public CreateEntryForm()
        {
            InitializeComponent();
        }

        // TODO Refactor creation to not repeat in both methods
        private void personCreatorCreateButton_Click(object sender, EventArgs e)
        {
            // Validate the data in the form
            if (ValidationHelper.ValidatePersonCreatorForm(out string errorMessage, personCreatorNicknameValue.Text,
                personCreatorFirstNameValue.Text, personCreatorLastNameValue.Text,
                personCreatorDiscordTagValue.Text, personCreatorEmailAddressValue.Text))
            {
                // Create the PersonModel
                PersonModel person = new PersonModel
                {
                    Nickname = personCreatorNicknameValue.Text,
                    FirstName = personCreatorFirstNameValue.Text,
                    LastName = personCreatorLastNameValue.Text,
                    DiscordTag = personCreatorDiscordTagValue.Text,
                    EmailAddress = personCreatorEmailAddressValue.Text
                };

                // Add the PersonModel to the database
                GlobalConfig.Connection.CreatePerson(person);

                // Clear the form
                ClearPersonCreatorForm();
            }
            else
            {
                // Show error message
                MessageBox.Show($"The following errors exist in the form:\n{ errorMessage }");
            }
        }

        private void personCreatorCreateAndAddButton_Click(object sender, EventArgs e)
        {
            // Validate the data in the form
            if (ValidationHelper.ValidatePersonCreatorForm(out string errorMessage, personCreatorNicknameValue.Text, 
                    personCreatorFirstNameValue.Text, personCreatorLastNameValue.Text,
                    personCreatorDiscordTagValue.Text, personCreatorEmailAddressValue.Text))
            {
                // Create the PersonModel
                PersonModel person = new PersonModel
                {
                    Nickname = personCreatorNicknameValue.Text,
                    FirstName = personCreatorFirstNameValue.Text,
                    LastName = personCreatorLastNameValue.Text,
                    DiscordTag = personCreatorDiscordTagValue.Text,
                    EmailAddress = personCreatorEmailAddressValue.Text
                };

                // Add the PersonModel to the database
                GlobalConfig.Connection.CreatePerson(person);

                // Clear the form
                ClearPersonCreatorForm();

                // TODO Add member to entry
            }
            else
            {
                // Show error message
                MessageBox.Show($"The following errors exist in the form:\n{ errorMessage }");
            }
        }

        private void ClearPersonCreatorForm()
        {
            personCreatorNicknameValue.Text = "";
            personCreatorFirstNameValue.Text = "";
            personCreatorLastNameValue.Text = "";
            personCreatorDiscordTagValue.Text = "";
            personCreatorEmailAddressValue.Text = "";
        }

        private void CreateEntryForm_Load(object sender, EventArgs e)
        {

        }

        private void entryNameValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void addSelectedEntryButton_Click(object sender, EventArgs e)
        {

        }

        private void selectEntryLabel_Click(object sender, EventArgs e)
        {

        }

        private void selectEntryDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
