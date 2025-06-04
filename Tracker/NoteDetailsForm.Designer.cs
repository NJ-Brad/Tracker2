namespace Tracker
{
    partial class NoteDetailsForm
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
            label1 = new Label();
            textBoxNoteText = new TextBox();
            label2 = new Label();
            textBoxTags = new TextBox();
            textBoxFollowUp = new TextBox();
            label3 = new Label();
            textBoxCompleted = new TextBox();
            label4 = new Label();
            buttonMarkComplete = new Button();
            button2 = new Button();
            button3 = new Button();
            linkLabel1 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            linkLabel3 = new LinkLabel();
            textBoxTeam = new TextBox();
            labelTeam = new Label();
            textBoxID = new TextBox();
            label6 = new Label();
            textBoxMeeting = new TextBox();
            label5 = new Label();
            textBoxCreated = new TextBox();
            label7 = new Label();
            label8 = new Label();
            flagSelectionControl1 = new FlagSelectionControl();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(106, 137);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(46, 25);
            label1.TabIndex = 8;
            label1.Text = "Text:";
            // 
            // textBoxNoteText
            // 
            textBoxNoteText.Location = new Point(162, 133);
            textBoxNoteText.Margin = new Padding(4);
            textBoxNoteText.Name = "textBoxNoteText";
            textBoxNoteText.Size = new Size(729, 31);
            textBoxNoteText.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(106, 225);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(47, 25);
            label2.TabIndex = 10;
            label2.Text = "Tags";
            // 
            // textBoxTags
            // 
            textBoxTags.Location = new Point(161, 221);
            textBoxTags.Margin = new Padding(4);
            textBoxTags.Name = "textBoxTags";
            textBoxTags.Size = new Size(729, 31);
            textBoxTags.TabIndex = 11;
            // 
            // textBoxFollowUp
            // 
            textBoxFollowUp.Location = new Point(162, 260);
            textBoxFollowUp.Margin = new Padding(4);
            textBoxFollowUp.Multiline = true;
            textBoxFollowUp.Name = "textBoxFollowUp";
            textBoxFollowUp.Size = new Size(729, 56);
            textBoxFollowUp.TabIndex = 16;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(60, 293);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(92, 25);
            label3.TabIndex = 15;
            label3.Text = "Follow Up";
            // 
            // textBoxCompleted
            // 
            textBoxCompleted.Location = new Point(162, 330);
            textBoxCompleted.Margin = new Padding(4);
            textBoxCompleted.Name = "textBoxCompleted";
            textBoxCompleted.ReadOnly = true;
            textBoxCompleted.Size = new Size(190, 31);
            textBoxCompleted.TabIndex = 18;
            textBoxCompleted.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(51, 334);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(100, 25);
            label4.TabIndex = 17;
            label4.Text = "Completed";
            // 
            // buttonMarkComplete
            // 
            buttonMarkComplete.Location = new Point(360, 328);
            buttonMarkComplete.Margin = new Padding(4);
            buttonMarkComplete.Name = "buttonMarkComplete";
            buttonMarkComplete.Size = new Size(158, 36);
            buttonMarkComplete.TabIndex = 19;
            buttonMarkComplete.Text = "Mark Complete";
            buttonMarkComplete.UseVisualStyleBackColor = true;
            buttonMarkComplete.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(647, 376);
            button2.Margin = new Padding(4);
            button2.Name = "button2";
            button2.Size = new Size(118, 36);
            button2.TabIndex = 20;
            button2.Text = "OK";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(772, 376);
            button3.Margin = new Padding(4);
            button3.Name = "button3";
            button3.Size = new Size(118, 36);
            button3.TabIndex = 21;
            button3.Text = "Cancel";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(25, 176);
            linkLabel1.Margin = new Padding(4, 0, 4, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(46, 25);
            linkLabel1.TabIndex = 12;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Idea";
            linkLabel1.Visible = false;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new Point(13, 210);
            linkLabel2.Margin = new Padding(4, 0, 4, 0);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(58, 25);
            linkLabel2.TabIndex = 13;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "I Owe";
            linkLabel2.Visible = false;
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // linkLabel3
            // 
            linkLabel3.AutoSize = true;
            linkLabel3.Location = new Point(-2, 244);
            linkLabel3.Margin = new Padding(4, 0, 4, 0);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Size = new Size(73, 25);
            linkLabel3.TabIndex = 14;
            linkLabel3.TabStop = true;
            linkLabel3.Text = "I Expect";
            linkLabel3.Visible = false;
            linkLabel3.LinkClicked += linkLabel3_LinkClicked;
            // 
            // textBoxTeam
            // 
            textBoxTeam.Location = new Point(161, 54);
            textBoxTeam.Margin = new Padding(4);
            textBoxTeam.Name = "textBoxTeam";
            textBoxTeam.Size = new Size(729, 31);
            textBoxTeam.TabIndex = 5;
            // 
            // labelTeam
            // 
            labelTeam.AutoSize = true;
            labelTeam.Location = new Point(82, 60);
            labelTeam.Margin = new Padding(4, 0, 4, 0);
            labelTeam.Name = "labelTeam";
            labelTeam.Size = new Size(57, 25);
            labelTeam.TabIndex = 4;
            labelTeam.Text = "Team:";
            // 
            // textBoxID
            // 
            textBoxID.Location = new Point(161, 13);
            textBoxID.Margin = new Padding(4);
            textBoxID.Name = "textBoxID";
            textBoxID.ReadOnly = true;
            textBoxID.Size = new Size(288, 31);
            textBoxID.TabIndex = 1;
            textBoxID.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(105, 17);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(34, 25);
            label6.TabIndex = 0;
            label6.Text = "ID:";
            // 
            // textBoxMeeting
            // 
            textBoxMeeting.Location = new Point(161, 94);
            textBoxMeeting.Margin = new Padding(4);
            textBoxMeeting.Name = "textBoxMeeting";
            textBoxMeeting.Size = new Size(729, 31);
            textBoxMeeting.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(60, 97);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(81, 25);
            label5.TabIndex = 6;
            label5.Text = "Meeting:";
            // 
            // textBoxCreated
            // 
            textBoxCreated.Location = new Point(613, 13);
            textBoxCreated.Margin = new Padding(4);
            textBoxCreated.Name = "textBoxCreated";
            textBoxCreated.ReadOnly = true;
            textBoxCreated.Size = new Size(190, 31);
            textBoxCreated.TabIndex = 3;
            textBoxCreated.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(528, 16);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(77, 25);
            label7.TabIndex = 2;
            label7.Text = "Created:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(102, 176);
            label8.Name = "label8";
            label8.Size = new Size(49, 25);
            label8.TabIndex = 22;
            label8.Text = "Flag:";
            // 
            // flagSelectionControl1
            // 
            flagSelectionControl1.FlagValue = "None";
            flagSelectionControl1.Location = new Point(162, 171);
            flagSelectionControl1.Name = "flagSelectionControl1";
            flagSelectionControl1.Size = new Size(467, 45);
            flagSelectionControl1.TabIndex = 27;
            flagSelectionControl1.FlagChanged += flagSelectionControl1_FlagChanged;
            // 
            // button1
            // 
            button1.Location = new Point(521, 376);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(118, 36);
            button1.TabIndex = 28;
            button1.Text = "Save && New";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // NoteDetailsForm
            // 
            AcceptButton = button2;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button3;
            ClientSize = new Size(904, 425);
            Controls.Add(button1);
            Controls.Add(flagSelectionControl1);
            Controls.Add(label8);
            Controls.Add(textBoxCreated);
            Controls.Add(label7);
            Controls.Add(textBoxMeeting);
            Controls.Add(label5);
            Controls.Add(textBoxTeam);
            Controls.Add(labelTeam);
            Controls.Add(textBoxID);
            Controls.Add(label6);
            Controls.Add(linkLabel3);
            Controls.Add(linkLabel2);
            Controls.Add(linkLabel1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(buttonMarkComplete);
            Controls.Add(textBoxCompleted);
            Controls.Add(label4);
            Controls.Add(textBoxFollowUp);
            Controls.Add(label3);
            Controls.Add(textBoxTags);
            Controls.Add(label2);
            Controls.Add(textBoxNoteText);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NoteDetailsForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Note Details";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBoxNoteText;
        private Label label2;
        private TextBox textBoxTags;
        private TextBox textBoxFollowUp;
        private Label label3;
        private TextBox textBoxCompleted;
        private Label label4;
        private Button buttonMarkComplete;
        private Button button2;
        private Button button3;
        private LinkLabel linkLabel1;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel3;
        private TextBox textBoxTeam;
        private Label labelTeam;
        private TextBox textBoxID;
        private Label label6;
        private TextBox textBoxMeeting;
        private Label label5;
        private TextBox textBoxCreated;
        private Label label7;
        private Label label8;
        private FlagSelectionControl flagSelectionControl1;
        private Button button1;
    }
}