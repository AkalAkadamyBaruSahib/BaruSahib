using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for DrawingController
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class DrawingController : System.Web.Services.WebService {

    public DrawingController () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void SendDrawingsAsEmails(string[] drawingPaths, string to, string body, string subject)
    {

        List<Attachment> attachments = new List<Attachment>();
        Attachment att;
        foreach (string str in drawingPaths)
        {
            att = new Attachment(Server.MapPath(str));
            attachments.Add(att);
        }
        Utility.SendEmail(to, body, attachments, subject);
    }
    
}
