using System;
using System.Collections.Generic;
using System.Linq;

class Student1
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

class OrderingRes
{
    static void Main()
    {
        // Sample data: List of students
        List<Student1> students = new List<Student1>
        {
            new Student1 { Id = 1, Name = "Amit", Age = 20 },
            new Student1 { Id = 2, Name = "Riya", Age = 22 },
            new Student1 { Id = 3, Name = "Raj", Age = 21 },
            new Student1 { Id = 4, Name = "Neha", Age = 19 }
        };

        // Ordering students by Age in ascending order
        var orderedStudents = students.OrderBy(s => s.Age);

        Console.WriteLine("Students ordered by Age (ascending):");
        foreach (var student in orderedStudents)
        {
            Console.WriteLine($"Name: {student.Name}, Age: {student.Age}");
        }

        // Ordering students by Name in descending order
        var orderedByNameDesc = students.OrderByDescending(s => s.Name);

        Console.WriteLine("\nStudents ordered by Name (descending):");
        foreach (var student in orderedByNameDesc)
        {
            Console.WriteLine($"Name: {student.Name}, Age: {student.Age}");
        }
    }
}
