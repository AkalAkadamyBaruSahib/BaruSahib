using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AkalAcademy;
using System.Web.Script.Serialization;
using System.Data.Entity;
using System.IO;
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
    public view_BillSubmitedDetails GetEstimateAndBillCost(int worksAllotID, int AcademyID)
    {
        return _context.view_BillSubmitedDetails.Where(x => x.AcaId == AcademyID && x.WAId == worksAllotID && x.IsApproved == true).FirstOrDefault();
    }
    public List<Estimate> GetEstimateDetails(int WorkAllotID)
    {
        List<Estimate> EstCopy = _context.Estimate.Where(s => s.WAId == WorkAllotID && s.IsApproved == true).ToList();
        return EstCopy;
    }
    public int SaveCivilBillDetail(SubmitBillByUser SubmitBillByUser)
    {
        if (SubmitBillByUser.SubBillId > 0)
        {
            _context.SubmitBillByUserAndMaterialOthersRelation.RemoveRange(_context.SubmitBillByUserAndMaterialOthersRelation.Where(v => v.SubBillId == SubmitBillByUser.SubBillId));

            SubmitBillByUser submitBill = _context.SubmitBillByUser.Where(v => v.SubBillId == SubmitBillByUser.SubBillId).Include(r => r.SubmitBillByUserAndMaterialOthersRelation).FirstOrDefault();
            submitBill.ZoneId = SubmitBillByUser.ZoneId;
            submitBill.AcaId = SubmitBillByUser.AcaId;
            submitBill.ChargetoBillTyId = SubmitBillByUser.ChargetoBillTyId;
            submitBill.BillDate = SubmitBillByUser.BillDate;
            submitBill.GateEntryNo = SubmitBillByUser.GateEntryNo;
            submitBill.VendorID = SubmitBillByUser.VendorID;
            submitBill.AgencyName = SubmitBillByUser.AgencyName;
            submitBill.CreatedBy = SubmitBillByUser.CreatedBy;
            submitBill.ModifyBy = SubmitBillByUser.ModifyBy;
            submitBill.Active = SubmitBillByUser.Active;
            submitBill.BillType = SubmitBillByUser.BillType;
            submitBill.VendorBillNumber = SubmitBillByUser.VendorBillNumber;
            submitBill.TotalAmount = SubmitBillByUser.TotalAmount;
            submitBill.FirstVarifyStatus = SubmitBillByUser.FirstVarifyStatus;

            if (SubmitBillByUser.VendorBillPath != null)
            {
                submitBill.VendorBillPath = SubmitBillByUser.VendorBillPath;
            }

            submitBill.SubmitBillByUserAndMaterialOthersRelation = new List<SubmitBillByUserAndMaterialOthersRelation>();
            SubmitBillByUserAndMaterialOthersRelation submitBillByUserAndMaterialOthersRelation;
            foreach (SubmitBillByUserAndMaterialOthersRelation rm in SubmitBillByUser.SubmitBillByUserAndMaterialOthersRelation)
            {
                submitBillByUserAndMaterialOthersRelation = new SubmitBillByUserAndMaterialOthersRelation();
                submitBillByUserAndMaterialOthersRelation.SubBillId = rm.SubBillId;
                submitBillByUserAndMaterialOthersRelation.MatId = rm.MatId;
                submitBillByUserAndMaterialOthersRelation.MatTypeId = rm.MatTypeId;
                submitBillByUserAndMaterialOthersRelation.Qty = rm.Qty;
                submitBillByUserAndMaterialOthersRelation.Rate = rm.Rate;
                submitBillByUserAndMaterialOthersRelation.UnitName = rm.UnitName;
                submitBillByUserAndMaterialOthersRelation.ItemName = rm.ItemName;
                submitBillByUserAndMaterialOthersRelation.Remark = rm.Remark;
                submitBillByUserAndMaterialOthersRelation.UnitId = rm.UnitId;
                submitBillByUserAndMaterialOthersRelation.CreatedBy = rm.CreatedBy;
                submitBillByUserAndMaterialOthersRelation.ModifyBy = rm.ModifyBy;
                submitBillByUserAndMaterialOthersRelation.Active = rm.Active;
                submitBillByUserAndMaterialOthersRelation.Vat = rm.Vat;
                submitBillByUserAndMaterialOthersRelation.Amount = rm.Amount;
                submitBill.SubmitBillByUserAndMaterialOthersRelation.Add(submitBillByUserAndMaterialOthersRelation);
            }

            _context.Entry(submitBill).State = EntityState.Modified;
            _context.SaveChanges();
        }
        else
        {
            _context.SubmitBillByUser.Add(SubmitBillByUser);
            _context.SaveChanges();

        }
        return SubmitBillByUser.SubBillId;
    }
    public decimal? BillSumitRateCondition(int AcademyID, int BillTypeID)
    {
        DateTime date = Utility.GetLocalDateTime(DateTime.UtcNow);
        var firstDateOfMonth = new DateTime(date.Year, date.Month, 1);
        var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);

        decimal? receivedQty = (from S in _context.SubmitBillByUser
                                where S.AcaId == AcademyID && S.CreatedOn >= firstDateOfMonth && S.CreatedOn <= lastDateOfMonth && S.BillType == BillTypeID
                                && (S.FirstVarifyStatus == null || S.FirstVarifyStatus == 1)
                                select (decimal?)S.TotalAmount).Sum();
        return receivedQty;
    }

    public SubmitBillByUser GetNonSanctionedBillDetailByBillID(int BillID)
    {
        SubmitBillByUser bill = _context.SubmitBillByUser.Where(v => v.SubBillId == BillID).Include(r => r.SubmitBillByUserAndMaterialOthersRelation).FirstOrDefault();
        SubmitBillByUser dto = new SubmitBillByUser();
        dto.SubBillId = bill.SubBillId;
        dto.AgencyName = bill.AgencyName;
        dto.GateEntryNo = bill.GateEntryNo;
        dto.BillDate = bill.BillDate;
        dto.BillType = bill.BillType;
        dto.VendorBillNumber = bill.VendorBillNumber;
        dto.VendorBillPath = bill.VendorBillPath;

        List<SubmitBillByUserAndMaterialOthersRelation> SubmitBillAndMaterialRelation = new List<SubmitBillByUserAndMaterialOthersRelation>();

        SubmitBillByUserAndMaterialOthersRelation SubmitBillByUserAndMaterial;
        foreach (SubmitBillByUserAndMaterialOthersRelation rm in bill.SubmitBillByUserAndMaterialOthersRelation)
        {
            SubmitBillByUserAndMaterial = new SubmitBillByUserAndMaterialOthersRelation();
            SubmitBillByUserAndMaterial.SubBillId = rm.SubBillId;
            SubmitBillByUserAndMaterial.MatId = rm.MatId;
            SubmitBillByUserAndMaterial.UnitId = rm.UnitId;
            SubmitBillByUserAndMaterial.Qty = rm.Qty;
            SubmitBillByUserAndMaterial.Amount = rm.Amount;
            SubmitBillByUserAndMaterial.Rate = rm.Rate;
            SubmitBillByUserAndMaterial.ItemName = rm.ItemName;
            SubmitBillByUserAndMaterial.UnitName = rm.UnitName;
            SubmitBillAndMaterialRelation.Add(SubmitBillByUserAndMaterial);
        }

        dto.SubmitBillByUserAndMaterialOthersRelation = SubmitBillAndMaterialRelation;
        return dto;
 
    }

    public void GetBillDetailToDelete(int subBillID)
    {
        _context.SubmitBillByUserAndMaterialOthersRelation.RemoveRange(_context.SubmitBillByUserAndMaterialOthersRelation.Where(v => v.SubBillId == subBillID));
        _context.SaveChanges();

        SubmitBillByUser delBillinfo = _context.SubmitBillByUser.Where(v => v.SubBillId == subBillID).FirstOrDefault();
        if (File.Exists(HttpContext.Current.Server.MapPath("Bills/") + delBillinfo.VendorBillPath))
        {
            File.Delete(HttpContext.Current.Server.MapPath("Bills/") + delBillinfo.VendorBillPath);
        }
        _context.Entry(delBillinfo).State = EntityState.Deleted;
        _context.SaveChanges();
    }
}