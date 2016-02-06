using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AkalAcademy;
using System.Data;

/// <summary>
/// Summary description for ConstructionUserController
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ConstructionUserController : System.Web.Services.WebService
{

    public ConstructionUserController()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string GetMaterialDetails(int MatID, int WorkAllotID)
    {
        ConstructionUserRepository constructionUserRepository = new ConstructionUserRepository(new DataContext());
        return constructionUserRepository.GetMaterialDetails(MatID, WorkAllotID);
    }
}
