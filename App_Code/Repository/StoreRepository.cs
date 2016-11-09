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

        // return DAL.DalAccessUtility.GetDataInDataSet("select * from [view_StockMaterialDetails] where estID=" + EstID + " AND PSID=" + PSID);
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

        return ests;
    }

    public List<Estimate> GetStockRegisterInfo(int PurchaseID)
    {
        DateTime dt1 = DateTime.Now.AddDays(-7);

        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.CreatedOn >= dt1)
            .Include(z => z.Zone)
            .Include(a => a.Academy)
            .Where(x => x.EstimateAndMaterialOthersRelations.Any(er => er.PSId == PurchaseID))
            .OrderByDescending(e => e.ModifyOn).ToList();

        return ests;
    }

    public int StoreBillToDelete(int BillID)
    {
        StoreMaterialBill DelStoreMaterialBill = _context.StoreMaterialBill.Where(v => v.ID == BillID).FirstOrDefault();
        _context.Entry(DelStoreMaterialBill).State = EntityState.Deleted;
        _context.SaveChanges();
        return DelStoreMaterialBill.EstID;
    }

    public List<StoreMaterialBill> GetMaterialBillList(int estID)
    {
        return _context.StoreMaterialBill.Where(x => x.EstID == estID).ToList();
    }

    public List<Estimate> GetStockMaterialInfo(int EstID)
    {
        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.EstId == EstID)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e => e.ModifyOn).ToList();

        return ests;
    }

    public decimal? SaveStroeMaterialDetail(StockEntry stockentry)
    {
        _context.StockEntry.Add(stockentry);
        _context.SaveChanges();
        return GetReceivedQuantity(stockentry.EMRID);
    }

    public decimal? SaveDisatchMaterialDetail(StockDispatchEntry stockdispatchentry)
    {
        _context.StockDispatchEntry.Add(stockdispatchentry);
        _context.SaveChanges();

        return GetReceivedQuantity(stockdispatchentry.EMRID);
    }

   
    private decimal? GetReceivedQuantity(int? emrID)
    {
        decimal? receivedQty = (from qty in _context.StockEntry
                                     where qty.EMRID == emrID
                                     select (decimal?)qty.Quantity).Sum();

        decimal? disatchQty = (from qty in _context.StockDispatchEntry
                               where qty.EMRID == emrID
                               select (decimal?)qty.DispatchQuantity).Sum();
        if (disatchQty == null)
        {
            disatchQty = 0;
        }
        if (receivedQty == null)
        {
            receivedQty = 0;
        }

        decimal? remainQty = receivedQty - disatchQty;
        return remainQty;
    }
}