using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AkalAcademy;

/// <summary>
/// Summary description for PurchaseRepository
/// </summary>
public class PurchaseRepository
{

    private DataContext _context;

    public PurchaseRepository(DataContext context)
    {
        _context = context;
    }

    public List<Material> GetMaterialItemsByEstID(int Estid)
    {
        var Materials = (from EMR in _context.EstimateAndMaterialOthersRelations
                         join x in _context.Material on EMR.MatId equals x.MatId
                         where EMR.EstId == Estid
                         select new
                         {
                             MatId = x.MatId,
                             MatName = x.MatName,

                         }).AsEnumerable().Select(x => new Material
                         {
                             MatId = x.MatId,
                             MatName = x.MatName,
                         }).OrderByDescending(m => m.MatName).Reverse().ToList();


        return Materials;
    }

    public void RejectMaterialItemByID(int EMRID, int estID)
    {
        EstimateAndMaterialOthersRelations RejectMaterialItem = _context.EstimateAndMaterialOthersRelations.Where(v => v.Sno == EMRID).FirstOrDefault();
        RejectMaterialItem.IsApproved = false;
        _context.Entry(RejectMaterialItem).State = EntityState.Modified;
        _context.SaveChanges();

        Estimate estimate = _context.Estimate.Where(v => v.EstId == estID).FirstOrDefault();
        estimate.IsItemRejected = true;
        _context.Entry(estimate).State = EntityState.Modified;
        _context.SaveChanges();


    }

    public List<MaterialsDTO> GetActiveMaterials()
    {
        List<MaterialsDTO> mt = new List<MaterialsDTO>();
        return mt = _context.Material.Where(m => m.Active == 1).AsEnumerable().Select(x => new MaterialsDTO
                         {
                             MatID = x.MatId,
                             MatName = x.MatName,
                         }).OrderByDescending(m => m.MatName).Reverse().ToList();

    }

    public List<MaterialsDTO> GetMaterials()
    {
        List<MaterialsDTO> mt = new List<MaterialsDTO>();
        return mt = _context.Material.Where(m => m.Active == 1).AsEnumerable().Select(x => new MaterialsDTO
        {
            MatID = x.MatId,
            MatName = x.MatName,
        }).OrderByDescending(m => m.MatName).Reverse().ToList();
    }

    public List<MaterialsDTO> GetMaterialsByID(string materialList)
    {
        int[] myArray = materialList.Split(',').Select(int.Parse).ToArray();

        List<MaterialsDTO> mt = new List<MaterialsDTO>();
        mt = _context.Material.Include(x => x.Unit).Where(m => myArray.Contains(m.MatId)).AsEnumerable().Select(x => new MaterialsDTO
        {
            MatID = x.MatId,
            MatName = x.MatName,
            MatTypeID = x.MatTypeId,
            Unit = x.Unit,
            MatCost = x.MatCost,

        }).OrderByDescending(m => m.MatName).Reverse().ToList();
        return mt;
    }


    public List<PurchaseSource> GetPurchaseSource()
    {
        return _context.PurchaseSource.ToList();
    }


    public List<VendorInfo> GetVendorsNameList()
    {
        return _context.VendorInfo.ToList();
    }

    public List<Estimate> GetEstimateNumberList()
    {
        return _context.Estimate.ToList();
    }

    public List<EstimateAndMaterialOthersRelations> GetMaterialList(int EstimateID)
    {
        return _context.EstimateAndMaterialOthersRelations.Include(m => m.Material).Where(x => x.EstId == EstimateID).ToList();
    }

    public List<VendorInfo> GetVendorAddress(int VendorID)
    {
        return _context.VendorInfo.Where(x => x.ID == VendorID).ToList();
    }

    public List<POBillingAddress> GetDeliveryAddressList()
    {
        return _context.POBillingAddress.ToList();
    }

    public List<POBillingAddress> GetDeliveryAddressInfo(int AddressID)
    {
        return _context.POBillingAddress.Where(x => x.ID == AddressID).ToList();
    }

    public List<PurchaseOrder> GetPONumber()
    {
        return _context.PurchaseOrder.ToList();
    }

    public void AddNewVendorInformation(VendorInfo vendorInfo)
    {
        _context.Entry(vendorInfo).State = EntityState.Added;
        _context.SaveChanges();

    }

    public List<VendorInfoDTO> LoadActiveVendorInformation(int VendorID)
    {
        List<VendorInfo> vendorInfo = _context.VendorInfo.Where(v => v.Active == true)
                 .Include(e => e.VendorMaterialRelations).Distinct().OrderByDescending(x => x.CreatedOn).ToList();


        DataSet vendor = new DataSet();
        List<VendorInfoDTO> dto = new List<VendorInfoDTO>();

        VendorInfoDTO vendorDTO = null;
        foreach (VendorInfo v in vendorInfo)
        {
            vendorDTO = new VendorInfoDTO();

            vendorDTO.ID = v.ID;
            vendorDTO.VendorName = v.VendorName;
            vendorDTO.VendorAddress = v.VendorAddress;
            vendorDTO.VendorContactNo = v.VendorContactNo;
            vendorDTO.VendorState = v.VendorState;
            vendorDTO.VendorCity = v.VendorCity;
            vendorDTO.VendorZip = v.VendorZip;
            vendorDTO.Active = v.Active;
            vendorDTO.ModifyBy = v.ModifyBy;
            vendorDTO.ModifyOn = v.ModifyOn.ToString();
            vendorDTO.CreatedOn = v.CreatedOn.ToString();


            dto.Add(vendorDTO);
        }
        return dto;
    }

    public List<VendorInfoDTO> LoadInActiveVendorInformation(int VendorID)
    {
        List<VendorInfo> vendorInfo = _context.VendorInfo.Include(e => e.VendorMaterialRelations).Distinct().OrderByDescending(x => x.CreatedOn).ToList();


        DataSet vendor = new DataSet();
        List<VendorInfoDTO> dto = new List<VendorInfoDTO>();

        VendorInfoDTO vendorDTO = null;
        foreach (VendorInfo v in vendorInfo)
        {
            vendorDTO = new VendorInfoDTO();

            vendorDTO.ID = v.ID;
            vendorDTO.VendorName = v.VendorName;
            vendorDTO.VendorAddress = v.VendorAddress;
            vendorDTO.VendorContactNo = v.VendorContactNo;
            vendorDTO.VendorState = v.VendorState;
            vendorDTO.VendorCity = v.VendorCity;
            vendorDTO.VendorZip = v.VendorZip;
            vendorDTO.Active = v.Active;
            vendorDTO.ModifyBy = v.ModifyBy;
            vendorDTO.ModifyOn = v.ModifyOn.ToString();
            vendorDTO.CreatedOn = v.CreatedOn.ToString();

            dto.Add(vendorDTO);
        }

        return dto;
    }

    public VendorInfoDTO GetVendorInfoToUpdate(int VendorID)
    {
        VendorInfo vendorInfo = _context.VendorInfo.Where(v => v.ID == VendorID)
            .Include(e => e.VendorMaterialRelations)
          .FirstOrDefault();


        VendorInfoDTO dto = new VendorInfoDTO();
        dto.ID = vendorInfo.ID;
        dto.VendorName = vendorInfo.VendorName;
        dto.VendorAddress = vendorInfo.VendorAddress;
        dto.VendorContactNo = vendorInfo.VendorContactNo;
        dto.VendorState = vendorInfo.VendorState;
        dto.VendorCity = vendorInfo.VendorCity;
        dto.VendorZip = vendorInfo.VendorZip;

        dto.Active = vendorInfo.Active;

        dto.VendorMaterialRelationDTO = new List<VendorMaterialRelationDTO>();
        VendorMaterialRelationDTO vendorMatRel;
        DataSet dsmat = new DataSet();
        foreach (VendorMaterialRelation rm in vendorInfo.VendorMaterialRelations)
        {
            vendorMatRel = new VendorMaterialRelationDTO();

            vendorMatRel.ID = rm.ID;
            dsmat = DAL.DalAccessUtility.GetDataInDataSet("select MatName from Material where MatID = '" + rm.MatID + "'");
            if (dsmat.Tables[0].Rows.Count > 0)
            {
                vendorMatRel.MatName = dsmat.Tables[0].Rows[0]["MatName"].ToString();
            }

            vendorMatRel.VendorID = rm.VendorID;
            vendorMatRel.MatID = rm.MatID;
            vendorMatRel.MatType = rm.MatType;
            vendorMatRel.CreatedOn = rm.CreatedOn.ToString();
            vendorMatRel.CreatedOn = rm.ModifyOn.ToString();
            dto.VendorMaterialRelationDTO.Add(vendorMatRel);
        }

        return dto;
    }

    public void UpdateVendorInformation(VendorInfoDTO vendorInfo)
    {
        _context.VendorMaterialRelation.RemoveRange(_context.VendorMaterialRelation.Where(v => v.VendorID == vendorInfo.ID));

        VendorInfo newVendorInfo = _context.VendorInfo.Where(v => v.ID == vendorInfo.ID)
           .Include(r => r.VendorMaterialRelations)
            .FirstOrDefault();

        newVendorInfo.VendorName = vendorInfo.VendorName;
        newVendorInfo.VendorAddress = vendorInfo.VendorAddress;
        newVendorInfo.VendorContactNo = vendorInfo.VendorContactNo;
        newVendorInfo.VendorCity = vendorInfo.VendorCity;
        newVendorInfo.VendorState = vendorInfo.VendorState;
        newVendorInfo.VendorZip = vendorInfo.VendorZip;
        newVendorInfo.CreatedOn = newVendorInfo.CreatedOn;
        newVendorInfo.ModifyOn = DateTime.Now;
        newVendorInfo.Active = true;


        newVendorInfo.VendorMaterialRelations = new List<VendorMaterialRelation>();

        VendorMaterialRelation vendorMaterialRelation = new VendorMaterialRelation();
        DataSet dsmat = new DataSet();

        foreach (VendorMaterialRelationDTO item in vendorInfo.VendorMaterialRelationDTO)
        {
            vendorMaterialRelation = new VendorMaterialRelation();
            dsmat = DAL.DalAccessUtility.GetDataInDataSet("select MatTypeId,MatID from Material where MatName like '%" + item.MatName + "%'");
            if (dsmat.Tables[0].Rows.Count > 0)
            {
                vendorMaterialRelation.MatID = Convert.ToInt32(dsmat.Tables[0].Rows[0]["MatId"].ToString());
                vendorMaterialRelation.MatType = Convert.ToInt32(dsmat.Tables[0].Rows[0]["MatTypeId"].ToString());
                vendorMaterialRelation.CreatedOn = DateTime.Now;
                vendorMaterialRelation.ModifyOn = DateTime.Now;
                vendorMaterialRelation.ID = item.ID;
                vendorMaterialRelation.VendorID = vendorInfo.ID;
            }

            newVendorInfo.VendorMaterialRelations.Add(vendorMaterialRelation);
        }
        _context.Entry(newVendorInfo).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void VendorInfoToDelete(int vendorID)
    {
        _context.VendorMaterialRelation.RemoveRange(_context.VendorMaterialRelation.Where(v => v.VendorID == vendorID));

        VendorInfo DelVendorinfo = _context.VendorInfo.Where(v => v.ID == vendorID)
            .Include(r => r.VendorMaterialRelations)
            .FirstOrDefault();
        DelVendorinfo.VendorMaterialRelations = null;
        DelVendorinfo.Active = false;
        _context.Entry(DelVendorinfo).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public List<MaterialType> GetBindMaterialType()
    {
        return _context.MaterialType.OrderBy(x => x.MatTypeName).ToList();
    }

    public List<Material> GetBindMaterialNameByMaterialType(int MatTypeID)
    {
        return _context.Material.Where(x => x.MatTypeId == MatTypeID).OrderBy(x => x.MatName).ToList();
    }

    public List<Material> GeMaterialInformation(int MaterialID)
    {
        return _context.Material.Where(x => x.MatId == MaterialID).ToList();
    }

    public int SaveEstimateDetail(Estimate estimate)
    {
        _context.Estimate.Add(estimate);
        _context.SaveChanges();

        return estimate.EstId;
    }

    public List<Zone> GetZone()
    {
        return _context.Zone.ToList();
    }

    public List<Academy> GetAcademybyZoneID(int ZoneID)
    {
        return _context.Academy.Where(x => x.ZoneId == ZoneID).ToList();
    }

    public List<WorkAllot> GetWorkAllotByAcademyID(int AcademyID)
    {
        return _context.WorkAllot.Where(x => x.AcaId == AcademyID).ToList();
    }

    public List<Zone> GetZoneByInchargeID(int InchargeID)
    {
        var Zones = (from z in _context.Zone
                     join AAE in _context.AcademyAssignToEmployee on z.ZoneId equals AAE.ZoneId
                     where AAE.EmpId == InchargeID
                     select new
                     {
                         ZoneId = z.ZoneId,
                         ZoneName = z.ZoneName,

                     }).AsEnumerable().Select(x => new Zone
                 {
                     ZoneId = x.ZoneId,
                     ZoneName = x.ZoneName,
                 }).OrderBy(m => m.ZoneName).ToList();

        Zones = Zones.GroupBy(test => test.ZoneId)
                   .Select(grp => grp.First())
                   .ToList();
        return Zones;
    }

    public List<DrawingType> GetDrawingType()
    {
        return _context.DrawingType.ToList();
    }

    public List<SubDrawingTypes> GetSubDrawingByDrawingID(int DrawingID)
    {
        return _context.SubDrawingTypes.Where(x => x.DwgTypeId == DrawingID).ToList();
    }


    public void SaveDrawingDetail(Drawing drawing)
    {
        _context.Drawing.Add(drawing);
        _context.SaveChanges();
    }

    public List<Zone> GetDrawingZone()
    {
        return _context.Zone.ToList();
    }

    public List<DrawingDTO> GeTDrawingInformation(int DrawingID)
    {
        DateTime dt = DateTime.Now.AddDays(-30);
        List<Drawing> drawingInfo = _context.Drawing.Include(x => x.Zone).Include(a => a.Academy).Where(d => d.CreatedOn >= dt).OrderByDescending(x => x.CreatedOn).ToList();
        List<DrawingDTO> dto = new List<DrawingDTO>();

        DrawingDTO drawingDTO = null;
        foreach (Drawing v in drawingInfo)
        {
            drawingDTO = new DrawingDTO();

            drawingDTO.DwgId = v.DwgId;

            drawingDTO.ZoneId = v.ZoneId;
            drawingDTO.AcaId = v.AcaId;
            drawingDTO.DwTypeId = v.DwTypeId;
            drawingDTO.DwgNo = v.DwgNo;
            drawingDTO.RevisionNo = v.RevisionNo;
            drawingDTO.DwgFileName = v.DwgFileName;
            drawingDTO.DwgFilePath = v.DwgFilePath;
            drawingDTO.PdfFileName = v.PdfFileName;
            drawingDTO.PdfFilePath = v.PdfFilePath;
            drawingDTO.Active = v.Active;
            drawingDTO.CreatedBy = v.CreatedBy;
            drawingDTO.DrawingName = v.DrawingName;
            drawingDTO.ShiftedStatus = v.ShiftedStatus;
            drawingDTO.SubDwgTypeID = v.SubDwgTypeID;
            drawingDTO.IsApproved = v.IsApproved;
            drawingDTO.ModifyBy = v.ModifyBy;
            drawingDTO.ModifyOn = v.ModifyOn.ToString();
            drawingDTO.ZoneName = v.Zone.ZoneName;
            drawingDTO.AcaName = v.Academy.AcaName;
            if (v.CreatedOn != null)
            {
                drawingDTO.CreatedOn = v.CreatedOn.Value.ToShortDateString();
            }

            dto.Add(drawingDTO);
        }

        return dto;
    }

    public DrawingDTO GetDrawingInfoToUpdate(int DrawingID)
    {
        Drawing v = _context.Drawing.Where(d => d.DwgId == DrawingID)
                    .FirstOrDefault();
        DrawingDTO drawingDTO = new DrawingDTO();
        drawingDTO.DwgId = v.DwgId;
        drawingDTO.ZoneId = v.ZoneId;
        drawingDTO.AcaId = v.AcaId;
        drawingDTO.DwTypeId = v.DwTypeId;
        drawingDTO.DwgNo = v.DwgNo;
        drawingDTO.RevisionNo = v.RevisionNo;
        drawingDTO.DwgFileName = v.DwgFileName;
        drawingDTO.DwgFilePath = v.DwgFilePath;
        drawingDTO.PdfFileName = v.PdfFileName;
        drawingDTO.PdfFilePath = v.PdfFilePath;
        drawingDTO.Active = v.Active;
        drawingDTO.CreatedBy = v.CreatedBy;
        drawingDTO.DrawingName = v.DrawingName;
        drawingDTO.ShiftedStatus = v.ShiftedStatus;
        drawingDTO.SubDwgTypeID = v.SubDwgTypeID;
        drawingDTO.IsApproved = v.IsApproved;
        drawingDTO.ModifyBy = v.ModifyBy;
        drawingDTO.ModifyOn = v.ModifyOn.ToString();
        drawingDTO.CreatedOn = v.CreatedOn.ToString();

        return drawingDTO;
    }

    public void UpdateDrawingInformation(Drawing drawingInfo)
    {
        Drawing drawingDTO = _context.Drawing.Where(v => v.DwgId == drawingInfo.DwgId)
              .FirstOrDefault();
        drawingDTO.DwgId = drawingInfo.DwgId;
        drawingDTO.ZoneId = drawingInfo.ZoneId;
        drawingDTO.AcaId = drawingInfo.AcaId;
        drawingDTO.DwTypeId = drawingInfo.DwTypeId;
        drawingDTO.DwgNo = drawingInfo.DwgNo;
        drawingDTO.RevisionNo = drawingInfo.RevisionNo;
        drawingDTO.DwgFileName = drawingInfo.DwgFileName;
        drawingDTO.DwgFilePath = drawingInfo.DwgFilePath;
        drawingDTO.PdfFileName = drawingInfo.PdfFileName;
        drawingDTO.PdfFilePath = drawingInfo.PdfFilePath;
        drawingDTO.Active = drawingInfo.Active;
        drawingDTO.CreatedBy = drawingInfo.CreatedBy;
        drawingDTO.DrawingName = drawingInfo.DrawingName;
        drawingDTO.ShiftedStatus = drawingInfo.ShiftedStatus;
        drawingDTO.SubDwgTypeID = drawingInfo.SubDwgTypeID;
        drawingDTO.IsApproved = drawingInfo.IsApproved;
        drawingDTO.ModifyBy = drawingInfo.ModifyBy;
        drawingDTO.ModifyOn = DateTime.Now;
        drawingDTO.CreatedOn = drawingDTO.CreatedOn;

        _context.Entry(drawingDTO).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public List<DrawingDTO> GeTDrawingInformationByInchargeID(int InchargeID)
    {
        //DateTime dt = DateTime.Now.AddDays(-30);
        List<Drawing> drawingInfo = _context.Drawing.Include(x => x.Zone).Include(a => a.Academy).Where(d => d.CreatedBy == InchargeID).OrderByDescending(x => x.CreatedOn).ToList();
        List<DrawingDTO> dto = new List<DrawingDTO>();

        DrawingDTO drawingDTO = null;
        foreach (Drawing v in drawingInfo)
        {
            drawingDTO = new DrawingDTO();

            drawingDTO.DwgId = v.DwgId;

            drawingDTO.ZoneId = v.ZoneId;
            drawingDTO.AcaId = v.AcaId;
            drawingDTO.DwTypeId = v.DwTypeId;
            drawingDTO.DwgNo = v.DwgNo;
            drawingDTO.RevisionNo = v.RevisionNo;
            drawingDTO.DwgFileName = v.DwgFileName;
            drawingDTO.DwgFilePath = v.DwgFilePath;
            drawingDTO.PdfFileName = v.PdfFileName;
            drawingDTO.PdfFilePath = v.PdfFilePath;
            drawingDTO.Active = v.Active;
            drawingDTO.CreatedBy = v.CreatedBy;
            drawingDTO.DrawingName = v.DrawingName;
            drawingDTO.ShiftedStatus = v.ShiftedStatus;
            drawingDTO.SubDwgTypeID = v.SubDwgTypeID;
            drawingDTO.IsApproved = v.IsApproved;
            drawingDTO.ModifyBy = v.ModifyBy;
            drawingDTO.ModifyOn = v.ModifyOn.ToString();
            drawingDTO.ZoneName = v.Zone.ZoneName;
            drawingDTO.AcaName = v.Academy.AcaName;
            if (v.CreatedOn != null)
            {
                drawingDTO.CreatedOn = v.CreatedOn.Value.ToShortDateString();
            }

            dto.Add(drawingDTO);
        }

        return dto;
    }

    public List<MaterialType> GetBindMaterialTypeInTransport()
    {
        return _context.MaterialType.Where(x => x.MatTypeId == 49).OrderBy(x => x.MatTypeName).ToList();
    }

    public List<Estimate> EstimateViewForPurchase(int PSID, int UserTypeID, int InchargeID)
    {
        DateTime dt1 = DateTime.Now.AddDays(-7);


        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.CreatedOn >= dt1)
            .Include(z=>z.Zone)
            .Include(a=>a.Academy).OrderByDescending(e=>e.ModifyOn).ToList();

        
        List<Estimate> estimates = new List<Estimate>();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();

            e.SanctionDate = e.ModifyOn;

            if (estimateRelation.Count > 0)
            {
                e.EstimateAndMaterialOthersRelations = estimateRelation;
                estimates.Add(e);
            }
        }
        ests = null;
        return estimates;
    }

    public List<Estimate> EstimateViewForPurchaseByEmployeeID(int PSID,int UserTypeID ,int InchargeID)
    {

        DateTime dt1 = DateTime.Now.AddDays(-30);

        List<Estimate> estimates = new List<Estimate>();

        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.CreatedOn >= dt1)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e=>e.ModifyOn).ToList();
     

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId && er.DispatchStatus != 1 && er.PurchaseEmpID == InchargeID)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();

                e.SanctionDate = e.ModifyOn;

            if (estimateRelation.Count > 0)
            {
                e.EstimateAndMaterialOthersRelations = estimateRelation;
                estimates.Add(e);
            }
        }
       ests = null;
        return estimates;
        
    }

    public List<Estimate> MaterialDepatchStatusForAdmin(int PSID, int UserTypeID, int inchargeID)
    {
        DateTime dt1 = DateTime.Now.AddDays(-7);

        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.CreatedOn >= dt1)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e => e.ModifyOn).ToList();

        List<Estimate> estimates = new List<Estimate>();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er =>er.EstId == e.EstId  && er.PSId == 2 || er.PSId == 3)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();

            e.SanctionDate = e.ModifyOn;

            if (estimateRelation.Count > 0)
            {
                e.EstimateAndMaterialOthersRelations = estimateRelation;
                estimates.Add(e);
            }
        }
        ests = null;
        return estimates;
    }

    public List<Estimate> MaterialDepatchStatusForAdminLocal(int PSID, int UserTypeID, int inchargeID)
    {
        DateTime dt1 = DateTime.Now.AddDays(-7);

        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.CreatedOn >= dt1)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e => e.ModifyOn).ToList();

        List<Estimate> estimates = new List<Estimate>();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();

            e.SanctionDate = e.ModifyOn;

            if (estimateRelation.Count > 0)
            {
                e.EstimateAndMaterialOthersRelations = estimateRelation;
                estimates.Add(e);
            }
        }
        ests = null;
        return estimates;
    }

    public List<Estimate> MaterialDepatchStatus(int PSID, int UserTypeID,int InchargeID)
    {
        DateTime dt1 = DateTime.Now.AddDays(-30);

        List<Estimate> estimates = new List<Estimate>();
        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.CreatedOn >= dt1 && e.CreatedBy == InchargeID)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e => e.ModifyOn).ToList();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId && er.DispatchStatus != 1)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();
               e.SanctionDate = e.ModifyOn;

            if (estimateRelation.Count > 0)
            {
                e.EstimateAndMaterialOthersRelations = estimateRelation;
                estimates.Add(e);
            }
        }
        ests = null;
        return estimates;
    }

    public List<Estimate> EstimateViewForPurchaseByAcaID(int PSID,int UserTypeID,int InchargeID,int AcaID)
    {

        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.AcaId == AcaID)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e => e.ModifyOn).ToList();

        List<Estimate> estimates = new List<Estimate>();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();
                e.SanctionDate = e.ModifyOn;

            if (estimateRelation.Count > 0)
            {
                e.EstimateAndMaterialOthersRelations = estimateRelation;
                estimates.Add(e);
            }
        }
        ests = null;
        return estimates;
    }

    public List<Estimate> EstimateViewForPurchaseByEmployeeIDByAcaID(int PSID, int UserTypeID, int UserID, int AcaID)
    {
      List<Estimate> estimates = new List<Estimate>();

        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.AcaId == AcaID)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e=>e.ModifyOn).ToList();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId && er.DispatchStatus != 1 && er.PurchaseEmpID == UserID)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();
                e.SanctionDate = e.ModifyOn;

            if (estimateRelation.Count > 0)
            {
                e.EstimateAndMaterialOthersRelations = estimateRelation;
                estimates.Add(e);
            }
        }
        ests = null;
        return estimates;
        
    }

    public List<Estimate> MaterialDepatchStatusForAdminLocalByAcaID(int PSID, int UserTypeID, int inchargeID, int AcaID)
    {
        DateTime dt1 = DateTime.Now.AddDays(-7);

        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.CreatedOn >= dt1)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e => e.ModifyOn).ToList();

        List<Estimate> estimates = new List<Estimate>();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();

            e.SanctionDate = e.ModifyOn;

            if (estimateRelation.Count > 0)
            {
                e.EstimateAndMaterialOthersRelations = estimateRelation;
                estimates.Add(e);
            }
        }
        ests = null;
        return estimates;
    }

    public List<Estimate> MaterialDepatchStatusByAcaID(int PSID, int UserID, int AcaID)
    {
        List<Estimate> estimates = new List<Estimate>();
        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.CreatedBy == UserID && e.AcaId == AcaID)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e=>e.CreatedOn).ToList();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId  && er.DispatchStatus != 1)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();
               e.SanctionDate = e.ModifyOn;

            if (estimateRelation.Count > 0)
            {
                e.EstimateAndMaterialOthersRelations = estimateRelation;
                estimates.Add(e);
            }
        }
        ests = null;
        return estimates;
    }

    public List<Estimate> MaterialDepatchStatusForAdminByAcaID(int PSID, int UserTypeID, int inchargeID, int AcaID)
    {
        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.AcaId >= AcaID)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e => e.ModifyOn).ToList();

        List<Estimate> estimates = new List<Estimate>();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er =>er.EstId == e.EstId && er.PSId == 2 || er.PSId == 3)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();

            e.SanctionDate = e.ModifyOn;

            if (estimateRelation.Count > 0)
            {
                e.EstimateAndMaterialOthersRelations = estimateRelation;
                estimates.Add(e);
            }
        }
        ests = null;
        return estimates;
    }

    public List<Academy> GetAcademy()
    {
        return _context.Academy.OrderBy(x => x.AcaName).ToList();
    }

    public List<Academy> GetAcademybyZoneIDByEmpID(int ZoneID, int InchargeID)
    {
         var Academy = (from z in _context.Academy
                     join AAE in _context.AcademyAssignToEmployee on z.AcaID equals AAE.AcaId
                        where AAE.EmpId == InchargeID && AAE.ZoneId == ZoneID
                     select new
                     {
                         AcaID = z.AcaID,
                         AcaName = z.AcaName,

                     }).AsEnumerable().Select(x => new Academy
                 {
                     AcaID = x.AcaID,
                     AcaName = x.AcaName,
                 }).OrderBy(m => m.AcaName).ToList();

         Academy = Academy.GroupBy(test => test.AcaID)
                   .Select(grp => grp.First())
                   .ToList();
         return Academy;
      
    }
}