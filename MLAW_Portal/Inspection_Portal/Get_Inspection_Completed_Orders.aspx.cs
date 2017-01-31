using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System.Inspection_Portal
{
    public partial class Get_Inspection_Completed_Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DAL dal = new DAL();
            
            DateTime dtStartDate = Convert.ToDateTime(Request["StartDate"]);
            DateTime dtEndDate = Convert.ToDateTime(Request["EndDate"]);

            DataSet ds = dal.getInspectionCompletedOrders(dtStartDate, dtEndDate);

            DataTable dt = ds.Tables[0];


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            Response.Write(serializer.Serialize(rows));
            /*
            String strCurrentDivision = "";
            String strCurrentSubdivision = "";
            String strCurrentClient = "";

            int iTotal = 0;
            int iDivisionTotal = 0;
            int iSubdivisionTotal = 0;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (strCurrentDivision != dr["Division_Desc"].ToString())
                {
                    if (strCurrentDivision != "")
                    {
                        Response.Write(strCurrentDivision + ":" + iDivisionTotal.ToString() + "<br>");
                    }
                    strCurrentDivision = dr["Division_Desc"].ToString();
                    iDivisionTotal = 0;
                }

                if (strCurrentClient != dr["Client_Short_Name"].ToString())
                {
                    if (strCurrentClient != "")
                    {
                        Response.Write(strCurrentClient + ":" + iTotal.ToString() + "<br>");
                        iTotal = 0;
                    }
                    strCurrentClient = dr["Client_Short_Name"].ToString();
                }

                if (strCurrentSubdivision != dr["Subdivision_Name"].ToString())
                {
                    if (strCurrentSubdivision != "")
                    {
                        Response.Write(strCurrentSubdivision + ":" + iSubdivisionTotal.ToString() + "<br>");
                    }
                    strCurrentSubdivision = dr["Subdivision_Name"].ToString();
                    iSubdivisionTotal = 0;
                }


                iTotal = iTotal + Convert.ToInt32(dr["Count"]);
                iSubdivisionTotal = iSubdivisionTotal + Convert.ToInt32(dr["Count"]);
                iDivisionTotal = iDivisionTotal + Convert.ToInt32(dr["Count"]);

            }
             */ 
        }
    }
}