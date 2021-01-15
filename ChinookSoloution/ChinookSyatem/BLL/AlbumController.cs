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
    public class AlbumController
    {
        //controllers interact with the model and context linked to the database
        //call ODS to this method creating a new model from the Database
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ArtistAlbums> Albums_GetArtistAlbums()
        {
            using(var context = new ChinookSystemContext())
            {
                //select all albums and create new instance of viewmodel from the database 
                IEnumerable<ArtistAlbums> results = from x in context.Albums
                                                    select new ArtistAlbums
                                                    {
                                                        Title = x.Title,
                                                        ReleaseYear = x.ReleaseYear,
                                                        ArtistName = x.Artist.Name
                                                    };
                return results.ToList();
            }
        }


    }
}
