﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AkalAcademy;
using System.Data.Entity;
using System.Collections;
using System.Data;
using System.Web.UI;


/// <summary>
/// Summary description for WorkshopRepository
/// </summary>
public class WorkshopRepository
{
	private DataContext _context;

    public WorkshopRepository(DataContext context)
    {
        _context = context;
    }

    public List<WorkshopStoreMaterial> GetWorkShopMaterials(int AcaID)
    {
        List<WorkshopStoreMaterial> WorkshopStoreMaterials = new List<WorkshopStoreMaterial>();
        if (AcaID == 0)
        {
            WorkshopStoreMaterials = _context.WorkshopStoreMaterial.Include(a => a.Academy)
                .Include(m => m.Material)
                .Where(m => m.Material.Active == 1)
                .ToList().OrderByDescending(e => e.Material.MatName).ToList();
        }
        else
        {
            WorkshopStoreMaterials = _context.WorkshopStoreMaterial
                .Include(m => m.Material).Where(m => m.Material.Active == 1).Where(a => a.AcaID == AcaID).ToList().OrderByDescending(e => e.Material.MatName).ToList(); ;
        }

        return WorkshopStoreMaterials;
    }

    public void UpdateWorkshopMaterial(WorkshopStoreMaterialDTO workshopStoreMaterialDTO)
    {

        WorkshopStoreMaterial workshopStoreMaterial = _context.WorkshopStoreMaterial.Include(m => m.Material).Where(x => x.ID == workshopStoreMaterialDTO.ID).FirstOrDefault();

        workshopStoreMaterial.Material.AkalWorkshopRate = workshopStoreMaterialDTO.Rate;
        workshopStoreMaterial.InStoreQty = workshopStoreMaterialDTO.InStoreQty;
        workshopStoreMaterial.ModifyBy = workshopStoreMaterialDTO.ModifyBy;
        workshopStoreMaterial.ModifyOn = Utility.GetLocalDateTime(DateTime.UtcNow);
        _context.Entry(workshopStoreMaterial).State = EntityState.Modified;


        _context.SaveChanges();
    }

    public void AddNewWorkshopMaterial(WorkshopStoreMaterial workshopStoreMaterial)
    {
        _context.Entry(workshopStoreMaterial).State = EntityState.Added;
        _context.SaveChanges();
    }

    public void AddDispatchWorkshopMaterial(WorkshopDispatchMaterial workshopDispatchMaterial)
    {
        _context.Entry(workshopDispatchMaterial).State = EntityState.Added;
        _context.SaveChanges();
    }
    public List<Estimate> GetAcademyNameByEstId(int EstimateID)
    {
        return _context.Estimate.Where(x => x.EstId == EstimateID).Include(x => x.Academy).ToList();
    }
    public void AddNewBill(WorkshopBills wb)
    {
        _context.Entry(wb).State = EntityState.Added;
        _context.SaveChanges();

        WorkshopBills newBill = _context.WorkshopBills.Where(v => v.ID == wb.ID).FirstOrDefault();
        newBill.BillNumber = wb.ID.ToString();
        _context.Entry(newBill).State = EntityState.Modified;
        _context.SaveChanges();
       
    }
    public void ReturnEstimateMaterial(int EMRID)
    {
        EstimateAndMaterialOthersRelations RejectMaterialItem = _context.EstimateAndMaterialOthersRelations.Where(v => v.Sno == EMRID).FirstOrDefault();
        RejectMaterialItem.PurchaseEmpID = 0;
        _context.Entry(RejectMaterialItem).State = EntityState.Modified;
        _context.SaveChanges();

    }

    public void RejectEstimate(int EstID)
    {
        Estimate RejectItem = _context.Estimate.Where(v => v.EstId == EstID).FirstOrDefault();
        RejectItem.IsApproved = false;
        RejectItem.IsItemRejected = true;
        _context.Entry(RejectItem).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public WorkshopStoreMaterial GetWorkshopInStoreMaterialByMatID(int matID, int AcaID)
    {
        return _context.WorkshopStoreMaterial.Where(m => m.MatID == matID && m.AcaID == AcaID).FirstOrDefault();
    }

    public int GetUnCompletedEstimate(int inchargeID, int purchaseSourceID)
    {
        List<Estimate> estimates = new List<Estimate>();

        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.IsActive == true && e.IsReceived == false)
          .Where(r => r.EstimateAndMaterialOthersRelations.Any(er => er.PSId == purchaseSourceID && er.DispatchStatus == 0 && er.PurchaseEmpID == inchargeID)).Count();
        return ests;
    }

    public int GetUnAssignedEstimate(int purchaseSourceID)
    {
        List<Estimate> estimates = new List<Estimate>();

        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.IsActive == true && e.IsReceived == false)
          .Where(r => r.EstimateAndMaterialOthersRelations.Any(er => er.PSId == purchaseSourceID && er.DispatchStatus == 0 && er.PurchaseEmpID == 0)).Count();
        return ests;
    }

    public List<WorkshopBills> GetBillDetails(int EstID)
    {
        List<WorkshopBills> workMaterialBill = _context.WorkshopBills.Where(s => s.EstID == EstID).ToList();
        return workMaterialBill;
    }

    public int WorkshopBillToDelete(int BillID)
    {
        WorkshopBills DelWorkshopBills = _context.WorkshopBills.Where(v => v.ID == BillID).FirstOrDefault();
        _context.Entry(DelWorkshopBills).State = EntityState.Deleted;
        _context.SaveChanges();
        return DelWorkshopBills.EstID;
    }
}