using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace MLAW_Order_System.CustomerPortal
{
    public partial class Get_Customer_Quick_Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strAddress = Request["Address"].ToString();
            Int32 iClient_Id = Convert.ToInt32(Session["Client_Id"]);


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            DAL dal = new DAL();
            DataSet ds = dal.getClientQuickSearch(strAddress,iClient_Id);
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