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
    public partial class TournamentDashboardForm : Form
    {
        private List<TournamentModel> _activeTournaments = GlobalConfig.Connection.LoadActiveTournamentModels();

        public TournamentDashboardForm()
        {
            InitializeComponent();

            WireUpLists();
        }

        private void WireUpLists()
        {
            loadTournamentDropDown.DataSource = _activeTournaments;
            loadTournamentDropDown.DisplayMember = nameof(TournamentModel.TournamentName);
        }

        private void createNewTournamentButton_Click(object sender, EventArgs e)
        {
            CreateTournamentForm form = new CreateTournamentForm();
            form.Show();
        }

        private void loadTournamentButton_Click(object sender, EventArgs e)
        {
            TournamentModel selectedTournament = (TournamentModel) loadTournamentDropDown.SelectedItem;
            TournamentViewerForm form = new TournamentViewerForm(selectedTournament);
            form.Show();
        }
    }
}
