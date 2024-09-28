using Microsoft.Web.WebView2.Core;

namespace Tracker
{
    public partial class BrowserWrapper : UserControl
    {

        public string RootPath { get; set; } = string.Empty;

        public BrowserWrapper()
        {
            InitializeComponent();
        }

        bool initialized = false;
        public Dictionary<string, string> Replacements = new();
        public void PrintWithUI()
        {
            // More options here : https://github.com/MicrosoftDocs/edge-developer/blob/main/microsoft-edge/webview2/how-to/print.md
            webView21.CoreWebView2.ShowPrintUI();
        }

        // print documentation can be found here: https://github.com/MicrosoftEdge/WebView2Feedback/pull/2604/files#diff-9b5469f16f89f612e57b016fe9c040f7982e152235a0edd73c234c9d704a961a
        public async Task Print()
        {
            // https://stackoverflow.com/questions/62654738/print-functionality-in-webview2-control
            await webView21.CoreWebView2.PrintAsync(null);
        }

        protected override async void OnCreateControl()
        {
            base.OnCreateControl();
            webView21.CoreWebView2InitializationCompleted += (object? sender, CoreWebView2InitializationCompletedEventArgs e) => { initialized = true; if (InitializationComplete != null) InitializationComplete(); };
            await webView21.EnsureCoreWebView2Async(null);
        }

        public Action? InitializationComplete { get; set; } = null;
        public Action? NavigationComplete { get; set; } = null;

        public bool NavComplete { get; set; }

        // ShowUrl
        public void ShowUrl(string url)
        {
            if (!initialized)
                return;

            NavComplete = false;

            webView21.CoreWebView2.Navigate(url);
        }

        // ShowFile
        public void ShowFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                RootPath = Path.GetDirectoryName(fileName) ?? string.Empty;
            }
            else
                return;

            NavComplete = false;
            string fileText = File.ReadAllText(fileName);

            ShowHtmlText(fileText);
        }

        // ShowText
        public void ShowHtmlText(string textToShow)
        {
            if (!initialized)
                return;

            NavComplete = false;

            webView21.CoreWebView2.NavigateToString(textToShow);
            // This looks promising : https://www.rgraph.net/blog/2023/how-to-add-a-copy-to-clipboard-button-to-your-code-examples.html

            // use docfx instead? : https://dotnet.github.io/docfx/docs/markdown.html?tabs=linux%2Cdotnet

            // http://www.tcbin.com/2017/12/copy-clipboard-button-code-prettify-google.html
            // https://www.codewithfaraz.com/content/164/create-a-code-snippet-box-with-copy-functionality
            // https://stackoverflow.com/questions/49110041/how-can-i-copy-pre-tag-code-into-clipboard-in-html
            // https://www.dbaplus.ca/2021/11/javascriptcss-add-copy-to-clipboard.html
            // mkdocs is written in python : https://www.mkdocs.org/getting-started/
            // https://squidfunk.github.io/mkdocs-material/reference/code-blocks/
        }


        // https://stackoverflow.com/questions/66550671/ensurecorewebview2async-not-ready-even-after-corewebview2initializationcompleted
        //async Task InitializeBrowser()
        //{
        //    webView21.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;

        //    // this bad boy is being a pain in the butt
        //    await webView21.EnsureCoreWebView2Async(null);
        //    //            webView21.CoreWebView2.Navigate("about:blank");


        //    //webView21.CoreWebView2.Navigate("about:blank");
        //    // https://learn.microsoft.com/en-us/microsoft-edge/webview2/concepts/working-with-local-content?tabs=dotnetcsharp#loading-local-content-by-handling-the-webresourcerequested-event
        //    //webView21.CoreWebView2.WebResourceRequested += CoreWebView2_WebResourceRequested;
        //    //webView21.CoreWebView2.Navigate("https://www.microsoft.com");

        //    initialized = true;
        //}



        // Reading of response content stream happens asynchronously, and WebView2 does not 
        // directly dispose the stream once it read.  Therefore, use the following stream
        // class, which properly disposes when WebView2 has read all data.  For details, see
        // [CoreWebView2 does not close stream content](https://github.com/MicrosoftEdge/WebView2Feedback/issues/2513).
        class ManagedStream : Stream
        {
            public ManagedStream(Stream s)
            {
                s_ = s;
            }

            public override bool CanRead => s_.CanRead;

            public override bool CanSeek => s_.CanSeek;

            public override bool CanWrite => s_.CanWrite;

            public override long Length => s_.Length;

            public override long Position { get => s_.Position; set => s_.Position = value; }

            public override void Flush()
            {
                throw new NotImplementedException();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                return s_.Seek(offset, origin);
            }

            public override void SetLength(long value)
            {
                throw new NotImplementedException();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                int read = 0;
                try
                {
                    read = s_.Read(buffer, offset, count);
                    if (read == 0)
                    {
                        s_.Dispose();
                    }
                }
                catch
                {
                    s_.Dispose();
                    throw;
                }
                return read;
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotImplementedException();
            }

            private Stream s_;
        }

        private void webView21_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            NavComplete = true;
            if (NavigationComplete != null)
                NavigationComplete();
        }
    }
}
