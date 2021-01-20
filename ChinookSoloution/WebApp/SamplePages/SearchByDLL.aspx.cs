using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#region additional namespaces
using ChinookSystem.BLL; //controlllers
using ChinookSystem.ViewModels; //models
#endregion
namespace WebApp.SamplePages
{
    public partial class SearchByDLL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //first page load

                LoadArtistList();
            }

        }

        #region error handling ODS
        //link to ODS
        protected void SelectCheckForException(object sender, ObjectDataSourceStatusEventArgs evnt)
        {
            MessageUserControl.HandleDataBoundException(evnt);
        }

        #endregion

        protected void LoadArtistList()
        {
            ArtistController sysmgr = new ArtistController(); //new instance
            List<SelectionList> info = sysmgr.Artist_DDLList(); //fill new list by call to query from new instance

            //assume data collection needs to be sorted - could be done in BLL
            // X and Y => do the following
            info.Sort((x,y) => x.DisplayField.CompareTo(y.DisplayField)); //flip X and Y for opposite order

            //setup the DDL 
            ArtistList.DataSource = info;//attach data to object
            //ArtistList.DataTextField = "DisplayField";
            ArtistList.DataTextField = nameof(SelectionList.DisplayField); //info holds objects and selectionlist holds deffinitions
            ArtistList.DataValueField = nameof(SelectionList.ValueField);
            ArtistList.DataBind();//push into controller


            //prompt line
            ArtistList.Items.Insert(0, new ListItem("select...", "0")); //insert new item to DDL list

        }

        protected void SearchAlbumsBTN_Click(object sender, EventArgs e)
        {
            if(ArtistList.SelectedIndex == 0)
            {
                //index 0 physiclaly points to the prompt line
                // "select an artist";

                //using MessageUserControl for your own message..
                MessageUserControl.ShowInfo("Search Concern","select an artist");

                
                ArtistAlbumList.DataSource = null;
                ArtistAlbumList.DataBind();

            }
            else 
            {
                //normally when leaving web page to class library you will want try-catch error handling
                //use MessageUserControll to handle errors 
                //  MessageUserControll has try-catch embedded inside its logic
                MessageUserControl.TryRun(() => {
                    //bind to grid
                    AlbumController sysmgr = new AlbumController(); //new instance
                                                                    //make a controller request to list object to use fot populating the Gridview
                    List<ChinookSystem.ViewModels.ArtistAlbums> info = sysmgr.Albums_GetAlbumsForArtist(int.Parse(ArtistList.SelectedValue)); //fully qualify name to avoid descrepancies 

                    //bind list object to grid 
                    ArtistAlbumList.DataSource = info;
                    ArtistAlbumList.DataBind();
                }, "success message title", "success message goes here");


            }
        }
    }
}