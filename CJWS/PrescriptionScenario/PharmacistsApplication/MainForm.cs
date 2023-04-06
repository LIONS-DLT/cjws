using CJWS;
using DataExchangeClient;
using PrescriptionLibrary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace PharmacistsApplication
{
    public partial class MainForm : Form
    {
        public static X509Certificate2? officeCertificate { get; private set; }
        public static X509Certificate2? pharmacistsCertificate { get; private set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_loadCert_Pharmacy_Click(object sender, EventArgs e)
        {
            if (selectCertDialog.ShowDialog() != DialogResult.OK)
                return;

            PasswordDialog pwDialog = new PasswordDialog();
            if (pwDialog.ShowDialog() != DialogResult.OK)
                return;
            try
            {
                officeCertificate = new X509Certificate2(selectCertDialog.FileName, pwDialog.Password);
                btn_readPrescription.Enabled = officeCertificate != null && pharmacistsCertificate != null;
                btn_loadCert_Pharmacy.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_loadCert_Pharmacist_Click(object sender, EventArgs e)
        {
            if (selectCertDialog.ShowDialog() != DialogResult.OK)
                return;

            PasswordDialog pwDialog = new PasswordDialog();
            if (pwDialog.ShowDialog() != DialogResult.OK)
                return;
            try
            {
                pharmacistsCertificate = new X509Certificate2(selectCertDialog.FileName, pwDialog.Password);
                btn_readPrescription.Enabled = officeCertificate != null && pharmacistsCertificate != null;
                btn_loadCert_Pharmacist.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_retreiveDocument_Click(object sender, EventArgs e)
        {
            string baseUrl = "http://192.168.178.98:5059/App";
            string apiKey = "SGVsbG8gUHJlc2NyaXB0aW9uIEV4YW1wbGUh";

            DataExchangeClient.DataExchangeService service = new DataExchangeClient.DataExchangeService(baseUrl, apiKey);
            ShareDataDialog? dialog = null;

            string key = CryptHelper.GenerateAESKey();

            var result = service.RegisterMessageWithCallback((byte[]? data) =>
            {
                if(data == null)
                    return;

                openDocument(data, key);

                if (dialog != null)
                    dialog.Invoke(new Action(dialog.Close));
            });

            string shareUrl = string.Format("cjws:send/{0}/{1}", Uri.EscapeDataString(result.UploadUrl), Uri.EscapeDataString(key));
            dialog = new ShareDataDialog(shareUrl);
            dialog.ShowDialog();
        }

        private void openDocument(byte[] data, string key)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<byte[],string>(openDocument), data, key);
                return;
            }

            data = CryptHelper.DecryptAES(data, key);

            string cjwsString = Encoding.UTF8.GetString(data);
            CJWSHeaderInfo info = CJWS.CJWS.ExtractHeaderFromString(cjwsString);

            if (info.ContentType != "german-t-prescription")
            {
                MessageBox.Show(string.Format("Unknown content type: {0}", info.ContentType), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            CJWS2 document = CJWS2.Deserialize(cjwsString);
            if(!document.Verify())
            {
                MessageBox.Show("Document signatures invalid.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TPrescription prescription = document.GetPayloadObject<TPrescription>();
            CJWS2 prescriptionFormCJWS = CJWS2.Deserialize(prescription.Form);
            if (!prescriptionFormCJWS.Verify())
            {
                MessageBox.Show("Prescription form signatures invalid.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            CJWS2 prescriptionCopyCJWS = CJWS2.Deserialize(prescription.CarbonCopy);
            if (!prescriptionCopyCJWS.Verify())
            {
                MessageBox.Show("Prescription copy signatures invalid.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TPrescriptionForm form = prescriptionFormCJWS.GetPayloadObject<TPrescriptionForm>();
            TPrescriptionCarbonCopy copy = prescriptionCopyCJWS.GetPayloadObject<TPrescriptionCarbonCopy>();

            TPrescriptionViewer viewer = new TPrescriptionViewer(prescriptionFormCJWS, form, prescriptionCopyCJWS, copy);
            viewer.Show();
        }
    }
}