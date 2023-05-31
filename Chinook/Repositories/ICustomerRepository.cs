using Chinook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Repositories
{
    public interface ICustomerRepository
    {
        public List<Customer> GetAllCustomers(); //returns all customers. display: Id, firstname, lastname, country, postal cost, phone number, email
        public Customer GetCustomerByID(int id); //Returns one customer by Id. get one customer by Id. display: Id, firstname, lastname, country, postal cost, phone number, email
        public Customer GetCustomerByName(string partialname); //get one customer by name (HINT: LIKE keyword can help for partial matches). display: Id, firstname, lastname, country, postal cost, phone number, email
        public List<Customer> GetCustomerPage(int offset, int limit); //return a page of customers from the database. This should take in limit and offset as parameters. display: Id, firstname, lastname, country, postal cost, phone number, email
        public bool AddNewCustomer(Customer customer); //add a new customer to the database. add: Id, firstname, lastname, country, postal cost, phone number, email
        public bool UpdateCustomer(Customer customer); //update an existing customer
        public List<CustomerCountry> CustomersPerCountry(); //make a seperate class, CustomerCountry (add to Models folder): return the number of customers in each country, ordered descending (high to low)
        //public int MyProperty { get; set; } //make a seperate class, CustomerSpender (add to Models folder): customers who are the highest spenders (total in invoice table is the largest), ordered descending
        //public int MyProperty { get; set; } //make a seperate class, CustomerGenre (add to Models folder): for a given customer, thier most popular genre (in the case of a tie, display both). most popular = genre that corresponds to the most tracks from invoices associated to that customer
    }
}
