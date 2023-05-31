// See https://aka.ms/new-console-template for more information
using Chinook.Models;
using Chinook.Repositories;

Console.WriteLine("Hello, World!");

static void ReadAllCustomers(ICustomerRepository repository)
{ 
    ReadCustomers(repository.GetAllCustomers());
}

static void ReadOneCustomer(ICustomerRepository repository) 
{
    ReadCustomer(repository.GetCustomer("2"));
}

static void AddRecord (ICustomerRepository repository) 
{
    Customer customer = new Customer();
}

static void UpdateRecord (ICustomerRepository repository) 
{
    Customer customer = new Customer();
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