using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace MLAW_Order_System
{
    public partial class Get_All_Subdivisions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Returns a list of all subdivisions
            DAL dal = new DAL();
            DataSet dsSubdivisions = dal.getAllSubdivisions();

            String strJSON = "[";
            int iCount = 0;
            foreach (DataRow dr in dsSubdivisions.Tables[0].Rows)
            {
                if (iCount != 0)
                {
                    strJSON = strJSON + ",";     
                }
                iCount = iCount + 1;
                strJSON = strJSON + "{\"Subdivision_Id\":" + dr["Subdivision_Id"].ToString() + ", \"Subdivision_Name\":\"" + dr["Subdivision_Name"].ToString() + "\", \"Client_Id\":" + dr["Client_Id"].ToString() + "}";
            }

            strJSON = strJSON + "]";

            Response.Write(strJSON);

        }
    }
}