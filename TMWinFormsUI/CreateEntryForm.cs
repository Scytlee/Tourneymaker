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
        private IEntryRequester _callingForm;

        private List<PersonModel> _availablePeople = GlobalConfig.Connection.LoadPersonModels();
        private List<PersonModel> _selectedPeople = new List<PersonModel>();

        public CreateEntryForm(IEntryRequester caller)
        {
            InitializeComponent();

            _callingForm = caller;

            WireUpLists();
        }

        private void WireUpLists()
        {
            _availablePeople = _availablePeople.OrderBy(x => x.DisplayName).ToList();
            selectPersonDropDown.DataSource = null;
            selectPersonDropDown.DataSource = _availablePeople;
            selectPersonDropDown.DisplayMember = nameof(PersonModel.DisplayName);

            _selectedPeople = _selectedPeople.OrderBy(x => x.DisplayName).ToList();
            entryMembersListBox.DataSource = null;
            entryMembersListBox.DataSource = _selectedPeople;
            entryMembersListBox.DisplayMember = nameof(PersonModel.DisplayName);
        }

        private PersonModel CreatePerson(string nickname, string firstName, string lastName, string discordTag,
            string emailAddress)
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

                return person;
            }
            else
            {
                // Show error message
                MessageBox.Show($"The following errors exist in the form:\n{ errorMessage }", "Creation error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }

        private void personCreatorCreateButton_Click(object sender, EventArgs e)
        {
            PersonModel person = CreatePerson(personCreatorNicknameValue.Text,
                personCreatorFirstNameValue.Text, personCreatorLastNameValue.Text,
                personCreatorDiscordTagValue.Text, personCreatorEmailAddressValue.Text);

            if (person != null)
            {
                ClearPersonCreatorForm();

                _availablePeople.Add(person);

                WireUpLists();
            }
        }

        private void personCreatorCreateAndAddButton_Click(object sender, EventArgs e)
        {
            PersonModel person = CreatePerson(personCreatorNicknameValue.Text,
                personCreatorFirstNameValue.Text, personCreatorLastNameValue.Text,
                personCreatorDiscordTagValue.Text, personCreatorEmailAddressValue.Text);

            if (person != null)
            {
                ClearPersonCreatorForm();

                _selectedPeople.Add(person);

                WireUpLists();
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

        private void createEntryButton_Click(object sender, EventArgs e)
        {
            // Validate the data in the form
            if (ValidationHelper.ValidateEntryFormWithErrorMessage(out string errorMessage, entryNameValue.Text, _selectedPeople))
            {
                // Create the EntryModel
                EntryModel entry = new EntryModel
                {
                    EntryName = entryNameValue.Text,
                    EntryMembers = _selectedPeople
                };

                // Add the EntryModel to the database
                GlobalConfig.Connection.CreateEntry(entry);

                // Notify calling form about the entry
                _callingForm.EntryComplete(entry);

                // Clear the form
                ClearEntryForm();
            }
            else
            {
                // Show error message
                MessageBox.Show($"The following errors exist in the form:\n{ errorMessage }", "Creation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearEntryForm()
        {
            entryNameValue.Text = "";

            foreach (PersonModel person in _selectedPeople)
            {
                _availablePeople.Add(person);
            }

            _selectedPeople.Clear();

            WireUpLists();
        }
    }
}
