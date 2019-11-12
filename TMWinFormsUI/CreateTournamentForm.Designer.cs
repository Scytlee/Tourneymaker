namespace TMWinFormsUI
{
    partial class CreateTournamentForm
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
            this.headerLabel = new System.Windows.Forms.Label();
            this.tournamentNameLabel = new System.Windows.Forms.Label();
            this.tournamentNameValue = new System.Windows.Forms.TextBox();
            this.selectEntryDropDown = new System.Windows.Forms.ComboBox();
            this.selectEntryLabel = new System.Windows.Forms.Label();
            this.addSelectedEntryButton = new System.Windows.Forms.Button();
            this.createNewEntryButton = new System.Windows.Forms.Button();
            this.tournamentEntriesListBox = new System.Windows.Forms.ListBox();
            this.tournamentEntriesLabel = new System.Windows.Forms.Label();
            this.removeSelectedEntryButton = new System.Windows.Forms.Button();
            this.createTournamentButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Bahnschrift SemiBold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLabel.ForeColor = System.Drawing.Color.White;
            this.headerLabel.Location = new System.Drawing.Point(13, 9);
            this.headerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(336, 45);
            this.headerLabel.TabIndex = 3;
            this.headerLabel.Text = "Create tournament";
            // 
            // tournamentNameLabel
            // 
            this.tournamentNameLabel.AutoSize = true;
            this.tournamentNameLabel.Font = new System.Drawing.Font("Bahnschrift SemiLight", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tournamentNameLabel.Location = new System.Drawing.Point(65, 85);
            this.tournamentNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tournamentNameLabel.Name = "tournamentNameLabel";
            this.tournamentNameLabel.Size = new System.Drawing.Size(232, 33);
            this.tournamentNameLabel.TabIndex = 4;
            this.tournamentNameLabel.Text = "Tournament name";
            // 
            // tournamentNameValue
            // 
            this.tournamentNameValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.tournamentNameValue.ForeColor = System.Drawing.Color.White;
            this.tournamentNameValue.Location = new System.Drawing.Point(17, 121);
            this.tournamentNameValue.MaxLength = 100;
            this.tournamentNameValue.Name = "tournamentNameValue";
            this.tournamentNameValue.Size = new System.Drawing.Size(328, 40);
            this.tournamentNameValue.TabIndex = 9;
            this.tournamentNameValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // selectEntryDropDown
            // 
            this.selectEntryDropDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.selectEntryDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectEntryDropDown.Font = new System.Drawing.Font("Bahnschrift", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectEntryDropDown.ForeColor = System.Drawing.Color.White;
            this.selectEntryDropDown.FormattingEnabled = true;
            this.selectEntryDropDown.Location = new System.Drawing.Point(17, 231);
            this.selectEntryDropDown.Name = "selectEntryDropDown";
            this.selectEntryDropDown.Size = new System.Drawing.Size(328, 41);
            this.selectEntryDropDown.TabIndex = 10;
            // 
            // selectEntryLabel
            // 
            this.selectEntryLabel.AutoSize = true;
            this.selectEntryLabel.Font = new System.Drawing.Font("Bahnschrift SemiLight", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectEntryLabel.Location = new System.Drawing.Point(101, 195);
            this.selectEntryLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.selectEntryLabel.Name = "selectEntryLabel";
            this.selectEntryLabel.Size = new System.Drawing.Size(160, 33);
            this.selectEntryLabel.TabIndex = 11;
            this.selectEntryLabel.Text = "Select entry";
            // 
            // addSelectedEntryButton
            // 
            this.addSelectedEntryButton.BackColor = System.Drawing.Color.White;
            this.addSelectedEntryButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.addSelectedEntryButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.addSelectedEntryButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.addSelectedEntryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addSelectedEntryButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addSelectedEntryButton.ForeColor = System.Drawing.Color.Black;
            this.addSelectedEntryButton.Location = new System.Drawing.Point(91, 278);
            this.addSelectedEntryButton.Name = "addSelectedEntryButton";
            this.addSelectedEntryButton.Size = new System.Drawing.Size(181, 43);
            this.addSelectedEntryButton.TabIndex = 12;
            this.addSelectedEntryButton.Text = "Add selected entry";
            this.addSelectedEntryButton.UseVisualStyleBackColor = false;
            this.addSelectedEntryButton.Click += new System.EventHandler(this.addSelectedEntryButton_Click);
            // 
            // createNewEntryButton
            // 
            this.createNewEntryButton.BackColor = System.Drawing.Color.White;
            this.createNewEntryButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.createNewEntryButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.createNewEntryButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.createNewEntryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createNewEntryButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createNewEntryButton.ForeColor = System.Drawing.Color.Black;
            this.createNewEntryButton.Location = new System.Drawing.Point(101, 369);
            this.createNewEntryButton.Name = "createNewEntryButton";
            this.createNewEntryButton.Size = new System.Drawing.Size(160, 43);
            this.createNewEntryButton.TabIndex = 13;
            this.createNewEntryButton.Text = "Create new entry";
            this.createNewEntryButton.UseVisualStyleBackColor = false;
            this.createNewEntryButton.Click += new System.EventHandler(this.createNewEntryButton_Click);
            // 
            // tournamentEntriesListBox
            // 
            this.tournamentEntriesListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.tournamentEntriesListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tournamentEntriesListBox.Font = new System.Drawing.Font("Bahnschrift Light SemiCondensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tournamentEntriesListBox.ForeColor = System.Drawing.Color.White;
            this.tournamentEntriesListBox.FormattingEnabled = true;
            this.tournamentEntriesListBox.ItemHeight = 29;
            this.tournamentEntriesListBox.Location = new System.Drawing.Point(399, 91);
            this.tournamentEntriesListBox.Name = "tournamentEntriesListBox";
            this.tournamentEntriesListBox.Size = new System.Drawing.Size(333, 321);
            this.tournamentEntriesListBox.TabIndex = 14;
            // 
            // tournamentEntriesLabel
            // 
            this.tournamentEntriesLabel.AutoSize = true;
            this.tournamentEntriesLabel.Font = new System.Drawing.Font("Bahnschrift SemiLight", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tournamentEntriesLabel.Location = new System.Drawing.Point(440, 55);
            this.tournamentEntriesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tournamentEntriesLabel.Name = "tournamentEntriesLabel";
            this.tournamentEntriesLabel.Size = new System.Drawing.Size(251, 33);
            this.tournamentEntriesLabel.TabIndex = 15;
            this.tournamentEntriesLabel.Text = "Tournament entries";
            // 
            // removeSelectedEntryButton
            // 
            this.removeSelectedEntryButton.BackColor = System.Drawing.Color.White;
            this.removeSelectedEntryButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.removeSelectedEntryButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.removeSelectedEntryButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.removeSelectedEntryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeSelectedEntryButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeSelectedEntryButton.ForeColor = System.Drawing.Color.Black;
            this.removeSelectedEntryButton.Location = new System.Drawing.Point(738, 91);
            this.removeSelectedEntryButton.Name = "removeSelectedEntryButton";
            this.removeSelectedEntryButton.Size = new System.Drawing.Size(138, 78);
            this.removeSelectedEntryButton.TabIndex = 16;
            this.removeSelectedEntryButton.Text = "Remove selected entry";
            this.removeSelectedEntryButton.UseVisualStyleBackColor = false;
            this.removeSelectedEntryButton.Click += new System.EventHandler(this.removeSelectedEntryButton_Click);
            // 
            // createTournamentButton
            // 
            this.createTournamentButton.BackColor = System.Drawing.Color.White;
            this.createTournamentButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.createTournamentButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.createTournamentButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.createTournamentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createTournamentButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createTournamentButton.ForeColor = System.Drawing.Color.Black;
            this.createTournamentButton.Location = new System.Drawing.Point(435, 427);
            this.createTournamentButton.Name = "createTournamentButton";
            this.createTournamentButton.Size = new System.Drawing.Size(260, 65);
            this.createTournamentButton.TabIndex = 17;
            this.createTournamentButton.Text = "Create tournament";
            this.createTournamentButton.UseVisualStyleBackColor = false;
            this.createTournamentButton.Click += new System.EventHandler(this.createTournamentButton_Click);
            // 
            // CreateTournamentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(899, 517);
            this.Controls.Add(this.createTournamentButton);
            this.Controls.Add(this.removeSelectedEntryButton);
            this.Controls.Add(this.tournamentEntriesLabel);
            this.Controls.Add(this.tournamentEntriesListBox);
            this.Controls.Add(this.createNewEntryButton);
            this.Controls.Add(this.addSelectedEntryButton);
            this.Controls.Add(this.selectEntryLabel);
            this.Controls.Add(this.selectEntryDropDown);
            this.Controls.Add(this.tournamentNameValue);
            this.Controls.Add(this.tournamentNameLabel);
            this.Controls.Add(this.headerLabel);
            this.Font = new System.Drawing.Font("Bahnschrift SemiLight", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "CreateTournamentForm";
            this.Text = "Create Tournament";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Label tournamentNameLabel;
        private System.Windows.Forms.TextBox tournamentNameValue;
        private System.Windows.Forms.ComboBox selectEntryDropDown;
        private System.Windows.Forms.Label selectEntryLabel;
        private System.Windows.Forms.Button addSelectedEntryButton;
        private System.Windows.Forms.Button createNewEntryButton;
        private System.Windows.Forms.ListBox tournamentEntriesListBox;
        private System.Windows.Forms.Label tournamentEntriesLabel;
        private System.Windows.Forms.Button removeSelectedEntryButton;
        private System.Windows.Forms.Button createTournamentButton;
    }
}