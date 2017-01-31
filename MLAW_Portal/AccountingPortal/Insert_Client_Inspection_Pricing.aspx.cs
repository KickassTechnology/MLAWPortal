using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System
{
    public partial class Insert_Client_Inspection_Pricing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int iClientId = Convert.ToInt32(Request["Client_Id"]);
            int iInspectionTypeId = Convert.ToInt32(Request["Inspection_Type_Id"]);
            Double dAmount = Convert.ToDouble(Request["Amount"]);
            Double dReinspAmount = Convert.ToDouble(Request["ReinspAmount"]);


            DAL dal = new DAL();
            dal.insertClientInspectionPricing(iClientId, iInspectionTypeId, dAmount, dReinspAmount);

            Response.Write("1");
        }
    }
}