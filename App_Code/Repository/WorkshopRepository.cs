using System;
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
            WorkshopStoreMaterials = _context.WorkshopStoreMaterial
                .Include(m => m.Material)
                .Include(a => a.Academy).ToList().OrderByDescending(e => e.Material.MatName).ToList();
        }
        else
        {
            WorkshopStoreMaterials = _context.WorkshopStoreMaterial
                .Include(m => m.Material).Where(a => a.AcaID == AcaID).ToList().OrderByDescending(e => e.Material.MatName).ToList(); ;
        }

        return WorkshopStoreMaterials;
    }

    public void UpdateWorkshopMaterial(WorkshopStoreMaterialDTO workshopStoreMaterialDTO)
    {

        WorkshopStoreMaterial workshopStoreMaterial = _context.WorkshopStoreMaterial.Include(m => m.Material).Where(x => x.ID == workshopStoreMaterialDTO.ID).FirstOrDefault();

        workshopStoreMaterial.Material.MatCost = workshopStoreMaterialDTO.Rate;
        workshopStoreMaterial.InStoreQty = workshopStoreMaterialDTO.InStoreQty;
        workshopStoreMaterial.ModifyBy = workshopStoreMaterialDTO.ModifyBy;
        workshopStoreMaterial.ModifyOn = DateTime.UtcNow;
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
}