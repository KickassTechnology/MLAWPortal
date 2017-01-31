using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System
{
    public partial class Insert_Revision : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DAL dal = new DAL();

            String strOrderFile = Request["Order_Files"].ToString();
            String strPlanNumber = Request["Plan_Number"].ToString();
            String strPlanName = Request["Plan_Name"].ToString();
            Double dAmount = Convert.ToDouble(Request["Amount"]);
                

            DateTime dtReceivedDate = DateTime.Now;
            DataSet ds = dal.insertRevision(Convert.ToInt32(Request["Order_Id"]), Request["Revision_Text"].ToString(), dtReceivedDate, strPlanName, strPlanNumber, dAmount);

            Int32 iOrderId = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

            string[] strFiles = strOrderFile.Split(';');
            foreach (string strFile in strFiles)
            {
                dal.insertOrderFile(iOrderId, strFile);
            }

            Response.Write("1");
        }
    }
}