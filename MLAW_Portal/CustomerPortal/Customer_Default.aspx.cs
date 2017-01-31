using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System.CustomerPortal
{
    public partial class Customer_Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId.Text = "<script> var Client_Id=" + Session["Client_Id"].ToString() + ";</script>";
        }
    }
}