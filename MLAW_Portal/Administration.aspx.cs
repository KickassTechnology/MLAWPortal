using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System
{
    public partial class Administration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Level_Id"] == null)
            {
                Response.Redirect("login.aspx");
            }

            if (!Page.IsPostBack)
            {
                DAL dal = new DAL();
                DataSet dsUserLevels = dal.getUserLevels();

                User_Level_Id.DataSource = dsUserLevels.Tables[0];
                User_Level_Id.DataTextField = "User_Level_Desc";
                User_Level_Id.DataValueField = "User_Level_Id";
                User_Level_Id.DataBind();

                DataSet dsCustomers = dal.getAllClients();
                Customer_List.DataSource = dsCustomers.Tables[0];
                Customer_List.DataTextField = "Client_Short_Name";
                Customer_List.DataValueField = "Client_Id";
                Customer_List.DataBind();
                
                Customer_List.Items.Insert(0, new ListItem(String.Empty, "0"));
                Customer_List.SelectedIndex = 0;

                loadUserList();
            }
        }

        public void loadUserList()
        {
            DAL dal = new DAL();
            DataSet dsUsers = dal.getAllUsers();
            String strHTML = "";

            strHTML = strHTML + "<div style='float:left;width:180px;font-weight:bold;'>Name</div>";
            strHTML = strHTML + "<div style='float:left;width:300px;font-weight:bold;'>Email</div>";
            strHTML = strHTML + "<div style='float:left;width:220px;font-weight:bold;'>User Type</div>";
            strHTML = strHTML + "<div style='float:left;width:120px;font-weight:bold;'>Customer</div>";
            strHTML = strHTML + "<div style='clear:both'></div>";

            foreach (DataRow dr in dsUsers.Tables[0].Rows)
            {
                strHTML = strHTML + "<div style='float:left;width:180px;'>" + dr["First_Name"].ToString() + " " + dr["Last_Name"].ToString() + "</div>";
                strHTML = strHTML + "<div style='float:left;width:300px;'>" + dr["Email"].ToString() + "</div>";
                strHTML = strHTML + "<div style='float:left;width:220px;'>" + dr["User_Level_Desc"].ToString() + "</div>";
                strHTML = strHTML + "<div style='float:left;width:120px;'>" + dr["Client_Short_Name"].ToString() + "</div>";
                strHTML = strHTML + "<div style='clear:both'></div>";
            }

            user_list.InnerHtml = strHTML;
        }

        protected void add_user_Click(object sender, EventArgs e)
        {
            
            DAL dal = new DAL();
            String strFirstName = First_Name.Text;
            String strLastName = Last_Name.Text;
            String strEmail = Email.Text;
            String strPassword = Password.Text;
            int iUserLevel = Convert.ToInt32(User_Level_Id.SelectedValue);
            int iClient_Id = 0;

            if (Customer_List.SelectedIndex > -1)
            {
                iClient_Id = Convert.ToInt32(Customer_List.SelectedValue);
            }

            dal.insertUser(strFirstName, strLastName, strEmail, strPassword, iUserLevel, iClient_Id);

            loadUserList();

        }
    }
}