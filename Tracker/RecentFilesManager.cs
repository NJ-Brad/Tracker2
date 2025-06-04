using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Text.Json;

// https://github.com/NateShoffner/RecentFilesMenuItem/blob/master/RecentFilesMenuItem.cs
// The original had an option to display files in the main drop-down menu
// This was supported by functionality that changed the text of the clear all and open all items
// My version only displays items as a child, therefore, the clear all and open all items do not need
//    to be updated directly.  When the child menu opens, they will be created with the correct text.

namespace Tracker
{
    public partial class RecentFilesManager : Component
    {
        // Properties
        ToolStripMenuItem? mruMenuItem = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripMenuItem? MruMenuItem
        {
            get { return mruMenuItem; }
            set { mruMenuItem = value; Connect(); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ConfigFileName { get; set; } = "RecentFiles.json";

        /// <summary>
        ///     Gets or sets whether the 'clear all' option should be displayed.
        /// </summary>
        [Description("Indicates whether the 'clear all' option should be displayed."), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool DisplayClearOption { get; set; } = true;

        /// <summary>
        ///     Gets or sets the text to display for 'clear all' option.
        /// </summary>
        [Description("Text to display for 'clear all' option."), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ClearOptionText { get; set; } = "Clear All Recent Items";

        /// <summary>
        ///     Gets or sets whether the 'open all' option should be displayed.
        /// </summary>
        [Description("Indicates whether the 'open all' option should be displayed."), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool DisplayOpenAllOption { get; set; } = true;

        /// <summary>
        ///     Gets or sets the text to display for 'open all' option.
        /// </summary>
        [Description("Text to display for 'open all' option."), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string OpenAllOptionText { get; set; } = "Open All Recent Items";

        /// <summary>
        ///     Gets or sets the maximum number of items to display.
        /// </summary>
        [Description("Indicates the maximum number of items to display."), Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int MaxDisplayItems { get; set; } = 10;

        /// <summary>
        ///     Gets or sets whether to prepend human-friendly number indicatators to menu items.
        /// </summary>
        [Description("Indicates whether to prepend human-friendly number indicators to menu items."), Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool PrependItemNumbers { get; set; } = true;

        // Events

        /// <summary>
        ///     Occurs when the menu item is clicked or selected using a shortcut key or access key defined for the menu item.
        /// </summary>
        public event Action<object?, RecentFilesManagerEventArgs>? FileClicked;

        /// <summary>
        ///     Occurs when the 'open all' menu item is clicked or selected using a shortcut key or access key defined for the menu
        ///     item.
        /// </summary>
        public event Action<object?, RecentFilesManagerEventArgs>? AllFilesClicked;

        // variables
        private readonly ToolStripMenuItem _clearMenuItem = new();
        private readonly ToolStripMenuItem _openAllMenuItem = new();
        private List<FileEntry> fileEntries = new List<FileEntry>();

        // Methods
        public void FileOpened(string fileName)
        {
            // is it already in the list
            int idx = fileEntries.FindIndex(e => e.Path == fileName);
            if (idx != -1)
            {
                // remove the old one
                fileEntries.RemoveAt(idx);
            }

            fileEntries.Insert(0, new FileEntry() { Path = fileName, LastOpened = DateTime.Now });

            while (fileEntries.Count > MaxDisplayItems)
            {
                fileEntries.RemoveAt(MaxDisplayItems - 1);
            }

            SaveList();
        }


        public RecentFilesManager()
        {
            InitializeComponent();
        }

        public RecentFilesManager(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        ///     Initializes a new RecentFilesMenuItem.
        /// </summary>
        private void Connect()
        {
            _openAllMenuItem.Text = OpenAllOptionText;
            _openAllMenuItem.Click += delegate
            {
                if (AllFilesClicked != null)
                {
                    RecentFilesManagerEventArgs args = new RecentFilesManagerEventArgs();
                    args.FileNames = fileEntries.Select(e => e.Path).ToArray();

                    AllFilesClicked(this, args);
                }
            };

            _clearMenuItem.Text = ClearOptionText;
            _clearMenuItem.Click += (a, b) =>
            {
                Clear();
            };

            if (MruMenuItem != null)
            {
                MruMenuItem.DropDownOpening += (a, b) =>
                {
                    LoadList();
                    MruMenuItem.DropDownItems.Clear();
                    if (fileEntries.Count == 0)
                    {
                        MruMenuItem.DropDownItems.Add(new ToolStripMenuItem("No Recent Files"));
                    }
                    else
                    {
                        foreach (FileEntry fe in fileEntries)
                        {
                            AddFileItem(fe);
                        }

                        // these only make sense when there is at least one file
                        if (DisplayOpenAllOption || DisplayClearOption)
                            MruMenuItem.DropDownItems.Add(new ToolStripSeparator());

                        if (DisplayOpenAllOption)
                            MruMenuItem.DropDownItems.Add(_openAllMenuItem);

                        if (DisplayClearOption)
                            MruMenuItem.DropDownItems.Add(_clearMenuItem);
                    }
                };

                // Initialize the item
                MruMenuItem.DropDownItems.Clear();
                // This causes the arrow to be put on the menu item - drop down doesn't occur if there aren't any items
                MruMenuItem.DropDownItems.Add(new ToolStripMenuItem("No Recent Files"));

                // This will load the internal list of files.
                // This way loading any files opened prior to opening the menu will not clear the list.
                LoadList();

                MruMenuItem.Enabled = true;
            }
        }

        private void AddFileItem(FileEntry fe)
        {
            ToolStripMenuItem newFileItem = new("...");
            int fileNumber = MruMenuItem == null ? 0 : MruMenuItem.DropDownItems.Count;
            newFileItem.Text = PrependItemNumbers ? string.Format("{0}: {1}", fileNumber + 1, fe.Path) : fe.Path;

            newFileItem.Click += (a, b) =>
            {
                if (FileClicked != null)
                {
                    FileClicked(this, new RecentFilesManagerEventArgs(fe.Path));
                }
            };
            if (MruMenuItem != null)
                MruMenuItem.DropDownItems.Add(newFileItem);
        }


        // https://learn.microsoft.com/en-us/dotnet/standard/io/isolated-storage
        // https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-read-and-write-to-files-in-isolated-storage
        private void LoadList()
        {
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            if (isoStore.FileExists(ConfigFileName))
            {
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(ConfigFileName, FileMode.Open, isoStore))
                {
                    using (StreamReader reader = new StreamReader(isoStream))
                    {
                        string content = reader.ReadToEnd();

                        fileEntries = JsonSerializer.Deserialize<List<FileEntry>>(content) ?? new();
                    }
                }
            }
        }

        // https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-read-and-write-to-files-in-isolated-storage
        // https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-delete-files-and-directories-in-isolated-storage
        private void SaveList()
        {
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            if (isoStore.FileExists(ConfigFileName))
            {
                isoStore.DeleteFile(ConfigFileName);
            }

            using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(ConfigFileName, FileMode.CreateNew, isoStore))
            {
                using (StreamWriter writer = new StreamWriter(isoStream))
                {
                    writer.Write(JsonSerializer.Serialize(fileEntries));
                }
            }
        }

        /// <summary>
        ///     Clears all items in the menu.
        /// </summary>
        private void Clear()
        {
            fileEntries.Clear();
            SaveList();
        }

        public string GetLatestFile()
        {
            string rtnVal = string.Empty;

            LoadList();
            if (fileEntries.Count > 0)
            {
                rtnVal = fileEntries[0].Path;
            }

            return rtnVal;
        }

        internal class FileEntry
        {
            public string Path { get; set; } = string.Empty;
            public DateTime LastOpened { get; set; }
        }

        public class RecentFilesManagerEventArgs : EventArgs
        {
            public RecentFilesManagerEventArgs() { }
            public RecentFilesManagerEventArgs(string fileName)
            {
                this.FileName = fileName;
            }

            public string FileName { get; set; } = "";
            public string[] FileNames { get; set; } = new string[0];
        }
    }
}
