using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GebzBank
{
    public partial class DepositForm : Form
    {
        private int accountNumber;
        public DepositForm(int inputAccountNumber)
        {
            accountNumber = inputAccountNumber;
            InitializeComponent();
        }

        private void DepositForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonDepositFormDeposit_Click(object sender, EventArgs e)
        {

        }

        private void textDepositAmount_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Check if the pressed key is not a digit, a decimal point, or a backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
            {
                // Cancel the key press
                e.Handled = true;
            }
            // Check if the pressed key is a decimal point
            else if (e.KeyChar == '.')
            {
                // Check if the textbox already contains a decimal point
                if (textDepositAmount.Text.Contains("."))
                {
                    // Cancel the key press
                    e.Handled = true;
                }
            }
        }
    }
}
