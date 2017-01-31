using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System.DesignerPortal
{
    public partial class Designer_Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JS.Text = "<script>var current_user_id=" + Session["User_Id"].ToString() + ";</script>";
        }
    }
}