using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;


/// <summary>
/// Summary description for StoreController
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class StoreController : System.Web.Services.WebService
{

    public StoreController()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void SaveStoreBill(StoreMaterialBill StoreMaterialBill)
    {
        StoreRepository storeRepository = new StoreRepository(new AkalAcademy.DataContext());
        storeRepository.SaveStoreBill(StoreMaterialBill);
    }

    [WebMethod]
    public List<StoreMaterialBill> GetBillDetails(int EstID)
    {
        StoreRepository storeRepository = new StoreRepository(new AkalAcademy.DataContext());
        return storeRepository.GetBillDetails(EstID);
    }

    [WebMethod]
    public List<VendorInfo> GetVendorsNameList(int matID)
    {
        StoreRepository repository = new StoreRepository(new AkalAcademy.DataContext());
        return repository.GetVendorsNameList(matID);
    }

    [WebMethod]
    public List<StoreMaterialBill> GetMaterialBillList(int estID)
    {
        StoreRepository repository = new StoreRepository(new AkalAcademy.DataContext());
        return repository.GetMaterialBillList(estID);
    }

    [WebMethod]
    public int StoreBillToDelete(int BillID)
    {
        StoreRepository repository = new StoreRepository(new AkalAcademy.DataContext());
        return repository.StoreBillToDelete(BillID);
    }

    [WebMethod]
    public decimal? SaveStroeMaterialDetail(StockEntry stockentry)
    {
        stockentry.ReceivedOn = Utility.GetLocalDateTime(DateTime.UtcNow);
        StoreRepository repository = new StoreRepository(new AkalAcademy.DataContext());
        return repository.SaveStroeMaterialDetail(stockentry);
    }

    [WebMethod]
    public decimal? SaveDisatchMaterialDetail(StockDispatchEntry stockdispatchentry)
    {
        stockdispatchentry.DispatchOn = Utility.GetLocalDateTime(DateTime.UtcNow);
        StoreRepository repository = new StoreRepository(new AkalAcademy.DataContext());
        return repository.SaveDisatchMaterialDetail(stockdispatchentry);
    }
}