using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System
{
    public partial class output_excel_accounting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            String strStartDate = Request["Start_Date"].ToString();
            String strEndDate = Request["End_Date"].ToString();

            int iDeliveryType = Convert.ToInt32(Request["Delivery_Type"]);

            DAL dal = new DAL();

            String strResponse = "";
            String strName = "";

            if (iDeliveryType == 1)
            {
                strName = "invoiced_";
                DataSet ds = dal.getOrderSlice("11", 5, strStartDate, strEndDate);
                DataTable dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Invoice_Number"].ToString().Trim() != "")
                    {

                            strResponse = strResponse + "\"" + dr["MLAW_Number"].ToString() + "\",";
                            strResponse = strResponse + "\"" + dr["Amount"].ToString() + "\",";
                            strResponse = strResponse + "\"" + dr["Invoiced_Date_String"].ToString() + "\",";
                            strResponse = strResponse + "\"" + dr["Invoice_Number"].ToString() + "\",";
                            strResponse = strResponse + "\"F\",";
                            strResponse = strResponse + "\"4010\"" + System.Environment.NewLine;
                        
                    }
                }
            }
            else if (iDeliveryType == 2)
            {
                strName = "inspections_invoiced";
                DataSet ds = dal.getInspectionOrderSlice(strStartDate,strEndDate,"","3"); 
                DataTable dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    Double dAmount = 0.00;
                    if (dr["Amount"].ToString() != "")
                    {
                        dAmount = Convert.ToDouble(dr["Amount"]);
                    }
                        strResponse = strResponse + "\"" + dr["MLAW_Number"].ToString() + "\",";
                        strResponse = strResponse + "\"" + dr["Client_Short_Name"].ToString() + "\",";
                        strResponse = strResponse + "\"" + dr["Order_Date_String"].ToString() + "\",";
                        strResponse = strResponse + "\"" + dr["Subdivision_Name"].ToString() + "\",";
                        strResponse = strResponse + "\"" + dr["Address"].ToString() + "\",";

                        
                        strResponse = strResponse + "\"" + dAmount.ToString("C2") + "\"" + System.Environment.NewLine;

                }
            }
            else if (iDeliveryType == 3)
            {

            }
            else
            {
                strName = "new_orders_";
                DataSet ds = dal.getOrderSlice("1,2,3,4", 1, strStartDate, strEndDate);
                DataTable dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    strResponse = strResponse + "\"" + dr["MLAW_Number"].ToString() + "\",";
                    strResponse = strResponse + "\"\",";
                    strResponse = strResponse + "\"\",";
                    strResponse = strResponse + "\"\",";
                    strResponse = strResponse + "\"\",";
                    strResponse = strResponse + "\"\",";
                    strResponse = strResponse + "\"\",";
                    strResponse = strResponse + "\"\",";
                    strResponse = strResponse + "\"\",";
                    strResponse = strResponse + "\"\",";
                    strResponse = strResponse + "\"\",";
                    strResponse = strResponse + "\"\",";
                    strResponse = strResponse + "\"" + dr["Address"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["Address"].ToString() + "\",";
                    strResponse = strResponse + "\"" + dr["Received_Date_String"].ToString() + "\"" + System.Environment.NewLine;
                }
            }

            strName = strName + strStartDate + "-" + strEndDate + ".csv";
            string attachment = "attachment; filename=" + strName;

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