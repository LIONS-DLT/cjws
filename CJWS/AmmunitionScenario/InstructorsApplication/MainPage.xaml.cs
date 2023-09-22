using System.Security.Cryptography.X509Certificates;

namespace InstructorsApplication
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

        private void OnClickRequest(object sender, EventArgs e)
        {
            //App.Current.MainPage.Navigation.PushModalAsync(new QrModal("https://github.com/manuelbl/QrCodeGenerator/blob/master/Demo-SkiaSharp/QrCodeBitmapExtensions.cs"));
            if (App.Certificate == null)
            {
                DisplayAlert("No certificate", "You must load a certificate before you can create a request.", "OK");
                return;
            }

            App.Current.MainPage.Navigation.PushAsync(new RequestPage());
        }
    }
}