<Query Kind="Program">
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

void Main()
{
	// Nested queries
	
	// Sometimes referred to as subqueries.
	// Simply put, it is a query within a query within a query within [...]
	
	// List all sales support employees showing their 
	// full name (last, first), title, and phone.
	// For each employee, show a list of customers they support.
	// Show the customer full name (last, first), city, and state
	
	// Ex.
	// Employee 1, Title, Phone
	// 		Customer 2000, City, State
	// 		Customer 2109, City, State
	// 		Customer 5000, City, State
	// Employee 2, Title, Phone
	// 		Customer 301, City, State
	
	// There appears to be 2 separate lists that need to be 
	// within one final dataset collection.
	// List of employees
	// List of employee customers
	
	// Concern: The lists are intermixed!
	
	// C# POV in a class definition.
	//
	// First: This is a composite class
	// 		The class is describing an employee
	// 		Each instance of the employee will have a list of employee customers
	//
	// 		Ex.
	// 		Class EmployeeList
	// 		fullname (property)
	//		title (property)
	// 		phone (property)
	//		collection of customers (property: List<T>)
	//
	//		Class CustomerList
	// 		fullname (property)
	// 		city (property)
	// 		state (property)
	
	var results = Employees
					.Where(e => e.Title.Contains("Sales Support"))
					.Select(e => new EmployeeItem
					{
						FullName = e.LastName + ", " + e.FirstName,
						Title = e.Title,
						Phone = e.Phone,
						CustomerList = e.SupportRepCustomers
											.Select(c => new CustomerItem
											{
												FullName = c.LastName + ", " + c.FirstName,
												City = c.City,
												State = c.State
											}
											)
					}
					);
	results.Dump();
	
	// List all albums that are from 1990.
	// Display the album title and artist name.
	// For each album, display its tracks.
	
	var albumtracks = Albums
						.Where(x => x.ReleaseYear == 1990)
						.Select(x => new
						{
							Title = x.Title,
							Artist = x.Artist.Name,
							Tracks = x.Tracks
										.Select(y => new 
										{
											Song = y.Name,
											Genre = y.Genre.Name
										})
						})
						.Dump();
}

public class CustomerItem
{
	public string FullName {get; set;}
	public string City {get; set;}
	public string State {get; set;}
}

public class EmployeeItem
{
	public string FullName {get; set;}
	public string Title {get; set;}
	public string Phone {get; set;}
	public IEnumerable<CustomerItem> CustomerList {get; set;}
}