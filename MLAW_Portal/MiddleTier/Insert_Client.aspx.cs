using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System
{
    public partial class Insert_Client : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strShortName = Request["Short_Name"].ToString();
            String strFullName = Request["Full_Name"].ToString();
            String strAddress1 = Request["Billing_Address_1"].ToString();
            String strAddress2 = Request["Billing_Address_2"].ToString();
            String strCity = Request["Billing_City"].ToString();
            String strState = Request["Billing_State"].ToString();
            String strZip = Request["Billing_Zip"].ToString();
            String strPhone = Request["Phone"].ToString();
            String strFax = Request["Fax"].ToString();
            String strAttn = Request["Attn"].ToString();
            int iTurnAroundTime = Convert.ToInt32(Request["TurnAroundTime"]);
            int iPOFlag = Convert.ToInt32(Request["PO_Flag"]);
            int iInspectionFlag = Convert.ToInt32(Request["Inspection_Flag"]);
            String strVendorNumber = Request["Vendor_Number"].ToString();
            int iLowRange = 0;
            
            if(Request["Low_Range"].ToString().Trim() != "")
            {
                iLowRange = Convert.ToInt32(Request["Low_Range"]);
            }

            int iHighRange = 0;
            if (Request["High_Range"].ToString().Trim() != "")
            {
                iHighRange = Convert.ToInt32(Request["High_Range"]);
            }

            String strInvoiceNotes = Request["Invoice_Notes"].ToString();

            DAL dal = new DAL();


            dal.insertClient(Request["Short_Name"].ToString(), Request["Full_Name"].ToString(), Request["Billing_Address_1"].ToString(),
                Request["Billing_Address_2"].ToString(), Request["Billing_City"].ToString(), Request["Billing_State"].ToString(),
                Request["Billing_Zip"].ToString(), Request["Phone"].ToString(), Request["Fax"].ToString(), Request["Attn"].ToString(),
                Convert.ToInt32(Request["TurnAroundTime"]), Convert.ToInt32(Request["PO_Flag"]), Convert.ToInt32(Request["Inpection_Flag"]),
                Request["Vendor_Number"].ToString(), iLowRange, iHighRange, Request["Invoice_Notes"].ToString());



            Response.Write("1");
        }
    }
}