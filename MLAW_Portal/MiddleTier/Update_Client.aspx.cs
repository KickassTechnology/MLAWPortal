using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System
{
    public partial class Update_Client : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int iClientId = Convert.ToInt32(Request["Client_Id"]);
            String strClientShortName = Request["Client_Short_Name"].ToString();
            String strClientFullName = Request["Client_Full_Name"].ToString();
            String strBillingAddress1 = Request["Billing_Address_1"].ToString();
            String strBillingAddress2 = Request["Billing_Address_2"].ToString();
            String strBillingCity = Request["Billing_City"].ToString();
            String strBillingState = Request["Billing_State"].ToString();
            String strBillingZip = Request["Billing_Zip"].ToString();
            String strPhone = Request["Phone"].ToString();
            String strFax = Request["Fax"].ToString();
            String strAttn = Request["Attn"].ToString();
            String strVendorNumber = Request["Vendor_Number"].ToString();
            int iVPO = Convert.ToInt32(Request["VPO"]);

            DAL dal = new DAL();

            dal.updateClient(iClientId, strClientShortName, strClientFullName, strBillingAddress1, strBillingAddress2, strBillingCity, strBillingState, strBillingZip, strPhone, strFax, strAttn, strVendorNumber, iVPO);

            Response.Write("1");

        }
    }
}