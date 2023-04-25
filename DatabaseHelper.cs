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
}