using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System
{
    public partial class Update_Order_Status : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Updates an order status
            DAL dal = new DAL();

            dal.updateFoundationOrderStatus(Convert.ToInt32(Request["Order_Id"]), Convert.ToInt32(Request["Status_Id"]));

            Response.Write("1");
        }
    }
}