using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System
{
    public partial class Update_Revision : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //updates revision information
            int iOrderId = Convert.ToInt32(Request["Order_Id"]);
            int iOrderStatusId = Convert.ToInt32(Request["Order_Status_Id"]);
            int iDesignerId = 0;
            if (Request["Designer_Id"] != null)
            {
                Convert.ToInt32(Request["Designer_Id"]);
            }
            string strComments = Request["Comments"].ToString();

            DAL dal = new DAL();
            dal.updateRevision(iOrderId, iOrderStatusId, iDesignerId, strComments);
            Response.Write("1");

        }
    }
}