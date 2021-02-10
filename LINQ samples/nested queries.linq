<Query Kind="Program">
  <Connection>
    <ID>92e63037-ea98-4877-a380-296c48099583</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

// NESTED QUERIES 

void Main()
{
	//nested queries
	// sometimes reffered to as sub queries 
	
	//list ALL sales support employees showing the full name (last,First), Title and the # of customers Each supports
	//order by Full Name
	//in addition - show list of customers for each employee. list the customer Full Name, phone, City, state
	
	//there will be 2 seperate lists within the final dataset collection
		//1-employee
		//2-customers of employee
	var resultsq =
					//select the employee -
					from emp in Employees
					where emp.Title.Contains("Sales Support")
					orderby emp.LastName,emp.FirstName
					select new EmployeeCustomerList {

							Employee = emp.LastName + ',' + emp.FirstName,
							Title = emp.Title,
							CustomerSupportCount = emp.SupportRepIdCustomers.Count(),
							//select the customer -
							CustomerSupportItems = (from cus in emp.SupportRepIdCustomers
												select new CustomerSupportItem {
													CustomerName = cus.LastName + ',' + cus.FirstName,
													Phone = cus.Phone,
													City = cus.City,
													State = cus.State
												}).ToList()
		
		
		
						};
	
	resultsq.Dump();
			
	//Create alist of albums showing its title and artist.
//Show albums with 25 or more tracks only.
//Show the songs on the album listing the name and song length.


var albumres = from alb in Albums
				where alb.Tracks.Count() >= 25
				select new LargeAlbums {
					
					Title = alb.Title,
					Artist = alb.Artist.Name,
					Songs = from trk in alb.Tracks
							select new Song{
							
								Name = trk.Name,
								Length = trk.Milliseconds / 1000
							
							
							}
					
				};



albumres.Dump();




}


//define other classes, methods, namespaces here 



public class CustomerSupportItem{

	public string CustomerName{get; set;}
	public string Phone{get; set;}
	public string City{get; set;}
	public string State{get; set;}

}

public class EmployeeCustomerList{

	public string Employee{get; set;}
	public string Title{get; set;}
	public int CustomerSupportCount{get; set;}
	
	public IEnumerable<CustomerSupportItem> CustomerSupportItems{get; set;}

}

public class Song{

	public string Name{get; set;}
	public int Length{get; set;}
}



public class LargeAlbums{

	public string Title{get; set;}
	public string Artist{get; set;}
	public IEnumerable<Song> Songs{get; set;}

}


