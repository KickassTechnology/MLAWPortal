using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MLAW_Order_System
{
    public partial class upload_form : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var httpRequest = HttpContext.Current.Request;
            var filePath = "";
            var fileName = "";

            System.Drawing.Image img = null;
            ArrayList arrImages = null;

            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    fileName = postedFile.FileName;
                    filePath = HttpContext.Current.Server.MapPath("~/uploads/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);

                }

            }
        }
    }
}