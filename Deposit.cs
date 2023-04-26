using System;

// A class that represents a deposit transaction
public class Deposit
{
    // Properties of the deposit
    public int AccountNumber { get; private set; } // The account number of the beneficiary
    public decimal Amount { get; private set; } // The amount of money deposited
    public DateTime Date { get; private set; } // The date of the deposit
    public string Uuid { get; private set; } // The unique identifier of the deposit
    public bool DepositStatus { get; private set; } // The status of the deposit
    public string DepositStatusMessage { get; private set; } // The message of the deposit
    public decimal OldBalance { get; private set; } // The old balance of the account
    public decimal NewBalance { get; private set; } // The new balance of the account

    // Constructor that initializes the properties with parameters, generates a new uuid and executes the deposit
    public Deposit(int accountNumber, decimal amount, DateTime date)
    {
        AccountNumber = accountNumber;
        Amount = amount;
        Date = date;
        Uuid = Guid.NewGuid().ToString();
        DepositIntoAccount();
    }

    // A method that deposits money into the account and sets the properties with the transaction details
    private void DepositIntoAccount()
    {
        // Call the function with the properties of this deposit object as arguments and get the result tuple
        var result = DatabaseHelper.DepositIntoAccount(AccountNumber, Amount, Date);
        // Set the properties with the values from the result tuple
        DepositStatus = result.depositStatus;
        DepositStatusMessage = result.depositStatusMessage;
        OldBalance = result.oldBalance;
        NewBalance = result.newBalance;
    }
}