using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System
{
    public partial class Update_Warranty_Order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DAL dal = new DAL();
            int iWarrantyOrderId = Convert.ToInt32(Request["Warranty_Order_Id"]);
            String strWarrantyNotes = Request["Warranty_Notes"].ToString();

            dal.updateWarrantyOrder(iWarrantyOrderId, strWarrantyNotes);

            Response.Write("1");
        }
    }
}