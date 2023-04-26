using Npgsql;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic.Logging;

namespace GebzBank
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // Get the email and password from the text boxes
            var email = textEmail.Text;
            var password = maskedTextBoxPassword.Text;

            // Validate the email and password
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || User.IsValidEmail(email) == false)
            {
                // Show a message box if the email or password is empty
                MessageBox.Show("Please enter a valid email and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var login = User.Login(email, password);
            // Verify the password using the DatabaseConnection class
            //var result = User.VerifyPassword(email, password);

            // Show a message box depending on the result
            if (login.status)
            {
                var user = login.user;
                textFirstName.Text = user.FirstName;
                textLastName.Text = user.LastName;
                textEmailAddress.Text = user.Email;
                textAccountNumber.Text = Convert.ToString(user.AccountNumber);
                textBalance.Text = Convert.ToString(user.Balance);

                textEmail.Clear();
                maskedTextBoxPassword.Clear();


                // Show a message box if the password is correct and close the login form
                MessageBox.Show("Login successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //this.Close();
            }
            else
            {
                // Show a message box if the password is incorrect and clear the text boxes
                MessageBox.Show(login.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textEmail.Clear();
                maskedTextBoxPassword.Clear();
            }

            /*
            string loginQuery = "SELECT users.firstName, users.lastName, users.email, users.accountNumber, users.balance FROM users WHERE users.email = @name";
            DatabaseHelper.Execute(loginQuery,
                CommandType.Text, cmd => cmd.ExecuteReader(),
                new NpgsqlParameter("@name", "John"));
            */
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            registrationForm.Show();
        }

        private void buttonDeposit_Click(object sender, EventArgs e)
        {
            try
            {
                int _accountNumber = Convert.ToInt32(textAccountNumber.Text);

                if (_accountNumber != 0)
                {
                    DepositForm depositForm = new DepositForm(_accountNumber);
                    depositForm.Show();
                }
                else
                {
                    MessageBox.Show("Invalid account number, contact Administrator!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Must Login First.." + Ex.Message , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}