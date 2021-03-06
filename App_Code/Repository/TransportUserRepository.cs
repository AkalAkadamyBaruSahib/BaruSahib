﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AkalAcademy;
using System.Data;
using System.Collections;
/// <summary>
/// Summary description for TransportUserRepository
/// </summary>
public class TransportUserRepository
{

    private DataContext _context;

    public TransportUserRepository(DataContext context)
    {
        _context = context;
    }

    public DataSet GetVehiclesByInchargeID(int InchargeID, bool isApproved)
    {
        //  return DAL.DalAccessUtility.GetDataInDataSet("exec GetVehiclesByInchargeID " + InchargeID );
        return DAL.DalAccessUtility.GetDataInDataSet("exec GetVehiclesByInchargeID " + InchargeID + "," + isApproved);
    }

    public List<Vehicles> GetAllVehicles()
    {
        return _context.Vehicles
            .Include(a => a.Academy)
            .Include(z => z.Zone).ToList();
    }

    public List<Vehicles> GetAllVehiclesByAcademyID(int AcaID)
    {
        return _context.Vehicles.Where(v => v.AcademyID == AcaID && v.IsApproved == true)
            .Include(a => a.Academy)
            .Include(z => z.Zone)
            .Include(ve => ve.VehicleEmployee)
            .ToList();
    }

    public Vehicles GetVehicleByVehicleID(int VehicleID)
    {
        return _context.Vehicles.Where(v => v.ID == VehicleID)
            .Include(a => a.Academy)
            .Include(z => z.Zone)
            .Include(ve => ve.VehicleEmployee)
            .FirstOrDefault();
    }

    public List<Vehicles> GetVehiclesByZoneID(int ZoneID)
    {
        return _context.Vehicles.Where(v => v.ZoneID == ZoneID)
            .Include(a => a.Academy)
            .Include(z => z.Zone).ToList();
    }

    public List<Vehicles> GetVehiclesByZoneID(int ZoneID, bool IsApproved)
    {
        return _context.Vehicles.Where(v => v.ZoneID == ZoneID && v.IsApproved == IsApproved)
            .Include(a => a.Academy)
            .Include(z => z.Zone).Include(t => t.TransportTypes).Include(ve => ve.VehicleEmployee).OrderByDescending(v => v.AcademyID).ToList();
    }

    public List<Vehicles> GetAllVehiclesByZoneID(int ZoneID)
    {
        return _context.Vehicles.Where(x => x.ZoneID == ZoneID)
            .Include(a => a.Academy)
            .Include(z => z.Zone).ToList();
    }

    public List<VechilesDocumentRelation> GetVechilesDocumentRelationByVehicleID(int VehicleID)
    {
        return _context.VechilesDocumentRelation.Where(x => x.VehicleID == VehicleID && x.IsApproved == true).ToList();
    }

    public int SaveNewTransportEmployee(VehicleEmployee VehicleEmp)
    {
        Hashtable param = new Hashtable();
        param.Add("EmployeeType", VehicleEmp.EmployeeType);
        param.Add("TransportTypeID", VehicleEmp.TransportTypeID);
        param.Add("Name", VehicleEmp.Name);
        param.Add("MobileNumber", VehicleEmp.MobileNumber);
        param.Add("VehicleID", VehicleEmp.VehicleID);
        param.Add("DLType", VehicleEmp.DLType);
        param.Add("DLValidity", VehicleEmp.DLValidity);
        param.Add("CreatedOn", DateTime.Now);
        param.Add("Address", VehicleEmp.Address);
        param.Add("FatherName", VehicleEmp.FatherName);
        param.Add("DateOfBirth", VehicleEmp.DateOfBirth);
        param.Add("Qualification", VehicleEmp.Qualification);
        param.Add("DLNumber", VehicleEmp.DLNumber);
        param.Add("DLScanCopy", VehicleEmp.DLScanCopy);
        param.Add("DateOfJoining", VehicleEmp.DateOfJoining);
        param.Add("ApplicationForm", VehicleEmp.ApplicationForm);
        param.Add("ContactNoInCaseOfEmergency", VehicleEmp.ContactNoInCaseOfEmergency);
        param.Add("PreviousCompanyName", VehicleEmp.PreviousCompanyName);
        param.Add("ExperienceInYear", VehicleEmp.ExperienceInYear);
        param.Add("ExperienceInMonth", VehicleEmp.ExperienceInMonth);
        param.Add("ModifyOn", DateTime.Now);
        param.Add("IsActive", true);
        int VehicleEmployeeID = DAL.DalAccessUtility.GetDataInScaler("procSaveTransportEmployee", param);

        return VehicleEmployeeID;
    }

    public void SaveTransEmpRelation(TransportEmployeeRelation TransportEmployeeRelation)
    {
        Hashtable param = new Hashtable();
        param.Add("VehicleEmployeeID", TransportEmployeeRelation.VehicleEmployeeID);
        param.Add("Name", TransportEmployeeRelation.Name);
        param.Add("Age", TransportEmployeeRelation.Age);
        param.Add("Relation", TransportEmployeeRelation.Relation);
        param.Add("Nominee", TransportEmployeeRelation.Nominee);
        param.Add("PhoneNo", TransportEmployeeRelation.PhoneNo);
        param.Add("Address", TransportEmployeeRelation.Address);
        param.Add("RelationTypeID", TransportEmployeeRelation.RelationTypeID);
        param.Add("CreatedOn", DateTime.Now);
        param.Add("ModifyOn", DateTime.Now);
        int TransportEmpRelationID = DAL.DalAccessUtility.GetDataInScaler("procSaveTransportEmployeeRelation", param);

    }

    public List<VehicleEmployeeDTO> GeTransportEmployeeInformation(int EmployeeTypeID, int VehicleID)
    {

        List<VehicleEmployee> vehicleEmployee = _context.VehicleEmployee.Where(v => v.IsActive == true && v.EmployeeType == EmployeeTypeID && v.VehicleID == VehicleID)
            .Include(a => a.Vehicle)
         .Include(e => e.TransportEmployeeRelation).Distinct().OrderByDescending(x => x.CreatedOn).ToList();


        DataSet employeetype = new DataSet();
        List<VehicleEmployeeDTO> dto = new List<VehicleEmployeeDTO>();

        VehicleEmployeeDTO employeeDTO = null;
        foreach (VehicleEmployee v in vehicleEmployee)
        {
            employeeDTO = new VehicleEmployeeDTO();

            employeeDTO.ID = v.ID;
            employeeDTO.Name = v.Name;
            employeeDTO.EmployeeType = v.EmployeeType;
            employeeDTO.MobileNumber = v.MobileNumber;
            employeeDTO.DLType = v.DLType;
            employeeDTO.DLValidity = v.DLValidity.ToString();
            employeeDTO.CreatedOn = v.CreatedOn.ToString();
            employeeDTO.Address = v.Address;
            employeeDTO.FatherName = v.FatherName;
            employeeDTO.DateOfBirth = v.DateOfBirth.ToString();
            employeeDTO.Qualification = v.Qualification;
            employeeDTO.DLNumber = v.DLNumber;
            employeeDTO.DLScanCopy = v.DLScanCopy;
            if (v.DateOfJoining != null)
            {
                employeeDTO.DateOfJoining = v.DateOfJoining.Value.ToShortDateString();
            }
            employeeDTO.ApplicationForm = v.ApplicationForm;
            employeeDTO.ContactNoInCaseOfEmergency = v.ContactNoInCaseOfEmergency;
            employeeDTO.PreviousCompanyName = v.PreviousCompanyName;
            employeeDTO.ExperienceInYear = v.ExperienceInYear;
            employeeDTO.ExperienceInMonth = v.ExperienceInMonth;
            employeeDTO.ModifyOn = v.ModifyOn.ToString();
            employeeDTO.IsActive = v.IsActive;
            employeeDTO.TransportTypeID = v.TransportTypeID;
            employeeDTO.VehicleID = v.VehicleID;
            if (v.Vehicle != null)
            {
                employeeDTO.VehicleNumber = v.Vehicle.Number;
            }
            else
            {
                employeeDTO.VehicleNumber = string.Empty;
            }

            employeeDTO.TransportEmployeeRelationDTO = new List<TransportEmployeeRelationDTO>();
            TransportEmployeeRelationDTO tranemprelatn;
            foreach (TransportEmployeeRelation tmr in v.TransportEmployeeRelation)
            {
                tranemprelatn = new TransportEmployeeRelationDTO();
                tranemprelatn.VehicleEmployeeID = tmr.VehicleEmployeeID;
                tranemprelatn.Name = tmr.Name;
                tranemprelatn.Age = tmr.Age;
                tranemprelatn.Relation = tmr.Relation;
                tranemprelatn.Nominee = tmr.Nominee;
                tranemprelatn.PhoneNo = tmr.PhoneNo;
                tranemprelatn.Address = tmr.Address;
                tranemprelatn.RelationTypeID = tmr.RelationTypeID;
                tranemprelatn.CreatedOn = tmr.CreatedOn.ToString();
                tranemprelatn.ModifyOn = tmr.ModifyOn.ToString();
                employeeDTO.TransportEmployeeRelationDTO.Add(tranemprelatn);
            }
            dto.Add(employeeDTO);
        }

        // return dto.OrderByDescending(x => x.CreatedOn).ToList();
        return dto;
    }

    public VehicleEmployeeDTO GetTranportEmployeeInfoToUpdate(int VehicleEmployeeID)
    {
        VehicleEmployee vehicleEmployee = _context.VehicleEmployee.Where(v => v.ID == VehicleEmployeeID)
            .Include(e => e.TransportEmployeeRelation)
          .FirstOrDefault();


        VehicleEmployeeDTO dto = new VehicleEmployeeDTO();
        dto.ID = vehicleEmployee.ID;
        dto.Name = vehicleEmployee.Name;
        dto.EmployeeType = vehicleEmployee.EmployeeType;
        dto.TransportTypeID = vehicleEmployee.TransportTypeID;
        dto.MobileNumber = vehicleEmployee.MobileNumber;
        dto.DLType = vehicleEmployee.DLType;
        dto.DLValidity = vehicleEmployee.DLValidity.ToString();
        dto.Address = vehicleEmployee.Address;
        dto.FatherName = vehicleEmployee.FatherName;
        dto.DateOfBirth = vehicleEmployee.DateOfBirth.Value.ToShortDateString();
        dto.DLNumber = vehicleEmployee.DLNumber;
        if (vehicleEmployee.DateOfJoining != null)
        {
            dto.DateOfJoining = vehicleEmployee.DateOfJoining.Value.ToShortDateString();
        }
        dto.ContactNoInCaseOfEmergency = vehicleEmployee.ContactNoInCaseOfEmergency;
        dto.PreviousCompanyName = vehicleEmployee.PreviousCompanyName;
        dto.ExperienceInYear = vehicleEmployee.ExperienceInYear;
        dto.ExperienceInMonth = vehicleEmployee.ExperienceInMonth;
        dto.IsActive = vehicleEmployee.IsActive;
        if (vehicleEmployee.Qualification != "")
        {
            dto.Qualification = vehicleEmployee.Qualification;
        }
        dto.CreatedOn = vehicleEmployee.CreatedOn.ToString();
        dto.ModifyOn = vehicleEmployee.ModifyOn.ToString();
        if (vehicleEmployee.ApplicationForm != "")
        {
            dto.ApplicationForm = vehicleEmployee.ApplicationForm;
        }

        if (vehicleEmployee.DLScanCopy != "")
        {
            dto.DLScanCopy = vehicleEmployee.DLScanCopy;
        }
        dto.VehicleID = vehicleEmployee.VehicleID;

        List<TransportEmployeeRelationDTO> TransEmpRelations = new List<TransportEmployeeRelationDTO>();
        TransportEmployeeRelationDTO TransEmpRelation;
        foreach (TransportEmployeeRelation rm in vehicleEmployee.TransportEmployeeRelation)
        {
            TransEmpRelation = new TransportEmployeeRelationDTO();
            TransEmpRelation.ID = rm.ID;
            TransEmpRelation.VehicleEmployeeID = rm.VehicleEmployeeID;
            TransEmpRelation.Name = rm.Name;
            TransEmpRelation.Age = rm.Age;
            TransEmpRelation.Relation = rm.Relation;
            TransEmpRelation.Nominee = rm.Nominee;
            TransEmpRelation.PhoneNo = rm.PhoneNo;
            TransEmpRelation.Address = rm.Address;
            TransEmpRelation.RelationTypeID = rm.RelationTypeID;
            TransEmpRelation.CreatedOn = rm.CreatedOn.ToString();
            TransEmpRelation.ModifyOn = rm.ModifyOn.ToString();
            TransEmpRelations.Add(TransEmpRelation);
        }

        dto.TransportEmployeeRelationDTO = TransEmpRelations;
        return dto;
        //   newVisitor

        //declate all properties
    }

    public void UpdateTransportEmployeeInfo(VehicleEmployee VehicleEmp)
    {
        _context.TransportEmployeeRelation.RemoveRange(_context.TransportEmployeeRelation.Where(v => v.VehicleEmployeeID == VehicleEmp.ID));

        VehicleEmployee newVehicleEmployee = _context.VehicleEmployee.Where(v => v.ID == VehicleEmp.ID)
            .Include(r => r.TransportEmployeeRelation)
            .FirstOrDefault();

        newVehicleEmployee.Name = VehicleEmp.Name;
        newVehicleEmployee.EmployeeType = VehicleEmp.EmployeeType;
        newVehicleEmployee.MobileNumber = VehicleEmp.MobileNumber;
        newVehicleEmployee.DLType = VehicleEmp.DLType;
        newVehicleEmployee.DLNumber = VehicleEmp.DLNumber;
        newVehicleEmployee.DLValidity = VehicleEmp.DLValidity;
        newVehicleEmployee.Address = VehicleEmp.Address;
        newVehicleEmployee.FatherName = VehicleEmp.FatherName;
        newVehicleEmployee.DateOfBirth = VehicleEmp.DateOfBirth;
        newVehicleEmployee.DateOfJoining = VehicleEmp.DateOfJoining;
        newVehicleEmployee.ContactNoInCaseOfEmergency = VehicleEmp.ContactNoInCaseOfEmergency;
        newVehicleEmployee.PreviousCompanyName = VehicleEmp.PreviousCompanyName;
        newVehicleEmployee.ExperienceInYear = VehicleEmp.ExperienceInYear;
        newVehicleEmployee.ExperienceInMonth = VehicleEmp.ExperienceInMonth;
        if (VehicleEmp.Qualification != null)
        {
            newVehicleEmployee.Qualification = VehicleEmp.Qualification;
        }
        if (VehicleEmp.ApplicationForm != null)
        {
            newVehicleEmployee.ApplicationForm = VehicleEmp.ApplicationForm;
        }
        if (VehicleEmp.DLScanCopy != null)
        {
            newVehicleEmployee.DLScanCopy = VehicleEmp.DLScanCopy;
        }
        newVehicleEmployee.CreatedOn = newVehicleEmployee.CreatedOn;
        newVehicleEmployee.ModifyOn = DateTime.Now;
        newVehicleEmployee.IsActive = true;
        newVehicleEmployee.TransportTypeID = VehicleEmp.TransportTypeID;
        newVehicleEmployee.VehicleID = VehicleEmp.VehicleID;


        newVehicleEmployee.TransportEmployeeRelation = new List<TransportEmployeeRelation>();
        TransportEmployeeRelation transportEmployeeRelation;
        foreach (TransportEmployeeRelation rm in VehicleEmp.TransportEmployeeRelation)
        {
            transportEmployeeRelation = new TransportEmployeeRelation();
            transportEmployeeRelation.VehicleEmployeeID = rm.VehicleEmployeeID;
            transportEmployeeRelation.Name = rm.Name;
            transportEmployeeRelation.Age = rm.Age;
            transportEmployeeRelation.Relation = rm.Relation;
            transportEmployeeRelation.Nominee = rm.Nominee;
            transportEmployeeRelation.PhoneNo = rm.PhoneNo;
            transportEmployeeRelation.Address = rm.Address;
            transportEmployeeRelation.RelationTypeID = rm.RelationTypeID;

            transportEmployeeRelation.CreatedOn = newVehicleEmployee.CreatedOn;
            transportEmployeeRelation.ModifyOn = DateTime.Now;
            newVehicleEmployee.TransportEmployeeRelation.Add(transportEmployeeRelation);
        }

        _context.Entry(newVehicleEmployee).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void TranportEmployeeInfoToDelete(int vehicleEmployeeID)
    {

        _context.TransportEmployeeRelation.RemoveRange(_context.TransportEmployeeRelation.Where(v => v.VehicleEmployeeID == vehicleEmployeeID));

        VehicleEmployee DelVehicleEmployee = _context.VehicleEmployee.Where(v => v.ID == vehicleEmployeeID)
            .Include(r => r.TransportEmployeeRelation)
            .FirstOrDefault();



        DelVehicleEmployee.TransportEmployeeRelation = null;
        DelVehicleEmployee.IsActive = false;
        _context.Entry(DelVehicleEmployee).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void AddVechiclesRouteMap(VechiclesRouteMap vechiclesroutemap)
    {
        _context.Entry(vechiclesroutemap).State = EntityState.Added;
        _context.SaveChanges();

    }

    public void DeleteVechicleInfo(int VID)
    {
        Vehicles vehicles = _context.Vehicles.Where(v => v.ID == VID)
                             .FirstOrDefault();
        vehicles.IsApproved = false;
        _context.Entry(vehicles).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void ActiveVechicleInfo(int VID)
    {
        Vehicles vehicles = _context.Vehicles.Where(v => v.ID == VID)
                             .FirstOrDefault();
        vehicles.IsApproved = true;
        _context.Entry(vehicles).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public List<VehiclesDTO> GetActiveVehicles()
    {
        List<VehiclesDTO> mt = new List<VehiclesDTO>();
        return mt = _context.Vehicles.AsEnumerable().Select(x => new VehiclesDTO
        {
            ID = x.ID,
            Number = x.Number,
        }).OrderByDescending(m => m.Number).Reverse().ToList();

    }

    public List<VehiclesDTO> GetActiveVehiclesByInchargeID(int InchargeID)
    {
        var vehicle = (from V in _context.Vehicles
                       join A in _context.Academy on V.AcademyID equals A.AcaID
                       join AAE in _context.AcademyAssignToEmployee on A.AcaID equals AAE.AcaId
                       where AAE.EmpId == InchargeID
                       select new
                       {
                           ID = V.ID,
                           Number = V.Number,
                       }).AsEnumerable().Select(x => new VehiclesDTO
                      {
                          ID = x.ID,
                          Number = x.Number,
                      }).OrderByDescending(m => m.Number).Reverse().ToList();

        vehicle = vehicle.GroupBy(v => v.ID).Select(s => s.First()).ToList();

        return vehicle;
    }

    public VehicleEmployee GetEmployeeByVehicleID(int VehicleID, int EmployeeType)
    {
        return _context.VehicleEmployee.Where(x => x.VehicleID == VehicleID && x.EmployeeType == EmployeeType && x.IsActive == true).FirstOrDefault();
    }

    public List<VechilesNormsRelation> GetVechilesNormsRelationByVehicleID(int VehicleID)
    {
        return _context.VechilesNormsRelation.Where(x => x.VehicleID == VehicleID).ToList();
    }

    public List<Vehicles> GetVehiclesKmByVehicleID(int VehicleID)
    {
        return _context.Vehicles.Where(x => x.ID == VehicleID).ToList();
    }

    public List<VehicleContractRate> getVehiclesContractRateBySeater(int SeatingCapacity)
    {
        return _context.VehicleContractRate.Where(x => x.SeatCapacity == SeatingCapacity).ToList();
    }

    public List<VehiclesDTO> GetContracturalVehiclesByAcaID(int AcaID, bool isApproved)
    {
        List<VehiclesDTO> mt = new List<VehiclesDTO>();
        return mt = _context.Vehicles.Where(v => v.AcademyID == AcaID && v.IsApproved == isApproved && (v.TypeID == (int)TypeEnum.TransportType.Contractual || v.TypeID == (int)TypeEnum.TransportType.DailyWages || v.TypeID == (int)TypeEnum.TransportType.Passengervehicle)).AsEnumerable().Select(x => new VehiclesDTO
        {

            ID = x.ID,
            Number = x.Number,
            OwnerName = x.OwnerName,
            OwnerNumber = x.OwnerNumber
        }).OrderByDescending(m => m.Number).Reverse().ToList();
    }

    public List<Vehicles> GetVehicleDetailByVehicleID(int VehicleID)
    {
        return _context.Vehicles.Where(v => v.ID == VehicleID)
            .Include(a => a.Academy)
            .Include(z => z.Zone).ToList();
    }

    public int GetUnApprovedEstimates(int ModuleID)
    {
        return _context.Estimate.Where(e => e.IsApproved == false && e.ModuleID == ModuleID && e.IsActive == true)
        .Include(r => r.EstimateAndMaterialOthersRelations).Where(r => r.EstimateAndMaterialOthersRelations.Any(er => er.MatTypeId == 49)).Count();
    }

    public List<Academy> GetAcademyByInchargeID(int InchargeID)
    {
        var Zones = (from z in _context.Academy
                     join AAE in _context.AcademyAssignToEmployee on z.AcaID equals AAE.AcaId
                     where AAE.EmpId == InchargeID
                     select new
                     {
                         AcaID = z.AcaID,
                         AcaName = z.AcaName,

                     }).AsEnumerable().Select(x => new Academy
                     {
                         AcaID = x.AcaID,
                         AcaName = x.AcaName,
                     }).OrderBy(m => m.AcaName).ToList();

        Zones = Zones.GroupBy(test => test.AcaID)
                   .Select(grp => grp.First())
                   .ToList();
        return Zones;
    }

    public List<Vehicles> GetVehiclesByAcademyIDandTypeID(int AcaID, int TypeID)
    {
        return _context.Vehicles.Where(x => x.AcademyID == AcaID && x.IsApproved == true && x.TypeID != TypeID).ToList();
    }

    public List<Vehicles> GetTrustVehiclesByAcademyIdAndTypeID(int AcaID, int TypeID)
    {
        return _context.Vehicles.Where(x => x.AcademyID == AcaID && x.IsApproved == true && x.TypeID == TypeID).ToList();
    }

    public Vehicles GetVehiclesInfoByVehicleID(int VehicleID)
    {
        return _context.Vehicles.Where(v => v.ID == VehicleID).Include(t => t.TransportTypes).FirstOrDefault();
    }

    public VehicleEmployee GetVehicleEmployeeInfo(int VehicleID, int EmpType)
    {
        return _context.VehicleEmployee.Where(v => v.VehicleID == VehicleID && v.EmployeeType == EmpType && v.IsActive == true).FirstOrDefault();
    }
    public DieselPetrolPrice GetCurrentDieselRateByAcaID(int AcaID)
    {
        return _context.DieselPetrolPrice.Where(v => v.AcaID == AcaID).FirstOrDefault();
    }
    public List<UserRole> GetUserRoleByInchargeID(int UserID)
    {
        return _context.UserRole.Where(x => x.UserID == UserID).ToList();
    }

    public SittingTyreRelation GetVehiclesTyreCountBySitting(int seatingCapacity)
    {
        return _context.SittingTyreRelation.Where(x => x.SittingCapacity == seatingCapacity).FirstOrDefault();
    }

    public int SaveVehicleDetailService(VehicleServiceRecord VehicleServiceRecord)
    {
        _context.VehicleServiceRecord.Add(VehicleServiceRecord);
        _context.SaveChanges();

        return VehicleServiceRecord.ID;
    }

    public List<VehicleServiceRecord> GetLoadVehicleServiceInformation(int inchargeID)
    {
        List<VehicleServiceRecord> mt = new List<VehicleServiceRecord>();
        return mt = _context.VehicleServiceRecord.Include(v => v.Vehicles).Include(v => v.Academy).Where(v => v.CreatedBy == inchargeID).AsEnumerable().Select(v => new VehicleServiceRecord
        {
            ID = v.ID,
            VehicleID = v.VehicleID,
            AcaID = v.AcaID,
            CurrentKm = v.CurrentKm,
            LastServiceKm = v.LastServiceKm,
            MeterReadingFilePath = v.MeterReadingFilePath,
            BatteryInstalationDate = v.BatteryInstalationDate,
            CreatedOn = v.CreatedOn,
            CreatedBy = v.CreatedBy,
            Vehicles = v.Vehicles,
            Academy = v.Academy,
            MakeofBattery = v.MakeofBattery,
            BatteryCapacity = v.BatteryCapacity,
            BatterySerialNum = v.BatterySerialNum,
            BatteryLifeInYears = v.BatteryLifeInYears,
            LastServiceDate = v.LastServiceDate

        }).OrderByDescending(x => x.CreatedOn).ToList();
    }

    public VehicleServiceRecord GetGetVehicleInfoToUpdate(int VehicleServiceID)
    {
        VehicleServiceRecord vehicleServiceRecord = _context.VehicleServiceRecord.Where(v => v.ID == VehicleServiceID).FirstOrDefault();
        return vehicleServiceRecord;
        // Get Info through upper query
    }

    public void UpdateVehicleDetailService(VehicleServiceRecord VehicleServiceRecord)
    {
        VehicleServiceRecord newVehicleServiceRecord = _context.VehicleServiceRecord.Where(v => v.ID == VehicleServiceRecord.ID)
            .FirstOrDefault();

        newVehicleServiceRecord.ID = VehicleServiceRecord.ID;
        newVehicleServiceRecord.VehicleID = VehicleServiceRecord.VehicleID;
        newVehicleServiceRecord.AcaID = VehicleServiceRecord.AcaID;
        newVehicleServiceRecord.CurrentKm = VehicleServiceRecord.CurrentKm;
        newVehicleServiceRecord.FrontLeftKm = VehicleServiceRecord.FrontLeftKm;
        newVehicleServiceRecord.FrontRightSerialNum = VehicleServiceRecord.FrontRightSerialNum;
        newVehicleServiceRecord.FrontLeftSerialNum = VehicleServiceRecord.FrontLeftSerialNum;
        newVehicleServiceRecord.FrontRightKm = VehicleServiceRecord.FrontRightKm;
        newVehicleServiceRecord.RearLeftOneKm = VehicleServiceRecord.RearLeftOneKm;
        newVehicleServiceRecord.RearLeftSecondKm = VehicleServiceRecord.RearLeftSecondKm;
        newVehicleServiceRecord.RearRightOneKm = VehicleServiceRecord.RearRightOneKm;
        newVehicleServiceRecord.RearRightSecondKm = VehicleServiceRecord.RearRightSecondKm;
        newVehicleServiceRecord.RearLeftOneSerialNum = VehicleServiceRecord.RearLeftOneSerialNum;
        newVehicleServiceRecord.RearLeftSecondSerialNum = VehicleServiceRecord.RearLeftSecondSerialNum;
        newVehicleServiceRecord.RearRightOneSerialNum = VehicleServiceRecord.RearRightOneSerialNum;
        newVehicleServiceRecord.RearRightSecondSerialNum = VehicleServiceRecord.RearRightSecondSerialNum;
        newVehicleServiceRecord.LastServiceKm = VehicleServiceRecord.LastServiceKm;
        newVehicleServiceRecord.BatteryInstalationDate = VehicleServiceRecord.BatteryInstalationDate;
        newVehicleServiceRecord.StafneySerialNum = VehicleServiceRecord.StafneySerialNum;
        newVehicleServiceRecord.StafneyKm = VehicleServiceRecord.StafneyKm;
        newVehicleServiceRecord.MakeofBattery = VehicleServiceRecord.MakeofBattery;
        newVehicleServiceRecord.BatteryCapacity = VehicleServiceRecord.BatteryCapacity;
        newVehicleServiceRecord.BatterySerialNum = VehicleServiceRecord.BatterySerialNum;
        newVehicleServiceRecord.BatteryLifeInYears = VehicleServiceRecord.BatteryLifeInYears;
        newVehicleServiceRecord.LastServiceDate = VehicleServiceRecord.LastServiceDate;
        newVehicleServiceRecord.FrontLeftDateChanged = VehicleServiceRecord.FrontLeftDateChanged;
        newVehicleServiceRecord.FrontRightDateChanged = VehicleServiceRecord.FrontRightDateChanged;
        newVehicleServiceRecord.RearLeftOneDateChanged = VehicleServiceRecord.RearLeftOneDateChanged;
        newVehicleServiceRecord.RearLeftSecondDateChanged = VehicleServiceRecord.RearLeftSecondDateChanged;
        newVehicleServiceRecord.RearRightOneDateChanged = VehicleServiceRecord.RearRightOneDateChanged;
        newVehicleServiceRecord.RearRightSecondDateChanged = VehicleServiceRecord.RearRightSecondDateChanged;
        newVehicleServiceRecord.StafneyDateChanged = VehicleServiceRecord.StafneyDateChanged;
        newVehicleServiceRecord.BatteryChangeMeterReading = VehicleServiceRecord.BatteryChangeMeterReading;

        _context.Entry(newVehicleServiceRecord).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void AddNewStudents(StudentDetailInTransport student)
    {
        _context.Entry(student).State = EntityState.Added;
        _context.SaveChanges();
    }

    public void AddNewStaffDetail(StaffDetailInTransport staff)
    {
        _context.Entry(staff).State = EntityState.Added;
        _context.SaveChanges();
    }
    public void UpdateStudentsInfo(StudentDetailInTransport student)
    {
        StudentDetailInTransport stu = _context.StudentDetailInTransport.Where(v => v.ID == student.ID).FirstOrDefault();
        stu.ID = student.ID;
        stu.ContactNo = student.ContactNo;
        stu.Class = student.Class;
        stu.StudentName = student.StudentName;
        stu.NameOfVillage = student.NameOfVillage;
        stu.FatherName = student.FatherName;
        stu.AcaID = student.AcaID;

        _context.Entry(stu).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void UpdateStaffDetail(StaffDetailInTransport staff)
    {
        StaffDetailInTransport stu = _context.StaffDetailInTransport.Where(v => v.ID == staff.ID).FirstOrDefault();
        stu.ID = staff.ID;
        stu.StaffName = staff.StaffName;
        stu.FatherName = staff.FatherName;
        stu.StaffType = staff.StaffType;
        stu.CreatedBy = staff.CreatedBy;
        _context.Entry(stu).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public List<StudentDetailInTransport> GeTransportStudentInfo(int inchargeID)
    {
        var vehicle = (from S in _context.StudentDetailInTransport
                       join A in _context.Academy on S.AcaID equals A.AcaID
                       join AAE in _context.AcademyAssignToEmployee on A.AcaID equals AAE.AcaId
                       where AAE.EmpId == inchargeID
                       select new
                       {
                           ID= S.ID,
                           Class = S.Class,
                           AdmissionNumber = S.AdmissionNumber,
                           StudentName = S.StudentName,
                           FatherName = S.FatherName,
                           ContactNo = S.ContactNo,
                           NameOfVillage = S.NameOfVillage,

                       }).AsEnumerable().Select(x => new StudentDetailInTransport
                       {
                           ID = x.ID,
                           Class = x.Class,
                           AdmissionNumber = x.AdmissionNumber,
                           StudentName = x.StudentName,
                           FatherName = x.FatherName,
                           ContactNo = x.ContactNo,
                           NameOfVillage = x.NameOfVillage,
                       }).OrderByDescending(m => m.AdmissionNumber).Reverse().ToList();
      
        vehicle = vehicle.GroupBy(v => v.ID).Select(s => s.First()).ToList();
        return vehicle;
    }

    public List<StaffDetailInTransport> GetStaffInfoInTransport(int inchargeID)
    {
        if (inchargeID == 32)
        {
            return _context.StaffDetailInTransport.OrderByDescending(m => m.CreatedBy).ToList();
        }
        else
        {
            return _context.StaffDetailInTransport.Where(x => x.CreatedBy == inchargeID).OrderByDescending(m => m.CreatedBy).ToList();
        }
    }

    public StudentDetailInTransport GetStudentInfoByStudentID(int studentID)
    {
        StudentDetailInTransport studentinfo = _context.StudentDetailInTransport.Where(v => v.ID == studentID).FirstOrDefault();
        return studentinfo;
    }

    public void GetStudentInfoToDeleteByStudentID(int studentID)
    {
        StudentDetailInTransport DelVehicleEmployee = _context.StudentDetailInTransport.Where(v => v.ID == studentID).FirstOrDefault();
       _context.Entry(DelVehicleEmployee).State = EntityState.Deleted;
        _context.SaveChanges();
    }

    public StaffDetailInTransport GetStaffInfoByStaffID(int staffID)
    {
        StaffDetailInTransport staffinfo = _context.StaffDetailInTransport.Where(v => v.ID == staffID).FirstOrDefault();
        return staffinfo;
    }

    public void GetStaffInfoToDeleteByStaffID(int staffID)
    {
        StaffDetailInTransport DelStaff = _context.StaffDetailInTransport.Where(v => v.ID == staffID).FirstOrDefault();
        _context.Entry(DelStaff).State = EntityState.Deleted;
        _context.SaveChanges();
    }

    public int SaveProformaDetail(ProformaDetail proforma)
    {
        _context.Entry(proforma).State = EntityState.Added;
        _context.SaveChanges();
        return proforma.ID;
    }


    public int SaveGensetProformaMaterialDetail(ProformaMaterialDetail matDetail)
    {
        _context.Entry(matDetail).State = EntityState.Added;
        _context.SaveChanges();
        return matDetail.ID;
    }

    public int SaveDailyproformaDetail(Android_TransportDailyProformaDetail proforma)
    {
        _context.Entry(proforma).State = EntityState.Added;
        _context.SaveChanges();

        return proforma.TProformaID;
    }
}