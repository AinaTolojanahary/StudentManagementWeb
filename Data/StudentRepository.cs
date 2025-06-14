using MySql.Data.MySqlClient;
using StudentManagementWeb.Models;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace StudentManagementWeb.Data
{
    public class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration?.GetConnectionString("MySqlConnection") 
                ?? throw new ArgumentNullException(nameof(configuration), "Connection string 'MySqlConnection' not found in configuration.");
        }

        public void AddStudent(Student student)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO students (FirstName, LastName, Email, BirthDate) VALUES (@FirstName, @LastName, @Email, @BirthDate)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", student.FirstName);
                    command.Parameters.AddWithValue("@LastName", student.LastName);
                    command.Parameters.AddWithValue("@Email", student.Email);
                    command.Parameters.AddWithValue("@BirthDate", student.BirthDate ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Student> GetAllStudents()
        {
            var students = new List<Student>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM students";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                Id = reader.GetInt32("Id"),
                                FirstName = reader.GetString("FirstName"),
                                LastName = reader.GetString("LastName"),
                                Email = reader.GetString("Email"),
                                BirthDate = reader.IsDBNull(reader.GetOrdinal("BirthDate")) ? null : reader.GetDateTime("BirthDate")
                            });
                        }
                    }
                }
            }
            return students;
        }

        public Student? GetStudentById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM students WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Student
                            {
                                Id = reader.GetInt32("Id"),
                                FirstName = reader.GetString("FirstName"),
                                LastName = reader.GetString("LastName"),
                                Email = reader.GetString("Email"),
                                BirthDate = reader.IsDBNull(reader.GetOrdinal("BirthDate")) ? null : reader.GetDateTime("BirthDate")
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public void UpdateStudent(Student student)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE students SET FirstName = @FirstName, LastName = @LastName, Email = @Email, BirthDate = @BirthDate WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", student.Id);
                    command.Parameters.AddWithValue("@FirstName", student.FirstName);
                    command.Parameters.AddWithValue("@LastName", student.LastName);
                    command.Parameters.AddWithValue("@Email", student.Email);
                    command.Parameters.AddWithValue("@BirthDate", student.BirthDate ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStudent(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM students WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}