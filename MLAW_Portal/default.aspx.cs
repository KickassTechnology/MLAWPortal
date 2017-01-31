using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;
using System.Text;


namespace MLAW_Order_System
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Level_Id"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                if (Session["User_Level_Id"].ToString() == "3")
                {
                    Response.Redirect("CustomerPortal/Customer_Default.aspx");
                }
                else if (Session["User_Level_Id"].ToString() == "4")
                {
                    Response.Redirect("DesignerPortal/Designer_Default.aspx");
                }
                else if (Session["User_Level_Id"].ToString() == "6")
                {
                    Response.Redirect("FoundationQAPortal/QA_Default.aspx");
                }
                else if (Session["User_Level_Id"].ToString() == "7")
                {
                    Response.Redirect("DeliveryPortal/Delivery_Default.aspx");
                }
                else if (Session["User_Level_Id"].ToString() == "9")
                {
                    Response.Redirect("Inspection_Portal/Inspection_Default.aspx");
                }
                else if (Session["User_Level_Id"].ToString() == "10")
                {
                    Response.Redirect("AccountingPortal/Accounting_Default.aspx");
                }
            }
        }




    }
}