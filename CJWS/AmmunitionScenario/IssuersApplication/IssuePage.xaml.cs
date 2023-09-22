using AmmunitionLibrary;
using CJWS;
using DataExchangeClient;
using System.Security.Cryptography;
using System.Text;

namespace IssuersApplication;

public partial class IssuePage : ContentPage
{
    private AmmunitionRequest request = null;
    private CJWS2 requestCjws = null;

    public IssuePage(CJWS2 requestCjws)
	{
		InitializeComponent();

        this.requestCjws = requestCjws;
		request = requestCjws.GetPayloadObject<AmmunitionRequest>();

        StringBuilder sb = new StringBuilder();

        sb.AppendLine(string.Format("Order ID: {0}", request.OrderID));
        sb.AppendLine(string.Format("Identifier: {0}", request.Identifier));
        sb.AppendLine(string.Format("Ammunition Type: {0}", request.AmmunitionType));
        sb.AppendLine(string.Format("Amount (Rounds): {0}", request.Rounds));
        sb.AppendLine();

        sb.AppendLine("Signatures:");
        foreach(var signature in requestCjws.Signatures)
        {
            sb.AppendLine(string.Format("{0}", CJWS2.LoadCertificate(signature).Subject));
        }

        labelInfo.Text = sb.ToString();
        inputAmount.Text = request.Rounds.ToString();
	}


    private void Button_Clicked_Send(object sender, EventArgs e)
    {
        AmmunitionIssuance issuance = new AmmunitionIssuance();
        issuance.Request = requestCjws.Serialize();
        issuance.RoundsIssued = int.Parse(inputAmount.Text);
        
        CJWS2 cjws = new CJWS2(new CJWS2Header()
        {
            ContentType = "ammunition-issuance",
            DisplayText = "Ammunition Issuance Receipt " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()
        });

        cjws.SetPayloadObject(issuance);
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