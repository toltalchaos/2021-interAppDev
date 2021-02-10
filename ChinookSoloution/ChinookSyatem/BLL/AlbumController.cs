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
        #region Queries
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

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ArtistAlbums> Albums_GetAlbumsForArtist(int artistid)
        {
            using (var context = new ChinookSystemContext())
            {
                //select items where artistid==x.artistid
                IEnumerable<ArtistAlbums> results = from x in context.Albums
                                                    where x.ArtistId == artistid
                                                    select new ArtistAlbums
                                                    {
                                                        Title = x.Title,
                                                        ReleaseYear = x.ReleaseYear,
                                                        ArtistName = x.Artist.Name,
                                                        ArtistId = x.ArtistId
                                                    };
                return results.ToList();
            }
        }



        //query to return all data of the Album table
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AlbumItem> Albums_List()
        {
            using (var context = new ChinookSystemContext())
            {
                //select all albums and create new instance of viewmodel from the database 
                IEnumerable<AlbumItem> results = from x in context.Albums
                                                    select new AlbumItem
                                                    {
                                                        AlbumId = x.AlbumId,
                                                        Title = x.Title,
                                                        ReleaseYear = x.ReleaseYear,
                                                        ArtistId= x.ArtistId,
                                                        ReleaseLabel = x.ReleaseLabel
                                                    };
                return results.ToList();
            }
        }


        //query to lookup an album record by Pkey
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public AlbumItem Albums_FindByID(int albumid)
        {
            using (var context = new ChinookSystemContext())
            {
                //in 1517 when entity was public we stored to the entity 
                //using entiry framework .Find() and return context.DBsetName.find()...
                // now we must use a ViewModel 

                // .FirstOrDefault() / .First() - returns the first object, OR return a NULL value

                AlbumItem results = (from x in context.Albums
                              where x.AlbumId == albumid
                                                 select new AlbumItem
                                                 {
                                                     AlbumId = x.AlbumId,
                                                     Title = x.Title,
                                                     ReleaseYear = x.ReleaseYear,
                                                     ArtistId = x.ArtistId,
                                                     ReleaseLabel = x.ReleaseLabel
                                                 }).FirstOrDefault();
                //FirstOrDefault will return either A-the first record or B-a null value
                return results;//not .ToList() because of FirstOrDefault()
            }
        }



        #endregion

        #region Add, Update, Delete

        //ADD
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Album_Add(AlbumItem item)
        {
            using (var context = new ChinookSystemContext())
            {
                //"item" is viewmodel object => to Entity object before update!
                Album additem = new Album
                {
                    //load fast - no PK because KEY is DB given
                    Title = item.Title,
                    ArtistId = item.ArtistId,
                    ReleaseYear = item.ReleaseYear,
                    ReleaseLabel = item.ReleaseLabel
                };

                //staging in local mem 
                //at this point code will NOT have send to Database.
                //Therefore will not have the new PKey yet
                context.Albums.Add(additem);


                //commit to Database - on this command the item is shipped to entity deffinition validation then to context DB
                context.SaveChanges();
                //entity instance will have new Pkey attached to the object 

                return additem.AlbumId;

            }
        }

        //UPDATE
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Album_Update(AlbumItem item)
        {
            using (var context = new ChinookSystemContext())
            {
                //"item" is viewmodel object => to Entity object before update!
                Album updateitem = new Album
                {
                    AlbumId = item.AlbumId,
                    Title = item.Title,
                    ArtistId = item.ArtistId,
                    ReleaseYear = item.ReleaseYear,
                    ReleaseLabel = item.ReleaseLabel
                };

                //staging in local mem 
                //at this point code will NOT have send to Database.
 
                context.Entry(updateitem).State = System.Data.Entity.EntityState.Modified;


                //commit to Database - on this command the item is shipped to entity deffinition validation then to context DB
                context.SaveChanges();
                //entity instance will have new Pkey attached to the object 

            }
        }


        //DELETE
        //when we do an ODS CRUD on the delete, the ODS Sends in the ENTIRE instance record not just the PKvalue
        //overload the Album_Delete method so it recieves a whole instance then call the actual delete method

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Album_Delete(AlbumItem albumitem)
        {
            Album_Delete(albumitem.AlbumId);
        }

        public void Album_Delete (int itemid)
        {
            using (var context = new ChinookSystemContext())
            {
                //retreive the current entity instance based on the incoming param.
                var exists = context.Albums.Find(itemid);
                //stage removal
                context.Albums.Remove(exists);
                //commit
                context.SaveChanges();

            }
        }

        #endregion
    }
}
