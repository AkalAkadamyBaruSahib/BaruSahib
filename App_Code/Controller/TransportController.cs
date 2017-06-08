using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for TransportController
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class TransportController : System.Web.Services.WebService
{

    public TransportController()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public int SaveNewTransportEmployee(VehicleEmployee VehicleEmployee)
    {
        TransportUserRepository tr = new TransportUserRepository(new AkalAcademy.DataContext());
        return tr.SaveNewTransportEmployee(VehicleEmployee);

    }

    [WebMethod]
    public void SaveTransEmpRelation(TransportEmployeeRelation TransportEmployeeRelation)
    {
        TransportUserRepository tr = new TransportUserRepository(new AkalAcademy.DataContext());
        tr.SaveTransEmpRelation(TransportEmployeeRelation);
    }

    [WebMethod]
    public List<VehicleEmployeeDTO> GeTransportEmployeeInformation(int VehicleEmployeeID)
    {
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        return repository.GeTransportEmployeeInformation(VehicleEmployeeID);

    }

    [WebMethod]
    public VehicleEmployeeDTO GetTranportEmployeeInfoToUpdate(int VehicleEmployeeID)
    {
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        return repository.GetTranportEmployeeInfoToUpdate(VehicleEmployeeID);
    }

    [WebMethod]
    public void UpdateTransportEmployeeInfo(VehicleEmployee VehicleEmployee)
    {
        TransportUserRepository tr = new TransportUserRepository(new AkalAcademy.DataContext());
        tr.UpdateTransportEmployeeInfo(VehicleEmployee);
    }

    [WebMethod]
    public void TranportEmployeeInfoToDelete(int VehicleEmployeeID)
    {
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        repository.TranportEmployeeInfoToDelete(VehicleEmployeeID);
    }

    [WebMethod]
    public void DeleteVechicleInfo(int VID)
    {
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        repository.DeleteVechicleInfo(VID);
    }

    [WebMethod]
    public void ActiveVechicleInfo(int VID)
    {
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        repository.ActiveVechicleInfo(VID);
    }

    [WebMethod]
    public List<string> GetActiveVehicles()
    {
        List<string> arrVehicles = new List<string>();
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        List<VehiclesDTO> vehicles = repository.GetActiveVehicles();
        foreach (VehiclesDTO dto in vehicles)
        {
            arrVehicles.Add(dto.Number.Trim());
        }
        return arrVehicles;
    }

    [WebMethod]
    public List<string> GetActiveVehiclesByInchargeID(int InchargeID)
    {
        List<string> arrVehicles = new List<string>();
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        List<VehiclesDTO> vehicles = repository.GetActiveVehiclesByInchargeID(InchargeID);
        foreach (VehiclesDTO dto in vehicles)
        {
            arrVehicles.Add(dto.Number.Trim());
        }
        return arrVehicles;
    }


    //[WebMethod]
    //public List<VehiclesDTO> GetContracturalVehiclesByAcaID(int AcaID)
    //{
    //    TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
    //    return repository.GetContracturalVehiclesByAcaID(AcaID);
    //}

    [WebMethod]
    public List<Academy> GetAcademyByInchargeID(int InchargeID)
    {
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        return repository.GetAcademyByInchargeID(InchargeID);
    }

    [WebMethod]
    public List<Vehicles> GetVehiclesByAcademyIDandTypeID(int AcaID, int TypeID)
    {
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        return repository.GetVehiclesByAcademyIDandTypeID(AcaID, TypeID);
    }

    [WebMethod]
    public Vehicles GetVehiclesInfoByVehicleID(int VehicleID)
    {
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        var vehicle = repository.GetVehiclesInfoByVehicleID(VehicleID);
        return vehicle;
    }
    [WebMethod]
    public VehicleEmployee GetVehicleEmployeeInfo(int VehicleID, int EmpType)
    {
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        return repository.GetVehicleEmployeeInfo(VehicleID, EmpType);
    }

    [WebMethod]
    public List<Vehicles> GetTrustVehiclesByAcademyIdAndTypeID(int AcaID, int TypeID)
    {
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        return repository.GetTrustVehiclesByAcademyIdAndTypeID(AcaID, TypeID);
    }

    [WebMethod]
    public SittingTyreRelation GetVehiclesTyreCountBySitting(int seatingCapacity)
    {
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        return repository.GetVehiclesTyreCountBySitting(seatingCapacity);
    }

    [WebMethod]
    public int SaveVehicleDetailService(VehicleServiceRecord VehicleServiceRecord)
    {
        VehicleServiceRecord.CreatedOn = Utility.GetLocalDateTime(DateTime.UtcNow);
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        return repository.SaveVehicleDetailService(VehicleServiceRecord);
    }

    [WebMethod]
    public List<VehicleServiceRecord> GetLoadVehicleServiceInformation(int inchargeID)
    {
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        return repository.GetLoadVehicleServiceInformation(inchargeID);

    }

    [WebMethod]
    public VehicleServiceRecord GetGetVehicleInfoToUpdate(int VehicleServiceID)
    {
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        return repository.GetGetVehicleInfoToUpdate(VehicleServiceID);
    }

    [WebMethod]
    public void UpdateVehicleDetailService(VehicleServiceRecord VehicleServiceRecord)
    {
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        repository.UpdateVehicleDetailService(VehicleServiceRecord);
    }
}
