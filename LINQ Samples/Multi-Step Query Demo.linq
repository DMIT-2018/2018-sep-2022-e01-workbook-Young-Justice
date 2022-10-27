<Query Kind="Statements">
  <Connection>
    <ID>e7f33a1e-c12a-4b5f-b72d-2eee25c40ca3</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>WC320-08\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

// Problem

// One needs to have processed information from a collection
// to use against the same collection

// Solution to this type of problem is to use multiple queries
// the early query(ies) will produce the needed information/criteria
// to execute against the same collection in later query(ies)
// Basically we need to do some pre-processing.

// Query one will generate data/information that will be used in the
// next query (query two)



// Display the employees that have the most customers to support.
// Display the employee name and number of customers that employee supports.

// NOT Wanted: List of all employees sorted by number of customers supported.

// One could create a list of all employees, with the customer support count, ordered
// descending by support count. But, this is NOT what is requested.

// What information do I need?
// 	a) I need to know the maximum number of customers that any particular employee is supporting.
//	b) I need to take that piece of data and compare to all employees.

//	a) Get a list of employees and the count of the customers each employee supports.
//	b) From that list, I can obtain the largest number
//	c) Using the number, review all the employees and their counts, reporting ONLY the busiest
//	   employees.

var preProcessEmployeeList = Employees
								.Select(x => new 
									{
										Name = x.FirstName + x.LastName,
										CustomerCount = x.SupportRepCustomers.Count()
									})
								//.Dump()
								;

//var highCount = preProcessEmployeeList
//					.Max(x => x.CustomerCount)
//					//.Dump()
//					;

//var busyEmployees = preProcessEmployeeList
//						.Where(x => x.CustomerCount == highCount)
//						.Dump();
						
var busyEmployees = preProcessEmployeeList
						.Where(x => x.CustomerCount == 
									preProcessEmployeeList.Max(x => x.CustomerCount))
						.Dump();