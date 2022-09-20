<Query Kind="Expression">
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

// Aggregates

// .Count(): counts the number of instances in the collection
// .Sum(x => ...): sums (totals) a numeric field (numeric expression) in a collection
// .Min(x => ...): finds the minimum value of a collection for a field
// .Max(x => ...): finds the maximum value of a collection for a field
// .Average(x => ...): finds the average value of a numeric field (numeric expression) in a collection

// ----------
//| IMPORTANT |
// ----------
// Aggregates work ONLY on a collection of values
// Aggregates DO NOT work on a single instance (non-declared collection)

// .Sum, .Min, .Max, and .Average MUST have at least one record in their collection
// .Sum and .Average MUST work on numeric fields and the field CANNOT be null.

// Syntax: Method
//
// collectionset.aggregate(x => expression)
// collectionset.Select(...).aggregate()
// collectionset.Count()	----> .Count() does not contain an expression

// For .Sum, .Min, .Max, and .Average: the result is a single value.

// You can use multiple aggregates on a single column
// 	.Sum(x => [expression]).Min(x => [expression])



// Find the average playing time (length) of tracks in our music
// collection.

// Thought process:
// Average is an aggregate
// The Tracks table is the collection.
// Tracks.Milliseconds is the expression.

Tracks.Average(x => x.Milliseconds)
Tracks
	.Select(x => x.Milliseconds)
	.Average()

Tracks
	.Where(x => x.Album.Artist.Name == "Queen")
	.Average(x => x.Milliseconds)
	
// List all albums of the 60s showing the title, artist, and various aggregates for albums containing tracks

// For each album, show the number of tracks, the total price of
// all tracks, and the average playing length of the album tracks.

// Thought process:
// Start at Albums.
// Album.Artist.Name for Artist Name
// Album.Tracks for collection of tracks
// Album.Tracks.Count() for number of tracks
// Album.Tracks.UnitPrice.Sum() for price of tracks
// Album.Tracks.Milliseconds.Average() for average playing length

Albums
	.Where(x => x.Tracks.Count() != 0 && x.ReleaseYear < 1970 && x.ReleaseYear >= 1960)
	.Select(x => new 
	{
		Title = x.Title,
		Artist = x.Artist.Name,
		NumberOfTracks = x.Tracks.Count(),
		TotalPrice = x.Tracks.Sum(t => t.UnitPrice),
		AveragePlayingLength = x.Tracks.Select(t => t.Milliseconds).Average()
	})







