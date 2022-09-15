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
	// Pretend that the Main() is the web page
	
	// Find songs by partial song name.
	// Display the album title, song, and artist name
	// Order by song
	
	// Assume a value was entered into the web page
	// Assume that a post button was pressed
	// Assume Main() is the post event
	
	string inputvalue = "dance";
	
	List<SongList> songCollection = SongsByPartialName(inputvalue);
	
	songCollection.Dump();
}

// You can define other methods, fields, classes and namespaces here

// C# really enjoys strongly typed data fields
// whether these fields are primitive data types (int double, ...)
// or developer defined data types (i.e. classes)

public class SongList
{
	public string Album {get; set;}
	public string Song{get; set;}
	public string Artist{get; set;}
}

// Imagine the following method exists in a service in your BLL
// This method receives the web page parameter value for the query
// This method will need to return a collection

List<SongList> SongsByPartialName(string partialSongName)
{
	List<SongList> songCollection = Tracks
							.Where(t => t.Name.Contains(partialSongName))
							.OrderBy(t => t.Name)
							.Select(t => new SongList
								{
									Album = t.Album.Title,
									Song = t.Name,
									Artist = t.Album.Artist.Name
								}
							).ToList();
	
	return songCollection;
}
