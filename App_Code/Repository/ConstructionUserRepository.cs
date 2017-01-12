using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AkalAcademy;
using System.Web.Script.Serialization;
using System.Data.Entity;
/// <summary>
/// Summary description for ConstructionUserRepository
/// </summary>
public class ConstructionUserRepository
{
    private DataContext _context;

    public ConstructionUserRepository(DataContext context)
    {
        _context = context;
    }

    public string GetMaterialDetails(int MatID, int WorkAllotID)
    {
        DataSet dsMat = new System.Data.DataSet();
        dsMat = DAL.DalAccessUtility.GetDataInDataSet("SELECT S.SubBillId,S.AgencyName,SB.*,CONVERT(VARCHAR(19),SB.CreatedOn) AS SBCreatedOn FROM SubmitBillByUser S INNER JOIN SubmitBillByUserAndMaterialOthersRelation SB ON S.SubBillId = SB.SubBillId WHERE SB.MatId=" + MatID + " AND S.WAId=" + WorkAllotID);
        var json = Utility.SerializeDataTable(dsMat.Tables[0]);
        return json;

    }

    public string GetMaterialDetailsByWorkAllotID(int WorkAllotID)
    {
        DataSet dsAcademy = new DataSet();
        dsAcademy = DAL.DalAccessUtility.GetDataInDataSet("exec USP_getEstimateBalanceNew'" + WorkAllotID + "','1'");
        var json = Utility.SerializeDataTable(dsAcademy.Tables[0]);
        return json;
    }

    public void SaveEstimate(Estimate estimate)
    {
        _context.Estimate.Add(estimate);
        _context.SaveChanges();
    }
    public void SaveMaterial(MaterialNonApprovedRate materials)
    {

        _context.MaterialNonApprovedRate.Add(materials);
        _context.SaveChanges();
    }

    public void SaveApprovedMaterial(MaterialRateApproved materialrateapproved)
    {
        _context.MaterialRateApproved.Add(materialrateapproved);
        _context.SaveChanges();
    }
    public view_BillSubmitedDetails GetEstimateAndBillCost(int worksAllotID,int AcademyID)
    {
        return _context.view_BillSubmitedDetails.Where(x => x.AcaId == AcademyID && x.WAId == worksAllotID && x.IsApproved == true).FirstOrDefault();
    }
    public List<Estimate> GetEstimateDetails(int WorkAllotID)
    {
        List<Estimate> EstCopy = _context.Estimate.Where(s => s.WAId == WorkAllotID && s.IsApproved == true).ToList();
        return EstCopy;
    }
}