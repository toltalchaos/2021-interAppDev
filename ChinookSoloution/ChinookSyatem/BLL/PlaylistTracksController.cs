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
                        context.Playlists.Add(playlistExist);
                        tracknumber = 1;

                        
                    }
                    else
                    {
                        //existing playlist
                        //tasks - 
                        // does the track exist? - error if yes
                        // what is the last track number?
                        // insert track ++tracknumber



                    }


                }
                else // broken rules exist
                {

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
            using (var context = new ChinookSystemContext())
            {
               //code to go here


            }
        }//eom
    }
}
