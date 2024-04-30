using Ado.Net.Services;

namespace Ado.Net
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student()
            {
                Name = "Eli",
                Surname = "Eliyev",
                Point=90,
            };
          StudentService studentService = new StudentService();
         
            foreach(var item in studentService.GetAll())
            {
                Console.WriteLine(item);
            }
            studentService.Delete(1);

            Student studentToUpdate = new Student
            {
               
                Name = "Veli",
                Surname = "Veliyev",
                Point = 95
            };
            student.Update(studentToUpdate);
        }
    }
}
