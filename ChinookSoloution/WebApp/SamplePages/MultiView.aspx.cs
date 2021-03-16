using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.SamplePages
{
    public partial class MultiView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            //menu control
            //to switch views
            //grab value from E representing the new view to display
            int index = Int32.Parse(e.Item.Value);

            // assign the index to the active view index
            MultiView1.ActiveViewIndex = index;
        }

        protected void SendTo2From1_Click(object sender, EventArgs e)
        {
            //send data to next page 
            IODataV2.Text = IODataV1.Text;
            //go to next page 
            MultiView1.ActiveViewIndex = 1;
            //if altering page you should consider altering the menu button also
            Menu1.Items[1].Selected = true;
        }

        protected void SendTo3From1_Click(object sender, EventArgs e)
        {
            //send data to next page 
            IODataV3.Text = IODataV1.Text;
            //go to next page 
            MultiView1.ActiveViewIndex = 2;
            //if altering page you should consider altering the menu button also
            Menu1.Items[2].Selected = true;
        }

        protected void SendTo1From2_Click(object sender, EventArgs e)
        {

        }

        protected void SendTo3From2_Click(object sender, EventArgs e)
        {

        }

        protected void SendTo1From3_Click(object sender, EventArgs e)
        {

        }

        protected void SendTo2From3_Click(object sender, EventArgs e)
        {

        }
    }
}