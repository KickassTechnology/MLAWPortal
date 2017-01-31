using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System
{
    public partial class Insert_Client_Foundation_Pricing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int iClientId = Convert.ToInt32(Request["Client_Id"]);
            int iPricingType = Convert.ToInt32(Request["Pricing_Type"]);
            Double dAmount = Convert.ToDouble(Request["Amount"]);
            int iPricingTier = Convert.ToInt32(Request["Pricing_Tier"]);
            int iThreshold = Convert.ToInt32(Request["Threshold"]);

            DAL dal = new DAL();
            dal.insertClientFoundationPricing(iClientId, iPricingType, dAmount, iPricingTier, iThreshold);
        }
    }
}