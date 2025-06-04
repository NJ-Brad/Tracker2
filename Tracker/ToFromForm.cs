using System.ComponentModel;

namespace Tracker
{
    public partial class ToFromForm : Form
    {
        public ToFromForm()
        {
            InitializeComponent();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool To { get; set; } = true;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Person { get; set; } = "";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string By { get; set; } = "";

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (To)
            {
                Text = "I Owe";
                label1.Text = "I owe this to";
            }
            else
            {
                Text = "I Expect";
                label1.Text = "I expect this from";
            }

            dateTimePicker1.Value = DateTime.Now;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Person = textBox1.Text;
            By = dateTimePicker1.Text;
            Close();
        }

    }
}
