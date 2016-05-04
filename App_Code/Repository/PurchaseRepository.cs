using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AkalAcademy;

/// <summary>
/// Summary description for PurchaseRepository
/// </summary>
public class PurchaseRepository
{

    private DataContext _context;

    public PurchaseRepository(DataContext context)
    {
        _context = context;
    }

    public IList<Material> GetMaterialItemsByEstID(int Estid)
    {
        var Materials = (from EMR in _context.EstimateAndMaterialOthersRelations
                         join x in _context.Material on EMR.MatId equals x.MatId
                         where EMR.EstId == Estid
                         select new
                         {
                             MatId = x.MatId,
                             MatName = x.MatName,

                         }).AsEnumerable().Select(x => new Material
                         {
                             MatId = x.MatId,
                             MatName = x.MatName,
                         }).OrderByDescending(m => m.MatName).Reverse().ToList();


        return Materials;
    }

    public void RejectMaterialItemByID(int EMRID, int estID)
    {
        EstimateAndMaterialOthersRelations RejectMaterialItem = _context.EstimateAndMaterialOthersRelations.Where(v => v.Sno == EMRID).FirstOrDefault();
        RejectMaterialItem.IsApproved = false;
        _context.Entry(RejectMaterialItem).State = EntityState.Modified;
        _context.SaveChanges();

        Estimate estimate = _context.Estimate.Where(v => v.EstId == estID).FirstOrDefault();
        estimate.IsItemRejected = true;
        _context.Entry(estimate).State = EntityState.Modified;
        _context.SaveChanges();


    }

    public List<MaterialsDTO> GetActiveMaterials()
    {
        List<MaterialsDTO> mt = new List<MaterialsDTO>();
        return mt = _context.Material.Where(m => m.Active == 1).AsEnumerable().Select(x => new MaterialsDTO
                         {
                             MatID = x.MatId,
                             MatName = x.MatName,
                         }).OrderByDescending(m => m.MatName).Reverse().ToList();

    }

    public List<MaterialsDTO> GetMaterials()
    {
        List<MaterialsDTO> mt = new List<MaterialsDTO>();
        return mt = _context.Material.Where(m => m.Active == 1).AsEnumerable().Select(x => new MaterialsDTO
        {
            MatID = x.MatId,
            MatName = x.MatName,
        }).OrderByDescending(m => m.MatName).Reverse().ToList();

    }

    public List<VendorInfo> GetVendorsNameList()
    {
        return _context.VendorInfo.ToList();
    }

    public List<Estimate> GetEstimateNumberList()
    {
        return _context.Estimate.ToList();
    }

    public List<EstimateAndMaterialOthersRelations> GetMaterialList(int EstimateID)
    {

        return _context.EstimateAndMaterialOthersRelations.Include(m => m.Material).Where(x => x.EstId == EstimateID).ToList();
    }

    public List<VendorInfo> GetVendorAddress(int VendorID)
    {
        return _context.VendorInfo.Where(x => x.ID == VendorID).ToList();

    }

    public List<POBillingAddress> GetDeliveryAddressList()
    {
        return _context.POBillingAddress.ToList();
    }

    public List<POBillingAddress> GetDeliveryAddressInfo(int AddressID)
    {
        return _context.POBillingAddress.Where(x => x.ID == AddressID).ToList();
    }

    public List<PurchaseOrder> GetPONumber()
    {
        return _context.PurchaseOrder.Where(x => x.CreatedOn == DateTime.Now).ToList();
    }

    public void AddNewVendorInformation(VendorInfo vendorInfo)
    {
        _context.Entry(vendorInfo).State = EntityState.Added;
        _context.SaveChanges();

    }

    public List<VendorInfoDTO> LoadActiveVendorInformation(int VendorID)
    {
        List<VendorInfo> vendorInfo = _context.VendorInfo.Where(v => v.Active == true)
                 .Include(e => e.VendorMaterialRelations).Distinct().OrderByDescending(x => x.CreatedOn).ToList();


        DataSet vendor = new DataSet();
        List<VendorInfoDTO> dto = new List<VendorInfoDTO>();

        VendorInfoDTO vendorDTO = null;
        foreach (VendorInfo v in vendorInfo)
        {
            vendorDTO = new VendorInfoDTO();

            vendorDTO.ID = v.ID;
            vendorDTO.VendorName = v.VendorName;
            vendorDTO.VendorAddress = v.VendorAddress;
            vendorDTO.VendorContactNo = v.VendorContactNo;
            vendorDTO.VendorState = v.VendorState;
            vendorDTO.VendorCity = v.VendorCity;
            vendorDTO.VendorZip = v.VendorZip;
            vendorDTO.Active = v.Active;
            vendorDTO.ModifyBy = v.ModifyBy;
            vendorDTO.ModifyOn = v.ModifyOn.ToString();
            vendorDTO.CreatedOn = v.CreatedOn.ToString();

         
            dto.Add(vendorDTO);
        }
         return dto;
    }

    public List<VendorInfoDTO> LoadInActiveVendorInformation(int VendorID)
    {
        List<VendorInfo> vendorInfo = _context.VendorInfo.Include(e => e.VendorMaterialRelations).Distinct().OrderByDescending(x => x.CreatedOn).ToList();


        DataSet vendor = new DataSet();
        List<VendorInfoDTO> dto = new List<VendorInfoDTO>();

        VendorInfoDTO vendorDTO = null;
        foreach (VendorInfo v in vendorInfo)
        {
            vendorDTO = new VendorInfoDTO();

            vendorDTO.ID = v.ID;
            vendorDTO.VendorName = v.VendorName;
            vendorDTO.VendorAddress = v.VendorAddress;
            vendorDTO.VendorContactNo = v.VendorContactNo;
            vendorDTO.VendorState = v.VendorState;
            vendorDTO.VendorCity = v.VendorCity;
            vendorDTO.VendorZip = v.VendorZip;
            vendorDTO.Active = v.Active;
            vendorDTO.ModifyBy = v.ModifyBy;
            vendorDTO.ModifyOn = v.ModifyOn.ToString();
            vendorDTO.CreatedOn = v.CreatedOn.ToString();

            dto.Add(vendorDTO);
        }

         return dto;
    }

    public VendorInfoDTO GetVendorInfoToUpdate(int VendorID)
    {
        VendorInfo vendorInfo = _context.VendorInfo.Where(v => v.ID == VendorID)
            .Include(e => e.VendorMaterialRelations)
          .FirstOrDefault();


        VendorInfoDTO dto = new VendorInfoDTO();
        dto.ID = vendorInfo.ID;
        dto.VendorName = vendorInfo.VendorName;
        dto.VendorAddress = vendorInfo.VendorAddress;
        dto.VendorContactNo = vendorInfo.VendorContactNo;
        dto.VendorState = vendorInfo.VendorState;
        dto.VendorCity = vendorInfo.VendorCity;
        dto.VendorZip = vendorInfo.VendorZip;

        dto.Active = vendorInfo.Active;

        dto.VendorMaterialRelationDTO = new List<VendorMaterialRelationDTO>();
        VendorMaterialRelationDTO vendorMatRel;
        DataSet dsmat = new DataSet();
        foreach (VendorMaterialRelation rm in vendorInfo.VendorMaterialRelations)
        {
            vendorMatRel = new VendorMaterialRelationDTO();

            vendorMatRel.ID = rm.ID;
            dsmat = DAL.DalAccessUtility.GetDataInDataSet("select MatName from Material where MatID = '" + rm.MatID + "'");
            if (dsmat.Tables[0].Rows.Count > 0)
            {
                vendorMatRel.MatName = dsmat.Tables[0].Rows[0]["MatName"].ToString();
            }

            vendorMatRel.VendorID = rm.VendorID;
            vendorMatRel.MatID = rm.MatID;
            vendorMatRel.MatType = rm.MatType;
            vendorMatRel.CreatedOn = rm.CreatedOn.ToString();
            vendorMatRel.CreatedOn = rm.ModifyOn.ToString();
            dto.VendorMaterialRelationDTO.Add(vendorMatRel);
        }

        return dto;
    }

    public void UpdateVendorInformation(VendorInfoDTO vendorInfo)
    {
        _context.VendorMaterialRelation.RemoveRange(_context.VendorMaterialRelation.Where(v => v.VendorID == vendorInfo.ID));

        VendorInfo newVendorInfo = _context.VendorInfo.Where(v => v.ID == vendorInfo.ID)
           .Include(r => r.VendorMaterialRelations)
            .FirstOrDefault();

        newVendorInfo.VendorName = vendorInfo.VendorName;
        newVendorInfo.VendorAddress = vendorInfo.VendorAddress;
        newVendorInfo.VendorContactNo = vendorInfo.VendorContactNo;
        newVendorInfo.VendorCity = vendorInfo.VendorCity;
        newVendorInfo.VendorState = vendorInfo.VendorState;
        newVendorInfo.VendorZip = vendorInfo.VendorZip;
        newVendorInfo.CreatedOn = newVendorInfo.CreatedOn;
        newVendorInfo.ModifyOn = DateTime.Now;
        newVendorInfo.Active = true;


        newVendorInfo.VendorMaterialRelations = new List<VendorMaterialRelation>();

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
                vendorMaterialRelation.ID = item.ID;
                vendorMaterialRelation.VendorID = vendorInfo.ID;
            }

            newVendorInfo.VendorMaterialRelations.Add(vendorMaterialRelation);
        }
        _context.Entry(newVendorInfo).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void VendorInfoToDelete(int vendorID)
    {
        _context.VendorMaterialRelation.RemoveRange(_context.VendorMaterialRelation.Where(v => v.VendorID == vendorID));

        VendorInfo DelVendorinfo = _context.VendorInfo.Where(v => v.ID == vendorID)
            .Include(r => r.VendorMaterialRelations)
            .FirstOrDefault();
        DelVendorinfo.VendorMaterialRelations = null;
        DelVendorinfo.Active = false;
        _context.Entry(DelVendorinfo).State = EntityState.Modified;
        _context.SaveChanges();
    }
}