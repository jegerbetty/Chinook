# Chinook
Second part of assignment 2 of the Back-end Web Development with .NET course I am currently undertaking

## Assignment details
Chinook models the iTunes database of customers purchasing songs. You are to create a C# console application, install the SQL Client library, and create a repository to interact with the database. 

For customers in the database, the following functionality should be catered for:
1. Read all the customers in the database, this should display their: Id, first name, last name, country, postal code,
phone number and email.
2. Read a specific customer from the database (by Id), should display everything listed in the above point.
3. Read a specific customer by name. HINT: LIKE keyword can help for partial matches.
4. Return a page of customers from the database. This should take in limit and offset as parameters and make use
of the SQL limit and offset keywords to get a subset of the customer data. The customer model from above
should be reused.
5. Add a new customer to the database. You also need to add only the fields listed above (our customer object)
6. Update an existing customer.
7. Return the number of customers in each country, ordered descending (high to low). i.e. USA: 13, …
8. Customers who are the highest spenders (total in invoice table is the largest), ordered descending.
9. For a given customer, their most popular genre (in the case of a tie, display both). Most popular in this context
means the genre that corresponds to the most tracks from invoices associated to that customer.

## Tools
I have used the following to make this console application: 
* C#
* .NET6
* Microsoft Visual Studio 2022
* SQLServer
* Chinook database
