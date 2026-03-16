using System;
using System.Collections.Generic;
using System.Linq;

class Student5
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Course { get; set; }
}

class CheckingExist
{
    static void Main()
    {
        // Sample data: List of students
        List<Student5> students = new List<Student5>
        {
            new Student5 { Id = 1, Name = "Amit", Age = 20, Course = "Mathematics" },
            new Student5 { Id = 2, Name = "Riya", Age = 22, Course = "Physics" },
            new Student5 { Id = 3, Name = "Raj", Age = 21, Course = "Mathematics" },
            new Student5 { Id = 4, Name = "Neha", Age = 19, Course = "Biology" },
            new Student5 { Id = 5, Name = "Sita", Age = 23, Course = "Physics" }
        };

        // Check if any student is enrolled in "Mathematics"
        bool hasMathematicsStudent = students.Any(s => s.Course == "Mathematics");
        Console.WriteLine($"Is there any student enrolled in Mathematics? {hasMathematicsStudent}");

        // Check if all students are older than 18
        bool allStudentsOlderThan18 = students.All(s => s.Age > 18);
        Console.WriteLine($"Are all students older than 18? {allStudentsOlderThan18}");
    }

}
