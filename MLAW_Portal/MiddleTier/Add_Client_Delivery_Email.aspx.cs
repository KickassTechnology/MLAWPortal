using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System
{
    public partial class Add_Client_Delivery_Email : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int iClientId = Convert.ToInt32(Request["Client_Id"]);
            String strEmail = Request["Email"].ToString();

            DAL dal = new DAL();
            dal.insertClientDeliveryEmail(iClientId, strEmail);
            Response.Write("1");
        }
    }
}