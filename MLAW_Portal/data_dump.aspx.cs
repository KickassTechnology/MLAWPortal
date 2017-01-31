using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace MLAW_Portal
{
    public partial class data_dump : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var httpRequest = HttpContext.Current.Request;
            var filePath = "";
            var fileName = "";

            if (httpRequest.Files.Count > 0)
            {
             
                foreach (string file in httpRequest.Files)
                {
                    filePath = "";
                    var postedFile = httpRequest.Files[file];
                    if (postedFile.ContentLength > 0)
                    {
                        fileName = postedFile.FileName;

                        filePath = Path.Combine(Server.MapPath("~/data_dump/"), postedFile.FileName);

                        postedFile.SaveAs(filePath);

                    }
                }
            }
        }
    }
}