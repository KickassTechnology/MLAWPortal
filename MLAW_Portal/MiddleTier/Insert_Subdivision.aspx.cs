using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System
{
    public partial class Insert_Subdivision : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //inserts a subdivision into the database
            DAL dal = new DAL();
            DataSet ds = dal.insertSubdivision(Convert.ToInt32(Request["Client_Id"]), Convert.ToInt32(Request["Division_Id"]), Request["Division_Name"].ToString(), Convert.ToInt32(Request["Subdivision_Number"]));

            Response.Write(ds.Tables[0].Rows[0][0].ToString());
        }
    }
}