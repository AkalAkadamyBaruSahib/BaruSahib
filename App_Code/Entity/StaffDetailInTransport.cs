using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StaffDetailInTransport
/// </summary>
public class StaffDetailInTransport
{
    public StaffDetailInTransport()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [Key()]
    public int ID { get; set; }

    public string StaffName { get; set; }

    public int? StaffType { get; set; }

    public string FatherName { get; set; }

    public int? CreatedBy { get; set; }

}