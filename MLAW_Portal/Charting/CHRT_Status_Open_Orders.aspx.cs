using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System.Charting
{
    public partial class CHRT_Status_Open_Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DAL dal = new DAL();
            

            String strResponse = "";
            String strCurStatus = "";
            String strCurClient = "";
            String strCurSubdivision = "";
            bool hasComma = false;
            strResponse = "{\"name\": \"orders\",\"children\": [";
            
            DataSet ds = dal.getChrtOpenOrdersLate();
            
            if(ds.Tables[0].Rows.Count > 0)
            {
                
                strCurClient = "";
                strResponse = strResponse + "{\"name\": \"Late\",\"children\": [";
                     
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
               
                    if (strCurClient != dr["Client_Short_Name"].ToString())
                    {
                        if (strCurClient != "")
                        {
                            strResponse = strResponse + "]},";
                        }
                        strCurClient = dr["Client_Short_Name"].ToString();

                        strResponse = strResponse + "{\"name\": \"" + strCurClient + "\",\"children\": [";
                        hasComma = false;
                    }

                    if (hasComma == true)
                    {
                        strResponse = strResponse + ",";
                    }

                    strResponse = strResponse + "{\"name\": \"" + dr[3].ToString() + " " + dr["Subdivision_Name"].ToString() + "\", \"size\":" + dr[3].ToString() + "}";
                    hasComma = true;
                }
                strResponse = strResponse + "]}]}";
            }

            
            ds = dal.getChrtOpenOrdersDueToday();
            if (ds.Tables[0].Rows.Count > 0)
            {
                hasComma = false;
                strCurClient = "";
                strResponse = strResponse + ",{\"name\": \"Due Today\",\"children\": [";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    if (strCurClient != dr["Client_Short_Name"].ToString())
                    {
                        if (strCurClient != "")
                        {
                            strResponse = strResponse + "]},";
                        }
                        strCurClient = dr["Client_Short_Name"].ToString();

                        strResponse = strResponse + "{\"name\": \"" + strCurClient + "\",\"children\": [";
                        hasComma = false;
                    }

                    if (hasComma == true)
                    {
                        strResponse = strResponse + ",";
                    }

                    strResponse = strResponse + "{\"name\": \"" + dr[3].ToString() + " " + dr["Subdivision_Name"].ToString() + "\", \"size\":" + dr[3].ToString() + "}";
                    hasComma = true;
                }
                strResponse = strResponse + "]}]}";
            }
            
            ds = dal.getChrtOpenOrdersOnTime();
            if (ds.Tables[0].Rows.Count > 0)
            {
                hasComma = false;
                strCurClient = "";
                strResponse = strResponse + ",{\"name\": \"On Time\",\"children\": [";

                hasComma = false;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    if (strCurClient != dr["Client_Short_Name"].ToString())
                    {
                        if (strCurClient != "")
                        {
                            strResponse = strResponse + "]},";
                        }
                        strCurClient = dr["Client_Short_Name"].ToString();

                        strResponse = strResponse + "{\"name\": \"" + strCurClient + "\",\"children\": [";
                        hasComma = false;
                    }

                    if (hasComma == true)
                    {
                        strResponse = strResponse + ",";
                    }

                    strResponse = strResponse + "{\"name\": \"" + dr[3].ToString() + " " + dr["Subdivision_Name"].ToString() + "\", \"size\":" + dr[3].ToString() + "}";
                    hasComma = true;
                }
                strResponse = strResponse + "]}]}";
            }
            
            ds = dal.getChrtOpenOrdersHold();
            if (ds.Tables[0].Rows.Count > 0)
            {
                hasComma = false;
                strCurClient = "";
                strResponse = strResponse + ",{\"name\": \"On Hold\",\"children\": [";

                hasComma = false;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    if (strCurClient != dr["Client_Short_Name"].ToString())
                    {
                        if (strCurClient != "")
                        {
                            strResponse = strResponse + "]},";
                        }
                        strCurClient = dr["Client_Short_Name"].ToString();

                        strResponse = strResponse + "{\"name\": \"" + strCurClient + "\",\"children\": [";
                        hasComma = false;
                    }

                    if (hasComma == true)
                    {
                        strResponse = strResponse + ",";
                    }

                    strResponse = strResponse + "{\"name\": \"" + dr[3].ToString() + " " + dr["Subdivision_Name"].ToString() + "\", \"size\":" + dr[3].ToString() + "}";
                    hasComma = true;
                }
                strResponse = strResponse + "]}]}";
            }
            
            strResponse = strResponse + "]}";
            

            Response.Write(strResponse);

        }
    }
}