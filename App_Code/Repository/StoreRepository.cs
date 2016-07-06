using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

    //public DataSet GetStockRegisterInfo()
    //{
    //    //return DAL.DalAccessUtility.GetDataInDataSet("exec [USP_StckRegister]");
    //    return DAL.DalAccessUtility.GetDataInDataSet("exec [USP_NewStckRegister]");
    //}

    public DataSet GetStockMaterialInfo(int EstID, int PSID)
    {
        return DAL.DalAccessUtility.GetDataInDataSet("exec [USP_StockMaterialDetails]'" + EstID + "','2'");
    }

    public DataSet GetStoreMaterialBillInfo(int EstID, int EMRID)
    {
        return DAL.DalAccessUtility.GetDataInDataSet("exec [USP_StoreBillDetails]'" + EstID + "','" + EMRID + "'");
    }

    public List<StockRegister> GetStockRegisterInfo()
    {
        DateTime dt1 = DateTime.Now.AddDays(-15).Date;
        

         var StockRgs = (from E in _context.Estimate
                        join ER in _context.EstimateAndMaterialOthersRelations on E.EstId equals ER.EstId
                        join Z in _context.Zone on E.ZoneId equals Z.ZoneId
                        join A in _context.Academy on E.AcaId equals A.AcaID
                        where E.IsApproved == true && ER.PSId == 2
                        && E.CreatedOn >= dt1 
                        select new
                        {
                            SubEstimate = E.SubEstimate,
                            AcaID = A.AcaID,
                            CreatedOn = E.CreatedOn,
                            SanctionDate = E.ModifyOn,
                            ZoneName = Z.ZoneName,
                            AcaName = A.AcaName,
                            EstId = E.EstId,
                            PSId = ER.PSId
                        }).AsEnumerable().Select(x => new StockRegister
                             {
                                 SubEstimate = x.SubEstimate,
                                 AcaID = x.AcaID,
                                 CreatedOn = Convert.ToDateTime(x.CreatedOn),
                                 SanctionDate = Convert.ToDateTime(x.SanctionDate),
                                 ZoneName = x.ZoneName,
                                 AcaName = x.AcaName,
                                 EstId = x.EstId,
                                 PSId = x.PSId,
                               }).ToList();
        StockRgs = StockRgs.GroupBy(test => test.EstId)
                  .Select(grp => grp.First())
                  .ToList();
        return StockRgs;
    }

    public List<StockRegister> GetStockRegisterInfoByAcaID(int AcaID)
    {
        var StockRgs = (from E in _context.Estimate
                        join ER in _context.EstimateAndMaterialOthersRelations on E.EstId equals ER.EstId
                        join Z in _context.Zone on E.ZoneId equals Z.ZoneId
                        join A in _context.Academy on E.AcaId equals A.AcaID
                        where E.IsApproved == true && ER.PSId == 2 && E.AcaId == @AcaID
                        select new
                        {
                            SubEstimate = E.SubEstimate,
                            AcaID = A.AcaID,
                            CreatedOn = E.CreatedOn,
                            SanctionDate = E.ModifyOn,
                            ZoneName = Z.ZoneName,
                            AcaName = A.AcaName,
                            EstId = E.EstId,
                            PSId = ER.PSId
                        }).AsEnumerable().Select(x => new StockRegister
                        {
                            SubEstimate = x.SubEstimate,
                            AcaID = x.AcaID,
                            CreatedOn = Convert.ToDateTime(x.CreatedOn),
                            SanctionDate = Convert.ToDateTime(x.SanctionDate),
                            ZoneName = x.ZoneName,
                            AcaName = x.AcaName,
                            EstId = x.EstId,
                            PSId = x.PSId,
                        }).ToList();
        StockRgs = StockRgs.GroupBy(test => test.EstId)
                  .Select(grp => grp.First())
                  .ToList();
        return StockRgs;
    }


    //public List<StockMaterialDetail> GetStockMaterialInfo(int EstID, int PSID)
    //{
    //    var StockMaterial = (from E in _context.Estimate
    //                         join ER in _context.EstimateAndMaterialOthersRelations on E.EstId equals ER.EstId
    //                         join M in _context.Material on ER.MatId equals M.MatId
    //                         join U in _context.Unit on ER.UnitId equals U.UnitId
    //                         join P in _context.PurchaseSource on ER.PSId equals P.PSId
    //                         from SE in _context.StockEntry.Where(SE => SE.EMRID == ER.Sno).DefaultIfEmpty()
    //                         from SMB in _context.StoreMaterialBill.Where(SMB => SMB.EstID == E.EstId && SMB.BillNo == SE.BillPath).DefaultIfEmpty()
    //                         where E.EstId == EstID && ER.PSId == 2
    //                         select new
    //                         {
    //                             Sno = ER.Sno,
    //                             MatName = M.MatName,
    //                             UnitName = U.UnitName,
    //                             Qty = ER.Qty,
    //                             PSName = P.PSName,
    //                             Rate = ER.Rate,
    //                             ReceivedRate = ER.Rate,
    //                             ReceivedOn = ER.ModifyOn,
    //                             MatId = ER.MatId,
    //                             PurchaseQty = ER.PurchaseQty,
    //                             InStoreQuantity = (from SER in _context.StockEntry select SER.Quantity).Sum(),
    //                             DispatchQuantity = (from SDE in _context.StockDispatchEntry select SDE.DispatchQuantity).Sum(),
    //                         }).AsEnumerable().Select(x => new StockMaterialDetail
    //                               {
    //                                   Sno = x.Sno,
    //                                   MatName = x.MatName,
    //                                   UnitName = x.UnitName,
    //                                   Qty = x.Qty,
    //                                   PSName = x.PSName,
    //                                   Rate = x.Rate,
    //                                   //ReceivedRate = x.Rate,
    //                                   //ReceivedOn = x.ModifyOn,
    //                                   MatId = x.MatId,
    //                                  // InStoreQuantity = (from SER in _context.StockEntry select SER.Quantity).Sum(),
    //                                 //  DispatchQuantity = (from SDE in _context.StockDispatchEntry select SDE.DispatchQuantity).Sum(),
    //                                   PurchaseQty = x.PurchaseQty
    //                               }).ToList();
    //    StockMaterial = StockMaterial.GroupBy(test => test.Sno)
    //             .Select(grp => grp.First())
    //             .ToList();
      

    //    return StockMaterial;
    //}

    //public List<StockEntryDetail> GetStoreMaterialBillInfo(int EstID, int EMRID)
    //{
    //    var StoreBill = (from SE in _context.StockEntry
    //                     join SMB in _context.StoreMaterialBill on SE.BillPath equals SMB.BillNo
    //                     where SE.EMRID == EMRID && SMB.EstID == EstID
    //                     select new
    //                     {
    //                         ID = SE.ID,
    //                         ReceivedOn = SE.ReceivedOn,
    //                         BillPath = SE.BillPath
    //                     }).AsEnumerable().Select(x => new StockEntryDetail
    //                     {
    //                         ID = x.ID,
    //                         ReceivedOn = x.ReceivedOn,
    //                         BillPath = x.BillPath
    //                     }).ToList();

    //    return StoreBill;
    //}
}