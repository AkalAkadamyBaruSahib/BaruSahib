using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using AkalAcademy;

/// <summary>
/// Summary description for AdminRepository
/// </summary>
public class AdminRepository
{
    private DataContext _context;

    public AdminRepository(DataContext context)
    {
        _context = context;
    }

    public void AssignAllAcademiesToUser(int InchargeID)
    {
        _context.AcademyAssignToEmployee.RemoveRange(_context.AcademyAssignToEmployee.Where(x => x.EmpId == InchargeID));

        List<Academy> academies = _context.Academy.ToList();

        AcademyAssignToEmployee acaAssignToEmployee = null;

        foreach (Academy aca in academies)
        {
            acaAssignToEmployee = new AcademyAssignToEmployee();
            acaAssignToEmployee.AcaId = aca.AcaID;
            acaAssignToEmployee.EmpId = InchargeID;
            acaAssignToEmployee.ZoneId = aca.ZoneId;
            acaAssignToEmployee.Active = true;
            acaAssignToEmployee.CreatedOn = DateTime.Now;
            acaAssignToEmployee.CreatedBy = "ADMIN";
            _context.AcademyAssignToEmployee.Add(acaAssignToEmployee);
        }
        _context.SaveChanges();


    }

    public void AddNewPurchaseSource(PurchaseSource purchasesource)
    {
        Hashtable param = new Hashtable();
        param.Add("PSName", purchasesource.PSName);
        param.Add("CreatedOn", purchasesource.CreatedOn);
        param.Add("CreatedBy", purchasesource.CreatedBy);
        param.Add("ModifyBy", purchasesource.ModifyBy);
        param.Add("ModifyOn", purchasesource.ModifyOn);
        param.Add("Active", purchasesource.Active);
        _context.Entry(purchasesource).State = EntityState.Added;
        _context.SaveChanges();
    }

    public void DeletePSName(int PSID)
    {
        PurchaseSource purchasesource = _context.PurchaseSource.Where(v => v.PSId == PSID)
                             .FirstOrDefault();
        purchasesource.Active = 0;
        _context.Entry(purchasesource).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public EstiamteChart GetEstimateChartData()
    {
        EstiamteChart estimateChart = new EstiamteChart();
        estimateChart.ApprovedEstimates = _context.Estimate.Where(x => x.IsApproved == true).Count();
        estimateChart.NonApprovedEstimates = _context.Estimate.Where(x => x.IsApproved == false).Count();
        estimateChart.TotalEstimates = _context.Estimate.Count();

        return estimateChart;
    }

    public DrawingChart GetDrawingChartData()
    {
        DrawingChart drawingChart = new DrawingChart();
        drawingChart.ApprovedDrawings = _context.Drawing.Where(x => x.IsApproved == true).Count();
        drawingChart.NonApprovedDrawings = _context.Drawing.Where(x => x.IsApproved == false).Count();
        drawingChart.TotalDrawings = _context.Drawing.Count();

        return drawingChart;
    }

    public SubmitBillChart GetSubmitBillChartData()
    {
        SubmitBillChart submitBillChart = new SubmitBillChart();
        submitBillChart.ApprovedBills = _context.SubmitBillByUser.Where(x => x.FirstVarifyStatus == null || x.FirstVarifyStatus == 1).Count();
        submitBillChart.NonApprovedBills = _context.SubmitBillByUser.Where(x => x.FirstVarifyStatus == 0).Count();
        submitBillChart.TotalLocalPurchased = _context.SubmitBillByUser.Count();

        return submitBillChart;
    }
}
