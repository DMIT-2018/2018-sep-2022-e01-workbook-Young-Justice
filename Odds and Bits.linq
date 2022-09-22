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
	// Conversions
	
	// Collections we will look at are IQueryable, IEnumerable, and List
	
	// Display all albums and their tracks. Display the album title
	// artist name, and album tracks. For each track, show the song name
	// and play time. Show only albums with 25 or more tracks.
	
	List<AlbumTracks> albumList = Albums
					.Where(a => a.Tracks.Count >= 25)
					.Select(a => new AlbumTracks
					{
						Title = a.Title,
						Artist = a.Artist.Name,
						Songs = a.Tracks
								 .Select(tr => new SongItem
								 {
								 	Song = tr.Name,
									Playtime = tr.Milliseconds / 1000
								 })
								 .ToList()
					})
					.ToList()
					//.Dump()
					;
	
	// Using .FirstOrDefault()
	
	// Find the first album by Deep Purple
	
	var artistparam = "Deep Purple";
	var resultsFOD = Albums
					.Where(a => a.Artist.Name.Equals(artistparam))
					.Select(a => a)
					.OrderBy(a => a.ReleaseYear)
					.FirstOrDefault()
					//.Dump()
					;
	
	if (resultsFOD != null)
	{
		//resultsFOD.Dump();
	}
	else 
	{
		Console.WriteLine($"No albums found for artist {artistparam}");
	}
	
	// Distint()
	// Removes duplicate reported lines
	
	// Get a list of customer countries
	var resultsDistinct = Customers
							.OrderBy(c => c.Country)
							.Select(c => c.Country)
							.Distinct()
							//.Dump()
							;
	
	// .Take() and .Skip()
	// In CPSC1517, when you want your supplied Paginator,
	// the query method was to return ONLY the needed
	// records for the display, NOT the entire collection.
	// a) the query was executed returning a collection of size x
	// b) obtained the total count (x) of return records
	// c) calculated the number of records to skip (pageNumber - 1) * pageSize
	// d) on the return statement you used
	//		return variableName.Skip(rowsSkipped).Take(pageSize).ToList()
	
	// Union
	// Rules in Linq are the same as SQL
	// Result is the same as SQL, combine separate collections into one.
	// Syntax: (queryA).Union(queryB)[.Union(query...)]
	// Rules:
	// 		Number of columns must be the same
	//		Column datatypes must be the same
	// 		Ordering should be done as a method after the last Union
	
	var resultsUnion = (Albums
						.Where(x => x.Tracks.Count() == 0)
						.Select(x => new 
						{
							
							Title = x.Title,
							TotalTracks = 0,
							TotalCost = 0.00m,
							AverageLength = 0.00d
						})
						)
						.Union(Albums
						.Where(x => x.Tracks.Count() > 0)
						.Select(x => new 
						{
							Title = x.Title,
							TotalTracks = x.Tracks.Count(),
							TotalCost = x.Tracks.Sum(tr => tr.UnitPrice),
							AverageLength = x.Tracks.Average(tr => tr.Milliseconds)
						})
						)
						.OrderBy(x => x.TotalTracks)
						.Dump()
						;
}

public class SongItem 
{
	public string Song {get; set;}
	public double Playtime {get; set;}
}

public class AlbumTracks
{
	public string Title {get; set;}
	public string Artist {get; set;}
	public List<SongItem> Songs {get; set;}
}

// You can define other methods, fields, classes and namespaces here