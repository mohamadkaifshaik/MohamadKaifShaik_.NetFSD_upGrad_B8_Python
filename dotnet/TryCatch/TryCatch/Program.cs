using System;

class Account
{
    double balance;

    public Account(double amount)
    {
        balance = amount;
    }

    public void Withdraw(double amount)
    {
        if (amount <= 99)
        {
            throw new Exception("Withdrawal amount must be greater than 99");
        }

        if (amount > balance)
        {
            throw new Exception("Insufficient funds");
        }

        balance = balance - amount;
        Console.WriteLine("Withdrawal successful");
        Console.WriteLine("Remaining Balance: " + balance);
    }
}

class Program
{
    static void Main()
    {
        Account acc = new Account(5000);

        try
        {
            Console.Write("Enter amount to withdraw: ");
            double amount = Convert.ToDouble(Console.ReadLine());

            acc.Withdraw(amount);
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter numeric value.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Transaction completed.");
        }
    }
}