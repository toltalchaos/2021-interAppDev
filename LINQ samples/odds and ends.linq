<Query Kind="Statements">
  <Connection>
    <ID>92e63037-ea98-4877-a380-296c48099583</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

// odds and ends 
// linq = lazy load data
//times happen in memory - .ToList() to force into memory
//						   .AsEnumerable()
//						   .AsQueryable()

// .FirstOrDefault() / .First() - returns the first object, OR return a NULL value

//.SingleOrDefault() / .Single() - returns expected SINGLE ITEM (prop from obj), OR return a NULL value

//.Distinct() -- nukes repeated values.

var distinctMQ = Customers
					.Select(x => x.Country).Distinct();
distinctMQ.Dump();	

var distinctQS = (from x in Customers select x.Country).Distinct();
distinctQS.Dump();

// see .Take() and .TakeWhile() - conditional selecting 
//.Skip() and .SkipWhile() skips over records conditionally
// where... x.y.Any() grabs T or F on records matching specific perrams 
// .All()

//			show genres with tracks not on a playlist

//number of genres
var genrecount = Genres.Count();
genrecount.Dump();


var genresNotOnPlaylists = from gen in Genres 
							where gen.Tracks.Any(track => track.PlaylistTracks.Count() == 0) //if the count of a genre track on playlisttracks is 0 
							//returning 1 false will omit genre from clause
							select gen;

genresNotOnPlaylists.Dump();

//			show genres that have ALL tracks atleast once on any Playlists

var genresAllOnPlaylists = from gen in Genres
							where gen.Tracks.All(track => track.PlaylistTracks.Count() > 0)
							select gen;
genresAllOnPlaylists.Dump();









