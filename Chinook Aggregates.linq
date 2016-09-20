<Query Kind="Statements">
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

//---------------------
//C# Statements
//A statement has a receiving variable which is set by the results of the query
//When you need to use multiple steps to solve a problem, switch your language choice to either:
// Statement(s) or Program
var maxCount = (from x in MediaTypes
				select x.Tracks.Count()).Max();
//to display the contents of a variable in LinqPad, 
//you use the method .Dump()
//maxCount.Dump();

//-------------------
//to filter data, you can use the WHERE clause
var mediaTypeCounts = from x in MediaTypes
						where x.Tracks.Count() == maxCount 
						select new {
							Name = x.Name,
							TrackCount = x.Tracks.Count()
						};
	mediaTypeCounts.Dump();
//above example uses maxCount, instantiated in the previous example, but 
//we can nest the query used to make maxCount
//directly into our next example, so it can be run independently of the one above it.
var mediaTypeCounts = from x in MediaTypes
						where x.Tracks.Count() == (from y in MediaTypes
												select y.Tracks.Count()).Max() 
						select new {
							Name = x.Name,
							TrackCount = x.Tracks.Count()
						};
	mediaTypeCounts.Dump();

//using a method syntax to determine the Count() value for the Where expression
//this demonstrates that queries can be constructed using both query syntax and method syntax
var mediaTypeCountsMethod = from x in MediaTypes
						where x.Tracks.Count() == MediaTypes.Select(y => y.Tracks.Count()).Max()
						select new {
							Name = x.Name,
							TrackCount = x.Tracks.Count()
						};
	mediaTypeCountsMethod.Dump();