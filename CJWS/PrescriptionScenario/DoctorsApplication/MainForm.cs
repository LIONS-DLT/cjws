using System.Security.Cryptography.X509Certificates;

namespace DoctorsApplication
{
    public partial class MainForm : Form
    {
        public static X509Certificate2? officeCertificate { get; private set; }
        public static X509Certificate2? doctorsCertificate { get; private set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_createTPrescription_Click(object sender, EventArgs e)
        {
            TPrescriptionInputForm form = new TPrescriptionInputForm();
            form.Show();
        }

        private void btn_loadCert_Office_Click(object sender, EventArgs e)
        {
            if (selectCertDialog.ShowDialog() != DialogResult.OK)
                return;

            PasswordDialog pwDialog = new PasswordDialog();
            if(pwDialog.ShowDialog() != DialogResult.OK)
                return;
            try
            {
                officeCertificate = new X509Certificate2(selectCertDialog.FileName, pwDialog.Password);
                btn_createTPrescription.Enabled = officeCertificate != null && doctorsCertificate != null;
                btn_loadCert_Office.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_loadCert_Doctor_Click(object sender, EventArgs e)
        {
            if (selectCertDialog.ShowDialog() != DialogResult.OK)
                return;

            PasswordDialog pwDialog = new PasswordDialog();
            if (pwDialog.ShowDialog() != DialogResult.OK)
                return;
            try
            {
                doctorsCertificate = new X509Certificate2(selectCertDialog.FileName, pwDialog.Password);
                btn_createTPrescription.Enabled = officeCertificate != null && doctorsCertificate != null;
                btn_loadCert_Doctor.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}