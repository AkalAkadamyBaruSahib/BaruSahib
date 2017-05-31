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
    public List<Material> GetMaterialItemsByEstID(int EstID)
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
    public List<string> GetMaterialsBySourceTypeID(int sourceTypeID)
    {
        List<string> arrMaterials = new List<string>();
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        List<MaterialsDTO> materials = repository.GetMaterialsBySourceTypeID(sourceTypeID);
        foreach (MaterialsDTO dto in materials)
        {
            arrMaterials.Add(dto.MatName.Trim());
        }
        return arrMaterials;
    }

    [WebMethod]
    public List<MaterialsDTO> GetMaterialsBySourceTypeIDList(int sourceTypeID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetMaterialsBySourceTypeID(sourceTypeID);
    }

    [WebMethod]
    public List<string> GetActiveMaterialsForAutoFill()
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
    public List<MaterialType> GetActiveMaterialTypes()
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetActiveMaterialTypes();
    }

    [WebMethod]
    public List<MaterialsDTO> GetActiveMaterials()
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        List<MaterialsDTO> materials = repository.GetActiveMaterials();
        return materials;
    }

    [WebMethod]
    public List<VendorInfo> GetVendorsNameList()
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetVendorsNameList();
    }

    [WebMethod]
    public List<EstimateDTO> GetEstimateNumberList(int purchaseSourceID, int inchargeID = 0)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetEstimateNumberList(inchargeID, purchaseSourceID);
    }

    [WebMethod]
    public List<EstimateAndMaterialOthersRelations> GetMaterialList(int EstimateID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetMaterialList(EstimateID);
    }

    [WebMethod]
    public List<VendorInfo> GetVendorAddress(int vendorID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetVendorAddress(vendorID);
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
    public List<PurchaseOrderDetail> GetPONumber()
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetPONumber();
    }

    [WebMethod]
    public string AddNewVendorInformation(VendorInfoDTO vendorInfo)
    {

        VendorInfo venInfo = new VendorInfo();

        venInfo.VendorAddress = vendorInfo.VendorAddress;
        venInfo.VendorName = vendorInfo.VendorName;
        venInfo.VendorContactNo = vendorInfo.VendorContactNo;
        venInfo.VendorState = Convert.ToInt32(vendorInfo.VendorState);
        venInfo.VendorZip = vendorInfo.VendorZip;
        venInfo.VendorCity = Convert.ToInt32(vendorInfo.VendorCity);
        venInfo.BankName = vendorInfo.BankName;
        venInfo.IfscCode = vendorInfo.IfscCode;
        venInfo.PanNumber = vendorInfo.PanNumber;
        venInfo.TinNumber = vendorInfo.TinNumber;
        venInfo.AccountNumber = vendorInfo.AccountNumber;
        venInfo.ModifyOn = DateTime.Now;
        venInfo.ModifyBy = Convert.ToInt32(vendorInfo.ModifyBy);
        venInfo.Active = vendorInfo.Active;
        venInfo.CreatedOn = DateTime.Now;
        venInfo.AltrenatePhoneNumber = vendorInfo.AltrenatePhoneNumber;
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
        return repository.AddNewVendorInformation(venInfo);
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

    [WebMethod]
    public List<MaterialType> GetBindMaterialType()
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetBindMaterialType();
    }

    [WebMethod]
    public List<MaterialsDTO> GetMatDetailByMaterialType(int MatTypeID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetBindMaterialNameByMaterialType(MatTypeID);
    }

    [WebMethod]
    public List<MaterialsDTO> GetBindMaterialByMaterialName(string matName)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetBindMaterialByMaterialName(matName.Trim());
    }

    [WebMethod]
    public List<string> GetBindMaterialNameByMaterialType(int MatTypeID)
    {
        List<string> arrMaterials = new List<string>();
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        List<MaterialsDTO> materials = repository.GetBindMaterialNameByMaterialType(MatTypeID);
        foreach (MaterialsDTO dto in materials)
        {
            arrMaterials.Add(dto.MatName.Trim());
        }
        return arrMaterials;
    }

    [WebMethod]
    public List<Material> GeMaterialInformation(int MaterialID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GeMaterialInformation(MaterialID);
    }

    [WebMethod]
    public List<MaterialsDTO> GetMaterialsByID(string materialList)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetMaterialsByID(materialList);
    }

    [WebMethod]
    public List<PurchaseSource> GetPurchaseSource()
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetPurchaseSource();
    }


    [WebMethod]
    public int SaveEstimateDetail(Estimate estimate)
    {
        if (estimate.IsApproved == true)
        {
            estimate.SanctionDate = Utility.GetLocalDateTime(DateTime.UtcNow);
        }
        estimate.CreatedOn = Utility.GetLocalDateTime(DateTime.UtcNow);

        foreach (EstimateAndMaterialOthersRelations relation in estimate.EstimateAndMaterialOthersRelations)
        {
            relation.CreatedOn = Utility.GetLocalDateTime(DateTime.UtcNow);
            relation.ModifyOn = Utility.GetLocalDateTime(DateTime.UtcNow);
        }

        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.SaveEstimateDetail(estimate);
    }

    [WebMethod]
    public List<Zone> GetZone()
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetZone();
    }

    [WebMethod]
    public List<BucketName> GetBucketName(int inchargeID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetBucketName(inchargeID);
    }

    [WebMethod]
    public List<Academy> GetAcademybyZoneID(int ZoneID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetAcademybyZoneID(ZoneID);
    }


    [WebMethod]
    public List<WorkAllot> GetWorkAllotByAcademyID(int AcademyID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetWorkAllotByAcademyID(AcademyID);
    }

    [WebMethod]
    public List<Zone> GetZoneByInchargeID(int InchargeID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetZoneByInchargeID(InchargeID);
    }

    [WebMethod]
    public List<Zone> GetDrawingZone()
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetDrawingZone();
    }


    [WebMethod]
    public List<DrawingType> GetDrawingType()
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetDrawingType();
    }

    [WebMethod]
    public List<SubDrawingTypes> GetSubDrawingByDrawingID(int DrawingID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetSubDrawingByDrawingID(DrawingID);
    }


    [WebMethod]
    public void SaveDrawingDetail(Drawing drawing)
    {
        drawing.ModifyOn = DateTime.UtcNow;
        drawing.CreatedOn = DateTime.UtcNow;

        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        repository.SaveDrawingDetail(drawing);
    }


    [WebMethod]
    public List<DrawingDTO> GeTDrawingInformation(int DrawingID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GeTDrawingInformation(DrawingID);

    }

    [WebMethod]
    public DrawingDTO GetDrawingInfoToUpdate(int DrawingID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetDrawingInfoToUpdate(DrawingID);
    }

    [WebMethod]
    public void UpdateDrawingInformation(Drawing Drawing)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        repository.UpdateDrawingInformation(Drawing);
    }

    [WebMethod]
    public List<DrawingDTO> GeTDrawingInformationByInchargeID(int InchargeID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GeTDrawingInformationByInchargeID(InchargeID);
    }

    [WebMethod]
    public List<MaterialType> GetBindMaterialTypeInTransport()
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetBindMaterialTypeInTransport();
    }

    [WebMethod]
    public List<Academy> GetAcademy()
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetAcademy();
    }

    [WebMethod]
    public List<Estimate> EstimateViewForPurchase(int PSID, int InchrgID, int UserTypeID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.EstimateViewForPurchase(PSID,UserTypeID ,InchrgID);
    }
    [WebMethod]
    public List<Estimate> EstimateViewForPurchaseByAcaID(int PSID,int UserTypeID,int InchrgID,int AcaID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.EstimateViewForPurchaseByAcaID(PSID, UserTypeID, InchrgID, AcaID);
    }

    //[WebMethod]
    //public List<Estimate> EstimateViewForPurchaseByEmployeeID(int PSID, int UserTypeID, int InchrgID)
    //{
    //    PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
    //    return repository.EstimateViewForPurchaseByEmployeeID(PSID, UserTypeID,InchrgID);
    //}

    //[WebMethod]
    //public List<Estimate> EstimateViewForPurchaseByEmployeeIDByAcaID(int PSID, int UserTypeID, int InchrgID, int AcaID)
    //{
    //    PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
    //    return repository.EstimateViewForPurchaseByEmployeeIDByAcaID(PSID, UserTypeID, InchrgID, AcaID);
    //}

    [WebMethod]
    public List<Estimate> MaterialDepatchStatus(int PSID, int UserTypeID, int InchrgID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.MaterialDepatchStatus(PSID, UserTypeID,InchrgID);
    }

    [WebMethod]
    public List<Estimate> MaterialDepatchStatusByAcaID(int PSID,int InchrgID, int AcaID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.MaterialDepatchStatusByAcaID(PSID, InchrgID, AcaID);
    }

    [WebMethod]
    public List<Academy> GetAcademybyZoneIDByEmpID(int ZoneID, int InchargeID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetAcademybyZoneIDByEmpID(ZoneID, InchargeID);
    }
    [WebMethod]
    public void ReceivedMaterial(int EstID, int InchargeID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        repository.ReceivedMaterial(EstID, InchargeID);
    }

    [WebMethod]
    public List<string> GetActiveVendorForAutoFill()
    {
        List<string> arrVendors = new List<string>();
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        List<VendorInfoDTO> vendors = repository.GetActiveVendor();
        foreach (VendorInfoDTO dto in vendors)
        {
            arrVendors.Add(dto.VendorName.Trim());
        }
        return arrVendors;
    }

    [WebMethod]
    public List<VendorInfoDTO> GetActiveVendorObjectForAutoFill()
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetActiveVendor();
    }

    [WebMethod]
    public List<VendorInfo> GetDuplicateVendor(string vendorName, string vendorMobilePhone, string vendorLandlinePhone)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetDuplicateVendor(vendorName, vendorMobilePhone, vendorLandlinePhone);
    }

    [WebMethod]
    public List<GetBillDetailsByVendorID> GetAgencyMaterialDetails(int VendorID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetAgencyMaterialDetails(VendorID);
    }


    [WebMethod]
    public List<view_BillsApprovalForAdmin> GetBillForApprovalDetails(int acaID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetBillForApprovalDetails(acaID);
    }

    [WebMethod]
    public List<view_BillsApprovalForAdmin> GetBillStatusDetails()
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetBillStatusDetails();
    }
    [WebMethod]
    public List<MaterialsDTO> GetActiveMaterialsByMatTypeID(int MatTypeID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetActiveMaterialsByMatTypeID(MatTypeID);
    }

    [WebMethod]
    public void AddNewBucketInformation(BucketName bucketName)
    {
        foreach (EstimateBucketMaterialRelation relation in bucketName.EstimateBucketMaterialRelation)
        {
            relation.CreatedOn = Utility.GetLocalDateTime(DateTime.UtcNow);
        }

        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        repository.AddNewBucketInformation(bucketName);
    }

    [WebMethod]
    public List<EstimateBucketDTO> GetBucketInformation(int inchargeID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetBucketInformation(inchargeID);
    }
    [WebMethod]
    public BucketName GetBucketInfoToUpdate(int estBucketID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetBucketInfoToUpdate(estBucketID);
    }

    [WebMethod]
    public void UpdateBucketInformation(BucketName bucketName)
    {
        foreach (EstimateBucketMaterialRelation relation in bucketName.EstimateBucketMaterialRelation)
        {
            relation.CreatedOn = Utility.GetLocalDateTime(DateTime.UtcNow);
        }

        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        repository.UpdateBucketInformation(bucketName);
    }

    [WebMethod]
    public BucketName GetBucketInfoByBucketID(int buckID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetBucketInfoByBucketID(buckID);
    }

    [WebMethod]
    public List<MaterialsDTO> GetBindMaterialByMaterialID(int matID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetBindMaterialByMaterialID(matID);
    }
    [WebMethod]
    public void BucketInfoToDelete(int bucketID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        repository.BucketInfoToDelete(bucketID);
    }

    [WebMethod]
    public RateNonApprovedChart GetRateNonApprovedChartData()
    {
        PurchaseRepository repo = new PurchaseRepository(new AkalAcademy.DataContext());
        return repo.GetRateNonApprovedChartData();
    }

    [WebMethod]
    public List<PONumber> GetPONumberList()
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetPONumberList();
    }
    [WebMethod]
    public List<PurchaseOrderDetail> GetPurchaserOrderDetailByPONumber(int purchaserOrderID)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetPurchaserOrderDetailByPONumber(purchaserOrderID);

    }

    [WebMethod]
    public List<EstimateAndMaterialOthersRelations> GetMaterialDetailList(int sno)
    {
        PurchaseRepository repository = new PurchaseRepository(new AkalAcademy.DataContext());
        return repository.GetMaterialDetailList(sno);
    }
}
