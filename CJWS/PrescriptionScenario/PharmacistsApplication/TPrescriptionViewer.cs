using CJWS;
using DataExchangeClient;
using PrescriptionLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacistsApplication
{
    public partial class TPrescriptionViewer : Form
    {
        private CJWS2 _prescriptionFormCJWS;
        private CJWS2 _carbonCopyCJWS;
        private TPrescriptionForm _prescriptionForm;
        private TPrescriptionCarbonCopy _carbonCopy;

        public TPrescriptionViewer(CJWS2 formCJWS, TPrescriptionForm form, CJWS2 copyCJWS, TPrescriptionCarbonCopy copy)
        {
            _carbonCopy = copy;
            _carbonCopyCJWS = copyCJWS;
            _prescriptionForm = form;
            _prescriptionFormCJWS = formCJWS;

            InitializeComponent();

            StringBuilder info = new StringBuilder();
            info.AppendLine("INSURANCE");
            info.AppendLine(string.Format("Insurance Name: {0}", form.InsuranceName));
            info.AppendLine(string.Format("Insurance ID:   {0}", form.InsuranceId));
            info.AppendLine(string.Format("Patient ID:     {0}", form.PatientInsuranceId));
            info.AppendLine(string.Format("Status:         {0}", form.PatientInsuranceStatus));
            info.AppendLine();

            info.AppendLine("PATIENT");
            info.AppendLine(string.Format("Name:          {0} {1}", form.PatientFirstName, form.PatientLastName));
            info.AppendLine(string.Format("Date of birth: {0}", form.PatientDateOfBirth));
            info.AppendLine();

            info.AppendLine("PHYSICIAN");
            info.AppendLine(string.Format("Office ID:    {0}", form.PhysiciansOfficeId));
            info.AppendLine(string.Format("Physician ID: {0}", form.PhysiciansId));
            info.AppendLine();

            info.AppendLine("T-PRESCRIPTION");
            info.AppendLine(string.Format("Aut Idem:   {0}", form.AutIdem ? "X" : "-"));
            info.AppendLine(string.Format("Noctu:      {0}", form.Noctu ? "X" : "-"));
            info.AppendLine(string.Format("Chargeable: {0}", form.Chargeable ? "X" : "-"));
            info.AppendLine(string.Format("Chargefree: {0}", form.Chargefree ? "X" : "-"));
            info.AppendLine(string.Format("In-label:   {0}", form.TreatmentInLabel ? "X" : "-"));
            info.AppendLine(string.Format("Off-label:  {0}", form.TreatmentInLabel ? "X" : "-"));

            info.AppendLine();
            info.AppendLine(string.Format("Total:      {0}", form.Total.ToString("0.00")));
            info.AppendLine(string.Format("Co-Payment: {0}", form.CoPayment.ToString("0.00")));
            info.AppendLine();

            foreach(var item in form.PrescriptionItems)
            {
                info.AppendLine(string.Format("Product: {0}", item.Product));
                info.AppendLine(string.Format("Factor:  {0}", item.Factor.ToString()));
                info.AppendLine(string.Format("Fee:     {0}", item.Fee.ToString("0.00")));
                info.AppendLine();
            }

            info.AppendLine("SIGNATURES");

            foreach(var signature in formCJWS.Signatures)
            {
                X509Certificate2 cert = CJWS.CJWS.LoadCertificate(signature);
                X509Chain chain = new X509Chain();
                bool isValid = chain.Build(cert);
                info.AppendLine(string.Format("Valid:   {0}", isValid.ToString()));
                info.AppendLine(string.Format("Subject: {0}", cert.Subject));

                foreach (var c in chain.ChainElements)
                {
                    if (c.Certificate.Subject != cert.Subject)
                        info.AppendLine(string.Format("CA:      {0}", c.Certificate.Subject));
                }
                info.AppendLine();
            }


            textBox1.Text = info.ToString();
        }

        private void button_signAndReturn_Click(object sender, EventArgs e)
        {
            _prescriptionFormCJWS.Sign(MainForm.officeCertificate!, HashAlgorithmName.SHA512);
            _prescriptionFormCJWS.Sign(MainForm.pharmacistsCertificate!, HashAlgorithmName.SHA512);

            _carbonCopyCJWS.Sign(MainForm.officeCertificate!, HashAlgorithmName.SHA512);
            _carbonCopyCJWS.Sign(MainForm.pharmacistsCertificate!, HashAlgorithmName.SHA512);

            forwardToBfArM(_carbonCopyCJWS);

            returnToPatient(_prescriptionFormCJWS);
        }

        private void forwardToBfArM(CJWS2 carbonCopy)
        {
            // store carbon copy intil forwarding to BfArM...
        }

        private void returnToPatient(CJWS2 form)
        {
            string baseUrl = "http://192.168.178.98:5059/App";
            string apiKey = "SGVsbG8gUHJlc2NyaXB0aW9uIEV4YW1wbGUh";

            DataExchangeClient.DataExchangeService service = new DataExchangeClient.DataExchangeService(baseUrl, apiKey);

            byte[] data = Encoding.UTF8.GetBytes(form.Serialize());

            string key = CryptHelper.GenerateAESKey();
            data = CryptHelper.EncryptAES(data, key);

            string downloadUrl = service.UploadMessage(data).RetrieveUrl;

            string shareUrl = string.Format("cjws:retrieve/{0}/{1}", Uri.EscapeDataString(downloadUrl), Uri.EscapeDataString(key));

            ShareDataDialog dialog = new ShareDataDialog(shareUrl);
            dialog.ShowDialog();
        }


    }
}
