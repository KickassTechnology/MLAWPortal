using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System
{
    public partial class Assign_Order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //assign the order to the designer
            int iOrderId = Convert.ToInt32(Request["Order_Id"]);
            int iUserId = Convert.ToInt32(Request["User_Id"]);

            DAL dal = new DAL();

            dal.assignOrder(iOrderId, iUserId);

            Response.Write("1");
        }
    }
}