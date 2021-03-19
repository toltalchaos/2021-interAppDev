using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
using WebApp.security;

#endregion

namespace WebApp.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null; //throws error but who cares

            //security for forms security
            //check to see if the user is logged on 
            if (Request.IsAuthenticated)
            {
                //user is logged in
                //Do you have the authority to be here?
                if (User.IsInRole(ConfigurationManager.AppSettings["customerRole"])) //same same because of web.config|| User.IsInRole("Customers")
                {
                    //authorized user
                    //obtain customer ID on the security user record
                    SecurityController ssysmgr = new SecurityController();
                    //pass the value of the userName to the method GetCurrentCustomerId
                    //returned is the customer ID as (int?)
                    int? customerId = ssysmgr.GetCurrentUserCustomerId(User.Identity.Name);
                    //need to convert the nullable int to a normal int for lookup to the CustomerController in my BLL
                    //int custID = customerId != null ? int.Parse(customerId.ToString()) : default(int);
                    int custID = customerId ?? default(int);

                    //use the custID to do the standard Customer Record lookup


                    LoggedUser.Text = User.Identity.Name;
                }
                else
                {
                    //not authorized
                    Response.Redirect("~/SamplePages/DeniedAccess.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        

        protected void ArtistFetch_Click(object sender, EventArgs e)
        {

            // click on fetch by artist logic 
            // bind information to the hidden labels
            TracksBy.Text = "Artist";
            if (string.IsNullOrEmpty(ArtistName.Text)) // .Value if using HiddenField
            {
                MessageUserControl.ShowInfo("oopsies","input some damn data in the Artist search Field!");
            }
            else
            {
                SearchArg.Text = ArtistName.Text;
            }
            //to force exicution to ODS attached to a control - re-bind the Display control

            TracksSelectionList.DataBind();

          }


        protected void GenreFetch_Click(object sender, EventArgs e)
        {

            // click on fetch by artist logic 
            // bind information to the hidden labels
            TracksBy.Text = GenreDDL.Text;
            if (string.IsNullOrEmpty(GenreDDL.SelectedValue)) // .Value if using HiddenField
            {
                MessageUserControl.ShowInfo("oopsies", "input some damn data in the Genre search Field!");
            }
            else
            {
                SearchArg.Text = GenreDDL.SelectedValue;
            }
            //to force exicution to ODS attached to a control - re-bind the Display control

            TracksSelectionList.DataBind();

        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {

            // click on fetch by artist logic 
            // bind information to the hidden labels
            TracksBy.Text = "Album";
            if (string.IsNullOrEmpty(AlbumTitle.Text)) // .Value if using HiddenField
            {
                MessageUserControl.ShowInfo("oopsies", "input some damn data in the Album search Field!");
            }
            else
            {
                SearchArg.Text = AlbumTitle.Text;
            }
            //to force exicution to ODS attached to a control - re-bind the Display control

            TracksSelectionList.DataBind();

        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            //temporarily hardcoding username 
            //until security is implamented

            string username = User.Identity.Name;

            if (string.IsNullOrEmpty(PlaylistName.Text))
            {

                MessageUserControl.ShowInfo("Playlist Search", "No playlist name provided");
            }
            else
            {
                //user friendly error handling
                MessageUserControl.TryRun(() => {

                    PlaylistTracksController sysmgr = new PlaylistTracksController();


                    //rebind playlist
                    RefreshPlaylist(sysmgr, username);


                }, "WEEEEE playlist", "that list looks great!"); //success message (optional)

            }

 
        }

        protected void RefreshPlaylist(PlaylistTracksController sysmgr, string username)
        {
            sysmgr = new PlaylistTracksController();

            List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);

            PlayList.DataSource = info;
            PlayList.DataBind();

        }


        protected void MoveDown_Click(object sender, EventArgs e)
        {
 
            //form event validation - presence
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("missing data", "enter a playlist name to playlist name field");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("track movement", "no rows to select from");
                }
                else
                {
                    //collect the selected tracks 
                    MoveTrackItem moveTrack = new MoveTrackItem();
                    int rowselected = 0;
                    CheckBox trackSelection = null;

                    //traverse the gridview controll playlist
                    //could use FOREACH here
                    for (int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        //point to the checkbox control on the gridview row
                        trackSelection = PlayList.Rows[i].FindControl("Selected") as CheckBox;

                        //Test the setting of the Checkbox
                        if (trackSelection.Checked)
                        {
                            rowselected++;
                            moveTrack.TrackID = int.Parse((trackSelection.FindControl("TrackId") as Label).Text);
                            moveTrack.TrackNumber = int.Parse((trackSelection.FindControl("TrackNumber") as Label).Text);

                        }

                    }
                    //was a single song selected?
                    switch (rowselected)
                    {
                        case 0:
                            {
                                MessageUserControl.ShowInfo("track movement", "no rows to selected"); 
                            break;
                            }
                        case 1:
                            {
                                if (moveTrack.TrackNumber == PlayList.Rows.Count)
                                {
                                    MessageUserControl.ShowInfo("track movement", "song is last selected");
                                }
                                else
                                {
                                    moveTrack.Direction = "down";
                                    MoveTrack(moveTrack);
                                }
                               
                            }
                            break;
                        default:
                            { 
                                MessageUserControl.ShowInfo("track movement", "too many rows selected, select one");
                             break;
                            }
                            
                    }
                }
            }

        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {

            //form event validation - presence
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("missing data", "enter a playlist name to playlist name field");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("track movement", "no rows to select from");
                }
                else
                {
                    //collect the selected tracks 
                    MoveTrackItem moveTrack = new MoveTrackItem();
                    int rowselected = 0;
                    CheckBox trackSelection = null;

                    //traverse the gridview controll playlist
                    //could use FOREACH here
                    for (int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        //point to the checkbox control on the gridview row
                        trackSelection = PlayList.Rows[i].FindControl("Selected") as CheckBox;

                        //Test the setting of the Checkbox
                        if (trackSelection.Checked)
                        {
                            rowselected++;
                            moveTrack.TrackID = int.Parse((trackSelection.FindControl("TrackId") as Label).Text);
                            moveTrack.TrackNumber = int.Parse((trackSelection.FindControl("TrackNumber") as Label).Text);

                        }

                    }
                    //was a single song selected?
                    switch (rowselected)
                    {
                        case 0:
                            {
                                MessageUserControl.ShowInfo("track movement", "no rows to selected");
                                break;
                            }
                        case 1:
                            {
                                if (moveTrack.TrackNumber == 1)
                                {
                                    MessageUserControl.ShowInfo("track movement", "song is first selected");
                                }
                                else
                                {
                                    moveTrack.Direction = "up";
                                    MoveTrack(moveTrack);
                                }

                            }
                            break;
                        default:
                            {
                                MessageUserControl.ShowInfo("track movement", "too many rows selected, select one");
                                break;
                            }

                    }
                }
            }
        }

        protected void MoveTrack(MoveTrackItem movetrack)
        {
            //call BLL to move track
            string username = "HansenB"; //until security
            movetrack.UserName = username;
            movetrack.PlaylistName = PlaylistName.Text;

            MessageUserControl.TryRun(()=> {

                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.MoveTrack(movetrack);
                RefreshPlaylist(sysmgr, username);

            }, "movement success","good job moving that little guy, it looks so good");



        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
            string username = "HansenB"; //until security

            //form event validation - presence
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("missing data", "enter a playlist name to playlist name field");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("track removal", "no rows to select from");
                }
                else
                {
                    //collect the selected tracks 
                    List<int> trackids = new List<int>();
                    int rowselected = 0;
                    CheckBox trackSelection = null;

                    //traverse the gridview controll playlist
                    //could use FOREACH here
                    for (int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        //point to the checkbox control on the gridview row
                        trackSelection = PlayList.Rows[i].FindControl("Selected") as CheckBox;

                        //Test the setting of the Checkbox
                        if (trackSelection.Checked)
                        {
                            rowselected++;
                            trackids.Add(int.Parse((PlayList.Rows[i].FindControl("TrackId") as Label).Text));
                        }
                        
                    }
                    //was a song selected?
                    if (rowselected == 0)
                    {
                        MessageUserControl.ShowInfo("missing data", "no selected tracks");
                    }
                    else
                    {
                        //data collected - send for processing 
                        MessageUserControl.TryRun(() =>
                        {
                            PlaylistTracksController sysmgr = new PlaylistTracksController();

                            sysmgr.DeleteTracks(username, PlaylistName.Text, trackids);

                            RefreshPlaylist( sysmgr, username);


                        },"track removal", "great success");
                    }
                }
            }


        }

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {//event on listview - command arg**
            string username = "HansenB"; //until security

            //form event validation - presence
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("missing data", "enter a playlist name to playlist name field");
            }
            else
            {
                //playlist exists and arg has happened
                //REMINDER  - Message usercontrol will do error throws

                MessageUserControl.TryRun(() => {
                    //logic for the code block 
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    // e argument coming from button object in list ... parse e object to string then to int

                    //access a specific field on selected listview row
                    string song = (e.Item.FindControl("NameLabel") as Label).Text;

                    sysmgr.Add_TrackToPLaylist(PlaylistName.Text, username, int.Parse(e.CommandArgument.ToString()), song);


                    //rebind playlist
                    RefreshPlaylist(sysmgr, username);



                }, "great success", "added track to playlist");


            }


        }
        #region Message user Controll Error Handling For ODS
        protected void SelectCheckForException(object sender, ObjectDataSourceStatusEventArgs args)
        {
            MessageUserControl.HandleDataBoundException(args);
        }
        protected void InsertCheckForException(object sender, ObjectDataSourceStatusEventArgs args)
        {
            if (args.Exception == null)
            {
                MessageUserControl.ShowInfo("process successful", "Album was successfully inserted");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(args);
            }
        }
        protected void UpdateCheckForException(object sender, ObjectDataSourceStatusEventArgs args)
        {
            if (args.Exception == null)
            {
                MessageUserControl.ShowInfo("process successful", "Album was successfully Updated");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(args);
            }
        }
        protected void DeleteCheckForException(object sender, ObjectDataSourceStatusEventArgs args)
        {
            if (args.Exception == null)
            {
                MessageUserControl.ShowInfo("process successful", "Album was successfully DELETED");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(args);
            }
        }
        #endregion
    }
}