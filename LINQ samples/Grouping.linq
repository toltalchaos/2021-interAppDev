<Query Kind="Statements">
  <Connection>
    <ID>92e63037-ea98-4877-a380-296c48099583</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

// from x in item
// 		group x by new {x.property, x.propertyTwo}

// from x in item
// 		group x by x.property into result
//		select result

// item .groupby (x => x.property) .select(result => result)  


// grouping can be by column				groupname.Key 
// grouping can be by multiple columns		groupname.key.attribute
// grouping can be by entities				groupname.key.entityattribute

//groups have two components 
//A - key component (group by) - referenced by groupname.key[.attribute]
//B - data - instances in the group

//process. - start with pile of data
		// - specify grouping attribute(s)
		// - result is smaller "piles" of data by attributes

//display albums by release year - order by 

var resultsorderby = from x in Albums
						orderby x.ReleaseYear
						select x;			
resultsorderby.Dump();

//display albums by release year - order by 

var resultsgroupby = from x in Albums
						group x by x.ReleaseYear;				
resultsgroupby.Dump();


//group by artist name AND album release year

var resultsgroupbycolumns = from x in Albums
								group x by new {x.Artist.Name, x.ReleaseYear};
								
resultsgroupbycolumns.Dump();


//group tracks by their album. selecting entity attached to the track and grouping like-keys

var resultsgroupbyentity = from x in Tracks
							group x by x.Album;
						
resultsgroupbyentity.Dump();


//IMPORTANT!!!!!!!!
// IF YOU WISH TO "REPORT" ON GROUPS (AFTER THE GROUP BY) 
//		YOU MUST SAVE THE GROUPING TO A TEMPORARY DATASET
//		THEN YOU MUST USE THE TEMPORARY DATASET TO REPORT FROM 


//for query syntax
// your temporary dataset name is created by using  -> into gName

//for method syntax the temporary name is the placeholder of the select -> .Select(gName => ...)

//the temporary datasets are created in memory and once the query is completed the temporary dataset no longer exist.


var g = from x in Albums
			group x by x.ReleaseYear into gAlbumYear
			select new {
			
				KeyValue = gAlbumYear.Key,
				NumberOfAlbums = gAlbumYear.Count(),
				AlbumAndArtist = from y in gAlbumYear
									select new {
										Title = y.Title,
										Name = y.Artist.Name
									}
				
			
			
			};
			
g.Dump();


//same same but group on artist
//key = instance entity

var grouonartist = from x in Albums
			where x.ReleaseYear > 1969 && x.ReleaseYear < 1980
			group x by x.Artist into gAlbumArtist
			orderby gAlbumArtist.Key.Name
			select new {
			
				KeyValue = gAlbumArtist.Key.Name,
				NumberOfAlbums = gAlbumArtist.Count(),
				AlbumAndYear = from y in gAlbumArtist //selecting album from THIS artist group
									select new {
										Title = y.Title,
										Year = y.ReleaseYear
									}
			
			};
			
grouonartist.Dump();



//Create a query which will report the employee and their customer base.
//List the employee's full name (phone), number of customers in their base.
//List the full name, city and state for the customer base.

var employeeandcustomer = from emp in Employees
							group emp by emp into empId
							select new{
							
								EmployeeName = empId.Key.FirstName,
								EmployeePhone = empId.Key.Phone,
								CustCount = empId.Key.SupportRepIdCustomers.Count(),
								CustomerList = from cust in empId.Key.SupportRepIdCustomers
												select new {
													Name = cust.FirstName,
													City = cust.City,
													State = cust.State
												}
									
							
							};

employeeandcustomer.Dump();

//dons version

var groupEmployeesAndCustomerBase =	from x in Customers
									group x by x.SupportRepIdEmployee into gTemp
									select new
									{
										EmployeeName = gTemp.Key.LastName + ",  " + gTemp.Key.FirstName + "(" + gTemp.Key.Phone +")",
										
										BaseCount = gTemp.Count(),
										CustomerList = 	from y in gTemp
														select new
														{
															Name = y.FirstName + " " + y.LastName,
															City = y.City,
															State = y.State
														}
									};
	groupEmployeesAndCustomerBase.Dump();







