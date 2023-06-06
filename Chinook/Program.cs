// See https://aka.ms/new-console-template for more information
using Chinook.Models;
using Chinook.Repositories;
using System.Diagnostics.Metrics;
using System.Numerics;

Console.WriteLine("Hello, World!");

static void ReadAllCustomers(ICustomerRepository repository)
{ 
    ReadCustomers(repository.GetAllCustomers());
}

static void ReadOneCustomerByID(ICustomerRepository repository) 
{
    ReadCustomer(repository.GetCustomerByID(2));
}

static void ReadOneCustomerByName(ICustomerRepository repository)
{
    ReadCustomer(repository.GetCustomerByName("Wilson"));
}

static void AddNewRecord (ICustomerRepository repository)
{
    Customer addNewCustomer = new Customer();
    {
        FirstName = "FirstName",
        LastName = "LastName",
        Country = "Country",
        PostalCode = "PostalCode",
        Phone = "Phone",
        Email = "Email"
    };
    if (repository.AddNewCustomer(addNewCustomer)) 
    {
        Console.WriteLine("Successfully inserted record");
        ReadCustomer(repository.GetCustomerByName("LastName"));
    } else 
    {
        Console.WriteLine("Not successful");
    }
}

static void UpdateRecord (ICustomerRepository repository) 
{
    Customer updateCustomer = new Customer();
    {
        FirstName = "FirstName",
        LastName = "LastName",
        Country = "Country",
        PostalCode = "PostalCode",
        Phone = "Phone",
        Email = "Email",
    };
    if (repository.UpdateCustomer(updateCustomer))
    {
        Console.WriteLine("Successfully updated record");
        ReadCustomer(repository.GetCustomerByName("LastName"));
    }
    else
    {
        Console.WriteLine("Not successful");
    }
}

static IEnumerable<CustomerCountry> CustomerCountries(ICustomerRepository repository) 
{
    IEnumerable<CustomerCountry> customerCountries = repository.CustomersPerCountry();
    return customerCountries;
}

static IEnumerable<CustomerSpender> CustomerHighestSpenders(ICustomerRepository repository) 
{
    IEnumerable<CustomerSpender> customerHighestSpenders = repository.CustomersHighestSpenders();
    return customerHighestSpenders;
}

static IEnumerable<CustomerGenre> CustomerPopularGenres(ICustomerRepository repository) 
{
    IEnumerable<CustomerGenre> customerPopularGenres = repository.GetCustomerGenre(5);
    return customerPopularGenres;
}

//Methods for the ReadCustomer methods above
static void ReadCustomers(IEnumerable<Customer> customers) //reads several customers. uses the ReadCustomer method below for reading/printing customers
{
    foreach (Customer customer in customers)
    {
        ReadCustomer(customer);
    }
}

static void ReadCustomer(Customer customer) //Reads one customer
{
    Console.WriteLine($"{customer.CustomerId}, {customer.FirstName}, {customer.LastName}, {customer.Country}, {customer.PostalCode}, {customer.Phone}, {customer.Email}");
}