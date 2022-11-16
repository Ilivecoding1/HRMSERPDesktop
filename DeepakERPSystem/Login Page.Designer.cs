namespace DeepakERPSystem
{
    partial class Login_Page
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login_Page));
            this.cboFinYear = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboCompany = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.cboUsertype = new System.Windows.Forms.ComboBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboBackyear = new System.Windows.Forms.ComboBox();
            this.lblpcusername = new System.Windows.Forms.Label();
            this.lblfullpcname = new System.Windows.Forms.Label();
            this.lblpcipaddress = new System.Windows.Forms.Label();
            this.lblshowusername = new System.Windows.Forms.Label();
            this.lblshowpcname = new System.Windows.Forms.Label();
            this.lblshowipaddress = new System.Windows.Forms.Label();
            this.forgetUname = new System.Windows.Forms.LinkLabel();
            this.forgotPassword = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblAttemptcount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblUnlockUname = new System.Windows.Forms.LinkLabel();
            this.btnExitApp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboFinYear
            // 
            this.cboFinYear.AllowDrop = true;
            this.cboFinYear.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.cboFinYear.DisplayMember = "0";
            this.cboFinYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFinYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFinYear.FormattingEnabled = true;
            this.cboFinYear.Location = new System.Drawing.Point(171, 73);
            this.cboFinYear.Name = "cboFinYear";
            this.cboFinYear.Size = new System.Drawing.Size(260, 24);
            this.cboFinYear.TabIndex = 21;
            this.cboFinYear.ValueMember = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 16);
            this.label5.TabIndex = 20;
            this.label5.Text = "Accounts Year :";
            // 
            // cboCompany
            // 
            this.cboCompany.AllowDrop = true;
            this.cboCompany.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.cboCompany.DisplayMember = "0";
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(171, 162);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(260, 24);
            this.cboCompany.TabIndex = 19;
            this.cboCompany.ValueMember = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = "Select Company :";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLogin.BackgroundImage")));
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogin.Location = new System.Drawing.Point(171, 192);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(114, 38);
            this.btnLogin.TabIndex = 17;
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // cboUsertype
            // 
            this.cboUsertype.AllowDrop = true;
            this.cboUsertype.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.cboUsertype.DisplayMember = "0";
            this.cboUsertype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsertype.Enabled = false;
            this.cboUsertype.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboUsertype.ForeColor = System.Drawing.Color.Maroon;
            this.cboUsertype.FormattingEnabled = true;
            this.cboUsertype.Location = new System.Drawing.Point(171, 40);
            this.cboUsertype.Name = "cboUsertype";
            this.cboUsertype.Size = new System.Drawing.Size(260, 24);
            this.cboUsertype.TabIndex = 16;
            this.cboUsertype.ValueMember = "0";
            this.cboUsertype.SelectedIndexChanged += new System.EventHandler(this.cboUsertype_SelectedIndexChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(171, 134);
            this.txtPassword.MaxLength = 32;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(260, 22);
            this.txtPassword.TabIndex = 15;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(171, 104);
            this.txtUsername.MaxLength = 32;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(260, 22);
            this.txtUsername.TabIndex = 14;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Usertype :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Enter Password :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Enter Username :";
            // 
            // cboBackyear
            // 
            this.cboBackyear.AllowDrop = true;
            this.cboBackyear.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.cboBackyear.DisplayMember = "0";
            this.cboBackyear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBackyear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBackyear.FormattingEnabled = true;
            this.cboBackyear.Location = new System.Drawing.Point(443, 12);
            this.cboBackyear.Name = "cboBackyear";
            this.cboBackyear.Size = new System.Drawing.Size(156, 24);
            this.cboBackyear.TabIndex = 22;
            this.cboBackyear.ValueMember = "0";
            this.cboBackyear.Visible = false;
            // 
            // lblpcusername
            // 
            this.lblpcusername.AutoSize = true;
            this.lblpcusername.BackColor = System.Drawing.Color.Transparent;
            this.lblpcusername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpcusername.Location = new System.Drawing.Point(272, 268);
            this.lblpcusername.Name = "lblpcusername";
            this.lblpcusername.Size = new System.Drawing.Size(69, 13);
            this.lblpcusername.TabIndex = 23;
            this.lblpcusername.Text = "User Name";
            // 
            // lblfullpcname
            // 
            this.lblfullpcname.AutoSize = true;
            this.lblfullpcname.BackColor = System.Drawing.Color.Transparent;
            this.lblfullpcname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfullpcname.Location = new System.Drawing.Point(18, 268);
            this.lblfullpcname.Name = "lblfullpcname";
            this.lblfullpcname.Size = new System.Drawing.Size(96, 13);
            this.lblfullpcname.TabIndex = 24;
            this.lblfullpcname.Text = "Computer Name";
            // 
            // lblpcipaddress
            // 
            this.lblpcipaddress.AutoSize = true;
            this.lblpcipaddress.BackColor = System.Drawing.Color.Transparent;
            this.lblpcipaddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpcipaddress.Location = new System.Drawing.Point(504, 268);
            this.lblpcipaddress.Name = "lblpcipaddress";
            this.lblpcipaddress.Size = new System.Drawing.Size(68, 13);
            this.lblpcipaddress.TabIndex = 25;
            this.lblpcipaddress.Text = "IP Address";
            // 
            // lblshowusername
            // 
            this.lblshowusername.AutoSize = true;
            this.lblshowusername.BackColor = System.Drawing.Color.Transparent;
            this.lblshowusername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblshowusername.ForeColor = System.Drawing.Color.Maroon;
            this.lblshowusername.Location = new System.Drawing.Point(272, 290);
            this.lblshowusername.Name = "lblshowusername";
            this.lblshowusername.Size = new System.Drawing.Size(11, 13);
            this.lblshowusername.TabIndex = 26;
            this.lblshowusername.Text = ":";
            // 
            // lblshowpcname
            // 
            this.lblshowpcname.AutoSize = true;
            this.lblshowpcname.BackColor = System.Drawing.Color.Transparent;
            this.lblshowpcname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblshowpcname.ForeColor = System.Drawing.Color.Maroon;
            this.lblshowpcname.Location = new System.Drawing.Point(17, 290);
            this.lblshowpcname.Name = "lblshowpcname";
            this.lblshowpcname.Size = new System.Drawing.Size(11, 13);
            this.lblshowpcname.TabIndex = 27;
            this.lblshowpcname.Text = ":";
            // 
            // lblshowipaddress
            // 
            this.lblshowipaddress.AutoSize = true;
            this.lblshowipaddress.BackColor = System.Drawing.Color.Transparent;
            this.lblshowipaddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblshowipaddress.ForeColor = System.Drawing.Color.Maroon;
            this.lblshowipaddress.Location = new System.Drawing.Point(505, 290);
            this.lblshowipaddress.Name = "lblshowipaddress";
            this.lblshowipaddress.Size = new System.Drawing.Size(11, 13);
            this.lblshowipaddress.TabIndex = 28;
            this.lblshowipaddress.Text = ":";
            // 
            // forgetUname
            // 
            this.forgetUname.AutoSize = true;
            this.forgetUname.BackColor = System.Drawing.Color.Transparent;
            this.forgetUname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.forgetUname.Location = new System.Drawing.Point(117, 239);
            this.forgetUname.Name = "forgetUname";
            this.forgetUname.Size = new System.Drawing.Size(110, 15);
            this.forgetUname.TabIndex = 29;
            this.forgetUname.TabStop = true;
            this.forgetUname.Text = "Forgot Username?";
            this.forgetUname.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.forgetUname_LinkClicked);
            // 
            // forgotPassword
            // 
            this.forgotPassword.AutoSize = true;
            this.forgotPassword.BackColor = System.Drawing.Color.Transparent;
            this.forgotPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.forgotPassword.Location = new System.Drawing.Point(249, 239);
            this.forgotPassword.Name = "forgotPassword";
            this.forgotPassword.Size = new System.Drawing.Size(106, 15);
            this.forgotPassword.TabIndex = 30;
            this.forgotPassword.TabStop = true;
            this.forgotPassword.Text = "Forgot Password?";
            this.forgotPassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.forgotPassword_LinkClicked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Maroon;
            this.label6.Location = new System.Drawing.Point(232, 241);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "|";
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.Location = new System.Drawing.Point(507, 94);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(86, 36);
            this.btnExit.TabIndex = 32;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblAttemptcount
            // 
            this.lblAttemptcount.AutoSize = true;
            this.lblAttemptcount.BackColor = System.Drawing.Color.Transparent;
            this.lblAttemptcount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAttemptcount.ForeColor = System.Drawing.Color.Maroon;
            this.lblAttemptcount.Location = new System.Drawing.Point(272, 17);
            this.lblAttemptcount.Name = "lblAttemptcount";
            this.lblAttemptcount.Size = new System.Drawing.Size(11, 13);
            this.lblAttemptcount.TabIndex = 33;
            this.lblAttemptcount.Text = ".";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(358, 241);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "|";
            // 
            // lblUnlockUname
            // 
            this.lblUnlockUname.AutoSize = true;
            this.lblUnlockUname.BackColor = System.Drawing.Color.Transparent;
            this.lblUnlockUname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnlockUname.Location = new System.Drawing.Point(373, 239);
            this.lblUnlockUname.Name = "lblUnlockUname";
            this.lblUnlockUname.Size = new System.Drawing.Size(132, 15);
            this.lblUnlockUname.TabIndex = 34;
            this.lblUnlockUname.TabStop = true;
            this.lblUnlockUname.Text = "Unlock Username / Id?";
            this.lblUnlockUname.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblUnlockUname_LinkClicked);
            // 
            // btnExitApp
            // 
            this.btnExitApp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExitApp.BackgroundImage")));
            this.btnExitApp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExitApp.Location = new System.Drawing.Point(317, 192);
            this.btnExitApp.Name = "btnExitApp";
            this.btnExitApp.Size = new System.Drawing.Size(114, 38);
            this.btnExitApp.TabIndex = 36;
            this.btnExitApp.UseVisualStyleBackColor = true;
            this.btnExitApp.Click += new System.EventHandler(this.btnExitApp_Click);
            // 
            // Login_Page
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(611, 353);
            this.Controls.Add(this.btnExitApp);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblUnlockUname);
            this.Controls.Add(this.lblAttemptcount);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.forgotPassword);
            this.Controls.Add(this.forgetUname);
            this.Controls.Add(this.lblshowipaddress);
            this.Controls.Add(this.lblshowpcname);
            this.Controls.Add(this.lblshowusername);
            this.Controls.Add(this.lblpcipaddress);
            this.Controls.Add(this.lblfullpcname);
            this.Controls.Add(this.lblpcusername);
            this.Controls.Add(this.cboBackyear);
            this.Controls.Add(this.cboFinYear);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboCompany);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.cboUsertype);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login_Page";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login Page | MMC Soft Systems | HRMS | Version 20.0.1 R2";
            this.Load += new System.EventHandler(this.Login_Page_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboFinYear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboCompany;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.ComboBox cboUsertype;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboBackyear;
        private System.Windows.Forms.Label lblpcusername;
        private System.Windows.Forms.Label lblfullpcname;
        private System.Windows.Forms.Label lblpcipaddress;
        private System.Windows.Forms.Label lblshowusername;
        private System.Windows.Forms.Label lblshowpcname;
        private System.Windows.Forms.Label lblshowipaddress;
        private System.Windows.Forms.LinkLabel forgetUname;
        private System.Windows.Forms.LinkLabel forgotPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblAttemptcount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel lblUnlockUname;
        private System.Windows.Forms.Button btnExitApp;
    }
}