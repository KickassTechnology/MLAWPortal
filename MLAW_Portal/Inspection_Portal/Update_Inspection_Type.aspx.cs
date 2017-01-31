using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System.Inspection_Portal
{
    public partial class Update_Inspection_Type : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DAL dal = new DAL();
            int iInspectionTypeId = Convert.ToInt32(Request["Inspection_Type_Id"]);
            int iCompletionTime = Convert.ToInt32(Request["Completion_Time"]);

            dal.UpdateInspectionType(iInspectionTypeId, iCompletionTime);

        }
    }
}