using System;
using System.Collections.Generic;
using System.Linq;

class Students
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

class SpecificProp
{
    static void Main()
    {
        // Sample data: List of students
        List<Students> students = new List<Students>
        {
            new Students { Id = 1, Name = "Amit", Age = 20 },
            new Students { Id = 2, Name = "Riya", Age = 22 },
            new Students { Id = 3, Name = "Raj", Age = 21 }
        };

        // Selecting specific properties (Name and Age) using LINQ
        var selectedProperties = students.Select(s => new { s.Name, s.Age });

        // Displaying the selected properties
        foreach (var student in selectedProperties)
        {
            Console.WriteLine($"Name: {student.Name}, Age: {student.Age}");
        }
    }
}



