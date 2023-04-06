using Net.Codecrete.QrCodeGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorsApplication
{
    public partial class ShareDataDialog : Form
    {
        string htmlContent = "";

        public ShareDataDialog(string url)
        {
            InitializeComponent();

            string svg = QrCode.EncodeText(url, QrCode.Ecc.Low).ToSvgString(4);
            svg = Convert.ToBase64String(Encoding.UTF8.GetBytes(svg));

            StringBuilder html = new StringBuilder();

            html.AppendLine("<html>");
            html.AppendLine("<body style=\"text-align:center\">");
            html.AppendLine("<img style=\"height:99%\" src =\"data:image/svg+xml;base64," + svg + "\" />");
            html.AppendLine("</body>");
            html.AppendLine("/<html>");

            htmlContent = html.ToString();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitializeAsync();
        }

        private void setContent(string html)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(setContent), html);
                return;
            }

            webView.NavigateToString(html);
        }


        private async Task InitializeAsync()
        {
            await webView.EnsureCoreWebView2Async(null);

            if ((webView == null) || (webView.CoreWebView2 == null))
            {
                Debug.WriteLine("not ready");
            }

            webView.NavigateToString(htmlContent);
        }
    }
}
