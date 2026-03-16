using System;
using System.Collections.Generic;
using System.Linq;

class Student4
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CourseId { get; set; }
}

class Course
{
    public int Id { get; set; }
    public string CourseName { get; set; }
}

class JoiningTwoColl
{
    static void Main()
    {
        // Sample data: List of students
        List<Student4> students = new List<Student4>
        {
            new Student4 { Id = 1, Name = "Amit", CourseId = 1 },
            new Student4 { Id = 2, Name = "Riya", CourseId = 2 },
            new Student4 { Id = 3, Name = "Raj", CourseId = 1 },
            new Student4 { Id = 4, Name = "Neha", CourseId = 3 },
            new Student4 { Id = 5, Name = "Sita", CourseId = 2 }
        };

        // Sample data: List of courses
        List<Course> courses = new List<Course>
        {
            new Course { Id = 1, CourseName = "Mathematics" },
            new Course { Id = 2, CourseName = "Physics" },
            new Course { Id = 3, CourseName = "Biology" }
        };

        // Joining students and courses on CourseId
        var joinedData = from student in students
                         join course in courses on student.CourseId equals course.Id
                         select new
                         {
                             StudentName = student.Name,
                             CourseName = course.CourseName
                         };

        // Displaying the joined results
        Console.WriteLine("Students and their Courses:");
        foreach (var item in joinedData)
        {
            Console.WriteLine($"Student: {item.StudentName}, Course: {item.CourseName}");
        }
    }
}

