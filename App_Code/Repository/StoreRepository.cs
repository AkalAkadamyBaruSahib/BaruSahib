using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AkalAcademy;

/// <summary>
/// Summary description for Store
/// </summary>
public class StoreRepository
{
    private DataContext _context;

    public StoreRepository(DataContext context)
    {
        _context = context;
    }

    public void SaveStoreBill(StoreMaterialBill StoreMaterialBill)
    {
        _context.Entry(StoreMaterialBill).State = System.Data.Entity.EntityState.Added;
        _context.SaveChanges();
    }

    public List<StoreMaterialBill> GetBillDetails(int EstID)
    {
        List<StoreMaterialBill> storeMaterialBill = _context.StoreMaterialBill.Where(s => s.EstID == EstID).ToList();
        return storeMaterialBill;
    }

    public List<VendorInfo> GetVendorsNameList(int matID)
    {
        var store = (from ven in _context.VendorInfo
                     join vm in _context.VendorMaterialRelation on ven.ID equals vm.VendorID
                     where vm.MatID == matID
                     select new
                     {
                         ID = ven.ID,
                         VendorName = ven.VendorName
                     }).AsEnumerable().Select(v => new VendorInfo
                {
                    ID = v.ID,
                    VendorName = v.VendorName
                }).ToList();

        store = store.GroupBy(test => test.ID)
                  .Select(grp => grp.First())
                  .ToList();
        return store;
    }

    public DataSet GetStockMaterialInfo(int EstID, int PSID)
    {
        return DAL.DalAccessUtility.GetDataInDataSet("exec [USP_StockMaterialDetails]'" + EstID + "','2'");
    }

    public DataSet GetStoreMaterialBillInfo(int EstID, int EMRID)
    {
        return DAL.DalAccessUtility.GetDataInDataSet("exec [USP_StoreBillDetails]'" + EstID + "','" + EMRID + "'");
    }

    public List<Estimate> GetStockRegisterInfoByAcaID(int AcaID)
    {
        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.AcaId == AcaID)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e => e.ModifyOn).ToList();


        List<Estimate> estimates = new List<Estimate>();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == 2 && er.EstId == e.EstId)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();

            e.SanctionDate = e.ModifyOn;

            if (estimateRelation.Count > 0)
            {
                e.EstimateAndMaterialOthersRelations = estimateRelation;
                estimates.Add(e);
            }
        }
        ests = null;
        return estimates;
    }

    public List<Estimate> GetStockRegisterInfo()
    {
        DateTime dt1 = DateTime.Now.AddDays(-7);


        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.CreatedOn >= dt1)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e => e.ModifyOn).ToList();


        List<Estimate> estimates = new List<Estimate>();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == 2 && er.EstId == e.EstId)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();

            e.SanctionDate = e.ModifyOn;

            if (estimateRelation.Count > 0)
            {
                e.EstimateAndMaterialOthersRelations = estimateRelation;
                estimates.Add(e);
            }
        }
        ests = null;
        return estimates;
    }
}