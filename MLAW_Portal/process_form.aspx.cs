using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;
using System.IO.Packaging;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using Tesseract;
using System.Web.Script.Serialization;
/*using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;*/
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Text.RegularExpressions;


namespace MLAW_Order_System
{

    class FrmResult
    {
        public String strCompany;
        public String strDate;
        public String strSubdivision;
        public String strCity;
        public String strCounty;
        public String strAddress;
        public String strOrderType;
        public String strContact;
        public String strPhone;
        public String strPlanName;
        public String strPlanNumber;
        public String strElevation;
        public String strGarage;
        public String strLot;
        public String strBlock;
        public String strSection;
        public String strPhase;
        public String strComments;
        public String strCustomerJobNumber;
        public String strMasonrySides;
        public String strPatio;
        public Int32 isRevision;
    }


    public partial class process_form : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
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

                StringBuilder text = new StringBuilder();
                Boolean bIsWord = false;
                Boolean bIsExcel = false;
                bool bIsMLAWForm = false;

                FrmResult thisResp = new FrmResult();


                if (filePath.ToLower().IndexOf(".xls") > -1 || filePath.ToLower().IndexOf(".xlsx") > -1)
                {
                    bIsExcel = true;
                    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = excel.Workbooks.Open(filePath);
                    Worksheet excelSheet = wb.ActiveSheet;

                    string test = "";

                    if (excelSheet.Cells[1, 1].Value2 != null)
                    {
                        test = Convert.ToString(excelSheet.Cells[1, 1].Value2);
                    }

                    if (test != null && test.Trim().IndexOf("Plot Plan Order Form") > -1)
                    {
                        thisResp.strCompany = "EndWal";
                        if (excelSheet.Cells[7, 3].Value2 != null)
                        {
                            thisResp.strSubdivision = Convert.ToString(excelSheet.Cells[7, 3].Value2);
                        }

                        if (excelSheet.Cells[9, 3].Value2 != null)
                        {
                            thisResp.strCustomerJobNumber = Convert.ToString(excelSheet.Cells[9, 3].Value2);
                        }

                        if (excelSheet.Cells[11, 3].Value2 != null)
                        {
                            thisResp.strAddress = Convert.ToString(excelSheet.Cells[11, 3].Value2);
                        }

                        if (excelSheet.Cells[13, 2].Value2 != null)
                        {
                            thisResp.strLot = Convert.ToString(excelSheet.Cells[13, 2].Value2);
                        }

                        if (excelSheet.Cells[13, 6].Value2 != null)
                        {
                            thisResp.strBlock = Convert.ToString(excelSheet.Cells[13, 6].Value2);
                        }

                        if (excelSheet.Cells[15, 2].Value2 != null)
                        {
                            thisResp.strPlanNumber = Convert.ToString(excelSheet.Cells[15, 2].Value2);
                        }

                        if (excelSheet.Cells[15, 7].Value2 != null)
                        {
                            thisResp.strElevation = Convert.ToString(excelSheet.Cells[15, 7].Value2);
                        }

                        if (excelSheet.Cells[9, 3].Value2 != null)
                        {
                            thisResp.strCustomerJobNumber = Convert.ToString(excelSheet.Cells[9, 3].Value2);
                        }

                        if (excelSheet.Cells[17, 3].Value2 != null)
                        {
                            thisResp.strGarage = Convert.ToString(excelSheet.Cells[17, 3].Value2);
                        }
                    }

                    if (excelSheet.Cells[2, 1].Value2 != null)
                    {
                        test = Convert.ToString(excelSheet.Cells[2, 1].Value2);
                    }

                    if (test != null && test.Trim().IndexOf("Ashton Woods") > -1)
                    {
                        thisResp.strCompany = "Ashton";
                        if (excelSheet.Cells[9, 2].Value2 != null)
                        {
                            thisResp.strSubdivision = Convert.ToString(excelSheet.Cells[9, 2].Value2);
                        }

                        if (excelSheet.Cells[10, 2].Value2 != null)
                        {
                            thisResp.strAddress = Convert.ToString(excelSheet.Cells[10, 2].Value2);
                        }

                        if (excelSheet.Cells[11, 2].Value2 != null)
                        {
                            thisResp.strLot = Convert.ToString(excelSheet.Cells[11, 2].Value2);
                        }

                        if (excelSheet.Cells[11, 4].Value2 != null)
                        {
                            thisResp.strBlock = Convert.ToString(excelSheet.Cells[11, 4].Value2);
                        }

                        if (excelSheet.Cells[10, 8].Value2 != null)
                        {
                            thisResp.strSection = Convert.ToString(excelSheet.Cells[10, 8].Value2);
                        }

                        if (excelSheet.Cells[9, 11].Value2 != null)
                        {
                            thisResp.strPhase = Convert.ToString(excelSheet.Cells[9, 11].Value2);
                        }

                        if (excelSheet.Cells[7, 11].Value2 != null)
                        {
                            double a = double.Parse(excelSheet.Cells[7, 11].Value2);
                            thisResp.strDate = DateTime.FromOADate(a).ToShortDateString();
                        }

                        if (excelSheet.Cells[12, 6].Value2 != null)
                        {
                            thisResp.strPlanNumber = Convert.ToString(excelSheet.Cells[12, 6].Value2);
                        }

                        if (excelSheet.Cells[12, 2].Value2 != null)
                        {
                            thisResp.strPlanName = Convert.ToString(excelSheet.Cells[12, 2].Value2);
                        }

                        if (excelSheet.Cells[12, 4].Value2 != null)
                        {
                            thisResp.strElevation = Convert.ToString(excelSheet.Cells[12, 4].Value2);
                        }

                        if (excelSheet.Cells[17, 2].Value2 != null)
                        {
                            thisResp.strGarage = Convert.ToString(excelSheet.Cells[17, 2].Value2);
                        }
                    }

                    if (excelSheet.Cells[1, 1].Value2 != null)
                    {
                        test = Convert.ToString(excelSheet.Cells[1, 1].Value2);
                    }

                    if (test != null && test.ToLower().Trim().IndexOf("castlerock") > -1)
                    {
                        thisResp.strCompany = "Castle";
                        
                        if (excelSheet.Cells[2, 1].Value2 != null)
                        {
                            thisResp.strSubdivision = Convert.ToString(excelSheet.Cells[2, 1].Value2);
                        }

                        if (excelSheet.Cells[2, 2].Value2 != null)
                        {
                            thisResp.strAddress = Convert.ToString(excelSheet.Cells[2, 2].Value2);
                        }

                        /*
                        if (excelSheet.Cells[2, 3].Value2 != null)
                        {
                            thisResp.strPlanName = Convert.ToString(excelSheet.Cells[2, 3].Value2);
                        }

                        if (excelSheet.Cells[2, 4].Value2 != null)
                        {
                            thisResp.strPlanNumber = Convert.ToString(excelSheet.Cells[2, 4].Value2);
                        }
                        */

                        if (excelSheet.Cells[2, 5].Value2 != null)
                        {
                            string[] strLegal = Convert.ToString(excelSheet.Cells[2, 5].Value2).Split('.');
                            thisResp.strLot = strLegal[0];

                            if (strLegal.Length > 1)
                            {
                                thisResp.strBlock = strLegal[1];
                            }

                            if (strLegal.Length > 2)
                            {
                                thisResp.strSection = strLegal[2];
                            }

                        }

                        if (excelSheet.Cells[2, 6].Value2 != null)
                        {
                            string strGarage = Convert.ToString(excelSheet.Cells[2, 6].Value2);
                            if(strGarage.ToLower().IndexOf("left") > -1)
                            {
                                thisResp.strGarage = "Left";
                            }else{
                                thisResp.strGarage = "Right";
                            }
                        }

                        if (excelSheet.Cells[2, 7].Value2 != null)
                        {
                            string[] strLegal = Regex.Split(Convert.ToString(excelSheet.Cells[2, 7].Value2), "Elevation");
                            thisResp.strElevation = strLegal[1].Trim();

                            string strPlanInfo = strLegal[0].Trim();
                            int iPos = strPlanInfo.LastIndexOf(" ");
                            thisResp.strPlanName = strPlanInfo.Substring(0, iPos).Trim();
                            thisResp.strPlanNumber = strPlanInfo.Substring(iPos, (strPlanInfo.Length - iPos)).Trim();

                            if (strLegal.Length > 1)
                            {
                                thisResp.strBlock = strLegal[1];
                            }

                            if (strLegal.Length > 2)
                            {
                                thisResp.strSection = strLegal[2];
                            }

                        }

                        if (excelSheet.Cells[2, 8].Value2 != null)
                        {
                            thisResp.strComments = Convert.ToString(excelSheet.Cells[2, 8].Value2);
                        }

                    }

                    if (excelSheet.Cells[2, 4].Value2 != null)
                    {
                        test = Convert.ToString(excelSheet.Cells[2, 4].Value2);
                    }

                    if (test.Trim().IndexOf("REQUEST FOR SURVEY") > -1)
                    {
                        thisResp.strCompany = excelSheet.Cells[2, 4].Value2;
                        if (excelSheet.Cells[11, 2].Value2 != null)
                        {
                            thisResp.strSubdivision = Convert.ToString(excelSheet.Cells[11, 2].Value2);
                        }
                        
                        ArrayList arrMatches = getCompanySubdivisionMatch(thisResp.strSubdivision);
                        thisResp.strCompany = arrMatches.Count.ToString();
                        String strMatches = "";
                        foreach(String[] strMatch in arrMatches)
                        {
                            strMatches = strMatches + "," + strMatch[0];
                            if (strMatch[0] == "MHICTX")
                            {
                                thisResp.strCompany = "MHICTX";
                                thisResp.strSubdivision = strMatch[1];
                            }

                            if (strMatch[0] == "MHISA")
                            {
                                thisResp.strCompany = "MHISA";
                                thisResp.strSubdivision = strMatch[1];
                            }
                        }
                        
                       
                        if (excelSheet.Cells[11, 4].Value2 != null)
                        {
                            thisResp.strAddress = Convert.ToString(excelSheet.Cells[11, 4].Value2);
                        }

                        if (excelSheet.Cells[11, 5].Value2 != null)
                        {
                            thisResp.strPlanNumber = Convert.ToString(excelSheet.Cells[11, 5].Value2);
                        }

                        if (excelSheet.Cells[11, 6].Value2 != null)
                        {
                            String strLegal = Convert.ToString(excelSheet.Cells[11, 6].Value2);
                            String[] arrLegal = strLegal.Split('/');
                            if (arrLegal.Length > 0)
                            {
                                thisResp.strLot = arrLegal[0];
                            }

                            if (arrLegal.Length > 1)
                            {
                                thisResp.strBlock = arrLegal[1];
                            }

                            if (arrLegal.Length > 2)
                            {
                                thisResp.strSection = arrLegal[2];
                            }

                        }
                        

                    }

                    if (excelSheet.Cells[8, 2].Value2 != null)
                    {
                        test = Convert.ToString(excelSheet.Cells[8, 2].Value2);
                    }

                    if (test !=  null && test.Trim().IndexOf("Buffington") > -1)
                    {
                        thisResp.strCompany = "Buff";
                        if (excelSheet.Cells[9, 2].Value2 != null)
                        {
                            thisResp.strSubdivision = Convert.ToString(excelSheet.Cells[9, 2].Value2);
                        }

                        if (excelSheet.Cells[10, 2].Value2 != null)
                        {
                            thisResp.strAddress = Convert.ToString(excelSheet.Cells[10, 2].Value2);
                        }

                        if (excelSheet.Cells[11, 2].Value2 != null)
                        {
                            thisResp.strLot = Convert.ToString(excelSheet.Cells[11, 2].Value2);
                        }

                        if (excelSheet.Cells[11, 4].Value2 != null)
                        {
                            thisResp.strBlock = Convert.ToString(excelSheet.Cells[11, 4].Value2);
                        }

                        if (excelSheet.Cells[9, 8].Value2 != null)
                        {
                            thisResp.strSection = Convert.ToString(excelSheet.Cells[9, 8].Value2);
                        }

                        if (excelSheet.Cells[9, 11].Value2 != null)
                        {
                            thisResp.strPhase = Convert.ToString(excelSheet.Cells[9, 11].Value2);
                        }

                        if (excelSheet.Cells[12, 6].Value2 != null)
                        {
                            thisResp.strPlanNumber = Convert.ToString(excelSheet.Cells[12, 6].Value2);
                        }

                        if (excelSheet.Cells[12, 2].Value2 != null)
                        {
                            thisResp.strPlanName = Convert.ToString(excelSheet.Cells[12, 2].Value2);
                        }

                        if (excelSheet.Cells[12, 4].Value2 != null)
                        {
                            thisResp.strElevation = Convert.ToString(excelSheet.Cells[12, 4].Value2);
                        }

                        if (excelSheet.Cells[17, 3].Value2 != null)
                        {
                            thisResp.strGarage = Convert.ToString(excelSheet.Cells[17, 3].Value2);
                        }
                    }


                    if (excelSheet.Cells[2, 4].Value2 != null)
                    {
                        test = Convert.ToString(excelSheet.Cells[2, 4].Value2);
                    }

                    if (test.Trim().IndexOf("New Starts Order Form") > -1)
                    {
                        thisResp.strCompany = "MainVu";

                        if (excelSheet.Cells[2, 6].Value2 != null)
                        {
                            thisResp.strDate = Convert.ToString(excelSheet.Cells[2, 6].Value2);
                        }

                        if (excelSheet.Cells[12, 3].Value2 != null)
                        {
                            thisResp.strSubdivision = Convert.ToString(excelSheet.Cells[12, 3].Value2);
                        }

                        if (excelSheet.Cells[13, 3].Value2 != null)
                        {
                            thisResp.strLot = Convert.ToString(excelSheet.Cells[13, 3].Value2);
                        }

                        if (excelSheet.Cells[14, 3].Value2 != null)
                        {
                            thisResp.strBlock = Convert.ToString(excelSheet.Cells[14, 3].Value2);
                        }

                        if (excelSheet.Cells[15, 3].Value2 != null)
                        {
                            thisResp.strPhase = Convert.ToString(excelSheet.Cells[15, 3].Value2);
                        }

                        if (excelSheet.Cells[16, 3].Value2 != null)
                        {
                            thisResp.strAddress = Convert.ToString(excelSheet.Cells[16, 3].Value2);
                        }

                        if (excelSheet.Cells[17, 3].Value2 != null)
                        {
                            thisResp.strCity = Convert.ToString(excelSheet.Cells[17, 3].Value2);
                        }

                        if (excelSheet.Cells[19, 3].Value2 != null)
                        {
                            thisResp.strCounty = Convert.ToString(excelSheet.Cells[19, 3].Value2);
                        }

                        if (excelSheet.Cells[21, 3].Value2 != null)
                        {
                            thisResp.strPlanName = Convert.ToString(excelSheet.Cells[21, 3].Value2);
                        }

                        if (excelSheet.Cells[22, 3].Value2 != null)
                        {
                            thisResp.strElevation = Convert.ToString(excelSheet.Cells[22, 3].Value2);
                        }

                        if (excelSheet.Cells[25, 3].Value2 != null)
                        {
                            thisResp.strGarage = Convert.ToString(excelSheet.Cells[25, 3].Value2);
                        }
                    }

                    if (excelSheet.Cells[2, 4].Value2 != null)
                    {
                        test = Convert.ToString(excelSheet.Cells[2, 4].Value2);
                    }

                    if (test.Trim().IndexOf("New Starts Order Form") > -1)
                    {
                        thisResp.strCompany = "MainVu";

                        if (excelSheet.Cells[2, 6].Value2 != null)
                        {
                            thisResp.strDate = Convert.ToString(excelSheet.Cells[2, 6].Value2);
                        }

                        if (excelSheet.Cells[12, 3].Value2 != null)
                        {
                            thisResp.strSubdivision = Convert.ToString(excelSheet.Cells[12, 3].Value2);
                        }

                        if (excelSheet.Cells[13, 3].Value2 != null)
                        {
                            thisResp.strLot = Convert.ToString(excelSheet.Cells[13, 3].Value2);
                        }

                        if (excelSheet.Cells[14, 3].Value2 != null)
                        {
                            thisResp.strBlock = Convert.ToString(excelSheet.Cells[14, 3].Value2);
                        }

                        if (excelSheet.Cells[15, 3].Value2 != null)
                        {
                            thisResp.strPhase = Convert.ToString(excelSheet.Cells[15, 3].Value2);
                        }

                        if (excelSheet.Cells[16, 3].Value2 != null)
                        {
                            thisResp.strAddress = Convert.ToString(excelSheet.Cells[16, 3].Value2);
                        }

                        if (excelSheet.Cells[17, 3].Value2 != null)
                        {
                            thisResp.strCity = Convert.ToString(excelSheet.Cells[17, 3].Value2);
                        }

                        if (excelSheet.Cells[19, 3].Value2 != null)
                        {
                            thisResp.strCounty = Convert.ToString(excelSheet.Cells[19, 3].Value2);
                        }

                        if (excelSheet.Cells[21, 3].Value2 != null)
                        {
                            thisResp.strPlanName = Convert.ToString(excelSheet.Cells[21, 3].Value2);
                        }

                        if (excelSheet.Cells[22, 3].Value2 != null)
                        {
                            thisResp.strElevation = Convert.ToString(excelSheet.Cells[22, 3].Value2);
                        }

                        if (excelSheet.Cells[25, 3].Value2 != null)
                        {
                            thisResp.strGarage = Convert.ToString(excelSheet.Cells[25, 3].Value2);
                        }
                    }



                    wb.Saved = true;
                    wb.Close();
                }


                if (filePath.IndexOf(".doc") > -1 || filePath.IndexOf(".docx") > -1 || filePath.IndexOf(".rtf") > -1)
                {

                    Microsoft.Office.Interop.Word.Application appWord = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document wordDocument = appWord.Documents.Open(filePath);
                    text.Append("Word Document:" + wordDocument.Content.Text.Trim());

                    List<string> headers = new List<string>();
                    List<string> footers = new List<string>();
                    foreach (Section aSection in appWord.ActiveDocument.Sections)
                    {
                        foreach (Microsoft.Office.Interop.Word.HeaderFooter aHeader in aSection.Headers)
                            text.Append(aHeader.Range.Text);
                        foreach (Microsoft.Office.Interop.Word.HeaderFooter aFooter in aSection.Footers)
                            text.Append(aFooter.Range.Text);
                    }

                    foreach (Microsoft.Office.Interop.Word.Table tb in wordDocument.Tables)
                    {
                        for (int row = 1; row <= tb.Rows.Count; row++)
                        {
                            var cell = tb.Cell(row, 1);
                            text.Append(cell.Range.Text);

                            // text now contains the content of the cell.
                        }
                    }
                    wordDocument.Close();
                    bIsWord = true;

                }

               

                if (filePath.ToLower().IndexOf(".pdf") > -1)
                {
                    Boolean bHasContent = false;
                    if (File.Exists(filePath))
                    {
                        PdfReader pdfReader = new PdfReader(filePath);

                        //Check the rotation
                        /*
                        int n = pdfReader.NumberOfPages;
                    
                        for (int page = 0; page < n; ){
                            ++page;
                            float width = pdfReader.GetPageSize(page).Width;
                            float height = pdfReader.GetPageSize(page).Height;

                            if (width > height)
                            {
                                PdfDictionary pageDict = pdfReader.GetPageN(page);
                                pageDict.Put(PdfName.ROTATE, new PdfNumber(90));
                            }
                        }*/

                        

                        AcroFields pdfFormFields = pdfReader.AcroFields;
                        text.Append("---FORM FIELDS---" + Environment.NewLine);
                        foreach (KeyValuePair<string, AcroFields.Item> kvp in pdfFormFields.Fields)
                        {
                            bHasContent = true;
                            text.Append(kvp.Key.ToString() + ":" + pdfFormFields.GetField(kvp.Key.ToString()) + Environment.NewLine);

                            ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                            string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, 1, strategy);

                            currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));

                            if (currentText.ToLower().IndexOf("engineering services request") > -1)
                            {
                                bIsMLAWForm = true;
                            }

                        }
                        text.Append("---END FORM FIELDS---" + Environment.NewLine);

                        if (bHasContent == false)
                        {
                            for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                            {
                                //ITextExtractionStrategy strategy = new TopToBottomExtractor();
                                ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                                string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                                currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                                text.Append(currentText);
                                if (currentText.Trim() != "" && currentText.Length > 100)
                                {
                                    bHasContent = true;
                                }
                            }
                        }
                        pdfReader.Close();
                        text.Append("IS THIS RUNNING?");

                        if (bHasContent == false)
                        {
                            text.Append("NO CONTENT");
                            int desired_x_dpi = 300;
                            int desired_y_dpi = 300;


                            string inputPdfPath = filePath;
                            string outputPath = HttpContext.Current.Server.MapPath("~/uploads/");
                            
                            GhostscriptVersionInfo _lastInstalledVersion =
                                GhostscriptVersionInfo.GetLastInstalledVersion(
                                        GhostscriptLicense.GPL | GhostscriptLicense.AFPL,
                                        GhostscriptLicense.GPL);

                            GhostscriptRasterizer _rasterizer = new GhostscriptRasterizer();

                            _rasterizer.Open(inputPdfPath, _lastInstalledVersion, false);
                          
                            arrImages = new ArrayList();
                            for (int pageNumber = 1; pageNumber <= _rasterizer.PageCount; pageNumber++)
                            {
                                string strFileName = fileName.Replace(".pdf", "") + "-" + pageNumber.ToString() + ".bmp";
                                string pageFilePath = Path.Combine(outputPath, strFileName);

                                arrImages.Add(pageFilePath);

                                img = _rasterizer.GetPage(desired_x_dpi, desired_y_dpi, pageNumber);
                                img.Save(pageFilePath, System.Drawing.Imaging.ImageFormat.Bmp);
                            }
                            _rasterizer.Close();
                            
                            


                            using (var engine = new Tesseract.TesseractEngine(Server.MapPath(@"~/tessdata"), "eng", Tesseract.EngineMode.Default))
                            {
                                for (int i = 0; i < arrImages.Count; i++)
                                {

                                    Bitmap bmp = (Bitmap)Image.FromFile(arrImages[0].ToString(), true);
                                    bmp.SetResolution(1000.0F, 1000.0F);
                                    Graphics g = Graphics.FromImage(bmp);
                                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
                                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
  


                                    using (var pix = Tesseract.PixConverter.ToPix(bmp))
                                    {
                                        using (var page = engine.Process(pix))
                                        {
                                            text.Append(page.GetText());
                                        }
                                    }

                                    g.Dispose();
                                }

                            }
                        }
                    }
                }

               
                String strText = text.ToString();
                strText = strText.Replace("|", "l");

                
                if (bIsWord == true && strText.ToLower().IndexOf("service order form") > -1)
                {
                    int iPos1 = strText.IndexOf("Date Ordered:") + 13;
                    int iPos2 = strText.IndexOf("Time Ordered:");
                    thisResp.strDate = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                    iPos1 = strText.IndexOf("Contact Number:") + 15;
                    iPos2 = strText.IndexOf("Email Address:");
                    thisResp.strContact = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                    iPos1 = strText.IndexOf("Company Name:") + 13;
                    iPos2 = strText.IndexOf("Site Supervisor/Contact Number:");
                    thisResp.strCompany = getCompanyNameMatch(strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim());

                    iPos1 = strText.IndexOf("(911 Address and City):") + 23;
                    iPos2 = strText.IndexOf("Project Address/Legal");
                    thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                    iPos1 = strText.IndexOf("):", iPos2) + 2;
                    iPos2 = strText.IndexOf("Subdivision and County");
                    String strLegal = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();
                    
                    int iPos3 = strLegal.ToLower().IndexOf("lot") + 3;
                    int iPos4 = strLegal.ToLower().IndexOf("block") + 5;
                    int iPos5 = strLegal.ToLower().IndexOf("unit") + 4;

                    if (iPos3 > -1 && iPos4 > iPos3)
                    {
                        thisResp.strLot = strLegal.Substring(iPos3, (iPos4 - iPos3)).Replace("block", "").Replace("BLOCK", "").Replace("Block", "").Trim();
                    }
                    else
                    {
                        thisResp.strLot = strLegal.Substring(iPos3, (strLegal.Length - iPos3)).Trim();
                    }

                    if (iPos4 > -1 && iPos5 > iPos4)
                    {
                        thisResp.strBlock = strLegal.Substring(iPos4, (iPos5 - iPos4)).Replace("UNIT", "").Replace("unit", "").Replace("Unit", "").Trim();
                    }
                    else
                    {
                        thisResp.strBlock = strLegal.Substring(iPos4, (strLegal.Length - iPos4)).Trim();
                    }

                    if (iPos5 > -1)
                    {
                        thisResp.strSection = strLegal.Substring(iPos5, (strLegal.Length - iPos5)).Trim();
                    }
                    
                    iPos1 = strText.IndexOf("Subdivision and County:") + 23;
                    iPos2 = strText.IndexOf("Form Heights");
                    thisResp.strSubdivision = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                    if (thisResp.strSubdivision.IndexOf(",") > -1)
                    {
                        thisResp.strSubdivision = thisResp.strSubdivision.Substring(0, thisResp.strSubdivision.IndexOf(","));
                    }

                    iPos1 = strText.IndexOf("Plan #:") + 7;
                    iPos2 = strText.IndexOf("Elevation");
                    thisResp.strPlanNumber = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                    iPos1 = strText.IndexOf("Elevation:") + 10;
                    iPos2 = strText.IndexOf("Swing");
                    thisResp.strElevation = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();


                }

                if (bIsMLAWForm == true)
                {

                    int iPos1 = strText.IndexOf("Name:") + 5;
                    int iPos2 = strText.IndexOf("\n", iPos1);
                    String strName = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();
                    

                    String[] arrName = strName.Split(' ');
                    String strInitialMatch = getCompanyNameMatch(arrName[0]);
                    thisResp.strCompany = strInitialMatch;
                    

                    iPos1 = strText.IndexOf("subdivision:") + 12;
                    iPos2 = strText.IndexOf("\n", iPos1);
                    thisResp.strSubdivision = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();
                   

                    ArrayList arrMatches = getCompanySubdivisionMatch(thisResp.strSubdivision);

                    foreach (String[] strMatch in arrMatches)
                    {
                        if (strMatch[2].ToLower().IndexOf(strName.ToLower()) != -1 || strName.ToLower().IndexOf(strMatch[2].ToLower()) != -1)
                        {
                            thisResp.strCompany = strMatch[0];
                            thisResp.strSubdivision = strMatch[1];
                        }
                    }
                    

                    iPos1 = strText.IndexOf("Plan Details:") + 13;
                    iPos2 = strText.IndexOf("\n", iPos1);
                    thisResp.strComments = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();
                    
                    iPos1 = strText.IndexOf("Address:") + 8;
                    iPos2 = strText.IndexOf("\n", iPos1);
                    thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                    

                    
                    iPos1 = strText.IndexOf("revision:") + 9;
                    iPos2 = strText.IndexOf("\n", iPos1);
                    String strRevision = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();
                    if (strRevision.ToLower().Trim() == "yes")
                    {
                        thisResp.isRevision = 1;
                    }

                    iPos1 = strText.IndexOf("City:") + 5;
                    iPos2 = strText.IndexOf("\n", iPos1);
                    thisResp.strCity = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                    iPos1 = strText.IndexOf("Plan Number:") + 12;
                    iPos2 = strText.IndexOf("\n", iPos1);
                    thisResp.strPlanNumber = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                    iPos1 = strText.IndexOf("Lot:") + 4;
                    iPos2 = strText.IndexOf("\n", iPos1);
                    thisResp.strLot = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                    iPos1 = strText.IndexOf("Phase:") + 6;
                    iPos2 = strText.IndexOf("\n", iPos1);
                    thisResp.strSection = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                    iPos1 = strText.IndexOf("block:") + 6;
                    iPos2 = strText.IndexOf("\n", iPos1);
                    thisResp.strBlock = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                    iPos1 = strText.IndexOf("elevation:") + 10;
                    iPos2 = strText.IndexOf("\n", iPos1);
                    thisResp.strElevation = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                    iPos1 = strText.IndexOf("garage swing:") + 13;
                    iPos2 = strText.IndexOf("\n", iPos1);
                    thisResp.strGarage = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                    iPos1 = strText.IndexOf("covered patio:") + 14;
                    iPos2 = strText.IndexOf("\n", iPos1);
                    thisResp.strPatio = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();
                    

                }
                else
                {

                    if (strText.IndexOf("BUZZSAW") > -1)
                    {
                        //Do Darling Houston
                        thisResp.strCompany = "DarHou";
                        int iPos1 = strText.IndexOf("City/State:") + 11;
                        int iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strCity = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Community:") + 10;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strSubdivision = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Address:") + 8;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Job Number:") + 11;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strCustomerJobNumber = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Swing:") + 6;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strGarage = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Base Plan:") + 10;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strPlanName = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Elevation:") + 10;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strElevation = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                    }

                    if (strText.IndexOf("Grand Haven") > -1)
                    {
                        //Do Grand Haven Homes
                        thisResp.strCompany = "Grand";

                        if (strText.IndexOf("Spec Home Order Sheet") > -1)
                        {
                            int iPos1 = strText.IndexOf("Community: ") + 11;
                            int iPos2 = strText.IndexOf("\n", iPos1);
                            thisResp.strSubdivision = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                            iPos1 = strText.IndexOf("vation: ") + 8;
                            iPos2 = strText.IndexOf("\n", iPos1);

                            String strToUse = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();
                            String[] strArrToUse = strToUse.Split(' ');

                            String strPlan = "";
                            String strElevation = "";
                            for (int i = 0; i < (strArrToUse.Length - 1); i++)
                            {
                                strPlan = strPlan + strArrToUse[i] + " ";
                            }

                            strElevation = strArrToUse[(strArrToUse.Length - 1)];

                            thisResp.strElevation = strElevation;
                            thisResp.strPlanName = strPlan;

                            iPos1 = strText.IndexOf("Block:") + 6;
                            iPos2 = strText.IndexOf(" ", iPos1);

                            thisResp.strBlock = strText.Substring(iPos1, (iPos2 - iPos1)).Replace(" ", "").Trim();

                            iPos1 = strText.IndexOf("Lot:") + 4;
                            iPos2 = strText.IndexOf("\n", iPos1);

                            thisResp.strLot = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                            if (iPos2 > -1)
                            {
                                iPos1 = iPos2;
                                iPos2 = strText.IndexOf("Plan/Elevation:", iPos1);

                                thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();
                            }
                        }
                        else
                        {

                            int iPos1 = strText.IndexOf("Community: ") + 11;
                            int iPos2 = strText.IndexOf("Model / Elevation", iPos1);
                            thisResp.strSubdivision = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                            iPos1 = strText.IndexOf("Elevation:") + 10;
                            iPos2 = strText.IndexOf("\n", iPos1);

                            string[] strInfo = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim().Split('/');
                            thisResp.strPlanName = strInfo[0];
                            thisResp.strElevation = strInfo[1];

                            iPos1 = strText.IndexOf("Lot:") + 4;
                            iPos2 = strText.IndexOf("Address", iPos1);

                            strInfo = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim().Split('/');
                            thisResp.strBlock = strInfo[0];
                            thisResp.strLot = strInfo[1];

                            iPos1 = strText.IndexOf("Address:") + 8;
                            iPos2 = strText.IndexOf("\n", iPos1);
                            thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                            iPos1 = strText.IndexOf("Garage") + 6;
                            iPos2 = strText.IndexOf("\n", iPos1);
                            thisResp.strGarage = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        }

                    }

                    if (strText.IndexOf("Buffington") > -1)
                    {
                        //Do Grand Haven Homes
                        thisResp.strCompany = "Buff";
                        int iPos1 = strText.IndexOf("Date: ") + 5;
                        int iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strDate = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Contact:") + 8;
                        iPos2 = strText.IndexOf("Date:", iPos1);
                        thisResp.strContact = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Contact:") + 8;
                        iPos2 = strText.IndexOf("Date:", iPos1);
                        thisResp.strContact = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Subdiv.") + 7;
                        iPos2 = strText.IndexOf("IECC:", iPos1);
                        thisResp.strSubdivision = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Address:") + 8;
                        iPos2 = strText.IndexOf("County:", iPos1);
                        thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Lot. No.") + 8;

                        if (iPos1 < 8)
                        {
                            iPos1 = strText.IndexOf("Lot. No:") + 8;
                        }

                        iPos2 = strText.IndexOf("Block", iPos1);

                        if (iPos2 == -1)
                        {

                            string strLegal = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();
                            string[] arrLegal = strLegal.Split('/');
                            if (arrLegal.Length > 0)
                            {
                                thisResp.strLot = arrLegal[0];
                                if (arrLegal.Length == 2)
                                {
                                    thisResp.strBlock = arrLegal[1];
                                }
                            }
                        }
                        else
                        {
                            thisResp.strLot = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                            iPos1 = strText.IndexOf("Block:") + 6;
                            iPos2 = strText.IndexOf("Square Footage");

                            thisResp.strBlock = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        }

                        iPos1 = strText.IndexOf("Option:") + 7;
                        iPos2 = strText.IndexOf("Fireplace:", iPos1);
                        String strTestPatio = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Replace("\\u0027", "").Trim();
                        if (strTestPatio.ToLower().IndexOf("yes") > -1)
                        {
                            thisResp.strPatio = "Y";
                        }

                        iPos1 = strText.IndexOf("Plan Name") + 9;
                        iPos2 = strText.IndexOf("Elevation:", iPos1);
                        thisResp.strPlanName = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Elevation:") + 10;
                        iPos2 = strText.IndexOf("Plan #:", iPos1);
                        thisResp.strElevation = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Plan #:") + 7;
                        iPos2 = strText.IndexOf("**", iPos1);
                        thisResp.strPlanNumber = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Phase:") + 6;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strPhase = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Plan Name") + 9;
                        iPos2 = strText.IndexOf("Elevation:", iPos1);
                        thisResp.strPlanName = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Swing Load:") + 11;
                        iPos2 = strText.IndexOf("Brick/Stone:", iPos1);

                        String strTestGarage = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();
                        if(strTestGarage.ToLower().IndexOf("left") > -1)
                        {
                            thisResp.strGarage = "L";
                        }else{
                            thisResp.strGarage = "R";
                        }

                        iPos1 = strText.IndexOf("Brick/Stone:") + 12;
                        iPos2 = strText.IndexOf("Levels:", iPos1);
                        String strTextMasonry = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();
                        Regex digitsOnly = new Regex(@"[^\d]");   
                        thisResp.strMasonrySides = digitsOnly.Replace(strTextMasonry, "");

                        iPos1 = strText.IndexOf("Notes:") + 9;
                        iPos2 = strText.IndexOf("Note:", iPos1);
                        thisResp.strComments = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Replace("\\u0027","").Trim();



                    }



                    if (strText.IndexOf("ENGINEERING REQUEST") > -1)
                    {
                        thisResp.strCompany = "HighAu";


                        int iPos1 = strText.IndexOf("Date: ") + 6;
                        int iPos2 = strText.IndexOf("Date Due:", iPos1);
                        thisResp.strDate = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\u0015", " ").Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Job No.:") + 8;
                        iPos2 = strText.IndexOf("Plan No.", iPos1);
                        thisResp.strCustomerJobNumber = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\u0015", " ").Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Plan No.:") + 9;
                        iPos2 = strText.IndexOf("Elevation (s):", iPos1);
                        thisResp.strPlanNumber = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\u0015", " ").Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Elevation (s):") + 14;
                        iPos2 = strText.IndexOf("Garage Swing", iPos1);
                        thisResp.strElevation = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\u0015", " ").Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Garage Swing:") + 13;
                        iPos2 = strText.IndexOf("Option(s)", iPos1);
                        thisResp.strGarage = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\u0015", " ").Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Address:") + 8;
                        iPos2 = strText.IndexOf("Lot/Block", iPos1);
                        thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\u0015", " ").Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Lot/Block:") + 10;
                        iPos2 = strText.IndexOf("Subdivision:", iPos1);
                        String strLB = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\u0015", " ").Replace("\n", "").Trim();
                        String[] arrLB = strLB.Split('/');
                        thisResp.strLot = arrLB[0];
                        if (arrLB.Length > 1)
                        {
                            thisResp.strBlock = arrLB[1];
                        }

                        iPos1 = strText.IndexOf("Subdivision:") + 12;
                        iPos2 = strText.IndexOf("Phase:", iPos1);
                        if (iPos2 < iPos1)
                        {
                            iPos2 = strText.IndexOf("Section", iPos1);
                        }

                        if (iPos2 < iPos1)
                        {
                            iPos2 = strText.IndexOf("City", iPos1);
                        }
                        thisResp.strSubdivision = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\u0015", " ").Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Phase:") + 6;
                        iPos2 = strText.IndexOf("City:", iPos1);
                        thisResp.strPhase = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\u0015", " ").Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Message:") + 16;
                        iPos2 = strText.IndexOf("Architectural Services", iPos1) - 1;
                        if (iPos2 < 0)
                        {
                            iPos2 = strText.Length;
                        }
                        thisResp.strComments = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\u0015", " ").Replace("\n", "").Replace("\r", "").Trim();

                    }

                    if (strText.IndexOf("Pacesetter Homes") > -1)
                    {
                        //Do Pacesetter Homes
                        thisResp.strCompany = "Pace";
                        int iPos1 = strText.IndexOf("DATE: ") + 6;
                        int iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strDate = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("SUB: ") + 5;
                        iPos2 = strText.IndexOf("SEC:", iPos1);
                        thisResp.strSubdivision = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = thisResp.strSubdivision.IndexOf("SEC:");
                        iPos2 = thisResp.strSubdivision.IndexOf("PHASE: ") + 7;
                        int iPos3 = thisResp.strSubdivision.IndexOf(" ", iPos2) + 1;
                        thisResp.strSubdivision = thisResp.strSubdivision.Substring(iPos3, (thisResp.strSubdivision.Length - iPos3));

                        iPos1 = strText.IndexOf("ADDRESS: ") + 8;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("MARK AN \"X\"");
                        iPos2 = strText.IndexOf("X ", iPos1) + 2;
                        iPos3 = strText.IndexOf("\n", iPos2);
                        thisResp.strOrderType = strText.Substring(iPos2, (iPos3 - iPos2)).Replace("\r", "").Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("NAME: ") + 6;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strContact = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("PHONE: ") + 7;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strPhone = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("PLAN: ") + 6;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strPlanNumber = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("ELEVATION: ") + 11;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strElevation = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("PLAN: ") + 6;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strPlanNumber = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("GARAGE SWING: ") + 14;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strGarage = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("LOT ") + 4;
                        iPos2 = strText.IndexOf(" ", iPos1);
                        thisResp.strLot = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("BLOCK ") + 6;
                        iPos2 = strText.IndexOf(" ", iPos1);
                        thisResp.strBlock = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("PHASE: ") + 7;
                        iPos2 = strText.IndexOf("TRCC", iPos1);
                        thisResp.strPhase = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("SPECIAL INSTRUCTIONS:");
                        iPos2 = strText.Length;
                        thisResp.strComments = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Replace("\r", "").Trim();

                    }

                    if (strText.IndexOf("Toll Brothers") > -1)
                    {
                        thisResp.strCompany = "Toll";

                        int iPos1 = strText.IndexOf("Community :") + 11;
                        int iPos2 = strText.IndexOf("Address:", iPos1);
                        thisResp.strSubdivision = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Replace("\r", "").Trim();

                        iPos1 = strText.IndexOf("Plan and Lot Information:");
                        strText = strText.Substring(iPos1, (strText.Length - iPos1));

                        iPos1 = strText.IndexOf("Plan # /") + 9;
                        iPos2 = strText.IndexOf("Hand Of House:", iPos1);
                        string strPlanInfo = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Replace("\r", "").Trim();

                        iPos1 = strPlanInfo.IndexOf("\t");
                        thisResp.strPlanNumber = strPlanInfo.Substring(0, iPos1).Replace("\n", "").Replace("\r", "").Trim();
                        thisResp.strPlanName = strPlanInfo.Substring(iPos1, (strPlanInfo.Length - iPos1)).Replace("\n", "").Replace("\r", "").Trim();

                        iPos1 = strText.IndexOf("Hand Of House:") + 14;
                        iPos2 = strText.IndexOf("TBI Lot #:", iPos1);
                        thisResp.strGarage = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Address:") + 8;
                        iPos2 = strText.IndexOf("Legal", iPos1);
                        thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("Section:") + 8;
                        iPos2 = strText.IndexOf("Roof Trusses:", iPos1);
                        string[] arrLegal = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim().Split('\t');

                        thisResp.strLot = arrLegal[0];

                        if (arrLegal.Length > 1)
                        {
                            thisResp.strBlock = arrLegal[1];
                        }

                        if (arrLegal.Length > 2)
                        {
                            thisResp.strSection = arrLegal[2];
                        }


                    }

                    if (strText.IndexOf("SCOTT FELDER HOMES") > -1)
                    {
                        //Do Scott Felder Homes
                        thisResp.strCompany = "Felder";

                        int iPos1 = strText.IndexOf("ADDRESS: ") + 8;
                        int iPos2 = strText.IndexOf("RELEASED BY: ");
                        thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("SUB: ") + 5;
                        iPos2 = strText.IndexOf("JOB#:");
                        thisResp.strSubdivision = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("JOB#:") + 5;
                        iPos2 = strText.IndexOf("SERIES:");
                        thisResp.strCustomerJobNumber = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("DATE: ") + 6;
                        iPos2 = strText.IndexOf("LEGAL DESC:");
                        thisResp.strDate = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("LEGAL DESC:") + 11;
                        iPos2 = strText.IndexOf("\n", iPos1);

                        string strLegal = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();
                        if (strLegal.IndexOf("/") > 0)
                        {
                            string[] arrLegal = strLegal.Split('/');
                            thisResp.strLot = arrLegal[0];
                            if (arrLegal.Length >= 2)
                            {
                                thisResp.strBlock = arrLegal[1];
                            }

                            if (arrLegal.Length == 3)
                            {
                                thisResp.strPhase = arrLegal[2];
                            }

                        }

                        if (strLegal.ToLower().IndexOf("lot") > -1)
                        {
                            iPos1 = strLegal.ToLower().IndexOf("lot") + 3;
                            iPos2 = strLegal.ToLower().IndexOf("blk");
                            if (iPos2 == -1)
                            {
                                iPos2 = strLegal.ToLower().IndexOf("block");
                            }

                            if (iPos2 == -1)
                            {
                                iPos2 = strLegal.Length;
                            }

                            thisResp.strLot = strLegal.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                            if (iPos2 != strLegal.Length)
                            {
                                if (strLegal.ToLower().IndexOf("blk") > -1)
                                {
                                    iPos1 = iPos2 + 3;
                                }
                                else
                                {
                                    iPos1 = iPos2 + 5;
                                }


                                if (strLegal.ToLower().IndexOf("ph") == -1 && strLegal.ToLower().IndexOf("unit") == -1)
                                {
                                    iPos2 = strLegal.Length;
                                    thisResp.strBlock = strLegal.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();
                                }
                                else if (strLegal.ToLower().IndexOf("ph") != -1)
                                {

                                    iPos2 = strLegal.ToLower().IndexOf("ph");
                                    thisResp.strBlock = strLegal.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                                    iPos1 = iPos2 + 2;
                                    iPos2 = strLegal.Length;
                                    thisResp.strSection = strLegal.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                                }
                                else
                                {
                                    iPos2 = strLegal.ToLower().IndexOf("unit");
                                    thisResp.strBlock = strLegal.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                                    iPos1 = iPos2 + 4;
                                    iPos2 = strLegal.Length;
                                    thisResp.strSection = strLegal.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                                }
                            }
                        }

                        iPos1 = strText.IndexOf("SWING:") + 6;
                        iPos2 = strText.IndexOf("DATE:");
                        thisResp.strDate = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();


                        iPos1 = strText.IndexOf("RELEASED BY: ") + 13;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strContact = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();


                        iPos1 = strText.IndexOf("SERIES:") + 7;
                        iPos2 = strText.IndexOf("SPEC");
                        thisResp.strPlanName = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("PLAN#:") + 6;
                        iPos2 = strText.IndexOf("ELEV");
                        thisResp.strPlanNumber = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("ELEV:") + 5;
                        iPos2 = strText.IndexOf("SWING");
                        thisResp.strElevation = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("SWING:") + 6;
                        iPos2 = strText.IndexOf("DATE");
                        thisResp.strGarage = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                        iPos1 = strText.IndexOf("MASONRY:") + 8;
                        iPos2 = strText.IndexOf("PLAN ", iPos1);
                        String strMas = strText.Substring(iPos1, (iPos2 - iPos1));
                        strMas = Regex.Replace(strMas, "[^0-9.]", "");
                        thisResp.strMasonrySides = strMas.Replace("\n", "").Trim();


                        iPos1 = strText.IndexOf("PATIO?") + 6;
                        iPos2 = strText.IndexOf("ESTIMATED FRONT END");
                        thisResp.strPatio = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "").Trim();

                    }

                    if (strText.IndexOf("Ryland Homes") > -1)
                    {
                        //Do Ryland Homes
                        thisResp.strCompany = "Ryland";

                        int iPos1 = strText.IndexOf("Date ") + 5;
                        int iPos2 = strText.IndexOf("Requester ");
                        thisResp.strDate = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = iPos2;
                        int iPos3 = strText.IndexOf(" X ") + 3;
                        int iPos4 = strText.IndexOfAny("0123456789".ToCharArray(), iPos3);
                        int iPos5 = strText.IndexOf(" ", iPos4);
                        thisResp.strSubdivision = strText.Substring(iPos3, (iPos5 - iPos3));


                        iPos1 = strText.IndexOf("Requester : ") + 12;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strContact = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "");


                        int iPosStart = strText.IndexOf("Lot information") + 15;
                        iPos1 = strText.IndexOf("Lot ", iPosStart) + 4;
                        iPos2 = strText.IndexOf(" ", iPos1);
                        thisResp.strLot = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "");

                        iPos1 = strText.IndexOf("Block ") + 6;
                        iPos2 = strText.IndexOf(" ", iPos1);
                        thisResp.strBlock = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "");

                        iPos1 = strText.IndexOf("Phase ") + 6;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strPhase = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "");

                        iPos1 = strText.IndexOf("Plan Number ") + 12;
                        iPos2 = strText.IndexOf("Plan Name", iPos1);
                        thisResp.strPlanNumber = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "");

                        iPos1 = strText.IndexOf("Plan Name ") + 10;
                        iPos2 = strText.IndexOf("Swing/Elev. Flight", iPos1);
                        thisResp.strPlanName = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "");

                        iPos1 = strText.IndexOf("Swing/Elev. Flight") + 18;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strElevation = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "");

                        iPos1 = strText.IndexOf("Address") + 10;
                        iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Replace("\n", "");


                    }

                    if (strText.ToLower().IndexOf("darling homes") > -1)
                    {
                        //Do Darling
                        //thisResp.strCompany = "Darlin";

                        int iPos1 = strText.IndexOf("marketed as");
                        if (iPos1 > -1)
                        {
                            strText = strText.Substring(iPos1, strText.Length - iPos1);

                            iPos1 = strText.IndexOf("1.") + 2;
                            int iPos2 = strText.IndexOf("FINAL");
                            thisResp.strDate = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                            iPos1 = strText.IndexOf("3.") + 2;
                            iPos2 = strText.IndexOf("Note");
                            thisResp.strSubdivision = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                            DAL dal = new DAL();
                            ArrayList arrMatches = getCompanySubdivisionMatch(thisResp.strSubdivision);

                            foreach (String[] strMatch in arrMatches)
                            {
                                if (strMatch[0].ToLower().IndexOf("dar") == 0)
                                {
                                    thisResp.strCompany = strMatch[0];
                                }
                            }

                            if (thisResp.strCompany == "")
                            {
                                thisResp.strSubdivision = thisResp.strSubdivision.Replace("at", "@");

                                arrMatches = getCompanySubdivisionMatch(thisResp.strSubdivision);

                                foreach (String[] strMatch in arrMatches)
                                {
                                    if (strMatch[0].ToLower().IndexOf("dar") == 0)
                                    {
                                        thisResp.strCompany = strMatch[0];
                                    }
                                }

                            }


                            iPos1 = strText.IndexOf("FINAL,,") + 7;
                            iPos2 = strText.IndexOf("\n", iPos1);
                            thisResp.strPlanNumber = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                            iPos1 = strText.IndexOf("JOB NO.:") + 8;
                            iPos2 = strText.IndexOf("REV.", iPos1);
                            String strSubJob = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();
                            String[] arrText = strSubJob.Split('\n');

                            thisResp.strCustomerJobNumber = arrText[0].ToString();

                            iPos1 = strText.IndexOf("city address") + 12;
                            if (iPos1 == 11)
                            {
                                iPos1 = strText.IndexOf("address city") + 12;
                            }

                            iPos2 = strText.IndexOf("plan date", iPos1);
                            if (iPos2 <= 0)
                            {
                                iPos2 = strText.IndexOf("date plan", iPos1);
                            }
                            thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();
                            //thisResp.strAddress = iPos1.ToString() + ":" + iPos2.ToString();

                            iPos1 = strText.IndexOf("slab location.") + 14;
                            iPos2 = strText.IndexOf("address city", iPos1);
                            arrText = strText.Substring(iPos1, (iPos2 - iPos1)).Trim().Split(' ');

                            thisResp.strLot = arrText[0];

                            if (arrText.Length > 1)
                            {
                                thisResp.strBlock = arrText[1];
                            }
                            if (arrText.Length > 2)
                            {
                                thisResp.strSection = arrText[2];
                            }
                        }

                        if (strText.IndexOf("SERVICES REVISION") > -1)
                        {
                            strText = strText.Replace("\r", "");
                            strText = strText.Replace("\u0007", "");

                            thisResp.isRevision = 1;

                            iPos1 = strText.IndexOf("ADDRESS") + 7;
                            int iPos2 = strText.IndexOf("ACR REVISIONS");
                            thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                            iPos1 = strText.IndexOf("MADE:") + 5;
                            iPos2 = strText.IndexOf("From:");
                            thisResp.strComments = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();
                        }

                    }

                    if (strText.ToLower().IndexOf("m/i homes") > -1)
                    {
                        thisResp.strCompany = "M/I SA";

                        int iPos1 = strText.IndexOf("Address:") + 8;
                        int iPos2 = strText.IndexOf("Community:", iPos1);
                        thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Trim().Replace("\t", " ");

                        iPos1 = strText.IndexOf("Community:") + 10;
                        iPos2 = strText.IndexOf("Plan #:", iPos1);
                        thisResp.strSubdivision = strText.Substring(iPos1, (iPos2 - iPos1)).Trim().Replace("\t", " ");

                        iPos1 = strText.IndexOf("Plan #:") + 7;
                        iPos2 = strText.IndexOf("Lot", iPos1);
                        thisResp.strPlanNumber = strText.Substring(iPos1, (iPos2 - iPos1)).Trim().Replace("\t", " ");

                        iPos1 = strText.IndexOf("Lot:") + 4;
                        iPos2 = strText.IndexOf("Elev", iPos1);
                        thisResp.strLot = strText.Substring(iPos1, (iPos2 - iPos1)).Trim().Replace("\t", " ");

                        iPos1 = strText.IndexOf("Lot:") + 4;
                        iPos2 = strText.IndexOf("Elev", iPos1);
                        thisResp.strLot = strText.Substring(iPos1, (iPos2 - iPos1)).Trim().Replace("\t", " ");

                        iPos1 = strText.IndexOf("Block:") + 6;
                        iPos2 = strText.IndexOf("Swing", iPos1);
                        thisResp.strBlock = strText.Substring(iPos1, (iPos2 - iPos1)).Trim().Replace("\t", " ");

                        iPos1 = strText.IndexOf("Unit:") + 5;
                        iPos2 = strText.IndexOf("Masonry", iPos1);
                        thisResp.strSection = strText.Substring(iPos1, (iPos2 - iPos1)).Trim().Replace("\t", " ");

                    }

                    //they misspelled their name on their form
                    if (strText.ToLower().IndexOf("mcmiillin") > -1)
                    {
                        //Do McMillin
                        thisResp.strCompany = "McMill";

                        int iPos1 = strText.IndexOf("ADDRESS") + 7;
                        int iPos2 = strText.IndexOf("SUBDIVISON", iPos1);
                        thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Trim().Replace("\t", " ");

                        iPos1 = strText.IndexOf("SUBDIVISON") + 10;
                        iPos2 = strText.IndexOf("PBU", iPos1);
                        thisResp.strSubdivision = strText.Substring(iPos1, (iPos2 - iPos1)).Trim().Replace("\t", " ");

                        iPos1 = strText.IndexOf("PLAN ID") + 7;
                        iPos2 = strText.IndexOf("GARAGE", iPos1);
                        thisResp.strPlanNumber = strText.Substring(iPos1, (iPos2 - iPos1)).Trim().Replace("\t", " ");

                        iPos1 = strText.IndexOf("GARAGE") + 6;
                        iPos2 = strText.IndexOf("LOAN TYPE", iPos1);
                        thisResp.strGarage = strText.Substring(iPos1, (iPos2 - iPos1)).Trim().Replace("\t", " ");

                    }

                    if (strText.ToLower().IndexOf("justin cox") > -1)
                    {
                        //Do Jimmy Jacobs - Century
                        thisResp.strCompany = "Jacobs";


                        int iPos1 = strText.IndexOf("DATE SUBMITTED:") + 15;
                        int iPos2 = strText.IndexOf("\n", iPos1);
                        thisResp.strDate = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("COMMUNITY:") + 10;
                        iPos2 = strText.IndexOf("BUYER:", iPos1);
                        thisResp.strSubdivision = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("ADDRESS:") + 8;
                        iPos2 = strText.IndexOf("CITY:", iPos1);
                        thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("CITY:") + 5;
                        iPos2 = strText.IndexOf("LOTUNIT:", iPos1);
                        thisResp.strCity = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("LOTUNIT:") + 8;
                        iPos2 = strText.IndexOf("BLK:", iPos1);
                        thisResp.strLot = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("BLK:") + 4;
                        iPos2 = strText.IndexOf("PHASE:", iPos1);
                        thisResp.strBlock = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("PHASE:") + 6;
                        iPos2 = strText.IndexOf("SEC:", iPos1);
                        thisResp.strPhase = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("SEC:") + 4;
                        iPos2 = strText.IndexOf("PLAN NAME:", iPos1);
                        thisResp.strSection = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("PLAN NAME:") + 10;
                        iPos2 = strText.IndexOf("SWING:", iPos1);
                        thisResp.strPlanName = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("SWING:") + 6;
                        iPos2 = strText.IndexOf("ELEVATION:", iPos1);
                        thisResp.strGarage = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("ELEVATION:") + 10;
                        iPos2 = strText.IndexOf("Builder", iPos1);
                        thisResp.strElevation = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                    }


                    if (thisResp.strCompany == null || thisResp.strCompany == "")
                    {
                        ArrayList arrResponse = getCompanySubdivisionMatch(strText);
                        String[] strMatch = (String[])arrResponse[0];
                        thisResp.strCompany = strMatch[0];
                        thisResp.strSubdivision = strMatch[1];

                        if (thisResp.strCompany == null || thisResp.strCompany == "")
                        {
                            thisResp.strCompany = getCompanyNameMatch(strText);

                        }
                    }


                    if (thisResp.strCompany == "McNair")
                    {
                        strText = strText.Replace("\r", "");
                        strText = strText.Replace("\u0007", "");
                        int iPos1 = strText.IndexOf("Address");
                        int iPos2 = strText.IndexOf("Lot / Block", iPos1);
                        thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();


                        iPos1 = strText.IndexOf("BlockLot") + 8;
                        iPos2 = strText.IndexOf(",", iPos1);
                        thisResp.strLot = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf(", Block") + 7;
                        iPos2 = strText.IndexOf("Plan Name", iPos1);
                        thisResp.strBlock = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("Plan Name", iPos2) + 9;
                        iPos2 = strText.IndexOf("Community", iPos1);
                        thisResp.strPlanName = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("Community", iPos2) + 9;
                        iPos2 = strText.IndexOf("Buyer", iPos1);
                        thisResp.strSubdivision = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("Date", iPos2) + 4;
                        iPos2 = iPos1 + 8;
                        thisResp.strDate = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();


                    }

                    if (strText.IndexOf("Taylor Morrison") > -1)
                    {

                        thisResp.strCompany = "Morrsn";
                        int iPos1 = strText.IndexOf("Plan Name:") + 10;
                        int iPos2 = strText.IndexOf("Plan #:", iPos1);
                        thisResp.strPlanName = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("Plan #:") + 7;
                        iPos2 = strText.IndexOf("Plan Series:", iPos1);
                        thisResp.strPlanNumber = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("Elevation:") + 10;
                        iPos2 = strText.IndexOf("Garage:", iPos1);
                        thisResp.strElevation = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("Garage:") + 7;
                        iPos2 = strText.IndexOf("Fireplace", iPos1);
                        thisResp.strGarage = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("Community:") + 10;
                        iPos2 = strText.IndexOf("Address:", iPos1);
                        thisResp.strSubdivision = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("Address:") + 8;
                        iPos2 = strText.IndexOf("Lot:", iPos1);
                        thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("Lot:") + 4;
                        iPos2 = strText.IndexOf("Block:", iPos1);
                        thisResp.strLot = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("Block:") + 6;
                        iPos2 = strText.IndexOf("Section:", iPos1);
                        thisResp.strBlock = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("Section:") + 8;
                        iPos2 = strText.IndexOf("NOTES:", iPos1);
                        thisResp.strSection = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("\r\r", iPos2) + 2;
                        iPos2 = strText.Length;
                        thisResp.strComments = strText.Substring(iPos1, (iPos2 - iPos1)).Trim().Replace("\u0026", "&");

                    }

                    if (thisResp.strCompany == "Meritg")
                    {
                        int iPos = strText.IndexOf("Plan and Homesite") + 16;
                        int iPos1 = strText.IndexOf("Plan ", iPos) + 5;
                        int iPos2 = strText.IndexOf("Tract ", iPos1);

                        thisResp.strPlanName = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("Address", iPos2) + 7;
                        iPos2 = strText.IndexOf("Stage", iPos1);

                        thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                    }

                    if (strText.IndexOf("Lot/Block Subdivision Plan/Elevation/Masonry/Curbcut") > -1)
                    {
                        //It's one of the Dhortons
                        int iPos1 = strText.IndexOf("Subdivision:") + 12;
                        int iPos2 = strText.IndexOf("/", iPos1);
                        thisResp.strSubdivision = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        DAL dal = new DAL();
                        ArrayList arrMatches = getCompanySubdivisionMatch(thisResp.strSubdivision);

                        foreach (String[] strMatch in arrMatches)
                        {
                            if (strMatch[0].ToLower().IndexOf("dr") == 0)
                            {
                                thisResp.strCompany = strMatch[0];
                            }
                        }

                        //thisResp.strCompany = "DRhort";

                        iPos1 = strText.IndexOf("Phase:") + 6;
                        iPos2 = strText.IndexOf("/", iPos1);
                        thisResp.strSection = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        iPos1 = strText.IndexOf("Block:") + 6;
                        iPos2 = strText.IndexOf("/ Address", iPos1);
                        String strLegal = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();

                        int iPosSlash = strLegal.IndexOf("/");
                        thisResp.strLot = strLegal.Substring(0, iPosSlash);
                        iPosSlash = iPosSlash + 1;
                        thisResp.strBlock = strLegal.Substring(iPosSlash, (strLegal.Length - iPosSlash));

                        iPos1 = strText.IndexOf("Address:") + 8;
                        iPos2 = strText.IndexOf("/", iPos1);
                        thisResp.strAddress = strText.Substring(iPos1, (iPos2 - iPos1)).Trim();


                        iPos1 = strText.IndexOf("Lot/Block Subdivision Plan/Elevation/Masonry/Curbcut");
                        iPos2 = strText.IndexOf(thisResp.strSubdivision, iPos1) + thisResp.strSubdivision.Length;
                        int iPos3 = strText.IndexOf("Stage of Construction", iPos2);
                        String strItem = strText.Substring(iPos2, (iPos3 - iPos2)).Trim();
                        String[] strItems = strItem.Split('/');
                        thisResp.strPlanName = strItems[0];
                        thisResp.strElevation = strItems[1];
                        thisResp.strMasonrySides = strItems[2];
                        thisResp.strMasonrySides = Regex.Replace(thisResp.strMasonrySides, "[^0-9.]", "");
                        thisResp.strGarage = strItems[3].Replace("-", "").Trim();
                    }
                }
                
                var json = new JavaScriptSerializer().Serialize(thisResp);
                Response.Write(json);
                //Response.Write(json + Environment.NewLine + "----------" + Environment.NewLine + strText);
                //Response.Write(strText);

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }

        }

        public ArrayList getCompanySubdivisionMatch(String strText)
        {

            ArrayList arrResponse = new ArrayList();
            
            DAL dal = new DAL();
            DataSet dsSubs = dal.getAllSubdivisions();
            String strMatchClient = "";
            
            foreach (DataRow dr in dsSubs.Tables[0].Rows)
            {
                if (dr["Subdivision_Name"].ToString().Trim() != "")
                {
                    String[] strResult = new String[3];

                    bool bAdd = true;
                    if (strText.ToLower().IndexOf(dr["Subdivision_Name"].ToString().ToLower()) > -1 || strText.ToLower().IndexOf(dr["Subdivision_Name"].ToString().ToLower().Replace("@", "at")) > -1)
                    {
                        strResult[0] = dr["Client_Short_Name"].ToString();
                        strResult[1] = dr["Subdivision_Name"].ToString();
                        strResult[2] = dr["Client_Full_Name"].ToString();
                        strMatchClient = strResult[0];
                     
                        arrResponse.Add(strResult);
                        bAdd = false;
                    }

                    if (dr["Subdivision_Name"].ToString().ToLower().IndexOf(strText.ToLower()) > -1 || dr["Subdivision_Name"].ToString().ToLower().Replace("@", "at").IndexOf(strText.ToLower()) > -1)
                    {
                        strResult[0] = dr["Client_Short_Name"].ToString();
                        strResult[1] = dr["Subdivision_Name"].ToString();
                        strResult[2] = dr["Client_Full_Name"].ToString();
                        strMatchClient = strResult[0];
                 
                        if (bAdd == true)
                        {
                            arrResponse.Add(strResult);
                        }
                    }
                }
            }
            
            return (arrResponse);
        }

        public String getCompanyNameMatch(String strText)
        {
            
            DAL dal = new DAL();
            DataSet dsSubs = dal.getAllClients();
            String strResult = "";
            foreach (DataRow dr in dsSubs.Tables[0].Rows)
            {
                if (dr["Client_Full_Name"].ToString() != "")
                {
                    if (strText.IndexOf(dr["Client_Full_Name"].ToString()) > -1 || dr["Client_Full_Name"].ToString().IndexOf(strText) > -1)
                    {
                        strResult = dr["Client_Short_Name"].ToString();
                    }
                }
            }
            return (strResult);
           
        }

    }
}
