<%@ WebHandler Language="C#" Class="TransferLetterHandler" %>

using System;
using System.Web;

public class TransferLetterHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {
            string path = context.Server.MapPath("~/SecurityAppointmentLetter/TransferLetters/");
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            var file = context.Request.Files[0];

            var fileName = context.Request.QueryString["name"];
            
            fileName = System.IO.Path.Combine(path, fileName);

            file.SaveAs(fileName);
        }
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}