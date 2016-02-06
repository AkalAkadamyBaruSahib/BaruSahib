using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AkalAcademy;
using System.Web.Script.Serialization;
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
}