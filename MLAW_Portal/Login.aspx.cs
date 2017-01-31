using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                DAL dal = new DAL();
                DataSet dsUser = dal.doLogin(login_username.Value, login_password.Value);

                if (dsUser.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsUser.Tables[0].Rows[0];
                    Session["User_Level_Id"] = Convert.ToInt32(dr["User_Level_Id"]);
                    Session["User_Id"] = Convert.ToInt32(dr["User_Id"]);
                    if (dr["Client_Id"].ToString() != "")
                    {
                        Session["Client_Id"] = Convert.ToInt32(dr["Client_Id"]);
                    }

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
                    else
                    {
                        Response.Redirect("Dashboard.aspx");
                    }
                }
                else
                {
                    dsUser = dal.doCustomerLogin(login_username.Value, login_password.Value);
                    if (dsUser.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = dsUser.Tables[0].Rows[0];
                        Session["Client_Id"] = Convert.ToInt32(dr["Client_Id"]);
                        Response.Redirect("/CustomerPortal/Customer_Default.aspx");
                  
                    }
                    else
                    {
                        error_msg.Text = "We did not find a user that matched this username and password <br />";
                    }
                }
            }
        }


        protected void login_Click(object sender, EventArgs e)
        {
            DAL dal = new DAL();
            DataSet dsUser = dal.doLogin(login_username.Value, login_password.Value);

            if (dsUser.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsUser.Tables[0].Rows[0];
                Session["User_Level_Id"] = Convert.ToInt32(dr["User_Level_Id"]);
                Session["User_Id"] = Convert.ToInt32(dr["User_Id"]);
                if (dr["Client_Id"].ToString() != "")
                {
                    Session["Client_Id"] = Convert.ToInt32(dr["Client_Id"]);
                }

                if (Session["User_Level_Id"].ToString() == "3")
                {
                    Response.Redirect("CustomerPortal/Customer_Default.aspx");
                }
                else if (Session["User_Level_Id"].ToString() == "4")
                {
                    Response.Redirect("DesignerPortal/Designer_Default.aspx");
                }
                else
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
            else
            {
                error_msg.Text = "We did not find a user that matched this username and password <br />";
            }
        }
    }
}