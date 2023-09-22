namespace PharmacistsApplication
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_readPrescription = new System.Windows.Forms.Button();
            this.btn_loadCert_Pharmacist = new System.Windows.Forms.Button();
            this.btn_loadCert_Pharmacy = new System.Windows.Forms.Button();
            this.selectCertDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btn_readPrescription
            // 
            this.btn_readPrescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_readPrescription.Enabled = false;
            this.btn_readPrescription.Location = new System.Drawing.Point(12, 156);
            this.btn_readPrescription.Name = "btn_readPrescription";
            this.btn_readPrescription.Size = new System.Drawing.Size(776, 82);
            this.btn_readPrescription.TabIndex = 5;
            this.btn_readPrescription.Text = "Retreive Document";
            this.btn_readPrescription.UseVisualStyleBackColor = true;
            this.btn_readPrescription.Click += new System.EventHandler(this.btn_retreiveDocument_Click);
            // 
            // btn_loadCert_Pharmacist
            // 
            this.btn_loadCert_Pharmacist.Location = new System.Drawing.Point(364, 12);
            this.btn_loadCert_Pharmacist.Name = "btn_loadCert_Pharmacist";
            this.btn_loadCert_Pharmacist.Size = new System.Drawing.Size(333, 46);
            this.btn_loadCert_Pharmacist.TabIndex = 4;
            this.btn_loadCert_Pharmacist.Text = "Load Certificate (Pharmacist)";
            this.btn_loadCert_Pharmacist.UseVisualStyleBackColor = true;
            this.btn_loadCert_Pharmacist.Click += new System.EventHandler(this.btn_loadCert_Pharmacist_Click);
            // 
            // btn_loadCert_Pharmacy
            // 
            this.btn_loadCert_Pharmacy.Location = new System.Drawing.Point(12, 12);
            this.btn_loadCert_Pharmacy.Name = "btn_loadCert_Pharmacy";
            this.btn_loadCert_Pharmacy.Size = new System.Drawing.Size(346, 46);
            this.btn_loadCert_Pharmacy.TabIndex = 3;
            this.btn_loadCert_Pharmacy.Text = "Load Certificate (Pharmacy)";
            this.btn_loadCert_Pharmacy.UseVisualStyleBackColor = true;
            this.btn_loadCert_Pharmacy.Click += new System.EventHandler(this.btn_loadCert_Pharmacy_Click);
            // 
            // selectCertDialog
            // 
            this.selectCertDialog.Title = "Select Certificate";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_readPrescription);
            this.Controls.Add(this.btn_loadCert_Pharmacist);
            this.Controls.Add(this.btn_loadCert_Pharmacy);
            this.Name = "MainForm";
            this.Text = "Pharmacists Application";
            this.ResumeLayout(false);

        }

        #endregion

        private Button btn_readPrescription;
        private Button btn_loadCert_Pharmacist;
        private Button btn_loadCert_Pharmacy;
        private OpenFileDialog selectCertDialog;
    }
}