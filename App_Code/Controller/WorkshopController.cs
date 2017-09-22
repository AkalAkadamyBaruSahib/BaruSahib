using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
/// <summary>
/// Summary description for WorkshopController
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WorkshopController : System.Web.Services.WebService
{

    public WorkshopController()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<WorkshopStoreMaterial> GetWorkShopMaterials(int AcaID)
    {
        WorkshopRepository repository = new WorkshopRepository(new AkalAcademy.DataContext());
        return repository.GetWorkShopMaterials(AcaID);
    }

    [WebMethod]
    public void UpdateWorkshopMaterial(WorkshopStoreMaterialDTO workshopStoreMaterialDTO)
    {
        WorkshopRepository repository = new WorkshopRepository(new AkalAcademy.DataContext());
        WorkshopStoreMaterial workshopStoreMaterial = new WorkshopStoreMaterial();
        repository.UpdateWorkshopMaterial(workshopStoreMaterialDTO);
    }

    [WebMethod]
    public List<Estimate> GetAcademyNameByEstId(int EstimateID)
    {
        WorkshopRepository repository = new WorkshopRepository(new AkalAcademy.DataContext());
        return repository.GetAcademyNameByEstId(EstimateID);
    }

    [WebMethod]
    public void ReturnEstimateMaterial(int EMRID)
    {
        WorkshopRepository workRepository = new WorkshopRepository(new AkalAcademy.DataContext());
        workRepository.ReturnEstimateMaterial(EMRID);
    }

    [WebMethod]
    public void RejectEstimate(int EstID)
    {
        WorkshopRepository workRepository = new WorkshopRepository(new AkalAcademy.DataContext());
        workRepository.RejectEstimate(EstID);
    }

    [WebMethod]
    public List<WorkshopBills> GetBillDetails(int EstID)
    {
        WorkshopRepository workRepository = new WorkshopRepository(new AkalAcademy.DataContext());
        return workRepository.GetBillDetails(EstID);
    }

    [WebMethod]
    public int WorkshopBillToDelete(int BillID)
    {
        WorkshopRepository repository = new WorkshopRepository(new AkalAcademy.DataContext());
        return repository.WorkshopBillToDelete(BillID);
    }
}