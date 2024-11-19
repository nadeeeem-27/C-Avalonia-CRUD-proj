using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace EmployeesList.Models
{
    public class EmployeeRepository
    {
        private readonly string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=EmployeeDBS;Trusted_Connection=True;";

        public ObservableCollection<Employee> GetEmployees()
        {
            var employees = new ObservableCollection<Employee>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Employee", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            ID = (int)reader["ID"],
                            Name = reader["Name"].ToString(),
                            Department = reader["Department"].ToString(),
                            DateOfJoining = (DateTime)reader["DateOfJoining"]
                        });
                    }
                }
            }
            return employees;
        }

        public void AddEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Employee (Name, Department, DateOfJoining) VALUES (@Name, @Department, @DateOfJoining)", connection);
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Department", employee.Department);
                command.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
                command.ExecuteNonQuery();
            }
        }

        public void EditEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Employee SET Name = @Name, Department = @Department, DateOfJoining = @DateOfJoining WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", employee.ID);
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Department", employee.Department);
                command.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Employee WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", employeeId);
                command.ExecuteNonQuery();
            }
        }
    }
}