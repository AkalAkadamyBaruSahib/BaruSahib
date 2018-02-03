using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AkalAcademy;
using System.Data;

/// <summary>
/// Summary description for ConstructionUserController
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ConstructionUserController : System.Web.Services.WebService
{

    public ConstructionUserController()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string GetMaterialDetails(int MatID, int WorkAllotID)
    {
        ConstructionUserRepository constructionUserRepository = new ConstructionUserRepository(new DataContext());
        return constructionUserRepository.GetMaterialDetails(MatID, WorkAllotID);
    }

    [WebMethod]
    public List<Estimate> GetEstimateDetails(int WorkAllotID)
    {
        ConstructionUserRepository constructionUserRepository = new ConstructionUserRepository(new DataContext());
        return constructionUserRepository.GetEstimateDetails(WorkAllotID);
    }
    [WebMethod]
    public int SaveCivilBillDetail(SubmitBillByUser SubmitBillByUser)
    {

        SubmitBillByUser.ModifyOn = Utility.GetLocalDateTime(DateTime.UtcNow);
        SubmitBillByUser.CreatedOn = Utility.GetLocalDateTime(DateTime.UtcNow);
        SubmitBillByUser.FirstVarifyStatus = null;

        foreach (SubmitBillByUserAndMaterialOthersRelation relation in SubmitBillByUser.SubmitBillByUserAndMaterialOthersRelation)
        {
            relation.CreatedOn = Utility.GetLocalDateTime(DateTime.UtcNow);
            relation.ModifyOn = Utility.GetLocalDateTime(DateTime.UtcNow);
        }
        ConstructionUserRepository repository = new ConstructionUserRepository(new AkalAcademy.DataContext());
        return repository.SaveCivilBillDetail(SubmitBillByUser);
    }

    [WebMethod]
    public decimal? BillSumitRateCondition(int AcademyID, int BillTypeID, int Month)
    {
        ConstructionUserRepository constructionUserRepository = new ConstructionUserRepository(new DataContext());
        return constructionUserRepository.BillSumitRateCondition(AcademyID, BillTypeID, Month);
    }

    [WebMethod]
    public SubmitBillByUser GetNonSanctionedBillDetailByBillID(int BillID)
    {
        ConstructionUserRepository repository = new ConstructionUserRepository(new AkalAcademy.DataContext());
        return repository.GetNonSanctionedBillDetailByBillID(BillID);
    }

}
