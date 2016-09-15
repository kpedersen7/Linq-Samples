<Query Kind="Expression">
  <Connection>
    <ID>bc6073ae-bf04-42ca-8dc7-72ea1e2524e2</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

/*
Linq expressions/statements/programs are written using C# syntax
*/
//Query syntax list all records from entity

Artists
from x in Artists
select x

//Method syntax list all records from entity
Artists.Select(x => x)

//sort albums by release
from x in Albums
orderby x.ReleaseYear descending, x.Title
select x

//list all albums belonging to artists
//the select is obtaining a subset of attributes from the chosen tables
//the new {} is called an anonymous data set
//anonymous data sets are IOrderedQueryable<>
from x in Albums
select new{
			x.Artist.Name,
			x.Title
			}

//list all albums belonging to artists where a condition exists
//find albums released in a particular year

from x in Albums
where x.ReleaseYear == 2008
orderby x.Artist.Name, x.Title
select new{
			x.Artist.Name,
			x.Title
			}