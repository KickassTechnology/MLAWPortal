using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System.Charting
{
    public partial class CHRT_designer_open_orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            DAL dal = new DAL();
            DataSet ds = dal.getChrtDesignerOpenOrders();

            String strResponse = "";
            String strCurClient = "";
            String strCurSubdivision = "";
            String strCurDesigner = "";

            strResponse = "{\"name\": \"orders\",\"children\": [";

            bool hasComma = false;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (strCurDesigner != dr["Full_Name"].ToString())
                {
                    if (strCurDesigner != "")
                    {
                        strResponse = strResponse + "]}]}]},";
                    }
                    strCurDesigner = dr["Full_Name"].ToString();

                    strResponse = strResponse + "{\"name\": \"" + strCurDesigner + "\",\"children\": [";
                    hasComma = false;
                    strCurSubdivision = "";
                    strCurClient = "";

                }

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

                if (strCurSubdivision != dr["Subdivision_Name"].ToString())
                {
                    if (strCurSubdivision != "")
                    {
                        strResponse = strResponse + "]},";
                    }
                    strCurSubdivision = dr["Subdivision_Name"].ToString();

                    strResponse = strResponse + "{\"name\": \"" + strCurSubdivision + "\",\"children\": [";
                    hasComma = false;
                }

                if (hasComma == true)
                {
                    strResponse = strResponse + ",";
                }

                strResponse = strResponse + "{\"name\": \"" + dr[4].ToString() + " " + dr["Order_Status_Desc"].ToString() + "\", \"size\":" + dr[4].ToString() + "}";
                hasComma = true;
            }

            strResponse = strResponse + "]}";
            strResponse = strResponse + "]}";
            strResponse = strResponse + "]}";
            strResponse = strResponse + "]}";
            Response.Write(strResponse);

        }
    }
}