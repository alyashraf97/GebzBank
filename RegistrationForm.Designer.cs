namespace GebzBank
{
    partial class RegistrationForm
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
            textRegistrationEmail = new TextBox();
            maskedRegistrationPassword = new MaskedTextBox();
            label1 = new Label();
            buttonRegistrationFormRegister = new Button();
            label2 = new Label();
            textRegistrationFirstName = new TextBox();
            label3 = new Label();
            label4 = new Label();
            textRegistrationLastName = new TextBox();
            SuspendLayout();
            // 
            // textRegistrationEmail
            // 
            textRegistrationEmail.Location = new Point(77, 25);
            textRegistrationEmail.Name = "textRegistrationEmail";
            textRegistrationEmail.Size = new Size(286, 23);
            textRegistrationEmail.TabIndex = 0;
            // 
            // maskedRegistrationPassword
            // 
            maskedRegistrationPassword.Location = new Point(77, 54);
            maskedRegistrationPassword.Name = "maskedRegistrationPassword";
            maskedRegistrationPassword.Size = new Size(286, 23);
            maskedRegistrationPassword.TabIndex = 1;
            maskedRegistrationPassword.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 28);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 2;
            label1.Text = "Email";
            // 
            // buttonRegistrationFormRegister
            // 
            buttonRegistrationFormRegister.Location = new Point(120, 141);
            buttonRegistrationFormRegister.Name = "buttonRegistrationFormRegister";
            buttonRegistrationFormRegister.Size = new Size(204, 42);
            buttonRegistrationFormRegister.TabIndex = 4;
            buttonRegistrationFormRegister.Text = "Register";
            buttonRegistrationFormRegister.UseVisualStyleBackColor = true;
            buttonRegistrationFormRegister.Click += buttonRegistrationFormRegister_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 57);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 5;
            label2.Text = "Password";
            // 
            // textRegistrationFirstName
            // 
            textRegistrationFirstName.Location = new Point(77, 83);
            textRegistrationFirstName.Name = "textRegistrationFirstName";
            textRegistrationFirstName.Size = new Size(286, 23);
            textRegistrationFirstName.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 86);
            label3.Name = "label3";
            label3.Size = new Size(64, 15);
            label3.TabIndex = 6;
            label3.Text = "First Name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 115);
            label4.Name = "label4";
            label4.Size = new Size(63, 15);
            label4.TabIndex = 8;
            label4.Text = "Last Name";
            // 
            // textRegistrationLastName
            // 
            textRegistrationLastName.Location = new Point(77, 112);
            textRegistrationLastName.Name = "textRegistrationLastName";
            textRegistrationLastName.Size = new Size(286, 23);
            textRegistrationLastName.TabIndex = 3;
            // 
            // RegistrationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(427, 215);
            Controls.Add(label4);
            Controls.Add(textRegistrationLastName);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textRegistrationFirstName);
            Controls.Add(buttonRegistrationFormRegister);
            Controls.Add(label1);
            Controls.Add(maskedRegistrationPassword);
            Controls.Add(textRegistrationEmail);
            Name = "RegistrationForm";
            Text = "Register";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textRegistrationEmail;
        private MaskedTextBox maskedRegistrationPassword;
        private Label label1;
        private Button buttonRegistrationFormRegister;
        private Label label2;
        private TextBox textRegistrationFirstName;
        private Label label3;
        private Label label4;
        private TextBox textRegistrationLastName;
    }
}