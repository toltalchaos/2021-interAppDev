<Query Kind="Statements">
  <Connection>
    <ID>92e63037-ea98-4877-a380-296c48099583</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

// aggregates

//.Count(), .Sum(), .Min(), .Max(), .Average()

//aggregates operate on collections - more then one record

var ex1 = Albums.Count();
ex1.Dump();

// .Sum()
// how much room does the Music Collection on the Database take for albums of the 1990s
//amount of room is the sum of the tracks for the album

var ex2 = (from x in Tracks where x.Album.ReleaseYear == 1990
			select x.Bytes).Sum(); //add the numeric field "bytes"
ex2.Dump();

var ex2m = Tracks
				.Where(x => x.Album.ReleaseYear == 1990)
				.Sum(x => x.Bytes);
ex2m.Dump();

//what is the length of the shortest track playtime from 1990

// select x track
// where releaseyear == 1990
// and
// x == x.min()

var ex3 = (from x in Tracks where x.Album.ReleaseYear == 1990
			select x.Milliseconds).Min(); //add the numeric field "bytes"
ex3.Dump();

var ex03 = Tracks
				.Where(x => x.Album.ReleaseYear == 1990)
				.Min(x => x.Milliseconds);
ex03.Dump();


//List all albums title namr and number of tracks from the 60s - ordered by number of tracks

var ex04 = (from x in Albums where x.ReleaseYear >= 1960 && x.ReleaseYear <= 1969
				orderby x.Tracks.Count() descending
				select new {
				
					Title= x.Title,
					Artist = x.Artist.Name,
					Year = x.ReleaseYear,
					NumberOfTracks= x.Tracks.Count()
					
				
				});
ex04.Dump();

var ex04a =Albums
				.Where( x => x.ReleaseYear >= 1960 && x.ReleaseYear <= 1969)
				.OrderByDescending(x=> x.Tracks.Count())
				.Select(x=> new {
				
					Title= x.Title,
					Artist = x.Artist.Name,
					Year = x.ReleaseYear,
					NumberOfTracks= x.Tracks.Count()
					
				
				});

ex04a.Dump();

var ex04b = (from x in Albums where x.ReleaseYear >= 1960 && x.ReleaseYear <= 1969
				orderby x.Tracks.Count() descending
				select new {
				
					Title= x.Title,
					Artist = x.Artist.Name,
					Year = x.ReleaseYear,
					NumberOfTracks= (from y in x.Tracks select y).Count()
					
				
				});
ex04b.Dump();




//Produce a list of 60's albums which have tracks showing
//their title, artist, number of tracks on album,
//total price of all tracks on album, the longest album track,
//the shortest album track and the average track length.


var ex05a = (from x in Albums 
				where ((x.ReleaseYear >= 1960 && x.ReleaseYear <= 1969) && (x.Tracks.Count() > 0))
				select new {
				
					Title = x.Title,
					Artist = x.Artist.Name,
					NumberOfTracks = x.Tracks.Count(),
					TotalPrice = x.Tracks.Sum(tr => tr.UnitPrice),
					LongestTrackName = (from y in x.Tracks 
											where y.Milliseconds == (from z in x.Tracks select z.Milliseconds).Max()
											select y.Name).FirstOrDefault(),
					LongestTrack = (from y in x.Tracks select y.Milliseconds).Max(),
					ShortestTrack = x.Tracks.Min(tr => tr.Milliseconds),
					AverageTrackLengthSeconds = x.Tracks.Average(tr => tr.Milliseconds / 1000)

				});
ex05a.Dump();





