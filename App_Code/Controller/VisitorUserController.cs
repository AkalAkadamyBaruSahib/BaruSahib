using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for VisitorUserController
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class VisitorUserController : System.Web.Services.WebService {

    public VisitorUserController () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<VisitorsDTO> GetVisitorInformation(string from, string to)
    {
        VisitorUserRepository repository = new VisitorUserRepository(new AkalAcademy.DataContext());
        return repository.GetVisitorInformation(Convert.ToDateTime(from), Convert.ToDateTime(to));

    }

    [WebMethod]
    public List<VisitorsDTO> GetVisitorInformationByVisitorTypeID(int visitorTypeID)
    {
        VisitorUserRepository repository = new VisitorUserRepository(new AkalAcademy.DataContext());
        return repository.GetVisitorInformationByVisitorTypeID(visitorTypeID);

    }

    [WebMethod]
    public List<BuildingName> GetBuildingNameList()
    {
        VisitorUserRepository repository = new VisitorUserRepository(new AkalAcademy.DataContext());
        return repository.GetBuildingNameList();
    }

    [WebMethod]
    public List<RoomNumbers> GetAvailableRoomList(int BuildingID)
    {
        VisitorUserRepository repository = new VisitorUserRepository(new AkalAcademy.DataContext());
        return repository.GetAvailableRoomList(BuildingID);
    }

    [WebMethod]
    public string GetBookedRoomList(int BuildingID)
    {
        VisitorUserRepository repository = new VisitorUserRepository(new AkalAcademy.DataContext());
        return repository.GetBookedRoomList(BuildingID);
    }

    [WebMethod]
    public List<RoomNumbers> GetBookedRoomsByVisitorID(int VisitorID)
    {
        VisitorUserRepository repository = new VisitorUserRepository(new AkalAcademy.DataContext());
        return repository.GetBookedRoomsByVisitorID(VisitorID);
    }

    [WebMethod]
    public List<RoomNumbers> GetRoomList(int BuildingID, int VisitorType)
    {
        VisitorUserRepository repository = new VisitorUserRepository(new AkalAcademy.DataContext());
        return repository.GetRoomList(BuildingID, VisitorType);
    }

    [WebMethod]
    public void CheckOutVisitor(int VisitorID)
    {
        VisitorUserRepository repository = new VisitorUserRepository(new AkalAcademy.DataContext());
        repository.CheckOutVisitor(VisitorID);
    }

    [WebMethod]
    public VisitorsDTO GetVisitorInfoByVisitorId(int VisitorID)
    {
        VisitorUserRepository repository = new VisitorUserRepository(new AkalAcademy.DataContext());
        return repository.GetVisitorInfoByVisitorId(VisitorID);
    }

    [WebMethod]
    public List<City> GetCityByStateID(int stateID)
    {
        VisitorUserRepository repository = new VisitorUserRepository(new AkalAcademy.DataContext());
        return repository.GetCityByStateID(stateID);
    }
    
}
