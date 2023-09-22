using AmmunitionLibrary;
using CJWS;
using DataExchangeClient;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace IssuersApplication
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnClickLoadCert(object sender, EventArgs e)
        {
            var certFileType = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                    { DevicePlatform.iOS, new[] { "public.pkcs12" } }, // UTType values
                    { DevicePlatform.Android, new[] { "application/x-pkcs12" } }, // MIME type
                    { DevicePlatform.WinUI, new[] { ".p12", ".pfx" } }, // file extension
                    { DevicePlatform.Tizen, new[] { "*/*" } },
                    { DevicePlatform.macOS, new[] { "p12", "pfx" } }, // UTType values
                    });

            PickOptions options = new()
            {
                PickerTitle = "Please select a certificate file",
                FileTypes = certFileType,
            };

            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                string filename = Path.GetFileName(result.FullPath);
                string filenpath = result.FullPath;

                if (filename.EndsWith(".p12", StringComparison.OrdinalIgnoreCase) ||
                    filename.EndsWith(".pfx", StringComparison.OrdinalIgnoreCase))
                {
                    await Task.Delay(3000);
                    await App.Current.MainPage.Navigation.PushModalAsync(new PasswordModal((string pswd) =>
                    {
                        if (string.IsNullOrEmpty(pswd))
                            return;

                        try
                        {
                            X509Certificate2 cert = new X509Certificate2(filenpath, pswd);

                            App.Certificate = cert;
                        }
                        catch
                        {
                            DisplayAlert("Invalid", "Password or certificate is invalid or not supportet on this platform.", "OK");
                        }

                    }));
                }
            }
        }


        private void OnClickIssue(object sender, EventArgs e)
        {
            if (App.Certificate == null)
            {
                DisplayAlert("No certificate", "You must load a certificate before you can create a request.", "OK");
                return;
            }

            string baseUrl = AmmunitionScenarioConfig.DxsBaseUrl;
            string apiKey = AmmunitionScenarioConfig.DxsApiKey;

            DataExchangeService service = new DataExchangeService(baseUrl, apiKey);

            string key = CryptHelper.GenerateAESKey();

            var result = service.RegisterMessageWithCallback((byte[] data) =>
            {
                if (data == null)
                    return;

                data = CryptHelper.DecryptAES(data, key);

                Dispatcher.Dispatch(() =>
                {
                    openRequest(data);
                });

                App.Current.MainPage.Navigation.PopModalAsync();
            });

            string shareUrl = string.Format("cjws:send/{0}/{1}", Uri.EscapeDataString(result.UploadUrl), Uri.EscapeDataString(key));
            App.Current.MainPage.Navigation.PushModalAsync(new QrModal(shareUrl));
        }

        private void OnClickReturn(object sender, EventArgs e)
        {
            if (App.Certificate == null)
            {
                DisplayAlert("No certificate", "You must load a certificate before you can create a request.", "OK");
                return;
            }

            string baseUrl = AmmunitionScenarioConfig.DxsBaseUrl;
            string apiKey = AmmunitionScenarioConfig.DxsApiKey;

            DataExchangeService service = new DataExchangeService(baseUrl, apiKey);

            string key = CryptHelper.GenerateAESKey();

            var result = service.RegisterMessageWithCallback((byte[] data) =>
            {
                if (data == null)
                    return;

                data = CryptHelper.DecryptAES(data, key);

                Dispatcher.Dispatch(() =>
                {
                    openReturn(data);
                });

                App.Current.MainPage.Navigation.PopModalAsync();
            });

            string shareUrl = string.Format("cjws:send/{0}/{1}", Uri.EscapeDataString(result.UploadUrl), Uri.EscapeDataString(key));
            App.Current.MainPage.Navigation.PushModalAsync(new QrModal(shareUrl));
        }

        private void openRequest(byte[] data)
        {
            string cjwsString = Encoding.UTF8.GetString(data);
            CJWSHeaderInfo info = CJWS.CJWS.ExtractHeaderFromString(cjwsString);

            if (info.ContentType != "ammunition-request")
            {
                DisplayAlert("ERROR", string.Format("Invalid content type: {0}", info.ContentType), "OK");
                return;
            }

            CJWS2 document = CJWS2.Deserialize(cjwsString);
            if (!document.Verify(false))
            {
                DisplayAlert("ERROR", "Document signatures invalid.", "OK");
                return;
            }
            App.Current.MainPage.Navigation.PushAsync(new IssuePage(document));
        }
        private void openReturn(byte[] data)
        {
            string cjwsString = Encoding.UTF8.GetString(data);
            CJWSHeaderInfo info = CJWS.CJWS.ExtractHeaderFromString(cjwsString);

            if (info.ContentType != "ammunition-issuance")
            {
                DisplayAlert("ERROR", string.Format("Invalid content type: {0}", info.ContentType), "OK");
                return;
            }

            CJWS2 document = CJWS2.Deserialize(cjwsString);
            if (!document.Verify(false))
            {
                DisplayAlert("ERROR", "Document signatures invalid.", "OK");
                return;
            }
            App.Current.MainPage.Navigation.PushAsync(new ReturnPage(document));
        }
    }
}