using System;

class Employee
{
    public string Name { get; set; }
    public double BaseSalary { get; set; }

    public virtual double CalculateSalary()
    {
        return BaseSalary;
    }
}

class Manager : Employee
{
    public override double CalculateSalary()
    {
        return BaseSalary + (BaseSalary * 0.20);
    }
}

class Developer : Employee
{
    public override double CalculateSalary()
    {
        return BaseSalary + (BaseSalary * 0.10);
    }
}

class Emp
{
    static void Main()
    {
        Employee manager = new Manager();
        manager.Name = "John";
        manager.BaseSalary = 50000;

        Employee developer = new Developer();
        developer.Name = "Alex";
        developer.BaseSalary = 50000;

        Console.WriteLine("Manager Salary = " + manager.CalculateSalary());
        Console.WriteLine("Developer Salary = " + developer.CalculateSalary());
    }
}
