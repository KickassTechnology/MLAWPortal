using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System
{
    public partial class Update_Client_Portal_Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //updates the username and password for a login to the client portal
            int iClientId = Convert.ToInt32(Request["client_id"]);
            String strUsername = Request["username"].ToString();
            String strPassword = Request["password"].ToString();

            DAL dal = new DAL();
            dal.updateClientPortalLogin(iClientId, strUsername, strPassword);

            Response.Write("1");
            
        }
    }
}