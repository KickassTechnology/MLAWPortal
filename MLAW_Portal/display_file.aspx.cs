using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MLAW_Order_System
{
    public partial class display_file : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DAL dal = new DAL();
            int iOrderFileId = Convert.ToInt32(Request["Order_File_Id"]);
            DataSet dsOrderFile = dal.getOrderFile(iOrderFileId);

            DataRow dr = dsOrderFile.Tables[0].Rows[0];
            String strFileType = "";
            String strFileName = dr["Order_File_Name"].ToString();
            byte[] btFile = (byte[])dr["Order_File"];


            if (strFileName.IndexOf(".pdf") > -1)
            {
                strFileType = "application/pdf";
            }
            else if (strFileName.IndexOf(".dwg") > -1)
            {
                strFileType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);
            }
            else if (strFileName.IndexOf(".dwf") > -1)
            {
                strFileType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);
            }
            else if (strFileName.IndexOf(".doc") > -1)
            {
                strFileType = "application/msword";
                Response.AddHeader("Content-Disposition","inline;filename=" + strFileName);
            }else if(strFileName.IndexOf(".docx") > -1)
            {
               strFileType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
               Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);
            }
            else if (strFileName.IndexOf(".xls") > -1)
            {
                strFileType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);
            }
            else if (strFileName.IndexOf(".xlsx") > -1)
            {
                strFileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);
            }



            Response.Buffer = true;
            Response.Charset = "";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + strFileName);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = strFileType;
            Response.BinaryWrite(btFile);
            Response.Flush();
            Response.End();

        }
    }
}