
using System;




public class Employee
{
    public int Empid = 101;
    public int salary = 20000;
}
public class Developer : Employee
{
    public int bonus = 2000;


}
class abc
{
    static void Main(string[] args)
    {
        Developer d = new Developer();




        Console.Write("empid,salary and bonus is " + d.Empid + d.salary + d.bonus);




    }
}