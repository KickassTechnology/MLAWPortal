using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Xml.Linq;
using System.IO;

namespace MLAW_Order_System.Inspection_Portal
{
    public partial class Route_Calculation : System.Web.UI.Page
    {
        public class Route_Info
        {
            public Int32 iUserId;
            public List<string> arrLocations;
        }

        public class LocationMap
        {
            public Int32 iLocationId;
            public List<Int32> arrDistances;
        }

        public class MatchResponse
        {
            public Int32 iLocationId;
            public Int32 iDuration;
        }

        public ArrayList arrRoutes;
        public ArrayList arrMapLocations;
        public ArrayList arrLocationGrid;
        public ArrayList arrCurRoutes;

        protected void Page_Load(object sender, EventArgs e)
        {
            //SETUP OUR VARIABLES
            arrMapLocations = new ArrayList();
            arrRoutes = new ArrayList();
            arrLocationGrid = new ArrayList();
            

            DAL dal = new DAL();
            DataSet dsInspectors = dal.getAllInspectors();
            
            //SETUP OUR ROUTES
            foreach (DataRow dr in dsInspectors.Tables[0].Rows)
            {
                List<string> strLoc = new List<string> {dr["Inspector_Home_Geolocation"].ToString()};
                
                Route_Info ri = new Route_Info();
                ri.iUserId = Convert.ToInt32(dr["User_Id"]);
                ri.arrLocations = strLoc;
                arrRoutes.Add(ri);
                
                bool bAdd = true;

                foreach (string strLocation in arrMapLocations)
                {
                    if (strLocation == dr["Inspector_Home_Geolocation"].ToString())
                    {
                        bAdd = false;
                    }
                }

                if (bAdd == true)
                {
                    arrMapLocations.Add(dr["Inspector_Home_Geolocation"].ToString());
                }
            }

            //GET TODAY'S ORDERS
            DataSet dsOrders = dal.getTodaysOrders();
            
            //CREATE OUR 'DESTINATIONS' STRING
            String strDestinations = "";
            foreach (DataRow dr in dsOrders.Tables[0].Rows)
            {
                if(strDestinations == "")
                {
                    strDestinations = dr["Address_Geocode"].ToString();
                }else{
                    strDestinations = strDestinations + "|" + dr["Address_Geocode"].ToString();
                }
                arrMapLocations.Add(dr["Address_Geocode"].ToString());
            }

            //GET THE DISTANCE FOR ALL OF THE GEOLOCATIONS TO EACH OTHER
            int iCount = 0;
            foreach (string strLoc in arrMapLocations)
            {
                LocationMap lm = new LocationMap();
                lm.iLocationId = iCount;
                lm.arrDistances = new List<Int32>();

                String strURL = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + strLoc + "&destinations=" + strDestinations + "&mode=bicycling&language=en-EN&key=AIzaSyAdK4V1d-ZqO9Bf6jFe-N0rtJd3Dd9WnPY";
                WebRequest request = WebRequest.Create(strURL);
                WebResponse response = request.GetResponse();
                XDocument xdoc = XDocument.Load(response.GetResponseStream());

                Response.Write("<br><br><br>");
                Response.Write(xdoc);
                Response.Write("<br><br><br>");
                
                foreach (XElement el in xdoc.Descendants("duration"))
                {
                   lm.arrDistances.Add( Convert.ToInt32(el.Element("value").Value) );
                }

                arrLocationGrid.Add(lm);
                iCount = iCount + 1;
            }

            //CREATE OUR FIRST ROUTE

            //GET THE NEAREST POINT TO THE FIRST ELEMENT
            Int32 iTotalDuration = 0;
            Int32 iCurrentLocation = 0;

            while (iTotalDuration < 36000)
            {
                MatchResponse mr = getNextLocation(iCurrentLocation);

                Response.Write(mr.iLocationId.ToString() + ":" + mr.iDuration.ToString() + "<br>");
                iCurrentLocation = mr.iLocationId;
                iTotalDuration = iTotalDuration + mr.iDuration;
            }
            
        }

        public MatchResponse getNextLocation(Int32 iCurLocation)
        {
            LocationMap loc1 = (LocationMap)arrLocationGrid[iCurLocation];
            int iToUse = 0;
            int Count = 0;

            Int32 curDuration = 10000;
            foreach (Int32 duration in loc1.arrDistances)
            {
                Count = Count + 1;
                if (duration != 0 && duration < curDuration)
                {
                    curDuration = duration;
                    iToUse = Count;
                } 
            }

            MatchResponse resp = new MatchResponse();
            resp.iLocationId = iToUse;
            resp.iDuration = curDuration;
            return (resp);
        }
    }
}