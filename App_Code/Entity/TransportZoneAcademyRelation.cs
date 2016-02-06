using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TransportZoneAcademyRelation
/// </summary>
public class TransportZoneAcademyRelation
{
     [Key()]
    public int ID { get; set; }
    public int TransportAcaID { get; set; }
    public int ZoneId { get; set; }
}