using System;

public interface SBI
{
    void calc_SBI();
}

public interface HDFC
{
    void calc_HDFC();
}

public class PNB : SBI, HDFC
{
    double amount = 1000;
    int years = 2;

    public void calc_SBI()
    {
        double rate = 12;
        double total = (amount * rate * years) / 100 + amount;
        Console.WriteLine("SBI final amount = " + total);
    }

    public void calc_HDFC()
    {
        double rate = 9;
        double total = (amount * rate * years) / 100 + amount;
        Console.WriteLine("HDFC final amount = " + total);
    }

    public void calc_PNB()
    {
        double rate = 10;
        double total = (amount * rate * years) / 100 + amount;
        Console.WriteLine("PNB final amount = " + total);
    }
}

class PPF_Calc
{
    static void Main(string[] args)
    {
        PNB obj = new PNB();

        obj.calc_SBI();
        obj.calc_HDFC();
        obj.calc_PNB();
    }
}
