﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System
{
    public partial class Get_Client_Id_By_Name : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Gets a Client Id based on a client Name
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            DAL dal = new DAL();
            DataSet ds = dal.getClientByName(Request["Name"].ToString());
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