﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System
{
    public partial class Get_Orders_By_Status : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Gets a list of foundation orders based on a list of statuses that are supplied
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            String strStatuses = Request["Statuses"].ToString();

            DAL dal = new DAL();
            DataSet ds = dal.getOrdersByStatus(strStatuses);
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