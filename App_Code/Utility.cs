using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Text;
using System.IO;
using System.Data;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using IronPdf;


/// <summary>
/// Summary description for Utility
/// </summary>
public static class Utility
{

    public static void SendEmail(string to, string cc, string body, List<Attachment> attachments, string subject)
    {
        MailMessage mail = new MailMessage();
        mail.To.Add(to);
        if (cc.Length > 0)
        {
            mail.CC.Add(cc);
        }

        mail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmailAddress"].ToString());
        mail.Subject = subject;

        mail.Body = body;
        foreach (Attachment att in attachments)
        {
            mail.Attachments.Add(att);
        }

        mail.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPortNum"].ToString());
        smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUserName"].ToString(), ConfigurationManager.AppSettings["SMTPPassword"].ToString());
        smtp.Timeout = 1000000;
        smtp.EnableSsl = true;
        smtp.Send(mail);
    }

    public static void SendSMS(string sto, string body)
    {
        string mobileNumber = sto;
        string senderId = "AKALCO";
        string userid = "AKALSEVA";
        string password = "c@120";
        string message = HttpUtility.UrlEncode(body);

        //Prepare you post parameters
        StringBuilder sbPostData = new StringBuilder();
        sbPostData.AppendFormat("username={0}", userid);
        sbPostData.AppendFormat("&password={0}", password);
        sbPostData.AppendFormat("&to={0}", mobileNumber);
        sbPostData.AppendFormat("&from={0}", senderId);
        sbPostData.AppendFormat("&message={0}", message);

        try
        {
            string sendSMSUri = "http://www.sunshinesms.org/quicksms/api.php";
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(sbPostData.ToString());
            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/x-www-form-urlencoded";
            httpWReq.ContentLength = data.Length;
            using (Stream stream = httpWReq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responseString = reader.ReadToEnd();
            reader.Close();
            response.Close();
        }
        catch (SystemException ex)
        {
            ex.Message.ToString();
        }
    }

    public static string GetPageContent(string FullUri)
    {
        HttpWebRequest Request;
        StreamReader ResponseReader;
        Request = ((HttpWebRequest)(WebRequest.Create(FullUri)));
        ResponseReader = new StreamReader(Request.GetResponse().GetResponseStream());
        return ResponseReader.ReadToEnd();
    }

    public static string SerializeDataTable(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    public static void SendDocumentAlerts()
    {
        // color , grill , speed governer, incase of rash driving, condition of tyres
        DataSet getAlertsInfo = new DataSet();
        DataSet getVehicles = new DataSet();
        DataSet getDocuments = new DataSet();
        DateTime dt;
        InchargeController inc = new InchargeController();
        DataSet ds = new DataSet();
        string numbers = string.Empty;
        string AdminNumber = string.Empty;
        getAlertsInfo = DAL.DalAccessUtility.GetDataInDataSet("select * from TransportDocuments");

        string smsBody = string.Empty;

        string documentBody = string.Empty;
        string replaceMainbody = string.Empty;
        string VehicleNumber = string.Empty;

        if (Convert.ToDateTime(getAlertsInfo.Tables[0].Rows[0]["AlertSentDateTime"].ToString()).Date < DateTime.Now.Date)
        {
            //for (int i = 0; i < getAlertsInfo.Tables[0].Rows.Count; i++)
            //{
            getVehicles = DAL.DalAccessUtility.GetDataInDataSet("select ID,number,ZoneID,TypeID,OwnerNumber from Vehicles where IsApproved=1");
            //= 
            for (int j = 0; j < getVehicles.Tables[0].Rows.Count; j++)
            {
                AdminNumber = ConfigurationManager.AppSettings["AdminTransportSMS"].ToString();
                getDocuments = DAL.DalAccessUtility.GetDataInDataSet("select * from dbo.VechilesDocumentRelation WHERE VehicleID=" + getVehicles.Tables[0].Rows[j]["ID"].ToString());
                numbers = string.Empty;
                VehicleNumber = getVehicles.Tables[0].Rows[j]["Number"].ToString();

                ds = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct INC.*,V.number,Ac.AcaName FROM Vehicles V INNER JOIN dbo.AcademyAssignToEmployee A ON A.AcaID=V.AcademyID INNER JOIN Incharge INC ON INC.InchargeID=A.EMPID INNER JOIN Academy Ac ON Ac.AcaId=A.AcaID WHERE INC.ModuleID=2 AND V.ID=" + getVehicles.Tables[0].Rows[j]["ID"].ToString());
                numbers = String.Join(",", ds.Tables[0].AsEnumerable().Select(x => x.Field<string>("InMobile").ToString()).ToArray());

                if (getDocuments.Tables[0].Rows.Count != 0)
                {
                    if (getVehicles.Tables[0].Rows[j]["TypeID"].ToString() == "2")
                    {
                        AdminNumber += "," + getVehicles.Tables[0].Rows[j]["OwnerNumber"].ToString();
                    }
                    if (getVehicles.Tables[0].Rows[j]["ZoneID"].ToString() == "3")
                    {
                        AdminNumber += ",9815747978";
                    }
                    if (getVehicles.Tables[0].Rows[j]["ZoneID"].ToString() == "6")
                    {
                        AdminNumber += ",9814836530";
                    }
                    if (getVehicles.Tables[0].Rows[j]["ZoneID"].ToString() == "5")
                    {
                        AdminNumber += ",9463372598";
                    }
                    if (getVehicles.Tables[0].Rows[j]["ZoneID"].ToString() == "4")
                    {
                        AdminNumber += ",9914848045";
                    }
                    if (getVehicles.Tables[0].Rows[j]["ZoneID"].ToString() == "7")
                    {
                        AdminNumber += ",9463501396";
                    }
                    if (getVehicles.Tables[0].Rows[j]["ZoneID"].ToString() == "12")
                    {
                        AdminNumber += ",9896661570";
                    }
                    if (getVehicles.Tables[0].Rows[j]["ZoneID"].ToString() == "1")
                    {
                        AdminNumber += ",9815078821";
                    }
                    if (getVehicles.Tables[0].Rows[j]["ZoneID"].ToString() == "10")
                    {
                        AdminNumber += ",9872815109";
                    }
                    if (getVehicles.Tables[0].Rows[j]["ZoneID"].ToString() == "20")
                    {
                        AdminNumber += ",9872815109";
                    }
                    if (getVehicles.Tables[0].Rows[j]["ZoneID"].ToString() == "11")
                    {
                        AdminNumber += ",9872075031";
                    }
                    if (getVehicles.Tables[0].Rows[j]["ZoneID"].ToString() == "14")
                    {
                        AdminNumber += ",9411451543";
                    }

                    numbers += "," + AdminNumber;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Select("UserTypeID = 14").Count() != 0)
                        {
                            smsBody = "Name of Academy: " + ds.Tables[0].Rows[0]["AcaName"].ToString() + ". Vehicle Number " + getVehicles.Tables[0].Rows[j]["Number"].ToString() + ". TM Name " + (from table in ds.Tables[0].AsEnumerable() where table.Field<int>("UserTypeID") == 14 select table.Field<string>("InName")).First<string>() + ". [DocumentDetails]";
                        }
                        else
                        {
                            smsBody = "Name of Academy: " + ds.Tables[0].Rows[0]["AcaName"].ToString() + ". Vehicle Number " + getVehicles.Tables[0].Rows[j]["Number"].ToString() + ". [DocumentDetails]";
                        }

                        documentBody = string.Empty;


                        dt = (from documents in getDocuments.Tables[0].AsEnumerable() where documents.Field<int>("TransportDocumentID") == 1 select documents.Field<DateTime>("DocumentEndDate")).FirstOrDefault();  // Convert.ToDateTime(getDocuments.Tables[0].Rows[j]["DocumentEndDate"].ToString());
                        if ((getDocuments.Tables[0].Select("TransportDocumentID = 1").Count() == 0) || DateTime.Now >= dt.AddDays(-60))
                        {
                            if (dt.ToShortDateString() == "1/1/0001")
                            {
                                documentBody += "Registration not uploaded ";
                            }
                            else
                            {
                                documentBody += "Registration expire(d) on " + dt.ToShortDateString() + " ";
                            }
                            //2 Months 
                        }

                        dt = (from documents in getDocuments.Tables[0].AsEnumerable() where documents.Field<int>("TransportDocumentID") == 2 select documents.Field<DateTime>("DocumentEndDate")).FirstOrDefault();  // Convert.ToDateTime(getDocuments.Tables[0].Rows[j]["DocumentEndDate"].ToString());
                        if ((getDocuments.Tables[0].Select("TransportDocumentID = 2").Count() == 0) || (DateTime.Now >= dt.AddDays(-15)))
                        {
                            string documentName = "Polllution";
                            if (dt.ToShortDateString() == "1/1/0001")
                            {
                                documentBody += documentName + " not uploaded ";
                            }
                            else
                            {
                                documentBody += documentName + " expire(d) on " + dt.ToShortDateString() + " ";
                            }
                            //15 Days Polution
                        }

                        dt = (from documents in getDocuments.Tables[0].AsEnumerable() where documents.Field<int>("TransportDocumentID") == 3 select documents.Field<DateTime>("DocumentEndDate")).FirstOrDefault();  // Convert.ToDateTime(getDocuments.Tables[0].Rows[j]["DocumentEndDate"].ToString());
                        if ((getDocuments.Tables[0].Select("TransportDocumentID = 3").Count() == 0) || (DateTime.Now >= dt.AddDays(-30)))
                        {
                            string documentName = "Permit";
                            if (dt.ToShortDateString() == "1/1/0001")
                            {
                                documentBody += documentName + " not uploaded ";
                            }
                            else
                            {
                                documentBody += documentName + " expire(d) on " + dt.ToShortDateString() + " ";
                            }
                            //1 Month Daily
                        }

                        dt = (from documents in getDocuments.Tables[0].AsEnumerable() where documents.Field<int>("TransportDocumentID") == 4 select documents.Field<DateTime>("DocumentEndDate")).FirstOrDefault();  // Convert.ToDateTime(getDocuments.Tables[0].Rows[j]["DocumentEndDate"].ToString());
                        if ((getDocuments.Tables[0].Select("TransportDocumentID = 4").Count() == 0) || (DateTime.Now >= dt.AddDays(-15)))
                        {

                            string documentName = "Tax";
                            if (dt.ToShortDateString() == "1/1/0001")
                            {
                                documentBody += documentName + " not uploaded ";
                            }
                            else
                            {
                                documentBody += documentName + " expire(d) on " + dt.ToShortDateString() + " ";
                            }
                        }

                        dt = (from documents in getDocuments.Tables[0].AsEnumerable() where documents.Field<int>("TransportDocumentID") == 5 select documents.Field<DateTime>("DocumentEndDate")).FirstOrDefault();  // Convert.ToDateTime(getDocuments.Tables[0].Rows[j]["DocumentEndDate"].ToString());
                        if ((getDocuments.Tables[0].Select("TransportDocumentID = 5").Count() == 0) || (DateTime.Now >= dt.AddDays(-30)))
                        {
                            string documentName = "Passing";
                            if (dt.ToShortDateString() == "1/1/0001")
                            {
                                documentBody += documentName + " not uploaded ";
                            }
                            else
                            {
                                documentBody += documentName + " expire(d) on " + dt.ToShortDateString() + " ";
                            }
                            //1 Month Daily
                        }

                        dt = (from documents in getDocuments.Tables[0].AsEnumerable() where documents.Field<int>("TransportDocumentID") == 6 select documents.Field<DateTime>("DocumentEndDate")).FirstOrDefault();  // Convert.ToDateTime(getDocuments.Tables[0].Rows[j]["DocumentEndDate"].ToString());
                        if ((getDocuments.Tables[0].Select("TransportDocumentID = 6").Count() == 0) || (DateTime.Now >= dt.AddDays(-30)))
                        {
                            string documentName = "Insurance";
                            if (dt.ToShortDateString() == "1/1/0001")
                            {
                                documentBody += documentName + " not uploaded ";
                            }
                            else
                            {
                                documentBody += documentName + " expire(d) on " + dt.ToShortDateString() + " ";
                            }
                            //1 Month Daily
                        }

                        smsBody = smsBody.Replace("[DocumentDetails]", documentBody);
                    }
                    else
                    {
                        //createTextFile("Vehicle Number:- " + VehicleNumber + " Number:- " + numbers + "Transport Manager Assign");
                        SendSMS(numbers, "Warning!! Vehicle" + getVehicles.Tables[0].Rows[j]["Number"].ToString() + "does not have transport manager assign."); // this vehicle does not belongs to any transport manager
                    }
                    //createTextFile("Vehicle Number:- " + VehicleNumber + " Number:- " + numbers + "Normal");
                    SendSMS(numbers, smsBody);
                }
                else
                {
                    //createTextFile("Vehicle Number:- " + VehicleNumber + " Number:- " + numbers + "does not have any document");
                    SendSMS(AdminNumber, "Warning!! Vehicle" + getVehicles.Tables[0].Rows[j]["Number"].ToString() + "does not have any document.Kindly upload the documents as soon as possible.");
                }
            }

            //}
            SendNormsAlerts(Convert.ToDateTime(getAlertsInfo.Tables[0].Rows[0]["AlertSentDateTime"].ToString()), getVehicles);
            DAL.DalAccessUtility.ExecuteNonQuery("Update TransportDocuments set AlertSentDateTime=getdate()");
        }
    }

    public static void SendNormsAlerts(DateTime alertDate, DataSet getAlertsInfo)
    {
        // color , grill , speed governer, incase of rash driving, condition of tyres , GPS,Women conducotr,camera

        DataSet getNorms = new DataSet();
        DataSet dsEmployeeNumber = new DataSet();
        string numbers = string.Empty;
        string AdminNumber = ConfigurationManager.AppSettings["AdminTransportSMS"].ToString();

        string smsBody = string.Empty;
        string documentBody = string.Empty;
        string vehicleNumber = string.Empty;

        if (alertDate.Date < DateTime.Now.Date)
        {

            for (int i = 0; i < getAlertsInfo.Tables[0].Rows.Count; i++)
            {
                getNorms = DAL.DalAccessUtility.GetDataInDataSet("select * from dbo.VechilesNormsRelation WHERE VehicleID=" + getAlertsInfo.Tables[0].Rows[i]["ID"].ToString());

                dsEmployeeNumber = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct INC.*,V.number,Ac.AcaName FROM Vehicles V INNER JOIN dbo.AcademyAssignToEmployee A ON A.AcaID=V.AcademyID INNER JOIN Incharge INC ON INC.InchargeID=A.EMPID INNER JOIN Academy Ac ON Ac.AcaId=A.AcaID WHERE INC.ModuleID=2 AND V.ID=" + getAlertsInfo.Tables[0].Rows[i]["ID"].ToString());

                if (dsEmployeeNumber.Tables[0].Rows.Count > 0)
                {

                    numbers = String.Join(",", dsEmployeeNumber.Tables[0].AsEnumerable().Select(x => x.Field<string>("InMobile").ToString()).ToArray());
                    numbers += "," + AdminNumber;

                    if (dsEmployeeNumber.Tables[0].Select("UserTypeID = 14").Count() != 0)
                    {
                        smsBody = "Name of Academy: " + dsEmployeeNumber.Tables[0].Rows[0]["AcaName"].ToString() + ". Vehicle Number " + getAlertsInfo.Tables[0].Rows[i]["Number"].ToString() + ". TM Name " + (from table in dsEmployeeNumber.Tables[0].AsEnumerable() where table.Field<int>("UserTypeID") == 14 select table.Field<string>("InName")).First<string>() + ". Following Norms are not followed : [DocumentDetails]";
                    }
                    else
                    {
                        smsBody = "Name of Academy: " + dsEmployeeNumber.Tables[0].Rows[0]["AcaName"].ToString() + ". Vehicle Number " + getAlertsInfo.Tables[0].Rows[i]["Number"].ToString() + ". Following Norms are not followed : [DocumentDetails]";
                    }
                    documentBody = string.Empty;

                    if (getNorms.Tables[0].Rows.Count != 0)
                    {

                        if (numbers != string.Empty)
                        {
                            if (getNorms.Tables[0].Select("NormID = 1").Count() == 0)
                            {
                                documentBody += "Yellow Color, ";
                            }
                            if (getNorms.Tables[0].Select("NormID = 2").Count() == 0)
                            {
                                documentBody += "Grill, ";
                            }
                            if (getNorms.Tables[0].Select("NormID = 8").Count() == 0)
                            {
                                documentBody += "Speed Governer, ";
                            }
                            if (getNorms.Tables[0].Select("NormID = 6").Count() == 0)
                            {
                                documentBody += "Incase of rash driving, ";
                            }
                            if (getNorms.Tables[0].Select("NormID = 13").Count() == 0)
                            {
                                documentBody += "GPS, ";
                            }
                            if (getNorms.Tables[0].Select("NormID = 14").Count() == 0)
                            {
                                documentBody += "Female Conductor, ";
                            }
                            if (getNorms.Tables[0].Select("NormID = 15").Count() == 0)
                            {
                                documentBody += "Camera, ";
                            }
                        }

                        smsBody = smsBody.Replace("[DocumentDetails]", documentBody.Substring(0, documentBody.Length - 1));
                        SendSMS(numbers, smsBody);
                    }
                    else
                    {
                        SendSMS(numbers, "Warning!! Vehicle" + getAlertsInfo.Tables[0].Rows[i]["Number"].ToString() + "does not follow any of the norms .Kindly fullfil the norm as soon as possible.");
                    }
                }
                else
                {
                    // does not have any transport manager
                }
            }
        }
    }

    public static void createTextFile(string text)
    {
        string filePath = @"E:\MyTest.txt";

        if (!File.Exists(filePath))
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine(text);
            }
        }

        using (StreamWriter sw = File.AppendText(filePath))
        {
            sw.WriteLine(text);
        }

    }

    public static void GeneratePDF(string html, string fileName, string folderPath)
    {
        HttpContext.Current.Response.ContentType = "application/pdf";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName);


        HtmlToPdf HtmlToPdf = new IronPdf.HtmlToPdf();
        HtmlToPdf.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
        HtmlToPdf.PrintOptions.Header.Spacing = 50;

        PdfResource PDF = HtmlToPdf.RenderHtmlAsPdf(html);

        var pdfBytes = PDF.BinaryData; //(new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(html);

        if (folderPath != string.Empty)
        {
            PDF.SaveAs(folderPath + "\\" + fileName);
        }
        //FileStream fs = new FileStream(folderPath + "\\" + fileName, FileMode.OpenOrCreate);
        //fs.Write(pdfBytes, 0, pdfBytes.Length);
        //fs.Close();

        HttpContext.Current.Response.BinaryWrite(pdfBytes);
        HttpContext.Current.Response.End();
    }

    public static DateTime GetLocalDateTime(DateTime UTCDateTime)
    {
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = DateTime.MinValue;

        if (UTCDateTime != null)
        {
            indianTime = TimeZoneInfo.ConvertTimeFromUtc((DateTime)UTCDateTime, INDIAN_ZONE);
        }
        return indianTime;
    }


    //public static void GeneratePDF(string url, string fileName, string folderPath)
    //{
    //    HttpContext.Current.Response.ContentType = "application/pdf";
    //    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName);

    //    HtmlToPdf.ConvertUrl(url, Response.OutputStream);
    //}


    //public byte[] GetPDF(string pHTML)
    //{
    //    byte[] bPDF = null;

    //    MemoryStream ms = new MemoryStream();
    //    TextReader txtReader = new StringReader(pHTML);

    //    // 1: create object of a itextsharp document class
    //    Document doc = new Document(PageSize.A4, 25, 25, 25, 25);

    //    // 2: we create a itextsharp pdfwriter that listens to the document and directs a XML-stream to a file
    //    PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, ms);

    //    // 3: we create a worker parse the document
    //    HTMLWorker htmlWorker = new HTMLWorker(doc);

    //    // 4: we open document and start the worker on the document
    //    doc.Open();
    //    htmlWorker.StartDocument();

    //    // 5: parse the html into the document
    //    htmlWorker.Parse(txtReader);

    //    // 6: close the document and the worker
    //    htmlWorker.EndDocument();
    //    htmlWorker.Close();
    //    doc.Close();

    //    bPDF = ms.ToArray();

    //    return bPDF;
    //}

    public static void SendEmailUsingAttachments(string filePaths, string to, string cc, string body, string subject)
    {
        List<Attachment> attachments = new List<Attachment>();
        Attachment att;

        att = new Attachment(filePaths);
        attachments.Add(att);

        SendEmail(to, cc, body, attachments, subject);
    }

    public static VehicleAPIData[] getDataFromAPI(string startDate, string endDate, string vehicleNumber)
    {
        string data = Utility.GetPageContent("http://fleetvts.com/attendance_json_api.php?name=" + vehicleNumber + "&from=" + startDate + "&to=" + endDate);
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Deserialize<VehicleAPIData[]>(data);
    }

    public class CookieAwareWebClient : WebClient
    {
        public CookieAwareWebClient()
        {
            CookieContainer = new CookieContainer();
        }
        public CookieContainer CookieContainer { get; private set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = (HttpWebRequest)base.GetWebRequest(address);
            request.CookieContainer = CookieContainer;
            return request;
        }
    }

    public static void GeneratePDFCivilMaterialBill(int BillID, string folderPath)
    {
        string htmlCode = string.Empty;
        Uri myuri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
        string appPath = HttpContext.Current.Request.ApplicationPath;

        string pathQuery = myuri.PathAndQuery;
        string FolderPath = myuri.ToString().Replace(pathQuery, "");
        string hostName = FolderPath + appPath;

        using (var client = new CookieAwareWebClient())
        {
            htmlCode = client.DownloadString(hostName + "/CivilGenerateBill.html");
        }

        DataTable dsBill = new DataTable();
        dsBill = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AdminBillViewByBillId_V2 '" + BillID + "'").Tables[0];
        if (dsBill != null && dsBill.Rows.Count > 0)
        {

            htmlCode = htmlCode.Replace("[CompanyName]", dsBill.Rows[0]["AgencyName"].ToString());
            htmlCode = htmlCode.Replace("[Date]", DateTime.Now.ToString());
            htmlCode = htmlCode.Replace("[DueDate]", dsBill.Rows[0]["BillDate"].ToString());
            htmlCode = htmlCode.Replace("[InvoiceNumber]", BillID.ToString());
            htmlCode = htmlCode.Replace("[ZoneName]", dsBill.Rows[0]["ZoneName"].ToString());
            htmlCode = htmlCode.Replace("[AcademyName]", dsBill.Rows[0]["AcaName"].ToString());
            htmlCode = htmlCode.Replace("[Grid]", Utility.getGridMaterial(Convert.ToInt32(BillID)));
        }

        var AgencyName = dsBill.Rows[0]["AgencyName"].ToString();
        AgencyName = AgencyName.Replace(" ", "");

        // pnlHtml.InnerHtml = htmlCode;


        string fileName = "Civil_Material_Bill_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "_" + AgencyName.Trim() + "_" + BillID + ".pdf";
        Utility.GeneratePDF(htmlCode, fileName, folderPath);
    }

    public static string getGridMaterial(int BID)
    {
       string MaterialInfo = string.Empty;
        decimal TotalAmount = 0;
        decimal SubTotal = 0;
        decimal totalIncludeVat = 0;
        decimal totalAmonutwithouVat = 0;
        decimal totalVat = 0;
        DataSet dsBill = new DataSet();
        dsBill = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AdminBillViewByBillId_V2 '" + BID + "'");

        MaterialInfo += "<table border='1' style='width:100%'>";
        MaterialInfo += "<thead>";
        MaterialInfo += "<tr>";
        MaterialInfo += "<th style='width: 30%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>DESCRIPTION</th>";
        MaterialInfo += "<th style='width: 10%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>QUANTITY</th>";
        MaterialInfo += "<th style='width: 15%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>UNIT PRICE</th>";
        MaterialInfo += "<th style='width: 15%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>TOTAL excl. VAT</th>";
        MaterialInfo += "<th style='width: 15%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>TOTAL incl. VAT</th>";
        MaterialInfo += "<th style='width: 15%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>VAT</th>";
        MaterialInfo += "</tr>";
        MaterialInfo += "</thead>";
        MaterialInfo += "<tbody>";
        for (int i = 0; i < dsBill.Tables[2].Rows.Count; i++)
        {
           
            MaterialInfo += "<tr>";

            MaterialInfo += "<td style='width: 35%; text-align: center; vertical-align: middle;'>" + dsBill.Tables[2].Rows[i]["MatName"].ToString() + "</td>";
            MaterialInfo += "<td style='width: 10%; text-align: center; vertical-align: middle;'>" + dsBill.Tables[2].Rows[i]["Qty"].ToString() + "</td>";
            MaterialInfo += "<td style='width: 10%; text-align: center; vertical-align: middle;'>" + dsBill.Tables[2].Rows[i]["Rate"].ToString() + "</td>";
            TotalAmount = Convert.ToDecimal(dsBill.Tables[2].Rows[i]["Qty"].ToString()) * Convert.ToDecimal(dsBill.Tables[2].Rows[i]["Rate"].ToString());
            TotalAmount = Math.Round(TotalAmount, 2);
            MaterialInfo += "<td style='width: 35%; text-align: center; vertical-align: middle;'>" + TotalAmount + "</td>";
            totalAmonutwithouVat += TotalAmount;
            if (dsBill.Tables[2].Rows[i]["Vat"].ToString() != "0.00")
            {
                SubTotal = (Convert.ToDecimal(TotalAmount) * Convert.ToDecimal(dsBill.Tables[2].Rows[i]["Vat"].ToString())) / 100;
                totalIncludeVat = TotalAmount + SubTotal;
                totalIncludeVat = Math.Round(totalIncludeVat, 2);
                MaterialInfo += "<td style='width: 35%; text-align: center; vertical-align: middle;'>" + totalIncludeVat + "</td>";
                MaterialInfo += "<td style='width: 10%; text-align: center; vertical-align: middle;'>" + dsBill.Tables[2].Rows[i]["Vat"].ToString() + "</td>";
                totalVat += totalIncludeVat - TotalAmount;
            }
            else
            {
                MaterialInfo += "<td style='width: 35%; text-align: center; vertical-align: middle;'>" + TotalAmount + "</td>";
                MaterialInfo += "<td style='width: 10%; text-align: center; vertical-align: middle;'>" + 0 + "</td>";
                totalVat += 0;
            }

            MaterialInfo += "</tr>";
        }
        MaterialInfo += "</tbody>";
        MaterialInfo += "<tfoot>";
        MaterialInfo += "<tr>";
        MaterialInfo += "<td  colspan='3' style='width: 35%; background-color: #CCCCCC; text-align: right; vertical-align: middle;'><b>SUM</b></td>";
        MaterialInfo += "<td colspan='3' style='width: 10%; text-align: center;  vertical-align: middle;'>" + totalAmonutwithouVat + "</td>";
        MaterialInfo += "</tr>";
        MaterialInfo += "<tr>";
        MaterialInfo += "<td  colspan='3' style='width: 35%; background-color: #CCCCCC; text-align: right; vertical-align: middle;'><b>VAT</b></td>";
        MaterialInfo += "<td  colspan='3' style='width: 10%; text-align: center; vertical-align: middle;'>" + totalVat + "</td>";
        MaterialInfo += "</tr>";
        MaterialInfo += "<tr>";
        MaterialInfo += "<td  colspan='3' style='width: 35%; background-color: #CCCCCC; text-align: right; vertical-align: middle;'><b>SUM Incl. VAT</b></td>";
        MaterialInfo += "<td colspan='3' style='width: 10%; text-align: center; vertical-align: middle;'>" + (totalAmonutwithouVat + totalVat) + "</td>";
        MaterialInfo += "</tr>";
        MaterialInfo += "</tfoot>";
        MaterialInfo += "</table>";
        return MaterialInfo;
    }

    public static string getPDFHTML(int numOfColumn, string[] columnName, int numOfRows, string header)
    {
        string grid = string.Empty;
        string columnstr = string.Empty;

        grid += "<table style='width:100%;'>";
        grid += "<tr>";
        grid += "<td style='padding:0px; text-align:left; width:50%' valign='top'>";
        grid += "<img src='http://barusahib.org/wp-content/uploads/2013/06/Logo.png' style='width:100%;' />";
        grid += "</td>";
        grid += "<td style='text-align: right; width:40%;'>";
        grid += "<br /><br />";
        grid += "<div style='font-style:italic; text-align: right;'>";
        grid += "Baru Shahib,";
        grid += "<br />Dist: Sirmaur";
        grid += "<br />Himachal Pradesh-173001";
        grid += "</td>";
        grid += "</tr>";
        grid += "</table>";
        grid += "<br /><br />";
        grid += "<table style='width:100%;'>";
        grid += "<tr>";
        grid += "<td>";
        grid += "<h2>" + header + "</h2>";
        grid += "</td>";
        grid += "</tr>";
        grid += "</table>";
        grid += "<table border='1' style='width:100%'>";
        grid += "<thead>";
        grid += "<tr>";

        foreach (string str in columnName)
        {
            grid += "<th style='width: 30%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>" + str + "</th>";
        }
        grid += "</tr>";
        grid += "</thead>";
        grid += "<tbody>";

        for (int i = 0; i < numOfRows; i++)
        {
            grid += "<tr>";

            foreach (string str in columnName)
            {
                columnstr = str.Substring(0, 4) + i + str.Substring(4);
                grid += "<td style='width: 35%; text-align: center; vertical-align: middle;'>" + columnstr + "</td>";
            }
            grid += "</tr>";
        }
        grid += "</tbody>";
        grid += "</table>";
    

        return grid;
    }
}