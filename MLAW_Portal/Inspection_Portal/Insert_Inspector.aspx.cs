using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System.Inspection_Portal
{
    public partial class Insert_Inspector : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strFirstName = Request["FirstName"].ToString();
            String strLastName = Request["LastName"].ToString();
            String strEmail = Request["Email"].ToString();
            String strPassword = Request["Password"].ToString();

            DAL dal = new DAL();
            
            DataSet dsUser = dal.insertUser(strFirstName, strLastName, strEmail, strPassword, 8, 0);
            int iInspector_Id = Convert.ToInt32(dsUser.Tables[0].Rows[0][0]);
            String strGeolocation = Request["Geolocation"].ToString();
            String[] strInspectionTypes = Request["Inspection_Types"].ToString().Split(',');

            dal.deleteInspectorInspectionTypes(iInspector_Id);

            foreach (String strIns in strInspectionTypes)
            {
                dal.insertInspectorInsType(iInspector_Id, Convert.ToInt32(strIns));
            }

            dal.upsertInspectorInfo(iInspector_Id, strGeolocation);
        }
    }
}