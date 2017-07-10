using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StudentDetailInTransport
/// </summary>
public class StudentDetailInTransport
{
    public StudentDetailInTransport()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [Key()]
    public int ID { get; set; }

    public int? AdmissionNumber { get; set; }

    public string Class { get; set; }

    public string StudentName { get; set; }

    public string FatherName { get; set; }

    public string ContactNo { get; set; }

    public string NameOfVillage { get; set; }

    public int? CreatedBy { get; set; }

    public int? AcaID { get; set; }

}