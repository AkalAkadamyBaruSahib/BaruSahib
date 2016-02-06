using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AkalAcademy;

/// <summary>
/// Summary description for InchargeController
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class InchargeController : System.Web.Services.WebService {

    public InchargeController () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    public List<Incharge> GetUsersByUserType(int UserTypeID)
    {
        UsersRepository usersRepository = new UsersRepository(new DataContext());
        return usersRepository.GetUsersByUserType(UserTypeID);
    }

    [WebMethod]
    public List<string> GetUsersByUserTypeAndAcademic(int UserTypeID, int AcaID)
    {
        UsersRepository usersRepository = new UsersRepository(new DataContext());
        return usersRepository.GetUsersByUserTypeAndAcademic(UserTypeID, AcaID);
    }
    
}
