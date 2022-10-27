<Query Kind="Expression">
  <Connection>
    <ID>c1f43fa7-9fd6-4326-9744-e514096b9097</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>WC320-08\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>FSIS_2018</Database>
  </Connection>
</Query>

// Question 1

// List all the Guardians with more than one child in the league.
// Sort the output by the number of guardian children, guardians 
// with the most children first. Sort the listed children by Age.


Guardians
	.Where(g => g.Players.Count() > 1)
	.OrderByDescending(g => g.Players.Count())
	.Select(g => new
	{
		Name = g.FirstName + " " + g.LastName,
		Children = g.Players
						.Select(p => new 
						{
							Name = p.FirstName + " " + p.LastName,
							Age = p.Age,
							Gender = p.Gender,
							Team = p.Team.TeamName
						})
						.OrderBy(p => p.Age)
	})






















