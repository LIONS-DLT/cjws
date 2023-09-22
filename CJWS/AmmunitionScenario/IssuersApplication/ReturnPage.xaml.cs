using AmmunitionLibrary;
using CJWS;
using DataExchangeClient;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace IssuersApplication;

public partial class ReturnPage : ContentPage
{
    private AmmunitionIssuance issuance = null;
    private AmmunitionRequest request = null;
    private CJWS2 receiptCjws = null;
    private CJWS2 requestCjws = null;

    public ReturnPage(CJWS2 receiptCjws)
    {
        InitializeComponent();

        this.receiptCjws = receiptCjws;
        issuance = receiptCjws.GetPayloadObject<AmmunitionIssuance>();
        this.requestCjws = CJWS2.Deserialize(issuance.Request);
        request = this.requestCjws.GetPayloadObject<AmmunitionRequest>();

        StringBuilder sb = new StringBuilder();

        sb.AppendLine(string.Format("Order ID: {0}", request.OrderID));
        sb.AppendLine(string.Format("Identifier: {0}", request.Identifier));
        sb.AppendLine(string.Format("Ammunition Type: {0}", request.AmmunitionType));
        sb.AppendLine(string.Format("Amount (Rounds): {0}", request.Rounds));
        sb.AppendLine();

        sb.AppendLine("Signatures:");
        foreach (var signature in requestCjws.Signatures)
        {
            sb.AppendLine(string.Format("{0}", CJWS2.LoadCertificate(signature).Subject));
        }
        sb.AppendLine();
        sb.AppendLine();
        sb.AppendLine(string.Format("Amount Issued (Rounds): {0}", issuance.RoundsIssued));
        sb.AppendLine();
        sb.AppendLine("Signatures:");
        foreach (var signature in receiptCjws.Signatures)
        {
            sb.AppendLine(string.Format("{0}", CJWS2.LoadCertificate(signature).Subject));
        }

        labelInfo.Text = sb.ToString();
        inputAmount.Text = issuance.RoundsIssued.ToString();
    }


    private void Button_Clicked_Send(object sender, EventArgs e)
    {
        AmmunitionReturnReceipt receipt = new AmmunitionReturnReceipt();
        receipt.Issuance = receiptCjws.Serialize();
        receipt.Request = requestCjws.Serialize();
        receipt.RoundsReturned = int.Parse(inputAmount.Text);

        CJWS2 cjws = new CJWS2(new CJWS2Header()
        {
            ContentType = "ammunition-return-receipt",
            DisplayText = "Ammunition Return Receipt " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()
        });

        cjws.SetPayloadObject(receipt);
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