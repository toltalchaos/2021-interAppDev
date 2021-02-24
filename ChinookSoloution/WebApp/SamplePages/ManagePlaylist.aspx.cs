﻿using System;
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
            
                //code to go here

          }


        protected void GenreFetch_Click(object sender, EventArgs e)
        {

                //code to go here

        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {

                //code to go here

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