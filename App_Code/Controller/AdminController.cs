using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for AdminController
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class AdminController : System.Web.Services.WebService
{

    public AdminController()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void AssignAllAcademiesToUser(int InchargeID)
    {
        AdminRepository adminRepository = new AdminRepository(new AkalAcademy.DataContext());
        adminRepository.AssignAllAcademiesToUser(InchargeID);
    }
    [WebMethod]
    public void DeletePSName(int PSID)
    {
        AdminRepository repository = new AdminRepository(new AkalAcademy.DataContext());
        repository.DeletePSName(PSID);
    }

    public static int count = 1;
    [WebMethod]
    public string MultiUpload()
    {
        count++;
        var filename = count.ToString();
        var chunks = HttpContext.Current.Request.InputStream;
        string path = Server.MapPath("~/AutoCad/temp");
        string newpath = Path.Combine(path, filename);

        using (System.IO.FileStream fs = System.IO.File.Create(newpath))
        {
            byte[] bytes = new byte[77570];
            int bytesRead;
            while ((bytesRead = HttpContext.Current.Request.InputStream.Read(bytes, 0, bytes.Length)) > 0)
            {

                fs.Write(bytes, 0, bytesRead);
            }
        }
        return "test";
    }

    [WebMethod]
    public string UploadComplete(string fileName)
    {
        string path = Server.MapPath("~/AutoCad/temp");
        string temppath = Path.Combine(path, fileName);
        string perapath = Server.MapPath("~/AutoCad");
        string perpath = Path.Combine(perapath, fileName);
        string[] filePaths = Directory.GetFiles(path).OrderByDescending(d => new FileInfo(d).CreationTime).Reverse().ToArray();

        foreach (string item in filePaths)
        {
            MergeFiles(temppath, item);
        }

        File.Copy(temppath, perpath, true);
        File.Delete(temppath);
        return "success";
    }



    private static void MergeFiles(string file1, string file2)
    {

        FileStream fs1 = null;
        FileStream fs2 = null;
        try
        {
            fs1 = System.IO.File.Open(file1, FileMode.Append);
            fs2 = System.IO.File.Open(file2, FileMode.Open);
            byte[] fs2Content = new byte[fs2.Length];
            fs2.Read(fs2Content, 0, (int)fs2.Length);
            fs1.Write(fs2Content, 0, (int)fs2.Length);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message + " : " + ex.StackTrace);
        }
        finally
        {
            fs1.Close();
            fs2.Close();
            System.IO.File.Delete(file2);
        }

    }


    /****************************************************************************************/

    public static int count2 = 1;
    [WebMethod]
    public string MultiUploadPdf()
    {
        count2++;
        var filename = count2.ToString();
        var chunks = HttpContext.Current.Request.InputStream;
        string path = Server.MapPath("~/PDF/temp");
        string newpath = Path.Combine(path, filename);

        using (System.IO.FileStream fs = System.IO.File.Create(newpath))
        {
            byte[] bytes = new byte[77570];
            int bytesRead;
            while ((bytesRead = HttpContext.Current.Request.InputStream.Read(bytes, 0, bytes.Length)) > 0)
            {

                fs.Write(bytes, 0, bytesRead);
            }
        }
        return "test";
    }

    [WebMethod]
    public string UploadCompletePdf(string fileName)
    {
        string path = Server.MapPath("~/PDF/temp");
        string temppath = Path.Combine(path, fileName);
        string perapath = Server.MapPath("~/PDF");
        string perpath = Path.Combine(perapath, fileName);
        string[] filePaths = Directory.GetFiles(path).OrderByDescending(d => new FileInfo(d).CreationTime).Reverse().ToArray();

        foreach (string item in filePaths)
        {
            MergePdfFiles(temppath, item);
        }

        File.Copy(temppath, perpath, true);
        File.Delete(temppath);
        return "success";
    }



    private static void MergePdfFiles(string file1, string file2)
    {

        FileStream fs1 = null;
        FileStream fs2 = null;
        try
        {
            fs1 = System.IO.File.Open(file1, FileMode.Append);
            fs2 = System.IO.File.Open(file2, FileMode.Open);
            byte[] fs2Content = new byte[fs2.Length];
            fs2.Read(fs2Content, 0, (int)fs2.Length);
            fs1.Write(fs2Content, 0, (int)fs2.Length);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message + " : " + ex.StackTrace);
        }
        finally
        {
            fs1.Close();
            fs2.Close();
            System.IO.File.Delete(file2);
        }

    }
    [WebMethod]
    public EstiamteChart GetEstimateChartData()
    {
        AdminRepository repo = new AdminRepository(new AkalAcademy.DataContext());
        return repo.GetEstimateChartData();
    }

    [WebMethod]
    public DrawingChart GetDrawingChartData()
    {
        AdminRepository repo = new AdminRepository(new AkalAcademy.DataContext());
        return repo.GetDrawingChartData();
    }
}
