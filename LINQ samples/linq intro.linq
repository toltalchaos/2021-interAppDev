<Query Kind="Expression">
  <Connection>
    <ID>92e63037-ea98-4877-a380-296c48099583</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
  <Output>DataGrids</Output>
</Query>

////method syntax 
//
Albums.Select(AnyRowAtAnyPointInTime => AnyRowAtAnyPointInTime), //xelegate to select all rows using lambda 
//
//// query syntax

from x in Albums
select x,





//filtering 
//where clause in QUERY syntax 
//.Where() method in method syntax

//----find all albums released in 1990

from x in Albums
where x.ReleaseYear == 1990
select x,


Albums
	.Where(x => x.ReleaseYear == 1990)
	.Select(x => x),
	

//find all albums released in the good old 70s

from x in Albums
where ((x.ReleaseYear >= 1970) && (x.ReleaseYear < 1980))
select x,

Albums
	.Where(x=>(x.ReleaseYear >= 1970) && (x.ReleaseYear < 1980))
	.Select(x=>x),
	
	
//ordering 
//list all albums by ascending year of release
from x in Albums
orderby x.ReleaseYear
select x,

Albums.OrderBy(x => (x.ReleaseYear)).Select(x => x),

//find albums in descending year of release in alphabetical order by Title

from x in Albums
orderby x.ReleaseYear descending, x.Title
select x,



Albums.OrderByDescending(x => (x.ReleaseYear)).ThenBy(x=>x.Title).Select(x => x),

//What about only certian fields (partial entity records or fields from another table)
//list all records from 1970 showing title artist and year


from x in Albums
where ((x.ReleaseYear >= 1970) && (x.ReleaseYear < 1980))
orderby x.ReleaseYear, x.Title
select new {
				Title = x.Title,
				Artist = x.Artist.Name,
				Year = x.ReleaseYear
			},
			

Albums
	.Where(x=>(x.ReleaseYear >= 1970) && (x.ReleaseYear < 1980))
	.OrderBy(x => (x.ReleaseYear))
	.ThenBy(x=>x.Title)
	.Select(x => new {
					Title = x.Title,
					Artist = x.Artist.Name,
					Year = x.ReleaseYear
					})




















