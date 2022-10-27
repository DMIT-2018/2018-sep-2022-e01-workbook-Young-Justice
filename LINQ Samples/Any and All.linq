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

// Any and All

// These filter tests return a true or false condition.
// They work at the complete collection level.

Genres
	.Count()
	//.Dump()
	;
// 25

// Show genres that have tracks which are not on any playlist.
Genres
	.Where(g => g.Tracks.Any(tr => tr.PlaylistTracks.Count() == 0))
	.Select(g => g)
	//.Dump()
	;

// Show genres that have all their tracks appearing at least once
// on a playlist
Genres
	.Where(g => g.Tracks.All(tr => tr.PlaylistTracks.Count() > 0))
	.Select(g => g)
	//.Dump()
	;
	
// There maybe times that using a !Any() -> All(!relationship)
//		and !All -> Any(!relationship)

// Using All and Any in comparing 2 collections
// if your collection is NOT a complex record there is a Linq method
// called .Except that can be used to solve your query.

// Compare the track collection of 2 people using All and Any

// Roberto Almeida and Michelle Brooks

var almeida = PlaylistTracks
				.Where(x => x.Playlist.UserName.Contains("AlmeidaR"))
				.Select(x => new 
				{
					Song = x.Track.Name,
					Genre = x.Track.Genre.Name,
					Id = x.TrackId,
					Artist = x.Track.Album.Artist.Name
				})
				.Distinct()
				.OrderBy(x => x.Song)
				//.Dump()	//	->	110
				;

var brooks = PlaylistTracks
				.Where(x => x.Playlist.UserName.Contains("BrooksM"))
				.Select(x => new 
				{
					Song = x.Track.Name,
					Genre = x.Track.Genre.Name,
					Id = x.TrackId,
					Artist = x.Track.Album.Artist.Name
				})
				.Distinct()
				.OrderBy(x => x.Song)
				//.Dump()	//	->	88
				;
				
// List the tracks that BOTH Roberto and Michelle like
// Compare 2 datasets together; data in listA that is also in listB
// Assume listA is Roberto and listB is Michelle
// listA is what you wish to report from
// listB is what you wish to compare to

// What songs does Roberto like but not Michelle

var c1 = almeida
			.Where(rob => !brooks.Any(mic => mic.Id == rob.Id))
			.OrderBy(rob => rob.Song)
			//.Dump()
			;

var c2 = almeida
			.Where(rob => brooks.All(mic => mic.Id != rob.Id))
			.OrderBy(rob => rob.Song)
			//.Dump()
			;

var c3 = brooks
			.Where(mic => almeida.All(rob => rob.Id != mic.Id))
			.OrderBy(mic => mic.Song)
			//.Dump()
			;

// What songs do Michelle and Roberto like

var c4 = brooks
			.Where(mic => almeida.Any(rob => rob.Id == mic.Id))
			.OrderBy(mic => mic.Song)
			//.Dump()
			;