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
                                           select x.TrackNumber).Count();
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
        public void MoveTrack(MoveTrackItem movetrack)
        {
            using (var context = new ChinookSystemContext())
            {
                //inital value errorchecking
                if (string.IsNullOrEmpty(movetrack.PlaylistName))
                {
                    //no playlist name supplied -error out
                    //BusinessRuleException -- part of freecode -import
                    brokenrules.Add(new BusinessRuleException<string>("playlist name is missing. unable to delete track",
                                                                        nameof(movetrack.PlaylistName), movetrack.PlaylistName)); //nameof not requires if specific
                }
                if (string.IsNullOrEmpty(movetrack.UserName))
                {
                    //no playlist name supplied -error out
                    //BusinessRuleException -- part of freecode -import
                    brokenrules.Add(new BusinessRuleException<string>("username name is missing. unable to delete track",
                                                                        "username", movetrack.UserName));
                }
                if (movetrack.TrackID <= 0)
                {
                    brokenrules.Add(new BusinessRuleException<string>("trackID no bueno",
                                                                        "trackID", movetrack.TrackID.ToString()));
                }
                if (movetrack.TrackNumber <= 0)
                {
                    brokenrules.Add(new BusinessRuleException<string>("trackID no bueno",
                                                                        "track Number", movetrack.TrackNumber.ToString()));
                }

                Playlist exist = (from x in context.Playlists
                                  where x.Name == movetrack.PlaylistName && x.UserName == movetrack.UserName
                                  select x).FirstOrDefault();
                if (exist == null)
                {
                    //broken rule
                    brokenrules.Add(new BusinessRuleException<string>("playlist no exist",
                                                                        nameof(movetrack.PlaylistName), movetrack.PlaylistName));
                }
                else
                {
                    //check if track exists on the database
                    PlaylistTrack trackexists = (from x in context.PlaylistTracks
                                                where 
                                                    x.Playlist.Name == movetrack.PlaylistName
                                                &&
                                                    x.Playlist.UserName == movetrack.UserName
                                                &&
                                                    x.TrackId == movetrack.TrackID

                                                select x).FirstOrDefault();
                    if (trackexists == null)
                    {
                        //broken rule
                        brokenrules.Add(new BusinessRuleException<string>("playlist track no exist",
                                                                            nameof(movetrack.TrackID), movetrack.TrackNumber.ToString())); //should lookup name.
                    }
                    else
                    {
                        //move it up or move it down
                        if (movetrack.Direction == "up")
                        {
                            //up
                            //check to see if at top - != 1
                            if (trackexists.TrackNumber != 1)
                            {
                                // do the move
                                //get adjacent track
                                PlaylistTrack othertrack = (from x in context.PlaylistTracks
                                                            where x.Playlist.Name == movetrack.PlaylistName
                                                            &&
                                                            x.Playlist.UserName == movetrack.UserName
                                                            &&
                                                            x.TrackNumber == trackexists.TrackNumber - 1
                                                            select x).FirstOrDefault();
                                //check other track exists 
                                if (othertrack != null)
                                {
                                    //good to swap
                                    //swap is a matter of changing the track Number Values
                                    trackexists.TrackNumber--;
                                    othertrack.TrackNumber++;
                                    //stage
                                    context.Entry(trackexists).Property(nameof(PlaylistTrack.TrackNumber)).IsModified = true;
                                    context.Entry(othertrack).Property(nameof(PlaylistTrack.TrackNumber)).IsModified = true;
                                }
                                else
                                {
                                    //broken rule
                                    brokenrules.Add(new BusinessRuleException<string>("track to swap is missing",
                                                                                        nameof(othertrack.Track.Name), othertrack.Track.Name));
                                }
                            }
                            else
                            {
                                //broken rule
                                brokenrules.Add(new BusinessRuleException<string>("playlist track already #1, refresh display",
                                                                                    nameof(movetrack.PlaylistName), trackexists.Track.Name));
                            }
                        }
                        else
                        {

                            int trackcountonexist = (from x in context.Playlists
                                                     where x.Name == movetrack.PlaylistName && x.UserName == movetrack.UserName
                                                     select x).FirstOrDefault().PlaylistTracks.ToList().Count();
                            trackcountonexist++; //lazy loader bullshi......
                            //down
                            //up
                            //check to see if at Bottom - != count of the existing playlist
                            if (trackexists.TrackNumber != trackcountonexist--)
                            {
                                // do the move
                                //get adjacent track
                                PlaylistTrack othertrack = (from x in context.PlaylistTracks
                                                            where x.Playlist.Name == movetrack.PlaylistName
                                                            &&
                                                            x.Playlist.UserName == movetrack.UserName
                                                            &&
                                                            x.TrackNumber == trackexists.TrackNumber + 1
                                                            select x).FirstOrDefault();
                                //check other track exists 
                                if (othertrack != null)
                                {
                                    //good to swap
                                    //swap is a matter of changing the track Number Values
                                    trackexists.TrackNumber++;
                                    othertrack.TrackNumber--;
                                    //stage
                                    context.Entry(trackexists).Property(nameof(PlaylistTrack.TrackNumber)).IsModified = true;
                                    context.Entry(othertrack).Property(nameof(PlaylistTrack.TrackNumber)).IsModified = true;
                                }
                                else
                                {
                                    //broken rule
                                    brokenrules.Add(new BusinessRuleException<string>("track to swap is missing",
                                                                                        nameof(othertrack.Track.Name), othertrack.Track.Name));
                                }
                            }
                            else
                            {
                                //broken rule
                                brokenrules.Add(new BusinessRuleException<string>("playlist track already #1, refresh display",
                                                                                    nameof(movetrack.PlaylistName), trackexists.Track.Name));
                            }

                        }
                    }

                }

                //resequence the kept tracks
                // option - use a list and update the records of the list 
                //option - delete all children records and re-add only the new kept records list
                //commit
                if (brokenrules.Count > 0)
                {
                    throw new BusinessRuleCollectionException("Track movement concerns", brokenrules);
                }
                else
                {
                    context.SaveChanges();
                }

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
                    //remove the desired track refrences
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
                    // option - use a list and update the records of the list 
                    //option - delete all children records and re-add only the new kept records list

                    tracknumber = 1;
                    foreach(var track in trackskept)
                    {
                        track.TrackNumber = tracknumber;
                        //stage
                        context.Entry(track).Property(nameof(PlaylistTrack.TrackNumber)).IsModified = true;

                        tracknumber++;
                    }
                    //commit
                    if(brokenrules.Count > 0)
                    {
                        throw new BusinessRuleCollectionException("Track removal concerns", brokenrules);
                    }
                    else
                    {
                        context.SaveChanges();
                    }


                }
            }
        }//eom
    }
}
