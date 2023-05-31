using Chinook.Models;
using Chinook.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> GetAllCustomers(int limit, int offset) //Read all the customers in the database, this should display their: Id, first name, last name, country, postal code, phone number and email.
        {
            List<Customer> customerList = new List<Customer>();
            //Write the SQL statement to get all customers
            string sql = "SELECT CustomerID, FirstName, LastName, Country, PostalCode, Phone, Email" + 
                         "FROM CUSTOMER";
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

        public Customer GetCustomerByID(int id) //Read a specific customer from the database (by Id), should display their: Id, first name, last name, country, postal code, phone number and email.
        {
            Customer customer = new Customer();
            
            string sql = "SELECT CustomerID, FirstName, LastName, Country, PostalCode, Phone, Email" + 
                         "FROM CUSTOMER" + 
                         "WHERE CustomerID=@CustomerID";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionHelper.GetConnectionString()))
                {
                    sqlConnection.Open(); 
                    
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customer.CustomerId); //provide PK value for 1 customer
                        using (SqlDataReader sqlDataReader = command.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                
                                Customer tempCustomer = new Customer();
                                
                                tempCustomer.CustomerId = sqlDataReader.GetInt32(0);
                                tempCustomer.FirstName = sqlDataReader.GetString(1);
                                tempCustomer.LastName = sqlDataReader.GetString(2);
                                tempCustomer.Country = sqlDataReader.GetString(3);
                                tempCustomer.PostalCode = sqlDataReader.GetString(4);
                                tempCustomer.Phone = sqlDataReader.GetString(5);
                                tempCustomer.Email = sqlDataReader.GetString(6);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }
            return customer;
        }

        public Customer GetCustomerByName(string partialname) //Read a specific customer from the database (by name). HINT: LIKE keyword can help for partial matches. Should display their: Id, first name, last name, country, postal code, phone number and email.
        {
            Customer customer = new Customer();

            string sql = "SELECT CustomerID, FirstName, LastName, Country, PostalCode, Phone, Email" + 
                         "FROM CUSTOMER" + 
                         "WHERE FirstName LIKE @partialname" +
                         "OR LastName LIKE @partialname";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionHelper.GetConnectionString()))
                {
                    sqlConnection.Open();

                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@partialname", partialname); //provide partial name value for customer
                        using (SqlDataReader sqlDataReader = command.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {

                                Customer tempCustomer = new Customer();

                                tempCustomer.CustomerId = sqlDataReader.GetInt32(0);
                                tempCustomer.FirstName = sqlDataReader.GetString(1);
                                tempCustomer.LastName = sqlDataReader.GetString(2);
                                tempCustomer.Country = sqlDataReader.GetString(3);
                                tempCustomer.PostalCode = sqlDataReader.GetString(4);
                                tempCustomer.Phone = sqlDataReader.GetString(5);
                                tempCustomer.Email = sqlDataReader.GetString(6);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }
            return customer;
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
