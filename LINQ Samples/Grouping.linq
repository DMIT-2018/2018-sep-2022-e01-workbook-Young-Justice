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

// Grouping

// When you create a group, it builds two (2) components:
//
// A) Key component (deciding criteria value(s)) defining the group.
// 	  Reference this component using the groupname.Key[.propertyname]
//	  a) If you have 1 value for key: groupname.Key
// 	  b) If you have n values for key: groupname.Key.propertyname
//
//	  (Property < - > Field < - > Attribute < - > Value)
//
// B) Data of the group (raw instances of the collection)
//
//	  Ways to group:
// 	  a) By a single column (field, attribute, property): groupname.Key
// 	  b) By a set of columns (anonymous dataset): 		  groupname.Key.property
// 	  c) by using an entity (entity name/navproperty):	  groupname.Key.property

// Concept Processing
//
// Start with a "pile" of data (original collection prior to grouping)
// 
// Specify the grouping property(ies)
//
// Result of the group operation will be to "place the data into a smaller piles"
// 		The piles are dependant on the grouping property(ies) value(s).
// 		The grouping property(ies) become the Key.
// 		The individual instances are the data in the smaller piles.
// 		The entire individual instance of the original collection is placed in the smaller piles.
//
// Manipulate each of the "smalller piles" using your Linq commands.

// Grouping is different than Ordering.
// Ordering is the final resequencing of a collection for display.
// Grouping reorganizes a collection into separate, usually smaller 
// collections for further processing (i.e. aggregates).

// Grouping is an excellent way to organize your data especially if
// you need to process data on a property that is "NOT" a relative key
// such as a foreign key which forms a "natural" group using the 
// navigational properties.

// Display albums by ReleaseYear
//		This request does NOT need grouping.
// 		This request is an ordering of output : OrderBy
// 		This ordering affects only display.
Albums
	.OrderBy(a => a.ReleaseYear)
	
// Display albums grouped by ReleaseYear
// 		Explicit request to breakup the display into desired piles
Albums
	.GroupBy(a => a.ReleaseYear)
	
// Processing on the groups created by the Group command

// Display the number of albums produced each year
// List only the years which have more than 10 albums
Albums
	.GroupBy(a => a.ReleaseYear)
	//.Where(eGP => eGP.Count() > 10)
	.Select(eachGroupPile => new
	{
		Year = eachGroupPile.Key,
		NumberOfAlbums = eachGroupPile.Count()
	})
	.Where(a => a.NumberOfAlbums > 10)

// Use a multiple set of properties to form the group.
// Include a nested query to report on the small pile group.

// Display albums grouped by ReleaseLabel, ReleaseYear.
// Display the ReleaseYear and number of albums.
// List only the years with 10 or more albums released.
// For each album, display the Title and ReleaseYear.
Albums
	.GroupBy(a => new {a.ReleaseLabel, a.ReleaseYear})
	.Where(eGP => eGP.Count() > 2)
	.ToList() // This forces the collection into memory for further processing in TrackCountA
	.Select(eachGroupPile => new
	{
		Label = eachGroupPile.Key.ReleaseLabel,
		Year = eachGroupPile.Key.ReleaseYear,
		NumberOfAlbums = eachGroupPile.Count(),
		AlbumItems = eachGroupPile
						.Select(eGPInstance => new 
						{
							Title = eGPInstance.Title,
							Artist = eGPInstance.Artist.Name,
							TrackCountA = eGPInstance.Tracks.Count(),
							TrackCountB = eGPInstance.Tracks.Select(x => x).Count(),
							YearOfAlbum = eGPInstance.ReleaseYear
						})
	})




	
	
	
	