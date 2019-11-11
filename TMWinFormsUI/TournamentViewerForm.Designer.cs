namespace TMWinFormsUI
{
    partial class TournamentViewerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tournamentNameLabel = new System.Windows.Forms.Label();
            this.selectedRoundLabel = new System.Windows.Forms.Label();
            this.selectedRoundDropDown = new System.Windows.Forms.ComboBox();
            this.showUnplayedOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.matchupsListBox = new System.Windows.Forms.ListBox();
            this.entryTwoNameLabel = new System.Windows.Forms.Label();
            this.entryOneNameLabel = new System.Windows.Forms.Label();
            this.entryOneScoreValue = new System.Windows.Forms.TextBox();
            this.entryTwoScoreValue = new System.Windows.Forms.TextBox();
            this.versusLabel = new System.Windows.Forms.Label();
            this.updateScoreButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tournamentNameLabel
            // 
            this.tournamentNameLabel.AutoSize = true;
            this.tournamentNameLabel.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 32.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tournamentNameLabel.ForeColor = System.Drawing.Color.White;
            this.tournamentNameLabel.Location = new System.Drawing.Point(13, 9);
            this.tournamentNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tournamentNameLabel.Name = "tournamentNameLabel";
            this.tournamentNameLabel.Size = new System.Drawing.Size(349, 52);
            this.tournamentNameLabel.TabIndex = 0;
            this.tournamentNameLabel.Text = "<TournamentName>";
            // 
            // selectedRoundLabel
            // 
            this.selectedRoundLabel.AutoSize = true;
            this.selectedRoundLabel.Font = new System.Drawing.Font("Bahnschrift SemiLight", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedRoundLabel.Location = new System.Drawing.Point(43, 89);
            this.selectedRoundLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.selectedRoundLabel.Name = "selectedRoundLabel";
            this.selectedRoundLabel.Size = new System.Drawing.Size(198, 33);
            this.selectedRoundLabel.TabIndex = 1;
            this.selectedRoundLabel.Text = "Selected round";
            // 
            // selectedRoundDropDown
            // 
            this.selectedRoundDropDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.selectedRoundDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectedRoundDropDown.Font = new System.Drawing.Font("Bahnschrift", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedRoundDropDown.ForeColor = System.Drawing.Color.White;
            this.selectedRoundDropDown.FormattingEnabled = true;
            this.selectedRoundDropDown.Location = new System.Drawing.Point(248, 86);
            this.selectedRoundDropDown.Name = "selectedRoundDropDown";
            this.selectedRoundDropDown.Size = new System.Drawing.Size(72, 41);
            this.selectedRoundDropDown.TabIndex = 2;
            this.selectedRoundDropDown.SelectedIndexChanged += new System.EventHandler(this.selectedRoundDropDown_SelectedIndexChanged);
            // 
            // showUnplayedOnlyCheckBox
            // 
            this.showUnplayedOnlyCheckBox.AutoSize = true;
            this.showUnplayedOnlyCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.showUnplayedOnlyCheckBox.Font = new System.Drawing.Font("Bahnschrift SemiLight", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showUnplayedOnlyCheckBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.showUnplayedOnlyCheckBox.Location = new System.Drawing.Point(72, 134);
            this.showUnplayedOnlyCheckBox.Name = "showUnplayedOnlyCheckBox";
            this.showUnplayedOnlyCheckBox.Size = new System.Drawing.Size(218, 29);
            this.showUnplayedOnlyCheckBox.TabIndex = 3;
            this.showUnplayedOnlyCheckBox.Text = "Show unplayed only";
            this.showUnplayedOnlyCheckBox.UseVisualStyleBackColor = false;
            this.showUnplayedOnlyCheckBox.CheckedChanged += new System.EventHandler(this.showUnplayedOnlyCheckBox_CheckedChanged);
            // 
            // matchupsListBox
            // 
            this.matchupsListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.matchupsListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.matchupsListBox.Font = new System.Drawing.Font("Bahnschrift Light SemiCondensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.matchupsListBox.ForeColor = System.Drawing.Color.White;
            this.matchupsListBox.FormattingEnabled = true;
            this.matchupsListBox.ItemHeight = 29;
            this.matchupsListBox.Location = new System.Drawing.Point(22, 181);
            this.matchupsListBox.Name = "matchupsListBox";
            this.matchupsListBox.Size = new System.Drawing.Size(435, 321);
            this.matchupsListBox.TabIndex = 4;
            this.matchupsListBox.SelectedIndexChanged += new System.EventHandler(this.matchupsListBox_SelectedIndexChanged);
            // 
            // entryTwoNameLabel
            // 
            this.entryTwoNameLabel.Font = new System.Drawing.Font("Bahnschrift SemiLight", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entryTwoNameLabel.Location = new System.Drawing.Point(480, 428);
            this.entryTwoNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.entryTwoNameLabel.Name = "entryTwoNameLabel";
            this.entryTwoNameLabel.Size = new System.Drawing.Size(441, 39);
            this.entryTwoNameLabel.TabIndex = 6;
            this.entryTwoNameLabel.Text = "<Team2>";
            this.entryTwoNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // entryOneNameLabel
            // 
            this.entryOneNameLabel.Font = new System.Drawing.Font("Bahnschrift SemiLight", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entryOneNameLabel.Location = new System.Drawing.Point(480, 216);
            this.entryOneNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.entryOneNameLabel.Name = "entryOneNameLabel";
            this.entryOneNameLabel.Size = new System.Drawing.Size(441, 39);
            this.entryOneNameLabel.TabIndex = 7;
            this.entryOneNameLabel.Text = "<Team1>";
            this.entryOneNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // entryOneScoreValue
            // 
            this.entryOneScoreValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.entryOneScoreValue.ForeColor = System.Drawing.Color.White;
            this.entryOneScoreValue.Location = new System.Drawing.Point(664, 258);
            this.entryOneScoreValue.Name = "entryOneScoreValue";
            this.entryOneScoreValue.Size = new System.Drawing.Size(72, 40);
            this.entryOneScoreValue.TabIndex = 8;
            this.entryOneScoreValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // entryTwoScoreValue
            // 
            this.entryTwoScoreValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.entryTwoScoreValue.ForeColor = System.Drawing.Color.White;
            this.entryTwoScoreValue.Location = new System.Drawing.Point(664, 385);
            this.entryTwoScoreValue.Name = "entryTwoScoreValue";
            this.entryTwoScoreValue.Size = new System.Drawing.Size(72, 40);
            this.entryTwoScoreValue.TabIndex = 9;
            this.entryTwoScoreValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // versusLabel
            // 
            this.versusLabel.AutoSize = true;
            this.versusLabel.Font = new System.Drawing.Font("Bahnschrift SemiLight SemiConde", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versusLabel.Location = new System.Drawing.Point(685, 329);
            this.versusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.versusLabel.Name = "versusLabel";
            this.versusLabel.Size = new System.Drawing.Size(30, 25);
            this.versusLabel.TabIndex = 10;
            this.versusLabel.Text = "vs";
            // 
            // updateScoreButton
            // 
            this.updateScoreButton.BackColor = System.Drawing.Color.White;
            this.updateScoreButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.updateScoreButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.updateScoreButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.updateScoreButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateScoreButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateScoreButton.ForeColor = System.Drawing.Color.Black;
            this.updateScoreButton.Location = new System.Drawing.Point(783, 320);
            this.updateScoreButton.Name = "updateScoreButton";
            this.updateScoreButton.Size = new System.Drawing.Size(127, 43);
            this.updateScoreButton.TabIndex = 11;
            this.updateScoreButton.Text = "Update score";
            this.updateScoreButton.UseVisualStyleBackColor = false;
            this.updateScoreButton.Click += new System.EventHandler(this.updateScoreButton_Click);
            // 
            // TournamentViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(937, 526);
            this.Controls.Add(this.updateScoreButton);
            this.Controls.Add(this.versusLabel);
            this.Controls.Add(this.entryTwoScoreValue);
            this.Controls.Add(this.entryOneScoreValue);
            this.Controls.Add(this.entryOneNameLabel);
            this.Controls.Add(this.entryTwoNameLabel);
            this.Controls.Add(this.matchupsListBox);
            this.Controls.Add(this.showUnplayedOnlyCheckBox);
            this.Controls.Add(this.selectedRoundDropDown);
            this.Controls.Add(this.selectedRoundLabel);
            this.Controls.Add(this.tournamentNameLabel);
            this.Font = new System.Drawing.Font("Bahnschrift SemiLight", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "TournamentViewerForm";
            this.Text = "Tournament Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tournamentNameLabel;
        private System.Windows.Forms.Label selectedRoundLabel;
        private System.Windows.Forms.ComboBox selectedRoundDropDown;
        private System.Windows.Forms.CheckBox showUnplayedOnlyCheckBox;
        private System.Windows.Forms.ListBox matchupsListBox;
        private System.Windows.Forms.Label entryTwoNameLabel;
        private System.Windows.Forms.Label entryOneNameLabel;
        private System.Windows.Forms.TextBox entryOneScoreValue;
        private System.Windows.Forms.TextBox entryTwoScoreValue;
        private System.Windows.Forms.Label versusLabel;
        private System.Windows.Forms.Button updateScoreButton;
    }
}

