using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMLibrary;
using TMLibrary.Helpers;
using TMLibrary.Models;

namespace TMWinFormsUI
{
    public partial class CreateTournamentForm : Form, IEntryRequester
    {
        private List<EntryModel> _availableEntries = GlobalConfig.Connection.LoadEntryModels();
        private List<EntryModel> _selectedEntries = new List<EntryModel>();

        public CreateTournamentForm()
        {
            InitializeComponent();

            WireUpLists();
        }

        private void WireUpLists()
        {
            _availableEntries = _availableEntries.OrderBy(x => x.DisplayName).ToList();
            selectEntryDropDown.DataSource = null;
            selectEntryDropDown.DataSource = _availableEntries;
            selectEntryDropDown.DisplayMember = nameof(EntryModel.DisplayName);

            _selectedEntries = _selectedEntries.OrderBy(x => x.DisplayName).ToList();
            tournamentEntriesListBox.DataSource = null;
            tournamentEntriesListBox.DataSource = _selectedEntries;
            tournamentEntriesListBox.DisplayMember = nameof(EntryModel.DisplayName);
        }

        private void addSelectedEntryButton_Click(object sender, EventArgs e)
        {
            EntryModel entry = (EntryModel) selectEntryDropDown.SelectedItem;

            if (entry != null)
            {
                _availableEntries.Remove(entry);
                _selectedEntries.Add(entry);

                WireUpLists();
            }
        }

        public void EntryComplete(EntryModel entry)
        {
            _selectedEntries.Add(entry);

            WireUpLists();
        }

        private void createNewEntryButton_Click(object sender, EventArgs e)
        {
            CreateEntryForm form = new CreateEntryForm(this);
            form.Show();
        }

        private void removeSelectedEntryButton_Click(object sender, EventArgs e)
        {
            EntryModel entry = (EntryModel) tournamentEntriesListBox.SelectedItem;

            if (entry != null)
            {
                _selectedEntries.Remove(entry);
                _availableEntries.Add(entry);

                WireUpLists();
            }
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            // Validate the data in the form
            if (ValidationHelper.ValidateTournamentForm(out string errorMessage, tournamentNameValue.Text, _selectedEntries))
            {
                // Create the TournamentModel
                TournamentModel tournament = new TournamentModel
                {
                    TournamentName = tournamentNameValue.Text,
                    TournamentEntries = _selectedEntries
                };

                // Wire up matchups
                TournamentLogic.CreateRounds(tournament);

                // Create Tournament entry
                // Create all TournamentEntries
                GlobalConfig.Connection.CreateTournament(tournament);
            }
            else
            {
                // Show error message
                MessageBox.Show($"The following errors exist in the form:\n{ errorMessage }", "Creation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
