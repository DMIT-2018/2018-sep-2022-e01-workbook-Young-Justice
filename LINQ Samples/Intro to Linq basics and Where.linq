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

Albums

//query syntax to list all records in an entity (table, collection)
//from arowoncollection in Albums
//select arowoncollection

// method syntax to list all records in an entity
Albums
	.Select (arowoncollection => arowoncollection)
	
// Where
// filter method
// the conditions are setup as you would in C#
// beware that Linqpad may NOT like some C# syntax (DateTime)
// beware that Linq is converted to Sql which may not 
//	 like certain C# syntax because Sql could not convert

// syntax
// notice that the method syntax makes use of the Lambda expressions
// Lambdas are common when performing Linq with the method syntax
// .Where(lambda expression)
// .Where(x => condition [logical operator] condition2 ...)
// .Where(x => x.Bytes > 350000)

Tracks
	.Where(x => x.Bytes > 700000000)
	
Tracks
	.Where(x => x.Milliseconds > 3000000)
	
from x in tracks
where x.Bytes > 700000000
select x

// Find all the albums of the artist Queen.
// concerns: the artist name is in another table
// 			 in an sql Select you would be using an inner Join
// 			 in Linq you do NOT need to specify your inner Join
// 			 instead use the "navigational properties" of your entity
// 			 to generate the relationship

Albums
	.Where(x => x.Artist.Name.Contains("Queen"))