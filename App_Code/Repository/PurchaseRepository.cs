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

    public void AddNewVendorInformation(VendorInfo vendorInfo)
    {
        _context.Entry(vendorInfo).State = EntityState.Added;
        _context.SaveChanges();

    }

    public void DeleteVendorInfo(int VID)
    {
        VendorInfo vendorinfo = _context.VendorInfo.Where(v => v.ID == VID)
                             .FirstOrDefault();
        vendorinfo.Active = false;
        _context.Entry(vendorinfo).State = EntityState.Modified;
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

        var Materialsname = _context.EstimateAndMaterialOthersRelations.Include(m => m.Material).Where(x => x.EstId == EstimateID).ToList();


        return Materialsname;
        //return _context.EstimateAndMaterialOthersRelations.Where(r => r.EstId == EstimateID).ToList();
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
}