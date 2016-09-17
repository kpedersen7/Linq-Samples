<Query Kind="Expression">
  <Connection>
    <ID>bc6073ae-bf04-42ca-8dc7-72ea1e2524e2</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//this sample requires a subset of the entity record
//can also use.Equals in WHERE clause.
from x in Customers
where x.SupportRepIdEmployee.FirstName == "Jane" && x.SupportRepIdEmployee.LastName == "Peacock"
orderby x.LastName
select new {
	x.LastName,
	x.FirstName, 
	x.City,
	x.State,
	x.Phone,
	x.Email
}

//----------------------
// declare variable and assign it to the count
//for aggregates (count, sum, max, min), it is best to consider starting from the parent table 
from x in Albums
orderby x.Title
select new {
	x.Title,
	TotalTracksForAlbum = x.Tracks.Count()
}

//----------------------
//null error could occur if a collection is empty for specific aggregates
//example: sum of tracks could = 0
from x in Albums
where x.Tracks.Count() > 0
orderby x.Title
select new {
	x.Title,
	TotalTracksForAlbum = x.Tracks.Count(),
	TotalPriceForAlbum = x.Tracks.Sum(y => y.UnitPrice)
}

//---------------------

from x in Albums
where x.Tracks.Count() > 0
orderby x.Title
select new {
	x.Title,
	TotalTracksForAlbum = x.Tracks.Count(),
	TotalPriceForAlbum = x.Tracks.Sum(y => y.UnitPrice),
	AverageTrackLength = (x.Tracks.Average(y => y.Milliseconds)) / 1000
}