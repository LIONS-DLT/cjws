using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorsApplication
{
    public partial class PasswordDialog : Form
    {
        public string Password { get; private set; } = string.Empty;

        public PasswordDialog()
        {
            InitializeComponent();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            this.Password = tb_password.Text;
            if (string.IsNullOrEmpty(this.Password))
                return;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
