using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System
{
    public partial class Update_Client_Is_Active : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //toggles whether a customer is active or inactive
            int iClientId = Convert.ToInt32(Request["Client_Id"]);
            int iStatus = Convert.ToInt32(Request["Is_Active"]);

            DAL dal = new DAL();
            dal.updateClientActiveStatus(iClientId, iStatus);
            

        }
    }
}