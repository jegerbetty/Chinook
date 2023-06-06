using Chinook.Models;
using Chinook.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chinook.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> GetAllCustomers() //Read all the customers in the database, this should display their: Id, first name, last name, country, postal code, phone number and email.
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
            }
            return customer;
        }
        public bool AddNewCustomer(Customer customer)
        {
            bool success = false;
            string sql = "INSERT INTO CUSTOMER(CustomerID, FirstName, LastName, Country, PostalCode, Phone, Email)" +
                         "VALUES(@CustomerID, @FirstName, @LastName, @Country, @PostalCode, @Phone, @Email";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionHelper.GetConnectionString()))
                {
                    sqlConnection.Open();

                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customer.CustomerId);
                        command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        command.Parameters.AddWithValue("@LastName", customer.LastName);
                        command.Parameters.AddWithValue("@Country", customer.Country);
                        command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                        command.Parameters.AddWithValue("@Phone", customer.Phone);
                        command.Parameters.AddWithValue("@Email", customer.Email); 

                        success = command.ExecuteNonQuery() > 0 ? true : false; //to execute the transaction for the insert command
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return success;
        }

        public List<Customer> GetCustomerPage(int offset, int limit)
        {
            List<Customer> customerList = new List<Customer>();

            string sql = "SELECT CustomerID, FirstName, LastName, Country, PostalCode, Phone, Email" +
                         "FROM CUSTOMER" +
                         "OFFSET @offset ROWS" + 
                         "FETCH NEXT @limit ROWS ONLY";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionHelper.GetConnectionString()))
                {
                    sqlConnection.Open(); 

                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@offset", offset);
                        command.Parameters.AddWithValue("@limit", limit);
                        
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
                                
                                customerList.Add(tempCustomer);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return customerList;
        }

        public bool UpdateCustomer(Customer customer)
        {
            bool success = false;
            string sql = "UPDATE CUSTOMER SET " +  
                         "FirstName=@FirstName, " + 
                         "LastName=@LastName, " + 
                         "Country=@Country, " + 
                         "PostalCode=@PostalCode, " + 
                         "Phone=@Phone, " + 
                         "Email=@Email" +
                         "WHERE CustomerID=@CustomerID";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionHelper.GetConnectionString()))
                {
                    sqlConnection.Open();

                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customer.CustomerId);
                        command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        command.Parameters.AddWithValue("@LastName", customer.LastName);
                        command.Parameters.AddWithValue("@Country", customer.Country);
                        command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                        command.Parameters.AddWithValue("@Phone", customer.Phone);
                        command.Parameters.AddWithValue("@Email", customer.Email);

                        success = command.ExecuteNonQuery() > 0 ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return success;
        }

        public List<CustomerCountry> CustomersPerCountry() 
        { 
            List<CustomerCountry> customerCountryList = new List<CustomerCountry>();

            string sql = "SELECT COUNT(Country) AS 'Count', Country " +
                         "FROM CUSTOMER " +
                         "GROUP BY COUNTRY " +
                         "ORDER BY 'Count' DESC";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionHelper.GetConnectionString()))
                {
                    sqlConnection.Open(); 
                   
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        using (SqlDataReader sqlDataReader = command.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                CustomerCountry tempCustomerCountry = new CustomerCountry();
                               
                                tempCustomerCountry.Country = sqlDataReader.GetString(0);
                                tempCustomerCountry.Count = sqlDataReader.GetInt32(1);

                                customerCountryList.Add(tempCustomerCountry);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return customerCountryList;
        }
        public List<CustomerSpender> CustomersHighestSpenders() 
        {
            List<CustomerSpender> customerSpenderList = new List<CustomerSpender>();

            string sql = "SELECT CUSTOMER.CustomerId, CUSTOMER.FirstName, CUSTOMER.LastName, INVOICE.Total AS 'TotalSpending' " +
                         "FROM INVOICE " +
                         "INNER JOIN CUSTOMER ON INVOICE.CustomerId = CUSTOMER.CustomerId " +
                         "ORDER BY 'TotalSpending' DESC";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionHelper.GetConnectionString()))
                {
                    sqlConnection.Open();

                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        using (SqlDataReader sqlDataReader = command.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                CustomerSpender tempCustomerSpender = new CustomerSpender();

                                tempCustomerSpender.CustomerId = sqlDataReader.GetInt32(0);
                                tempCustomerSpender.FirstName = sqlDataReader.GetString(1);
                                tempCustomerSpender.LastName = sqlDataReader.GetString(2);
                                tempCustomerSpender.TotalSpending = sqlDataReader.GetDecimal(3);

                                customerSpenderList.Add(tempCustomerSpender);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return customerSpenderList;

        }
        public List<CustomerGenre> GetCustomerGenre(int customerId) 
        {
            List<CustomerGenre> customerGenreList = new List<CustomerGenre>();

            string sql = "WITH"+
                         "Invoice_Details(InvoiceId, CustomerId, Total)" + 
                         "AS" + 
                         "(" +
                         "SELECT TOP 1 InvoiceId, CustomerId, Total" + 
                         "FROM Invoice" + 
                         "WHERE CustomerId = @CustomerID" + 
                         "ORDER BY Total DESC" +
                         ")" +
                         "select TOP 1 Customer.CustomerId, Customer.FirstName, Customer.LastName, Genre.Name" +
                         "FROM InvoiceLine" +
                         "INNER JOIN Invoice_Details ON Invoice_Details.InvoiceId = InvoiceLine.InvoiceId" +
                         "INNER JOIN Customer ON Invoice_Details.CustomerId = Customer.CustomerId" +
                         "INNER JOIN Track ON InvoiceLine.TrackId = Track.TrackId" +
                         "INNER JOIN Genre ON Genre.GenreId = Track.GenreId" +
                         "ORDER BY Track.Bytes DESC";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionHelper.GetConnectionString()))
                {
                    sqlConnection.Open();

                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerId);

                        using (SqlDataReader sqlDataReader = command.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                CustomerGenre tempCustomerGenre = new CustomerGenre();

                                tempCustomerGenre.CustomerId = sqlDataReader.GetInt32(0);
                                tempCustomerGenre.FirstName = sqlDataReader.GetString(1);
                                tempCustomerGenre.LastName = sqlDataReader.GetString(2);
                                tempCustomerGenre.GenreName = sqlDataReader.GetString(3);

                                customerGenreList.Add(tempCustomerGenre);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return customerGenreList;
        }
    }
}
