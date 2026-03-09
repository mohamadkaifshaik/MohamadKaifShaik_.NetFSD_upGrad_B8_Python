 class Details
{
     string address;
     int age;
     int marks;
     string name;
     string phone;

    public void Input()
    {
        Console.Write("Enter Name: ");
        name = Console.ReadLine();

        Console.Write("Enter Age: ");
        age = Convert.ToInt32(Console.ReadLine());

        if (age < 18)
        {
            Console.WriteLine("age must be 18+.");
        }

        Console.Write("Enter Address: ");
        address = Console.ReadLine();

        Console.Write("Enter Phone: ");
        phone = Console.ReadLine();

        Console.Write("Enter Marks: ");
        marks = Convert.ToInt32(Console.ReadLine());
    }

    public void Output()
    {
        Console.WriteLine("\nStudent Details");
        Console.WriteLine("Name: " + name);
        Console.WriteLine("Age: " + age);
        Console.WriteLine("Marks: " + marks);
        Console.WriteLine("Address: " + address);
        Console.WriteLine("Phone: " + phone);
    }
}

 class Result
{
    private static void Main()
    {
        Details d1 = new Details();
        d1.Input();
        d1.Output();
    }
}