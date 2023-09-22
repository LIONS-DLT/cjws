using AmmunitionLibrary;
using CJWS;
using DataExchangeClient;
using System.Security.Cryptography;
using System.Text;

namespace InstructorsApplication;

public partial class RequestPage : ContentPage
{
	public RequestPage()
	{
		InitializeComponent();
	}

    private void Button_Clicked_Send(object sender, EventArgs e)
    {
        AmmunitionRequest request = new AmmunitionRequest();
        request.OrderID = inputOrderId.Text;
        request.Identifier = inputIdentifier.Text;
        request.AmmunitionType = inputAmmunitionType.SelectedItem as string;
        request.Rounds = int.Parse(inputAmount.Text);


        CJWS2 cjws = new CJWS2(new CJWS2Header()
        {
            ContentType = "ammunition-request",
            DisplayText = "Ammunition Request " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()
        });

        cjws.SetPayloadObject(request);
        cjws.Sign(App.Certificate, HashAlgorithmName.SHA512);

        string requestString = cjws.Serialize();

        string baseUrl = AmmunitionScenarioConfig.DxsBaseUrl;
        string apiKey = AmmunitionScenarioConfig.DxsApiKey;

        DataExchangeClient.DataExchangeService service = new DataExchangeClient.DataExchangeService(baseUrl, apiKey);

        byte[] data = Encoding.UTF8.GetBytes(requestString);

        string key = CryptHelper.GenerateAESKey();
        data = CryptHelper.EncryptAES(data, key);

        string downloadUrl = service.UploadMessage(data).RetrieveUrl;

        string shareUrl = string.Format("cjws:retrieve/{0}/{1}", Uri.EscapeDataString(downloadUrl), Uri.EscapeDataString(key));


        App.Current.MainPage.Navigation.PushModalAsync(new QrModal(shareUrl));
    }

    private void Button_Clicked_Cancel(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PopAsync();
    }
}