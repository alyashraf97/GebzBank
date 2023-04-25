using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Npgsql;
using System.Security.Cryptography;

namespace GebzBank
{
    public class User
    {
        // properties to store the user information
        //public int Id { get; private set; } // a unique identifier for the user
        public string Email { get; private set; } // the email address of the user
        public string FirstName { get; private set; } // the first name of the user
        public string LastName { get; private set; } // the last name of the user
        public string PasswordHash { get; private set; } // the hashed password of the user
        public string PasswordSalt { get; private set; } // the salt used to hash the password
        public string userGuid { get; private set; } // a globally unique identifier for the user
        public int AccountNumber { get; private set; }
        public decimal Balance { get; private set; } // the balance of the user

        public User()
        {
            Email = "";
            FirstName = "";
            LastName = "";
            PasswordHash = "";
            PasswordSalt = "";
            AccountNumber = 0;
            userGuid = ""; // generate a new guid
            Balance = 0;
        }

        public User(string email, string firstName, string lastName, string passwordHash, string passwordSalt, string guid)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            userGuid = guid;
            Balance = 0; // assign a default value of zero
        }

        public static (bool userExists, string message) UserExistsByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return (false, "No Email given");
            email = email.Trim().ToLower();

            if (!IsValidEmail(email)) return (false, "Invalid Email");

            User user = null;

            var sql = "SELECT guid, email, firstName, lastName, accountNumber FROM users WHERE email = @email";

            var db = DatabaseHelper.GetInstance();
            var cmd = new NpgsqlCommand(sql, db);

            cmd.Parameters.AddWithValue("email", email);

            try
            {
                using var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    return (true, "User Exists");
                }
                else
                {
                    return (false, "User Doesn't Exist");
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public static bool PasswordIsSecure(string password)
        {
            if (password.Length < 8) return false;

            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;
            bool hasSymbol = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpper = true; // set flag to true if uppercase letter found
                else if (char.IsLower(c)) hasLower = true; // set flag to true if lowercase letter found
                else if (char.IsDigit(c)) hasDigit = true; // set flag to true if digit found
                else hasSymbol = true; //
            }

            if (!hasDigit || !hasLower || !hasUpper || !hasSymbol)
            { 
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"+ @"@([a-z0-9]([-a-z0-9]*[a-z0-9])?\.)+[a-z0-9]([-a-z0-9]*[a-z0-9])?$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(email);
        }


        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            /*
            // Query the database for the user with the given email and get their password hash and salt
            var sql = "SELECT passwordHash, passwordSalt FROM users WHERE email = @email";
            using var cmd = new NpgsqlCommand(sql, DatabaseHelper.GetInstance());
            cmd.Parameters.AddWithValue("email", email);
            using var reader = cmd.ExecuteReader();

            // If no user is found, return false
            if (!reader.Read())
            {
                return false;
            }

            // Get the password hash and salt from the reader
            var storedHash = reader.GetString(0);
            var storedSalt = reader.GetString(1);
            */

            // Combine the password and salt and convert them to bytes
            var passwordAndSalt = password + storedSalt;
            var passwordAndSaltBytes = Encoding.UTF8.GetBytes(passwordAndSalt);

            // Compute the hash using SHA512
            using var sha512 = SHA512.Create();
            var hashBytes = sha512.ComputeHash(passwordAndSaltBytes);

            // Convert the hash bytes to a base64 string
            var computedHash = Convert.ToBase64String(hashBytes);

            // Compare the computed hash with the stored hash and return true if they match, false otherwise
            return computedHash == storedHash;
            
        }

        public static (string hash, string salt) HashPassword(string password)
        {
            // Generate a random salt using RandomNumberGenerator
            var saltBytes = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            // Convert the salt bytes to a base64 string
            var salt = Convert.ToBase64String(saltBytes);

            // Combine the password and salt and convert them to bytes
            var passwordAndSalt = password + salt;
            var passwordAndSaltBytes = Encoding.UTF8.GetBytes(passwordAndSalt);

            // Compute the hash using SHA512
            using var sha512 = SHA512.Create();
            var hashBytes = sha512.ComputeHash(passwordAndSaltBytes);

            // Convert the hash bytes to a base64 string
            var hash = Convert.ToBase64String(hashBytes);

            // Return the hash and salt as a tuple
            return (hash, salt);
        }

        public static (bool status, string message) Register(string email, string password, string firstName, string lastName)
        {
            if (!IsValidEmail(email))
            {
                return (false, "Invalid email!");
            }
            
            if (!PasswordIsSecure(password))
            {
                return (false, "Registration failed, Weak password!");
            }

            var hashAndSalt = HashPassword(password);

            Guid guid = Guid.NewGuid();

            User user = new User(email, firstName, lastName, hashAndSalt.hash, hashAndSalt.salt, guid.ToString());

            try
            {
                var sql = "INSERT INTO public.users (email, firstname, lastname, passwordhash, passwordsalt, guid) VALUES (@email, @first_name, @last_name, @password_hash, @password_salt, @guid)";

                var db = DatabaseHelper.GetInstance();
                //using var db = new NpgsqlConnection(Program.connectionString);
                //db.Open();
                var cmd = new NpgsqlCommand(sql, db);

                cmd.Parameters.AddWithValue("email", user.Email);
                cmd.Parameters.AddWithValue("first_name", user.FirstName);
                cmd.Parameters.AddWithValue("last_name", user.LastName);
                cmd.Parameters.AddWithValue("password_hash", user.PasswordHash);
                cmd.Parameters.AddWithValue("password_salt", user.PasswordSalt);
                cmd.Parameters.AddWithValue("guid", user.userGuid);

                int rows = cmd.ExecuteNonQuery();

                if (rows>0)
                {
                    return (true, "Registration Successful");
                }
                else
                {
                    return (false, "Registration Failed");
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }            
        }
        public static (bool status, string message, User? user) Login(string email, string password)
        {
            User user = new User();

            var sql = "SELECT email, passwordhash, passwordsalt, accountnumber, guid, firstname, lastname, balance FROM users WHERE email = @email";

            var db = DatabaseHelper.GetInstance();
            using var cmd = new NpgsqlCommand(sql, db);
            cmd.Parameters.AddWithValue("email", email);

            try
            {                
                using var reader = cmd.ExecuteReader();

                if (reader.Read() && VerifyPassword(password, reader.GetString(1), reader.GetString(2)))
                {
                    user.Email = email;
                    user.AccountNumber = reader.GetInt32(3);
                    user.FirstName = reader.GetString(5);
                    user.LastName = reader.GetString(6);
                    user.Balance = reader.GetDecimal(7);
                    user.userGuid = reader.GetString(4);

                    return (true, "Login Successful", user);
                }
                else
                {
                    return (false, "Incorrect username or password", null);
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }
    }
}
