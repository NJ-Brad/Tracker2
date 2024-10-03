using Tracker.Models;

namespace Tracker
{
    public partial class NoteDetailsForm : Form
    {
        public NoteDetailsForm()
        {
            InitializeComponent();
        }

        public TrackedItemModel Data { get; set; }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            textBox1.DataBindings.Add("Text", Data, nameof(Data.Text));
            textBox2.DataBindings.Add("Text", Data, nameof(Data.Tag));
            textBox3.DataBindings.Add("Text", Data, nameof(Data.FollowUp));
            textBox4.DataBindings.Add("Text", Data, nameof(Data.WhenCompleted));
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
            textBox4.DataBindings[0].ReadValue();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Data.Tag))
            {
                Data.Tag += $" ";
            }
            Data.Tag += $"Idea";
            textBox2.DataBindings[0].ReadValue();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ToFromForm tff = new ToFromForm();
            tff.To = true;
            if (tff.ShowDialog() == DialogResult.OK)
            {
                Data.Tag = $"I owe this to {tff.Person} by {tff.By}";
                textBox2.DataBindings[0].ReadValue();
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ToFromForm tff = new ToFromForm();
            tff.To = false;
            if (tff.ShowDialog() == DialogResult.OK)
            {
                Data.Tag = $"I expect this from {tff.Person} by {tff.By}";
                textBox2.DataBindings[0].ReadValue();
            }
        }
    }
}
