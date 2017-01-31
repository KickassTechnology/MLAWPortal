using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Xml.Linq;
using System.Data;

namespace MLAW_Order_System.Inspection_Portal
{
    public partial class Insert_Inspection_Order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 iOrderId = Convert.ToInt32(Request["Order_Id"]);
            Int32 iInspectionType = Convert.ToInt32(Request["Inspection_Type"]);
            Int32 iIsReInspection = Convert.ToInt32(Request["IsReInspection"]);
            Int32 iIsMultiPour = Convert.ToInt32(Request["IsMultiPour"]);
            String strPhone = Request["Phone"].ToString();
            Int32 iCanText = Convert.ToInt32(Request["Can_Text"]);
            String strSpecialNotes = Request["SpecialNotes"].ToString();
            String strEmail = Request["Email"].ToString();
            Int32 iTimePref = Convert.ToInt32(Request["TimePref"]);
            DAL dal = new DAL();
            DataSet ds = dal.getOrderInfo(iOrderId);

            var address = ds.Tables[0].Rows[0]["Address"].ToString() + ", " + ds.Tables[0].Rows[0]["City"].ToString() + ",Tx";
            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));

            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var xdoc = XDocument.Load(response.GetResponseStream());

            var result = xdoc.Element("GeocodeResponse").Element("result");
            var status = xdoc.Element("GeocodeResponse").Element("status");

            String strStatus = status + "";
            String strGEO = "";

            if (strStatus.IndexOf("OK") > -1)
            {
                var locationElement = result.Element("geometry").Element("location");
                var lat = locationElement.Element("lat");
                var lng = locationElement.Element("lng");
                
                strGEO = lat + "," + lng;
                strGEO = strGEO.Replace("<lat>", "").Replace("</lat>", "").Replace("<lng>", "").Replace("</lng>", ""); 
            }
            else
            {
                address = ds.Tables[0].Rows[0]["Subdivision_Name"].ToString() + ", " + ds.Tables[0].Rows[0]["City"].ToString() + ",Tx";
                requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));

                var request2 = WebRequest.Create(requestUri);
                var response2 = request2.GetResponse();
                var xdoc2 = XDocument.Load(response2.GetResponseStream());

                var result2 = xdoc2.Element("GeocodeResponse").Element("result");
                var status2 = xdoc2.Element("GeocodeResponse").Element("status");

                String strStatus2 = status2 + "";

                if (strStatus2.IndexOf("OK") > -1)
                {
                    var locationElement = result2.Element("geometry").Element("location");
                    var lat = locationElement.Element("lat");
                    var lng = locationElement.Element("lng");

                    strGEO = lat + "," + lng;
                    strGEO = strGEO.Replace("<lat>", "").Replace("</lat>", "").Replace("<lng>", "").Replace("</lng>", "");
                }
                else
                {

                }
            }

            //If the address is no good, use the subdivision



            

            dal.insertInspectionOrder(iOrderId, iInspectionType, iIsReInspection, iIsMultiPour, strPhone, iCanText, strSpecialNotes, strGEO, strEmail, iTimePref);

            //Response.Write(lat + "," + lng);
        }


    }
}