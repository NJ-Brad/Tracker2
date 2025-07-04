﻿using System.ComponentModel;
using Tracker.Models;

namespace Tracker
{
    public partial class NoteDetailsForm : Form
    {
        public NoteDetailsForm()
        {
            InitializeComponent();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TrackedItemModel? Data { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Repeat { get; set; } = false;

        private readonly BindingSource dataSource = [];

        //bool creatingNew = false;
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public bool CreatingNew
        //{
        //    get { return creatingNew; }
        //    set
        //    {
        //        creatingNew = value;
        //        button1.Visible = creatingNew;
        //    }
        //}

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CreatingNew { get; set; } = false;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            dataSource.DataSource = Data;
            //if(textBoxID.DataBindings.Count > 0)
            //{
            //    textBoxID.DataBindings.Clear();
            //    textBoxTeam.DataBindings.Clear();
            //    textBoxMeeting.DataBindings.Clear();
            //    textBoxCreated.DataBindings.Clear();
            //    textBoxNoteText.DataBindings.Clear();
            //    textBoxTags.DataBindings.Clear();
            //    textBoxFollowUp.DataBindings.Clear();
            //    textBoxCompleted.DataBindings.Clear();
            //}
            if (textBoxID.DataBindings.Count == 0)
            {
                textBoxID.DataBindings.Add("Text", dataSource, nameof(Data.Id));
                textBoxTeam.DataBindings.Add("Text", dataSource, nameof(Data.Team));
                textBoxMeeting.DataBindings.Add("Text", dataSource, nameof(Data.Meeting));
                textBoxCreated.DataBindings.Add("Text", dataSource, nameof(Data.WhenCreated));

                textBoxNoteText.DataBindings.Add("Text", dataSource, nameof(Data.Text));
                textBoxTags.DataBindings.Add("Text", dataSource, nameof(Data.Tag));
                textBoxFollowUp.DataBindings.Add("Text", dataSource, nameof(Data.FollowUp));
                textBoxCompleted.DataBindings.Add("Text", dataSource, nameof(Data.WhenCompleted));

                flagSelectionControl1.DataBindings.Add("FlagValue", dataSource, nameof(Data.Flag));
                //flagSelectionControl1.FlagValue = Data.Flag ?? "None";
            }

            button1.Visible = CreatingNew;

            if (CreatingNew)
            {
                if (string.IsNullOrEmpty(Data?.Team))
                {
                    textBoxTeam.Focus();
                }
                else if (string.IsNullOrEmpty(Data?.Meeting))
                {
                    textBoxMeeting.Focus();
                }
                else
                {
                    textBoxNoteText.Focus();
                }

                AcceptButton = button1;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Repeat = false;
            //            Data.Flag = flagSelectionControl1.FlagValue;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Repeat = false;

            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(Data != null)
            {
                Data.WhenCompleted = DateTime.Now;
            }
            textBoxCompleted.DataBindings[0].ReadValue();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Data?.Tag))
            {
                Data.Tag += $" ";
            }
            if(Data?.Tag != null)
            {
                Data.Tag += $"Idea";
            }
            textBoxTags.DataBindings[0].ReadValue();
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ToFromForm tff = new()
            {
                To = true
            };
            if (tff.ShowDialog() == DialogResult.OK)
            {
                if(Data?.Tag != null)
                    Data.Tag = $"I owe this to {tff.Person} by {tff.By}";
                textBoxTags.DataBindings[0].ReadValue();
            }
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ToFromForm tff = new()
            {
                To = false
            };
            if (tff.ShowDialog() == DialogResult.OK)
            {
                if (Data?.Tag != null)
                    Data.Tag = $"I expect this from {tff.Person} by {tff.By}";
                textBoxTags.DataBindings[0].ReadValue();
            }
        }

        private void FlagSelectionControl1_FlagChanged(object sender, Tracker.FlagSelectionControl.FlagChangedEventArgs e)
        {
            switch (e.FlagValue)
            {
                case "None":
                    break;
                case "Idea":
                    if (!string.IsNullOrEmpty(Data?.Tag))
                    {
                        Data.Tag += $" ";
                    }
                    if (Data?.Tag != null)
                        Data.Tag = $"Idea";
                    textBoxTags.DataBindings[0].ReadValue();
                    break;
                case "I_Owe":
                    ToFromForm tff = new()
                    {
                        To = true
                    };
                    if (tff.ShowDialog() == DialogResult.OK)
                    {
                        if (Data?.Tag != null)
                            Data.Tag = $"I owe this to {tff.Person} by {tff.By}";
                        textBoxTags.DataBindings[0].ReadValue();
                    }
                    break;
                case "They_Owe":
                    ToFromForm tff2 = new()
                    {
                        To = false
                    };
                    if (tff2.ShowDialog() == DialogResult.OK)
                    {
                        if (Data?.Tag != null)
                            Data.Tag = $"I expect this from {tff2.Person} by {tff2.By}";
                        textBoxTags.DataBindings[0].ReadValue();
                    }
                    break;
                default:
                    break;
            }

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            Repeat = true;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
