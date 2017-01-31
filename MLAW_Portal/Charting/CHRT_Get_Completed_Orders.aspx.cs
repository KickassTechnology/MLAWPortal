using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System.Charting
{
    public partial class CHRT_Get_Completed_Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int iChartType = Convert.ToInt32(Request["Chart_Type"]);
            DateTime dtStartDate = Convert.ToDateTime(Request["Start_Date"]);
            DateTime dtEndDate = Convert.ToDateTime(Request["End_Date"]);


            DAL dal = new DAL();
            DataSet ds = dal.getCHRTCompletedOrders(dtStartDate, dtEndDate);

            String strResponse = "";

            if (iChartType == 1)
            {

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


            }
            else
            {
                String strCurClient = "";
                strResponse = "[";
                int iTotal = 0;   

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (strCurClient != dr["Client_Short_Name"].ToString())
                    {
                        if (strCurClient != "")
                        {

                            strResponse = strResponse + "{\"letter\":\"" + strCurClient + "\",	\"frequency\":" + iTotal.ToString() + "},";
                        }
                        strCurClient = dr["Client_Short_Name"].ToString();
                        iTotal = 0;

                    }
                    iTotal = iTotal + Convert.ToInt32(dr[3]);
                }

                strResponse = strResponse + "{\"letter\":\"" + strCurClient + "\",	\"frequency\":" + iTotal.ToString() + "}]";

            }
            Response.Write(strResponse);
        }
    }
}