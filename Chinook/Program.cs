// See https://aka.ms/new-console-template for more information
using Chinook.Models;
using Chinook.Repositories;
using System.Diagnostics.Metrics;
using System.Numerics;

Console.WriteLine("Hello, World!");

/// <summary>
/// 1. Read all the customers in the database, this should display their: Id, first name, last name, country, postal code, phone number and email.
/// </summary>
static void ReadAllCustomers(ICustomerRepository repository)
{ 
    ReadCustomers(repository.GetAllCustomers());
}

/// <summary>
/// 2. Read a specific customer from the database (by Id), should display everything listed in the above point.
/// </summary>
static void ReadOneCustomerByID(ICustomerRepository repository) 
{
    ReadCustomer(repository.GetCustomerByID(2));
}

/// <summary>
/// 3. Read a specific customer by name. HINT: LIKE keyword can help for partial matches.
/// </summary>
static void ReadOneCustomerByName(ICustomerRepository repository)
{
    ReadCustomer(repository.GetCustomerByName("Wilson"));
}

/// <summary>
/// 4. Return a page of customers from the database. 
/// This should take in limit and offset as parameters and make use of the SQL limit and offset keywords to get a subset of the customer data. 
/// The customer model from above should be reused.
/// </summary>

static IEnumerable<Customer> CustomerPage(ICustomerRepository repository) 
{  
    IEnumerable<Customer> customerPage = repository.GetCustomerPage(3,6);
    return customerPage; 
}

/// <summary>
/// 5. Add a new customer to the database. You also need to add only the fields listed above (our customer object)
/// </summary>
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

/// <summary>
/// 6. Update an existing customer.
/// </summary>
static void UpdateRecord (ICustomerRepository repository) 
{
    Customer updateCustomer = new Customer();
    {
        FirstName = "FirstName",
        LastName = "LastName",
        Country = "Country",
        PostalCode = "PostalCode",
        Phone = "Phone",
        Email = "Email"
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

/// <summary>
/// 7. Return the number of customers in each country, ordered descending (high to low). i.e. USA: 13, … 
/// </summary>
static IEnumerable<CustomerCountry> CustomerCountries(ICustomerRepository repository) 
{
    IEnumerable<CustomerCountry> customerCountries = repository.CustomersPerCountry();
    return customerCountries;
}

/// <summary>
/// 8. Customers who are the highest spenders (total in invoice table is the largest), ordered descending.
/// </summary>
static IEnumerable<CustomerSpender> CustomerHighestSpenders(ICustomerRepository repository) 
{
    IEnumerable<CustomerSpender> customerHighestSpenders = repository.CustomersHighestSpenders();
    return customerHighestSpenders;
}

/// <summary>
/// 9. For a given customer, their most popular genre (in the case of a tie, display both). 
/// Most popular in this context means the genre that corresponds to the most tracks from invoices associated to that customer.
/// </summary>
static IEnumerable<CustomerGenre> CustomerPopularGenres(ICustomerRepository repository) 
{
    IEnumerable<CustomerGenre> customerPopularGenres = repository.GetCustomerGenre(5);
    return customerPopularGenres;
}

//Methods for the ReadCustomer methods above
static void ReadCustomers(IEnumerable<Customer> customers) //Reads several customers. Uses the ReadCustomer method below for reading/printing customers
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