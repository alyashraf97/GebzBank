using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Npgsql;

namespace GebzBank
{
    internal static class Program
    {
        public static IConfiguration Configuration { get; private set; }
        public const string connectionString = "Host=localhost; Port=5432; Database=GebzBank; Username=postgres; Password=P@ssw0rd;";
        //public NpgsqlConnection dbConnection = new NpgsqlConnection(connectionString);

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            //Configuration
            DatabaseHelper.Initialize();

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

            DatabaseHelper.Dispose();
            /*
            try
            {
                using (NpgsqlConnection globalDatabaseConnection = new NpgsqlConnection(connectionString))
                {
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            */
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            
        }
    }
}