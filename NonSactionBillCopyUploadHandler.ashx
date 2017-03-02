<%@ WebHandler Language="C#" Class="NonSactionBillCopyUploadHandler" %>

using System;
using System.Web;

public class NonSactionBillCopyUploadHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {
            string path = context.Server.MapPath("~/Bills/VendorBill/");
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            string fileName = string.Empty;
            string databasePath = string.Empty;
            var file = context.Request.Files;
            var billID = context.Request.QueryString["BillId"];
            var agencyName = context.Request.QueryString["agencyname"];
            for (int i = 0; i < file.Count; i++)
            {

                string ext = System.IO.Path.GetExtension(file[i].FileName).Trim();

                string strFileName = agencyName + "_" + billID + ext;

                fileName = System.IO.Path.Combine(path, strFileName);

                file[i].SaveAs(fileName);

                databasePath += "VendorBill/" + strFileName + ",";
            }

            databasePath = databasePath.Substring(0, databasePath.Length - 1);
            DAL.DalAccessUtility.ExecuteNonQuery("Update SubmitBillByUser set VendorBillPath='" + databasePath + "' where SubBillId=" + billID);
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