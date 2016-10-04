using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AcademyAssignToEmployee
/// </summary>
public class SecurityEmployeeInfoDTO
{
    public SecurityEmployeeInfoDTO()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int ID { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string MobileNo { get; set; }

    public string Salary { get; set; }

    public string Deduction { get; set; }

    public int? AcaID { get; set; }

    public int? ZoneID { get; set; }

    public string Education { get; set; }

    public int? DesigID { get; set; }

    public string AppointmentLetter { get; set; }

    public string ExperienceLetter { get; set; }

    public string FamilyRationCard { get; set; }

    public string PCC { get; set; }

    public string QualificationLetter { get; set; }

    public string CreatedOn { get; set; }

    public string ModifyOn { get; set; }

    public bool IsApproved { get; set; }

    public string Photo { get; set; }

    public string DOJ { get; set; }

    public string DateOfAppraisal { get; set; }

    public string LastAppraisal { get; set; }
   
}