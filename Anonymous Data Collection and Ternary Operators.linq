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

// List all albums by release label. Any album with no label
// should be indicated by Unknown
//
// List Title, Label, and Artist Name
//
// Order by ReleaseLabel
//
// Understand the problem
// Collection: Albums
// Selective data: anonymous data set
// Label (nullable): either Unknown or label name
// Order by release label field
//
// Design
// Albums
// Select (new{})
// Fields: title,
// 		   label,	---> ternary operator (conditon(s) ? true value : false value)
// 		   Artist.Name

Albums
	//.OrderBy(x => x.ReleaseLabel)
	.Select( x => new
		{
			Title = x.Title,
			Label = x.ReleaseLabel == null ? "Unknown" : x.ReleaseLabel,
			Artist = x.Artist.Name,
		}
	)
	.OrderBy(x => x.Label)
	
// List all albums showing the Title, Artist Name, Year and decade of
// release using oldies, 70s, 80s, 90s or modern.
// Order by decade.

Albums
	.Select( x => new
		{
			Title = x.Title,
			Artist = x.Artist.Name,
			Year = x.ReleaseYear,
			Decade = (x.ReleaseYear < 1970) ? "Oldies" : 
					 (x.ReleaseYear < 1980) ? "70s" :
					 (x.ReleaseYear < 1990) ? "80s" : 
					 (x.ReleaseYear < 2000) ? "90s" :
					 "Modern"
		}
	)
	.OrderBy(x => x.Year)