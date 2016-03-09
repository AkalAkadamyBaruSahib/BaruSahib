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
        Hashtable param = new Hashtable();
        param.Add("EstID", StoreMaterialBill.EstID);
        param.Add("BillName", StoreMaterialBill.BillName);
        param.Add("BillNo", StoreMaterialBill.BillNo);
        param.Add("BillPath", StoreMaterialBill.BillPath);
        int StoreMaterialBillID = DAL.DalAccessUtility.GetDataInScaler("procSaveStoreMaterialBill", param);

    }
}