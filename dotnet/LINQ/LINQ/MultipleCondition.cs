using System;
using System.Collections.Generic;
using System.Linq;

class Student2
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Course { get; set; }
}

class MultipleCondition
{
    static void Main()
    {
        // Sample data: List of students
        List<Student2> students = new List<Student2>
        {
            new Student2 { Id = 1, Name = "Amit", Age = 20, Course = "Mathematics" },
            new Student2 { Id = 2, Name = "Riya", Age = 22, Course = "Physics" },
            new Student2 { Id = 3, Name = "Raj", Age = 21, Course = "Mathematics" },
            new Student2 { Id = 4, Name = "Neha", Age = 19, Course = "Biology" },
            new Student2 { Id = 5, Name = "Sita", Age = 23, Course = "Physics" }
        };

        // Filtering students who are older than 20 and enrolled in Mathematics
        var filteredStudents = students.Where(s => s.Age > 20 && s.Course == "Mathematics");

        Console.WriteLine("Students older than 20 and enrolled in Mathematics:");
        foreach (var student in filteredStudents)
        {
            Console.WriteLine($"Name: {student.Name}, Age: {student.Age}, Course: {student.Course}");
        }
    }
}
