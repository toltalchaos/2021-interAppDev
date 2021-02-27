using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;

#endregion

namespace WebApp.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
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
            //code to go here
 
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track
 
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            //code to go here
            
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