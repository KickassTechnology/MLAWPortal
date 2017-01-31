using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PdfSharp.Pdf;
using System.Data;
using PdfSharp.Drawing;
using System.Drawing;
using System.IO;

namespace MLAW_Order_System
{
    public partial class Create_Invoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DAL dal = new DAL();
            int iOrderId = Convert.ToInt32(Request["Order_Id"]);

            DataSet ds = dal.getOrderInfo(iOrderId);

            DataRow dr = ds.Tables[0].Rows[0];

            PdfDocument document = new PdfDocument();
            document.Info.Author = "MLAW Engineers";
            document.Info.Keywords = "";

            PdfPage page = document.AddPage();
            page.Size = PdfSharp.PageSize.A4;

            // Obtain an XGraphics object to render to
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            double fontHeight = 10;
            XFont font = new XFont("Times New Roman", fontHeight, XFontStyle.BoldItalic);

            // Get the centre of the page
            double y = 20;
            int lineCount = 0;
            double linePadding = 10;

            // Create a rectangle to draw the text in and draw in it
            XRect rect = new XRect(0, y, page.Width, fontHeight);

            lineCount++;
            y += fontHeight;

            XFont fontNormal = new XFont("Times New Roman", 10, XFontStyle.Regular);
            PointF pt = new PointF(40, 180);
            gfx.DrawString("To: " + dr["Client_Full_Name"].ToString(), fontNormal, XBrushes.Black, pt);
            pt = new PointF(57, 191);
            gfx.DrawString(dr["Billing_Address_1"].ToString(), fontNormal, XBrushes.Black, pt);
            pt = new PointF(57, 202);
            gfx.DrawString(dr["Billing_City"].ToString() + ", " + dr["Billing_State"].ToString() + " " + dr["Billing_Postal_Code"].ToString(), fontNormal, XBrushes.Black, pt);
            pt = new PointF(340, 180);


            gfx.DrawString("Date: " + dr["Invoice_Date_String"].ToString(), fontNormal, XBrushes.Black, pt);
            pt = new PointF(340, 191);
            gfx.DrawString("Invoice Number: " + dr["Invoice_Number"].ToString(), fontNormal, XBrushes.Black, pt);
            pt = new PointF(340, 202);
            gfx.DrawString("CC: 001240", fontNormal, XBrushes.Black, pt);
            pt = new PointF(340, 223);
            gfx.DrawString("Comments:", fontNormal, XBrushes.Black, pt);
            pt = new PointF(340, 234);
            gfx.DrawString(dr["Comments"].ToString(), fontNormal, XBrushes.Black, pt);

            XFont fontBold = new XFont("Times New Roman", fontHeight, XFontStyle.Bold);
            pt = new PointF(40, 250);
            gfx.DrawString("Address: " + dr["Address"].ToString(), fontBold, XBrushes.Black, pt);
            pt = new PointF(81, 261);
            gfx.DrawString("Lot: " + dr["Lot"].ToString(), fontBold, XBrushes.Black, pt);
            pt = new PointF(160, 261);
            gfx.DrawString("Block: " + dr["Block"].ToString(), fontBold, XBrushes.Black, pt);
            pt = new PointF(81, 272);
            gfx.DrawString("Subdivision: " + dr["Subdivision_Name"].ToString(), fontBold, XBrushes.Black, pt);


            pt = new PointF(40, 300);
            gfx.DrawString("Engineers Project No: " + dr["MLAW_Number"].ToString(), fontNormal, XBrushes.Black, pt);
            pt = new PointF(40, 311);
            gfx.DrawString("Date Received: " + dr["Received_Date_String"].ToString(), fontNormal, XBrushes.Black, pt);
            pt = new PointF(40, 322);
            gfx.DrawString("Date Delivered: ", fontNormal, XBrushes.Black, pt);


            //Figure out what to bill the customer

            Decimal dAmount = 0;
            Decimal dDiscount = 0;
            Decimal dTotal = 0;
            Decimal number;

            if (Decimal.TryParse(dr["Amount"].ToString(), out number))
            {
                dAmount = number;
            }

            if (Decimal.TryParse(dr["Discount"].ToString(), out number))
            {
                dDiscount = number;
            }

            dTotal = dAmount - dDiscount;

            /*
            Decimal number;
            Decimal dPayPerSqFt = 0;
            Decimal dPayAmount = 0;
            Decimal dSlabSqFt = 0;
            Int32 iLevel1 = 0;
            Int32 iLevel2 = 0;


            if (Decimal.TryParse(dr["Pay_Per_Sqft"].ToString(), out number))
            {
                dPayPerSqFt = number;
            }

            if (Decimal.TryParse(dr["Slab_Square_Feet"].ToString(), out number))
            {
                dSlabSqFt = number;
            }

            if (Convert.ToInt32(dPayPerSqFt) > 0)
            {


                if (dSlabSqFt == 0)
                {
                    MessageBox.Show(dr["MLAW_Number"].ToString() + " is supposed to be charged by Square Foot, but we have no square foot info.");
                }
                else
                {
                    dPayAmount = dPayPerSqFt * dSlabSqFt;
                }

            }
            else
            {
                iLevel1 = Convert.ToInt32(dr["Pay_Level_1"]);
                iLevel2 = Convert.ToInt32(dr["Pay_Level_2"]);

                if (iLevel1 == 0)
                {
                    dPayAmount = Convert.ToDecimal(dr["Pay_Tier_1"]);
                }
                else
                {

                    if (dSlabSqFt < iLevel1)
                    {
                        dPayAmount = Convert.ToDecimal(dr["Pay_Tier_1"]);
                    }
                    else if (dSlabSqFt >= iLevel1 && dSlabSqFt < iLevel2)
                    {
                        dPayAmount = Convert.ToDecimal(dr["Pay_Tier_2"]);
                    }
                    else
                    {
                        dPayAmount = Convert.ToDecimal(dr["Pay_Tier_3"]);
                    }

                }
            }*/


            PointF pt1 = new PointF(40, 400);
            PointF pt2 = new PointF(540, 400);
            gfx.DrawLine(XPens.Black, pt1, pt2);

            pt = new PointF(40, 430);
            gfx.DrawString("Foundation Design Services: ", fontNormal, XBrushes.Black, pt);

            pt = new PointF(40, 460);
            gfx.DrawString("Base Charge: ..........................................................................................................................", fontNormal, XBrushes.Black, pt);

            pt = new PointF(500, 460);
            gfx.DrawString(dAmount.ToString("C2"), fontNormal, XBrushes.Black, pt);

            if (dDiscount > 0)
            {
                pt = new PointF(500, 480);
                gfx.DrawString(dDiscount.ToString("C2"), fontNormal, XBrushes.Black, pt);

            }
            pt = new PointF(140, 480);
            String strSqFt = "X";

            /*
            if (dSlabSqFt > 0 || iLevel1 > 0)
            {
                strSqFt = Convert.ToInt32(dSlabSqFt).ToString();
            }


            gfx.DrawString("(         Sq Ft:  " + strSqFt + "             )", fontNormal, XBrushes.Black, pt);
            */

            pt = new PointF(140, 500);
            gfx.DrawString("Copies of Document Included", fontNormal, XBrushes.Black, pt);

            pt1 = new PointF(40, 540);
            pt2 = new PointF(540, 540);
            gfx.DrawLine(XPens.Black, pt1, pt2);

            pt = new PointF(300, 600);
            gfx.DrawString("Total Charge:", fontNormal, XBrushes.Black, pt);

            pt = new PointF(500, 600);
            gfx.DrawString(dTotal.ToString("C2"), fontNormal, XBrushes.Black, pt);

            pt = new PointF(300, 530);
            gfx.DrawString("Approved:     ____________________________", fontNormal, XBrushes.Black, pt);



            // Send PDF to browser
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", stream.Length.ToString());
            Response.BinaryWrite(stream.ToArray());
            Response.Flush();
            stream.Close();
            Response.End();

        }
    }
}