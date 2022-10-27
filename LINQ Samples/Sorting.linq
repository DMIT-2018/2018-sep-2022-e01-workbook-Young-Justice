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

// Sorting

// There is a significant difference between query syntax
// and method syntax
//
// Query syntax is much like SQL
//
// orderby field {[ascending]|descending} [,field....]
// ascending is the default option
//
// Method syntax is a series of individual methods
// .OrderBy(x => x.field) 		   	------> FIRST FIELD ONLY
// .OrderByDescending(x => x.field) ------> FIRST FIELD ONLY
// .ThenBy(x => x.field) 			------> EACH FOLLOWING FIELD
// .ThenByDescending(x => x.field) 	------> EACH FOLLOWING FIELD

// Ex. Find all of the album tracks for the band Queen.
// Order the track by the album and then the track name alphabetically.

// Query Syntax
from x in Tracks
where x.Album.Artist.Name.Contains("Queen")
orderby x.AlbumId, x.Name
select x

// Method Syntax
Tracks
	.Where(x => x.Album.Artist.Name.Contains("Queen"))
	.OrderBy(x => x.Album.Title)
	.ThenBy(x => x.Name)

// Order of sorting and filtering can be changed:

Tracks
	.OrderBy(x => x.Album.Title)
	.ThenBy(x => x.Name)
	.Where(x => x.Album.Artist.Name.Contains("Queen"))

	