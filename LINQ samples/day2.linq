<Query Kind="Program">
  <Connection>
    <ID>92e63037-ea98-4877-a380-296c48099583</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	//list all customers in alphabetic order by last name then first name who live in the US and have an email of yahoo
	//list their full name, email, city, and state 
	//
	//from x in Customers
	//where x.Country == "US" && x.Email =="yahoo"//use.Contains("")
	//orderby x.LastName, x.FirstName
	//select new{
	//	Name = x.FirstName + " " + x.LastName,
	//	Email = x.Email,
	//	City = x.City,
	//	State = x.State
	//},
	//
	//
	//
	//Customers
	//	.Where(x=> x.Country == "USA" && (x.Email.Contains("yahoo")))
	//	.OrderBy(x => x.LastName)
	//	.ThenBy(x => x.FirstName)
	//	.Select(x => new{
	//		Name = x.FirstName + " " + x.LastName,
	//		Email = x.Email,
	//		City = x.City,
	//		State = x.State
	//}),
	//
	//create an alphabetic list of albums by release label.
	//show the title and release label
	//missing album labels will be listed as "UNKNOWN"
	//
	//from x in Albums 
	//orderby x.ReleaseLabel
	//select new
	//{
	//		Title = x.Title,
	//		Label = x.ReleaseLabel == null? "UNKNOWN" : x.ReleaseLabel
	//},
	//
	//
	//
	//
	//Albums
	//		.OrderBy(x => x.ReleaseLabel)
	//		.Select(x => new {
	//			Label = x.ReleaseLabel == null? "UNKNOWN" : x.ReleaseLabel,
	//			Title = x.Title
	//		}),
	//		
	//		
	//create an alphabetic list of albums by stating the album decade for the 70s 80s and 90s
	//list the alphabetic title, year, and its decade
	//
	//Albums 
	//		.Where(x => x.ReleaseYear < 2000 && x.ReleaseYear > 1969)
	//		.OrderBy(x => x.Title)
	//		.Select( x=> new {
	//				Title = x.Title,
	//				Year = x.ReleaseYear,
	//				Decade = (x.ReleaseYear > 1969 && x.ReleaseYear < 1980)? "70s": 
	//				((x.ReleaseYear > 1979 && x.ReleaseYear < 1990)? "80s" : "90s")
	//		}),
	//	
	//
	//from x in Albums
	//orderby x.Title
	//where x.ReleaseYear > 1969 && x.ReleaseYear < 2000
	//select new
	//{​​
	//Title = x.Title,
	//Year = x.ReleaseYear,
	//Decade = (x.ReleaseYear > 1969 && x.ReleaseYear < 1980)? "70s":
	//((x.ReleaseYear > 1979 && x.ReleaseYear < 1990)? "80s": "90s")}​​,
	//
	//
	//	using vars
	//
	//string country = "USA";
	//string email = "yahoo";
	//var results = 		
	//Customers
	//	.Where(x=> x.Country == country && (x.Email.Contains(email)))
	//	.OrderBy(x => x.LastName)
	//	.ThenBy(x => x.FirstName)
	//	.Select(x => new CustomersOfCountryEmail{
	//		Name = x.FirstName + " " + x.LastName,
	//		Email = x.Email,
	//		City = x.City,
	//		State = x.State
	//});	
	//
	//use linq pad .Dump() to print
	
	//results.Dump();
	
	
	string country2 = "USA";
	string email2 = "yahoo";
	 var results2 = 
				from x in Customers
				where x.Country.Contains(country2) && x.Email.Contains(email2)
				orderby x.LastName, x.FirstName
				select new CustomersOfCountryEmail{
						Name = x.FirstName + " " + x.LastName,
						Email = x.Email,
						City = x.City,
						State = x.State,
						Country = x.Country
					};
			
	results2.Dump();
}

public class CustomersOfCountryEmail
{
		public string Name {get; set;}
		public string Email {get; set;}
		public string City {get; set;}
		public string State {get; set;}
		public string Country{get; set;}
}




// You can define other methods, fields, classes and namespaces here
