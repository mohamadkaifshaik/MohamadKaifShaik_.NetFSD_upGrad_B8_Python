using System;

class BankAcc
{
    private double balance;

    public void Deposit(double amount)
    {
        balance = balance + amount;
    }

    public void Withdraw(double amount)
    {
        if (amount <= balance)
        {
            balance = balance - amount;
        }
        else
        {
            Console.WriteLine("Insufficient balance");
        }
    }

    public double GetBalance()
    {
        return balance;
    }
}

class Encap
{
    static void Main()
    {
        BankAcc account = new BankAcc();

        account.Deposit(1000);
        account.Withdraw(300);

        Console.WriteLine("Current Balance = " + account.GetBalance());
    }
}