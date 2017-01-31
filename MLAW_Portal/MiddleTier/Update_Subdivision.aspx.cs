using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System
{
    public partial class Update_Subdivision : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int iSubdivisionId = Convert.ToInt32(Request["Subdivision_Id"]);
            String strSubdivisionName = Request["Subdivision_Name"].ToString();
            int iSubdivisionNumber = Convert.ToInt32(Request["Subdivision_Number"]);
            int iDivisionId = Convert.ToInt32(Request["Division_Id"]);

            DAL dal = new DAL();
            dal.updateSubdivision(iSubdivisionId, strSubdivisionName, iSubdivisionNumber, iDivisionId);

            Response.Write("1");
        }
    }
}