using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System.Inspection_Portal
{
    public partial class Update_Inspection_Order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int iOrderId = Convert.ToInt32(Request["Inspection_Order_Id"]);
            int iTypeId = Convert.ToInt32(Request["Type"]);
            int iOrderStatus = Convert.ToInt32(Request["Status"]);
            int iResult = Convert.ToInt32(Request["Result"]);
            string strResultNotes = Request["Result_Notes"].ToString();

            DAL dal = new DAL();
            dal.updateInspectionOrder(iOrderId, iOrderStatus, iTypeId, iResult, strResultNotes);

            Response.Write("1");

        }
    }
}