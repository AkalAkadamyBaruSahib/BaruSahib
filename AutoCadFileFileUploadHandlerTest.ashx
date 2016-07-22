<%@ WebHandler Language="C#" Class="AutoCadFileFileUploadHandler" %>

using System;
using System.Web;

public class AutoCadFileFileUploadHandler : IHttpHandler {
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {
            string path = context.Server.MapPath("~/AutoCad/temp");
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);



            var filename = context.Request.QueryString["count"];
            
            var file = context.Request.Files;
            string newpath = System.IO.Path.Combine(path, filename);
            
            var chunks = HttpContext.Current.Request.InputStream;


            //string tempFileName = System.IO.Path.Combine(path, "temp.dwg");

            //    if (System.IO.File.Exists(tempFileName))
            //    {
            //        using (System.IO.FileStream fs = System.IO.File.Open(tempFileName, System.IO.FileMode.Append))
            //        {
            //            byte[] bytes = new byte[77570];
            //            int bytesRead;
            //            while ((bytesRead = HttpContext.Current.Request.InputStream.Read(bytes, 0, bytes.Length)) > 0)
            //            {
            //                fs.Write(bytes, 0, bytesRead);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        using (System.IO.FileStream fs = System.IO.File.Create(tempFileName))
            //        {
            //            byte[] bytes = new byte[77570];
            //            int bytesRead;
            //            while ((bytesRead = HttpContext.Current.Request.InputStream.Read(bytes, 0, bytes.Length)) > 0)
            //            {
            //                fs.Write(bytes, 0, bytesRead);
            //            }

            //        }
            //    }


            using (System.IO.FileStream fs = System.IO.File.Create(newpath))
            {
                byte[] bytes = new byte[77570];
                int bytesRead;
                while ((bytesRead = HttpContext.Current.Request.InputStream.Read(bytes, 0, bytes.Length)) > 0)
                {
                    fs.Write(bytes, 0, bytesRead);
                }
            }
            
            
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