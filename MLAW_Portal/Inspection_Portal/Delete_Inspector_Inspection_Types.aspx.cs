using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System.Inspection_Portal
{
    public partial class Delete_Inspector_Inspection_Types : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DAL dal = new DAL();
            int iInspectorId = Convert.ToInt32(Request["Inspector_Id"]);

            dal.deleteInspectorInspectionTypes(iInspectorId);


        }
    }
}