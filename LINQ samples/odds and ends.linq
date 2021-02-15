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
// .All() returns objects matching perrams

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




// comparing the playlist of roberto almedia  (AlmediaR) and MichelleBrooks (BrooksM)

// compare two Lists<> to eachother
//obtain a distinct list of all playlist tracks for Roberto
// .Distinct() can destroy the sort of a query syntax, thus add sort after the .distinct()

var almedia = (from x in PlaylistTracks
				where x.Playlist.UserName.Contains("AlmeidaR")
				select new {
					genre = x.Track.Genre.Name,
					id = x.TrackId,
					song = x.Track.Album.Artist.Name,
					artist = x.Track.Album.Artist.Name
				}).Distinct().OrderBy(y=>y.song);
almedia.Dump(); //110

var brooksm = (from x in PlaylistTracks
				where x.Playlist.UserName.Contains("BrooksM")
				select new {
					genre = x.Track.Genre.Name,
					id = x.TrackId,
					song = x.Track.Album.Artist.Name,
					artist = x.Track.Album.Artist.Name
				}).Distinct().OrderBy(y=>y.song);
brooksm.Dump(); //88


//compare the two for repeating data

var likes = almedia
				.Where(rob => brooksm.Any(mic => mic.id == rob.id))
				.OrderBy(rob => rob.song)
				.Select(rob => rob);
likes.Dump();

//list the tracks that rob likes but mich does not -dont include ANY tracks that dont match and select ALL almedia tracks
var notlikes = almedia
				.Where(rob => !brooksm.Any(mic => mic.id == rob.id))
				.OrderBy(rob => rob.song)
				.Select(rob => rob);
notlikes.Dump(); //109
				
				
	//compairing that the track IDs in robs list dont match the IDs in mics list
	//select all the tracks in MIC and dont include the tracks that are Not Equal to robs List
var notlikes2 = from mic in brooksm
				where almedia.All(rob => mic.id != rob.id)
				orderby mic.song
				select mic;
notlikes2.Dump(); //87


//produce a report where the track is flagged as shorter, longer, or average then average in play length

//find average play length
var averageLength = Tracks
					.Where(tr => tr.Genre.Name.Contains("Rock"))
					.Average(tr => tr.Milliseconds);
					
averageLength.Dump();


//select play length and flag based on comparison
var resultsTrackAverageLength = (from tra in Tracks
								where tra.Genre.Name.Contains("Rock")
								select new 
								{
								
									song= tra.Name,
									miliseconds = tra.Milliseconds,
									length = (tra.Milliseconds < averageLength) ? "Shorter":
											(tra.Milliseconds < averageLength) ? "Longer": "Average"
											
								}).OrderBy(tra => tra.length);
								
resultsTrackAverageLength.Dump();




//unions. 
//the joining of mul

//(query).Union(query).Union(query).Union(query)
//rules. same as SQL - # of columns must be the same as well as datatype
//ordering should be done as a method on the unioned dataset

//list the stats of albums on Tracks (count, Cost, average Length)
//note - cost and average one will need an instance (track on album) to actually process the method.
//	if an album contains no tracks then no SUM() or AVG() can be physically done.

//to do this example you will need an album with no tracks on the database. 


var unionresults = (from x in Albums 
					where x.Tracks.Count() > 0
					select new {
						title = x.Title,
						totaltracks = x.Tracks.Count(),
						totalprice = x.Tracks.Sum(tr => tr.UnitPrice),
						avglength = x.Tracks.Average(tr => tr.Milliseconds) / 1000
					});
unionresults.Dump();					
				
var nontrack = (Albums.Where(x => x.Tracks.Count() == 0)
					.Select(x => new {

						title = x.Title,
						totaltracks = 0,
						totalprice =0m,
						avglength = 0
					})); 
nontrack.Dump();

//written as union

var fullunionresults = (from x in Albums 
					where x.Tracks.Count() > 0
					select new {
						title = x.Title,
						totaltracks = x.Tracks.Count(),
						totalprice = x.Tracks.Sum(tr => tr.UnitPrice),
						avglength = x.Tracks.Average(tr => tr.Milliseconds) / 1000.0
					}).Union(Albums
									.Where(x => x.Tracks.Count() == 0)
									.Select(x => new {
													title = x.Title,
													totaltracks = 0,
													totalprice = 0.00m,
													avglength = 0.0
					})).OrderBy(x => x.totaltracks);
fullunionresults.Dump();	











