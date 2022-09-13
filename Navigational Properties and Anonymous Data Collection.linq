<Query Kind="Expression">
  <Connection>
    <ID>2bdb0ba9-1c6c-468e-8219-519357e91e39</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>WB320-15\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

// Using Navigational Properties and Anonymous data set (collection)
//
// Reference: Student Notes/Demo/eRestaurant/Linq: Query and Method Syntax
//
// Find all albums released in the 90's (1990-1999).
// Order the albums by ascending year and then alphabetically by album title.
// Display the Year, Title, Artist Name, and Release Label.
//
// Concerns: A) Not all properties of Album are to be displayed.
//			 B) The order of the properties are to be displayed
//				in a different sequence than the definition of 
//				the properties on the entity Album.
//			 C) The artist name is NOT on the Album table BUT 
//				is on the Artist Table.
//
// Solution: Use an anonymous data collection
//
// The anonymous data instance is defined within the Select by
// the declared fields (properties).
//
// The order of the fields on the new defined instance will be
// done in specifying the properties of the anonymous data collection.

Albums
	.Where(x => x.ReleaseYear > 1990 && x.ReleaseYear < 2000)
	//.OrderBy(x => x.ReleaseYear)
	//.ThenBy(x => x.Title)
	.Select(
		x =>
			new
			{
				Year = x.ReleaseYear,
				Title = x.Title,
				Artist = x.Artist.Name,
				Label = x.ReleaseLabel
			}
	)
	// Year is in the anonymous data type definition. ReleaseYear is NOT.
	.OrderBy(x => x.Year)
	.ThenBy(x => x.Title)