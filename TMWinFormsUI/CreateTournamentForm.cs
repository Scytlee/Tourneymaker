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
            _availableEntries = _availableEntries.OrderBy(x => x.EntryName).ToList();
            selectEntryDropDown.DataSource = null;
            selectEntryDropDown.DataSource = _availableEntries;
            selectEntryDropDown.DisplayMember = nameof(EntryModel.EntryName);

            _selectedEntries = _selectedEntries.OrderBy(x => x.EntryName).ToList();
            tournamentEntriesListBox.DataSource = null;
            tournamentEntriesListBox.DataSource = _selectedEntries;
            tournamentEntriesListBox.DisplayMember = nameof(EntryModel.EntryName);
        }

        private void addSelectedEntryButton_Click(object sender, EventArgs e)
        {
            //PersonModel person = (PersonModel) selectPersonDropDown.SelectedItem;

            //if (person != null)
            //{
            //    _availablePeople.Remove(person);
            //    _selectedPeople.Add(person);

            //    WireUpLists();
            //}

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
    }
}
