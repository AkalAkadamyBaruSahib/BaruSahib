using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AkalAcademy;

/// <summary>
/// Summary description for SecurityRepository
/// </summary>
public class SecurityRepository
{
    private DataContext _context;

    public SecurityRepository(DataContext context)
    {
        _context = context;
    }

    public void AddNewSecurityEmp(SecurityEmployeeInfo securityemp)
    {
        _context.Entry(securityemp).State = EntityState.Added;
        _context.SaveChanges();

    }

    public List<SecurityEmployeeInfoDTO> GetSecurityEmployeeInformation(int SecurityEmployeeID)
    {
        List<SecurityEmployeeInfo> securityemp = _context.SecurityEmployeeInfo.Where(v => v.IsApproved == true)
             .Distinct().OrderByDescending(x => x.CreatedOn).ToList();
        DataSet employeetype = new DataSet();
        List<SecurityEmployeeInfoDTO> dto = new List<SecurityEmployeeInfoDTO>();

        SecurityEmployeeInfoDTO employeeDTO = null;
        foreach (SecurityEmployeeInfo v in securityemp)
        {
            employeeDTO = new SecurityEmployeeInfoDTO();

            employeeDTO.ID = v.ID;
            employeeDTO.Name = v.Name;
            employeeDTO.CreatedOn = v.CreatedOn.ToString();
            employeeDTO.Address = v.Address;
            employeeDTO.Education = v.Education;
            employeeDTO.MobileNo = v.MobileNo;
            employeeDTO.Salary = v.Salary;
            employeeDTO.Deduction = v.Deduction.ToString();
            employeeDTO.ZoneID = v.ZoneID;
            employeeDTO.AcaID = v.AcaID;
            employeeDTO.DesigID = v.DesigID;
            employeeDTO.QualificationLetter = v.QualificationLetter;
            employeeDTO.AppointmentLetter = v.AppointmentLetter;
            employeeDTO.ExperienceLetter = v.ExperienceLetter;
            employeeDTO.FamilyRationCard = v.FamilyRationCard;
            employeeDTO.PCC = v.PCC;
            employeeDTO.ModifyOn = v.ModifyOn.ToString();
            employeeDTO.IsApproved = Convert.ToBoolean(v.IsApproved);
            employeeDTO.Photo = v.Photo;
            employeeDTO.DOJ = v.DOJ.ToString();
            employeeDTO.DateOfAppraisal = v.DateOfAppraisal.ToString();
            employeeDTO.LastAppraisal = v.LastAppraisal;
            dto.Add(employeeDTO);
        }
        return dto;
    }

    public void SecurityEmployeeInfoToDelete(int securityEmployeeID)
    {
        SecurityEmployeeInfo Delsecurityemp = _context.SecurityEmployeeInfo.Where(v => v.ID == securityEmployeeID)
            .FirstOrDefault();
        Delsecurityemp.IsApproved = false;
        _context.Entry(Delsecurityemp).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public SecurityEmployeeInfoDTO GetSecurityEmployeeInfoToUpdate(int SecurityEmployeeID)
    {
        SecurityEmployeeInfo securityemp = _context.SecurityEmployeeInfo.Where(v => v.ID == SecurityEmployeeID)
         .FirstOrDefault();
        SecurityEmployeeInfoDTO dto = new SecurityEmployeeInfoDTO();
        dto.ID = securityemp.ID;
        dto.Name = securityemp.Name;
        dto.Address = securityemp.Address;
        dto.ZoneID = securityemp.ZoneID;
        if (securityemp.AcaID != null && securityemp.AcaID != 0)
        {
            dto.AcaID = securityemp.AcaID;
        }
        dto.DesigID = securityemp.DesigID;
        dto.Education = securityemp.Education;
        dto.Deduction = securityemp.Deduction.ToString();
        dto.MobileNo = securityemp.MobileNo;
        dto.Salary = securityemp.Salary;
        dto.IsApproved = Convert.ToBoolean(securityemp.IsApproved);
        if (securityemp.QualificationLetter != "")
        {
            dto.QualificationLetter = securityemp.QualificationLetter;
        }
        if (securityemp.AppointmentLetter != "")
        {
            dto.AppointmentLetter = securityemp.AppointmentLetter;
        }
        if (securityemp.PCC != "")
        {
            dto.PCC = securityemp.PCC;
        }
        if (securityemp.FamilyRationCard != "")
        {
            dto.FamilyRationCard = securityemp.FamilyRationCard;
        }
        if (securityemp.ExperienceLetter != "")
        {
            dto.ExperienceLetter = securityemp.ExperienceLetter;
        }
        if (securityemp.Photo != "")
        {
            dto.Photo = securityemp.Photo;
        }
        dto.DOJ = securityemp.DOJ.ToString();
        dto.DateOfAppraisal = securityemp.DateOfAppraisal.ToString();
        dto.LastAppraisal = securityemp.LastAppraisal;
        dto.CreatedOn = securityemp.CreatedOn.ToString();
        dto.ModifyOn = securityemp.ModifyOn.ToString();
        return dto;

    }

    public void UpdateSecurityEmp(SecurityEmployeeInfo securityemp)
    {
        SecurityEmployeeInfo newSecurity = _context.SecurityEmployeeInfo.Where(v => v.ID == securityemp.ID)
        .FirstOrDefault();

        newSecurity.Name = securityemp.Name;

        newSecurity.ID = securityemp.ID;
        newSecurity.Name = securityemp.Name;
        newSecurity.Address = securityemp.Address;
        newSecurity.ZoneID = securityemp.ZoneID;
        newSecurity.AcaID = securityemp.AcaID;
        newSecurity.DesigID = securityemp.DesigID;
        newSecurity.Education = securityemp.Education;
        newSecurity.Deduction = securityemp.Deduction;
        newSecurity.MobileNo = securityemp.MobileNo;
        newSecurity.Salary = securityemp.Salary;
        newSecurity.IsApproved = securityemp.IsApproved;
        if (securityemp.QualificationLetter != "")
        {
            newSecurity.QualificationLetter = securityemp.QualificationLetter;
        }
        if (securityemp.AppointmentLetter != "")
        {
            newSecurity.AppointmentLetter = securityemp.AppointmentLetter;
        }
        if (securityemp.PCC != "")
        {
            newSecurity.PCC = securityemp.PCC;
        }
        if (securityemp.FamilyRationCard != "")
        {
            newSecurity.FamilyRationCard = securityemp.FamilyRationCard;
        }
        if (securityemp.ExperienceLetter != "")
        {
            newSecurity.ExperienceLetter = securityemp.ExperienceLetter;
        }
        if (securityemp.Photo != "")
        {
            newSecurity.Photo = securityemp.Photo;
        }
        newSecurity.CreatedOn = securityemp.CreatedOn;
        newSecurity.ModifyOn = securityemp.ModifyOn;
        newSecurity.DOJ = securityemp.DOJ;
        newSecurity.DateOfAppraisal = securityemp.DateOfAppraisal;
        newSecurity.LastAppraisal = securityemp.LastAppraisal;
        _context.Entry(newSecurity).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public List<Zone> GetZone()
    {
        return _context.Zone.ToList();
    }

    public List<Academy> GetAcademybyZoneID(int ZoneID)
    {
        return _context.Academy.Where(x => x.ZoneId == ZoneID).ToList();
    }

    public List<SecurityEmployeeInfo> SecurityEmployeeInfoView()
    {

        return _context.SecurityEmployeeInfo.Where(s => s.IsApproved == true)
            .Include(z => z.Zone)
            .Include(a => a.Academy)
            .OrderByDescending(e => e.CreatedOn).ToList();
    }

    public void SaveSecurityTransferLetter(EmployeeTransfer EmployeeTransfer)
    {
        _context.Entry(EmployeeTransfer).State = System.Data.Entity.EntityState.Added;
        _context.SaveChanges();

        SecurityEmployeeInfo newEmpInfo = _context.SecurityEmployeeInfo.Where(v => v.ID == EmployeeTransfer.EmpID).FirstOrDefault();
        newEmpInfo.AcaID = EmployeeTransfer.NewAcaID;
        newEmpInfo.ZoneID = EmployeeTransfer.NewZoneID;
        newEmpInfo.ModifyOn = DateTime.UtcNow;
        _context.Entry(newEmpInfo).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void DeleteEmployeeInfo(int EID)
    {
        SecurityEmployeeInfo employee = _context.SecurityEmployeeInfo.Where(v => v.ID == EID)
                             .FirstOrDefault();
        employee.IsApproved = false;
        _context.Entry(employee).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void ActiveEmployeeInfo(int EID)
    {
        SecurityEmployeeInfo employee = _context.SecurityEmployeeInfo.Where(v => v.ID == EID)
                             .FirstOrDefault();
        employee.IsApproved = true;
        _context.Entry(employee).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public List<SecurityEmployeeInfo> GetActiveSecurityEmployee()
    {
        List<SecurityEmployeeInfo> mt = new List<SecurityEmployeeInfo>();
        return mt = _context.SecurityEmployeeInfo.Include(u => u.Zone).Include(a => a.Academy).AsEnumerable().Select(x => new SecurityEmployeeInfo
        {
            ID = x.ID,
            Name = x.Name.Trim(),

        }).OrderByDescending(m => m.Name).Reverse().ToList();
    }

    public List<SecurityEmployeeInfo> SearchSecurityEmployeeInfoView(string EmpName)
    {

        return _context.SecurityEmployeeInfo.Where(s => s.Name == EmpName)
            .Include(z => z.Zone)
            .Include(a => a.Academy)
            .OrderByDescending(e => e.CreatedOn).ToList();
    }
}