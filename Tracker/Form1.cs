using System.ComponentModel;
using System.Text.Json;
using Tracker.Models;

namespace Tracker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateDataBindings();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            string latestFile = recentFilesManager1.GetLatestFile();
            if (!string.IsNullOrEmpty(latestFile))
            {
                LoadDocument(latestFile);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDocument();
        }

        TrackerDocument doc = new TrackerDocument();
        string documentName = string.Empty;
        private void LoadDocument(string fileName)
        {
            this.documentName = fileName;
            Text = Path.GetFileNameWithoutExtension(fileName);
            string docText = File.ReadAllText(documentName);
            doc = JsonSerializer.Deserialize<TrackerDocument>(docText) ?? new TrackerDocument();
            ShowDocument();
        }

        private void NewDocument()
        {
            doc = new TrackerDocument();
            documentName = string.Empty;
            ShowDocument();
        }

        private void SaveDocument()
        {
            dataGridView1.CancelEdit();

            File.WriteAllText(documentName, JsonSerializer.Serialize(doc));
        }

        BindingSource bs1 = new BindingSource();

        private void CreateDataBindings()
        {
            // https://stackoverflow.com/questions/11078919/bindingsource-datagridview-interaction

            BindingList<TrackedItemModel> bList1 = new BindingList<TrackedItemModel>(doc.Items);
            bs1.DataSource = bList1;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bs1;
        }

        private void ShowDocument()
        {
            // https://stackoverflow.com/questions/11078919/bindingsource-datagridview-interaction

            BindingList<TrackedItemModel> bList1 = new BindingList<TrackedItemModel>(doc.Items);
            bs1.DataSource = bList1;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tracker");
            ofd.Filter = "Tracker File (*.trk)|*.trk|All Files (*.*)|*.*";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadDocument(ofd.FileName);
                recentFilesManager1.FileOpened(ofd.FileName);
            }
        }

        private void recentFilesManager1_FileClicked(object arg1, Tracker.RecentFilesManager.RecentFilesManagerEventArgs arg2)
        {
            LoadDocument(arg2.FileName);
            recentFilesManager1.FileOpened(arg2.FileName);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(documentName))
                SaveDocumentAs();
            else
                SaveDocument();
        }
        private void SaveDocumentAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Tracker File (*.trk)|*.trk|All Files (*.*)|*.*";
            sfd.AddExtension = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                documentName = sfd.FileName;
                recentFilesManager1.FileOpened(documentName);
                SaveDocument();
            }

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocumentAs();
        }

        // [System.Diagnostics.CodeAnalysis.SuppressMessage(
        //     "Style",
        //     //"S125:Simplify object initialization",
        //     "S125",
        //     Justification = "Removing conflict with WindowsBase triggered by WebView2, until resolution is found ")]
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // https://stackoverflow.com/questions/56538847/how-to-ignore-sonar-rule-to-specific-line-of-code-in-c
            // S is for Sonar
#pragma warning disable S125
            // BEGIN-NOSCAN  // this is supposed to work for SonarQube 
            //PrintForm pf = new();
            //pf.Document = doc;
            ////pf.Visible = false;
            ////pf.ImmediatePrint = true;
            ////pf.Show();

            //pf.ShowDialog();
            // END-NOSCAN
#pragma warning restore S125
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(e.Row.State.ToString());
        }

        string? previousMeeting = "New Meeting";
        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Index > 0)
            {
                //previousMeeting = dataGridView1.Rows[e.Row.Index - 1].Cells["MeetingCol"].Value.ToString() ?? "New Meeting";
                if (dataGridView1.Rows[e.Row.Index - 1].Cells["MeetingCol"].Value == null)
                {
                    previousMeeting = "";
                }
                else
                {
                    previousMeeting = dataGridView1.Rows[e.Row.Index - 1].Cells["MeetingCol"].Value.ToString();
                }
            }
            e.Row.Cells["MeetingCol"].Value = previousMeeting;
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.ColumnIndex == 1)
                {
                    if (e.RowIndex != -1)
                    {
                        TrackedItemModel tim = dataGridView1.Rows[e.RowIndex].DataBoundItem as TrackedItemModel;

                        NoteDetailsForm ndf = new NoteDetailsForm();

                        ndf.Data = new TrackedItemModel(tim);

                        if (ndf.ShowDialog() == DialogResult.OK)
                        {
                            //dataGridView1.Rows[e.RowIndex].SetValues(ndf.Data);
                            tim.CopyFrom(ndf.Data);

                            dataGridView1.Refresh();
                        }
                    }
                }

            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                TrackedItemModel tim = dataGridView1.Rows[e.RowIndex].DataBoundItem as TrackedItemModel;

                NoteDetailsForm ndf = new NoteDetailsForm();

                ndf.Data = new TrackedItemModel(tim);

                if (ndf.ShowDialog() == DialogResult.OK)
                {
                    //dataGridView1.Rows[e.RowIndex].SetValues(ndf.Data);
                    tim.CopyFrom(ndf.Data);

                    dataGridView1.Refresh();
                }
            }
        }
    }
}
