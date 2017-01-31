using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System.Inspection_Portal
{
    public partial class Update_Inspector : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int iInspector_Id = Convert.ToInt32(Request["Inspector_Id"]);
            String strGeolocation = Request["Geolocation"].ToString();
            String[] strInspectionTypes = Request["Inspection_Types"].ToString().Split(',');
            String strEmail = Request["Email"].ToString();
            String strPassword = Request["Password"].ToString();
            String strFirstName = Request["FirstName"].ToString();
            String strLastName = Request["LastName"].ToString();
            int iActive = Convert.ToInt32(Request["Active"].ToString());

            DAL dal = new DAL();
            dal.deleteInspectorInspectionTypes(iInspector_Id);

            foreach (String strIns in strInspectionTypes)
            {
                dal.insertInspectorInsType(iInspector_Id, Convert.ToInt32(strIns));
            }

            dal.upsertInspectorInfo(iInspector_Id, strGeolocation);

            dal.updateUser(strEmail, 8, strPassword, strFirstName, strLastName, 0, iInspector_Id, iActive);

            Response.Write(iInspector_Id.ToString());
        }
    }
}