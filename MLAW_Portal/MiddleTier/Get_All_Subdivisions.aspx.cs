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
            /*
            [{"Subdivision_Id":15964,"Subdivision_Name":"Chaparral Crossing","Subdivision_Number":1,"Client_Id":3,"Division_Id":1,"Division_Id1":1,"Division_Desc":"Austin","Division_Number":1},
            */
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