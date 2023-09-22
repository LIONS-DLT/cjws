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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorsApplication
{
    public partial class TPrescriptionInputForm : Form
    {
        public TPrescriptionInputForm()
        {
            InitializeComponent();
        }

        private TPrescriptionForm createPrescriptionFromInputs()
        {
            TPrescriptionForm prescriptionForm = new TPrescriptionForm();

            prescriptionForm.Noctu = cb_noctu.Checked;
            prescriptionForm.Chargeable = cb_Chargeable.Checked;
            prescriptionForm.Chargefree = cb_chargefree.Checked;
            prescriptionForm.InsuranceId = tb_insurance_id.Text;
            prescriptionForm.InsuranceName = tb_insurance_name.Text;
            prescriptionForm.AutIdem = cb_autIdem.Checked;
            prescriptionForm.CoPayment = (double)num_copayment.Value;
            prescriptionForm.PatientDateOfBirth = tb_patient_dateOfBirth.Text;
            prescriptionForm.PatientFirstName = tb_patient_firstname.Text;
            prescriptionForm.PatientInsuranceId = tb_patient_insuranceId.Text;
            prescriptionForm.PatientInsuranceStatus = tb_patient_insuranceStatus.Text;
            prescriptionForm.PatientLastName = tb_patient_lastname.Text;
            prescriptionForm.PhysiciansId = tb_doctor_id.Text;
            prescriptionForm.PhysiciansOfficeId = tb_doctor_officeId.Text;
            prescriptionForm.PrescriptionNumber = tb_prescription_id.Text;
            prescriptionForm.Total = (double)num_total.Value;
            prescriptionForm.TreatmentInLabel = cb_inlabel.Checked;
            prescriptionForm.TreatmentOffLabel = cb_offlabel.Checked;

            foreach (DataGridViewRow row in grid_items.Rows)
            {
                if (row.IsNewRow)
                    continue;
                TPrescriptionItem item = new TPrescriptionItem();

                item.Product = (string)row.Cells["Product"].Value;
                item.Factor = double.Parse((string)row.Cells["Factor"].Value);
                item.Fee = double.Parse((string)row.Cells["Fee"].Value);

                prescriptionForm.PrescriptionItems.Add(item);
            }

            return prescriptionForm;
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            TPrescriptionForm prescriptionForm = createPrescriptionFromInputs();

            TPrescriptionCarbonCopy carbonCopy = prescriptionForm.CreateCarbonCopy();

            TPrescription prescription = new TPrescription();


            // Create the form serailized as CJWS2 string.
            CJWS2 prescriptionFormCJWS = new CJWS2(new CJWS2Header()
            {
                ContentType = "german-t-prescription-form",
                DisplayText = "Form"
            });
            prescriptionFormCJWS.SetPayloadObject(prescriptionForm);
            prescriptionFormCJWS.Sign(MainForm.officeCertificate!, HashAlgorithmName.SHA512);
            prescriptionFormCJWS.Sign(MainForm.doctorsCertificate!, HashAlgorithmName.SHA512);

            prescription.Form = prescriptionFormCJWS.Serialize();


            // Create the carbon copy serailized as CJWS2 string.
            CJWS2 carbonCopyCJWS = new CJWS2(new CJWS2Header()
            {
                ContentType = "german-t-prescription-copy",
                DisplayText = "Carbon Copy"
            });
            carbonCopyCJWS.SetPayloadObject(carbonCopy);
            carbonCopyCJWS.Sign(MainForm.officeCertificate!, HashAlgorithmName.SHA512);

            prescription.CarbonCopy = carbonCopyCJWS.Serialize();


            // Create the complete prescription serailized as CJWS2 string.
            CJWS2 prescriptionCJWS = new CJWS2(new CJWS2Header()
            {
                ContentType = "german-t-prescription",
                DisplayText = "T-Rezept " + DateTime.Now.ToShortDateString()
            });

            prescriptionCJWS.SetPayloadObject(prescription);
            prescriptionCJWS.Sign(MainForm.officeCertificate!, HashAlgorithmName.SHA512);

            string prescriptionString = prescriptionCJWS.Serialize();

            storePrescription(prescriptionString);

            sharePrescription(prescriptionString);
        }

        private void storePrescription(string prescriptionString)
        {
            // store in database or whatever...
        }

        private void sharePrescription(string prescriptionString)
        {
            string baseUrl = PrescriptionScenarioConfig.DxsBaseUrl;
            string apiKey = PrescriptionScenarioConfig.DxsApiKey;

            DataExchangeClient.DataExchangeService service = new DataExchangeClient.DataExchangeService(baseUrl, apiKey);

            byte[] data = Encoding.UTF8.GetBytes(prescriptionString);

            string key = CryptHelper.GenerateAESKey();
            data = CryptHelper.EncryptAES(data, key);

            string downloadUrl = service.UploadMessage(data).RetrieveUrl;

            string shareUrl = string.Format("cjws:retrieve/{0}/{1}", Uri.EscapeDataString(downloadUrl), Uri.EscapeDataString(key));

            ShareDataDialog dialog = new ShareDataDialog(shareUrl);
            dialog.ShowDialog();
        }


    }
}
