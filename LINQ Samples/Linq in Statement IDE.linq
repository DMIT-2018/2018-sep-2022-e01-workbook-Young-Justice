<Query Kind="Statements">
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

// The Statement ide
// This environment expects the use of C# statement grammar
//
// The results of a query is NOT automatically displayed as is
// the Expression environment.
//
// To display the results you need to .Dump() the variable
// holding the data result.
//
// IMPORTANT!!
// .Dump() is a Linqpad Method. It is NOT a C# method.
//
// Within the Statement environment, one can run ALL the queries
// in one execution.

var qsyntaxlist = from arowoncollection in Albums
				  select arowoncollection;
//qsyntaxlist.Dump();

var msyntaxlist = Albums 
			.Select (arowoncollection => arowoncollection)
			.Dump();
//msyntaxlist.Dump();

var QueenAlbums = Albums
	.Where(a => a.Artist.Name.Contains("Queen"))
	.Dump();