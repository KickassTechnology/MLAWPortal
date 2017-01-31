using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System
{
    public partial class orders_excel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Returns orders based on filters in a .csv format
            String strStatuses = Request["Statuses"].ToString();
            
            Int32 iDateType = Convert.ToInt32(Request["Date_Type"]);
            String strStartDate = Request["Start_Date"].ToString();
            String strEndDate = Request["End_Date"].ToString();

            if (strStartDate.Trim() == "")
            {
                strStartDate = "1/1/1900";
            }

            if(strEndDate.Trim() == "")
            {
                strEndDate = "1/1/2100";
            }

            DAL dal = new DAL();
            DataSet ds = dal.getOrderSlice(strStatuses, iDateType, strStartDate, strEndDate);
            DataTable dt = ds.Tables[0]; 

            String strResponse = "";
            strResponse = strResponse + "\"MLAW Number\",";
            strResponse = strResponse + "\"Client Name\",";
            strResponse = strResponse + "\"Client Short Name\",";
            strResponse = strResponse + "\"Subdivision\",";
            strResponse = strResponse + "\"Address\",";
            strResponse = strResponse + "\"Lot\",";
            strResponse = strResponse + "\"Block\",";
            strResponse = strResponse + "\"Section\",";
            strResponse = strResponse + "\"City\",";
            strResponse = strResponse + "\"Plan Name\",";
            strResponse = strResponse + "\"Order Status\",";
            strResponse = strResponse + "\"Received Date\",";
            strResponse = strResponse + "\"Delivered Date\",";
            strResponse = strResponse + "\"Fill Applied\",";
            strResponse = strResponse + "\"Soils Report\",";
            strResponse = strResponse + "\"Slope\",";
            strResponse = strResponse + "\"Elevation\",";
            strResponse = strResponse + "\"Amount\",";
            strResponse = strResponse + "\"Discount\",";
            strResponse = strResponse + "\"Total\"" + System.Environment.NewLine;

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Client_Short_Name"].ToString().ToLower().IndexOf(Request["Client"].ToString().ToLower()) != -1 &&
                    dr["Division_Desc"].ToString().ToLower().IndexOf(Request["Division"].ToString().ToLower()) != -1 &&
                    dr["Subdivision_Name"].ToString().ToLower().IndexOf(Request["Subdivision"].ToString().ToLower()) != -1 &&
                    dr["Order_Status_Desc"].ToString().ToLower().IndexOf(Request["Status"].ToString().ToLower()) != -1 &&
                    dr["Address"].ToString().ToLower().IndexOf(Request["Address"].ToString().ToLower()) != -1)
                {
                    strResponse = strResponse + "\"" + dr["MLAW_Number"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["Client_Short_Name"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["Client_Full_Name"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["Subdivision_Name"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["Address"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["Lot"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["Block"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["Section"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["City"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["Plan_Name"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["Order_Status_Desc"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["Received_Date_String"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["Delivered_Date_String"].ToString() + "\",";
                    if (dr["Fill_Applied"].ToString().Trim() == "1")
                    {
                        strResponse = strResponse + "\"YES\",";
                    }
                    else
                    {
                        strResponse = strResponse + "\"NO\",";
                    }
                    strResponse = strResponse + "\"" + dr["Soils_Data_Source"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["Slope"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["Elevation"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["Amount"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["Discount"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["Amount"].ToString() + "\"" + System.Environment.NewLine;
                }
            }

            string attachment = "attachment; filename=Open_Orders.csv";
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "text/csv";
            Response.AddHeader("Pragma", "public");
            Response.Write(strResponse);

        }
    }
}