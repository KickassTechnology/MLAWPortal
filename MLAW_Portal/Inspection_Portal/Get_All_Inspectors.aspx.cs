using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System.Inspection_Portal
{
    public partial class Get_All_Inspectors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DAL dal = new DAL();
            DataSet dsInspectors = dal.getAllInspectors();

            String strJSON = "[";
            int iCount = 0;
            foreach (DataRow dr in dsInspectors.Tables[0].Rows)
            {
                if (iCount != 0)
                {
                    strJSON = strJSON + ",";
                }
                iCount = iCount + 1;
                strJSON = strJSON + "{\"User_Id\":" + dr["User_Id"].ToString() + ", \"First_Name\":\"" + dr["First_Name"].ToString() + "\", \"Last_Name\":\"" + dr["Last_Name"].ToString() + "\"}";
            }

            strJSON = strJSON + "]";

            Response.Write(strJSON);

        }
    }
}