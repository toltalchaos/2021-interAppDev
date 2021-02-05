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
    public class EmployeeController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<EmployeeCustomerList> Employee_EmployeeCustomerList()
        {

            using(var context = new ChinookSystemContext())
            {

				IEnumerable<EmployeeCustomerList> resultsq =
					//select the employee -
					from emp in context.Employees
					where emp.Title.Contains("Sales Support")
					orderby emp.LastName, emp.FirstName
					select new EmployeeCustomerList
					{

						Employee = emp.LastName + "," + emp.FirstName,
						Title = emp.Title,
						CustomerSupportCount = emp.Customers.Count(),
						//select the customer -
						CustomerSupportItems = (from cus in emp.Customers
												select new CustomerSupportItem
												{
													CustomerName = cus.LastName + "," + cus.FirstName,
													Phone = cus.Phone,
													City = cus.City,
													State = cus.State

												}).ToList()



					};

				return resultsq.ToList();
			}


        }
    }
}
