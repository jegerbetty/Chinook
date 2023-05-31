using Chinook.Models;
using Chinook.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> GetAllCustomers(int limit, int offset)
        {
            List<Customer> customerList = new List<Customer>();
            //Write the SQL statement to get all customers
            string sql = "SELECT CustomerID, FirstName, LastName, Country, PostalCode, Phone, Email FROM CUSTOMER";
            try
            {
                //write the statement for connection (to connect)
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionHelper.GetConnectionString())) 
                {
                    sqlConnection.Open(); //Open the connection
                    //Write the command for the sql
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection)) 
                    {
                        //Write the statement to execute query
                        //SQLDataReader
                        using (SqlDataReader sqlDataReader = command.ExecuteReader()) 
                        {
                            while (sqlDataReader.Read()) 
                            {
                                //Create a customer object
                                Customer tempCustomer = new Customer();
                                //Assign value from database to the ID
                                tempCustomer.CustomerId = sqlDataReader.GetInt32(0);
                                tempCustomer.FirstName = sqlDataReader.GetString(1);
                                tempCustomer.LastName = sqlDataReader.GetString(2);
                                tempCustomer.Country = sqlDataReader.GetString(3);
                                tempCustomer.PostalCode = sqlDataReader.GetString(4);
                                tempCustomer.Phone = sqlDataReader.GetString(5);
                                tempCustomer.Email = sqlDataReader.GetString(6);
                                //Add to the collection list
                                customerList.Add(tempCustomer);


                            }
                        }
                    } 
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message, ex);
            }
            return customerList;
        }

        public Customer GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(string name)
        {
            throw new NotImplementedException();
        }
        public bool AddNewCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public bool UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
