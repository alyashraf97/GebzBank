using System;
using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;


public class DatabaseHelper
{
    // A private static field that holds the instance of the connection
    private static NpgsqlConnection _instance;
    public const string connectionString = "Host=localhost; Port=5432; Database=GebzBank; Username=postgres; Password=P@ssw0rd;";

    // A private constructor that initializes the connection with the connection string
    private DatabaseHelper(string connectionString)
    {
        _instance = new NpgsqlConnection(connectionString);
        _instance.Open();
    }


    //IConfiguration configuration

    public static void Initialize()
    {
        // Use a lock to ensure thread-safety
        lock (typeof(DatabaseHelper))
        {
            // If the instance is null, create a new one with the connection string from the appsettings.json file
            if (_instance == null)
            {

                //var connectionString = configuration.GetConnectionString("DefaultConnection");
                new DatabaseHelper(connectionString);
            }
        }
    }

    public static NpgsqlConnection GetInstance()
    {
        // Use a lock to ensure thread-safety
        lock (typeof(DatabaseHelper))
        {
            // If the instance is null, create a new one with the connection string from the appsettings.json file
            if (_instance == null)
            {
                try
                {
                    new DatabaseHelper(connectionString);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }
            // Check if the connection is open
            if (_instance.State != ConnectionState.Open)
            {
                // Open the connection
                _instance.Open();
            }
            // Return the instance
            return _instance;
        }
    }


    public static T Execute<T>(string sql, CommandType commandType, Func<NpgsqlCommand, T> function,
        params NpgsqlParameter[] parameters)
    {
        // Use a lock to ensure thread-safety
        lock (typeof(DatabaseHelper))
        {
            // Create a command using the sql string, the command type and the parameters
            using var cmd = new NpgsqlCommand(sql, GetInstance());
            cmd.CommandType = commandType;
            if (parameters.Length > 0)
            {
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            // Return the result of invoking the function delegate on the command
            return function(cmd);
        }
    }

    public static DataTable Query(string sql, params NpgsqlParameter[] parameters)
    {

        // Use a lock to ensure thread-safety
        lock (typeof(DatabaseHelper))
        {
            // Create a command using the sql string and the parameters
            using var cmd = new NpgsqlCommand(sql, GetInstance());
            if (parameters.Length > 0)
            {
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            // Create a data adapter using the command
            using var adapter = new NpgsqlDataAdapter(cmd);

            // Create a data table and fill it with the results of the query
            var table = new DataTable();
            adapter.Fill(table);

            // Return the data table
            return table;
        }
    }

    public static int Insert(string sql, CommandType commandType, params NpgsqlParameter[] parameters)
    {
        // Use a lock to ensure thread-safety
        lock (typeof(DatabaseHelper))
        {
            // Create a command using the sql string, the command type and the parameters
            using var cmd = new NpgsqlCommand(sql, GetInstance());
            cmd.CommandType = commandType;
            if (parameters.Length > 0)
            {
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            // Execute the command and return the number of affected rows
            return cmd.ExecuteNonQuery();
        }
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

    public static bool VerifyPassword(string email, string password)
    {
        // Use a lock to ensure thread-safety
        lock (typeof(DatabaseHelper))
        {
            // Query the database for the user with the given email and get their password hash and salt
            var sql = "SELECT passwordHash, passwordSalt FROM users WHERE email = @email";
            using var cmd = new NpgsqlCommand(sql, GetInstance());
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
    }

    // A method to dispose the connection when the application ends
    public static void Dispose()
    {
        // Use a lock to ensure thread-safety
        lock (typeof(DatabaseHelper))
        {
            // If the instance is not null, close and dispose it
            if (_instance != null)
            {
                _instance.Close();
                _instance.Dispose();
            }
        }
    }

    public static (bool state, string message, string email, int account_number, decimal balance, string uuid, string first_name, string last_name) QueryUserByAccountNumber(int accountNumber)
    {
        string sql = "SELECT email, accountnumber, balance, guid, firstname, lastname FROM users WHERE accountnumber = @account_number";
        using var db = new NpgsqlConnection(connectionString);
        using var cmd = new NpgsqlCommand(sql, db);

        cmd.Parameters.AddWithValue("account_number", accountNumber);

        using var reader = cmd.ExecuteReader();

        if (!reader.Read())
        {
            return (false, "User doesn't Exist", "", 0, 0, "", "", "");
        }
        else
        {
            return (true, "User Exists", reader.GetString(0), reader.GetInt32(1), reader.GetDecimal(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
        }


    }

    public static (bool state, string message, string email, int account_number, decimal balance, string uuid, string first_name, string last_name) QueryUserByEmail(string email)
    {
        try
        {
            string sql = "SELECT email, accountnumber, balance, guid, firstname, lastname FROM users WHERE email = @user_email";
            using var db = new NpgsqlConnection(connectionString);
            using var cmd = new NpgsqlCommand(sql, db);

            cmd.Parameters.AddWithValue("user_email", email);

            using var reader = cmd.ExecuteReader();

            if (!reader.Read())
            {
                return (false, "User doesn't Exist", "", 0, 0, "", "", "");
            }
            else
            {
                return (true, "User Exists", reader.GetString(0), reader.GetInt32(1), reader.GetDecimal(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
            }
        }
        catch (Exception ex)
        {
            return (false, ex.Message, "", 0, 0, "", "", "");
        }
    }

    public static (bool status, string message) InsertNewDepositRecord(string id, DateTime date, decimal amount, string accountGuid)
    {
        try
        {
            string sql = "INSERT INTO public.deposits (id, date, amount, account_uuid) VALUES (@deposit_id, @deposit_date, @deposit_amount, @deposit_account_uuid";
            using var db = new NpgsqlConnection(connectionString);
            using var cmd = new NpgsqlCommand(sql, db);

            cmd.Parameters.AddWithValue("deposit_id", id);
            cmd.Parameters.AddWithValue("deposit_date", date);
            cmd.Parameters.AddWithValue("deposit_amount", amount);
            cmd.Parameters.AddWithValue("deposit_account_uuid", accountGuid);

            int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
            {
                return (true, "Deposit Record Inserted");
            }
            else
            {
                return (false, "Deposit Failed");
            }


        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }

    }

    public (bool depositStatus, string depositStatusMessage, int accountNumber, string transactionUuid, DateTime depositDate, decimal oldBalance, decimal newBalance, decimal depositAmount) DepositIntoAccount(int accountNumber, decimal amount, DateTime date)
    {
        // Create a tuple to store the return values
        var result = (depositStatus: false, depositStatusMessage: "", accountNumber: accountNumber, transactionUuid: "", depositDate: date, oldBalance: 0m, newBalance: 0m, depositAmount: amount);
        using (var connection = new NpgsqlConnection(connectionString))
        {
            // Open the connection to the database
            connection.Open();

            // Create a SQL transaction object using the connection object
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string checkAccountSql = "SELECT balance, guid FROM users WHERE accountnumber = @accountnumber";

                    // Create a SQL command to update the balance of the account
                    string updateBalanceSql = "UPDATE users SET balance = balance + @amount WHERE accountnumber = @accountnumber";

                    // Create a SQL command to insert a new record into the deposit table
                    string insertDepositSql = "INSERT INTO deposit (id, date, amount, account_uuid) VALUES (DEFAULT, @date, @amount, @account_uuid) RETURNING id";

                    // Create a SQL command object using the check account command and the connection object
                    using (var checkAccountCommand = new NpgsqlCommand(checkAccountSql, connection))
                    {
                        // Add a parameter with the account number
                        checkAccountCommand.Parameters.AddWithValue("accountnumber", accountNumber);
                        // Execute the command and get the result
                        using (var reader = checkAccountCommand.ExecuteReader())
                        {
                            // Check if there is a row in the result
                            if (reader.Read())
                            {
                                // Get the balance and guid of the account from the result
                                var balance = reader.GetDecimal(0);
                                var guid = reader.GetString(1);
                                // Close the reader
                                reader.Close();
                                // Set the old balance in the result tuple
                                result.oldBalance = balance;
                                // Create a SQL command object using the update balance command and the connection object
                                using (var updateBalanceCommand = new NpgsqlCommand(updateBalanceSql, connection))
                                {
                                    // Add parameters with the amount and account number
                                    updateBalanceCommand.Parameters.AddWithValue("amount", amount);
                                    updateBalanceCommand.Parameters.AddWithValue("accountnumber", accountNumber);
                                    // Execute the command and get the number of rows affected
                                    int rowsAffected = updateBalanceCommand.ExecuteNonQuery();
                                    // Check if one row was updated successfully
                                    if (rowsAffected == 1)
                                    {
                                        // Set the new balance in the result tuple
                                        result.newBalance = balance + amount;
                                        // Create a SQL command object using the insert deposit command and the connection object
                                        using (var insertDepositCommand = new NpgsqlCommand(insertDepositSql, connection))
                                        {
                                            // Add parameters with the date, amount and account guid
                                            insertDepositCommand.Parameters.AddWithValue("date", date);
                                            insertDepositCommand.Parameters.AddWithValue("amount", amount);
                                            insertDepositCommand.Parameters.AddWithValue("account_uuid", guid);
                                            // Execute the command and get the id of the inserted record
                                            var id = insertDepositCommand.ExecuteScalar();
                                            // Set the transaction uuid in the result tuple as id-guid
                                            result.transactionUuid = $"{id}-{guid}";
                                            // Set the deposit status and message in the result tuple as true and success
                                            result.depositStatus = true;
                                            result.depositStatusMessage = "Deposit completed successfully.";
                                        }
                                    }
                                    else
                                    {
                                        // Set the deposit status and message in the result tuple as false and error
                                        result.depositStatus = false;
                                        result.depositStatusMessage = "Error updating balance.";
                                    }
                                }
                            }
                            else
                            {
                                // Set the deposit status and message in the result tuple as false and error
                                result.depositStatus = false;
                                result.depositStatusMessage = "Account not found.";
                            }
                        }
                    }
                    // Commit the transaction to save changes to the database
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Rollback the transaction to undo any changes to the database
                    transaction.Rollback();
                    // Set the deposit status and message in the result tuple as false and exception
                    result.depositStatus = false;
                    result.depositStatusMessage = $"Exception: {ex.Message}";
                }
                // Close the connection to the database
                connection.Close();
            }

            // Return the result tuple
            return result;
        }

    }

    public (bool withdrawStatus, string withdrawStatusMessage, int accountNumber, string transactionUuid, DateTime withdrawDate, decimal oldBalance, decimal newBalance, decimal withdrawAmount) WithdrawFromAccount(int accountNumber, decimal amount, DateTime date)
    {

        // Create a SQL command to check if the account exists and get its balance and guid
        string checkAccountSql = "SELECT balance, guid FROM users WHERE accountnumber = @accountnumber";

        // Create a SQL command to update the balance of the account
        string updateBalanceSql = "UPDATE users SET balance = balance - @amount WHERE accountnumber = @accountnumber";

        // Create a SQL command to insert a new record into the withdraw table
        string insertWithdrawSql = "INSERT INTO withdraw (id, date, amount, account_uuid) VALUES (DEFAULT, @date, @amount, @account_uuid) RETURNING id";

        // Create a tuple to store the return values
        var result = (withdrawStatus: false, withdrawStatusMessage: "", accountNumber: accountNumber, transactionUuid: "", withdrawDate: date, oldBalance: 0m, newBalance: 0m, withdrawAmount: amount);

        // Create a SQL connection object using the connection string
        using (var connection = new NpgsqlConnection(connectionString))
        {
            // Open the connection to the database
            connection.Open();

            // Create a SQL transaction object using the connection object
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Create a SQL command object using the check account command and the connection object
                    using (var checkAccountCommand = new NpgsqlCommand(checkAccountSql, connection))
                    {
                        // Add a parameter with the account number
                        checkAccountCommand.Parameters.AddWithValue("accountnumber", accountNumber);
                        // Execute the command and get the result
                        using (var reader = checkAccountCommand.ExecuteReader())
                        {
                            // Check if there is a row in the result
                            if (reader.Read())
                            {
                                // Get the balance and guid of the account from the result
                                var balance = reader.GetDecimal(0);
                                var guid = reader.GetString(1);
                                // Close the reader
                                reader.Close();
                                // Set the old balance in the result tuple
                                result.oldBalance = balance;
                                // Check if the balance is enough for the withdrawal
                                if (balance >= amount)
                                {
                                    // Create a SQL command object using the update balance command and the connection object
                                    using (var updateBalanceCommand = new NpgsqlCommand(updateBalanceSql, connection))
                                    {
                                        // Add parameters with the amount and account number
                                        updateBalanceCommand.Parameters.AddWithValue("amount", amount);
                                        updateBalanceCommand.Parameters.AddWithValue("accountnumber", accountNumber);
                                        // Execute the command and get the number of rows affected
                                        int rowsAffected = updateBalanceCommand.ExecuteNonQuery();
                                        // Check if one row was updated successfully
                                        if (rowsAffected == 1)
                                        {
                                            // Set the new balance in the result tuple
                                            result.newBalance = balance - amount;
                                            // Create a SQL command object using the insert withdraw command and the connection object
                                            using (var insertWithdrawCommand = new NpgsqlCommand(insertWithdrawSql, connection))
                                            {
                                                // Add parameters with the date, amount and account guid
                                                insertWithdrawCommand.Parameters.AddWithValue("date", date);
                                                insertWithdrawCommand.Parameters.AddWithValue("amount", amount);
                                                insertWithdrawCommand.Parameters.AddWithValue("account_uuid", guid);
                                                // Execute the command and get the id of the inserted record
                                                var id = insertWithdrawCommand.ExecuteScalar();
                                                // Set the transaction uuid in the result tuple as id-guid
                                                result.transactionUuid = $"{id}-{guid}";
                                                // Set the withdraw status and message in the result tuple as true and success
                                                result.withdrawStatus = true;
                                                result.withdrawStatusMessage = "Withdrawal completed successfully.";
                                            }
                                        }
                                        else
                                        {
                                            // Set the withdraw status and message in the result tuple as false and error
                                            result.withdrawStatus = false;
                                            result.withdrawStatusMessage = "Error updating balance.";
                                        }
                                    }
                                }
                                else
                                {
                                    // Set the withdraw status and message in the result tuple as false and error
                                    result.withdrawStatus = false;
                                    result.withdrawStatusMessage = "Insufficient balance.";
                                }
                            }
                            else
                            {
                                // Set the withdraw status and message in the result tuple as false and error
                                result.withdrawStatus = false;
                                result.withdrawStatusMessage = "Account not found.";
                            }
                        }
                    }
                    // Commit the transaction to save changes to the database
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Rollback the transaction to undo any changes to the database
                    transaction.Rollback();
                    // Set the withdraw status and message in the result tuple as false and exception
                    result.withdrawStatus = false;
                    result.withdrawStatusMessage = $"Exception: {ex.Message}";
                }
            }

            // Close the connection to the database
            connection.Close();
        }

        // Return the result tuple
        return result;
    }
}


    /*
    public static (bool depositStatus, string depositStatusMessage, int accountNumber, string transactionUuid, DateTime depositDate, decimal oldBalance, decimal newBalance, decimal depositAmount) DepositIntoAccount(int accountNumber, decimal amount, DateTime date)
    {
        // Create a connection string to connect to the database
        string connectionString = connectionString;

        // Create a SQL command to check if the account exists and get its balance and guid
        string checkAccountCommand = "SELECT balance, guid FROM users WHERE accountnumber = @accountnumber";

        // Create a SQL command to update the balance of the account
        string updateBalanceCommand = "UPDATE users SET balance = balance + @amount WHERE accountnumber = @accountnumber";

        // Create a SQL command to insert a new record into the deposit table
        string insertDepositCommand = "INSERT INTO deposit (id, date, amount, account_uuid) VALUES (DEFAULT, @date, @amount, @account_uuid) RETURNING id";

        // Create a tuple to store the return values
        var result = (depositStatus: false, depositStatusMessage: "", accountNumber: accountNumber, transactionUuid: "", depositDate: date, oldBalance: 0m, newBalance: 0m, depositAmount: amount);

        // Create a SQL connection object using the connection string
        using (var connection = new NpgsqlConnection(connectionString))
        {
            // Open the connection to the database
            connection.Open();

            // Create a SQL transaction object using the connection object
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Create a SQL command object using the check account command and the connection object
                    using (var checkAccountCommand = new NpgsqlCommand(checkAccountCommand, connection))
                    {
                        // Add a parameter with the account number
                        checkAccountCommand.Parameters.AddWithValue("accountnumber", accountNumber);
                        // Execute the command and get the result
                        using (var reader = checkAccountCommand.ExecuteReader())
                        {
                            // Check if there is a row in the result
                            if (reader.Read())
                            {
                                // Get the balance and guid of the account from the result
                                var balance = reader.GetDecimal(0);
                                var guid = reader.GetString(1);
                                // Close the reader
                                reader.Close();
                                // Set the old balance in the result tuple
                                result.oldBalance = balance;
                                // Create a SQL command object using the update balance command and the connection object
                                using (var updateBalanceCommand = new NpgsqlCommand(updateBalanceCommand, connection))
                                {
                                    // Add parameters with the amount and account number
                                    updateBalanceCommand.Parameters.AddWithValue("amount", amount);
                                    updateBalanceCommand.Parameters.AddWithValue("accountnumber", accountNumber);
                                    // Execute the command and get the number of rows affected
                                    int rowsAffected = updateBalanceCommand.ExecuteNonQuery();
                                    // Check if one row was updated successfully
                                    if (rowsAffected == 1)
                                    {
                                        // Set the new balance in the result tuple
                                        result.newBalance = balance + amount;
                                        // Create a SQL command object using the insert deposit command and the connection object
                                        using (var insertDepositCommand = new NpgsqlCommand(insertDepositCommand, connection))
                                        {
                                            // Add parameters with the date, amount and account guid
                                            insertDepositCommand.Parameters.AddWithValue("date", date);
                                            insertDepositCommand.Parameters.AddWithValue("amount", amount);
                                            insertDepositCommand.Parameters.AddWithValue("account_uuid", guid);
                                            // Execute the command and get the id of the inserted record
                                            var id = insertDepositCommand.ExecuteScalar();
                                            // Set the transaction uuid in the result tuple as id-guid
                                            result.transactionUuid = $"{id}-{guid}";
                                            // Set the deposit status and message in the result tuple as true and success
                                            result.depositStatus = true;
                                            result.depositStatusMessage = "Deposit completed successfully.";
                                        }
                                    }
                                    else
                                    {
                                        // Set the deposit status and message in the result tuple as false and error
                                        result.depositStatus = false;
                                        result.depositStatusMessage = "Error updating balance.";
                                    }
                                }
                            }
                            else
                            {
                                // Set the deposit status and message in the result tuple as false and error
                                result.depositStatus = false;
                                result.depositStatusMessage = "Account not found.";
                            }
                        }
                    }
                    // Commit the transaction to save changes to the database
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Rollback the transaction to undo any changes to the database
                    transaction.Rollback();
                    // Set the deposit status and message in the result tuple as false and exception
                    result.depositStatus = false;
                    result.depositStatusMessage = $"Exception: {ex.Message}";
                }



            }
    */