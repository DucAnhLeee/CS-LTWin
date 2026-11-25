using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    public int ID { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
}

public class Program
{
    public static void Main()
    {
        int studentCount = ReadInt("Nhập số lượng học sinh (ít nhất 5): ", minimum: 5);
        List<Student> students = [];

        for (int i = 0; i < studentCount; i++)
        {
            Console.WriteLine($"\nNhập thông tin học sinh thứ {i + 1}:");
            students.Add(new Student
            {
                ID = ReadInt("Mã số: "),
                Name = ReadName("Tên: "),
                Age = ReadInt("Tuổi: ", minimum: 0)
            });
        }

        while (true)
        {
            Console.WriteLine(
                "\n===== MENU =====\n" +
                "a. In danh sách toàn bộ học sinh\n" +
                "b. Tìm học sinh từ 15 đến 18 tuổi\n" +
                "c. Tìm học sinh có tên bắt đầu bằng chữ A\n" +
                "d. Tính tổng tuổi học sinh\n" +
                "e. Tìm học sinh có tuổi lớn nhất\n" +
                "f. Sắp xếp học sinh theo tuổi tăng dần\n" +
                "0. Thoát");
            Console.Write("Chọn chức năng: ");

            string choice = Console.ReadLine()?.Trim().ToLowerInvariant() ?? "";
            switch (choice)
            {
                case "a":
                    PrintStudents(students);
                    break;
                case "b":
                    PrintStudents(students.Where(student => student.Age >= 15 && student.Age <= 18));
                    break;
                case "c":
                    PrintStudents(students.Where(student =>
                        student.Name.StartsWith("A", StringComparison.OrdinalIgnoreCase)));
                    break;
                case "d":
                    Console.WriteLine($"Tổng tuổi các học sinh: {students.Sum(student => student.Age)}");
                    break;
                case "e":
                    int maxAge = students.Max(student => student.Age);
                    PrintStudents(students.Where(student => student.Age == maxAge));
                    break;
                case "f":
                    PrintStudents(students.OrderBy(student => student.Age));
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                    break;
            }
        }
    }

    private static int ReadInt(string prompt, int minimum = int.MinValue)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value) && value >= minimum)
            {
                return value;
            }

            Console.WriteLine($"Vui lòng nhập số nguyên lớn hơn hoặc bằng {minimum}.");
        }
    }

    private static string ReadName(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? name = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                return name;
            }

            Console.WriteLine("Tên không được để trống.");
        }
    }

    private static void PrintStudents(IEnumerable<Student> items)
    {
        List<Student> result = items.ToList();
        if (result.Count == 0)
        {
            Console.WriteLine("Không tìm thấy học sinh phù hợp.");
            return;
        }

        foreach (Student student in result)
        {
            Console.WriteLine($"ID: {student.ID}, Tên: {student.Name}, Tuổi: {student.Age}");
        }
    }
}
