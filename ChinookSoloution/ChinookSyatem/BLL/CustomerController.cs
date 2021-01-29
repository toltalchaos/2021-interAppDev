using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region additional namespaces
using ChinookSystem.DAL;
using ChinookSystem.Entities; //for SQL and are internal
using ChinookSystem.ViewModels; //for dataclasses to transfer data from BLL to webapp
using System.ComponentModel; //for ODS Wizard
#endregion
namespace ChinookSystem.BLL
{
    [DataObject]
    class CustomerController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CustomersOfCountryEmail> Customers_GetCustomerForCountryEmail(string country2, string email2)
        {
            using (var context = new ChinookSystemContext())
            {
                //select all albums and create new instance of viewmodel from the database 
                //linq doesnt have using context clause.
                IEnumerable<CustomersOfCountryEmail> results = from x in context.Customers
                                                               where x.Country.Contains(country2) && x.Email.Contains(email2)
                                                               orderby x.LastName, x.FirstName
                                                               select new CustomersOfCountryEmail
                                                               {
                                                                   Name = x.FirstName + " " + x.LastName,
                                                                   Email = x.Email,
                                                                   City = x.City,
                                                                   State = x.State,
                                                                   Country = x.Country
                                                               };

                return results.ToList();
            }
        }
    }
}
