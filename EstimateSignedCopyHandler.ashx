<%@ WebHandler Language="C#" Class="EstimateSignedCopyHandler" %>

using System;
using System.Web;

public class EstimateSignedCopyHandler : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {
            string path = context.Server.MapPath("~/EstFile/");
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            string fileName = string.Empty;
            string databasePath = string.Empty;
            var file = context.Request.Files;
            var estID = context.Request.QueryString["EstId"];
            for (int i = 0; i < file.Count; i++)
            {

                string ext = System.IO.Path.GetExtension(file[i].FileName).Trim();

                string strFileName = estID + "_Signcopy_" + (i + 1) + ext;
                fileName = System.IO.Path.Combine(path, strFileName);

                file[i].SaveAs(fileName);

                databasePath += "EstFile/" + strFileName + ",";
            }

            databasePath = databasePath.Substring(0, databasePath.Length - 1);
            DAL.DalAccessUtility.ExecuteNonQuery("Update Estimate set FilePath='" + databasePath + "' where estid=" + estID);
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
