<%@ WebHandler Language="C#" Class="VehicleCurrentMeterReadingHandler" %>

using System;
using System.Web;

public class VehicleCurrentMeterReadingHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {
            string path = context.Server.MapPath("~/VehicleDoc/");
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            string fileName = string.Empty;
            string databasePath = string.Empty;
            var file = context.Request.Files;
            var vserviceid = context.Request.QueryString["VehicleServiceID"];
            var number = context.Request.QueryString["VehicleNumber"];
          
            for (int i = 0; i < file.Count; i++)
            {

                string ext = System.IO.Path.GetExtension(file[i].FileName).Trim();
                string strFileName = string.Empty;

                strFileName = "CurrentMeterReading_" + vserviceid + "_" + number + "_" + (i + 1) + ext;
                
                fileName = System.IO.Path.Combine(path, strFileName);

                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                    file[i].SaveAs(fileName);
                }
                else
                {
                    file[i].SaveAs(fileName);
                }
                databasePath += "VehicleDoc/" + strFileName + ",";
            }

            databasePath = databasePath.Substring(0, databasePath.Length - 1);
            DAL.DalAccessUtility.ExecuteNonQuery("Update VehicleServiceRecord set MeterReadingFilePath='" + databasePath + "' where ID=" + vserviceid);
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