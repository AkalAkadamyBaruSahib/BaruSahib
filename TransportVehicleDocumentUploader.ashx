<%@ WebHandler Language="C#" Class="TransportVehicleDocumentUploader" %>

using System;
using System.Web;

public class TransportVehicleDocumentUploader : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.Files.Count > 0)
        {
            string path = context.Server.MapPath("~/VehicleDoc/");
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            var file = context.Request.Files;
            var fileName = context.Request.QueryString["name"];
            for (int i = 0; i < file.Count; i++)
            {
                string ext = System.IO.Path.GetExtension(file[i].FileName);
                fileName += ext;
                string strFileName = fileName;
                fileName = System.IO.Path.Combine(path, fileName);

                //if (System.IO.File.Exists(context.Server.MapPath("~/VehicleDoc/" + fileName)))
                //{
                //    System.IO.File.Delete(context.Server.MapPath("~/VehicleDoc/" + fileName));
                //}
                file[i].SaveAs(fileName);
            }

        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}