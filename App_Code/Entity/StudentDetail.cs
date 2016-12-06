using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StudentDetail
/// </summary>
public class StudentDetail
{
    public StudentDetail()
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

    public int? CountryID { get; set; }

    public int? StateID { get; set; }

    public int? CityID { get; set; }

    public string Address { get; set; }

}