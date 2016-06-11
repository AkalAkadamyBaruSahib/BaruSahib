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

/// <summary>
/// Summary description for Utility
/// </summary>
public static class Utility
{

    public static void SendEmail(string to, string body, List<Attachment> attachments, string subject)
    {
        MailMessage mail = new MailMessage();
        mail.To.Add(to);

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

    public static void GeneratePDF(string html, string fileName)
    {
        HttpContext.Current.Response.ContentType = "application/pdf";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(html);
        HttpContext.Current.Response.BinaryWrite(pdfBytes);
        HttpContext.Current.Response.End();
    }


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


}