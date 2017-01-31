using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System
{
    public partial class Get_All_inspection_Types : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Returns a list of all the inspection Types
            DAL dal = new DAL();
            DataSet dsInspectionTypes = dal.getAllInspectionTypes();

            String strJSON = "[";
            int iCount = 0;
            foreach (DataRow dr in dsInspectionTypes.Tables[0].Rows)
            {
                if (iCount != 0)
                {
                    strJSON = strJSON + ",";
                }
                iCount = iCount + 1;
                strJSON = strJSON + "{\"Inspection_Type_Id\":" + dr["Inspection_Type_Id"].ToString() + ", \"Inspection_Type\":\"" + dr["Inspection_Type"].ToString() + "\", \"Completion_Time\":" + dr["Completion_Time"].ToString() + "}";
            }

            strJSON = strJSON + "]";

            Response.Write(strJSON);

        }
    }
}