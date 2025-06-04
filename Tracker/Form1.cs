using System.Text.Json;
using Tracker.Models;

namespace Tracker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Bitmap bm = (Bitmap)imageList1.Images[0];
            bm.MakeTransparent();
            imageList1.Images[0] = bm;

            bm = (Bitmap)imageList1.Images[1];
            bm.MakeTransparent();
            imageList1.Images[1] = bm;
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

        // https://csharpexamples.com/c-resize-bitmap-example/
        public Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
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
            File.WriteAllText(documentName, JsonSerializer.Serialize(doc));
        }

        private void ShowDocument()
        {
            doc.Items.Select(i => i.Team).OrderBy(i => i).Distinct().ToList().ForEach(team =>
            {
                AddHeading(team);
            });

            ShowHeadings();
        }

        private void AddHeading(string team)
        {
            Heading heading = new Heading(team);
            headings.Add(heading.Title, heading);
            listView2.Items.Add(heading.Title).Tag = heading;
        }

        Dictionary<string, Heading> headings = new Dictionary<string, Heading>();

        // fill the teams first, including the IsExpanded property
        // THEN fill the details, only for the teams that are expanded

        //bool rowShown = false;
        private void ShowHeadings()
        {
            listView2.Items.Clear();
            //rowShown = false;
            foreach (string key in headings.Keys)
            {
                Heading h = headings[key];
                listView2.Items.Add(h.Title).Tag = h;
                if (h.IsExpanded)
                {
                    ShowItems(h.Title);
                }
            }

            //if (rowShown)
            //    listView2.Columns[1].Width = -1;
            //else
            //    listView2.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView2.Columns[1].Width = 200;
            listView2.Columns[2].Width = -2;
        }

        private void ShowItems(string team)
        {
            //doc.Items.Select(i => i.Team).OrderBy(i => i).Distinct().ToList().ForEach(team =>
            doc.Items.Where(i => i.Team == team).ToList().ForEach(item =>
            {
                ListViewItem lvi = new ListViewItem("", 0);
                lvi.SubItems.Add(item.Meeting ?? "New Meeting");
                lvi.SubItems.Add(item.Text ?? "");
                lvi.Tag = item;
                listView2.Items.Add(lvi);
                //rowShown = true;
            });
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

        string? previousMeeting = "New Meeting";
        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Index > 0)
            {
                //if (dataGridView1.Rows[e.Row.Index - 1].Cells["MeetingCol"].Value == null)
                //{
                //    previousMeeting = "";
                //}
                //else
                //{
                //    previousMeeting = dataGridView1.Rows[e.Row.Index - 1].Cells["MeetingCol"].Value.ToString();
                //}
            }
            e.Row.Cells["MeetingCol"].Value = previousMeeting;
        }

        private void listView2_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if (e.Item != null && e.Item.Tag != null && e.Item.Tag is Heading heading)
            {
                Rectangle bounds = e.Bounds;
                bounds.Width = listView2.ClientRectangle.Width - SystemInformation.VerticalScrollBarWidth; // Adjust for scrollbar width
                if (heading.IsExpanded)
                {
                    e.Graphics.FillRectangle(SystemBrushes.ControlLight, bounds);
                    e.Graphics.DrawImage(imageList1.Images[0], bounds.X + 2, bounds.Y + 4, 16, 16);
                }
                else
                {
                    //e.Graphics.FillRectangle(SystemBrushes.ControlDark, bounds);
                    e.Graphics.FillRectangle(SystemBrushes.ControlLight, bounds);
                    e.Graphics.DrawImage(imageList1.Images[1], bounds.X + 2, bounds.Y + 4, 16, 16);
                }

                bounds.X += 20; // Adjust for image width
                bounds.Width -= 20; // Adjust for image width

                e.Graphics.DrawString(e.Item.Text, listView2.Font, SystemBrushes.ActiveCaptionText, bounds);
            }
            else if (e.Item != null && e.Item.Tag != null && e.Item.Tag is TrackedItemModel trackedItem)
            {
                Rectangle cellBounds = e.Bounds;
                cellBounds.Width = listView2.Columns[0].Width;


                //if ((e.State & ListViewItemStates.Selected) != 0)
                if (e.Item.Selected)
                {
                    // Draw the background and focus rectangle for a selected item.
                    //e.Graphics.FillRectangle(Brushes.Maroon, e.Bounds);
                    //                    using (Brush brush = SolidBrush()

                    e.Graphics.FillRectangle(SystemBrushes.Highlight, cellBounds);
                    e.DrawFocusRectangle();
                }
                else
                {
                    using (Brush brush = new SolidBrush(listView2.BackColor))
                    {
                        e.Graphics.FillRectangle(brush, cellBounds);
                    }
                    //// Draw the background for an unselected item.
                    //using (LinearGradientBrush brush =
                    //    new LinearGradientBrush(e.Bounds, Color.Orange,
                    //    Color.Maroon, LinearGradientMode.Horizontal))
                    //{
                    //    e.Graphics.FillRectangle(brush, e.Bounds);
                    //}
                }

                if (cellBounds.Width > 20) // Prevent drawing if the width is too small
                {
                    int middle = cellBounds.X + cellBounds.Width / 2;
                    int start = middle - 8;
                    cellBounds.X = start;
                }
                switch (trackedItem.Flag)
                {
                    case "Idea":
                        //                        e.Graphics.DrawImage(imageList2.Images[0], bounds.X, bounds.Y + 4, 16, 16);
                        e.Graphics.DrawImage(imageList2.Images[0], cellBounds.X, cellBounds.Y + 4, 16, 16);
                        break;
                    case "I_Owe":
                        e.Graphics.DrawImage(imageList2.Images[1], cellBounds.X, cellBounds.Y + 4, 16, 16);
                        break;
                    case "They_Owe":
                        e.Graphics.DrawImage(imageList2.Images[2], cellBounds.X, cellBounds.Y + 4, 16, 16);
                        break;
                        //default:
                        //    e.Graphics.FillRectangle(SystemBrushes.ControlLight, bounds);
                        //    break;
                }
            }
            else
                e.DrawDefault = true;

        }

        private void listView2_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (e.ColumnIndex == 0) // Draw the first column (Heading or TrackedItemModel)
            {
                return;
            }
            e.DrawDefault = true;

            //if (e.Item != null && e.Item.Tag != null && e.Item.Tag is TrackedItemModel tim)
            //{
            //    e.DrawDefault = true;
            //}

            //if (headings["a"].IsExpanded)
            //    e.DrawDefault = true;
            //else
            //{
            //    //e.DrawBackground(); // this is the default
            //    e.Graphics.FillRectangle(SystemBrushes.ControlDark, e.Bounds);
            //    e.Graphics.DrawString(e.Item.Text, listView2.Font, SystemBrushes.ActiveCaptionText, e.Bounds);
            //}
        }

        private void listView2_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void listView2_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hit = listView2.HitTest(e.X, e.Y);

            if (hit.Item == null || hit.Item.Tag == null)
                return;

            if (hit.Item.Tag is Heading heading)
            {
                if (e.X < hit.Item.Bounds.X + 20) // Check if clicked on the image area
                {
                    heading.IsExpanded = !heading.IsExpanded;
                    ShowHeadings();
                    return;
                }
            }
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hit = listView2.HitTest(e.X, e.Y);

            if (hit.Item == null || hit.Item.Tag == null)
                return;

            if (hit.Item.Tag is TrackedItemModel tim)
            {
                NoteDetailsForm ndf = new NoteDetailsForm();

                ////ndf.Data = new TrackedItemModel(tim);
                ndf.Data = tim;

                if (ndf.ShowDialog() == DialogResult.OK)
                {
                    ShowHeadings();
                }
            }
        }

        private void listView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.Insert)
            {
                NoteDetailsForm ndf = new NoteDetailsForm();

                ndf.Data = new TrackedItemModel();
                if (listView2.SelectedItems.Count > 0 && listView2.SelectedItems[0].Tag is TrackedItemModel tim)
                {
                    ndf.Data.Team = tim.Team; // Copy the team from the selected item
                    ndf.Data.Meeting = tim.Meeting; // Copy the team from the selected item
                    ndf.CreatingNew = true;
                }

                do
                {
                    if (ndf.ShowDialog() == DialogResult.OK)
                    {
                        if (!headings.ContainsKey(ndf.Data.Team))
                        {
                            AddHeading(ndf.Data.Team);
                        }
                        doc.Items.Add(ndf.Data);
                        ShowHeadings();

                        string prevTeam = ndf.Data.Team;
                        string prevMeeting = ndf.Data.Meeting;

                        if (ndf.Repeat)
                        {
                            ndf.Data = new TrackedItemModel();
                            ndf.Data.Team = prevTeam;
                            ndf.Data.Meeting = prevMeeting;
                            ndf.CreatingNew = true;
                        }
                    }
                } while (ndf.Repeat);
            }

            if (e.KeyValue == (int)Keys.Delete)
            {
                if (listView2.SelectedItems.Count > 0 && listView2.SelectedItems[0].Tag is TrackedItemModel tim)
                {
                    DialogResult dr = MessageBox.Show($"Are you sure you want to delete the item '{tim.Text}'?", "Delete Item", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        doc.Items.Remove(tim);
                        ShowHeadings();
                    }
                }
            }
        }
    }
}
