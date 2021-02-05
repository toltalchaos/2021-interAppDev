using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.SamplePages
{
    public partial class ODS_repreater : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #region Message user Controll Error Handling For ODS
        protected void SelectCheckForException(object sender, ObjectDataSourceStatusEventArgs args)
        {
            MessageUserControl.HandleDataBoundException(args);
        }
       
        #endregion
    }
}