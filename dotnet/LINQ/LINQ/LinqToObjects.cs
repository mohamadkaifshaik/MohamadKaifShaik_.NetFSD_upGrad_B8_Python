using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class LinqToObjects
{
    public static void Main()
    {
        List<Student> students = new List<Student> {
            new Student { Name = "Amit", Age = 17 },
            new Student { Name = "Priya", Age = 19 },
            new Student { Name = "Raj", Age = 20 },
            new Student { Name = "Neha", Age = 18 }
        };

        // Query Syntax
        var adultStudentsQuery = from student in students
                                 where student.Age >= 18
                                 select student;

        Console.WriteLine("Adult Students (Query Syntax):");
        foreach (var student in adultStudentsQuery)
        {
            Console.WriteLine(student.Name);
        }

        // Method Syntax
        var adultStudentsMethod = students.Where(student => student.Age >= 18);

        Console.WriteLine("\nAdult Students (Method Syntax):");
        foreach (var student in adultStudentsMethod)
        {
            Console.WriteLine(student.Name);
        }
    }



}
