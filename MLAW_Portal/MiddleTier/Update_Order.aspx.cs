using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System
{
    public partial class Update_Order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int iOrder_Id = Convert.ToInt32(Request["Order_Id"]);
            String strAddress = Request["Address"].ToString();
            String strCity = Request["City"].ToString();
            String strState = Request["State"].ToString();
            String strZip = Request["Zip"].ToString();
            int? iSubdivisionId = null;
            DateTime dtStartDate = Convert.ToDateTime(Request["Start_Date"]);
            DateTime dtDueDate = Convert.ToDateTime(Request["Due_Date"]);
            int iOrderStatusId = Convert.ToInt32(Request["Order_Status_Id"]);
            int? iOrderWarrantyId = Convert.ToInt32(Request["Order_Warranty_Id"]);
            int iOrderTypeId = Convert.ToInt32(Request["Order_Type_Id"]);
            String strPlanNumber = Request["Plan_Number"].ToString();
            String strPlanName = Request["Plan_Name"].ToString();
            String strLot = Request["Lot"].ToString();
            String strBlock = Request["Block"].ToString();
            String strSection = Request["Section"].ToString();
            int iDivisionId = Convert.ToInt32(Request["Division_Id"]);
            String strComments = Request["Comments"].ToString();
            String strElevation = Request["Elevation"].ToString();
            String strContact = Request["Contact"].ToString();
            String strPhone = Request["Phone"].ToString();
            String strFoundationType = Request["Foundation_Type"].ToString();
            String strPhase = Request["Phase"].ToString();
            String strGarageOptions = Request["Garage_Options"].ToString();
            String strPatioOptions = Request["Patio_Options"].ToString();
            int iMasonry = Convert.ToInt32(Request["Masonry_Sides"]);

            String strPI = Request["PI"].ToString();

            DateTime? dtVisualGeotecDate;

            if(Request["Subdivision_Id"].ToString().Trim() != "")
            {
                iSubdivisionId = Convert.ToInt32(Request["Subdivision_Id"]);
            }

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
            String strSlope = Request["Slope"].ToString();


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
                dBrg_cap= null;
            }
            else
            {
                dBrg_cap = Convert.ToDecimal(Request["Brg_cap"]);
            }

            int? iSF = null;
            


            if (iOrderWarrantyId == 0)
            {
                iOrderWarrantyId = null;
            }


            DAL dal = new DAL();
            dal.updateOrder(iOrder_Id, strAddress, strCity, strState, strZip, iSubdivisionId, dtStartDate, dtDueDate, iOrderStatusId, null, iOrderTypeId, strPlanNumber, strPlanName, strLot, strBlock, strSection, iDivisionId, strPI, dtVisualGeotecDate, strSoilsDataSource, iFillApplied, strSlope, dSlabSquareFeet, dEm_ctr, dEm_edg, dYm_ctr, dYm_edg, dBrg_cap, iSF, strComments, strElevation, strContact, strPhone, strFoundationType, strPhase, strGarageOptions, strPatioOptions, iMasonry); 

            Response.Write("1");


        }
    }
}