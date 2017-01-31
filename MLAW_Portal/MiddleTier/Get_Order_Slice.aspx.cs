using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System
{
    public partial class Get_Order_Slice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            String strStatuses = Request["Statuses"].ToString();
            Int32 iDateType = Convert.ToInt32(Request["Date_Type"]);
            String strStartDate = Request["Start_Date"].ToString();
            String strEndDate = Request["End_Date"].ToString();

            DAL dal = new DAL();
            DataSet ds = dal.getOrderSlice(strStatuses, iDateType, strStartDate, strEndDate);
            DataTable dt = ds.Tables[0];

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

        }
    }
}