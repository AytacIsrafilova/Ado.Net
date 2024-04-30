using Ado.Net.DataBase;
using Ado.Net.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.Net.Services
{
    internal class StudentService
    {
        AppDbContext dbContext;
        public StudentService()
        {
            this.dbContext = new AppDbContext();
        }

        public void Delete(int id)
        {
            string command = $"delete from Students where Id={id}";
            dbContext.NonQuery(command);
        }
        public void Create(Student student)
        {
            string createCommand = $"insert into Students values('{student.Name}','{student.Surname}','{student.Point}')";
            int result = dbContext.NonQuery(createCommand);

        }

        public List<Student> GetAll(Func<Student, bool> predicate = null)
        {
            string query = "select *from Students";
            DataTable table = dbContext.Query(query);
            List<Student> list = new List<Student>();
            foreach (DataRow item in table.Rows)
            {
                var student = new Student();

                list.Add(new Student()
                {
                    Id = int.Parse(item["Id"].ToString()),
                    Name = item["Name"].ToString(),
                    Surname = item["Surname"].ToString(),
                    Point = double.Parse(item["Point"].ToString())

                });

                if (predicate != null || predicate(student))
                {
                    list.Add(student);
                }
            }
            return list;
        }


        public void Update(Student student,string connectionString)
        {
            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Students SET Name = @Name, Surname = @Surname, Point = @Point WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@Surname", student.Surname);
                    command.Parameters.AddWithValue("@Point", student.Point);
                    command.Parameters.AddWithValue("@Id", student.Id);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        Console.WriteLine("No student with that ID exists.");
                    }
                    else
                    {
                        Console.WriteLine("Student updated successfully.");
                    }
                }

            }
        }
    }
}
