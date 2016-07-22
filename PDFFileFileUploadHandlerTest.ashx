<%@ WebHandler Language="C#" Class="PDFFileFileUploadHandler" %>

using System;
using System.Web;

public class PDFFileFileUploadHandler : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {
            string path = context.Server.MapPath("~/PDF/temp");
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            var filename = context.Request.QueryString["count"];

            var file = context.Request.Files;
            string newpath = System.IO.Path.Combine(path, filename);

            var chunks = HttpContext.Current.Request.InputStream;
            //for (int i = 0; i < file.Count; i++)
            //{
            //    string fileName;

            //    if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE")
            //    {
            //        string[] files = file[i].FileName.Split(new char[] { '\\' });
            //        fileName = files[files.Length - 1];
            //    }
            //    else
            //    {
            //        fileName = file[i].FileName;
            //    }
            //    string strFileName = fileName;
            //    fileName = System.IO.Path.Combine(path, fileName);

            //    file[i].SaveAs(fileName);
            //}


            using (System.IO.FileStream fs = System.IO.File.Create(newpath))
            {
                byte[] bytes = new byte[77570];
                int bytesRead;
                while ((bytesRead = HttpContext.Current.Request.InputStream.Read(bytes, 0, bytes.Length)) > 0)
                {
                    fs.Write(bytes, 0, bytesRead);
                }
            }

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