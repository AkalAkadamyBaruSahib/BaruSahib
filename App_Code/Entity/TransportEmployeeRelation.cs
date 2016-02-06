using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TransportEmployeeRelation
/// </summary>
public class TransportEmployeeRelation
{

    [Key()]
    public int ID { get; set; }
    public int VehicleEmployeeID { get; set; }
    public string Name { get; set; }
    public string Age { get; set; }
    public string Relation { get; set; }
    public bool? Nominee { get; set; }
    public string PhoneNo { get; set; }
    public string Address { get; set; }
    public int RelationTypeID { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifyOn { get; set; }


}