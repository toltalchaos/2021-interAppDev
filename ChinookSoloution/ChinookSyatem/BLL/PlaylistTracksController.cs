using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Entities;
using ChinookSystem.ViewModels;
using ChinookSystem.DAL;
using System.ComponentModel;
using FreeCode.Exceptions;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        //class level prop for error messaging 
        //needing freecodeexceptions for msgusercontroll

        List<Exception> brokenrules = new List<Exception>();

        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookSystemContext())
            {

                var results = from x in context.PlaylistTracks
                              where x.Playlist.Name == playlistname && x.Playlist.UserName == username
                              orderby x.TrackNumber
                              select new UserPlaylistTrack
                              {
                                  TrackID = x.TrackId,
                                  TrackNumber = x.TrackNumber,
                                  TrackName = x.Track.Name,
                                  Milliseconds = x.Track.Milliseconds,
                                  UnitPrice = x.Track.UnitPrice
                              };
                             

                return results.ToList();
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid, string song)
        {
            //early var placeholders
            Playlist playlistExist = null;
            PlaylistTrack playlistTrackExist = null;
            int tracknumber = 0 ;

            using (var context = new ChinookSystemContext())
            {
                //ensure data recieved - this class is in BLL and must enforce business rules
                //rules - no song repetition in playlists
                //      - playlist name must be unique to the user
                //      - each track on a playlist is assigned a continous track number -- track number id on playlist


                //BLL must ensure that data exists for processing transaction

                if (string.IsNullOrEmpty(playlistname))
                {
                    //no playlist name supplied -error out
                    //BusinessRuleException -- part of freecode -import
                    brokenrules.Add(new BusinessRuleException<string>("playlist name is missing. unable to add track", 
                                                                        nameof(playlistname), playlistname)); //nameof not requires if specific
                }
                if (string.IsNullOrEmpty(username))
                {
                    //no playlist name supplied -error out
                    //BusinessRuleException -- part of freecode -import
                    brokenrules.Add(new BusinessRuleException<string>("username name is missing. unable to add track",
                                                                        "username", username));
                }
                if (brokenrules.Count() == 0)
                {
                    //does the playlist already exist? -- return null or first -- should only be one
                    playlistExist = (from x in context.Playlists
                                    where x.Name == playlistname && x.UserName == username
                                    select x).FirstOrDefault();
                    if (playlistExist == null)
                    {
                        //new playlist
                        //tasks - create a new playlist class instance
                        // load instance with data
                        //stage the playlist instance 
                        // set track number - 1

                        playlistExist = new Playlist()
                        {
                             Name = playlistname,
                              UserName = username,

                        };
                        context.Playlists.Add(playlistExist); //staged in memory
                        tracknumber = 1;

                        
                    }
                    else
                    {
                        //existing playlist
                        //tasks - 
                        // does the track exist? - error if yes
                        // what is the last track number?
                        // insert track ++tracknumber

                        playlistTrackExist = (from x in context.PlaylistTracks
                                              where x.Playlist.Name == playlistname && x.Playlist.UserName == username && x.TrackId == trackid
                                              select x).FirstOrDefault(); //look for track
                        if (playlistTrackExist == null)
                        {
                            //track does not exist on desired playlist
                            //find highest track number and incriment 
                            tracknumber = (from x in context.PlaylistTracks
                                           where x.Playlist.Name == playlistname && x.Playlist.UserName == username
                                           select x.TrackNumber).Max();
                            tracknumber++;

                            //add track to playlist 

                        }
                        else
                        {
                            //business rule broken. track DOES exist on playlist.
                            brokenrules.Add(new BusinessRuleException<string>("Track already on playlist",
                                                                        nameof(song), song)); // showing the data causing error
                        }
                        



                    }
                           //create instance 
                        playlistTrackExist = new PlaylistTrack();
                        //load instance
                        playlistTrackExist.TrackId = trackid;
                        playlistTrackExist.TrackNumber = tracknumber;
                    //SQL CREATING PK 
                    //where new record is ONLY staged there is no PK value.
                    // if you access the new playlist record the PK would default of 0 <--- bad

                    //stage parent with track item
                    //entity framework will ensure that the adding of records to the database will be done in 
                    // the appropriate order and will add the missing compound PK value (playlistId) to the new playlist track record


                    //add instance
                    //THIS IS CORRECT, USING NAVIGATIONAL PROPERTIES to stage
                    playlistExist.PlaylistTracks.Add(playlistTrackExist);
                    
                        

                }

                //check again for errors up to this point
                if (brokenrules.Count() == 0)
                {
                    //do the commit
                    //all the staged records will be sent to SQL for processing 
                    context.SaveChanges();
                    //note COMPLETED transaction - 1 and only 1 .savechanges() 

                }
                // broken rules exist
                else 
                {
                    throw new BusinessRuleCollectionException("Adding playlist Track concerns", brokenrules); //title and exception list
                }
                




            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookSystemContext())
            {
                //code to go here 

            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
                //early var placeholders
                Playlist playlistExist = null;
                PlaylistTrack playlistTrackExist = null;
                int tracknumber = 0;
            using (var context = new ChinookSystemContext())
            {

                if (string.IsNullOrEmpty(playlistname))
                {
                    //no playlist name supplied -error out
                    //BusinessRuleException -- part of freecode -import
                    brokenrules.Add(new BusinessRuleException<string>("playlist name is missing. unable to delete track",
                                                                        nameof(playlistname), playlistname)); //nameof not requires if specific
                }
                if (string.IsNullOrEmpty(username))
                {
                    //no playlist name supplied -error out
                    //BusinessRuleException -- part of freecode -import
                    brokenrules.Add(new BusinessRuleException<string>("username name is missing. unable to delete track",
                                                                        "username", username));
                }
                if (trackstodelete.Count == 0)
                {
                    //no playlist name supplied -error out
                    //BusinessRuleException -- part of freecode -import
                    brokenrules.Add(new BusinessRuleException<string>("tracks to delete. unable to delete track",
                                                                        "track count", trackstodelete.Count.ToString()));
                }
                playlistExist = (from x in context.Playlists
                                 where x.Name == playlistname && x.UserName == username
                                 select x).FirstOrDefault();
                if (playlistExist == null)
                {
                    //broken rule
                    brokenrules.Add(new BusinessRuleException<string>("playlist no exist",
                                                                        nameof(playlistname), playlistname));
                }
                else
                {
                    //good data
                    //list all tracks that are to be kept
                    //compare lists of delete to list of kept
                    var trackskept = from x in context.PlaylistTracks
                                     where x.Playlist.Name == playlistname && x.Playlist.UserName == username && !trackstodelete.Any(tod => tod == x.TrackId)
                                     orderby x.TrackNumber
                                     select x;
                    //remove the desired tracks
                    PlaylistTrack item = null;

                    foreach(var deleterecord in trackstodelete)
                    {
                        item = (from x in context.PlaylistTracks
                               where x.Playlist.Name == playlistname && x.Playlist.UserName == username && x.TrackId == deleterecord
                               orderby x.TrackNumber
                               select x).FirstOrDefault();
                        if (item != null)
                        {
                            //delete

                            //stage via parent.nav.remove()
                            playlistExist.PlaylistTracks.Remove(item);

                        }
                        

                    }
                    //resequence the kept tracks



                }
            }
        }//eom
    }
}
