using System.ComponentModel;
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
        public TrackedItemModel Data { get; set; }

        private BindingSource dataSource = new BindingSource();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CreatingNew { get; set; } = false;


        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            dataSource.DataSource = Data;
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

            if (CreatingNew)
                textBoxNoteText.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //            Data.Flag = flagSelectionControl1.FlagValue;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Data.WhenCompleted = DateTime.Now;
            textBoxCompleted.DataBindings[0].ReadValue();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Data.Tag))
            {
                Data.Tag += $" ";
            }
            Data.Tag += $"Idea";
            textBoxTags.DataBindings[0].ReadValue();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ToFromForm tff = new ToFromForm();
            tff.To = true;
            if (tff.ShowDialog() == DialogResult.OK)
            {
                Data.Tag = $"I owe this to {tff.Person} by {tff.By}";
                textBoxTags.DataBindings[0].ReadValue();
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ToFromForm tff = new ToFromForm();
            tff.To = false;
            if (tff.ShowDialog() == DialogResult.OK)
            {
                Data.Tag = $"I expect this from {tff.Person} by {tff.By}";
                textBoxTags.DataBindings[0].ReadValue();
            }
        }
    }
}
