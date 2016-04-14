using System;
using System.Collections;
using System.Collections.Generic;
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
        return (from ven in _context.VendorInfo
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
    }
}