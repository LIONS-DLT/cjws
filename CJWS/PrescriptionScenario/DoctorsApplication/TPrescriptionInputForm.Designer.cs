namespace DoctorsApplication
{
    partial class TPrescriptionInputForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cb_Chargeable = new System.Windows.Forms.CheckBox();
            this.cb_chargefree = new System.Windows.Forms.CheckBox();
            this.cb_noctu = new System.Windows.Forms.CheckBox();
            this.cb_autIdem = new System.Windows.Forms.CheckBox();
            this.cb_inlabel = new System.Windows.Forms.CheckBox();
            this.cb_offlabel = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_insurance_name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_insurance_id = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_patient_lastname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_patient_firstname = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_patient_insuranceId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_patient_dateOfBirth = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_patient_insuranceStatus = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_doctor_id = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_doctor_officeId = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.num_total = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.num_copayment = new System.Windows.Forms.NumericUpDown();
            this.grid_items = new System.Windows.Forms.DataGridView();
            this.Product = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Factor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_create = new System.Windows.Forms.Button();
            this.tb_prescription_id = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.num_total)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_copayment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_items)).BeginInit();
            this.SuspendLayout();
            // 
            // cb_Chargeable
            // 
            this.cb_Chargeable.AutoSize = true;
            this.cb_Chargeable.Location = new System.Drawing.Point(12, 12);
            this.cb_Chargeable.Name = "cb_Chargeable";
            this.cb_Chargeable.Size = new System.Drawing.Size(167, 36);
            this.cb_Chargeable.TabIndex = 0;
            this.cb_Chargeable.Text = "Chargeable";
            this.cb_Chargeable.UseVisualStyleBackColor = true;
            // 
            // cb_chargefree
            // 
            this.cb_chargefree.AutoSize = true;
            this.cb_chargefree.Location = new System.Drawing.Point(217, 12);
            this.cb_chargefree.Name = "cb_chargefree";
            this.cb_chargefree.Size = new System.Drawing.Size(164, 36);
            this.cb_chargefree.TabIndex = 1;
            this.cb_chargefree.Text = "Chargefree";
            this.cb_chargefree.UseVisualStyleBackColor = true;
            // 
            // cb_noctu
            // 
            this.cb_noctu.AutoSize = true;
            this.cb_noctu.Location = new System.Drawing.Point(429, 12);
            this.cb_noctu.Name = "cb_noctu";
            this.cb_noctu.Size = new System.Drawing.Size(111, 36);
            this.cb_noctu.TabIndex = 2;
            this.cb_noctu.Text = "Noctu";
            this.cb_noctu.UseVisualStyleBackColor = true;
            // 
            // cb_autIdem
            // 
            this.cb_autIdem.AutoSize = true;
            this.cb_autIdem.Location = new System.Drawing.Point(590, 12);
            this.cb_autIdem.Name = "cb_autIdem";
            this.cb_autIdem.Size = new System.Drawing.Size(144, 36);
            this.cb_autIdem.TabIndex = 3;
            this.cb_autIdem.Text = "Aut idem";
            this.cb_autIdem.UseVisualStyleBackColor = true;
            // 
            // cb_inlabel
            // 
            this.cb_inlabel.AutoSize = true;
            this.cb_inlabel.Location = new System.Drawing.Point(829, 12);
            this.cb_inlabel.Name = "cb_inlabel";
            this.cb_inlabel.Size = new System.Drawing.Size(127, 36);
            this.cb_inlabel.TabIndex = 4;
            this.cb_inlabel.Text = "In-label";
            this.cb_inlabel.UseVisualStyleBackColor = true;
            // 
            // cb_offlabel
            // 
            this.cb_offlabel.AutoSize = true;
            this.cb_offlabel.Location = new System.Drawing.Point(998, 12);
            this.cb_offlabel.Name = "cb_offlabel";
            this.cb_offlabel.Size = new System.Drawing.Size(141, 36);
            this.cb_offlabel.TabIndex = 5;
            this.cb_offlabel.Text = "Off-label";
            this.cb_offlabel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 32);
            this.label1.TabIndex = 6;
            this.label1.Text = "Insturance";
            // 
            // tb_insurance_name
            // 
            this.tb_insurance_name.Location = new System.Drawing.Point(12, 166);
            this.tb_insurance_name.Name = "tb_insurance_name";
            this.tb_insurance_name.Size = new System.Drawing.Size(297, 39);
            this.tb_insurance_name.TabIndex = 9;
            this.tb_insurance_name.Text = "Insurance Corp.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 32);
            this.label2.TabIndex = 8;
            this.label2.Text = "Insurance Name";
            // 
            // tb_insurance_id
            // 
            this.tb_insurance_id.Location = new System.Drawing.Point(315, 166);
            this.tb_insurance_id.Name = "tb_insurance_id";
            this.tb_insurance_id.Size = new System.Drawing.Size(297, 39);
            this.tb_insurance_id.TabIndex = 11;
            this.tb_insurance_id.Text = "IC1234567";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(315, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 32);
            this.label3.TabIndex = 10;
            this.label3.Text = "Insurance ID";
            // 
            // tb_patient_lastname
            // 
            this.tb_patient_lastname.Location = new System.Drawing.Point(315, 309);
            this.tb_patient_lastname.Name = "tb_patient_lastname";
            this.tb_patient_lastname.Size = new System.Drawing.Size(297, 39);
            this.tb_patient_lastname.TabIndex = 15;
            this.tb_patient_lastname.Text = "Doe";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(315, 264);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 32);
            this.label4.TabIndex = 14;
            this.label4.Text = "Last Name";
            // 
            // tb_patient_firstname
            // 
            this.tb_patient_firstname.Location = new System.Drawing.Point(12, 309);
            this.tb_patient_firstname.Name = "tb_patient_firstname";
            this.tb_patient_firstname.Size = new System.Drawing.Size(297, 39);
            this.tb_patient_firstname.TabIndex = 13;
            this.tb_patient_firstname.Text = "John";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(12, 264);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 32);
            this.label5.TabIndex = 12;
            this.label5.Text = "First Name";
            // 
            // tb_patient_insuranceId
            // 
            this.tb_patient_insuranceId.Location = new System.Drawing.Point(12, 507);
            this.tb_patient_insuranceId.Name = "tb_patient_insuranceId";
            this.tb_patient_insuranceId.Size = new System.Drawing.Size(297, 39);
            this.tb_patient_insuranceId.TabIndex = 19;
            this.tb_patient_insuranceId.Text = "ABC1234XYZ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(12, 462);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(224, 32);
            this.label6.TabIndex = 18;
            this.label6.Text = "Insurance Person ID";
            // 
            // tb_patient_dateOfBirth
            // 
            this.tb_patient_dateOfBirth.Location = new System.Drawing.Point(12, 407);
            this.tb_patient_dateOfBirth.Name = "tb_patient_dateOfBirth";
            this.tb_patient_dateOfBirth.Size = new System.Drawing.Size(297, 39);
            this.tb_patient_dateOfBirth.TabIndex = 17;
            this.tb_patient_dateOfBirth.Text = "1985/01/01";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(12, 362);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 32);
            this.label7.TabIndex = 16;
            this.label7.Text = "Date of birth";
            // 
            // tb_patient_insuranceStatus
            // 
            this.tb_patient_insuranceStatus.Location = new System.Drawing.Point(315, 507);
            this.tb_patient_insuranceStatus.Name = "tb_patient_insuranceStatus";
            this.tb_patient_insuranceStatus.Size = new System.Drawing.Size(297, 39);
            this.tb_patient_insuranceStatus.TabIndex = 21;
            this.tb_patient_insuranceStatus.Text = "public";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(315, 462);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(187, 32);
            this.label8.TabIndex = 20;
            this.label8.Text = "Insurance Status";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(12, 232);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 32);
            this.label9.TabIndex = 22;
            this.label9.Text = "Patient";
            // 
            // tb_doctor_id
            // 
            this.tb_doctor_id.Location = new System.Drawing.Point(315, 653);
            this.tb_doctor_id.Name = "tb_doctor_id";
            this.tb_doctor_id.Size = new System.Drawing.Size(297, 39);
            this.tb_doctor_id.TabIndex = 27;
            this.tb_doctor_id.Text = "DOC0891622";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(315, 608);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(132, 32);
            this.label10.TabIndex = 26;
            this.label10.Text = "Doctor\'s ID";
            // 
            // tb_doctor_officeId
            // 
            this.tb_doctor_officeId.Location = new System.Drawing.Point(12, 653);
            this.tb_doctor_officeId.Name = "tb_doctor_officeId";
            this.tb_doctor_officeId.Size = new System.Drawing.Size(297, 39);
            this.tb_doctor_officeId.TabIndex = 25;
            this.tb_doctor_officeId.Text = "OF0987152467";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(12, 608);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 32);
            this.label11.TabIndex = 24;
            this.label11.Text = "Office ID";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label12.Location = new System.Drawing.Point(12, 576);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 32);
            this.label12.TabIndex = 23;
            this.label12.Text = "Doctor";
            // 
            // num_total
            // 
            this.num_total.DecimalPlaces = 2;
            this.num_total.Location = new System.Drawing.Point(997, 166);
            this.num_total.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.num_total.Name = "num_total";
            this.num_total.Size = new System.Drawing.Size(297, 39);
            this.num_total.TabIndex = 28;
            this.num_total.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.num_total.Value = new decimal(new int[] {
            9853,
            0,
            0,
            65536});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(997, 121);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 32);
            this.label13.TabIndex = 29;
            this.label13.Text = "Total";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Location = new System.Drawing.Point(1300, 121);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(147, 32);
            this.label14.TabIndex = 31;
            this.label14.Text = "Co-payment";
            // 
            // num_copayment
            // 
            this.num_copayment.DecimalPlaces = 2;
            this.num_copayment.Location = new System.Drawing.Point(1300, 166);
            this.num_copayment.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.num_copayment.Name = "num_copayment";
            this.num_copayment.Size = new System.Drawing.Size(297, 39);
            this.num_copayment.TabIndex = 30;
            this.num_copayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.num_copayment.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // grid_items
            // 
            this.grid_items.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid_items.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_items.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Product,
            this.Factor,
            this.Fee});
            this.grid_items.Location = new System.Drawing.Point(694, 277);
            this.grid_items.Name = "grid_items";
            this.grid_items.RowHeadersWidth = 82;
            this.grid_items.RowTemplate.Height = 41;
            this.grid_items.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grid_items.Size = new System.Drawing.Size(903, 415);
            this.grid_items.TabIndex = 32;
            // 
            // Product
            // 
            this.Product.HeaderText = "Product";
            this.Product.MinimumWidth = 10;
            this.Product.Name = "Product";
            // 
            // Factor
            // 
            this.Factor.HeaderText = "Factor";
            this.Factor.MinimumWidth = 10;
            this.Factor.Name = "Factor";
            // 
            // Fee
            // 
            this.Fee.HeaderText = "Fee";
            this.Fee.MinimumWidth = 10;
            this.Fee.Name = "Fee";
            // 
            // btn_create
            // 
            this.btn_create.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_create.Location = new System.Drawing.Point(12, 875);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(600, 110);
            this.btn_create.TabIndex = 33;
            this.btn_create.Text = "Sign + send";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // tb_prescription_id
            // 
            this.tb_prescription_id.Location = new System.Drawing.Point(694, 166);
            this.tb_prescription_id.Name = "tb_prescription_id";
            this.tb_prescription_id.Size = new System.Drawing.Size(297, 39);
            this.tb_prescription_id.TabIndex = 35;
            this.tb_prescription_id.Text = "T123456";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Location = new System.Drawing.Point(694, 121);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(257, 32);
            this.label15.TabIndex = 34;
            this.label15.Text = "T-Prescription Number";
            // 
            // TPrescriptionInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1841, 997);
            this.Controls.Add(this.tb_prescription_id);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.btn_create);
            this.Controls.Add(this.grid_items);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.num_copayment);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.num_total);
            this.Controls.Add(this.tb_doctor_id);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tb_doctor_officeId);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tb_patient_insuranceStatus);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tb_patient_insuranceId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb_patient_dateOfBirth);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tb_patient_lastname);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_patient_firstname);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_insurance_id);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_insurance_name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_offlabel);
            this.Controls.Add(this.cb_inlabel);
            this.Controls.Add(this.cb_autIdem);
            this.Controls.Add(this.cb_noctu);
            this.Controls.Add(this.cb_chargefree);
            this.Controls.Add(this.cb_Chargeable);
            this.Name = "TPrescriptionInputForm";
            this.Text = "T-Prescription Form";
            ((System.ComponentModel.ISupportInitialize)(this.num_total)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_copayment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_items)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox cb_Chargeable;
        private CheckBox cb_chargefree;
        private CheckBox cb_noctu;
        private CheckBox cb_autIdem;
        private CheckBox cb_inlabel;
        private CheckBox cb_offlabel;
        private Label label1;
        private TextBox tb_insurance_name;
        private Label label2;
        private TextBox tb_insurance_id;
        private Label label3;
        private TextBox tb_patient_lastname;
        private Label label4;
        private TextBox tb_patient_firstname;
        private Label label5;
        private TextBox tb_patient_insuranceId;
        private Label label6;
        private TextBox tb_patient_dateOfBirth;
        private Label label7;
        private TextBox tb_patient_insuranceStatus;
        private Label label8;
        private Label label9;
        private TextBox tb_doctor_id;
        private Label label10;
        private TextBox tb_doctor_officeId;
        private Label label11;
        private Label label12;
        private NumericUpDown num_total;
        private Label label13;
        private Label label14;
        private NumericUpDown num_copayment;
        private DataGridView grid_items;
        private DataGridViewTextBoxColumn Product;
        private DataGridViewTextBoxColumn Factor;
        private DataGridViewTextBoxColumn Fee;
        private Button btn_create;
        private TextBox tb_prescription_id;
        private Label label15;
    }
}