using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System
{
    public partial class Update_Client_Foundation_Revision_Pricing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Updates Revision Pricing Information
            int iClientId = Convert.ToInt32(Request["Client_Id"]);
            double dBase = Convert.ToDouble(Request["Base"]);
            double dNewHome = Convert.ToDouble(Request["New_Home"]);

            DAL dal = new DAL();
            dal.updateClientFoundationRevisionPricing(iClientId, dBase, dNewHome);

            Response.Write("1");


        }
    }
}