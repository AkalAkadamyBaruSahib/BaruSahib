using System;
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

    public DataSet GetVehiclesByInchargeID(int InchargeID)
    {
        return DAL.DalAccessUtility.GetDataInDataSet("exec GetVehiclesByInchargeID " + InchargeID);
    }

    public List<Vehicles> GetAllVehicles()
    {
        return _context.Vehicles
            .Include(a => a.Academy)
            .Include(z => z.Zone).ToList();
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
            .Include(z => z.Zone).ToList();
    }

    public List<Vehicles> GetAllVehiclesByZoneID(int ZoneID)
    {
        return _context.Vehicles.Where(x => x.ZoneID == ZoneID)
            .Include(a => a.Academy)
            .Include(z => z.Zone).ToList();
    }

    public List<VechilesDocumentRelation> GetVechilesDocumentRelationByVehicleID(int VehicleID)
    {
        return _context.VechilesDocumentRelation.Where(x => x.VehicleID == VehicleID).ToList();
    }

    public int SaveNewTransportEmployee(VehicleEmployee VehicleEmp)
    {
        Hashtable param = new Hashtable();
        param.Add("EmployeeType", VehicleEmp.EmployeeType);
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

    public List<VehicleEmployeeDTO> GeTransportEmployeeInformation(int VehicleEmployeeID)
    {
       
        List<VehicleEmployee> vehicleEmployee = _context.VehicleEmployee.Where(v => v.IsActive == true)
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
            employeeDTO.DLValidity = v.DLValidity;
            employeeDTO.CreatedOn = v.CreatedOn.ToString();
            employeeDTO.Address = v.Address;
            employeeDTO.FatherName = v.FatherName;
            employeeDTO.DateOfBirth = v.DateOfBirth.ToString();
            employeeDTO.Qualification = v.Qualification;
            employeeDTO.DLNumber = v.DLNumber;
            employeeDTO.DLScanCopy = v.DLScanCopy;
            employeeDTO.DateOfJoining = v.DateOfJoining.Value.ToShortDateString();
            employeeDTO.ApplicationForm = v.ApplicationForm;
            employeeDTO.ContactNoInCaseOfEmergency = v.ContactNoInCaseOfEmergency;
            employeeDTO.PreviousCompanyName = v.PreviousCompanyName;
            employeeDTO.ExperienceInYear = v.ExperienceInYear;
            employeeDTO.ExperienceInMonth = v.ExperienceInMonth;
            employeeDTO.ModifyOn = v.ModifyOn.ToString();
            employeeDTO.IsActive = v.IsActive;
          

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
        dto.MobileNumber = vehicleEmployee.MobileNumber;
        dto.DLType = vehicleEmployee.DLType;
        dto.DLValidity = vehicleEmployee.DLValidity;
        dto.Address = vehicleEmployee.Address;
        dto.FatherName = vehicleEmployee.FatherName;
        dto.DateOfBirth = vehicleEmployee.DateOfBirth.Value.ToShortDateString();
        dto.DLNumber = vehicleEmployee.DLNumber;
        dto.DateOfJoining = vehicleEmployee.DateOfJoining.Value.ToShortDateString();
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
}
