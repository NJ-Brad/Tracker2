using System.ComponentModel;
using System.Text;
using Tracker.Models;

namespace Tracker
{
    public partial class PrintForm : Form
    {
        public PrintForm()
        {
            InitializeComponent();
        }

        public TrackerDocument Document { get; set; }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            string printerDoc = BuildDocumentForPrinter();
            //browserWrapper1.InitializationComplete = async () =>
            browserWrapper1.InitializationComplete = () =>
            {
                //MessageBox.Show("Hello");
                browserWrapper1.ShowHtmlText(printerDoc);
                //browserWrapper1.ShowUrl("https://www.google.com");
            };

            browserWrapper1.NavigationComplete = async () =>
            {
                if (ImmediatePrint)
                {
                    //browserWrapper1.PrintWithUI();
                    await browserWrapper1.Print();
                    Close();
                }
            };
        }

        public void Print()
        {
            browserWrapper1.PrintWithUI();
        }

        private string BuildDocumentForPrinter()
        {
            StringBuilder sb = new StringBuilder();

            //sb.AppendLine("<h1>Hello Brad</h1>");

            //sb.AppendLine($"<h1>{Document.Header.Text}</h1>");
            //sb.AppendLine($"{Document.Header.StartTime}");
            //sb.AppendLine($"<p>");
            sb.AppendLine($"<h2>Notes</h2>");
            sb.AppendLine($"<table>");
            sb.AppendLine($"<tr><th>Tag</th><th>Content</th></tr>");
            foreach (TrackedItemModel nm in Document.Items)
            {
                sb.AppendLine($"<tr><td>{nm.Tag}</td><td>{nm.Text}</td></tr>");
            }
            sb.AppendLine($"</table>");
            //sb.AppendLine("<hr/>");
            //sb.AppendLine(Document.Header.MeetingSummary);
            //sb.AppendLine("<hr/>");

            return sb.ToString();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(PrintForm));
            toolStrip1 = new ToolStrip();
            toolStripButton1 = new ToolStripButton();
            browserWrapper1 = new BrowserWrapper();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(612, 27);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(63, 24);
            toolStripButton1.Text = "Print";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // browserWrapper1
            // 
            browserWrapper1.Dock = DockStyle.Fill;
            browserWrapper1.InitializationComplete = null;
            browserWrapper1.Location = new Point(0, 27);
            browserWrapper1.Margin = new Padding(3, 4, 3, 4);
            browserWrapper1.Name = "browserWrapper1";
            browserWrapper1.NavComplete = false;
            browserWrapper1.NavigationComplete = null;
            browserWrapper1.RootPath = "";
            browserWrapper1.Size = new Size(612, 342);
            browserWrapper1.TabIndex = 1;
            // 
            // PrintForm
            // 
            ClientSize = new Size(612, 369);
            Controls.Add(browserWrapper1);
            Controls.Add(toolStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PrintForm";
            ShowInTaskbar = false;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private BrowserWrapper browserWrapper1;

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            browserWrapper1.PrintWithUI();
        }

        public bool ImmediatePrint { get; set; } = false;
    }
}
