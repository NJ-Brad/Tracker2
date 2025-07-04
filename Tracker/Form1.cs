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
        public static Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDocument();
        }

        TrackerDocument doc = new();
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
            groupedListView1.Groups.Clear();

            doc.Items.Select(i => i.Team).OrderBy(i => i).Distinct().ToList().ForEach(team =>
            {
                groupedListView1.Groups.Add(team, team);

                doc.Items.Where(i => i.Team == team).ToList().ForEach(item =>
                {
                    ListViewItem lvi = new("", 0);
                    lvi.SubItems.Add(item.Meeting ?? "New Meeting");
                    lvi.SubItems.Add(item.Text ?? "");
                    lvi.Tag = item;
                    lvi.Group = groupedListView1.Groups[^1];
                    groupedListView1.Items.Add(lvi);
                });
            });
            groupedListView1.ShowData();

            groupedListView1.Columns[1].Width = 200;
            groupedListView1.Columns[2].Width = -2;
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tracker"),
                Filter = "Tracker File (*.trk)|*.trk|All Files (*.*)|*.*",
                Multiselect = false
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadDocument(ofd.FileName);
                recentFilesManager1.FileOpened(ofd.FileName);
            }
        }

        private void RecentFilesManager1_FileClicked(object arg1, Tracker.RecentFilesManager.RecentFilesManagerEventArgs arg2)
        {
            LoadDocument(arg2.FileName);
            recentFilesManager1.FileOpened(arg2.FileName);
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(documentName))
                SaveDocumentAs();
            else
                SaveDocument();
        }
        private void SaveDocumentAs()
        {
            SaveFileDialog sfd = new ()
            {
                Filter = "Tracker File (*.trk)|*.trk|All Files (*.*)|*.*",
                AddExtension = true
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                documentName = sfd.FileName;
                recentFilesManager1.FileOpened(documentName);
                SaveDocument();
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocumentAs();
        }

        // [System.Diagnostics.CodeAnalysis.SuppressMessage(
        //     "Style",
        //     //"S125:Simplify object initialization",
        //     "S125",
        //     Justification = "Removing conflict with WindowsBase triggered by WebView2, until resolution is found ")]
        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // https://stackoverflow.com/questions/56538847/how-to-ignore-sonar-rule-to-specific-line-of-code-in-c
            // S is for Sonar
//#pragma warning disable S125
            // BEGIN-NOSCAN  // this is supposed to work for SonarQube 
            //PrintForm pf = new();
            //pf.Document = doc;
            ////pf.Visible = false;
            ////pf.ImmediatePrint = true;
            ////pf.Show();

            //pf.ShowDialog();
            // END-NOSCAN
//#pragma warning restore S125
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GroupedListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hit = groupedListView1.HitTest(e.X, e.Y);

            if (hit.Item == null || hit.Item.Tag == null)
                return;

            if (hit.Item.Tag is TrackedItemModel tim)
            {
                NoteDetailsForm ndf = new ()
                {
                    Data = tim
                };

                if (ndf.ShowDialog() == DialogResult.OK)
                {
                    ShowDocument();
                }
            }
        }

        private void GroupedListView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if (e.Item != null && e.Item.Tag != null && e.Item.Tag is TrackedItemModel trackedItem)
            {
                Rectangle cellBounds = e.Bounds;
                cellBounds.Width = groupedListView1.Columns[0].Width;


                if (e.Item.Selected)
                {
                    // Draw the background and focus rectangle for a selected item.
                    e.Graphics.FillRectangle(SystemBrushes.Highlight, cellBounds);
                    e.DrawFocusRectangle();
                }
                else
                {
                    using Brush brush = new SolidBrush(groupedListView1.BackColor);
                    e.Graphics.FillRectangle(brush, cellBounds);
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
                        e.Graphics.DrawImage(imageList2.Images[0], cellBounds.X, cellBounds.Y + 4, 32, 32);
                        break;
                    case "I_Owe":
                        e.Graphics.DrawImage(imageList2.Images[1], cellBounds.X, cellBounds.Y + 4, 32, 32);
                        break;
                    case "They_Owe":
                        e.Graphics.DrawImage(imageList2.Images[2], cellBounds.X, cellBounds.Y + 4, 32, 32);
                        break;
                        //default:
                        //    e.Graphics.FillRectangle(SystemBrushes.ControlLight, bounds);
                        //    break;
                }
                e.DrawDefault = false;
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        private void GroupedListView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.Insert)
            {
                NoteDetailsForm ndf = new()
                {
                    Data = new TrackedItemModel(),
                    CreatingNew = true
                };
                if (groupedListView1.SelectedItems.Count > 0 && groupedListView1.SelectedItems[0].Tag is TrackedItemModel tim)
                {
                    ndf.Data.Team = tim.Team; // Copy the team from the selected item
                    ndf.Data.Meeting = tim.Meeting; // Copy the team from the selected item
                }

                do
                {
                    if (ndf.ShowDialog() == DialogResult.OK)
                    {
                        if(ndf.Data != null)
                            doc.Items.Add(ndf.Data);
                        ShowDocument();

                        string prevTeam = ndf.Data?.Team ?? string.Empty;
                        string prevMeeting = ndf.Data?.Meeting ?? string.Empty;

                        if (ndf.Repeat)
                        {
                            ndf.Data = new()
                            {
                                Team = prevTeam,
                                Meeting = prevMeeting
                            };
                            ndf.CreatingNew = true;
                        }
                    }
                } while (ndf.Repeat);
            }

            if (e.KeyValue == (int)Keys.Delete)
            {
                if (groupedListView1.SelectedItems.Count > 0 && groupedListView1.SelectedItems[0].Tag is TrackedItemModel tim)
                {
                    DialogResult dr = MessageBox.Show($"Are you sure you want to delete the item '{tim.Text}'?", "Delete Item", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        doc.Items.Remove(tim);
                        ShowDocument();
                    }
                }
            }
        }
    }
}
