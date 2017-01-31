using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace MLAW_Order_System
{
    public partial class Insert_Order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strMLAWNumber = Request["MLAW_Number"].ToString();
            String strAddress = Request["Address"].ToString();
            String strCity = Request["City"].ToString();
            String strState = Request["State"].ToString();
            String strZip = Request["Zip"].ToString();

            int? iSubdivisionId;
            if(Request["Subdivision_Id"].ToString().Trim() != "")
            {
                iSubdivisionId = Convert.ToInt32(Request["Subdivision_Id"]);
            }
            else
            {
                iSubdivisionId = null;
            }

            int? iDivisionId;
            if(Request["Division_Id"].ToString().Trim() != "")
            {
                iDivisionId = Convert.ToInt32(Request["Division_Id"]);
            }
            else {
                iDivisionId = null;
            }

            int? iClientId;
            if(Request["Client_Id"].ToString().Trim() != "")
            {
                iClientId = Convert.ToInt32(Request["Client_Id"]);
            }
            else
            {
                iClientId = null;
            }

            DateTime dtStartDate = Convert.ToDateTime(Request["Start_Date"]);
            int iOrderStatusId = Convert.ToInt32(Request["Order_Status_Id"]);
            int iOrderWarrantyId = Convert.ToInt32(Request["Order_Warranty_Id"]);
            int iOrderTypeId = Convert.ToInt32(Request["Order_Type_Id"]);
            String strPlanNumber = Request["Plan_Number"].ToString();
            String strPlanName = Request["Plan_Name"].ToString();
            String strLot = Request["Lot"].ToString();
            String strBlock = Request["Block"].ToString();
            String strSection = Request["Section"].ToString();
            String strPhase = Request["Phase"].ToString();
            String strComments = Request["Comments"].ToString();
            String strOrderFile = Request["Order_File"].ToString();

            String strElevation = Request["Elevation"].ToString();
            String strContact = Request["Contact"].ToString();
            String strPhone = Request["Phone"].ToString();
            String strFoundationType = Request["Foundation_Type"].ToString();

            String strCounty = Request["County"].ToString();
            String strIsRevision = Request["IsRevision"].ToString();
            String strGarageType = Request["Garage"].ToString();
            String strPatio = Request["Patio"].ToString();
            String strFireplace = Request["Fireplace"].ToString();
            String strGarageOptions = Request["Garage_Options"].ToString();
            String strPatioOptions = Request["Patio_Options"].ToString();
            int iMasonry = Convert.ToInt32(Request["Masonry_Sides"]);
            double dAmount = Convert.ToDouble(Request["Amount"]);
            double dDiscount = Convert.ToDouble(Request["Discount"]);
            int iFoundationOrderType = Convert.ToInt32(Request["FoundationOrderType"]);

            String strPI = Request["PI"].ToString();
            DateTime? dtVisualGeotecDate;

            if (Request["Visual_Geotec_Date"].ToString() == "")
            {
                dtVisualGeotecDate = null;
            }
            else
            {
                dtVisualGeotecDate = Convert.ToDateTime(Request["Visual_Geotec_Date"]);
            }

            String strSoilsDataSource = Request["Soils_Data_Source"].ToString();
            int iFillApplied = Convert.ToInt32(Request["Fill_Applied"]);
            String strFillDepth = Request["Fill_Depth"].ToString();
            String strCustomerJobNumber = Request["Customer_Job_Number"].ToString();
            String strSoilsComments = Request["Soils_Comments"].ToString();
            String strSlope = Request["Slope"].ToString();

            int? iParentId = null;

            if (Request["Parent_Id"].ToString().Trim() != "")
            {
                iParentId = Convert.ToInt32(Request["Parent_Id"]);
            }


            Decimal? dSlabSquareFeet;
            Decimal? dEm_ctr;
            Decimal? dEm_edg;
            Decimal? dYm_ctr;
            Decimal? dYm_edg;
            Decimal? dBrg_cap;

            if (Request["Slab_Square_Feet"].ToString().Trim() == "")
            {
                dSlabSquareFeet = null;
            }
            else
            {
                dSlabSquareFeet = Convert.ToDecimal(Request["Slab_Square_Feet"]);
            }

            if (Request["Em_ctr"].ToString().Trim() == "")
            {
                dEm_ctr = null;
            }
            else
            {
                dEm_ctr = Convert.ToDecimal(Request["Em_ctr"]);
            }

            if (Request["Em_edg"].ToString().Trim() == "")
            {
                dEm_edg = null;
            }
            else
            {
                dEm_edg = Convert.ToDecimal(Request["Em_edg"]);
            }

            if (Request["Ym_ctr"].ToString().Trim() == "")
            {
                dYm_ctr = null;
            }
            else
            {
                dYm_ctr = Convert.ToDecimal(Request["Ym_ctr"]);
            }

            if (Request["Ym_edg"].ToString().Trim() == "")
            {
                dYm_edg = null;
            }
            else
            {
                dYm_edg = Convert.ToDecimal(Request["Ym_edg"]);
            }

            if (Request["Brg_cap"].ToString().Trim() == "")
            {
                dBrg_cap = null;
            }
            else
            {
                dBrg_cap = Convert.ToDecimal(Request["Brg_cap"]);
            }

            int? iSF = null;

            DAL dal = new DAL();




                DataSet ds = dal.insertOrder(strMLAWNumber, strAddress, strCity, strState, strZip, iSubdivisionId, dtStartDate, iOrderStatusId, null, iOrderTypeId, strPlanNumber, strPlanName, strLot, strBlock, strSection, strPI, dtVisualGeotecDate, strSoilsDataSource, iFillApplied, strSlope, dSlabSquareFeet, dEm_ctr, dEm_edg, dYm_ctr, dYm_edg, dBrg_cap, null, strComments, strElevation, strContact, strPhone, strFoundationType, strPhase, strCounty, strIsRevision, strGarageType, strPatio, strFireplace, strGarageOptions, strPatioOptions, iMasonry, strFillDepth, strSoilsComments, strCustomerJobNumber,iParentId, dAmount, dDiscount, iFoundationOrderType, iClientId, iDivisionId);

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;

                DataTable dt = ds.Tables[0];

                if (ds.Tables[0].Rows[0][0].ToString() != "-1")
                {
                    int iOrderId = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    if (strOrderFile != "")
                    {
                        string[] strFiles = strOrderFile.Split(';');
                        foreach (string strFile in strFiles)
                        {
                            dal.insertOrderFile(iOrderId, strFile);
                        }
                    }
                }

                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }

                Response.Write(serializer.Serialize(rows));

        }
        private String Number2String(int number, bool isCaps)
        {
            Char c = (Char)((isCaps ? 65 : 97) + (number - 1));
            return c.ToString();
        }
    }

}