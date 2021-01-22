using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.SamplePages
{
    public partial class ListViewODSCRUD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Message user Controll Error Handling For ODS
         protected void SelectCheckForException(object sender, ObjectDataSourceStatusEventArgs args)
        {
            MessageUserControl.HandleDataBoundException(args);
        }
        protected void InsertCheckForException(object sender, ObjectDataSourceStatusEventArgs args)
        {
            if(args.Exception == null)
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