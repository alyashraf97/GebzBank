namespace GebzBank
{
    partial class Form1
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
            textEmail = new TextBox();
            buttonLogin = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textFirstName = new TextBox();
            label4 = new Label();
            textLastName = new TextBox();
            label5 = new Label();
            textEmailAddress = new TextBox();
            panel1 = new Panel();
            panel2 = new Panel();
            maskedTextBoxPassword = new MaskedTextBox();
            buttonRegister = new Button();
            panel3 = new Panel();
            buttonLogout = new Button();
            buttonTransfer = new Button();
            label8 = new Label();
            buttonWithdraw = new Button();
            label7 = new Label();
            textBalance = new TextBox();
            buttonDeposit = new Button();
            label6 = new Label();
            textAccountNumber = new TextBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // textEmail
            // 
            textEmail.Location = new Point(53, 17);
            textEmail.Name = "textEmail";
            textEmail.Size = new Size(250, 23);
            textEmail.TabIndex = 0;
            // 
            // buttonLogin
            // 
            buttonLogin.Location = new Point(548, 17);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(75, 23);
            buttonLogin.TabIndex = 1;
            buttonLogin.Text = "Login";
            buttonLogin.UseVisualStyleBackColor = true;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 20);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 2;
            label1.Text = "Email";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(309, 20);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 4;
            label2.Text = "Password";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 20);
            label3.Name = "label3";
            label3.Size = new Size(64, 15);
            label3.TabIndex = 6;
            label3.Text = "First Name";
            // 
            // textFirstName
            // 
            textFirstName.Location = new Point(94, 17);
            textFirstName.Name = "textFirstName";
            textFirstName.ReadOnly = true;
            textFirstName.Size = new Size(209, 23);
            textFirstName.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(309, 20);
            label4.Name = "label4";
            label4.Size = new Size(63, 15);
            label4.TabIndex = 8;
            label4.Text = "Last Name";
            // 
            // textLastName
            // 
            textLastName.Location = new Point(378, 17);
            textLastName.Name = "textLastName";
            textLastName.ReadOnly = true;
            textLastName.Size = new Size(222, 23);
            textLastName.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(7, 49);
            label5.Name = "label5";
            label5.Size = new Size(81, 15);
            label5.TabIndex = 10;
            label5.Text = "Email Address";
            // 
            // textEmailAddress
            // 
            textEmailAddress.Location = new Point(94, 46);
            textEmailAddress.Name = "textEmailAddress";
            textEmailAddress.ReadOnly = true;
            textEmailAddress.Size = new Size(278, 23);
            textEmailAddress.TabIndex = 9;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(textLastName);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(textFirstName);
            panel1.Controls.Add(textEmailAddress);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label4);
            panel1.Location = new Point(12, 73);
            panel1.Name = "panel1";
            panel1.Size = new Size(713, 89);
            panel1.TabIndex = 11;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(maskedTextBoxPassword);
            panel2.Controls.Add(buttonRegister);
            panel2.Controls.Add(textEmail);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(buttonLogin);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(12, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(713, 55);
            panel2.TabIndex = 12;
            // 
            // maskedTextBoxPassword
            // 
            maskedTextBoxPassword.Location = new Point(369, 17);
            maskedTextBoxPassword.Name = "maskedTextBoxPassword";
            maskedTextBoxPassword.Size = new Size(173, 23);
            maskedTextBoxPassword.TabIndex = 6;
            maskedTextBoxPassword.UseSystemPasswordChar = true;
            // 
            // buttonRegister
            // 
            buttonRegister.Location = new Point(629, 17);
            buttonRegister.Name = "buttonRegister";
            buttonRegister.Size = new Size(75, 23);
            buttonRegister.TabIndex = 5;
            buttonRegister.Text = "Register";
            buttonRegister.UseVisualStyleBackColor = true;
            buttonRegister.Click += buttonRegister_Click;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(buttonLogout);
            panel3.Controls.Add(buttonTransfer);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(buttonWithdraw);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(textBalance);
            panel3.Controls.Add(buttonDeposit);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(textAccountNumber);
            panel3.Location = new Point(12, 168);
            panel3.Name = "panel3";
            panel3.Size = new Size(713, 75);
            panel3.TabIndex = 13;
            // 
            // buttonLogout
            // 
            buttonLogout.Location = new Point(606, 38);
            buttonLogout.Name = "buttonLogout";
            buttonLogout.Size = new Size(75, 23);
            buttonLogout.TabIndex = 18;
            buttonLogout.Text = "Logout";
            buttonLogout.UseVisualStyleBackColor = true;
            // 
            // buttonTransfer
            // 
            buttonTransfer.Location = new Point(606, 9);
            buttonTransfer.Name = "buttonTransfer";
            buttonTransfer.Size = new Size(75, 23);
            buttonTransfer.TabIndex = 17;
            buttonTransfer.Text = "Transfer";
            buttonTransfer.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(456, 41);
            label8.Name = "label8";
            label8.Size = new Size(13, 15);
            label8.TabIndex = 15;
            label8.Text = "$";
            // 
            // buttonWithdraw
            // 
            buttonWithdraw.Location = new Point(525, 37);
            buttonWithdraw.Name = "buttonWithdraw";
            buttonWithdraw.Size = new Size(75, 23);
            buttonWithdraw.TabIndex = 16;
            buttonWithdraw.Text = "Withdraw";
            buttonWithdraw.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(57, 41);
            label7.Name = "label7";
            label7.Size = new Size(48, 15);
            label7.TabIndex = 14;
            label7.Text = "Balance";
            // 
            // textBalance
            // 
            textBalance.Location = new Point(111, 38);
            textBalance.Name = "textBalance";
            textBalance.ReadOnly = true;
            textBalance.Size = new Size(339, 23);
            textBalance.TabIndex = 13;
            // 
            // buttonDeposit
            // 
            buttonDeposit.Location = new Point(525, 9);
            buttonDeposit.Name = "buttonDeposit";
            buttonDeposit.Size = new Size(75, 23);
            buttonDeposit.TabIndex = 5;
            buttonDeposit.Text = "Deposit";
            buttonDeposit.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 12);
            label6.Name = "label6";
            label6.Size = new Size(99, 15);
            label6.TabIndex = 12;
            label6.Text = "Account Number";
            // 
            // textAccountNumber
            // 
            textAccountNumber.Location = new Point(111, 9);
            textAccountNumber.Name = "textAccountNumber";
            textAccountNumber.ReadOnly = true;
            textAccountNumber.Size = new Size(362, 23);
            textAccountNumber.TabIndex = 11;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(737, 258);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Gebz Bank";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox textEmail;
        private Button buttonLogin;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textFirstName;
        private Label label4;
        private TextBox textLastName;
        private Label label5;
        private TextBox textEmailAddress;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Button buttonLogout;
        private Button buttonTransfer;
        private Label label8;
        private Button buttonWithdraw;
        private Label label7;
        private TextBox textBalance;
        private Button buttonDeposit;
        private Label label6;
        private TextBox textAccountNumber;
        private Button buttonRegister;
        private MaskedTextBox maskedTextBoxPassword;
    }
}