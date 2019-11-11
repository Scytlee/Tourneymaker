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
    public partial class TournamentViewerForm : Form
    {
        private TournamentModel _activeTournament;
        private List<int> _rounds;
        private List<MatchupModel> _matchups = new List<MatchupModel>();

        public TournamentViewerForm(TournamentModel tournament)
        {
            InitializeComponent();

            _activeTournament = tournament;

            LoadFormData();

            LoadRounds();
        }

        private void LoadFormData()
        {
            tournamentNameLabel.Text = _activeTournament.TournamentName;
        }

        private void WireUpRoundList()
        {
            selectedRoundDropDown.DataSource = null;
            selectedRoundDropDown.DataSource = _rounds;
        }

        private void WireUpMatchupList()
        {
            matchupsListBox.DataSource = null;
            matchupsListBox.DataSource = _matchups;
            matchupsListBox.DisplayMember = nameof(MatchupModel.DisplayName);
        }

        private void LoadRounds()
        {
            _rounds = new List<int>();

            for (int i = 1; i <= _activeTournament.Rounds.Count; i++)
            {
                _rounds.Add(i);
            }

            WireUpRoundList();
        }

        private void selectedRoundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchups(showUnplayedOnlyCheckBox.Checked);
        }

        private void LoadMatchups(bool unplayedOnly)
        {
            if (selectedRoundDropDown.SelectedItem == null)
            {
                return;
            }

            int round = (int) selectedRoundDropDown.SelectedItem;

            if (unplayedOnly)
            {
                List<MatchupModel> matchups = new List<MatchupModel>();

                foreach (MatchupModel matchup in _activeTournament.Rounds[round - 1])
                {
                    if (matchup.Winner == null)
                    {
                        matchups.Add(matchup);
                    }
                }

                _matchups = matchups;
            }
            else
            {
                _matchups = _activeTournament.Rounds[round - 1];
            }

            if (_matchups.Count == 0)
            {
                HideAll();
            }

            WireUpMatchupList();
        }

        private void HideElementsBye()
        {
            entryTwoNameLabel.Hide();
            entryTwoScoreValue.Hide();
            versusLabel.Hide();
            updateScoreButton.Hide();
        }

        private void HideElementsTBD()
        {
            entryOneScoreValue.Hide();
            entryTwoScoreValue.Hide();
            updateScoreButton.Hide();
        }

        private void HideAll()
        {
            entryOneNameLabel.Hide();
            entryOneScoreValue.Hide();
            entryTwoNameLabel.Hide();
            entryTwoScoreValue.Hide();
            versusLabel.Hide();
            updateScoreButton.Hide();
        }
        private void ShowAll()
        {
            entryOneNameLabel.Show();
            entryOneScoreValue.Show();
            entryTwoNameLabel.Show();
            entryTwoScoreValue.Show();
            versusLabel.Show();
            updateScoreButton.Show();
        }

        private void LoadMatchup()
        {
            if (matchupsListBox.SelectedItem == null)
            {
                return;
            }

            ShowAll();

            MatchupModel matchup = (MatchupModel) matchupsListBox.SelectedItem;

            if (matchup.MatchupEntries.Count == 1)
            {
                // Bye matchup
                entryOneNameLabel.Text = matchup.MatchupEntries[0].EntryCompeting.EntryName;
                entryOneScoreValue.Text = "bye";
                HideElementsBye();
            }
            else
            {
                for (int i = 0; i < matchup.MatchupEntries.Count; i++)
                {
                    if (i == 0)
                    {
                        if (matchup.MatchupEntries[0].EntryCompeting != null)
                        {
                            entryOneNameLabel.Text = matchup.MatchupEntries[0].EntryCompeting.EntryName;
                            entryOneScoreValue.Text = matchup.MatchupEntries[0].Score.ToString();
                        }
                        else
                        {
                            HideElementsTBD();
                            entryOneNameLabel.Text = "TBD";
                        }
                    }

                    if (i == 1)
                    {
                        if (matchup.MatchupEntries[1].EntryCompeting != null)
                        {
                            entryTwoNameLabel.Text = matchup.MatchupEntries[1].EntryCompeting.EntryName;
                            entryTwoScoreValue.Text = matchup.MatchupEntries[1].Score.ToString();
                        }
                        else
                        {
                            HideElementsTBD();
                            entryTwoNameLabel.Text = "TBD";
                        }
                    }
                } 
            }
        }

        private void matchupsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchup();
        }

        private void showUnplayedOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatchups(showUnplayedOnlyCheckBox.Checked);
        }

        private void updateScoreButton_Click(object sender, EventArgs e)
        {
            MatchupModel matchup = (MatchupModel) matchupsListBox.SelectedItem;

            // Validate the data in the form
            if (ValidationHelper.ValidateUpdateScoreForm(out string errorMessage, entryOneScoreValue.Text, entryTwoScoreValue.Text))
            {
                // Update tournament
                TournamentLogic.UpdateTournament(_activeTournament, matchup, entryOneScoreValue.Text, entryTwoScoreValue.Text);

                LoadMatchups(showUnplayedOnlyCheckBox.Checked);
            }
            else
            {
                // Show error message
                MessageBox.Show($"The following errors exist in the form:\n{ errorMessage }", "Update error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
