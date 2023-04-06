namespace DoctorsApplication
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
            this.btn_loadCert_Office = new System.Windows.Forms.Button();
            this.btn_loadCert_Doctor = new System.Windows.Forms.Button();
            this.btn_createTPrescription = new System.Windows.Forms.Button();
            this.selectCertDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btn_loadCert_Office
            // 
            this.btn_loadCert_Office.Location = new System.Drawing.Point(12, 12);
            this.btn_loadCert_Office.Name = "btn_loadCert_Office";
            this.btn_loadCert_Office.Size = new System.Drawing.Size(314, 46);
            this.btn_loadCert_Office.TabIndex = 0;
            this.btn_loadCert_Office.Text = "Load Certificate (Office)";
            this.btn_loadCert_Office.UseVisualStyleBackColor = true;
            this.btn_loadCert_Office.Click += new System.EventHandler(this.btn_loadCert_Office_Click);
            // 
            // btn_loadCert_Doctor
            // 
            this.btn_loadCert_Doctor.Location = new System.Drawing.Point(332, 12);
            this.btn_loadCert_Doctor.Name = "btn_loadCert_Doctor";
            this.btn_loadCert_Doctor.Size = new System.Drawing.Size(314, 46);
            this.btn_loadCert_Doctor.TabIndex = 1;
            this.btn_loadCert_Doctor.Text = "Load Certificate (Doctor)";
            this.btn_loadCert_Doctor.UseVisualStyleBackColor = true;
            this.btn_loadCert_Doctor.Click += new System.EventHandler(this.btn_loadCert_Doctor_Click);
            // 
            // btn_createTPrescription
            // 
            this.btn_createTPrescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_createTPrescription.Enabled = false;
            this.btn_createTPrescription.Location = new System.Drawing.Point(12, 156);
            this.btn_createTPrescription.Name = "btn_createTPrescription";
            this.btn_createTPrescription.Size = new System.Drawing.Size(864, 82);
            this.btn_createTPrescription.TabIndex = 2;
            this.btn_createTPrescription.Text = "Create T-Prescription";
            this.btn_createTPrescription.UseVisualStyleBackColor = true;
            this.btn_createTPrescription.Click += new System.EventHandler(this.btn_createTPrescription_Click);
            // 
            // selectCertDialog
            // 
            this.selectCertDialog.Title = "Select Certificate";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 613);
            this.Controls.Add(this.btn_createTPrescription);
            this.Controls.Add(this.btn_loadCert_Doctor);
            this.Controls.Add(this.btn_loadCert_Office);
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.ResumeLayout(false);

        }

        #endregion

        private Button btn_loadCert_Office;
        private Button btn_loadCert_Doctor;
        private Button btn_createTPrescription;
        private OpenFileDialog selectCertDialog;
    }
}