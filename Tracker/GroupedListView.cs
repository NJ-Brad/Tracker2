namespace Tracker
{
    public class GroupedListView : ListView
    {
        public GroupedListView()
        {
            // Initialize the ListView with necessary properties
            this.View = View.Details;
            this.FullRowSelect = true;
            this.HideSelection = false;
            this.MultiSelect = false;
            this.OwnerDraw = true;
            base.DrawItem += DrawListItem;
            base.DrawSubItem += DrawListSubItem;
            base.DrawColumnHeader += DrawListColumnHeader;
            base.MouseDown += ListMouseDown;

            Bitmap bitmap2 = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bitmap2))
            {
                g.FillRectangle(SystemBrushes.Window, new Rectangle(0, 0, 16, 16));
                g.DrawLine(Pens.Black, 1, 8, 7, 14);
                g.DrawLine(Pens.Black, 2, 8, 7, 13);
                g.DrawLine(Pens.Black, 8, 13, 13, 8);
                g.DrawLine(Pens.Black, 8, 14, 14, 8);
                bitmap2.MakeTransparent();
                imageList1.Images.Add(bitmap2);
            }

            Bitmap bitmap = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.FillRectangle(SystemBrushes.Window, new Rectangle(0, 0, 16, 16));
                g.DrawLine(Pens.Black, 8, 1, 14, 7);
                g.DrawLine(Pens.Black, 8, 2, 13, 7);
                g.DrawLine(Pens.Black, 13, 8, 8, 13);
                g.DrawLine(Pens.Black, 14, 8, 8, 14);
                bitmap.MakeTransparent();
                imageList1.Images.Add(bitmap);
            }
        }

        private readonly ListView shadowListView = new ListView();
        private readonly ImageList imageList1 = new ImageList();

        new public event DrawListViewItemEventHandler? DrawItem;
        new public event DrawListViewSubItemEventHandler? DrawSubItem;
        new public event DrawListViewColumnHeaderEventHandler? DrawColumnHeader;
        new public event MouseEventHandler? MouseDown;

        public new ListViewGroupCollection Groups { get { return shadowListView.Groups; } }

        public void ShowData()
        {
            RefreshItems();
        }

        private void RefreshItems()
        {
            this.BeginUpdate();
            try
            {
                Items.Clear();
                foreach (ListViewGroup group in shadowListView.Groups)
                {
                    Items.Add(group.Header).Tag = group;
                    if (group.CollapsedState != ListViewGroupCollapsedState.Collapsed)
                    {
                        // Default or Expanced
                        foreach (ListViewItem lvi in group.Items)
                        {
                            Items.Add(lvi);
                        }
                    }
                    // add a footer item for the group (Future enhancement)
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }

        private void DrawListItem(object sender, DrawListViewItemEventArgs e)
        {
            if (DrawItem != null)
            {
                DrawItem(sender, e);
                if (!e.DrawDefault)
                    return;
            }
            e.DrawDefault = false;

            //if (!e.DrawDefault)
            //    return;

            if (e.Item != null && e.Item.Tag != null && e.Item.Tag is ListViewGroup group)
            {
                Rectangle bounds = e.Bounds;
                //bounds.Width = ClientRectangle.Width - SystemInformation.VerticalScrollBarWidth; // Adjust for scrollbar width
                bounds.Width = ClientRectangle.Width;
                if (group.CollapsedState != ListViewGroupCollapsedState.Collapsed)
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

                e.Graphics.DrawString(e.Item.Text, Font, SystemBrushes.ActiveCaptionText, bounds);
            }
            else
                e.DrawDefault = true;

        }

        private void DrawListSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (DrawSubItem != null)
            {
                DrawSubItem(sender, e);
                if (!e.DrawDefault)
                    return;
            }
            e.DrawDefault = false;

            if (e.ColumnIndex == 0) // Draw the first column (Heading)
            {
                return;
            }
            e.DrawDefault = true;
        }

        private void DrawListColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if (DrawColumnHeader != null)
            {
                DrawColumnHeader(sender, e);
                if (!e.DrawDefault)
                    return;
            }
            e.DrawDefault = false;

            // Draw the column header with a custom style

            e.DrawDefault = true;
        }

        private void ListMouseDown(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hit = HitTest(e.X, e.Y);

            if (hit.Item == null || hit.Item.Tag == null)
                return;

            if (hit.Item.Tag is ListViewGroup group)
            {
                if (e.X < hit.Item.Bounds.X + 20) // Check if clicked on the image area
                {
                    group.CollapsedState = group.CollapsedState == ListViewGroupCollapsedState.Collapsed ? ListViewGroupCollapsedState.Expanded : ListViewGroupCollapsedState.Collapsed;
                    RefreshItems();
                    return;
                }
            }

            if (MouseDown != null)
            {
                MouseDown(sender, e);
                return;
            }
        }

    }
}
