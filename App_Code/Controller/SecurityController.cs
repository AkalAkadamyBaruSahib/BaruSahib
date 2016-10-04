using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for SecurityController
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class SecurityController : System.Web.Services.WebService {

    public SecurityController () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<SecurityEmployeeInfoDTO> GetSecurityEmployeeInformation(int SecurityEmployeeID)
    {
        SecurityRepository repository = new SecurityRepository(new AkalAcademy.DataContext());
        return repository.GetSecurityEmployeeInformation(SecurityEmployeeID);
    }

    [WebMethod]
    public void SecurityEmployeeInfoToDelete(int SecurityEmployeeID)
    {
        SecurityRepository repository = new SecurityRepository(new AkalAcademy.DataContext());
        repository.SecurityEmployeeInfoToDelete(SecurityEmployeeID);
    }

    [WebMethod]
    public SecurityEmployeeInfoDTO GetSecurityEmployeeInfoToUpdate(int SecurityEmployeeID)
    {
        SecurityRepository repository = new SecurityRepository(new AkalAcademy.DataContext());
        return repository.GetSecurityEmployeeInfoToUpdate(SecurityEmployeeID);
    }

    [WebMethod]
    public List<Zone> GetZone()
    {
        SecurityRepository repository = new SecurityRepository(new AkalAcademy.DataContext());
        return repository.GetZone();
    }

    [WebMethod]
    public List<Academy> GetAcademybyZoneID(int ZoneID)
    {
        SecurityRepository repository = new SecurityRepository(new AkalAcademy.DataContext());
        return repository.GetAcademybyZoneID(ZoneID);
    }

    [WebMethod]
    public void SaveSecurityTransferLetter(EmployeeTransfer EmployeeTransfer)
    {
        SecurityRepository securityRepository = new SecurityRepository(new AkalAcademy.DataContext());
        securityRepository.SaveSecurityTransferLetter(EmployeeTransfer);
    }

    [WebMethod]
    public void DeleteEmployeeInfo(int EID)
    {
        SecurityRepository repository = new SecurityRepository(new AkalAcademy.DataContext());
        repository.DeleteEmployeeInfo(EID);
    }

    [WebMethod]
    public void ActiveEmployeeInfo(int EID)
    {
        SecurityRepository repository = new SecurityRepository(new AkalAcademy.DataContext());
        repository.ActiveEmployeeInfo(EID);
    }

    [WebMethod]
    public List<string> GetActiveSecurityEmployee()
    {
        List<string> arrEmp = new List<string>();
        SecurityRepository repository = new SecurityRepository(new AkalAcademy.DataContext());
        List<SecurityEmployeeInfo> employee = repository.GetActiveSecurityEmployee();
        foreach (SecurityEmployeeInfo dto in employee)
        {
            arrEmp.Add(dto.Name.Trim());
        }
        return arrEmp;
    }
}
