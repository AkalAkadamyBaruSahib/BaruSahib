using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for PurchaseControler
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class PurchaseControler : System.Web.Services.WebService
{

    public PurchaseControler()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public IList<Material> GetMaterialItemsByEstID(int EstID)
    {
        PurchaseRepository purRepository = new PurchaseRepository(new AkalAcademy.DataContext());
        return purRepository.GetMaterialItemsByEstID(EstID);
    }
    [WebMethod]
    public void RejectMaterialItemByID(int EMRID, int EstID)
    {
        PurchaseRepository purRepository = new PurchaseRepository(new AkalAcademy.DataContext());
        purRepository.RejectMaterialItemByID(EMRID, EstID);
    }
    [WebMethod]
    public void DeleteVendorInfo(int VID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        repository.DeleteVendorInfo(VID);
    }

    [WebMethod]
    public List<string> GetActiveMaterials()
    {
        List<string> arrMaterials = new List<string>();
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        List<MaterialsDTO> materials = repository.GetActiveMaterials();
        foreach (MaterialsDTO dto in materials)
        {
            arrMaterials.Add(dto.MatName.Trim());
        }
        return arrMaterials;
    }

    [WebMethod]
    public List<VendorInfo> GetVendorsNameList()
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetVendorsNameList();
    }

    [WebMethod]
    public List<Estimate> GetEstimateNumberList()
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetEstimateNumberList();
    }

    [WebMethod]
    public List<EstimateAndMaterialOthersRelations> GetMaterialList(int EstimateID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetMaterialList(EstimateID);
    }
}
