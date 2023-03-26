using System;
using System.Collections.Generic;
using System.Linq;

public class Student    // класс Студент
{
    private string firstName;
    private string lastName;
    private int age;
    private double gpa;

    public string FirstName
    {
        set
        {
            bool isOk = false;
            while (!isOk)
            {
                try
                {
                    if (value.Length == 0)
                        throw new Exception();
                    else isOk = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Wrong first name!");
                    value = Console.ReadLine();
                }
                firstName = value;
            }
        }
        get { return firstName; }
    }

    public string LastName
    {
        set
        {
            bool isOk = false;
            while (!isOk)
            {
                try
                {
                    if (value.Length == 0)
                        throw new Exception();
                    else isOk = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Wrong last name!");
                    value = Console.ReadLine();
                }
                lastName = value;
            }
        }
        get { return lastName; }
    }

    public int Age
    {
        set
        {
            bool isOk = false;
            while (!isOk)
            {
                try
                {
                    if (value <= 17 || value > 25)
                        throw new Exception();
                    else isOk = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Wrong age!");
                    value = Int32.Parse(Console.ReadLine());
                }
                this.age = value;
            }
        }
        get { return age; }
    }

    public double GPA
    {
        set
        {
            bool isOk = false;
            while (!isOk)
            {
                try
                {
                    if (value <= 0 || value > 12)
                        throw new Exception();
                    else isOk = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Wrong GPA!");
                    value = double.Parse(Console.ReadLine());
                }
                this.gpa = value;
            }
        }
        get { return gpa; }
    }

    public Student()
    {
        lastName = GenerateName();
        firstName = GenerateName();
        age = GenerateAge();
        gpa = GenerateGPA();
    }

    private static readonly Random rand = new Random();
    private static string GenerateName()
    {
        string[] names = { "Alex", "Bob", "Charlie", "David", "Emily", "Frank", "Grace", "Hannah", "Ivy", "Jack" };
        return names[rand.Next(names.Length)];
    }
    private static int GenerateAge()
    {
        return rand.Next(17, 25);
    }
    private static double GenerateGPA()
    {
        return Math.Round(rand.NextDouble() * 2 + 3, 2);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
    public static bool operator ==(Student st1, Student st2)
    {
        if (st1.gpa == st2.gpa) return true;
        else return false;
    }
    public static bool operator !=(Student st1, Student st2)
    {
        return !(st1 == st2);
    }
}

public class Group    // класс Группа студентов
{
    private List<Student> students = new List<Student>();
    private string name;
    private string specialization;
    private int course;

    public Group()
    {
        name = "Group П11";
        specialization = "Computer Science";
        course = 1;
        for (int i = 0; i < 10; i++)
        {
            AddStudent();
        }
    }

    public Group(List<Student> students)
    {
        name = "Group П11";
        specialization = "Computer Science";
        course = 1;
        this.students = students;
    }

    public Group(Group group)
    {
        name = group.name;
        specialization = group.specialization;
        course = group.course;
        students = new List<Student>(group.students);
    }

    public void ShowStudents()
    {
        Console.WriteLine("Group name: " + name);
        Console.WriteLine("Specialization: " + specialization);
        Console.WriteLine("Course: " + course);
        Console.WriteLine("Students:");
        for (int i = 0; i < students.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {students[i].LastName} {students[i].FirstName}");
        }
    }

    public void AddStudent()
    {
        students.Add(new Student());
    }

    public void EditGroup(string name, string specialization, int course)
    {
        //this.name = name;
        bool isOk = false;
        while (!isOk)
        {
            try
            {
                if (name.Length == 0)
                    throw new Exception();
                else isOk = true;
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong group name!");
                name = Console.ReadLine();
            }
            this.name = name;
        }

        //this.specialization = specialization;
        isOk = false;
        while (!isOk)
        {
            try
            {
                if (specialization.Length == 0)
                    throw new Exception();
                else isOk = true;
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong group specialization!");
                specialization = Console.ReadLine();
            }
            this.specialization = specialization;
        }

        //this.course = course;
        isOk = false;
        while (!isOk)
        {
            try
            {
                if (course <= 0 || course > 5)
                    throw new Exception();
                else isOk = true;
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong group course!");
                course = Int32.Parse(Console.ReadLine());
            }
            this.course = course;
        }
    }

    public void EditStudent(int index, string lastName, string firstName, int age, double gpa)
    {
        bool isOk = false;
        while (!isOk)
        {
            try
            {
                if (index < 0 || index > students.Count)
                    throw new Exception();
                else isOk = true;
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong index!");
                index = Int32.Parse(Console.ReadLine());
            }
        }

        students[index].LastName=lastName;
        students[index].FirstName=firstName;
        students[index].Age=age;
        students[index].GPA=gpa;
    }

    public void TransferStudent(int index, Group group)
    {
        group.students.Add(students[index]);
        students.RemoveAt(index);
    }

    public void ExpelFailedStudents()
    {
        for (int i = 0; i < students.Count; i++)
        {
            if (students[i].GPA < 3.0)
            {
                students.RemoveAt(i);
                i--;
            }
        }
    }

    public void ExpelWorstStudent()
    {
        int worstIndex = 0;
        double worstGPA = students[0].GPA;
        for (int i = 0; i < students.Count; i++)
        {
            if (students[i].GPA < worstGPA)
            {
                worstIndex = i;
                worstGPA = students[i].GPA;
            }
        }
        students.RemoveAt(worstIndex);
    }

    public bool checkEqualStudents(int st1, int st2)
    {
        if (students[st1] == students[st2]) return true;
        else return false;
    }

    public static bool operator ==(Group g1, Group g2)
    {
        if (g1.students.Count == g2.students.Count) return true;
        else return false;
    }
    public static bool operator !=(Group g1, Group g2)
    {
        return !(g1 == g2);
    }
}
