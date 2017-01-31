using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System
{
    public partial class Delete_Client_Foundation_Pricing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int iClientId = Convert.ToInt32(Request["Client_Id"]);
            DAL dal = new DAL();

            dal.deleteClientFoundationPricing(iClientId);

            Response.Write("1");
        }
    }
}