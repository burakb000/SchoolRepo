using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

public class Student
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string SchoolNumber { get; set; }
    public int Note { get; set; }
}

public class SchoolDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Your database connection string");
    }
}

class Program
{
    static void Main()
    {

        while (true)
        {
            Console.WriteLine("\n");
            Console.WriteLine("1. Öğrenci Ekle");
            Console.WriteLine("2. Öğrenci Güncelle");
            Console.WriteLine("3. Öğrenci Sil");
            Console.WriteLine("4. Öğrenci Listele");
            Console.WriteLine("5. Çıkış");

            Console.Write("Seçiminizi yapın: ");
            var selection = Console.ReadLine();
            Console.Clear();
            switch (selection)
            {
                case "1":
                    AddStudent();
                    break;
                case "2":
                    UpdateStudent();
                    break;
                case "3":
                    DeleteStudent();
                    break;
                case "4":
                    ListStudents();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    break;
            }
        }
    }

    static void AddStudent()
    {
        using (var context = new SchoolDbContext())
        {
            Console.Write("Öğrenci Adı: ");
            string firstName = Console.ReadLine();

            Console.Write("Öğrenci Soyadı: ");
            string lastName = Console.ReadLine();

            Console.Write("Okul Numarası: ");
            string schoolNumber = Console.ReadLine();

            Console.Write("Puan: ");
            int note = int.Parse(Console.ReadLine());

            Student student = new()
            {
                FirstName = firstName,
                LastName = lastName,
                SchoolNumber = schoolNumber,
                Note = note
            };

            context.Students.Add(student);
            context.SaveChanges();


            Console.WriteLine("Öğrenci başarıyla eklendi.");
        }
    }

    static void UpdateStudent()
    {
        using (var context = new SchoolDbContext())
        {
            Console.Write("Güncellenecek Öğrenci ID'si: ");
            int studentId = int.Parse(Console.ReadLine());

            var student = context.Students.Find(studentId);

            if (student == null)
            {
                Console.WriteLine("Öğrenci bulunamadı.");
                return;
            }

            Console.Write("Öğrenci Adı: ");
            student.FirstName = Console.ReadLine();

            Console.Write("Öğrenci Soyadı: ");
            student.LastName = Console.ReadLine();

            Console.Write("Okul Numarası: ");
            student.SchoolNumber = Console.ReadLine();

            Console.Write("Puan: ");
            student.Note = int.Parse(Console.ReadLine());

            context.SaveChanges();


            Console.WriteLine("Öğrenci başarıyla güncellendi.");
        }
    }

    static void DeleteStudent()
    {

        using (var context = new SchoolDbContext())
        {
            Console.Write("Silinecek Öğrenci ID'si: ");
            int studentId = int.Parse(Console.ReadLine());

            var student = context.Students.Find(studentId);

            if (student == null)
            {
                Console.WriteLine("Öğrenci bulunamadı.");
                return;
            }

            context.Students.Remove(student);
            context.SaveChanges();


            Console.WriteLine("Öğrenci başarıyla silindi.");
        }
    }

    static void ListStudents()
    {

        using (var context = new SchoolDbContext())
        {
            var students = context.Students.ToList();

            Console.WriteLine("Öğrenciler:");
            foreach (Student student in students)
            {
                Console.WriteLine($"{student.StudentId} - {student.FirstName} {student.LastName} - {student.SchoolNumber} - Puan: {student.Note}");

            }
        }
    }
}
