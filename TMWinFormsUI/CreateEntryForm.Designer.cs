namespace TMWinFormsUI
{
    partial class CreateEntryForm
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
            this.entryNameValue = new System.Windows.Forms.TextBox();
            this.entryNameLabel = new System.Windows.Forms.Label();
            this.entryNameInfoLabel = new System.Windows.Forms.Label();
            this.addSelectedPersonButton = new System.Windows.Forms.Button();
            this.selectPersonLabel = new System.Windows.Forms.Label();
            this.selectPersonDropDown = new System.Windows.Forms.ComboBox();
            this.EntryMembersListBox = new System.Windows.Forms.ListBox();
            this.removeSelectedPersonButton = new System.Windows.Forms.Button();
            this.personCreatorGroupBox = new System.Windows.Forms.GroupBox();
            this.personCreatorCreateAndAddButton = new System.Windows.Forms.Button();
            this.personCreatorCreateButton = new System.Windows.Forms.Button();
            this.personCreatorEmailAddressValue = new System.Windows.Forms.TextBox();
            this.personCreatorEmailAddressLabel = new System.Windows.Forms.Label();
            this.personCreatorDiscordTagValue = new System.Windows.Forms.TextBox();
            this.personCreatorDiscordTagLabel = new System.Windows.Forms.Label();
            this.personCreatorLastNameValue = new System.Windows.Forms.TextBox();
            this.personCreatorLastNameLabel = new System.Windows.Forms.Label();
            this.personCreatorFirstNameValue = new System.Windows.Forms.TextBox();
            this.personCreatorFirstNameLabel = new System.Windows.Forms.Label();
            this.personCreatorNicknameValue = new System.Windows.Forms.TextBox();
            this.personCreatorNicknameLabel = new System.Windows.Forms.Label();
            this.createEntryButton = new System.Windows.Forms.Button();
            this.personCreatorGroupBox.SuspendLayout();
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
            this.headerLabel.Size = new System.Drawing.Size(228, 45);
            this.headerLabel.TabIndex = 4;
            this.headerLabel.Text = "Create entry";
            // 
            // entryNameValue
            // 
            this.entryNameValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.entryNameValue.ForeColor = System.Drawing.Color.White;
            this.entryNameValue.Location = new System.Drawing.Point(21, 121);
            this.entryNameValue.Name = "entryNameValue";
            this.entryNameValue.Size = new System.Drawing.Size(328, 40);
            this.entryNameValue.TabIndex = 11;
            this.entryNameValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.entryNameValue.TextChanged += new System.EventHandler(this.entryNameValue_TextChanged);
            // 
            // entryNameLabel
            // 
            this.entryNameLabel.AutoSize = true;
            this.entryNameLabel.Font = new System.Drawing.Font("Bahnschrift SemiLight", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entryNameLabel.Location = new System.Drawing.Point(109, 85);
            this.entryNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.entryNameLabel.Name = "entryNameLabel";
            this.entryNameLabel.Size = new System.Drawing.Size(153, 33);
            this.entryNameLabel.TabIndex = 10;
            this.entryNameLabel.Text = "Entry name";
            // 
            // entryNameInfoLabel
            // 
            this.entryNameInfoLabel.AutoSize = true;
            this.entryNameInfoLabel.Font = new System.Drawing.Font("Bahnschrift SemiLight", 12F, System.Drawing.FontStyle.Italic);
            this.entryNameInfoLabel.Location = new System.Drawing.Point(31, 164);
            this.entryNameInfoLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.entryNameInfoLabel.Name = "entryNameInfoLabel";
            this.entryNameInfoLabel.Size = new System.Drawing.Size(308, 19);
            this.entryNameInfoLabel.TabIndex = 12;
            this.entryNameInfoLabel.Text = "only for entries with more than 1 member";
            // 
            // addSelectedPersonButton
            // 
            this.addSelectedPersonButton.BackColor = System.Drawing.Color.White;
            this.addSelectedPersonButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.addSelectedPersonButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.addSelectedPersonButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.addSelectedPersonButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addSelectedPersonButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addSelectedPersonButton.ForeColor = System.Drawing.Color.Black;
            this.addSelectedPersonButton.Location = new System.Drawing.Point(89, 307);
            this.addSelectedPersonButton.Name = "addSelectedPersonButton";
            this.addSelectedPersonButton.Size = new System.Drawing.Size(192, 43);
            this.addSelectedPersonButton.TabIndex = 15;
            this.addSelectedPersonButton.Text = "Add selected person";
            this.addSelectedPersonButton.UseVisualStyleBackColor = false;
            this.addSelectedPersonButton.Click += new System.EventHandler(this.addSelectedEntryButton_Click);
            // 
            // selectPersonLabel
            // 
            this.selectPersonLabel.AutoSize = true;
            this.selectPersonLabel.Font = new System.Drawing.Font("Bahnschrift SemiLight", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectPersonLabel.Location = new System.Drawing.Point(94, 224);
            this.selectPersonLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.selectPersonLabel.Name = "selectPersonLabel";
            this.selectPersonLabel.Size = new System.Drawing.Size(182, 33);
            this.selectPersonLabel.TabIndex = 14;
            this.selectPersonLabel.Text = "Select person";
            this.selectPersonLabel.Click += new System.EventHandler(this.selectEntryLabel_Click);
            // 
            // selectPersonDropDown
            // 
            this.selectPersonDropDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.selectPersonDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectPersonDropDown.Font = new System.Drawing.Font("Bahnschrift", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectPersonDropDown.ForeColor = System.Drawing.Color.White;
            this.selectPersonDropDown.FormattingEnabled = true;
            this.selectPersonDropDown.Location = new System.Drawing.Point(54, 260);
            this.selectPersonDropDown.Name = "selectPersonDropDown";
            this.selectPersonDropDown.Size = new System.Drawing.Size(263, 41);
            this.selectPersonDropDown.TabIndex = 13;
            this.selectPersonDropDown.SelectedIndexChanged += new System.EventHandler(this.selectEntryDropDown_SelectedIndexChanged);
            // 
            // EntryMembersListBox
            // 
            this.EntryMembersListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.EntryMembersListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EntryMembersListBox.Font = new System.Drawing.Font("Bahnschrift Light SemiCondensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EntryMembersListBox.ForeColor = System.Drawing.Color.White;
            this.EntryMembersListBox.FormattingEnabled = true;
            this.EntryMembersListBox.ItemHeight = 29;
            this.EntryMembersListBox.Location = new System.Drawing.Point(37, 401);
            this.EntryMembersListBox.Name = "EntryMembersListBox";
            this.EntryMembersListBox.Size = new System.Drawing.Size(296, 118);
            this.EntryMembersListBox.TabIndex = 16;
            // 
            // removeSelectedPersonButton
            // 
            this.removeSelectedPersonButton.BackColor = System.Drawing.Color.White;
            this.removeSelectedPersonButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.removeSelectedPersonButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.removeSelectedPersonButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.removeSelectedPersonButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeSelectedPersonButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeSelectedPersonButton.ForeColor = System.Drawing.Color.Black;
            this.removeSelectedPersonButton.Location = new System.Drawing.Point(77, 525);
            this.removeSelectedPersonButton.Name = "removeSelectedPersonButton";
            this.removeSelectedPersonButton.Size = new System.Drawing.Size(217, 43);
            this.removeSelectedPersonButton.TabIndex = 17;
            this.removeSelectedPersonButton.Text = "Remove selected person";
            this.removeSelectedPersonButton.UseVisualStyleBackColor = false;
            // 
            // personCreatorGroupBox
            // 
            this.personCreatorGroupBox.Controls.Add(this.personCreatorCreateAndAddButton);
            this.personCreatorGroupBox.Controls.Add(this.personCreatorCreateButton);
            this.personCreatorGroupBox.Controls.Add(this.personCreatorEmailAddressValue);
            this.personCreatorGroupBox.Controls.Add(this.personCreatorEmailAddressLabel);
            this.personCreatorGroupBox.Controls.Add(this.personCreatorDiscordTagValue);
            this.personCreatorGroupBox.Controls.Add(this.personCreatorDiscordTagLabel);
            this.personCreatorGroupBox.Controls.Add(this.personCreatorLastNameValue);
            this.personCreatorGroupBox.Controls.Add(this.personCreatorLastNameLabel);
            this.personCreatorGroupBox.Controls.Add(this.personCreatorFirstNameValue);
            this.personCreatorGroupBox.Controls.Add(this.personCreatorFirstNameLabel);
            this.personCreatorGroupBox.Controls.Add(this.personCreatorNicknameValue);
            this.personCreatorGroupBox.Controls.Add(this.personCreatorNicknameLabel);
            this.personCreatorGroupBox.ForeColor = System.Drawing.Color.White;
            this.personCreatorGroupBox.Location = new System.Drawing.Point(393, 39);
            this.personCreatorGroupBox.Name = "personCreatorGroupBox";
            this.personCreatorGroupBox.Size = new System.Drawing.Size(427, 410);
            this.personCreatorGroupBox.TabIndex = 18;
            this.personCreatorGroupBox.TabStop = false;
            this.personCreatorGroupBox.Text = "Person creator";
            // 
            // personCreatorCreateAndAddButton
            // 
            this.personCreatorCreateAndAddButton.BackColor = System.Drawing.Color.White;
            this.personCreatorCreateAndAddButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.personCreatorCreateAndAddButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.personCreatorCreateAndAddButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.personCreatorCreateAndAddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.personCreatorCreateAndAddButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personCreatorCreateAndAddButton.ForeColor = System.Drawing.Color.Black;
            this.personCreatorCreateAndAddButton.Location = new System.Drawing.Point(216, 321);
            this.personCreatorCreateAndAddButton.Name = "personCreatorCreateAndAddButton";
            this.personCreatorCreateAndAddButton.Size = new System.Drawing.Size(192, 67);
            this.personCreatorCreateAndAddButton.TabIndex = 28;
            this.personCreatorCreateAndAddButton.Text = "Create and add";
            this.personCreatorCreateAndAddButton.UseVisualStyleBackColor = false;
            this.personCreatorCreateAndAddButton.Click += new System.EventHandler(this.personCreatorCreateAndAddButton_Click);
            // 
            // personCreatorCreateButton
            // 
            this.personCreatorCreateButton.BackColor = System.Drawing.Color.White;
            this.personCreatorCreateButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.personCreatorCreateButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.personCreatorCreateButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.personCreatorCreateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.personCreatorCreateButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personCreatorCreateButton.ForeColor = System.Drawing.Color.Black;
            this.personCreatorCreateButton.Location = new System.Drawing.Point(18, 321);
            this.personCreatorCreateButton.Name = "personCreatorCreateButton";
            this.personCreatorCreateButton.Size = new System.Drawing.Size(192, 67);
            this.personCreatorCreateButton.TabIndex = 19;
            this.personCreatorCreateButton.Text = "Create";
            this.personCreatorCreateButton.UseVisualStyleBackColor = false;
            this.personCreatorCreateButton.Click += new System.EventHandler(this.personCreatorCreateButton_Click);
            // 
            // personCreatorEmailAddressValue
            // 
            this.personCreatorEmailAddressValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.personCreatorEmailAddressValue.ForeColor = System.Drawing.Color.White;
            this.personCreatorEmailAddressValue.Location = new System.Drawing.Point(161, 251);
            this.personCreatorEmailAddressValue.Name = "personCreatorEmailAddressValue";
            this.personCreatorEmailAddressValue.Size = new System.Drawing.Size(247, 40);
            this.personCreatorEmailAddressValue.TabIndex = 26;
            // 
            // personCreatorEmailAddressLabel
            // 
            this.personCreatorEmailAddressLabel.AutoSize = true;
            this.personCreatorEmailAddressLabel.Font = new System.Drawing.Font("Bahnschrift SemiLight Condensed", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personCreatorEmailAddressLabel.Location = new System.Drawing.Point(16, 255);
            this.personCreatorEmailAddressLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.personCreatorEmailAddressLabel.Name = "personCreatorEmailAddressLabel";
            this.personCreatorEmailAddressLabel.Size = new System.Drawing.Size(141, 33);
            this.personCreatorEmailAddressLabel.TabIndex = 27;
            this.personCreatorEmailAddressLabel.Text = "Email address";
            // 
            // personCreatorDiscordTagValue
            // 
            this.personCreatorDiscordTagValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.personCreatorDiscordTagValue.ForeColor = System.Drawing.Color.White;
            this.personCreatorDiscordTagValue.Location = new System.Drawing.Point(161, 202);
            this.personCreatorDiscordTagValue.Name = "personCreatorDiscordTagValue";
            this.personCreatorDiscordTagValue.Size = new System.Drawing.Size(247, 40);
            this.personCreatorDiscordTagValue.TabIndex = 24;
            // 
            // personCreatorDiscordTagLabel
            // 
            this.personCreatorDiscordTagLabel.AutoSize = true;
            this.personCreatorDiscordTagLabel.Font = new System.Drawing.Font("Bahnschrift SemiLight Condensed", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personCreatorDiscordTagLabel.Location = new System.Drawing.Point(16, 206);
            this.personCreatorDiscordTagLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.personCreatorDiscordTagLabel.Name = "personCreatorDiscordTagLabel";
            this.personCreatorDiscordTagLabel.Size = new System.Drawing.Size(115, 33);
            this.personCreatorDiscordTagLabel.TabIndex = 25;
            this.personCreatorDiscordTagLabel.Text = "Discord tag";
            // 
            // personCreatorLastNameValue
            // 
            this.personCreatorLastNameValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.personCreatorLastNameValue.ForeColor = System.Drawing.Color.White;
            this.personCreatorLastNameValue.Location = new System.Drawing.Point(161, 153);
            this.personCreatorLastNameValue.Name = "personCreatorLastNameValue";
            this.personCreatorLastNameValue.Size = new System.Drawing.Size(247, 40);
            this.personCreatorLastNameValue.TabIndex = 22;
            // 
            // personCreatorLastNameLabel
            // 
            this.personCreatorLastNameLabel.AutoSize = true;
            this.personCreatorLastNameLabel.Font = new System.Drawing.Font("Bahnschrift SemiLight Condensed", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personCreatorLastNameLabel.Location = new System.Drawing.Point(16, 157);
            this.personCreatorLastNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.personCreatorLastNameLabel.Name = "personCreatorLastNameLabel";
            this.personCreatorLastNameLabel.Size = new System.Drawing.Size(108, 33);
            this.personCreatorLastNameLabel.TabIndex = 23;
            this.personCreatorLastNameLabel.Text = "Last name";
            // 
            // personCreatorFirstNameValue
            // 
            this.personCreatorFirstNameValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.personCreatorFirstNameValue.ForeColor = System.Drawing.Color.White;
            this.personCreatorFirstNameValue.Location = new System.Drawing.Point(161, 104);
            this.personCreatorFirstNameValue.Name = "personCreatorFirstNameValue";
            this.personCreatorFirstNameValue.Size = new System.Drawing.Size(247, 40);
            this.personCreatorFirstNameValue.TabIndex = 20;
            // 
            // personCreatorFirstNameLabel
            // 
            this.personCreatorFirstNameLabel.AutoSize = true;
            this.personCreatorFirstNameLabel.Font = new System.Drawing.Font("Bahnschrift SemiLight Condensed", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personCreatorFirstNameLabel.Location = new System.Drawing.Point(16, 108);
            this.personCreatorFirstNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.personCreatorFirstNameLabel.Name = "personCreatorFirstNameLabel";
            this.personCreatorFirstNameLabel.Size = new System.Drawing.Size(110, 33);
            this.personCreatorFirstNameLabel.TabIndex = 21;
            this.personCreatorFirstNameLabel.Text = "First name";
            // 
            // personCreatorNicknameValue
            // 
            this.personCreatorNicknameValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.personCreatorNicknameValue.ForeColor = System.Drawing.Color.White;
            this.personCreatorNicknameValue.Location = new System.Drawing.Point(161, 55);
            this.personCreatorNicknameValue.Name = "personCreatorNicknameValue";
            this.personCreatorNicknameValue.Size = new System.Drawing.Size(247, 40);
            this.personCreatorNicknameValue.TabIndex = 19;
            // 
            // personCreatorNicknameLabel
            // 
            this.personCreatorNicknameLabel.AutoSize = true;
            this.personCreatorNicknameLabel.Font = new System.Drawing.Font("Bahnschrift SemiLight Condensed", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personCreatorNicknameLabel.Location = new System.Drawing.Point(16, 59);
            this.personCreatorNicknameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.personCreatorNicknameLabel.Name = "personCreatorNicknameLabel";
            this.personCreatorNicknameLabel.Size = new System.Drawing.Size(103, 33);
            this.personCreatorNicknameLabel.TabIndex = 19;
            this.personCreatorNicknameLabel.Text = "Nickname";
            // 
            // createEntryButton
            // 
            this.createEntryButton.BackColor = System.Drawing.Color.White;
            this.createEntryButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.createEntryButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.createEntryButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.createEntryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createEntryButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createEntryButton.ForeColor = System.Drawing.Color.Black;
            this.createEntryButton.Location = new System.Drawing.Point(476, 490);
            this.createEntryButton.Name = "createEntryButton";
            this.createEntryButton.Size = new System.Drawing.Size(260, 65);
            this.createEntryButton.TabIndex = 19;
            this.createEntryButton.Text = "Create entry";
            this.createEntryButton.UseVisualStyleBackColor = false;
            // 
            // CreateEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(848, 608);
            this.Controls.Add(this.createEntryButton);
            this.Controls.Add(this.personCreatorGroupBox);
            this.Controls.Add(this.removeSelectedPersonButton);
            this.Controls.Add(this.EntryMembersListBox);
            this.Controls.Add(this.addSelectedPersonButton);
            this.Controls.Add(this.selectPersonLabel);
            this.Controls.Add(this.selectPersonDropDown);
            this.Controls.Add(this.entryNameInfoLabel);
            this.Controls.Add(this.entryNameValue);
            this.Controls.Add(this.entryNameLabel);
            this.Controls.Add(this.headerLabel);
            this.Font = new System.Drawing.Font("Bahnschrift SemiLight", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "CreateEntryForm";
            this.Text = "Create Team";
            this.Load += new System.EventHandler(this.CreateEntryForm_Load);
            this.personCreatorGroupBox.ResumeLayout(false);
            this.personCreatorGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.TextBox entryNameValue;
        private System.Windows.Forms.Label entryNameLabel;
        private System.Windows.Forms.Label entryNameInfoLabel;
        private System.Windows.Forms.Button addSelectedPersonButton;
        private System.Windows.Forms.Label selectPersonLabel;
        private System.Windows.Forms.ComboBox selectPersonDropDown;
        private System.Windows.Forms.ListBox EntryMembersListBox;
        private System.Windows.Forms.Button removeSelectedPersonButton;
        private System.Windows.Forms.GroupBox personCreatorGroupBox;
        private System.Windows.Forms.Button personCreatorCreateAndAddButton;
        private System.Windows.Forms.Button personCreatorCreateButton;
        private System.Windows.Forms.TextBox personCreatorEmailAddressValue;
        private System.Windows.Forms.Label personCreatorEmailAddressLabel;
        private System.Windows.Forms.TextBox personCreatorDiscordTagValue;
        private System.Windows.Forms.Label personCreatorDiscordTagLabel;
        private System.Windows.Forms.TextBox personCreatorLastNameValue;
        private System.Windows.Forms.Label personCreatorLastNameLabel;
        private System.Windows.Forms.TextBox personCreatorFirstNameValue;
        private System.Windows.Forms.Label personCreatorFirstNameLabel;
        private System.Windows.Forms.TextBox personCreatorNicknameValue;
        private System.Windows.Forms.Label personCreatorNicknameLabel;
        private System.Windows.Forms.Button createEntryButton;
    }
}