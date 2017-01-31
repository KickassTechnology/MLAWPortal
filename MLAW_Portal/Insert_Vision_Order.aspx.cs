using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace MLAW_Order_System
{
    public partial class Insert_Vision_Order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                        
            int iOrderId = Convert.ToInt32(Request["Order_Id"]);

            DAL dal = new DAL();

            DataSet dsOrder = dal.getOrderInfo(iOrderId);

            Response.Write(dsOrder.Tables[0].Rows.Count.ToString());
            DataRow dr = dsOrder.Tables[0].Rows[0];

            string strHashPwd = sha256("JOEHINDER");

            //This hash is used to authenticate without a password
            strHashPwd = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";

            string query = "";
            string strResponse = "";
            string strClientId = "";
            String strMLAWNumber = dr["MLAW_Number"].ToString();
            string strCity = "";

            if (dr["Vision_Client_ID"].ToString().Trim() == "")
            {
                strClientId = dr["Vision_Client_Name"].ToString().Trim();
            }
            else
            {
                strClientId = dr["Vision_Client_Id"].ToString().Trim();
            }

            /*
            query = "<Queries>";
            query = query + "<Query ID=\"1\">select * from Client where Name = '" + dr + "'</Query>";
            query = query + "</Queries>";
            */
            
            string soap = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            soap = soap + "<VisionConnInfo>";
            soap = soap + "<databaseDescription>MLAW</databaseDescription>";
            soap = soap + "<userName>JOEHINDER</userName>";
            soap = soap + "<userPassword></userPassword>";
            soap = soap + "<encPassword>" + strHashPwd + "</encPassword>";
            soap = soap + "<integratedSecurity>N</integratedSecurity>";
            soap = soap + "<SessionID></SessionID>";
            soap = soap + "</VisionConnInfo>";

            if(strMLAWNumber.Substring(2, 1) == "1")
            {
                strCity = "Austin";
            }else if(strMLAWNumber.Substring(2, 1) == "2")
            {
                strCity = "San Antonio";
            }else if(strMLAWNumber.Substring(2, 1) == "3")
            {
                strCity = "Dallas";
            }else if(strMLAWNumber.Substring(2, 1) == "4")
            {
                strCity = "Killeen";
            }else if(strMLAWNumber.Substring(2, 1) == "5")
            {
                strCity = "Houston";
            }else if(strMLAWNumber.Substring(2, 1) == "6")
            {
                strCity = "Bryan College Station";
            }

            string strInfoProject = "<?xml version=\"1.0\" encoding=\"utf-8\"?><InfoCenters>";
            strInfoProject = strInfoProject + "<InfoCenter ID=\"1\" Name=\"Projects\" Table=\"PR\" RowAccess=\"1\" PartialAccess = \"1\" Chunk=\"1\" ChunkSize=\"100\">";
            strInfoProject = strInfoProject + "</InfoCenter>";
            strInfoProject = strInfoProject + "</InfoCenters>";

            string strProject = "<RECS xmlns=\"http://deltek.vision.com/XMLSchema\" xmlns:xdv=\"http://deltek.vision.com/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">";
            strProject = strProject + "<REC>" + Environment.NewLine;
            strProject = strProject + "<PR name=\"PR\" alias=\"PR\" keys=\"WBS1,WBS2,WBS3\" xmlns=\"http://deltek.vision.com/XMLSchema\">" + Environment.NewLine;
            strProject = strProject + "<ROW tranType=\"INSERT\" >" + Environment.NewLine;
            
            string[] strArray = strMLAWNumber.Split('.');
            String strName = dr["Address"].ToString();

            if (strArray[1].Length == 3)
            {
                strMLAWNumber = strMLAWNumber + "0";
            }else{
                strName = strName + " - REVISION";
            }

            if (strArray[0].Substring(5, 3) == "000")
            {
                strMLAWNumber = strArray[0] + ".0000";
            }

            
            strProject = strProject + "<WBS1>" + strMLAWNumber + "</WBS1>" + Environment.NewLine;
            strProject = strProject + "<WBS2></WBS2>" + Environment.NewLine;
            strProject = strProject + "<WBS3></WBS3>" + Environment.NewLine;
            strProject = strProject + "<Name><![CDATA[" + strName + "]]></Name>" + Environment.NewLine;
            strProject = strProject + "<ChargeType>R</ChargeType>" + Environment.NewLine;
            strProject = strProject + "<SubLevel>N</SubLevel>" + Environment.NewLine;
            strProject = strProject + "<Principal>BURNEYCH</Principal>" + Environment.NewLine;
            strProject = strProject + "<ProjMgr>DVORAKBR</ProjMgr>" + Environment.NewLine;
            strProject = strProject + "<ClientID>" + strClientId + "</ClientID>" + Environment.NewLine;
            strProject = strProject + "<CLAddress>" + strCity + "</CLAddress>" + Environment.NewLine;
            strProject = strProject + "<Status>A</Status>" + Environment.NewLine;
            strProject = strProject + "<RevType>B</RevType>" + Environment.NewLine;
            strProject = strProject + "<Org>" + strMLAWNumber.Substring(2, 1) + ":0" + strMLAWNumber.Substring(3, 1) + "</Org>" + Environment.NewLine;
            strProject = strProject + "<Memo><![CDATA[" + dr["Comments"].ToString() + "]]></Memo>";
            strProject = strProject + "<CLBillingAddr>" + strCity + "</CLBillingAddr>" + Environment.NewLine;
            strProject = strProject + "<LongName><![CDATA[" + strName + "]]></LongName>" + Environment.NewLine;
            strProject = strProject + "<OpportunityID></OpportunityID>" + Environment.NewLine;
            strProject = strProject + "<AvailableForCRM>Y</AvailableForCRM>" + Environment.NewLine;
            strProject = strProject + "<ReadyForApproval>Y</ReadyForApproval>" + Environment.NewLine;
            strProject = strProject + "<ReadyForProcessing>Y</ReadyForProcessing>" + Environment.NewLine;
            strProject = strProject + "<BillingClientID>" + strClientId + "</BillingClientID>" + Environment.NewLine;
            strProject = strProject + "</ROW>" + Environment.NewLine;
            strProject = strProject + "</PR>" + Environment.NewLine;
            strProject = strProject + "</REC>" + Environment.NewLine;
            strProject = strProject + "</RECS>" + Environment.NewLine;

            query = "<Queries>";
            query = query + "<Query ID=\"1\">select * from PR where PR.WBS1= '" + strMLAWNumber + "'</Query>";
            query = query + "</Queries>";

            MLAW_Portal.ServiceReference1.DeltekVisionOpenAPIWebServiceSoapClient cl = new MLAW_Portal.ServiceReference1.DeltekVisionOpenAPIWebServiceSoapClient();

            string RecordDetail = "Primary";

            strResponse = strProject;

               
            strResponse = cl.GetProjectsByQuery(soap, query, RecordDetail);

            //string strResponse = cl.GetClientsByQuery(soap, query, RecordDetail);
            //string strResponse = cl.GetRecordsByQuery(soap, strInfoProject, query, RecordDetail);
            //string strResponse = cl.AddProject(soap,strProject);

                
            if (strResponse.IndexOf("<WBS1>") == -1)
            {
                Response.Write(strProject);
                strResponse = cl.AddProject(soap, strProject);
            }
            else
            {
                Response.Write("ALREADY HERE:" + strResponse);
                /*
                String strUpdateProj = "<RECS xmlns=\"http://deltek.vision.com/XMLSchema\" xmlns:xdv=\"http://deltek.vision.com/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">";
                strUpdateProj = strUpdateProj + "<REC>";
                strUpdateProj = strUpdateProj + "<PR name=\"PR\" alias=\"PR\" keys=\"WBS1,WBS2,WBS3\">";
                strUpdateProj = strUpdateProj + "<ROW tranType=\"UPDATE\">";
                strUpdateProj = strUpdateProj + "<WBS1>" + strMLAWNumber + "</WBS1>";
                strUpdateProj = strUpdateProj + "<WBS2></WBS2>";
                strUpdateProj = strUpdateProj + "<WBS3></WBS3>";
                strUpdateProj = strUpdateProj + "<SubLevel>N</SubLevel>" + Environment.NewLine;
                strUpdateProj = strUpdateProj + "<AvailableForCRM>Y</AvailableForCRM>" + Environment.NewLine;
                strUpdateProj = strUpdateProj + "<ReadyForApproval>Y</ReadyForApproval>" + Environment.NewLine;
                strUpdateProj = strUpdateProj + "<ReadyForProcessing>Y</ReadyForProcessing>" + Environment.NewLine;
                strUpdateProj = strUpdateProj + "</ROW>";
                strUpdateProj = strUpdateProj + "</PR>";
                strUpdateProj = strUpdateProj + "</REC>";
                strUpdateProj = strUpdateProj + "</RECS>";

                strResponse = cl.UpdateProject(soap, strUpdateProj);
                 */
                
            }


            Response.Write(System.Environment.NewLine);
            Response.Write(System.Environment.NewLine);
            Response.Write(strResponse);
        }


        static string sha256(string password)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(password), 0, Encoding.ASCII.GetByteCount(password));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
        
    }
}