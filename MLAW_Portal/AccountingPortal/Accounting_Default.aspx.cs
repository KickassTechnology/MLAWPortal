using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System.AccountingPortal
{
    public partial class Accounting_Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Level_Id"] == null)
            {
                Response.Redirect("../login.aspx");
            }

        }
    }
}