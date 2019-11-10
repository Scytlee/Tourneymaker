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
        private List<PersonModel> _availablePeople = GlobalConfig.Connection.LoadPersonModels();
        private List<PersonModel> _selectedPeople = new List<PersonModel>();

        public CreateEntryForm()
        {
            InitializeComponent();

            //CreateSampleData();

            WireUpLists();
        }

        private void CreateSampleData()
        {
            _availablePeople.Add(new PersonModel { FirstName = "Test", LastName = "Person1", Nickname = "TP1"});
            _availablePeople.Add(new PersonModel { FirstName = "Test", LastName = "Person2", Nickname = "" });
            _availablePeople.Add(new PersonModel { FirstName = "", LastName = "", Nickname = "TP3"});
            _availablePeople.Add(new PersonModel { FirstName = "VeryLongFirstName", LastName = "VeryLongLastName", Nickname = "VeryLongNickname" });

            _selectedPeople.Add(new PersonModel { FirstName = "Test", LastName = "Person1", Nickname = "TP1" });
            _selectedPeople.Add(new PersonModel { FirstName = "Test", LastName = "Person2", Nickname = "" });
            _selectedPeople.Add(new PersonModel { FirstName = "", LastName = "", Nickname = "TP3" });
            _selectedPeople.Add(new PersonModel { FirstName = "VeryLongFirstName", LastName = "VeryLongLastName", Nickname = "VeryLongNickname" });
        }

        private void WireUpLists()
        {
            selectPersonDropDown.DataSource = null;
            selectPersonDropDown.DataSource = _availablePeople;
            selectPersonDropDown.DisplayMember = "DisplayName";

            entryMembersListBox.DataSource = null;
            entryMembersListBox.DataSource = _selectedPeople;
            entryMembersListBox.DisplayMember = "DisplayName";
        }

        // TODO Refactor person creation to not repeat code in both methods
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

                _availablePeople.Add(person);

                WireUpLists();
            }
            else
            {
                // Show error message
                // TODO Correct the message
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

                _selectedPeople.Add(person);

                WireUpLists();

                // Clear the form
                ClearPersonCreatorForm();
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

        private void addSelectedEntryButton_Click(object sender, EventArgs e)
        {
            PersonModel person = (PersonModel) selectPersonDropDown.SelectedItem;

            if (person != null)
            {
                _availablePeople.Remove(person);
                _selectedPeople.Add(person);

                WireUpLists(); 
            }
        }

        private void removeSelectedPersonButton_Click(object sender, EventArgs e)
        {
            PersonModel person = (PersonModel) entryMembersListBox.SelectedItem;

            if (person != null)
            {
                _selectedPeople.Remove(person);
                _availablePeople.Add(person);

                WireUpLists(); 
            }
        }

        private void CreateEntryForm_Load(object sender, EventArgs e)
        {

        }

        private void entryNameValue_TextChanged(object sender, EventArgs e)
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
