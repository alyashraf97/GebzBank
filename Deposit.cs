using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace GebzBank
{
    internal class Deposit
    {
        public Guid DepositID { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public decimal TransactionAmount { get; private set; }
        public int AccountNumber { get; private set; }
        public decimal InitialBalance { get; set; }
        public decimal FinalBalance { get; set; }
        public bool DepositStatus { get; private set; }

        private Deposit(Guid id, DateTime date, decimal amount, int accountNumber, decimal initBalance, decimal finalBalance, bool status)
        {
            DepositID = id;
            TransactionDate = date;
            TransactionAmount = amount;
            AccountNumber = accountNumber;
            InitialBalance = initBalance;
            FinalBalance = finalBalance;
            DepositStatus = status;
        }

        public static (bool status, string message, Deposit deposit) NewDeposit(string accountUuid, DateTime date, decimal amount)
        {

            string sql = "INSERT INTO deposits (id, date, amount, account_uuid) VALUES (@id, @date, @amount, @account_uuid)";
            var db = DatabaseHelper.GetInstance();
            using var cmd = new NpgsqlCommand(sql, db);
            cmd.Parameters.AddWithValue("id", Guid.NewGuid().ToString());
            cmd.Parameters.AddWithValue("date", date);
            cmd.Parameters.AddWithValue("amount", amount);
            cmd.Parameters.AddWithValue("account_uuid", accountUuid);

        }
    }
}
