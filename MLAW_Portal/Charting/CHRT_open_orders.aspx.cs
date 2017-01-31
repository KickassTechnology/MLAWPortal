using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

namespace MLAW_Order_System
{
    public partial class CHRT_open_orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DAL dal = new DAL();
            DataSet ds = dal.getChrtOpenOrders();

            String strResponse = "";
            String strCurClient = "";
            String strCurSubdivision = "";

            strResponse = "{\"name\": \"orders\",\"children\": [";

            bool hasComma = false;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (strCurClient != dr["Client_Short_Name"].ToString())
                {
                    if (strCurClient != "")
                    {
                        strResponse = strResponse + "]}]},";
                    }
                    strCurClient = dr["Client_Short_Name"].ToString();

                    strResponse = strResponse + "{\"name\": \"" + strCurClient + "\",\"children\": [";
                    hasComma = false;
                    strCurSubdivision = "";

                }

                if(strCurSubdivision != dr["Subdivision_Name"].ToString())
                {
                    if (strCurSubdivision != "")
                    {
                        strResponse = strResponse + "]},";
                    }
                    strCurSubdivision = dr["Subdivision_Name"].ToString();

                    strResponse = strResponse + "{\"name\": \"" + strCurSubdivision + "\",\"children\": [";
                    hasComma = false;
                }
                
                if(hasComma == true)
                {
                    strResponse = strResponse + ",";
                }

                strResponse = strResponse + "{\"name\": \"" + dr[3].ToString() + " " + dr["Order_Status_Desc"].ToString() + "\", \"size\":" + dr[3].ToString() + "}";
                hasComma = true;
            }

            strResponse = strResponse + "]}";
            strResponse = strResponse + "]}";
            strResponse = strResponse + "]}";
            Response.Write(strResponse);


            
        }
    }
}