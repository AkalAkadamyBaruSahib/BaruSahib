<%@ WebHandler Language="C#" Class="TransportEmployeeQualificationHandler" %>

using System;
using System.Web;

public class TransportEmployeeQualificationHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {
            string path = context.Server.MapPath("~/TransportEmployeeQualification/");
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            var file = context.Request.Files[0];

            string fileName;

            if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE")
            {
                string[] files = file.FileName.Split(new char[] { '\\' });
                fileName = files[files.Length - 1];
            }
            else
            {
                fileName = file.FileName;
            }
            string strFileName = fileName;
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
