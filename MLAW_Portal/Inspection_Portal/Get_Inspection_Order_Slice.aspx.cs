using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System.Inspection_Portal
{
    public partial class Get_Inspection_Order_Slice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            */
            String strStatuses = Request["Statuses"].ToString();
            String strTypes = Request["Types"].ToString();
            String strStartDate = Request["StartDate"].ToString();
            String strEndDate = Request["EndDate"].ToString();

            DAL dal = new DAL();
            DataSet ds = dal.getInspectionOrderSlice(strStartDate, strEndDate, strTypes, strStatuses);
            DataTable dt = ds.Tables[0];
            /*
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            */

            String strCurClient = "";
            String strCurSubdivision = "";
            String strCurDesigner = "";

            String strResponse = "[";

            foreach (DataRow dr in dt.Rows)
            {
                if (strResponse != "[")
                {
                    strResponse = strResponse + ",";
                }

                strResponse = strResponse + "{";
                strResponse = strResponse + "\"Inspection_Order_Id\":" + dr["Inspection_Order_Id"].ToString() + ",";
                strResponse = strResponse + "\"Inspection_Type\":\"" + dr["Inspection_Type"].ToString() + "\",";
                strResponse = strResponse + "\"Inspection_Status\":\"" + dr["Inspection_Order_Status_Desc"].ToString() + "\",";
                strResponse = strResponse + "\"MLAW_Number\":\"" + dr["MLAW_Number"].ToString() + "\",";
                strResponse = strResponse + "\"Client_Short_Name\":\"" + dr["Client_Short_Name"].ToString() + "\",";
                strResponse = strResponse + "\"Subdivision_Name\":\"" + dr["Subdivision_Name"].ToString() + "\",";
                strResponse = strResponse + "\"Address\":\"" + dr["Address"].ToString() + "\",";
                strResponse = strResponse + "\"Order_Date\":\"" + dr["Order_Date_String"].ToString() + "\",";
                strResponse = strResponse + "\"Amount\":\"" + dr["Amount"].ToString() + "\",";
                strResponse = strResponse + "\"Discount\":\"" + dr["Discount"].ToString() + "\",";
                strResponse = strResponse + "\"Division\":\"" + dr["Division_Desc"].ToString() + "\"";
                strResponse = strResponse + "}";
            }

            strResponse = strResponse + "]";

            Response.Write(strResponse);

            /*
            Response.Write(serializer.Serialize(rows));
            */
        }
    }
}