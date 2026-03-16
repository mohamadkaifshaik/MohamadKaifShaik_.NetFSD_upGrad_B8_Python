using System;
using System.Collections.Generic;
using System.Linq;

class Student3
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Course { get; set; }
}

class GroupingData
{
    static void Main()
    {
        // Sample data: List of students
        List<Student3> students = new List<Student3>
        {
            new Student3 { Id = 1, Name = "Amit", Age = 20, Course = "Mathematics" },
            new Student3 { Id = 2, Name = "Riya", Age = 22, Course = "Physics" },
            new Student3 { Id = 3, Name = "Raj", Age = 21, Course = "Mathematics" },
            new Student3 { Id = 4, Name = "Neha", Age = 19, Course = "Biology" },
            new Student3 { Id = 5, Name = "Sita", Age = 23, Course = "Physics" }
        };

        // Grouping students by Course
        var groupedStudents = students.GroupBy(s => s.Course);

        // Displaying grouped results
        foreach (var group in groupedStudents)
        {
            Console.WriteLine($"Course: {group.Key}");
            foreach (var student in group)
            {
                Console.WriteLine($"  Name: {student.Name}, Age: {student.Age}");
            }
        }
    }
}
