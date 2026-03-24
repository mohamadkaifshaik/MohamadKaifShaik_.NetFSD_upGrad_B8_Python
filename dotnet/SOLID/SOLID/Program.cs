using System;
using System.Collections.Generic;

class Student
{
    public int StudentId;
    public string? StudentName;
    public int Marks;
}

class StudentRepository
{
    private List<Student> list = new List<Student>();

    public void Add(Student s)
    {
        list.Add(s);
    }

    public List<Student> GetAll()
    {
        return list;
    }
}

class ReportGenerator
{
    public void Print(List<Student> list)
    {
        Console.WriteLine("----- Student Report -----");

        for (int i = 0; i < list.Count; i++)
        {
            var s = list[i];

            Console.WriteLine("Id: " + s.StudentId);
            Console.WriteLine("Name: " + s.StudentName);
            Console.WriteLine("Marks: " + s.Marks);

            if (s.Marks >= 50)
                Console.WriteLine("Result: Pass");
            else
                Console.WriteLine("Result: Fail");

            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main()
    {
        StudentRepository repo = new StudentRepository();

        Student s1 = new Student();
        s1.StudentId = 1;
        s1.StudentName = "Kaif";
        s1.Marks = 78;

        Student s2 = new Student();
        s2.StudentId = 2;
        s2.StudentName = "Rahul";
        s2.Marks = 40;

        repo.Add(s1);
        repo.Add(s2);

        ReportGenerator report = new ReportGenerator();
        report.Print(repo.GetAll());
    }
}