using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System
{
    public partial class Delete_Order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Allows the Foundation Admin to delete Orders
            int iOrderId = Convert.ToInt32(Request["Order_Id"]);

            DAL dal = new DAL();
            dal.deleteOrder(iOrderId);

            Response.Write("1");
        }
    }
}