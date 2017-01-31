using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System
{
    public partial class Update_For_Designer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int iOrderId = Convert.ToInt32(Request["Order_Id"]);
            int iStatus = Convert.ToInt32(Request["Status_Id"]);
            
            String strMLAWNum = Request["MLAW_Num"].ToString();
            String strComments = "";
            String strFile = "";

            if(iStatus == 6)
            {
                strComments = Request["Comments"].ToString();
            }

            if (iStatus == 7)
            {
                strComments = Request["Comments"].ToString();
            }

            if(iStatus == 8)
            {
                strFile = Request["File"].ToString();
            }

            DAL dal = new DAL();
            dal.updateDesignerStatus(iOrderId, iStatus, strComments);
            
            if (iStatus == 8)
            {
                dal.insertOrderFile(iOrderId, strFile);
            }

            Response.Write("1");
        }
    }
}