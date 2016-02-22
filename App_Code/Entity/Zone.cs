using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Zone
/// </summary>
public class Zone
{
    [Key()]
    public int ZoneId { get; set; }
    public string ZoneName { get; set; }
}