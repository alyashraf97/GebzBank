namespace GebzBank
{
    partial class DepositForm
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
            label1 = new Label();
            textDepositAmount = new TextBox();
            label2 = new Label();
            buttonDepositFormDeposit = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 29);
            label1.Name = "label1";
            label1.Size = new Size(94, 15);
            label1.TabIndex = 0;
            label1.Text = "Deposit Amount";
            // 
            // textDepositAmount
            // 
            textDepositAmount.Location = new Point(112, 26);
            textDepositAmount.Name = "textDepositAmount";
            textDepositAmount.Size = new Size(250, 23);
            textDepositAmount.TabIndex = 1;
            textDepositAmount.KeyPress += textDepositAmount_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(368, 29);
            label2.Name = "label2";
            label2.Size = new Size(13, 15);
            label2.TabIndex = 2;
            label2.Text = "$";
            // 
            // buttonDepositFormDeposit
            // 
            buttonDepositFormDeposit.Location = new Point(153, 71);
            buttonDepositFormDeposit.Name = "buttonDepositFormDeposit";
            buttonDepositFormDeposit.Size = new Size(162, 34);
            buttonDepositFormDeposit.TabIndex = 3;
            buttonDepositFormDeposit.Text = "Deposit";
            buttonDepositFormDeposit.UseVisualStyleBackColor = true;
            buttonDepositFormDeposit.Click += buttonDepositFormDeposit_Click;
            // 
            // DepositForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(428, 134);
            Controls.Add(buttonDepositFormDeposit);
            Controls.Add(label2);
            Controls.Add(textDepositAmount);
            Controls.Add(label1);
            Name = "DepositForm";
            Text = "Deposit";
            Load += DepositForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textDepositAmount;
        private Label label2;
        private Button buttonDepositFormDeposit;
    }
}