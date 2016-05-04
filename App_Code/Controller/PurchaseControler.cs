using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
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
    public List<string> GetMaterials()
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

    [WebMethod]
    public List<VendorInfo> GetVendorAddress(int VendorID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetVendorAddress(VendorID);
    }

    [WebMethod]
    public List<POBillingAddress> GetDeliveryAddressList()
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetDeliveryAddressList();
    }

    [WebMethod]
    public List<POBillingAddress> GetDeliveryAddressInfo(int AddressID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetDeliveryAddressInfo(AddressID);
    }

    [WebMethod]
    public List<PurchaseOrder> GetPONumber()
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetPONumber();
    }

    [WebMethod]
    public void AddNewVendorInformation(VendorInfoDTO vendorInfo)
    {

        VendorInfo venInfo = new VendorInfo();

        venInfo.VendorAddress = vendorInfo.VendorAddress;
        venInfo.VendorName = vendorInfo.VendorName;
        venInfo.VendorContactNo = vendorInfo.VendorContactNo;
        venInfo.VendorState = vendorInfo.VendorState;
        venInfo.VendorZip = vendorInfo.VendorZip;
        venInfo.VendorCity = vendorInfo.VendorCity;
        venInfo.ModifyOn = DateTime.Now;
        venInfo.Active = vendorInfo.Active;
        venInfo.CreatedOn = DateTime.Now;
        venInfo.VendorMaterialRelations = new List<VendorMaterialRelation>();

        VendorMaterialRelation vendorMaterialRelation = new VendorMaterialRelation();
        DataSet dsmat = new DataSet();

        foreach (VendorMaterialRelationDTO item in vendorInfo.VendorMaterialRelationDTO)
        {
            vendorMaterialRelation = new VendorMaterialRelation();
            dsmat = DAL.DalAccessUtility.GetDataInDataSet("select MatTypeId,MatID from Material where MatName like '%" + item.MatName + "%'");
            if (dsmat.Tables[0].Rows.Count > 0)
            {
                vendorMaterialRelation.MatID = Convert.ToInt32(dsmat.Tables[0].Rows[0]["MatId"].ToString());
                vendorMaterialRelation.MatType = Convert.ToInt32(dsmat.Tables[0].Rows[0]["MatTypeId"].ToString());
                vendorMaterialRelation.CreatedOn = DateTime.Now;
                vendorMaterialRelation.ModifyOn = DateTime.Now;
            }

            venInfo.VendorMaterialRelations.Add(vendorMaterialRelation);
        }

        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        repository.AddNewVendorInformation(venInfo);
    }

    [WebMethod]
    public List<VendorInfoDTO> LoadActiveVendorInformation(int VendorID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.LoadActiveVendorInformation(VendorID);

    }

    [WebMethod]
    public List<VendorInfoDTO> LoadInActiveVendorInformation(int VendorID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.LoadInActiveVendorInformation(VendorID);

    }

    [WebMethod]
    public VendorInfoDTO GetVendorInfoToUpdate(int VendorID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetVendorInfoToUpdate(VendorID);
    }

    [WebMethod]
    public void UpdateVendorInformation(VendorInfoDTO VendorInfo)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        repository.UpdateVendorInformation(VendorInfo);
    }

    [WebMethod]
    public void VendorInfoToDelete(int VendorID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        repository.VendorInfoToDelete(VendorID);
    }

  
}
