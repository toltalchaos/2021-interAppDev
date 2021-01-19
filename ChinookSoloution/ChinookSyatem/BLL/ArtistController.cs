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
    public class ArtistController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)] //expose to ODS
        public List<SelectionList> Artist_DDLList()
        {
            using(var context = new ChinookSystemContext())
            {
                //select ALL records (x) . this property (using entities as refrence to object properties as refrenced in context)
                IEnumerable<SelectionList> results = from x in context.Artists
                                                     select new SelectionList
                                                     {
                                                         ValueField = x.ArtistId,
                                                         DisplayField = x.Name
                                                     };
                return results.ToList();
            }
        }
    }
}
