using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GebzBank
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void buttonRegistrationFormRegister_Click(object sender, EventArgs e)
        {
            string email = textRegistrationEmail.Text;
            string password = maskedRegistrationPassword.Text;
            string firstName = textRegistrationFirstName.Text;
            string lastName = textRegistrationLastName.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("Please fill all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!User.IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (User.UserExistsByEmail(email).userExists == true)
            {
                MessageBox.Show("Email already registered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!User.PasswordIsSecure(password))
            {
                MessageBox.Show("Email already registered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var status = User.Register(email, password, firstName, lastName);

            if (status.status)
            {
                MessageBox.Show("Registration successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(status.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
