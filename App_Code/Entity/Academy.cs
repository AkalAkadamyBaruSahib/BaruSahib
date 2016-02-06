using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Academy
/// </summary>
public class Academy
{
     [Key()]
    public int AcaID { get; set; }
    public string AcaName { get; set; }
    public int ZoneId { get; set; }
}