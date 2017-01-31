using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System
{
    public partial class Remove_Client_Delivery_Mail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int iMailId = Convert.ToInt32(Request["Client_Mail_Id"]);
            DAL dal = new DAL();

            dal.removeClientMailDelivery(iMailId);

            Response.Write("1");

        }
    }
}