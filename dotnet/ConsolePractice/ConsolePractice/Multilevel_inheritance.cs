using System;
namespace MULTILEVELInheritan
{
    public class SBI
    {
        public float rate_sbi = 6.5f;
        public double calc_Sbi(double amount, int years)
        {
            return amount = ((amount * rate_sbi * years) / 100 + (amount));


        }
    }
    public class HDFC : SBI
    {
        public float rate_HDFC = 8.5f;
        public double calc_HDFC(double amount, int years)
        {
            return amount = ((amount * rate_HDFC * years) / 100 + (amount));
        }
    }
    public class PNB : HDFC
    {
        public float rate_PNB = 5.0f;
        public double calc_PNB(double amount, int years)
        {
            return amount = ((amount * rate_PNB * years) / 100 + (amount));
        }
    }




    class abc
    {
        static void Main(string[] args)
        {
            PNB f = new PNB();
            Console.WriteLine("enter yr amount");
            double amount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("for how many years ");
            int years = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine("your amount accoring to SBI after" + years + "year will be " + f.calc_Sbi(amount, years));
            Console.WriteLine("your amount accoring to HDFC after" + years + "year will be " + f.calc_HDFC(amount, years));


        }
    }
}

