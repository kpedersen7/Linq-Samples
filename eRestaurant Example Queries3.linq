<Query Kind="Expression">
  <Connection>
    <ID>bc6073ae-bf04-42ca-8dc7-72ea1e2524e2</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Syntax style for .Union() is 
//(query).Union(query2).Union(queryn).OrderBy(firstsortfield).ThenBy(anotherfield)

//to get both albums with tracks and without tracks, ---> use .Union()
(from x in Albums
where x.Tracks.Count() > 0
select new{
	Title = x.Title,
	TotalTracksForAlbum = x.Tracks.Count(),
	TotalPriceForAlbumTracks = x.Tracks.Sum(y => y.UnitPrice),
	AverageTrackLength = x.Tracks.Average(y => y.Milliseconds)/1000.0
}).Union(
from x in Albums
where x.Tracks.Count() > 0
orderby x.Tracks.Count(), x.Title
select new{
	Title = x.Title,
	TotalTracksForAlbum = 0,
	TotalPriceForAlbumTracks = 0.00m,
	AverageTrackLength = 0.00
}).OrderBy(y => y.TotalTracksForAlbum).ThenBy(y => y.Title)