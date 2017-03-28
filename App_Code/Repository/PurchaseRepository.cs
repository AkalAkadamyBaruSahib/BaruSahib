using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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

    public List<MaterialsDTO> GetActiveMaterialsByMatTypeID(int MatTypeID)
    {
        List<MaterialsDTO> mt = new List<MaterialsDTO>();
        return mt = _context.Material.Include(u => u.Unit).Where(m => m.Active == 1 && m.MatTypeId == MatTypeID).AsEnumerable().Select(x => new MaterialsDTO
        {
            MatID = x.MatId,
            MatName = x.MatName.Trim(),
            MatCost = x.MatCost,
            AkalWorkshopRate = x.AkalWorkshopRate,
            LocalRate = x.LocalRate,
            Unit = x.Unit,
            MatTypeID = x.MatTypeId
        }).OrderByDescending(m => m.MatName).Reverse().ToList();
    }

    public List<MaterialsDTO> GetMaterialsBySourceTypeID(int sourceTypeID)
    {
        List<MaterialsDTO> mt = new List<MaterialsDTO>();
        if (sourceTypeID == (int)TypeEnum.PurchaseSourceID.AkalWorkshop)
        {
            mt = _context.Material.Include(u => u.Unit).Include(x => x.MaterialType).Where(m => m.Active == 1 && m.MatTypeId == 83).AsEnumerable().Select(x => new MaterialsDTO
            {
                MatID = x.MatId,
                MatName = x.MatName.Trim(),
                MatCost = x.MatCost,
                AkalWorkshopRate = x.AkalWorkshopRate,
                LocalRate = x.LocalRate,
                Unit = x.Unit,
                MatTypeID = x.MatTypeId,
                MaterialType = x.MaterialType
            }).OrderByDescending(m => m.MatName).Reverse().ToList();

        }
        else
        {
            mt = _context.Material.Include(u => u.Unit).Include(x => x.MaterialType).Where(m => m.Active == 1).AsEnumerable().Select(x => new MaterialsDTO
            {
                MatID = x.MatId,
                MatName = x.MatName.Trim(),
                MatCost = x.MatCost,
                AkalWorkshopRate = x.AkalWorkshopRate,
                LocalRate = x.LocalRate,
                Unit = x.Unit,
                MatTypeID = x.MatTypeId,
                MaterialType = x.MaterialType,
            }).OrderByDescending(m => m.MatName).Reverse().ToList();
        }
        return mt;
    }

    public List<MaterialsDTO> GetActiveMaterials()
    {
        List<MaterialsDTO> mt = new List<MaterialsDTO>();
        return mt = _context.Material.Include(u => u.Unit).Include(x => x.MaterialType).Where(m => m.Active == 1).AsEnumerable().Select(x => new MaterialsDTO
        {
            MatID = x.MatId,
            MatName = x.MatName.Trim(),
            MatCost = x.MatCost,
            AkalWorkshopRate = x.AkalWorkshopRate,
            LocalRate = x.LocalRate,
            Unit = x.Unit,
            MatTypeID = x.MatTypeId,
            MaterialType = x.MaterialType
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
            LocalRate = x.LocalRate,
            AkalWorkshopRate = x.AkalWorkshopRate

        }).OrderByDescending(m => m.MatName).Reverse().ToList();
        return mt;
    }

    public List<PurchaseSource> GetPurchaseSource()
    {
        return _context.PurchaseSource.OrderBy(x => x.PSName).ToList();
    }

    public List<VendorInfo> GetVendorsNameList()
    {
        return _context.VendorInfo.ToList();
    }

    public List<Estimate> GetEstimateNumberList(int inchargeID, int purchaseSourceID)
    {
        List<Estimate> estimates = new List<Estimate>();

        DateTime getdate = DateTime.Now.AddDays(-30);

        if (inchargeID > 0)
        {
            estimates = _context.Estimate.Where(e => e.IsApproved == true && e.EstimateAndMaterialOthersRelations.Any(er => er.PSId == purchaseSourceID && er.DispatchStatus == 1 && er.PurchaseEmpID == inchargeID)).ToList();
        }
        else
        {
            estimates = _context.Estimate.Where(e => e.IsApproved == true && e.ModifyOn >= getdate && e.EstimateAndMaterialOthersRelations.Any(er => er.PSId == purchaseSourceID && er.DispatchStatus == 1)).ToList();
        }

        return estimates;
    }

    public List<EstimateAndMaterialOthersRelations> GetMaterialList(int EstimateID)
    {
        return _context.EstimateAndMaterialOthersRelations.Include(m => m.Material).Where(x => x.EstId == EstimateID && x.DispatchStatus == 1).ToList();
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

    public string AddNewVendorInformation(VendorInfo vendorInfo)
    {
        _context.Entry(vendorInfo).State = EntityState.Added;
        _context.SaveChanges();

        VendorInfo vendorName = _context.VendorInfo.Where(v => v.ID == vendorInfo.ID).FirstOrDefault();
        return vendorName.VendorName;
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
            vendorDTO.VendorState = v.VendorState.ToString();
            vendorDTO.VendorCity = v.VendorCity.ToString();
            vendorDTO.VendorZip = v.VendorZip;
            vendorDTO.Active = v.Active;
            vendorDTO.ModifyBy = v.ModifyBy.ToString();
            vendorDTO.ModifyOn = v.ModifyOn.ToString();
            vendorDTO.CreatedOn = v.CreatedOn.ToString();
            vendorDTO.BankName = v.BankName;
            vendorDTO.IfscCode = v.IfscCode;
            vendorDTO.AccountNumber = v.AccountNumber;
            if (v.PanNumber != null)
            {
                vendorDTO.PanNumber = v.PanNumber.ToString();
            }
            if (v.TinNumber != null)
            {
                vendorDTO.TinNumber = v.TinNumber.ToString();
            }


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
            vendorDTO.VendorState = v.VendorState.ToString();
            vendorDTO.VendorCity = v.VendorCity.ToString();
            vendorDTO.VendorZip = v.VendorZip;
            vendorDTO.Active = v.Active;
            vendorDTO.ModifyBy = v.ModifyBy.ToString();
            vendorDTO.ModifyOn = v.ModifyOn.ToString();
            vendorDTO.CreatedOn = v.CreatedOn.ToString();
            vendorDTO.BankName = v.BankName;
            vendorDTO.IfscCode = v.IfscCode;
            vendorDTO.AccountNumber = v.AccountNumber;
            if (v.PanNumber != null)
            {
                vendorDTO.PanNumber = v.PanNumber.ToString();
            }
            if (v.TinNumber != null)
            {
                vendorDTO.TinNumber = v.TinNumber.ToString();
            }

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
        dto.VendorState = vendorInfo.VendorState.ToString();
        dto.VendorCity = vendorInfo.VendorCity.ToString();
        dto.VendorZip = vendorInfo.VendorZip;
        dto.BankName = vendorInfo.BankName;
        dto.IfscCode = vendorInfo.IfscCode;
        dto.AccountNumber = vendorInfo.AccountNumber;
        if (vendorInfo.PanNumber != null)
        {
            dto.PanNumber = vendorInfo.PanNumber.ToString();
        }
        if (vendorInfo.TinNumber != null)
        {
            dto.TinNumber = vendorInfo.TinNumber.ToString();
        }

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
        newVendorInfo.VendorCity = Convert.ToInt32(vendorInfo.VendorCity);
        newVendorInfo.VendorState = Convert.ToInt32(vendorInfo.VendorState);
        newVendorInfo.VendorZip = vendorInfo.VendorZip;
        newVendorInfo.CreatedOn = newVendorInfo.CreatedOn;
        newVendorInfo.ModifyOn = DateTime.Now;
        newVendorInfo.Active = true;
        newVendorInfo.BankName = vendorInfo.BankName;
        newVendorInfo.IfscCode = vendorInfo.IfscCode;
        newVendorInfo.AccountNumber = vendorInfo.AccountNumber;
        newVendorInfo.PanNumber = vendorInfo.PanNumber;
        newVendorInfo.TinNumber = vendorInfo.TinNumber;

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

    public List<MaterialsDTO> GetBindMaterialNameByMaterialType(int MatTypeID)
    {
        List<MaterialsDTO> mt = new List<MaterialsDTO>();
        mt = _context.Material.Include(u => u.Unit).Include(x => x.MaterialType).Where(m => m.Active == 1 && m.MatTypeId == MatTypeID).AsEnumerable().Select(x => new MaterialsDTO
        {
            MatID = x.MatId,
            MatName = x.MatName.Trim(),
            MatCost = x.MatCost,
            AkalWorkshopRate = x.AkalWorkshopRate,
            LocalRate = x.LocalRate,
            Unit = x.Unit,
            MatTypeID = x.MatTypeId,
        }).OrderByDescending(m => m.MatName).Reverse().ToList();

        return mt;
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
        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.IsReceived == false)
            .Include(z => z.Zone)
            .Include(a => a.Academy).ToList();

        List<Estimate> estimates = new List<Estimate>();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId && er.PurchaseEmpID == 0)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();

            if (estimateRelation.Count > 0)
            {
                e.EstimateAndMaterialOthersRelations = estimateRelation;
                estimates.Add(e);
            }
        }

        ests = null;
        return estimates.OrderByDescending(e => e.SanctionDate).ToList();
    }

    public List<Estimate> EstimateViewForWorkshopPurchase(int PSID, int UserTypeID, int InchargeID, int PurchaseEmpID)
    {
        DateTime dt1 = DateTime.Now.AddDays(-60);
        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.SanctionDate >= dt1)
             .Include(z => z.Zone)
             .Include(a => a.Academy).OrderByDescending(e => e.SanctionDate).ToList();

        List<Estimate> estimates = new List<Estimate>();
        if (PurchaseEmpID == 0)
        {
            foreach (Estimate e in ests)
            {
                var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId && er.PurchaseEmpID == 0)
                    .Include(m => m.Material)
                    .Include(u => u.Unit)
                    .Include(i => i.Incharge)
                    .Include(p => p.PurchaseSource).ToList();

               
                if (estimateRelation.Count > 0)
                {
                    e.EstimateAndMaterialOthersRelations = estimateRelation;
                    estimates.Add(e);
                }
            }
        }
        else
        {
            foreach (Estimate e in ests)
            {
                var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId && er.PurchaseEmpID != 0)
                    .Include(m => m.Material)
                    .Include(u => u.Unit)
                    .Include(i => i.Incharge)
                    .Include(p => p.PurchaseSource).ToList();

               

                if (estimateRelation.Count > 0)
                {
                    e.EstimateAndMaterialOthersRelations = estimateRelation;
                    estimates.Add(e);
                }
            }
        }
        ests = null;
        return estimates;
    }

    public List<Estimate> EstimateViewForWorkshopPurchaseByAcaID(int PSID, int UserTypeID, int InchargeID, int AcaID, int PurchaseEmpID)
    {
        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.AcaId == AcaID)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e => e.SanctionDate).ToList();

        List<Estimate> estimates = new List<Estimate>();
        if (PurchaseEmpID == 0)
        {
            foreach (Estimate e in ests)
            {
                var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId && er.PurchaseEmpID == 0)
                    .Include(m => m.Material)
                    .Include(u => u.Unit)
                    .Include(i => i.Incharge)
                    .Include(p => p.PurchaseSource).ToList();

                
                if (estimateRelation.Count > 0)
                {
                    e.EstimateAndMaterialOthersRelations = estimateRelation;
                    estimates.Add(e);
                }
            }
        }
        else
        {
            foreach (Estimate e in ests)
            {
                var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId && er.PurchaseEmpID != 0)
                    .Include(m => m.Material)
                    .Include(u => u.Unit)
                    .Include(i => i.Incharge)
                    .Include(p => p.PurchaseSource).ToList();

                
                if (estimateRelation.Count > 0)
                {
                    e.EstimateAndMaterialOthersRelations = estimateRelation;
                    estimates.Add(e);
                }
            }
        }
        ests = null;
        return estimates;
    }

    public List<Estimate> EstimateViewForPurchaseByEmployeeID(int PSID, int UserTypeID, int InchargeID, int DispatchStatus)
    {
        List<Estimate> estimates = new List<Estimate>();

        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.IsReceived==false)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e => e.SanctionDate).ToList();


        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId && er.DispatchStatus == DispatchStatus && er.PurchaseEmpID == InchargeID)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();

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

        var assignAcademies = _context.AcademyAssignToEmployee.Where(a => a.EmpId == inchargeID).Select(s => s.AcaId).ToList();

        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.SanctionDate >= dt1 && assignAcademies.Contains(e.AcaId))
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e => e.SanctionDate).ToList();

        List<Estimate> estimates = new List<Estimate>();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.EstId == e.EstId && (er.PSId == 2 || er.PSId == 3))
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();

           
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
            .Include(a => a.Academy).OrderByDescending(e => e.SanctionDate).ToList();

        List<Estimate> estimates = new List<Estimate>();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();

         
            if (estimateRelation.Count > 0)
            {
                e.EstimateAndMaterialOthersRelations = estimateRelation;
                estimates.Add(e);
            }
        }
        ests = null;
        return estimates;
    }

    public List<Estimate> MaterialDepatchStatus(int PSID, int UserTypeID, int InchargeID)
    {
        DateTime dt1 = DateTime.Now.AddDays(-30);

        List<Estimate> estimates = new List<Estimate>();
        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.SanctionDate >= dt1 && e.CreatedBy == InchargeID)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e => e.SanctionDate).ToList();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId && er.DispatchStatus != 1)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();
           
            if (estimateRelation.Count > 0)
            {
                e.EstimateAndMaterialOthersRelations = estimateRelation;
                estimates.Add(e);
            }
        }
        ests = null;
        return estimates;
    }

    public List<Estimate> EstimateViewForPurchaseByAcaID(int PSID, int UserTypeID, int InchargeID, int AcaID)
    {
        DateTime dt1 = DateTime.Now.AddDays(-180);

        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.AcaId == AcaID && e.SanctionDate >= dt1 && e.IsReceived == false)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e => e.SanctionDate).ToList();

        List<Estimate> estimates = new List<Estimate>();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId && er.PurchaseEmpID == 0)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();

            if (estimateRelation.Count > 0)
            {
                e.EstimateAndMaterialOthersRelations = estimateRelation;
                estimates.Add(e);
            }
        }

        ests = null;
        return estimates;
    }

    public List<Estimate> EstimateViewForPurchaseByEmployeeIDByAcaID(int PSID, int UserTypeID, int UserID, int AcaID, int DispatchStatus)
    {
        List<Estimate> estimates = new List<Estimate>();

        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.AcaId == AcaID && e.IsReceived == false)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e => e.SanctionDate).ToList();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId && er.DispatchStatus == DispatchStatus && er.PurchaseEmpID == UserID)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();
           
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

        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.SanctionDate >= dt1)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e => e.SanctionDate).ToList();

        List<Estimate> estimates = new List<Estimate>();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();

           
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
            .Include(a => a.Academy).OrderByDescending(e => e.CreatedOn).ToList();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId && er.DispatchStatus != 1)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();
           
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
            .Include(a => a.Academy).OrderByDescending(e => e.SanctionDate).ToList();

        List<Estimate> estimates = new List<Estimate>();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.EstId == e.EstId && er.PSId == 2 || er.PSId == 3)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();

           
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

    public List<Estimate> EstimateDetailByEstId(int EstID, int PSID, int UserTypeId, int UserId)
    {
        List<Estimate> estimates = new List<Estimate>();
        if ((UserTypeId == (int)TypeEnum.UserType.CONSTRUCTION) || (UserTypeId == (int)TypeEnum.UserType.ADMIN) || (UserTypeId == (int)TypeEnum.UserType.WORKSHOPADMIN))
        {
            var ests = _context.Estimate.Where(e => e.EstId == EstID && e.IsActive==true)
                .Include(z => z.Zone)
                .Include(a => a.Academy).ToList();


            foreach (Estimate e in ests)
            {
                var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId)
                    .Include(m => m.Material)
                    .Include(u => u.Unit)
                    .Include(i => i.Incharge)
                    .Include(p => p.PurchaseSource).ToList();


                if (estimateRelation.Count > 0)
                {
                    e.EstimateAndMaterialOthersRelations = estimateRelation;
                    estimates.Add(e);
                }
            }
            ests = null;
        }
        else if (UserTypeId == (int)TypeEnum.UserType.PURCHASE || (UserTypeId == (int)TypeEnum.UserType.PURCHASECOMMITTEE))
        {
            var ests = _context.Estimate.Where(e => e.EstId == EstID && e.IsApproved == true && e.IsActive == true)
             .Include(z => z.Zone)
             .Include(a => a.Academy).ToList();


            foreach (Estimate e in ests)
            {
                var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId)
                    .Include(m => m.Material)
                    .Include(u => u.Unit)
                    .Include(i => i.Incharge)
                    .Include(p => p.PurchaseSource).ToList();


                if (estimateRelation.Count > 0)
                {
                    e.EstimateAndMaterialOthersRelations = estimateRelation;
                    estimates.Add(e);
                }
            }
            ests = null;
        }
        else
        {
            var ests = _context.Estimate.Where(e => e.EstId == EstID && e.IsApproved == true && e.IsActive == true)
               .Include(z => z.Zone)
               .Include(a => a.Academy).ToList();


            foreach (Estimate e in ests)
            {
                var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId && er.PurchaseEmpID == UserId)
                    .Include(m => m.Material)
                    .Include(u => u.Unit)
                    .Include(i => i.Incharge)
                    .Include(p => p.PurchaseSource).ToList();
 

                if (estimateRelation.Count > 0)
                {
                    e.EstimateAndMaterialOthersRelations = estimateRelation;
                    estimates.Add(e);
                }
            }
            ests = null;
        }

        return estimates;
    }

    public List<Estimate> EstimateViewDispatchMaterialForWorkshop(int PSID, int UserTypeID, int InchargeID)
    {

        DateTime dt1 = DateTime.Now.AddDays(-30);

        List<Estimate> estimates = new List<Estimate>();

        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.CreatedOn >= dt1)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e => e.ModifyOn).ToList();


        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId && er.DispatchStatus == 1 && er.PurchaseEmpID == InchargeID)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();

           
            if (estimateRelation.Count > 0)
            {
                e.EstimateAndMaterialOthersRelations = estimateRelation;
                estimates.Add(e);
            }
        }
        ests = null;
        return estimates;

    }

    public List<Estimate> EstimateDispatchForWorkshopByAcaID(int PSID, int UserTypeID, int UserID, int AcaID)
    {
        List<Estimate> estimates = new List<Estimate>();

        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.AcaId == AcaID)
            .Include(z => z.Zone)
            .Include(a => a.Academy).OrderByDescending(e => e.ModifyOn).ToList();

        foreach (Estimate e in ests)
        {
            var estimateRelation = _context.EstimateAndMaterialOthersRelations.Where(er => er.PSId == PSID && er.EstId == e.EstId && er.DispatchStatus == 1 && er.PurchaseEmpID == UserID)
                .Include(m => m.Material)
                .Include(u => u.Unit)
                .Include(i => i.Incharge)
                .Include(p => p.PurchaseSource).ToList();
           
            if (estimateRelation.Count > 0)
            {
                e.EstimateAndMaterialOthersRelations = estimateRelation;
                estimates.Add(e);
            }
        }
        ests = null;
        return estimates;

    }

    public List<MaterialType> GetActiveMaterialTypes()
    {
        return _context.MaterialType.Where(m => m.Active == 1).OrderBy(x=>x.MatTypeName).ToList();
    }

    public void ReceivedMaterial(int EstID, int InchargeID)
    {
        Estimate estimate = _context.Estimate.Where(v => v.EstId == EstID).FirstOrDefault();
        estimate.IsReceived = true;
        estimate.ReceivedMaterialDate = DateTime.UtcNow;
        estimate.ReceivedBy = InchargeID;
        _context.Entry(estimate).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public List<Estimate> MaterialReceivedStatusAcaID(int PSID, int UserID, int AcaID)
    {
        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.CreatedBy == UserID && e.AcaId == AcaID && e.IsReceived == false)
                   .Include(z => z.Zone)
                   .Include(a => a.Academy)
                   .Where(x => x.EstimateAndMaterialOthersRelations.Any(er => er.PSId == PSID))
                   .OrderByDescending(e => e.ModifyOn).ToList();

        return ests;
    }

    public List<Estimate> MaterialReceivedStatusForAdminAcaID(int PSID, int UserID, int AcaID)
    {
        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.AcaId == AcaID && e.IsReceived == false)
                      .Include(z => z.Zone)
                      .Include(a => a.Academy)
                      .Where(x => x.EstimateAndMaterialOthersRelations.Any(er => er.PSId == PSID))
                      .OrderByDescending(e => e.ModifyOn).ToList();
        return ests;
    }

    public List<Estimate> MaterialReceivedStatus(int PSID, int UserID)
    {
        DateTime dt1 = DateTime.Now.AddDays(-30);
        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.CreatedOn >= dt1 && e.CreatedBy == UserID && e.IsReceived == false)
                  .Include(z => z.Zone)
                  .Include(a => a.Academy)
                  .Where(x => x.EstimateAndMaterialOthersRelations.Any(er => er.PSId == PSID))
                  .OrderByDescending(e => e.ModifyOn).ToList();

        return ests;
    }

    public List<Estimate> MaterialReceivedStatusForAdmin(int PSID, int UserID)
    {
        DateTime dt1 = DateTime.Now.AddDays(-30);
        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.CreatedOn >= dt1 && e.IsReceived == false)
                      .Include(z => z.Zone)
                      .Include(a => a.Academy)
                      .Where(x => x.EstimateAndMaterialOthersRelations.Any(er => er.PSId == PSID))
                      .OrderByDescending(e => e.ModifyOn).ToList();
        return ests;
    }

    public List<Estimate> PendingEstimateForPurchaser(int PSID, DateTime date)
    {
        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.CreatedOn >= date)
                      .Include(z => z.Zone)
                      .Include(a => a.Academy)
                      .Where(x => x.EstimateAndMaterialOthersRelations.Any(er => er.PSId == PSID && er.DispatchStatus == 0))
                      .OrderByDescending(e => e.ModifyOn).ToList();
        return ests;
    }

    public AutogeneratedEmail GetReportDay(int ReportType)
    {
        var day = _context.AutogeneratedEmail.Where(x => x.EmailType == ReportType).FirstOrDefault();
        return day;
    }

    public void UpdateAutogeneratedEmail(AutogeneratedEmail emaildays)
    {
        AutogeneratedEmail autogeneratedemail = _context.AutogeneratedEmail.Where(v => v.ID == emaildays.ID).FirstOrDefault();
        autogeneratedemail.EmailSent = emaildays.EmailSent;
        _context.Entry(autogeneratedemail).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public List<Estimate> MaterialClosedReceivedStatusAcaID(int PSID, int UserID, int AcaID)
    {
        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.CreatedBy == UserID && e.AcaId == AcaID && e.IsReceived == true)
                   .Include(z => z.Zone)
                   .Include(a => a.Academy)
                   .Where(x => x.EstimateAndMaterialOthersRelations.Any(er => er.PSId == PSID))
                   .OrderByDescending(e => e.ModifyOn).ToList();

        return ests;
    }

    public List<Estimate> MaterialClosedReceivedStatusForAdminAcaID(int PSID, int UserID, int AcaID)
    {
        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.AcaId == AcaID && e.IsReceived == true)
                      .Include(z => z.Zone)
                      .Include(a => a.Academy)
                      .Where(x => x.EstimateAndMaterialOthersRelations.Any(er => er.PSId == PSID))
                      .OrderByDescending(e => e.ModifyOn).ToList();
        return ests;
    }

    public List<Estimate> MaterialClosedReceivedStatus(int PSID, int UserID)
    {
        DateTime dt1 = DateTime.Now.AddDays(-30);
        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.CreatedOn >= dt1 && e.CreatedBy == UserID && e.IsReceived == true)
                  .Include(z => z.Zone)
                  .Include(a => a.Academy)
                  .Where(x => x.EstimateAndMaterialOthersRelations.Any(er => er.PSId == PSID))
                  .OrderByDescending(e => e.ModifyOn).ToList();

        return ests;
    }

    public List<Estimate> MaterialClosedReceivedStatusForAdmin(int PSID, int UserID)
    {
        DateTime dt1 = DateTime.Now.AddDays(-30);
        var ests = _context.Estimate.Where(e => e.IsApproved == true && e.CreatedOn >= dt1 && e.IsReceived == true)
                      .Include(z => z.Zone)
                      .Include(a => a.Academy)
                      .Where(x => x.EstimateAndMaterialOthersRelations.Any(er => er.PSId == PSID))
                      .OrderByDescending(e => e.ModifyOn).ToList();
        return ests;
    }

    public List<VendorInfoDTO> GetActiveVendor()
    {
        List<VendorInfoDTO> mt = new List<VendorInfoDTO>();
        return mt = _context.VendorInfo.Where(x => x.Active == true).AsEnumerable().Select(x => new VendorInfoDTO
        {
            ID = x.ID,
            VendorName = x.VendorName.Trim(),
        }).OrderByDescending(m => m.VendorName).Reverse().ToList();
    }

    public List<VendorInfo> GetDuplicateVendor(string VendorName)
    {
        return _context.VendorInfo.Where(x => x.VendorName == VendorName).ToList();
    }

    public List<GetBillDetailsByVendorID> GetAgencyMaterialDetails(int VendorID)
    {
        Hashtable param = new Hashtable();
        param.Add("VendorID", VendorID);

        SqlDataReader reader = DAL.DalAccessUtility.GetDataInDataReader("GetBillDetailsByVendorID", param);
        List<GetBillDetailsByVendorID> getBillDetailsByVendorIDs = new List<GetBillDetailsByVendorID>();

        GetBillDetailsByVendorID getBillDetailsByVendorID = null;
        while (reader.Read())
        {
            getBillDetailsByVendorID = new GetBillDetailsByVendorID();
            getBillDetailsByVendorID.VendorName = (string)reader["VendorName"];
            getBillDetailsByVendorID.SubBillId = (int)reader["SubBillId"];
            getBillDetailsByVendorID.WorkAllotName = (string)reader["WorkAllotName"];
            getBillDetailsByVendorID.TotalAmount = (decimal)reader["TotalAmount"];
            getBillDetailsByVendorID.MatName = (string)reader["MatName"];
            getBillDetailsByVendorID.InName = (string)reader["InName"];
            getBillDetailsByVendorID.CreatedOn = (DateTime)reader["CreatedOn"];
            getBillDetailsByVendorIDs.Add(getBillDetailsByVendorID);
        }
        reader.Close();

        return getBillDetailsByVendorIDs;
    }

    public void AddEstimateChangeInfo(EstimateLog log)
    {
        _context.EstimateLog.Add(log);
        _context.SaveChanges();
    }
}